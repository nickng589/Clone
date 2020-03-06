using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubPlayerController : MonoBehaviour
{

    Rigidbody2D playerRB;

    float xAxis;
    float yAxis;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");
        Vector2 movementVector = new Vector2(xAxis, yAxis);
        movementVector = movementVector * 4;
        playerRB.velocity = movementVector;
    }
}
