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
    public Animator playerAnim;

    float xAxis;
    float yAxis;

    public bool canMove;
    private bool moving = false;
    private AudioSource source;
    private AudioClip walking;

    [SerializeField]
    private float walkLeftAnimSpeed;
    [SerializeField]
    private float walkRightAnimSpeed;
    [SerializeField]
    private float walkUpAnimSpeed;
    [SerializeField]
    private float walkDownAnimSpeed;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        walking = source.clip;
        canMove = true;
        playerRB = GetComponent<Rigidbody2D>();
        playerAnim.SetFloat("LeftSpeed", walkLeftAnimSpeed);
        playerAnim.SetFloat("RightSpeed", walkRightAnimSpeed);
        playerAnim.SetFloat("UpSpeed", walkUpAnimSpeed);
        playerAnim.SetFloat("DownSpeed", walkDownAnimSpeed);
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
                SetAnim(movementVector, yAxis);
                if(!moving)
                {
                    source.Play();
                    moving = true;
                }
            }
            else
            {
                ResetAnim();
                if (moving)
                {
                    source.Stop();
                    moving = false;
                }
            }
            movementVector = movementVector * 4;
            playerRB.velocity = movementVector;
        } else
        {
            ResetAnim();
            playerRB.velocity = Vector2.zero;
        }
    }

    void SetAnim(Vector2 movement, float yAxis) {
        //use angles to determine which anim to use, relative to x axis
        playerAnim.SetTrigger("Walk");
        float angle = Vector2.Angle(Vector2.right, movement);
        if (angle <= 45) {
            playerAnim.SetBool("WalkRight", true);
            playerAnim.SetBool("WalkUp", false);
            playerAnim.SetBool("WalkLeft", false);
            playerAnim.SetBool("WalkDown", false);
        } else if (angle <= 90+45 && yAxis > 0) {
            playerAnim.SetBool("WalkUp", true);
            playerAnim.SetBool("WalkRight", false);
            playerAnim.SetBool("WalkLeft", false);
            playerAnim.SetBool("WalkDown", false);
        } else if (angle <= 90 + 45 && yAxis < 0) {
            playerAnim.SetBool("WalkDown", true);
            playerAnim.SetBool("WalkUp", false);
            playerAnim.SetBool("WalkRight", false);
            playerAnim.SetBool("WalkLeft", false);
        } else if (angle <= 180) {
            playerAnim.SetBool("WalkLeft", true);
            playerAnim.SetBool("WalkUp", false);
            playerAnim.SetBool("WalkRight", false);
            playerAnim.SetBool("WalkDown", false);
        }
    }

    void ResetAnim() {
        playerAnim.SetBool("WalkUp", false);
        playerAnim.SetBool("WalkRight", false);
        playerAnim.SetBool("WalkLeft", false);
        playerAnim.SetBool("WalkDown", false);
    }

}
