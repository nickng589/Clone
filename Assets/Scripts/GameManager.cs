using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Editor Variables

    #endregion

    #region Private Variables

    #endregion

    #region Public Variables
    public int numMovingPlayers;
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
        playersCanMove = (numMovingPlayers == 0);
    }
}
