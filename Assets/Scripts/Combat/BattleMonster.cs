using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BattleMonster : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    public HealthBar healthBar;
    public TurnTracker turnTracker;
    bool playerTurn = true;
    bool enemyTurn = false;
    public GameObject enemyAttackPosition;
    public float movementTime = 2f;
    public float rotationSpeed = 3f;
    public float returnDelay = 1f;
    public float endOfTurnDelay = 1f;
    bool coroutineStarted = false;
    public float damage = 10.0f;
    public GameObject player;
    PlayerCombat playerCombat;
    bool dead = false;
    public Animator animator;
    bool deathStarted = false;



    BattleTransition battleTransition;
    NavMeshAgent navMeshAgent;
    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        startPosition = transform.position;
        navMeshAgent = GetComponent<NavMeshAgent>();
        battleTransition = GetComponent<BattleTransition>();
        playerCombat = player.GetComponent<PlayerCombat>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTurns();
        Death();
        EnemyAttack();
    }

    public void Death()
    {
        if(currentHealth <= 0 && deathStarted == false)
        {
            deathStarted = true;
            dead = true;
            StartCoroutine(DeathCoroutine());
        }
    }

    IEnumerator DeathCoroutine()
    {
        animator.SetBool("isDead", true);
        yield return new WaitForSeconds(2);

        StartCoroutine(battleTransition.Transition());

    }



    public void UpdateTurns()
    {
        playerTurn = turnTracker.GetPlayerTurn();
        enemyTurn = turnTracker.GetEnemyTurn();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public void EnemyAttack()
    {
        if (coroutineStarted == false && playerTurn == false && dead == false)
        {
            coroutineStarted = true;
            StartCoroutine(EnemyAttackCoroutine());
        }


    }


    IEnumerator EnemyAttackCoroutine()
    {
        print(enemyAttackPosition.transform.position);
        navMeshAgent.SetDestination(enemyAttackPosition.transform.position);
        //navMeshAgent.SetDestination(enemyAttackPosition.transform.position);
     
        yield return new WaitForSeconds(movementTime);
        
            

        if (Vector3.Distance(enemyAttackPosition.transform.position, gameObject.transform.position) < 1f)
        {
            animator.SetBool("isAttacking", true);
            yield return new WaitForSeconds(returnDelay);
            playerCombat.TakeDamage(damage);
            animator.SetBool("isAttacking", false);

        }

        yield return new WaitForSeconds(returnDelay);
        navMeshAgent.SetDestination(startPosition);
        yield return new WaitForSeconds(movementTime);

        var oldRotation = transform.rotation;

        var newRotation = Quaternion.Euler(0, 90, 0);
        for (float t = 0; t <= 1.0; t += Time.deltaTime * rotationSpeed)
        {
            transform.rotation = Quaternion.Slerp(oldRotation, newRotation, t);
            yield return null;
        }
        transform.rotation = newRotation;
        yield return new WaitForSeconds(endOfTurnDelay);
        turnTracker.NextTurn();
        coroutineStarted = false;

    }
}


