using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TeleportController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The teleport location X (with respect to current location)")]
    public int telepX;

    [SerializeField]
    [Tooltip("The teleport location Y (with respect to current location)")]
    public int telepY;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int[] getTPLoc()
    {
        return new int[] { telepX, telepY };
    }
}
