using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroCutsceneTrigger : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The text file for level order")]
    public TextAsset textFile;

    private string text;
    private string[] lines;
    public void Start()
    {
        if (!PlayerPrefs.HasKey("CurrentLevel"))
        {
            PlayerPrefs.SetInt("CurrentLevel", 0);
        }
        text = textFile.text;
        lines = text.Split('\n');
    }
    public void Update()
    {
        if (Input.anyKeyDown) {
            //change this line
            int currLevel = PlayerPrefs.GetInt("CurrentLevel");
            if(currLevel == 0)
            {
                string name = lines[1].Trim().Replace("\r", "");
                PlayerPrefs.SetInt("CurrentLevel", currLevel + 1);
                SceneManager.LoadScene(name);
            }
            else
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    if (i == currLevel)
                    {
                        for (int j = i; j >= 0; j--)
                        {
                            string tempName = lines[j].Trim().Replace("\r", "");
                            if (tempName.Contains("Hub"))
                            {
                                SceneManager.LoadScene(tempName);
                                return;
                            }
                        }
                        string name = lines[i].Trim().Replace("\r", "");
                        SceneManager.LoadScene(name);
                        return;
                    }
                }
            }
            
        }
    }
}
