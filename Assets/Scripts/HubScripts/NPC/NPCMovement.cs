using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public bool canMove;
    public bool moveVertical;

    public bool walking;
    public int ydir;
    //public int ydist;

    public float timerMax;

    [SerializeField]
    private string tag_anim1;

    [SerializeField]
    private string tag_anim2;

    [SerializeField]
    private Animator anim;

    private bool useAnim1;
    private bool useAnim2;

    private float walkTimer;

    Rigidbody2D npcRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        ydir = -1;
        if (timerMax == 0) {
            timerMax = 1;
        }
        walking = false;
        canMove = true;
        walkTimer = timerMax;
        npcRigidBody = GetComponent<Rigidbody2D>();
        useAnim1 = true;
        useAnim2 = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponentInParent<NPCMovement>().canMove = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponentInParent<NPCMovement>().canMove = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove == false)
        {
            npcRigidBody.velocity = Vector2.zero;
            SetAnim(false, false);
        }
        else
        {
            walkTimer -= Time.deltaTime;
            if (walkTimer <= 0)
            {
                if (!walking)
                {
                    //actually do movement
                    SetAnim(false, false);
                    Flip();
                    walkTimer = timerMax;
                    walking = true;
                    ydir *= -1;
                }
                else if (walking)
                {
                    //Turn around, after timer elapses initiate above clause
                    SetAnim(false, false);
                    walkTimer = timerMax;
                    walking = false;
                    //npcRigidBody.velocity = Vector2.zero;
                }
            }
            if (walking)
            {
                Vector2 movementVector = new Vector2(1 * ydir, 0);
                if (moveVertical)
                {
                    movementVector = new Vector2(0, 1 * ydir);
                }
                
                npcRigidBody.velocity = movementVector;
                SetAnim(useAnim1, useAnim2);
            } else
            {
                npcRigidBody.velocity = Vector2.zero;
            }
        }
    }

    void SetAnim(bool v1, bool v2) {
        if (anim != null)
        {
            anim.SetBool(tag_anim1, v1);
            anim.SetBool(tag_anim2, v2);
        }
    }

    void Flip() {
        useAnim1 = !useAnim1;
        useAnim2 = !useAnim2;
    }
}
