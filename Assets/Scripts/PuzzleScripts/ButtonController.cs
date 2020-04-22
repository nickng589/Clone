using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The Door this button controls")]
    private GameObject[] m_doors;
    [SerializeField]
    [Tooltip("The SpriteRenderer for the button")]
    private SpriteRenderer m_sprRend;
    [SerializeField]
    [Tooltip("Sprite for button on")]
    private Sprite m_spriteOn;
    [SerializeField]
    [Tooltip("Sprite for button off")]
    private Sprite m_spriteOff;

    private DoorController[] p_doorCons;
    private GameManager p_GM;
    // Start is called before the first frame update
    void Start()
    {
        p_GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        p_doorCons = new DoorController[m_doors.Length];
        for(int i =0; i< m_doors.Length;i++)
        {
            p_doorCons[i] = m_doors[i].GetComponent<DoorController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Box")
        {
            p_GM.playButtonDown();
            foreach (DoorController doorCon in p_doorCons)
            {
                doorCon.SwapOpenClose();
                if (m_sprRend != null) {
                    m_sprRend.sprite = m_spriteOn;
                }
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Box")
        {
            p_GM.playButtonUp();
            foreach (DoorController doorCon in p_doorCons)
            {
                doorCon.SwapOpenClose();
                if (m_sprRend != null)
                {
                    m_sprRend.sprite = m_spriteOff;
                }
            }
        }      
    }
}
