using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Private Variables
    private GameManager p_GM;
    #endregion
    #region Public Variables
    public bool stillMoving = true;
    public float m_speed;
    public int moveDist = 1;
    public int moveDistLeft = 1;
    #endregion
    // Start is called before the first frame update
    void Start()
    { 
        p_GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        stillMoving = true;
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void MoveTo(Vector3 finalPos)
    {
        //gameObject.transform.position = finalPos;
        p_GM.IncreaseNumMoving();
        StartCoroutine(MoveToCoroutine(finalPos));
    }
 

    #region Coroutines
    IEnumerator MoveToCoroutine(Vector3 finalPos)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime<m_speed)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, finalPos, elapsedTime / m_speed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        gameObject.transform.position = finalPos;
        p_GM.DecreaseNumMoving();
        //p_GM.readyToMoveConveyors();
    }
   

    #endregion
}
