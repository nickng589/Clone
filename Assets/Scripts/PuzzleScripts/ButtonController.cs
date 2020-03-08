using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The Door this button controls")]
    private GameObject[] m_doors;

    private DoorController[] p_doorCons;
    // Start is called before the first frame update
    void Start()
    {
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
        if (collision.gameObject.tag == "Player")
        {
            foreach(DoorController doorCon in p_doorCons)
            {
                doorCon.SwapOpenClose();
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        foreach (DoorController doorCon in p_doorCons)
        {
            doorCon.SwapOpenClose();
        }
    }
}
