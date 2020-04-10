using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("The number of goals")]
    private int m_numGoals;

    [SerializeField]
    [Tooltip("The text object for when player beats the level")]
    private Text m_VictoryText;

    [SerializeField]
    [Tooltip("The text to show when the player beats the level")]
    private string m_VictoryMessage;

    [SerializeField]
    [Tooltip("The slider to adjust move speed")]
    private Slider m_MoveSpeedSlider;

    [SerializeField]
    [Tooltip("The speed of player")]
    private float m_defaultSpeed;


    #endregion

    #region Private Variables
    private int numMovingPlayers;
    private List<PowerUpScript> activePowerUps = new List<PowerUpScript>();
    private int numInGoal;
    private GameObject[] boxes;
    private GameObject[] players;
    private float m_speed;
    private int offsetX;
    private int offsetY;
    private GameObject[,] worldMatrix;
    private GameObject[,] conveyorMatrix;
    private GameObject[,] telepMatrix;
    private int dimX;
    private int dimY;
    private GameObject[,] prevLocations;
    private bool movingConveyors = false;
    #endregion

    #region Public Variables
    public bool playersCanMove;
    public int moveDist;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        numInGoal = 0;
        numMovingPlayers = 0;
        moveDist = 1;
        playersCanMove = true;
        m_VictoryText.text = "";
        boxes = GameObject.FindGameObjectsWithTag("Box");
        players = GameObject.FindGameObjectsWithTag("Player");
        m_MoveSpeedSlider.onValueChanged.AddListener(delegate { SliderValueChanged(m_MoveSpeedSlider); });
        m_speed = m_defaultSpeed;

        float minX = 100;//the X coordinate of the furthest left object
        float minY = 100;//the Y coordinate of the furthest down object
        float maxX = -100;//the X coordinate of the furthest right object
        float maxY = -100;//the Y coordinate of the furthest up object

        object[] obj = GameObject.FindObjectsOfType(typeof(GameObject));
        foreach (object o in obj)
        {
            GameObject g = (GameObject)o;
            if (g.layer == LayerMask.NameToLayer("Player") || g.layer == LayerMask.NameToLayer("Box") || g.layer == LayerMask.NameToLayer("Wall") || g.layer == LayerMask.NameToLayer("Conveyor") || g.layer == LayerMask.NameToLayer("Door"))
            {
                float gX = g.transform.position.x;
                float gY = g.transform.position.y;
                if (gX < minX)
                {
                    minX = gX;
                }
                else if (gX > maxX)
                {
                    maxX = gX;
                }
                if (gY < minY)
                {
                    minY = gY;
                }
                else if (gY > maxY)
                {
                    maxY = gY;
                }
            }
        }
        dimX = (int)Math.Round(maxX) - (int)Math.Round(minX) + 1;
        dimY = (int)Math.Round(maxY) - (int)Math.Round(minY) + 1;
        offsetX = (int)Math.Round(minX);
        offsetY = (int)Math.Round(minY);
        worldMatrix = new GameObject[dimX,dimY];
        conveyorMatrix = new GameObject[dimX, dimY];
        telepMatrix = new GameObject[dimX, dimY];
        foreach (object o in obj)
        {
            GameObject g = (GameObject)o;
            if(g.layer ==LayerMask.NameToLayer("Player") || g.layer == LayerMask.NameToLayer("Box") || g.layer == LayerMask.NameToLayer("Wall") || g.layer == LayerMask.NameToLayer("Door"))
            {
                int gX = (int)Math.Round(g.transform.position.x) - offsetX;
                int gY = (int)Math.Round(g.transform.position.y) - offsetY;
                worldMatrix[gX, gY] = g;
            }
            else if(g.layer == LayerMask.NameToLayer("Conveyor"))
            {
                int gX = (int)Math.Round(g.transform.position.x) - offsetX;
                int gY = (int)Math.Round(g.transform.position.y) - offsetY;
                conveyorMatrix[gX, gY] = g;
            }
            else if (g.layer == LayerMask.NameToLayer("Teleporter"))
            {
                int gX = (int)Math.Round(g.transform.position.x) - offsetX;
                int gY = (int)Math.Round(g.transform.position.y) - offsetY;
                telepMatrix[gX, gY] = g;
            }
        }

        for (int x = 0; x < dimX; x++)
        {
            for (int y = 0; y < dimY; y++)
            {
                if (worldMatrix[x, y] != null)
                {
                    if (worldMatrix[x, y].tag == "Player")
                    {
                        worldMatrix[x, y].GetComponent<PlayerController>().m_speed = m_defaultSpeed;
                    }
                    else if (worldMatrix[x, y].tag == "Box")
                    {
                        worldMatrix[x, y].GetComponent<BoxController>().m_speed = m_defaultSpeed;
                    }
                }
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        if(playersCanMove)
        {
            int dX = 0;
            int dY = 0;
            if(Input.GetKey(KeyCode.W))
            {
                dY = 1;
                playersCanMove = false;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                dX = -1;
                playersCanMove = false;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dY = -1;
                playersCanMove = false;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dX = 1;
                playersCanMove = false;
            }
            else if (Input.GetKey(KeyCode.E))
            {
                playersCanMove = false;
            }
            if(!playersCanMove)
            {
                //Calculate Player movements
                #region Player Movement
                for (int x = 0; x < dimX; x++)
                {
                    for (int y = 0; y < dimY; y++)
                    {
                        if (worldMatrix[x, y] != null)
                        {
                            if (worldMatrix[x, y].tag == "Player")
                            {
                                if(worldMatrix[x, y].GetComponent<PlayerController>().moveDist<0)
                                {
                                    dX *= -1;
                                    dY *= -1;
                                }
                                worldMatrix[x, y].GetComponent<PlayerController>().moveDistLeft = Math.Abs(worldMatrix[x,y].GetComponent<PlayerController>().moveDist);
                            }

                        }
                    }
                }

                prevLocations = new GameObject[worldMatrix.GetLength(0), worldMatrix.GetLength(1)];
                while (!arraysEqual(worldMatrix, prevLocations))
                {
                    Array.Copy(worldMatrix, 0, prevLocations, 0, worldMatrix.Length);

                    for (int x = 0; x < dimX; x++)
                    {
                        for (int y = 0; y < dimY; y++)
                        {
                            if (worldMatrix[x, y] != null && worldMatrix[x, y].tag == "Player" && worldMatrix[x, y].GetComponent<PlayerController>().moveDistLeft>0)
                            {
                                if (worldMatrix[x + dX, y + dY] == null)//there is no object in front of player, they can move and be donw
                                {
                                    worldMatrix[x, y].GetComponent<PlayerController>().moveDistLeft -= 1;
                                    worldMatrix[x + dX, y + dY] = worldMatrix[x, y];
                                    worldMatrix[x, y] = null;
                                }
                                else if (worldMatrix[x + dX, y + dY].tag == "Wall")//there is a wall in front of player, they can't move and they are done
                                {
                                    worldMatrix[x, y].GetComponent<PlayerController>().moveDistLeft = 0;
                                }
                                else if (worldMatrix[x + dX, y + dY].tag == "Box")//there is a box that must be pushed, tell the box to push and remember that the player isnt done moving
                                {
                                    /*if (!worldMatrix[x + dX, y + dY].GetComponent<BoxController>().stillMoving)//box cant be pushed treat it as a wall
                                    {
                                        worldMatrix[x, y].GetComponent<PlayerController>().moveDistLeft = 0;
                                    }
                                    else
                                    {*/
                                        int dist = 2;
                                        while ((x + dX * dist) >= 0 && (x + dX * dist) < dimX && (y + dY * dist) >= 0 && (y + dY * dist) < dimY)//should always end through break, but this is here just in case
                                        {
                                            if (worldMatrix[x + dX * dist, y + dY * dist] == null)
                                            {
                                                for (int i = dist - 1; i >= 0; i--)
                                                {
                                                    if (worldMatrix[x + dX * i, y + dY * i] == null)
                                                    {
                                                        print("error, somehow reaching null");
                                                    }
                                                    else if (worldMatrix[x + dX * i, y + dY * i].tag == "Box")
                                                    {
                                                        //worldMatrix[x + dX * i, y + dY * i].GetComponent<BoxController>().stillMoving = true;
                                                        worldMatrix[x + dX * (i + 1), y + dY * (i + 1)] = worldMatrix[x + dX * i, y + dY * i];
                                                        worldMatrix[x + dX * i, y + dY * i] = null;

                                                    }
                                                    else if (worldMatrix[x + dX * i, y + dY * i].tag == "Player")
                                                    {
                                                        worldMatrix[x + dX * i, y + dY * i].GetComponent<PlayerController>().moveDistLeft -= 1;
                                                        worldMatrix[x + dX * (i + 1), y + dY * (i + 1)] = worldMatrix[x + dX * i, y + dY * i];
                                                        worldMatrix[x + dX * i, y + dY * i] = null;
                                                    }
                                                }
                                                break;
                                            }
                                            else if (worldMatrix[x + dX * dist, y + dY * dist].tag == "Wall") //|| (worldMatrix[x + dX * dist, y + dY * dist].tag == "Box" && !worldMatrix[x + dX * dist, y + dY * dist].GetComponent<PlayerController> ().stillMoving))
                                            {
                                                for (int i = dist - 1; i >= 0; i--)
                                                {
                                                    if (worldMatrix[x + dX * i, y + dY * i] == null)
                                                    {
                                                        print("error, somehow reaching null");
                                                    }
                                                    else if (worldMatrix[x + dX * i, y + dY * i].tag == "Box")
                                                    {
                                                        worldMatrix[x + dX * i, y + dY * i].GetComponent<BoxController>().stillMoving = false;
                                                    }
                                                    else if (worldMatrix[x + dX * i, y + dY * i].tag == "Player")
                                                    {
                                                        worldMatrix[x + dX * i, y + dY * i].GetComponent<PlayerController>().moveDistLeft = 0;
                                                    }
                                                }
                                                break;
                                            }
                                            dist += 1;
                                        }
                                    //}
                                }
                            }
                        }
                    }
                }
                #endregion

                //Tell of the objects to move to their new spots
                for (int x = 0; x < dimX; x++)
                {
                    for (int y = 0; y < dimY; y++)
                    {
                        if (worldMatrix[x, y] != null)
                        {
                            if (worldMatrix[x, y].tag == "Player")
                            {
                                Vector3 finalPos = new Vector3(x + offsetX, y + offsetY);
                                worldMatrix[x, y].GetComponent<PlayerController>().MoveTo(finalPos);
                                worldMatrix[x, y].GetComponent<PlayerController>().stillMoving = true;
                            }
                            else if (worldMatrix[x, y].tag == "Box")
                            {
                                Vector3 finalPos = new Vector3(x + offsetX, y + offsetY);
                                worldMatrix[x, y].GetComponent<BoxController>().MoveTo(finalPos);
                                worldMatrix[x, y].GetComponent<BoxController>().stillMoving = true;
                            }
                        }
                    }
                }
                //movingConveyors = false;
                //Calculate Conveyor movements
                #region Conveyor Movement
                for (int x = 0; x < dimX; x++)
                {
                    for (int y = 0; y < dimY; y++)
                    {
                        if (worldMatrix[x, y] != null)
                        {
                            if (worldMatrix[x, y].tag == "Player" && worldMatrix[x, y].GetComponent<PlayerController>().stillMoving)
                            {
                                GameObject player = worldMatrix[x, y];
                                int convDir = 0;//0=none, 1=up, 2=right, 3=down, 4=left
                                if (conveyorMatrix[x, y] != null)
                                {
                                    convDir = conveyorMatrix[x, y].GetComponent<ConveyorBeltController>().direction;
                                }
                                if (convDir == 1)
                                {
                                    dX = 0;
                                    dY = 1;
                                }
                                else if (convDir == 2)
                                {
                                    dX = 1;
                                    dY = 0;
                                }
                                else if (convDir == 3)
                                {
                                    dX = 0;
                                    dY = -1;
                                }
                                else if (convDir == 4)
                                {
                                    dX = -1;
                                    dY = 0;
                                }
                                if (convDir != 0)
                                {
                                    prevLocations = new GameObject[worldMatrix.GetLength(0), worldMatrix.GetLength(1)];
                                    while (!arraysEqual(prevLocations, worldMatrix))
                                    {
                                        Array.Copy(worldMatrix, 0, prevLocations, 0, worldMatrix.Length);
                                        if (worldMatrix[x + dX, y + dY] == null)//there is no object in front of player, they can move and be donw
                                        {
                                            worldMatrix[x, y].GetComponent<PlayerController>().stillMoving = false;
                                            worldMatrix[x + dX, y + dY] = worldMatrix[x, y];
                                            worldMatrix[x, y] = null;
                                        }
                                        else if (worldMatrix[x + dX, y + dY].tag == "Wall")//there is a wall in front of player, they can't move and they are done
                                        {
                                            worldMatrix[x, y].GetComponent<PlayerController>().stillMoving = false;
                                        }
                                        else if (worldMatrix[x + dX, y + dY].tag == "Box")//there is a box that must be pushed, tell the box to push and remember that the player isnt done moving
                                        {
                                            if (!worldMatrix[x + dX, y + dY].GetComponent<BoxController>().stillMoving)//box cant be pushed treat it as a wall
                                            {
                                                worldMatrix[x, y].GetComponent<PlayerController>().stillMoving = false;
                                            }
                                            else
                                            {
                                                int dist = 2;
                                                while ((x + dX * dist) >= 0 && (x + dX * dist) < dimX && (y + dY * dist) >= 0 && (y + dY * dist) < dimY)//should always end through break, but this is here just in case
                                                {
                                                    if (worldMatrix[x + dX * dist, y + dY * dist] == null)
                                                    {
                                                        for (int i = dist - 1; i >= 0; i--)
                                                        {
                                                            if (worldMatrix[x + dX * i, y + dY * i] == null)
                                                            {
                                                                print("error, somehow reaching null");
                                                            }
                                                            else if (worldMatrix[x + dX * i, y + dY * i].tag == "Box")
                                                            {
                                                                worldMatrix[x + dX * i, y + dY * i].GetComponent<BoxController>().stillMoving = false;
                                                                worldMatrix[x + dX * (i + 1), y + dY * (i + 1)] = worldMatrix[x + dX * i, y + dY * i];
                                                                worldMatrix[x + dX * i, y + dY * i] = null;

                                                            }
                                                            else if (worldMatrix[x + dX * i, y + dY * i].tag == "Player")
                                                            {
                                                                worldMatrix[x + dX * i, y + dY * i].GetComponent<PlayerController>().stillMoving = false;
                                                                worldMatrix[x + dX * (i + 1), y + dY * (i + 1)] = worldMatrix[x + dX * i, y + dY * i];
                                                                worldMatrix[x + dX * i, y + dY * i] = null;
                                                            }
                                                        }
                                                        break;
                                                    }
                                                    else if (worldMatrix[x + dX * dist, y + dY * dist].tag == "Wall") //|| (worldMatrix[x + dX * dist, y + dY * dist].tag == "Box" && !worldMatrix[x + dX * dist, y + dY * dist].GetComponent<PlayerController> ().stillMoving))
                                                    {
                                                        for (int i = dist - 1; i >= 0; i--)
                                                        {
                                                            if (worldMatrix[x + dX * i, y + dY * i] == null)
                                                            {
                                                                print("error, somehow reaching null");
                                                            }
                                                            else if (worldMatrix[x + dX * i, y + dY * i].tag == "Box")
                                                            {
                                                                worldMatrix[x + dX * i, y + dY * i].GetComponent<BoxController>().stillMoving = false;
                                                            }
                                                            else if (worldMatrix[x + dX * i, y + dY * i].tag == "Player")
                                                            {
                                                                worldMatrix[x + dX * i, y + dY * i].GetComponent<PlayerController>().stillMoving = false;
                                                            }
                                                        }
                                                        break;
                                                    }
                                                    dist += 1;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else if (worldMatrix[x, y].tag == "Box" && worldMatrix[x, y].GetComponent<BoxController>().stillMoving)
                            {
                                GameObject box = worldMatrix[x, y];
                                int convDir = 0;//0=none, 1=up, 2=right, 3=down, 4=left
                                if (conveyorMatrix[x, y] != null)
                                {
                                    convDir = conveyorMatrix[x, y].GetComponent<ConveyorBeltController>().direction;
                                }
                                if (convDir == 1)
                                {
                                    dX = 0;
                                    dY = 1;
                                }
                                else if (convDir == 2)
                                {
                                    dX = 1;
                                    dY = 0;
                                }
                                else if (convDir == 3)
                                {
                                    dX = 0;
                                    dY = -1;
                                }
                                else if (convDir == 4)
                                {
                                    dX = -1;
                                    dY = 0;
                                }
                                if (convDir != 0)
                                {
                                    prevLocations = new GameObject[worldMatrix.GetLength(0), worldMatrix.GetLength(1)];
                                    while (!arraysEqual(prevLocations, worldMatrix))
                                    {
                                        Array.Copy(worldMatrix, 0, prevLocations, 0, worldMatrix.Length);
                                        if (worldMatrix[x + dX, y + dY] == null)//there is no object in front of player, they can move and be donw
                                        {
                                            worldMatrix[x, y].GetComponent<BoxController>().stillMoving = false;
                                            worldMatrix[x + dX, y + dY] = worldMatrix[x, y];
                                            worldMatrix[x, y] = null;
                                        }
                                        else if (worldMatrix[x + dX, y + dY].tag == "Wall")//there is a wall in front of player, they can't move and they are done
                                        {
                                            box.GetComponent<BoxController>().stillMoving = false;
                                        }
                                        else if (worldMatrix[x + dX, y + dY].tag == "Box")//there is a box that must be pushed, tell the box to push and remember that the player isnt done moving
                                        {
                                            if (!worldMatrix[x + dX, y + dY].GetComponent<BoxController>().stillMoving)//box cant be pushed treat it as a wall
                                            {
                                                box.GetComponent<BoxController>().stillMoving = false;
                                            }
                                            else
                                            {
                                                int dist = 2;
                                                while ((x + dX * dist) >= 0 && (x + dX * dist) < dimX && (y + dY * dist) >= 0 && (y + dY * dist) < dimY)//should always end through break, but this is here just in case
                                                {
                                                    if (worldMatrix[x + dX * dist, y + dY * dist] == null)
                                                    {
                                                        for (int i = dist - 1; i >= 0; i--)
                                                        {
                                                            if (worldMatrix[x + dX * i, y + dY * i] == null)
                                                            {
                                                                print("error, somehow reaching null");
                                                            }
                                                            else if (worldMatrix[x + dX * i, y + dY * i].tag == "Box")
                                                            {
                                                                worldMatrix[x + dX * i, y + dY * i].GetComponent<BoxController>().stillMoving = false;
                                                                worldMatrix[x + dX * (i + 1), y + dY * (i + 1)] = worldMatrix[x + dX * i, y + dY * i];
                                                                worldMatrix[x + dX * i, y + dY * i] = null;

                                                            }
                                                            else if (worldMatrix[x + dX * i, y + dY * i].tag == "Player")
                                                            {
                                                                worldMatrix[x + dX * i, y + dY * i].GetComponent<PlayerController>().stillMoving = false;
                                                                worldMatrix[x + dX * (i + 1), y + dY * (i + 1)] = worldMatrix[x + dX * i, y + dY * i];
                                                                worldMatrix[x + dX * i, y + dY * i] = null;
                                                            }
                                                        }
                                                        break;
                                                    }
                                                    else if (worldMatrix[x + dX * dist, y + dY * dist].tag == "Wall") //|| (worldMatrix[x + dX * dist, y + dY * dist].tag == "Box" && !worldMatrix[x + dX * dist, y + dY * dist].GetComponent<PlayerController> ().stillMoving))
                                                    {
                                                        for (int i = dist - 1; i >= 0; i--)
                                                        {
                                                            if (worldMatrix[x + dX * i, y + dY * i] == null)
                                                            {
                                                                print("error, somehow reaching null");
                                                            }
                                                            else if (worldMatrix[x + dX * i, y + dY * i].tag == "Box")
                                                            {
                                                                worldMatrix[x + dX * i, y + dY * i].GetComponent<BoxController>().stillMoving = false;
                                                            }
                                                            else if (worldMatrix[x + dX * i, y + dY * i].tag == "Player")
                                                            {
                                                                worldMatrix[x + dX * i, y + dY * i].GetComponent<PlayerController>().stillMoving = false;
                                                            }
                                                        }
                                                        break;
                                                    }
                                                    dist += 1;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion



                //Tell of the objects to move to their new spots
                for (int x = 0; x < dimX; x++)
                {
                    for (int y = 0; y < dimY; y++)
                    {
                        if (worldMatrix[x,y]!=null)
                        {
                            if(worldMatrix[x,y].tag=="Player")
                            {
                                Vector3 finalPos = new Vector3(x + offsetX, y + offsetY);
                                worldMatrix[x, y].GetComponent<PlayerController>().MoveTo(finalPos);
                                worldMatrix[x, y].GetComponent<PlayerController>().stillMoving = true;
                            }
                            else if (worldMatrix[x, y].tag == "Box")
                            {
                                Vector3 finalPos = new Vector3(x + offsetX, y + offsetY);
                                if (telepMatrix[x,y]!=null)
                                {
                                    int[] tpLoc = telepMatrix[x, y].GetComponent<TeleportController>().getTPLoc();
                                    int tpX = tpLoc[0];
                                    int tpY = tpLoc[1];
                                    if(worldMatrix[x+tpX,y+tpY]==null)
                                    {
                                        worldMatrix[x + tpX, y + tpY] = worldMatrix[x, y];
                                        worldMatrix[x, y] = null; 
                                        finalPos = new Vector3(x+offsetX+tpX,y+offsetY+tpY);
                                    }
                                }
                                worldMatrix[x, y].GetComponent<BoxController>().MoveTo(finalPos);
                                worldMatrix[x, y].GetComponent<BoxController>().stillMoving = true;
                            }
                        }
                    }
                }
                //StartCoroutine(WaitCoroutine());
            }
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            SceneManager.LoadScene("HubWorld");
        }

    }
    
    /*public void readyToMoveConveyors()
    {
        if(!movingConveyors)
        {
            movingConveyors = true;
            //Tell of the objects to move to their new spots
            for (int x = 0; x < dimX; x++)
            {
                for (int y = 0; y < dimY; y++)
                {
                    if (worldMatrix[x, y] != null)
                    {
                        if (worldMatrix[x, y].tag == "Player")
                        {
                            Vector3 finalPos = new Vector3(x + offsetX, y + offsetY);
                            worldMatrix[x, y].GetComponent<PlayerController>().MoveTo(finalPos);
                            worldMatrix[x, y].GetComponent<PlayerController>().stillMoving = true;
                        }
                        else if (worldMatrix[x, y].tag == "Box")
                        {
                            Vector3 finalPos = new Vector3(x + offsetX, y + offsetY);
                            worldMatrix[x, y].GetComponent<BoxController>().MoveTo(finalPos);
                            worldMatrix[x, y].GetComponent<BoxController>().stillMoving = true;
                        }
                    }
                }
            }
        }
    }*/

    bool arraysEqual(GameObject[,] a1, GameObject[,]a2)
    {
        for(int x = 0; x< dimX; x++)
        {
            for(int y = 0; y< dimY;y++)
            {
                if(a1[x,y] != a2[x,y])
                {
                    return false;
                }
            }
        }
        return true;
    }

    void SliderValueChanged(Slider changed)
    {
        float newMoveSpeed = m_defaultSpeed + (5 - changed.value) * 0.1f;
        m_speed = newMoveSpeed;
        for (int x = 0; x < dimX; x++)
        {
            for (int y = 0; y < dimY; y++)
            {
                if (worldMatrix[x, y] != null)
                {
                    if (worldMatrix[x, y].tag == "Player")
                    {
                        worldMatrix[x, y].GetComponent<PlayerController>().m_speed = newMoveSpeed;
                    }
                    else if (worldMatrix[x, y].tag == "Box")
                    {
                        worldMatrix[x, y].GetComponent<BoxController>().m_speed = newMoveSpeed;
                    }
                }
            }
        }
    }

    public void IncreaseNumMoving(bool fromBox = false)
    {
        numMovingPlayers += 1;
        StartCoroutine(waitOneUpdateCoroutine());
    }


    public void addToMatrix(GameObject g)
    {
        int gX = (int)Math.Round(g.transform.position.x) - offsetX;
        int gY = (int)Math.Round(g.transform.position.y) - offsetY;
        if(gX>=0 && gX<dimX && gY>=0 && gY<dimY)
        {
            if(worldMatrix[gX, gY]!=null && worldMatrix[gX, gY].tag=="Player")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            worldMatrix[gX, gY] = g;
        }
        
    }

    public void removeFromMatrix(GameObject g)
    {
        for (int x = 0; x < dimX; x++)
        {
            for (int y = 0; y < dimY; y++)
            {
                if(worldMatrix[x,y] != null)
                {
                    if (worldMatrix[x, y].Equals(g))
                    {
                        worldMatrix[x, y] = null;
                    }
                }
            }
        }
    }

    public void DecreaseNumMoving()
    {
        numMovingPlayers -= 1;
        if(numMovingPlayers <= 0)
        {
            numMovingPlayers = 0;
            playersCanMove = true;
            //iterate through list backward to prevent modification while iteration issue
            for (int i = activePowerUps.Count - 1; i >= 0; i--)
            {
                activePowerUps[i].increaseMoveCount();
            }
        }   
    }

    public void IncreaseInGoal()
    {
        numInGoal += 1;
        if(numInGoal >= m_numGoals)
        {
            win();
        }
    }

    public void DecreaseInGoal()
    {
        numInGoal -= 1;
    }

    public void addPowerUp(PowerUpScript pow)
    {
        activePowerUps.Add(pow);
    }

    public void removePowerUp(PowerUpScript pow)
    {
        activePowerUps.Remove(pow);
    }

    public void win()
    {
        m_VictoryText.text = m_VictoryMessage;
        playersCanMove = false;
        StartCoroutine(freezeCoroutine(3));    
    }

    IEnumerator WaitCoroutine()
    {
        //yield return new WaitForSeconds(m_speed);
        yield return new WaitForSeconds(0.5f);
        playersCanMove = true;
    }

    IEnumerator freezeCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("HubWorld");
    }
    IEnumerator waitOneUpdateCoroutine() // wait one update to change the players can move value (without this, only the first player can move)
    {
        int i = 0;
        while(i<1)
        {
            i++;
            yield return null;
        }
        playersCanMove = false;
    }
}
