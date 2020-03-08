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

    private bool open;
    // Start is called before the first frame update
    void Start()
    {
        open = m_startOpen;
        DoorUpdate(); 
    }

    private void DoorUpdate()
    {
        if(open)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = m_OpenSprite;
            gameObject.layer = 0;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = m_ClosedSprite;
            gameObject.layer = 9;
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
