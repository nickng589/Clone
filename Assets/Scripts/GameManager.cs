using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Editor Variables

    #endregion

    #region Private Variables
    private int numMovingPlayers;
    private ArrayList activePowerUps = new ArrayList();
    #endregion

    #region Public Variables

    public bool playersCanMove;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
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

    public void addPowerUp(PowerUpScript pow)
    {
        activePowerUps.Add(pow);
    }

    public void removePowerUp(PowerUpScript pow)
    {
        activePowerUps.Remove(pow);
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
