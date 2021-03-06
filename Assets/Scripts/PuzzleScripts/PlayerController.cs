﻿using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Private Variables
    private GameManager p_GM;
    [SerializeField]
    private Animator p_Anim;
    #endregion
    #region Public Variables
    public bool stillMoving = true;
    public bool inverted = false;
    public float m_speed;
    public int moveDist = 1;
    public int moveDistLeft = 1;
    public Vector3 midpoint;
    public float multiplier;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        p_Anim.SetFloat("LeftSpeed", multiplier);
        p_Anim.SetFloat("RightSpeed", multiplier);
        p_Anim.SetFloat("UpSpeed", multiplier);
        p_Anim.SetFloat("DownSpeed", multiplier);
        p_GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        stillMoving = true;
    }

    public void AnimatePlayer(bool up, bool down, bool left, bool right)
    {
        if (p_Anim == null) { return; }
        p_Anim.SetTrigger("Walk");
        p_Anim.SetBool("WalkUp", up);
        p_Anim.SetBool("WalkDown", down);
        p_Anim.SetBool("WalkLeft", left);
        p_Anim.SetBool("WalkRight", right);
    }

    public void FinishAnimatePlayer()
    {
        if (p_Anim == null) { return; }
        p_Anim.SetBool("WalkUp", false);
        p_Anim.SetBool("WalkDown", false);
        p_Anim.SetBool("WalkLeft", false);
        p_Anim.SetBool("WalkRight", false);
    }


    public void MoveTo(Vector3 initialPos, Vector3 finalPos)
    {
        //gameObject.transform.position = finalPos;
        if (midpoint != initialPos && midpoint != finalPos)
        {
            midpoint = (((initialPos + finalPos) * 0.5f + midpoint) * 0.5f + midpoint) * 0.5f;
        }
        p_GM.IncreaseNumMoving();
        if(initialPos!=midpoint || initialPos != finalPos)
        {
            p_GM.playPlayerSound();
        }
        StartCoroutine(MoveToCoroutine(initialPos, finalPos));
    }

    public void Die()
    {
        StartCoroutine(DeathCoroutine());
    }

    IEnumerator DeathCoroutine()
    {
        float animSpeed = 0.2f;
        float elapsedTime = 0.0f;
        Vector3 originalSize = gameObject.transform.localScale;
        while (elapsedTime < animSpeed)
        {
            //gameObject.transform.localRotation = Quaternion.Lerp(new Quaternion(0, 0, 360f, 0), new Quaternion(0, 0, 0, 0), elapsedTime / m_speed);
            gameObject.transform.localScale = Vector3.Lerp(originalSize,Vector3.zero, elapsedTime / animSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        p_GM.PlayerDeath();
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
                gameObject.transform.position = ThreePointLerp(initialPos, midpoint, finalPos, (Mathf.Sin(Mathf.PI * (elapsedTime / m_speed) - Mathf.PI / 2) + 1) / 2);
                elapsedTime += Time.deltaTime * 0.8f;
            }
            yield return null;
        }
        gameObject.transform.position = finalPos;
        p_GM.DecreaseNumMoving();
        FinishAnimatePlayer();
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
