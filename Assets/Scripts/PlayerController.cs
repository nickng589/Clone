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
    #endregion

    #region Public Variables
    public int move_Dist;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        p_wallMask = LayerMask.GetMask("Wall");
        move_Dist = 1;
        p_GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {   
        if (p_GM.playersCanMove) //only calculate movement if GameManager says all players can move
        {
            float right = Input.GetAxis("Horizontal");
            float up = Input.GetAxis("Vertical");
            if(move_Dist == -1)
            {
                right *= -1.0f;
                up *= -1.0f;
            }
            //Else ifs insure that the game doesnt register 2 directions at the same time
            if (up > 0.01) //Up
            {
                for(int i = Mathf.Abs(move_Dist); i>= 1; i--) // Moves the longest possible distance (example, move_dist =3, wall 2 blocks away, so only move 2 blocks)
                {
                    if(!Physics2D.Raycast(gameObject.transform.position, new Vector2(0,1),i,p_wallMask))
                    {
                        p_GM.IncreaseNumMoving(); // increase GameManagers count of players currently moving
                        StartCoroutine(MoveUpCoroutine(i));
                        break;
                    }
                }
                    
            }
            else if(up < -0.01) //Down
            {
                for (int i = Mathf.Abs(move_Dist); i >= 1; i--) // Moves the longest possible distance (example, move_dist =3, wall 2 blocks away, so only move 2 blocks)
                {
                    if (!Physics2D.Raycast(gameObject.transform.position, new Vector2(0, -1), i, p_wallMask))
                    {
                        p_GM.IncreaseNumMoving(); // increase GameManagers count of players currently moving
                        StartCoroutine(MoveDownCoroutine(i));
                        break;
                    }
                }
            }
            else if(right > 0.01) //Right
            {
                for (int i = Mathf.Abs(move_Dist); i >= 1; i--) // Moves the longest possible distance (example, move_dist =3, wall 2 blocks away, so only move 2 blocks)
                {
                    if (!Physics2D.Raycast(gameObject.transform.position, new Vector2(1, 0), i, p_wallMask))
                    {
                        p_GM.IncreaseNumMoving(); // increase GameManagers count of players currently moving
                        StartCoroutine(MoveRightCoroutine(i));
                        break;
                    }
                }
            }
            else if(right < -0.01) //Left
            {
                for (int i = Mathf.Abs(move_Dist); i >= 1; i--) // Moves the longest possible distance (example, move_dist =3, wall 2 blocks away, so only move 2 blocks)
                {
                    if (!Physics2D.Raycast(gameObject.transform.position, new Vector2(-1, 0), i, p_wallMask))
                    {
                        p_GM.IncreaseNumMoving(); // increase GameManagers count of players currently moving
                        StartCoroutine(MoveLeftCoroutine(i));
                        break;
                    }
                }
            }
        }
        
    }
    #region Coroutines
    IEnumerator MoveUpCoroutine(int distance)
    {  
        float elapsedTime = 0.0f;
        Vector3 finalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + distance, gameObject.transform.position.z);
        while(gameObject.transform.position != finalPos)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, finalPos , elapsedTime / m_speed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        p_GM.DecreaseNumMoving(); // decrease GameManagers count of players currently moving
    }

    IEnumerator MoveDownCoroutine(int distance)
    {
        float elapsedTime = 0.0f;
        Vector3 finalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - distance, gameObject.transform.position.z);
        while (gameObject.transform.position != finalPos)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, finalPos, elapsedTime / m_speed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        p_GM.DecreaseNumMoving(); // decrease GameManagers count of players currently moving
    }

    IEnumerator MoveLeftCoroutine(int distance)
    {
        float elapsedTime = 0.0f;
        Vector3 finalPos = new Vector3(gameObject.transform.position.x - distance, gameObject.transform.position.y, gameObject.transform.position.z);
        while (gameObject.transform.position != finalPos)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, finalPos, elapsedTime / m_speed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        p_GM.DecreaseNumMoving(); // decrease GameManagers count of players currently moving
    }

    IEnumerator MoveRightCoroutine(int distance)
    {
        float elapsedTime = 0.0f;
        Vector3 finalPos = new Vector3(gameObject.transform.position.x + distance, gameObject.transform.position.y, gameObject.transform.position.z);
        while (gameObject.transform.position != finalPos)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, finalPos, elapsedTime / m_speed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        p_GM.DecreaseNumMoving(); // decrease GameManagers count of players currently moving
    }


    #endregion
}
