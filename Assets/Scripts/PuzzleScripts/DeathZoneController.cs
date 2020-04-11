using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZoneController : MonoBehaviour
{

    [SerializeField]
    [Tooltip("The game object for the bridge made when box is pushed into death zone")]
    private GameObject m_bridgeGO;
    [SerializeField]
    [Tooltip("The sprite for the leaves")]
    private Sprite sprite;

    [SerializeField]
    [Tooltip("The sprite for the leaves Top gone")]
    private Sprite spriteT;

    [SerializeField]
    [Tooltip("The sprite for the Left gone")]
    private Sprite spriteL;

    [SerializeField]
    [Tooltip("The sprite for the Right gone")]
    private Sprite spriteR;

    [SerializeField]
    [Tooltip("The sprite for the Top Right Gone")]
    private Sprite spriteTR;

    [SerializeField]
    [Tooltip("The sprite for the Top Left Gone")]
    private Sprite spriteTL;

    [SerializeField]
    [Tooltip("The sprite for the Top Right Left gone")]
    private Sprite spriteTRL;

    [SerializeField]
    [Tooltip("The sprite for the Right Left gone")]
    private Sprite spriteRL;


    [SerializeField]
    [Tooltip("The sprite for the trees")]
    private Sprite spriteB;

    [SerializeField]
    [Tooltip("The sprite for the trees right gone")]
    private Sprite spriteBR;

    [SerializeField]
    [Tooltip("The sprite for the trees left gone")]
    private Sprite spriteBL;

    [SerializeField]
    [Tooltip("The sprite for the trees right left gone")]
    private Sprite spriteBRL;

    [SerializeField]
    [Tooltip("The sprite for the trees top gone")]
    private Sprite spriteTB;

    [SerializeField]
    [Tooltip("The sprite for the trees top left gone")]
    private Sprite spriteTBL;

    [SerializeField]
    [Tooltip("The sprite for the trees top right left gone")]
    private Sprite spriteTBRL;

    [SerializeField]
    [Tooltip("The sprite for the trees top right gone")]
    private Sprite spriteTBR;


    [SerializeField]
    [Tooltip("The SpriteRenderer for the wall")]
    private SpriteRenderer m_sprRend;

    private GameManager p_GM;
    // Start is called before the first frame update
    void Start()
    {
        p_GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        updateVisuals();
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
    public void updateVisuals()
    {
        if (m_sprRend != null)
        {
            bool bottom = false;
            bool top = false;
            bool right = false;
            bool left = false;
            LayerMask mask = LayerMask.GetMask("DeathZone");
            RaycastHit2D hitBottom = Physics2D.Raycast((Vector2)transform.position + Vector2.down, Vector2.down, 0.1f, mask);
            RaycastHit2D hitTop = Physics2D.Raycast((Vector2)transform.position + Vector2.up, Vector2.up, 0.1f, mask);
            RaycastHit2D hitRight = Physics2D.Raycast((Vector2)transform.position + Vector2.right, Vector2.right, 0.1f, mask);
            RaycastHit2D hitLeft = Physics2D.Raycast((Vector2)transform.position + Vector2.left, Vector2.left, 0.1f, mask);
            if (hitBottom.collider != null)
            {
                bottom = true;
                //hitBottom.collider.gameObject.GetComponent<WallSetupScript>().updateVisuals();
            }
            if (hitTop.collider != null)
            {
                top = true;
                //hitTop.collider.gameObject.GetComponent<WallSetupScript>().updateVisuals();
            }
            if (hitRight.collider != null)
            {
                right = true;
                //hitRight.collider.gameObject.GetComponent<WallSetupScript>().updateVisuals();
            }
            if (hitLeft.collider != null)
            {
                left = true;
                //hitLeft.collider.gameObject.GetComponent<WallSetupScript>().updateVisuals();
            }

            if (bottom)
            {
                if (top && right && left)
                {
                    m_sprRend.sprite = sprite;
                }
                else if (!top && right && left)
                {
                    m_sprRend.sprite = spriteT;
                }
                else if (top && !right && left)
                {
                    m_sprRend.sprite = spriteR;
                }
                else if (top && right && !left)
                {
                    m_sprRend.sprite = spriteL;
                }
                else if (!top && !right && left)
                {
                    m_sprRend.sprite = spriteTR;
                }
                else if (!top && right && !left)
                {
                    m_sprRend.sprite = spriteTL;
                }
                else if (!top && !right && !left)
                {
                    m_sprRend.sprite = spriteTRL;
                }
                else if (top && !right && !left)
                {
                    m_sprRend.sprite = spriteRL;
                }

            }
            else
            {
                if (top && right && left)
                {
                    m_sprRend.sprite = spriteB;
                }
                else if (!top && right && left)
                {
                    m_sprRend.sprite = spriteTB;
                }
                else if (top && !right && left)
                {
                    m_sprRend.sprite = spriteBR;
                }
                else if (top && right && !left)
                {
                    m_sprRend.sprite = spriteBL;
                }
                else if (!top && !right && left)
                {
                    m_sprRend.sprite = spriteTBR;
                }
                else if (!top && right && !left)
                {
                    m_sprRend.sprite = spriteTBL;
                }
                else if (!top && !right && !left)
                {
                    m_sprRend.sprite = spriteTBRL;
                }
                else if (top && !right && !left)
                {
                    m_sprRend.sprite = spriteBRL;
                }
            }
        }
    }


}
