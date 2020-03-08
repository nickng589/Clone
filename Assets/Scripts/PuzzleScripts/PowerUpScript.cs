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

    private int countMoves;
    private GameObject player;
    private GameManager p_GM;
    // Start is called before the first frame update
    void Start()
    {
        countMoves = -1;
        p_GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            player = collision.gameObject;
            player.GetComponent<PlayerController>().move_Dist *= m_mul;
            p_GM.addPowerUp(this);
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(GetComponent<SpriteRenderer>());
            Destroy(GetComponent<BoxCollider2D>());
        }
    }

    public void increaseMoveCount()
    {
        countMoves += 1;
        if(countMoves >= m_numMoves)
        {
            if(m_mul == 0)
            {
                player.GetComponent<PlayerController>().move_Dist = 1;
            }
            else
            {
                player.GetComponent<PlayerController>().move_Dist /= m_mul;
            }
            p_GM.removePowerUp(this);
            Destroy(gameObject);
        }
    }
}
