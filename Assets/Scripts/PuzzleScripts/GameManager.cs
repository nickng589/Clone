using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("The number of goals")]
    private int m_numGoals;

    [SerializeField]
    [Tooltip("The text object for when player beats the level")]
    private Text m_VictoryText;

    [SerializeField]
    [Tooltip("The text to show when the player beats the level")]
    private string m_VictoryMessage;

    [SerializeField]
    [Tooltip("The slider to adjust move speed")]
    private Slider m_MoveSpeedSlider;

    [SerializeField]
    [Tooltip("The speed of player")]
    private float m_defaultSpeed;

    #endregion

    #region Private Variables
    private int numMovingPlayers;
    private ArrayList activePowerUps = new ArrayList();
    private int numInGoal;
    private GameObject[] boxes;
    private GameObject[] players;
    private float m_speed;
    #endregion

    #region Public Variables

    public bool playersCanMove;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        numInGoal = 0;
        numMovingPlayers = 0;
        playersCanMove = true;
        m_VictoryText.text = "";
        boxes = GameObject.FindGameObjectsWithTag("Box");
        players = GameObject.FindGameObjectsWithTag("Player");
        m_MoveSpeedSlider.onValueChanged.AddListener(delegate { SliderValueChanged(m_MoveSpeedSlider); });
        m_speed = m_defaultSpeed;
        foreach (GameObject player in players)
        {
            player.GetComponent<PlayerController>().m_speed = m_defaultSpeed;
        }

        foreach (GameObject box in boxes)
        {
            box.GetComponent<BoxController>().m_speed = m_defaultSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            SceneManager.LoadScene("HubWorld");
        }

        if (Input.GetKey(KeyCode.E)&&playersCanMove)
        {
            Wait();   
        }
    }

    void SliderValueChanged(Slider changed)
    {
        float newMoveSpeed = m_defaultSpeed + (5 - changed.value) * 0.1f;
        m_speed = newMoveSpeed;
        foreach(GameObject player in players)
        {
            player.GetComponent<PlayerController>().m_speed = newMoveSpeed;
        }

        foreach (GameObject box in boxes)
        {
            box.GetComponent<BoxController>().m_speed = newMoveSpeed;
        }
    }

    public void IncreaseNumMoving(bool fromBox = false)
    {
        if(!fromBox && numMovingPlayers==0)
        {
            StartedMoving();
        }
        numMovingPlayers += 1;
        StartCoroutine(waitOneUpdateCoroutine());
    }

    public void BoxRemoved()
    {
        boxes = GameObject.FindGameObjectsWithTag("Box");
    }


    public void StartedMoving()
    {
        foreach(GameObject box in boxes)
        {
            box.GetComponent<BoxController>().OnPlayerMove();
        }
    }

    public void Wait()
    {
        foreach(GameObject player in players)
        {
            player.GetComponent<PlayerController>().OnWait();
        }
        foreach (GameObject box in boxes)
        {
            box.GetComponent<BoxController>().OnPlayerMove();
        }
    }

    public void DecreaseNumMoving()
    {
        numMovingPlayers -= 1;
        if(numMovingPlayers <= 0)
        {
            numMovingPlayers = 0;
            playersCanMove = true;
            foreach(PowerUpScript pow in activePowerUps)
            {
                pow.increaseMoveCount();
            }
        }   
    }

    public void IncreaseInGoal()
    {
        numInGoal += 1;
        if(numInGoal >= m_numGoals)
        {
            win();
        }
    }

    public void DecreaseInGoal()
    {
        numInGoal -= 1;
    }

    public void addPowerUp(PowerUpScript pow)
    {
        activePowerUps.Add(pow);
    }

    public void removePowerUp(PowerUpScript pow)
    {
        activePowerUps.Remove(pow);
    }

    public void win()
    {
        m_VictoryText.text = m_VictoryMessage;
        playersCanMove = false;
        StartCoroutine(freezeCoroutine(3));    
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(m_speed);
        playersCanMove = true;
    }

    IEnumerator freezeCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("HubWorld");
    }
    IEnumerator waitOneUpdateCoroutine() // wait one update to change the players can move value (without this, only the first player can move)
    {
        int i = 0;
        while(i<1)
        {
            i++;
            yield return null;
        }
        playersCanMove = false;
    }
}
