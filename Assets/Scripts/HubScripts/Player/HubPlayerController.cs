﻿using System.Collections;
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

    // Start is called before the first frame update
    void Start()
    {
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
            movementVector = movementVector * 4;
            playerRB.velocity = movementVector;
        } else
        {
            playerRB.velocity = Vector2.zero;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Finish") && Input.GetKeyDown("space") && canMove == true)
        {
            SceneManager.LoadScene("Level1");
        }
    }
}
