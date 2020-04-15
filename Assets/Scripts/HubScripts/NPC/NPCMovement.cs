using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public bool canMove;

    public bool walking;

    public float walkTimer;

    public int ydir;

    public int ydist;

    Rigidbody2D npcRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        ydir = -1;
        walking = false;
        canMove = true;
        walkTimer = 1;
        npcRigidBody = GetComponent<Rigidbody2D>();
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
        }
        else
        {
            walkTimer -= Time.deltaTime;
            if (walkTimer <= 0)
            {
                Debug.Log("Start");
                if (!walking)
                {
                    Debug.Log("first");
                    //Vector2 movementVector = new Vector2(1 * ydir, 0);
                    //npcRigidBody.velocity = movementVector;
                    walkTimer = 1;
                    walking = true;
                    ydir *= -1;
                }
                else if (walking)
                {
                    Debug.Log("second");
                    walkTimer = 1;
                    walking = false;
                    //npcRigidBody.velocity = Vector2.zero;
                }
            }
            if (walking)
            {
                Vector2 movementVector = new Vector2(1 * ydir, 0);
                npcRigidBody.velocity = movementVector;
            } else
            {
                npcRigidBody.velocity = Vector2.zero;
            }
        }
    }
}
