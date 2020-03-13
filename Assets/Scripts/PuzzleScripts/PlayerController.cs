using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("The time it takes the player to move (in seconds)")]
    private float m_speed;

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
                    move_Dist += 1;
                    RaycastHit2D rayBox = Physics2D.Raycast(gameObject.transform.position, upV, move_Dist, p_boxMask);
                    if (rayBox.transform != null)
                    {
                        distanceToBox = (int)Mathf.Round(Vector2.Distance(gameObject.transform.position, rayBox.transform.position));
                        RaycastHit2D rayWall = Physics2D.Raycast(gameObject.transform.position, upV, distanceToBox, p_wallMask);
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
                            distanceAfterBox = rayBox.transform.gameObject.GetComponent<BoxController>().PushUp(move_Dist - distanceToBox + 1);
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
                        for (int i = Mathf.Abs(move_Dist); i >= 1; i--) // Moves the longest possible distance (example, move_dist =3, wall 2 blocks away, so only move 2 blocks)
                        {
                            if (!Physics2D.Raycast((Vector2)gameObject.transform.position + upV, upV, i - 1, p_wallMask))
                            {
                                moving = true;
                                p_GM.IncreaseNumMoving(); // increase GameManagers count of players currently moving
                                StartCoroutine(MoveUpCoroutine(i));
                                break;
                            }
                        }
                    }
                    move_Dist -= 1;
                }
                else if (conveyorDirection == 3) //down
                {

                }
                else
                {
                    RaycastHit2D rayBox = Physics2D.Raycast(gameObject.transform.position, upV, move_Dist, p_boxMask);
                    if (rayBox.transform != null)
                    {
                        distanceToBox = (int)Mathf.Round(Vector2.Distance(gameObject.transform.position, rayBox.transform.position));
                        RaycastHit2D rayWall = Physics2D.Raycast(gameObject.transform.position, upV, distanceToBox, p_wallMask);
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
                            distanceAfterBox = rayBox.transform.gameObject.GetComponent<BoxController>().PushUp(move_Dist - distanceToBox + 1);
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
                        for (int i = Mathf.Abs(move_Dist); i >= 1; i--) // Moves the longest possible distance (example, move_dist =3, wall 2 blocks away, so only move 2 blocks)
                        {
                            if (!Physics2D.Raycast((Vector2)gameObject.transform.position + upV, upV, i - 1, p_wallMask))
                            {
                                moving = true;
                                p_GM.IncreaseNumMoving(); // increase GameManagers count of players currently moving
                                StartCoroutine(MoveUpCoroutine(i));
                                break;
                            }
                        }
                    }
                }


            }
            else if (up < -0.01) //Down
            {
                if (conveyorDirection == 1)
                {

                }
                else if (conveyorDirection == 3)
                {
                    move_Dist += 1;
                    RaycastHit2D rayBox = Physics2D.Raycast(gameObject.transform.position, downV, move_Dist, p_boxMask);
                    if (rayBox.transform != null)
                    {
                        distanceToBox = (int)Mathf.Round(Vector2.Distance(gameObject.transform.position, rayBox.transform.position));
                        RaycastHit2D rayWall = Physics2D.Raycast(gameObject.transform.position, downV, distanceToBox, p_wallMask);
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
                            distanceAfterBox = rayBox.transform.gameObject.GetComponent<BoxController>().PushDown(move_Dist - distanceToBox + 1);
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
                        for (int i = Mathf.Abs(move_Dist); i >= 1; i--) // Moves the longest possible distance (example, move_dist =3, wall 2 blocks away, so only move 2 blocks)
                        {
                            if (!Physics2D.Raycast((Vector2)gameObject.transform.position + downV, downV, i - 1, p_wallMask))
                            {
                                moving = true;
                                p_GM.IncreaseNumMoving(); // increase GameManagers count of players currently moving
                                StartCoroutine(MoveDownCoroutine(i));
                                break;
                            }
                        }
                    }
                    move_Dist -= 1;
                }
                else
                {
                    RaycastHit2D rayBox = Physics2D.Raycast(gameObject.transform.position, downV, move_Dist, p_boxMask);
                    if (rayBox.transform != null)
                    {
                        distanceToBox = (int)Mathf.Round(Vector2.Distance(gameObject.transform.position, rayBox.transform.position));
                        RaycastHit2D rayWall = Physics2D.Raycast(gameObject.transform.position, downV, distanceToBox, p_wallMask);
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
                            distanceAfterBox = rayBox.transform.gameObject.GetComponent<BoxController>().PushDown(move_Dist - distanceToBox + 1);
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
                        for (int i = Mathf.Abs(move_Dist); i >= 1; i--) // Moves the longest possible distance (example, move_dist =3, wall 2 blocks away, so only move 2 blocks)
                        {
                            if (!Physics2D.Raycast((Vector2)gameObject.transform.position + downV, downV, i - 1, p_wallMask))
                            {
                                moving = true;
                                p_GM.IncreaseNumMoving(); // increase GameManagers count of players currently moving
                                StartCoroutine(MoveDownCoroutine(i));
                                break;
                            }
                        }
                    }
                }
            }
            else if (right > 0.01) //Right
            {
                if (conveyorDirection == 2)
                {
                    move_Dist += 1;
                    RaycastHit2D rayBox = Physics2D.Raycast(gameObject.transform.position, rightV, move_Dist, p_boxMask);
                    if (rayBox.transform != null)
                    {
                        distanceToBox = (int)Mathf.Round(Vector2.Distance(gameObject.transform.position, rayBox.transform.position));
                        RaycastHit2D rayWall = Physics2D.Raycast(gameObject.transform.position, rightV, distanceToBox, p_wallMask);
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
                            distanceAfterBox = rayBox.transform.gameObject.GetComponent<BoxController>().PushRight(move_Dist - distanceToBox + 1);
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
                        for (int i = Mathf.Abs(move_Dist); i >= 1; i--) // Moves the longest possible distance (example, move_dist =3, wall 2 blocks away, so only move 2 blocks)
                        {
                            if (!Physics2D.Raycast((Vector2)gameObject.transform.position + rightV, rightV, i - 1, p_wallMask))
                            {
                                moving = true;
                                p_GM.IncreaseNumMoving(); // increase GameManagers count of players currently moving
                                StartCoroutine(MoveRightCoroutine(i));
                                break;
                            }
                        }
                    }
                    move_Dist -= 1;
                }
                else if (conveyorDirection == 4)
                {

                }
                else
                {
                    RaycastHit2D rayBox = Physics2D.Raycast(gameObject.transform.position, rightV, move_Dist, p_boxMask);
                    if (rayBox.transform != null)
                    {
                        distanceToBox = (int)Mathf.Round(Vector2.Distance(gameObject.transform.position, rayBox.transform.position));
                        RaycastHit2D rayWall = Physics2D.Raycast(gameObject.transform.position, rightV, distanceToBox, p_wallMask);
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
                            distanceAfterBox = rayBox.transform.gameObject.GetComponent<BoxController>().PushRight(move_Dist - distanceToBox + 1);
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
                        for (int i = Mathf.Abs(move_Dist); i >= 1; i--) // Moves the longest possible distance (example, move_dist =3, wall 2 blocks away, so only move 2 blocks)
                        {
                            if (!Physics2D.Raycast((Vector2)gameObject.transform.position + rightV, rightV, i - 1, p_wallMask))
                            {
                                moving = true;
                                p_GM.IncreaseNumMoving(); // increase GameManagers count of players currently moving
                                StartCoroutine(MoveRightCoroutine(i));
                                break;
                            }
                        }
                    }
                }

            }
            else if (right < -0.01) //Left
            {
                if (conveyorDirection == 2)
                {

                }
                else if (conveyorDirection == 4)
                {
                    move_Dist += 1;
                    RaycastHit2D rayBox = Physics2D.Raycast(gameObject.transform.position, leftV, move_Dist, p_boxMask);
                    if (rayBox.transform != null)
                    {
                        distanceToBox = (int)Mathf.Round(Vector2.Distance(gameObject.transform.position, rayBox.transform.position));
                        RaycastHit2D rayWall = Physics2D.Raycast(gameObject.transform.position, leftV, distanceToBox, p_wallMask);
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
                            distanceAfterBox = rayBox.transform.gameObject.GetComponent<BoxController>().PushLeft(move_Dist - distanceToBox + 1);
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
                        for (int i = Mathf.Abs(move_Dist); i >= 1; i--) // Moves the longest possible distance (example, move_dist =3, wall 2 blocks away, so only move 2 blocks)
                        {
                            if (!Physics2D.Raycast((Vector2)gameObject.transform.position + leftV, leftV, i - 1, p_wallMask))
                            {
                                moving = true;
                                p_GM.IncreaseNumMoving(); // increase GameManagers count of players currently moving
                                StartCoroutine(MoveLeftCoroutine(i));
                                break;
                            }
                        }
                    }
                    move_Dist -= 1;
                }
                else
                {
                    RaycastHit2D rayBox = Physics2D.Raycast(gameObject.transform.position, leftV, move_Dist, p_boxMask);
                    if (rayBox.transform != null)
                    {
                        distanceToBox = (int)Mathf.Round(Vector2.Distance(gameObject.transform.position, rayBox.transform.position));
                        RaycastHit2D rayWall = Physics2D.Raycast(gameObject.transform.position, leftV, distanceToBox, p_wallMask);
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
                            distanceAfterBox = rayBox.transform.gameObject.GetComponent<BoxController>().PushLeft(move_Dist - distanceToBox + 1);
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
                        for (int i = Mathf.Abs(move_Dist); i >= 1; i--) // Moves the longest possible distance (example, move_dist =3, wall 2 blocks away, so only move 2 blocks)
                        {
                            if (!Physics2D.Raycast((Vector2)gameObject.transform.position + leftV, leftV, i - 1, p_wallMask))
                            {
                                moving = true;
                                p_GM.IncreaseNumMoving(); // increase GameManagers count of players currently moving
                                StartCoroutine(MoveLeftCoroutine(i));
                                break;
                            }
                        }
                    }
                }      
            }
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
