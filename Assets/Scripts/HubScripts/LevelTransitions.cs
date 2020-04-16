using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class LevelTransitions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("CurrentLevel"))
        {
            PlayerPrefs.SetInt("CurrentLevel", 0);
        }
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
        string file_path = "Assets/LevelOrder.txt";
        StreamReader inp_stm = new StreamReader(file_path);
        int count = 0;
        int currLevel = PlayerPrefs.GetInt("CurrentLevel");
        while (!inp_stm.EndOfStream)
        {
            string inp_ln = inp_stm.ReadLine();
            if (count == currLevel+1)
            {
                PlayerPrefs.SetInt("CurrentLevel", currLevel + 1);
                SceneManager.LoadScene(inp_ln);
                return;
            }
            count += 1;
        }

        inp_stm.Close();
    }
}
