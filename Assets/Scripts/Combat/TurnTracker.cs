using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTracker : MonoBehaviour
{
    bool playerTurn = true;
    bool enemyTurn = false;
    int turnNumber = 1;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetTurnNumber()
    {
        return turnNumber;
    }
    private void  IncrementTurnNumber()
    {
        turnNumber = turnNumber + 1;
    }

    public bool GetPlayerTurn()
    {
        return playerTurn;
    }
    public bool GetEnemyTurn()
    {
        return enemyTurn;
    }

    public void SetPlayerTurn(bool setting)
    {
        playerTurn = setting;
    }

    public void SetEnemyTurn(bool setting)
    {
        enemyTurn = setting;
    }

    public void NextTurn()
    {
        IncrementTurnNumber();
        if(playerTurn == true)
        {
            SetPlayerTurn(false);
            SetEnemyTurn(true);
        }
        else if ( playerTurn == false)
        {
            SetPlayerTurn(true);
            SetEnemyTurn(false);
        }
    }
}
