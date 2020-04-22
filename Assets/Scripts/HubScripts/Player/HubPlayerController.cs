using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HubPlayerController : MonoBehaviour
{

    Rigidbody2D playerRB;

    float xAxis;
    float yAxis;

    public bool canMove;
    private bool moving = false;
    private AudioSource source;
    private AudioClip walking;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        walking = source.clip;
        canMove = true;
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            xAxis = Input.GetAxisRaw("Horizontal");
            yAxis = Input.GetAxisRaw("Vertical");
            Vector2 movementVector = new Vector2(xAxis, yAxis);
            if(Mathf.Abs(xAxis) > 0.1f || Mathf.Abs(yAxis) > 0.1f)
            {
                if(!moving)
                {
                    source.Play();
                    moving = true;
                }
            }
            else
            {
                if(moving)
                {
                    source.Stop();
                    moving = false;
                }
            }
            movementVector = movementVector * 4;
            playerRB.velocity = movementVector;
        } else
        {
            playerRB.velocity = Vector2.zero;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        /*if (other.CompareTag("Finish") && Input.GetKeyDown("space") && canMove == true)
        {
            SceneManager.LoadScene("Level1");
        }*/
    }
}
