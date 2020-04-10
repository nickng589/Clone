using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public float m_speed;
    public bool stillMoving;


    private GameManager p_GM;
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

    IEnumerator MoveToCoroutine(Vector3 finalPos)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < m_speed)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, finalPos, elapsedTime / m_speed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        gameObject.transform.position = finalPos;
        p_GM.DecreaseNumMoving();
        //p_GM.readyToMoveConveyors();
    }
   
}
