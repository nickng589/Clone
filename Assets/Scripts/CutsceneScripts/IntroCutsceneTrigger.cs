using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroCutsceneTrigger : MonoBehaviour
{
    public void Update()
    {
        if (Input.anyKeyDown) {
            //change this line
            SceneManager.LoadScene("HubWorld"); 
        }
    }
}
