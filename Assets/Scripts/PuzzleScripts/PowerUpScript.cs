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
            player.GetComponent<PlayerController>().moveDist *= m_mul;
            if(m_mul<0)
            {
                player.GetComponent<PlayerController>().inverted = true;
            }
            p_GM.addPowerUp(this);
            gameObject.SetActive(false);
        }
    }

    public void increaseMoveCount()
    {
        countMoves += 1;
        if(countMoves >= m_numMoves)
        {
            if(m_mul == 0)
            {
                player.GetComponent<PlayerController>().moveDist = 1;
            }
            else if(m_mul < 0)
            {
                player.GetComponent<PlayerController>().inverted = false;
            }
            else
            {
                player.GetComponent<PlayerController>().moveDist /= m_mul;
         
            }
            p_GM.removePowerUp(this);
            Destroy(gameObject);
        }
    }
}
