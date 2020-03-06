    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("The number of goals")]
    private int m_numGoals;
    #endregion

    #region Private Variables
    private int numMovingPlayers;
    private ArrayList activePowerUps = new ArrayList();
    private int numInGoal;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseNumMoving()
    {
        numMovingPlayers += 1;
        StartCoroutine(waitOneUpdateCoroutine());
    }

    public void DecreaseNumMoving()
    {
        numMovingPlayers -= 1;
        if(numMovingPlayers == 0)
        {
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
