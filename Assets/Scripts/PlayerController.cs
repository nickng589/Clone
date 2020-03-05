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
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        p_GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {   
        if (p_GM.playersCanMove) //only calculate movement if GameManager says all players can move
        {
            float right = Input.GetAxis("Horizontal");
            float up = Input.GetAxis("Vertical");
            //Else ifs insure that the game doesnt register 2 directions at the same time
            if (up > 0.01) //Up
            {
                if(!Physics2D.Raycast(new Vector2(gameObject.transform.position.x,gameObject.transform.position.y+1), new Vector2(0, 0),1))//checks that there are no walls above the player
                {
                    p_GM.numMovingPlayers += 1; // increase GameManagers count of players currently moving
                    StartCoroutine(MoveUpCoroutine(1));
                }
                    
            }
            else if(up < -0.01) //Down
            {
                if (!Physics2D.Raycast(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 1), new Vector2(0, 0), 1))//checks that there are no walls below the player
                {
                    p_GM.numMovingPlayers += 1; // increase GameManagers count of players currently moving
                    StartCoroutine(MoveDownCoroutine(1));
                }
            }
            else if(right > 0.01) //Right
            {
                if (!Physics2D.Raycast(new Vector2(gameObject.transform.position.x + 1, gameObject.transform.position.y), new Vector2(0, 0), 1))//checks that there are no walls to the right of the player
                {
                    p_GM.numMovingPlayers += 1; // increase GameManagers count of players currently moving
                    StartCoroutine(MoveRightCoroutine(1));
                }
            }
            else if(right < -0.01) //Left
            {
                if (!Physics2D.Raycast(new Vector2(gameObject.transform.position.x - 1, gameObject.transform.position.y), new Vector2(0, 0), 1))//checks that there are no walls to the left of the player
                {
                    p_GM.numMovingPlayers += 1; // increase GameManagers count of players currently moving
                    StartCoroutine(MoveLeftCoroutine(1));
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
        p_GM.numMovingPlayers -= 1; // decrease GameManagers count of players currently moving
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
        p_GM.numMovingPlayers -= 1; // decrease GameManagers count of players currently moving
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
        p_GM.numMovingPlayers -= 1; // decrease GameManagers count of players currently moving
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
        p_GM.numMovingPlayers -= 1; // decrease GameManagers count of players currently moving
    }

    #endregion
}
