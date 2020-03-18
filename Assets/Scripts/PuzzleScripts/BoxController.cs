using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public float m_speed;
    public int conveyorDirection; //0=none, 1=up, 2=right, 3=down, 4=left

    private Vector2 upV = new Vector2(0, 1);
    private Vector2 downV = new Vector2(0, -1);
    private Vector2 rightV = new Vector2(1, 0);
    private Vector2 leftV = new Vector2(-1, 0);
    private GameManager p_GM;
    private LayerMask p_wallMask;
    private LayerMask p_boxMask;
    private LayerMask p_playerMask;
    private int distanceToBox;
    private int distanceToWall;
    private int distanceAfterBox;
    private bool moving = false;
    // Start is called before the first frame update
    void Start()
    {
        p_wallMask = LayerMask.GetMask("Wall");
        p_boxMask = LayerMask.GetMask("Box");
        p_playerMask = LayerMask.GetMask("Player");
        p_GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayerMove()//called by game manager when player moves or waits
    {
        if(conveyorDirection!=0)
        {
            if(conveyorDirection==1)
            {
                this.PushUp(1);
            }
            else if(conveyorDirection ==2 )
            {
                this.PushRight(1);
            }
            else if (conveyorDirection == 3)
            {
                this.PushDown(1);
            }
            else if (conveyorDirection == 4)
            {
                this.PushLeft(1);
            }
        }
    }

    public int PushUp(int distance)
    {
        if(moving)
        {
            return 0;
        }
        RaycastHit2D rayBox = Physics2D.Raycast((Vector2)gameObject.transform.position+upV, upV, distance-1, p_boxMask);
        if (rayBox.transform != null)
        {
            distanceToBox = (int)Mathf.Round(Vector2.Distance(gameObject.transform.position, rayBox.transform.position));
            RaycastHit2D rayWall = Physics2D.Raycast(gameObject.transform.position, upV, distanceToBox, p_wallMask);
            if (rayWall.transform != null)
            {
                distanceToWall = (int)Mathf.Round(Vector2.Distance(gameObject.transform.position, rayWall.transform.position)); 
                if(distanceToWall -1 > 0)
                {
                    StartCoroutine(MoveUpCoroutine(distanceToWall - 1));
                }  
                return distanceToWall - 1;
            }
            else
            {
                distanceAfterBox = rayBox.transform.gameObject.GetComponent<BoxController>().PushUp(distance - distanceToBox + 1);
                if(distanceToBox + distanceAfterBox - 1 > 0)
                {
                    StartCoroutine(MoveUpCoroutine(distanceToBox + distanceAfterBox - 1));
                }
                return distanceToBox + distanceAfterBox-1;
            }
        }
        else
        {
            RaycastHit2D rayWall = Physics2D.Raycast(gameObject.transform.position, upV, distance, p_wallMask);
            if (rayWall.transform != null)
            {
                distanceToWall = (int)Mathf.Round(Vector2.Distance(gameObject.transform.position, rayWall.transform.position));
                if(distanceToWall - 1 > 0)
                {
                    StartCoroutine(MoveUpCoroutine(distanceToWall - 1));
                }        
                return distanceToWall - 1;
            }
            if(distance > 0)
            {
                StartCoroutine(MoveUpCoroutine(distance));
            }  
            return distance;
        } 
    }

    public int PushDown(int distance)
    {
        if (moving)
        {
            return 0;
        }
        RaycastHit2D rayBox = Physics2D.Raycast((Vector2)gameObject.transform.position + downV, downV, distance - 1, p_boxMask);
        if (rayBox.transform != null)
        {
            distanceToBox = (int)Mathf.Round(Vector2.Distance(gameObject.transform.position, rayBox.transform.position));
            RaycastHit2D rayWall = Physics2D.Raycast(gameObject.transform.position, downV, distanceToBox, p_wallMask);
            if (rayWall.transform != null)
            {
                distanceToWall = (int)Mathf.Round(Vector2.Distance(gameObject.transform.position, rayWall.transform.position));
                if(distanceToWall -1 >0)
                {
                    StartCoroutine(MoveDownCoroutine(distanceToWall - 1));
                }
                return distanceToWall - 1;
            }
            else
            {
                distanceAfterBox = rayBox.transform.gameObject.GetComponent<BoxController>().PushDown(distance - distanceToBox + 1);
                if(distanceToBox + distanceAfterBox - 1 > 0)
                {
                    StartCoroutine(MoveDownCoroutine(distanceToBox + distanceAfterBox - 1));
                }
                return distanceToBox + distanceAfterBox - 1;
            }
        }
        else
        {
            RaycastHit2D rayWall = Physics2D.Raycast(gameObject.transform.position, downV, distance, p_wallMask);
            if (rayWall.transform != null)
            {
                distanceToWall = (int)Mathf.Round(Vector2.Distance(gameObject.transform.position, rayWall.transform.position));
                if(distanceToWall -1 >0)
                {
                    StartCoroutine(MoveDownCoroutine(distanceToWall - 1));
                }
                return distanceToWall - 1;
            }
            if (distance > 0)
            {
                StartCoroutine(MoveDownCoroutine(distance));
            }
            return distance;
        }
    }

    public int PushRight(int distance)
    {
        if (moving)
        {
            return 0;
        }
        RaycastHit2D rayBox = Physics2D.Raycast((Vector2)gameObject.transform.position + rightV, rightV, distance - 1, p_boxMask);
        if (rayBox.transform != null)
        {
            distanceToBox = (int)Mathf.Round(Vector2.Distance(gameObject.transform.position, rayBox.transform.position));
            RaycastHit2D rayWall = Physics2D.Raycast(gameObject.transform.position, rightV, distanceToBox, p_wallMask);
            if (rayWall.transform != null)
            {
                distanceToWall = (int)Mathf.Round(Vector2.Distance(gameObject.transform.position, rayWall.transform.position));
                if(distanceToWall -1 >0)
                {
                    StartCoroutine(MoveRightCoroutine(distanceToWall - 1));
                }
                return distanceToWall - 1;
            }
            else
            {
                distanceAfterBox = rayBox.transform.gameObject.GetComponent<BoxController>().PushRight(distance - distanceToBox + 1);
                if(distanceToBox + distanceAfterBox - 1 >0)
                {
                    StartCoroutine(MoveRightCoroutine(distanceToBox + distanceAfterBox - 1));
                }
                return distanceToBox + distanceAfterBox - 1;
            }
        }
        else
        {
            RaycastHit2D rayWall = Physics2D.Raycast(gameObject.transform.position, rightV, distance, p_wallMask);
            if (rayWall.transform != null)
            {
                distanceToWall = (int)Mathf.Round(Vector2.Distance(gameObject.transform.position, rayWall.transform.position));
                if (distanceToWall - 1 > 0)
                {
                    StartCoroutine(MoveRightCoroutine(distanceToWall - 1));
                }
                return distanceToWall - 1;
            }
            if (distance > 0)
            {
                StartCoroutine(MoveRightCoroutine(distance));
            }
            return distance;
        }
    }

    public int PushLeft(int distance)
    {
        if (moving)
        {
            return 0;
        }
        RaycastHit2D rayBox = Physics2D.Raycast((Vector2)gameObject.transform.position + leftV, leftV, distance - 1, p_boxMask);
        if (rayBox.transform != null)
        {
            distanceToBox = (int)Mathf.Round(Vector2.Distance(gameObject.transform.position, rayBox.transform.position));
            RaycastHit2D rayWall = Physics2D.Raycast(gameObject.transform.position, leftV, distanceToBox, p_wallMask);
            if (rayWall.transform != null)
            {
                distanceToWall = (int)Mathf.Round(Vector2.Distance(gameObject.transform.position, rayWall.transform.position));
                if(distanceToWall - 1 >0)
                {
                    StartCoroutine(MoveLeftCoroutine(distanceToWall - 1));
                } 
                return distanceToWall - 1;
            }
            else
            {
                distanceAfterBox = rayBox.transform.gameObject.GetComponent<BoxController>().PushLeft(distance - distanceToBox + 1);
                if(distanceToBox + distanceAfterBox - 1 >0)
                {
                    StartCoroutine(MoveLeftCoroutine(distanceToBox + distanceAfterBox - 1));
                } 
                return distanceToBox + distanceAfterBox - 1;
            }
        }
        else
        {
            RaycastHit2D rayWall = Physics2D.Raycast(gameObject.transform.position, leftV, distance, p_wallMask);
            if (rayWall.transform != null)
            {
                distanceToWall = (int)Mathf.Round(Vector2.Distance(gameObject.transform.position, rayWall.transform.position));
                if(distanceToWall - 1 <0)
                {
                    StartCoroutine(MoveLeftCoroutine(distanceToWall - 1));
                }
                return distanceToWall - 1;
            }
            if (distance > 0)
            {
                StartCoroutine(MoveLeftCoroutine(distance));
            }
            return distance;
        }
    }


    IEnumerator MoveUpCoroutine(int distance)
    {
        moving = true;
        p_GM.IncreaseNumMoving(true);
        conveyorDirection = 0;
        float elapsedTime = 0.0f;
        Vector3 finalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + distance, gameObject.transform.position.z);
        while (gameObject.transform.position != finalPos)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, finalPos, elapsedTime / m_speed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        p_GM.DecreaseNumMoving();
        moving = false;
    }

    IEnumerator MoveDownCoroutine(int distance)
    {
        moving = true;
        p_GM.IncreaseNumMoving(true);
        conveyorDirection = 0;
        float elapsedTime = 0.0f;
        Vector3 finalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - distance, gameObject.transform.position.z);
        while (gameObject.transform.position != finalPos)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, finalPos, elapsedTime / m_speed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        p_GM.DecreaseNumMoving();
        moving = false;
    }

    IEnumerator MoveLeftCoroutine(int distance)
    {
        moving = true;
        p_GM.IncreaseNumMoving(true);
        conveyorDirection = 0;
        float elapsedTime = 0.0f;
        Vector3 finalPos = new Vector3(gameObject.transform.position.x - distance, gameObject.transform.position.y, gameObject.transform.position.z);
        while (gameObject.transform.position != finalPos)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, finalPos, elapsedTime / m_speed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        p_GM.DecreaseNumMoving();
        moving = false;
    }

    IEnumerator MoveRightCoroutine(int distance)
    {
        moving = true;
        p_GM.IncreaseNumMoving(true);
        conveyorDirection = 0;
        float elapsedTime = 0.0f;
        Vector3 finalPos = new Vector3(gameObject.transform.position.x + distance, gameObject.transform.position.y, gameObject.transform.position.z);
        while (gameObject.transform.position != finalPos)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, finalPos, elapsedTime / m_speed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        p_GM.DecreaseNumMoving();
        moving = false;
    }
}
