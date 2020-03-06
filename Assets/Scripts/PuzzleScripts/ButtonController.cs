using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The Door this button controls")]
    private GameObject m_door;

    private DoorController p_doorCon;
    // Start is called before the first frame update
    void Start()
    {
        p_doorCon = m_door.GetComponent<DoorController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            p_doorCon.OpenDoor();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            p_doorCon.CloseDoor();
        }
    }
}
