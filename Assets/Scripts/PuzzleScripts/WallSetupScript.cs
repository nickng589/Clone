using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSetupScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The sprite for the leaves")]
    private Sprite m_leafSprite;

    [SerializeField]
    [Tooltip("The sprite for the leaves Top gone")]
    private Sprite m_leafSpriteT;

    [SerializeField]
    [Tooltip("The sprite for the Left gone")]
    private Sprite m_leafSpriteL;

    [SerializeField]
    [Tooltip("The sprite for the Right gone")]
    private Sprite m_leafSpriteR;

    [SerializeField]
    [Tooltip("The sprite for the Top Right Gone")]
    private Sprite m_leafSpriteTR;

    [SerializeField]
    [Tooltip("The sprite for the Top Left Gone")]
    private Sprite m_leafSpriteTL;

    [SerializeField]
    [Tooltip("The sprite for the Top Right Left gone")]
    private Sprite m_leafSpriteTRL;

    [SerializeField]
    [Tooltip("The sprite for the Right Left gone")]
    private Sprite m_leafSpriteRL;


    [SerializeField]
    [Tooltip("The sprite for the trees")]
    private Sprite m_treeSprite;

    [SerializeField]
    [Tooltip("The sprite for the trees right gone")]
    private Sprite m_treeSpriteR;

    [SerializeField]
    [Tooltip("The sprite for the trees left gone")]
    private Sprite m_treeSpriteL;

    [SerializeField]
    [Tooltip("The sprite for the trees right left gone")]
    private Sprite m_treeSpriteRL;

    [SerializeField]
    [Tooltip("The sprite for the trees top gone")]
    private Sprite m_treeSpriteT;

    [SerializeField]
    [Tooltip("The sprite for the trees top left gone")]
    private Sprite m_treeSpriteTL;

    [SerializeField]
    [Tooltip("The sprite for the trees top right left gone")]
    private Sprite m_treeSpriteTRL;

    [SerializeField]
    [Tooltip("The sprite for the trees top right gone")]
    private Sprite m_treeSpriteTR;


    [SerializeField]
    [Tooltip("The SpriteRenderer for the wall")]
    private SpriteRenderer m_sprRend;

    // Start is called before the first frame update
    void Start()
    {
        updateVisuals();
    }
    public void updateVisuals()
    {
        if (m_sprRend != null)
        {
            bool bottom = false;
            bool top = false;
            bool right = false;
            bool left = false;
            LayerMask mask = LayerMask.GetMask("Wall");
            RaycastHit2D hitBottom = Physics2D.Raycast((Vector2)transform.position + Vector2.down, Vector2.down, 0.1f, mask);
            RaycastHit2D hitTop = Physics2D.Raycast((Vector2)transform.position + Vector2.up, Vector2.up, 0.1f, mask);
            RaycastHit2D hitRight = Physics2D.Raycast((Vector2)transform.position + Vector2.right, Vector2.right, 0.1f, mask);
            RaycastHit2D hitLeft = Physics2D.Raycast((Vector2)transform.position + Vector2.left, Vector2.left, 0.1f, mask);
            if(hitBottom.collider != null)
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
                    m_sprRend.sprite = m_leafSprite;
                }
                else if (!top && right && left)
                {
                    m_sprRend.sprite = m_leafSpriteT;
                }
                else if (top && !right && left)
                {
                    m_sprRend.sprite = m_leafSpriteR;
                }
                else if (top && right && !left)
                {
                    m_sprRend.sprite = m_leafSpriteL;
                }
                else if (!top && !right && left)
                {
                    m_sprRend.sprite = m_leafSpriteTR;
                }
                else if (!top && right && !left)
                {
                    m_sprRend.sprite = m_leafSpriteTL;
                }
                else if (!top && !right && !left)
                {
                    m_sprRend.sprite = m_leafSpriteTRL;
                }
                else if (top && !right && !left)
                {
                    m_sprRend.sprite = m_leafSpriteRL;
                }

            }
            else
            {
                if (top && right && left)
                {
                    m_sprRend.sprite = m_treeSprite;
                }
                else if (!top && right && left)
                {
                    m_sprRend.sprite = m_treeSpriteT;
                }
                else if (top && !right && left)
                {
                    m_sprRend.sprite = m_treeSpriteR;
                }
                else if (top && right && !left)
                {
                    m_sprRend.sprite = m_treeSpriteL;
                }
                else if (!top && !right && left)
                {
                    m_sprRend.sprite = m_treeSpriteTR;
                }
                else if (!top && right && !left)
                {
                    m_sprRend.sprite = m_treeSpriteTL;
                }
                else if (!top && !right && !left)
                {
                    m_sprRend.sprite = m_treeSpriteTRL;
                }
                else if (top && !right && !left)
                {
                    m_sprRend.sprite = m_treeSpriteRL;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
