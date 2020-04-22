using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class LevelTransitions : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The text file for level order")]
    public TextAsset textFile;


    private string text;
    private string[] lines;
    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("CurrentLevel"))
        {
            PlayerPrefs.SetInt("CurrentLevel", 0);
        }
        text = textFile.text;
        lines = text.Split('\n');
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void NextLevel()
    {
        readTextFile();
    }

    void readTextFile()
    {
        int currLevel = PlayerPrefs.GetInt("CurrentLevel");
        for(int i = 0; i<lines.Length; i++)
        {
            if(i == currLevel + 1)
            {
                string name = lines[i].Trim().Replace("\r", "");
                PlayerPrefs.SetInt("CurrentLevel", currLevel + 1);
                SceneManager.LoadScene(name);
                return;
            }
        }
    }
}
