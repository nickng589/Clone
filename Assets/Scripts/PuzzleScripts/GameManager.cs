using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;

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
    [Tooltip("The speed of player")]
    private float m_defaultSpeed;

    [SerializeField]
    [Tooltip("The text file for level order")]
    public TextAsset textFile;


    
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
    private string text;
    private string[] lines;

    private float slowAnimSpeed=0.1f;
    private float fastAnimSpeed=1;
    private bool lockGame = false;

    private AudioSource source;
    private AudioClip footStep1;
    private AudioClip footStep2;
    private AudioClip footStep3;
    private AudioClip doorOpenClose;
    private AudioClip boxBreak;
    private AudioClip buttonDown;
    private AudioClip buttonUp;
    #endregion

    #region Public Variables
    public bool playersCanMove;
    public int moveDist;
    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        source = audioSources[0];
        footStep1 = audioSources[0].clip;
        footStep2 = audioSources[1].clip;
        footStep3 = audioSources[2].clip;
        doorOpenClose = audioSources[3].clip;
        boxBreak = audioSources[4].clip;
        buttonDown = audioSources[5].clip;
        buttonUp = audioSources[6].clip;

        text = textFile.text;
        lines = text.Split('\n');
        numInGoal = 0;
        numMovingPlayers = 0;
        moveDist = 1;
        playersCanMove = true;
        m_VictoryText.text = "";
        boxes = GameObject.FindGameObjectsWithTag("Box");
        players = GameObject.FindGameObjectsWithTag("Player");
        if(PlayerPrefs.HasKey("MoveSpeed"))
        {
            m_speed = PlayerPrefs.GetFloat("MoveSpeed");
        }
        else
        {
            m_speed = m_defaultSpeed;
        }
       

        float minX = float.MaxValue;//the X coordinate of the furthest left object
        float minY = float.MaxValue;//the Y coordinate of the furthest down object
        float maxX = float.MinValue;//the X coordinate of the furthest right object
        float maxY = float.MinValue;//the Y coordinate of the furthest up object

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
            if(g.layer ==LayerMask.NameToLayer("Player") || g.layer == LayerMask.NameToLayer("Box") || g.layer == LayerMask.NameToLayer("Wall"))
            {
                int gX = (int)Math.Round(g.transform.position.x) - offsetX;
                int gY = (int)Math.Round(g.transform.position.y) - offsetY;
                worldMatrix[gX, gY] = g;
            }
            /*else if(g.tag =="Door" && !g.GetComponent<DoorController>().m_startOpen)
            {
                int gX = (int)Math.Round(g.transform.position.x) - offsetX;
                int gY = (int)Math.Round(g.transform.position.y) - offsetY;
                worldMatrix[gX, gY] = g;
            }*/
            else if(g.layer == LayerMask.NameToLayer("Conveyor"))
            {
                int gX = (int)Math.Round(g.transform.position.x) - offsetX;
                int gY = (int)Math.Round(g.transform.position.y) - offsetY;
                conveyorMatrix[gX, gY] = g;
                g.GetComponent<Animator>().speed = slowAnimSpeed;
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
                        worldMatrix[x, y].GetComponent<PlayerController>().m_speed = m_speed;
                    }
                    else if (worldMatrix[x, y].tag == "Box")
                    {
                        worldMatrix[x, y].GetComponent<BoxController>().m_speed = m_speed;
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
            bool anim_left = false;
            bool anim_right = false;
            bool anim_up = false;
            bool anim_down = false;
            if (Input.GetKey(KeyCode.W))
            {
                dY = 1;
                anim_up = true;
                playersCanMove = false;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                dX = -1;
                anim_left = true;
                playersCanMove = false;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dY = -1;
                anim_down = true;
                playersCanMove = false;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dX = 1;
                anim_right = true;
                playersCanMove = false;
            }
            else if (Input.GetKey(KeyCode.E))
            {
                playersCanMove = false;
            }
            if (!playersCanMove)
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
                                worldMatrix[x, y].GetComponent<PlayerController>().moveDistLeft = Math.Abs(worldMatrix[x, y].GetComponent<PlayerController>().moveDist);
                            }
                        }
                        if (conveyorMatrix[x, y] != null)
                        {
                            conveyorMatrix[x, y].GetComponent<Animator>().speed = fastAnimSpeed;
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
                            if (worldMatrix[x, y] != null && worldMatrix[x, y].tag == "Player" && worldMatrix[x, y].GetComponent<PlayerController>().moveDistLeft > 0)
                            {
                                GameObject player = worldMatrix[x, y];
                                if (player.GetComponent<PlayerController>().inverted)
                                {
                                    dX *= -1;
                                    dY *= -1;
                                }
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
                                        else if (worldMatrix[x + dX * dist, y + dY * dist].tag == "Wall" || worldMatrix[x + dX * dist, y + dY * dist].tag == "Door") //|| (worldMatrix[x + dX * dist, y + dY * dist].tag == "Box" && !worldMatrix[x + dX * dist, y + dY * dist].GetComponent<PlayerController> ().stillMoving))
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
                                if (player.GetComponent<PlayerController>().inverted)
                                {
                                    dX *= -1;
                                    dY *= -1;
                                }
                            }
                        }
                    }
                }
                #endregion

                //Tell objects what the midpoint of their movement is
                for (int x = 0; x < dimX; x++)
                {
                    for (int y = 0; y < dimY; y++)
                    {
                        if (worldMatrix[x, y] != null)
                        {
                            if (worldMatrix[x, y].tag == "Player")
                            {
                                Vector3 midPos = new Vector3(x + offsetX, y + offsetY);
                                worldMatrix[x, y].GetComponent<PlayerController>().midpoint = midPos;
                                worldMatrix[x, y].GetComponent<PlayerController>().stillMoving = true;
                            }
                            else if (worldMatrix[x, y].tag == "Box")
                            {
                                Vector3 midPos = new Vector3(x + offsetX, y + offsetY);
                                worldMatrix[x, y].GetComponent<BoxController>().midpoint = midPos;
                                worldMatrix[x, y].GetComponent<BoxController>().stillMoving = true;
                            }
                        }
                    }
                }
                //movingConveyors = false;
                //Calculate Conveyor movements
                #region Conveyor Movement
                prevLocations = new GameObject[worldMatrix.GetLength(0), worldMatrix.GetLength(1)];
                while (!arraysEqual(prevLocations, worldMatrix))
                {
                    Array.Copy(worldMatrix, 0, prevLocations, 0, worldMatrix.Length);
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
                                            /*else
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
                                            }*/
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
                                            /*else
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
                                            }*/
                                        }
                                    }

                                }
                            }
                        }
                    }
                }

                #endregion

                //Calculate conveyor Pushes
                #region Conveyor Pushes
                prevLocations = new GameObject[worldMatrix.GetLength(0), worldMatrix.GetLength(1)];
                while (!arraysEqual(prevLocations, worldMatrix))
                {
                    Array.Copy(worldMatrix, 0, prevLocations, 0, worldMatrix.Length);
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
                                PlayerController pc = worldMatrix[x, y].GetComponent<PlayerController>();
                                pc.AnimatePlayer(anim_up, anim_down, anim_left, anim_right);
                                pc.MoveTo(worldMatrix[x, y].transform.position, finalPos);
                                pc.stillMoving = true;
                            }
                            else if (worldMatrix[x, y].tag == "Box")
                            {
                                GameObject box = worldMatrix[x, y];
                                if (!box.GetComponent<BoxController>().teleported)
                                {
                                    Vector3 finalPos = new Vector3(x + offsetX, y + offsetY);
                                    if (telepMatrix[x, y] != null)
                                    {
                                        int[] tpLoc = telepMatrix[x, y].GetComponent<TeleportController>().getTPLoc();
                                        int tpX = tpLoc[0];
                                        int tpY = tpLoc[1];
                                        if (worldMatrix[x + tpX, y + tpY] == null)
                                        {
                                            worldMatrix[x + tpX, y + tpY] = worldMatrix[x, y];
                                            worldMatrix[x, y] = null;
                                            box.GetComponent<BoxController>().TeleportTo(new Vector3(x + offsetX + tpX, y + offsetY + tpY));
                                            finalPos = new Vector3(x + offsetX + tpX, y + offsetY + tpY);
                                        }
                                    }
                                    box.GetComponent<BoxController>().MoveTo(box.transform.position, finalPos);
                                    box.GetComponent<BoxController>().stillMoving = true;
                                }
                            }
                        }
                    }
                }
                for (int x = 0; x < dimX; x++)
                {
                    for (int y = 0; y < dimY; y++)
                    {
                        if (worldMatrix[x, y] != null)
                        {
                            if (worldMatrix[x, y].tag == "Box")
                            {
                                GameObject box = worldMatrix[x, y];
                                box.GetComponent<BoxController>().teleported = false;
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
            PlayerPrefs.SetInt("CurrentLevel", PlayerPrefs.GetInt("CurrentLevel") - 1);
            SceneManager.LoadScene("HubWorld");
        }

    }
    

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

    public void UpdateMoveSpeed(float newMoveSpeed)
    {
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
            if(worldMatrix[gX, gY]!=null)
            {
                if (worldMatrix[gX, gY].tag == "Player")
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                if (worldMatrix[gX, gY].tag == "Box")
                {
                    source.PlayOneShot(boxBreak);
                    DecreaseNumMoving();
                    Destroy(worldMatrix[gX, gY]);
                }
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

    #region sounds
    private bool doorCanPlay = true;
    public void playDoorSound()
    {
        if(doorCanPlay)
        {
            source.PlayOneShot(doorOpenClose);
            doorCanPlay = false;
            StartCoroutine(waitDoorCoroutine());
        }
    }

    IEnumerator waitDoorCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        doorCanPlay = true;
    }

    public void playPlayerSound()
    {
        float rand = UnityEngine.Random.Range(0f, 3f);
        if (rand < 1f)
        {
            source.PlayOneShot(footStep1);
        }
        else if (rand < 2f)
        {
            source.PlayOneShot(footStep2);
        }
        else
        {
            source.PlayOneShot(footStep3);
        }
    }

    public void playButtonDown()
    {
        source.PlayOneShot(buttonDown);
    }
    public void playButtonUp()
    {
        source.PlayOneShot(buttonUp);
    }
    #endregion


    public void DecreaseNumMoving()
    {
        numMovingPlayers -= 1;
        if(numMovingPlayers <= 0 && !lockGame)
        {
            numMovingPlayers = 0;
            playersCanMove = true;
            //iterate through list backward to prevent modification while iteration issue
            for (int i = activePowerUps.Count - 1; i >= 0; i--)
            {
                activePowerUps[i].increaseMoveCount();
            }
            for (int x = 0; x < dimX; x++)
            {
                for (int y = 0; y < dimY; y++)
                {
                    if (conveyorMatrix[x, y] != null)
                    {
                        conveyorMatrix[x, y].GetComponent<Animator>().speed = slowAnimSpeed;
                    }
                }
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
        lockGame = true;
        StartCoroutine(freezeCoroutine(1.5f));    
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
        if (!PlayerPrefs.HasKey("MoveSpeed"))
        {
            PlayerPrefs.SetFloat("MoveSpeed", m_defaultSpeed);
        }
        PlayerPrefs.SetInt("CurrentLevel", PlayerPrefs.GetInt("CurrentLevel") + 1);
        readTextFile();
    }

    void readTextFile()
    {
        bool next = false;
        for (int i = 0; i < lines.Length; i++)
        {
            string name = lines[i].Trim().Replace("\r","");
            if (next)
            {
                SceneManager.LoadScene(name);
                next = false;
            }
            if (name == SceneManager.GetActiveScene().name)
            {
                next = true;
            }
        }
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
