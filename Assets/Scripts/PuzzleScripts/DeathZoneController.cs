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
            Destroy(collision.gameObject);
            StartCoroutine(waitOneUpdateCoroutine());
        }
    }

    IEnumerator waitOneUpdateCoroutine() // wait one update to change the players can move value (without this, only the first player can move)
    {
        int i = 0;
        while (i < 1)
        {
            i++;
            yield return null;
        }
        p_GM.BoxRemoved();
        Destroy(gameObject);
    }
}
