using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Editor Variables

    

    #endregion

    #region Private Variables
    private GameManager p_GM;
    private LayerMask p_wallMask;
    private LayerMask p_boxMask;
    private bool moving = false;
    private Vector2 upV = new Vector2(0, 1);
    private Vector2 downV = new Vector2(0, -1);
    private Vector2 rightV = new Vector2(1, 0);
    private Vector2 leftV = new Vector2(-1, 0);
    private int distanceToBox;
    private int distanceToWall;
    private int distanceAfterBox;
    #endregion

    #region Public Variables
    public int move_Dist;
    public int conveyorDirection; //0=none, 1=up, 2=right, 3=down, 4=left
    public float m_speed;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        p_wallMask = LayerMask.GetMask("Wall");
        p_boxMask = LayerMask.GetMask("Box");
        move_Dist = 1;
        p_GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        conveyorDirection = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!moving && p_GM.playersCanMove) //only calculate movement if GameManager says all players can move
        {
            float right = Input.GetAxis("Horizontal");
            float up = Input.GetAxis("Vertical");
            if (move_Dist == -1)
            {
                right *= -1.0f;
                up *= -1.0f;
            }
            //Else ifs insure that the game doesnt register 2 directions at the same time
            if (up > 0.01) //Up
            {
                if (conveyorDirection == 1) //up
                {
                    MoveUp(move_Dist + 1);
                }
                else if (conveyorDirection == 3) //down
                {
                    p_GM.StartedMoving();
                }
                else
                {
                    MoveUp(move_Dist);
                }
            }
            else if (up < -0.01) //Down
            {
                if (conveyorDirection == 1)
                {
                    p_GM.StartedMoving();
                }
                else if (conveyorDirection == 3)
                {
                    MoveDown(move_Dist + 1);
                }
                else
                {
                    MoveDown(move_Dist);
                }
            }
            else if (right > 0.01) //Right
            {
                if (conveyorDirection == 2)
                {
                    MoveRight(move_Dist + 1);
                }
                else if (conveyorDirection == 4)
                {
                    p_GM.StartedMoving();
                }
                else
                {
                    MoveRight(move_Dist);  
                }
            }
            else if (right < -0.01) //Left
            {
                if (conveyorDirection == 2)
                {
                    p_GM.StartedMoving();
                }
                else if (conveyorDirection == 4)
                {
                    MoveLeft(move_Dist + 1);
                }
                else
                {
                    MoveLeft(move_Dist);
                }
            }
        }
        
    }

    public void OnWait()//called when the player waits
    {
        if (!moving && conveyorDirection != 0)
        {
            if (conveyorDirection == 1)
            {
                MoveUp(1);
            }
            else if (conveyorDirection == 2)
            {
                MoveRight(1);
            }
            else if (conveyorDirection == 3)
            {
                MoveDown(1);
            }
            else if (conveyorDirection == 4)
            {
                MoveLeft(1);
            }
        }
    }

    private void MoveUp(int distance)
    {
        RaycastHit2D rayBox = Physics2D.Raycast(gameObject.transform.position, upV, distance, p_boxMask); // find if there are any boxes in the players path
        if (rayBox.transform != null)
        {
            distanceToBox = (int)Mathf.Round(Vector2.Distance(gameObject.transform.position, rayBox.transform.position));
            RaycastHit2D rayWall = Physics2D.Raycast(gameObject.transform.position, upV, distanceToBox, p_wallMask); // find if there are any walls before the box in front of us
            if (rayWall.transform != null)
            {
                distanceToWall = (int)Mathf.Round(Vector2.Distance(gameObject.transform.position, rayWall.transform.position));
                if (distanceToWall > 1)
                {
                    moving = true;
                    p_GM.IncreaseNumMoving();
                    StartCoroutine(MoveUpCoroutine(distanceToWall - 1));
                }
            }
            else
            {
                distanceAfterBox = rayBox.transform.gameObject.GetComponent<BoxController>().PushUp(distance - distanceToBox + 1); // ask box how far it can be pushed
                if (distanceToBox + distanceAfterBox > 1)
                {
                    moving = true;
                    p_GM.IncreaseNumMoving();
                    StartCoroutine(MoveUpCoroutine(distanceToBox + distanceAfterBox - 1));
                }
            }
        }
        else
        {
            RaycastHit2D rayWall = Physics2D.Raycast(gameObject.transform.position, upV, distance, p_wallMask); // find if any walls in out path
            if (rayWall.transform != null)
            {
                distanceToWall = (int)Mathf.Round(Vector2.Distance(gameObject.transform.position, rayWall.transform.position));
                if (distanceToWall > 1)
                {
                    moving = true;
                    p_GM.IncreaseNumMoving();
                    StartCoroutine(MoveUpCoroutine(distanceToWall - 1)); ;
                }   
            }
            moving = true;
            p_GM.IncreaseNumMoving();
            StartCoroutine(MoveUpCoroutine(distance));
        }
    }

    private void MoveDown(int distance)
    {
        RaycastHit2D rayBox = Physics2D.Raycast(gameObject.transform.position, downV, distance, p_boxMask); // find if there are any boxes in the players path
        if (rayBox.transform != null)
        {
            distanceToBox = (int)Mathf.Round(Vector2.Distance(gameObject.transform.position, rayBox.transform.position));
            RaycastHit2D rayWall = Physics2D.Raycast(gameObject.transform.position, downV, distanceToBox, p_wallMask); // find if there are any walls before the box in front of us
            if (rayWall.transform != null)
            {
                distanceToWall = (int)Mathf.Round(Vector2.Distance(gameObject.transform.position, rayWall.transform.position));
                if (distanceToWall > 1)
                {
                    moving = true;
                    p_GM.IncreaseNumMoving();
                    StartCoroutine(MoveDownCoroutine(distanceToWall - 1));
                }
            }
            else
            {
                distanceAfterBox = rayBox.transform.gameObject.GetComponent<BoxController>().PushDown(distance - distanceToBox + 1); // ask box how far it can be pushed
                if (distanceToBox + distanceAfterBox > 1)
                {
                    moving = true;
                    p_GM.IncreaseNumMoving();
                    StartCoroutine(MoveDownCoroutine(distanceToBox + distanceAfterBox - 1));
                }
            }
        }
        else
        {
            RaycastHit2D rayWall = Physics2D.Raycast(gameObject.transform.position, downV, distance, p_wallMask); // find if any walls in out path
            if (rayWall.transform != null)
            {
                distanceToWall = (int)Mathf.Round(Vector2.Distance(gameObject.transform.position, rayWall.transform.position));
                if (distanceToWall > 1)
                {
                    moving = true;
                    p_GM.IncreaseNumMoving();
                    StartCoroutine(MoveDownCoroutine(distanceToWall - 1)); ;
                }
            }
            moving = true;
            p_GM.IncreaseNumMoving();
            StartCoroutine(MoveDownCoroutine(distance));
        }
    }

    private void MoveRight(int distance)
    {
        RaycastHit2D rayBox = Physics2D.Raycast(gameObject.transform.position, rightV, distance, p_boxMask); // find if there are any boxes in the players path
        if (rayBox.transform != null)
        {
            distanceToBox = (int)Mathf.Round(Vector2.Distance(gameObject.transform.position, rayBox.transform.position));
            RaycastHit2D rayWall = Physics2D.Raycast(gameObject.transform.position, rightV, distanceToBox, p_wallMask); // find if there are any walls before the box in front of us
            if (rayWall.transform != null)
            {
                distanceToWall = (int)Mathf.Round(Vector2.Distance(gameObject.transform.position, rayWall.transform.position));
                if (distanceToWall > 1)
                {
                    moving = true;
                    p_GM.IncreaseNumMoving();
                    StartCoroutine(MoveRightCoroutine(distanceToWall - 1));
                }
            }
            else
            {
                distanceAfterBox = rayBox.transform.gameObject.GetComponent<BoxController>().PushRight(distance - distanceToBox + 1); // ask box how far it can be pushed
                if (distanceToBox + distanceAfterBox > 1)
                {
                    moving = true;
                    p_GM.IncreaseNumMoving();
                    StartCoroutine(MoveRightCoroutine(distanceToBox + distanceAfterBox - 1));
                }
            }
        }
        else
        {
            RaycastHit2D rayWall = Physics2D.Raycast(gameObject.transform.position, rightV, distance, p_wallMask); // find if any walls in out path
            if (rayWall.transform != null)
            {
                distanceToWall = (int)Mathf.Round(Vector2.Distance(gameObject.transform.position, rayWall.transform.position));
                if (distanceToWall > 1)
                {
                    moving = true;
                    p_GM.IncreaseNumMoving();
                    StartCoroutine(MoveRightCoroutine(distanceToWall - 1)); ;
                }
            }
            moving = true;
            p_GM.IncreaseNumMoving();
            StartCoroutine(MoveRightCoroutine(distance));
        }
    }

    private void MoveLeft(int distance)
    {
        RaycastHit2D rayBox = Physics2D.Raycast(gameObject.transform.position, leftV, distance, p_boxMask); // find if there are any boxes in the players path
        if (rayBox.transform != null)
        {
            distanceToBox = (int)Mathf.Round(Vector2.Distance(gameObject.transform.position, rayBox.transform.position));
            RaycastHit2D rayWall = Physics2D.Raycast(gameObject.transform.position, leftV, distanceToBox, p_wallMask); // find if there are any walls before the box in front of us
            if (rayWall.transform != null)
            {
                distanceToWall = (int)Mathf.Round(Vector2.Distance(gameObject.transform.position, rayWall.transform.position));
                if (distanceToWall > 1)
                {
                    moving = true;
                    p_GM.IncreaseNumMoving();
                    StartCoroutine(MoveLeftCoroutine(distanceToWall - 1));
                }
            }
            else
            {
                distanceAfterBox = rayBox.transform.gameObject.GetComponent<BoxController>().PushLeft(distance - distanceToBox + 1); // ask box how far it can be pushed
                if (distanceToBox + distanceAfterBox > 1)
                {
                    moving = true;
                    p_GM.IncreaseNumMoving();
                    StartCoroutine(MoveLeftCoroutine(distanceToBox + distanceAfterBox - 1));
                }
            }
        }
        else
        {
            RaycastHit2D rayWall = Physics2D.Raycast(gameObject.transform.position, leftV, distance, p_wallMask); // find if any walls in out path
            if (rayWall.transform != null)
            {
                distanceToWall = (int)Mathf.Round(Vector2.Distance(gameObject.transform.position, rayWall.transform.position));
                if (distanceToWall > 1)
                {
                    moving = true;
                    p_GM.IncreaseNumMoving();
                    StartCoroutine(MoveLeftCoroutine(distanceToWall - 1)); ;
                }
            }
            moving = true;
            p_GM.IncreaseNumMoving();
            StartCoroutine(MoveLeftCoroutine(distance));
        }
    }
    #region Coroutines
    IEnumerator MoveUpCoroutine(int distance)
    {
        conveyorDirection = 0;
        float elapsedTime = 0.0f;
        Vector3 finalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + distance, gameObject.transform.position.z);
        while(gameObject.transform.position != finalPos)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, finalPos , elapsedTime / m_speed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        moving = false;
        p_GM.DecreaseNumMoving(); // decrease GameManagers count of players currently moving
    }

    IEnumerator MoveDownCoroutine(int distance)
    {
        conveyorDirection = 0;
        float elapsedTime = 0.0f;
        Vector3 finalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - distance, gameObject.transform.position.z);
        while (gameObject.transform.position != finalPos)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, finalPos, elapsedTime / m_speed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        moving = false;
        p_GM.DecreaseNumMoving(); // decrease GameManagers count of players currently moving
    }

    IEnumerator MoveLeftCoroutine(int distance)
    {
        conveyorDirection = 0;
        float elapsedTime = 0.0f;
        Vector3 finalPos = new Vector3(gameObject.transform.position.x - distance, gameObject.transform.position.y, gameObject.transform.position.z);
        while (gameObject.transform.position != finalPos)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, finalPos, elapsedTime / m_speed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        moving = false;
        p_GM.DecreaseNumMoving(); // decrease GameManagers count of players currently moving
    }

    IEnumerator MoveRightCoroutine(int distance)
    {
        conveyorDirection = 0;
        float elapsedTime = 0.0f;
        Vector3 finalPos = new Vector3(gameObject.transform.position.x + distance, gameObject.transform.position.y, gameObject.transform.position.z);
        while (gameObject.transform.position != finalPos)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, finalPos, elapsedTime / m_speed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        moving = false;
        p_GM.DecreaseNumMoving(); // decrease GameManagers count of players currently moving
    }


    #endregion
}
