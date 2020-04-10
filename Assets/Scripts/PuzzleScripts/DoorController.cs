using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The sprite for the open door")]
    Sprite m_OpenSprite;

    [SerializeField]
    [Tooltip("The sprite for the closed door")]
    Sprite m_ClosedSprite;

    [SerializeField]
    [Tooltip("Whether or not the door starts open")]
    bool m_startOpen;

    [SerializeField]
    [Tooltip("Animator")]
    Animator m_Animator;

    private GameManager p_GM;
    public bool open;
    // Start is called before the first frame update
    void Start()
    {
        p_GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (m_Animator == null) {
            open = m_startOpen;
            DoorUpdate();
        }
        else if (m_startOpen) {
            m_Animator.SetTrigger("Open");
        }
    }

    private void DoorUpdate()
    {
        if(open)
        {
            if (m_Animator == null)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = m_OpenSprite;
            }
            else
            {
                m_Animator.ResetTrigger("Close");
                m_Animator.SetTrigger("Open");
            }
            p_GM.removeFromMatrix(gameObject);
        }
        else
        {
            if (m_Animator == null)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = m_ClosedSprite;
            }
            else
            {
                m_Animator.ResetTrigger("Open");
                m_Animator.SetTrigger("Close");
            }
            p_GM.addToMatrix(gameObject);
        }
        
    }

    public void OpenDoor()
    {
        open = true;
        DoorUpdate();
    }

    public void CloseDoor()
    {
        open = false;
        DoorUpdate();
    }

    public void SwapOpenClose()
    {
        open = !open;
        DoorUpdate();
    }
}
