using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTracker : MonoBehaviour
{
    bool playerTurn = true;
    bool enemyTurn = false;
    int turnNumber = 1;
    public GameObject player;
    public GameObject enemy;
    public BattleSpawn battleSpawn;
    PlayerCombat playerCombat;
    BattleMonster battleMonster;
    int numberOfMonsters;



    // Start is called before the first frame update
    void Start()
    {
        playerCombat = player.GetComponent<PlayerCombat>();
        battleMonster = enemy.GetComponent<BattleMonster>();

        playerCombat.enabled = true;
        player.GetComponent<PlayerController>().enabled = false;
        //numberOfMonsters = battleSpawn.GetNumberOfMonsters();

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
            playerCombat.enabled = false;
            battleMonster.enabled = true;
            SetEnemyTurn(true);

        }
        else if ( playerTurn == false)
        {
            SetPlayerTurn(true);
            playerCombat.enabled = true;
            battleMonster.enabled = false;
            //disable enemy combat script
            SetEnemyTurn(false);
        }
    }
}
