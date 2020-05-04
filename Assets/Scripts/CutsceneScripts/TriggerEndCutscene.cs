using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerEndCutscene : MonoBehaviour
{
    public string end1name;
    public string end2name;
    public string end3name;

    public int moralityThreshold1;
    public int moralityThreshold2;

    void Start()
    {
        if (PlayerPrefs.GetInt("morality") < moralityThreshold1)
        {
            SceneManager.LoadScene(end1name);
        }
        else if (PlayerPrefs.GetInt("morality") < moralityThreshold2)
        {
            SceneManager.LoadScene(end2name);
        }
        else
        {
            SceneManager.LoadScene(end3name);
        }
    }
}
