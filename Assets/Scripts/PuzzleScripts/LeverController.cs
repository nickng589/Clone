using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The Door this button controls")]
    private GameObject[] m_doors; 

    [SerializeField]
    [Tooltip("The sprite for the on lever")]
    Sprite m_OnSprite;

    [SerializeField]
    [Tooltip("The sprite for the off lever")]
    Sprite m_OffSprite;

    private bool on = false;
    private DoorController[] p_doorCons;
    private GameManager p_GM;
    // Start is called before the first frame update
    void Start()
    {
        p_GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        p_doorCons = new DoorController[m_doors.Length];
        for (int i = 0; i < m_doors.Length; i++)
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
        if (collision.gameObject.tag == "Player")
        {
            p_GM.playButtonDown();
            foreach (DoorController doorCon in p_doorCons)
            {
                doorCon.SwapOpenClose();
            }
            on = !on;
            if (on)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = m_OnSprite;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = m_OffSprite;
            }
        }
    }
}
