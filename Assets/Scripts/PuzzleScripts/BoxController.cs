using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public float m_speed;
    public bool stillMoving;
    public Vector3 midpoint;
    public bool teleported = false;
    public bool teleport = false;
    public Vector3 tpLoc;
    public bool moved = false;

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

    public void TeleportTo(Vector3 finalPos)
    {
        teleport = true;
        tpLoc = finalPos;
        /*gameObject.transform.position = finalPos;
        teleported = true;*/
    }
    public void MoveTo(Vector3 initialPos, Vector3 finalPos)
    {
        //gameObject.transform.position = finalPos;
        moved = true;
        p_GM.IncreaseNumMoving();

        if (midpoint != initialPos && midpoint != finalPos)
        {
            midpoint = (((initialPos + finalPos) * 0.5f + midpoint) * 0.5f + midpoint) * 0.5f;
        }       
        StartCoroutine(MoveToCoroutine(initialPos, finalPos));
    }

    
    IEnumerator HalfMoveCoroutine(Vector3 initialPos, Vector3 finalPos)
    {
        float elapsedTime = 0.0f;
        while(elapsedTime <m_speed)
        {
            gameObject.transform.position = Vector3.Lerp(initialPos, finalPos, (Mathf.Sin(Mathf.PI * (elapsedTime / m_speed) - Mathf.PI / 2) + 1) / 2);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        gameObject.transform.position = finalPos;
        p_GM.DecreaseNumMoving();
    }

    IEnumerator TPAnimCoroutine()
    {
        teleport = false;
        float animSpeed = 0.2f;
        float elapsedTime = 0.0f;
        Vector3 originalSize = gameObject.transform.localScale;
        while (elapsedTime < animSpeed)
        {
            //gameObject.transform.localRotation = Quaternion.Lerp(new Quaternion(0, 0, 360f, 0), new Quaternion(0, 0, 0, 0), elapsedTime / m_speed);
            gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.black, Color.white, elapsedTime / animSpeed);
            if(elapsedTime < animSpeed*0.7f)
            {
                gameObject.transform.localScale = Vector3.Lerp(Vector3.zero ,originalSize + new Vector3(0.2f, 0.2f, 0.2f), elapsedTime / animSpeed);
            }
            else
            {
                gameObject.transform.localScale = Vector3.Lerp(originalSize + new Vector3(0.2f, 0.2f, 0.2f), originalSize , elapsedTime / animSpeed);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        //gameObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
        gameObject.transform.localScale = originalSize;
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        
    }


    IEnumerator MoveToCoroutine(Vector3 initialPos, Vector3 finalPos)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < m_speed)
        {
            if (midpoint == initialPos || midpoint == finalPos)
            {
                gameObject.transform.position = Vector3.Lerp(initialPos, finalPos, (Mathf.Sin(Mathf.PI * (elapsedTime / m_speed) - Mathf.PI / 2) + 1) / 2);
                elapsedTime += Time.deltaTime;
            }
            else
            {
                gameObject.transform.position = ThreePointLerp(initialPos, midpoint, finalPos, (Mathf.Sin(Mathf.PI*(elapsedTime / m_speed)-Mathf.PI/2)+1)/2);
                elapsedTime += Time.deltaTime*0.8f;
            }
            yield return null;
        }
        gameObject.transform.position = finalPos;
        p_GM.DecreaseNumMoving();
        if(teleport)
        {
            gameObject.transform.position = tpLoc;
            StartCoroutine(TPAnimCoroutine());
        }
        //p_GM.readyToMoveConveyors();
    }

    private Vector3 ThreePointLerp(Vector3 start, Vector3 mid, Vector3 end, float time)
    {
        if (time <= 0.5)
        {
            return Vector3.Lerp(start, mid, time * 2f);
        }
        else
        {
            return Vector3.Lerp(mid, end, (time - 0.5f) * 2f);
        }
    }

}
