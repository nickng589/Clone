using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public bool canMove;

    public bool walking;

    public float walkTimer;

    public int ydir;

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

    // Update is called once per frame
    void Update()
    {
        walkTimer -= Time.deltaTime;
        if (canMove == false)
        {
            npcRigidBody.velocity = Vector2.zero;
        }
        if (walkTimer <= 0)
        {
            Debug.Log("Start");
            if (canMove & !walking)
            {
                Debug.Log("first");
                Vector2 movementVector = new Vector2(1 * ydir, 0);
                npcRigidBody.velocity = movementVector;
                walkTimer = 1;
                walking = true;
                ydir *= -1;
            } else if (walking)
            {
                Debug.Log("second");
                walkTimer = 1;
                walking = false;
                npcRigidBody.velocity = Vector2.zero;
            }
        }
    }
}
