using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZoneController : MonoBehaviour
{

    [SerializeField]
    [Tooltip("The game object for the bridge made when box is pushed into death zone")]
    private GameObject m_bridgeGO;

    private GameManager p_GM;
    // Start is called before the first frame update
    void Start()
    {
        p_GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (collision.gameObject.tag == "Box")
        {
            Instantiate(m_bridgeGO, gameObject.transform.position, Quaternion.identity);
            p_GM.DecreaseNumMoving();
            p_GM.DecreaseNumMoving();
            p_GM.removeFromMatrix(collision.gameObject);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }


}
