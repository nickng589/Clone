using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The multiplicative factor of the powerUp")]
    private int m_mul;

    [SerializeField]
    [Tooltip("The number of moves the affect lasts for")]
    private int m_numMoves;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            collision.gameObject.GetComponent<PlayerController>().move_Dist = m_mul;
            Destroy(gameObject);
        }
    }
}
