using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Saving;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour, ISaveable
{
    Camera cam;
    PlayerMotor motor;
    public LayerMask clickableInCombat;
    public float maxHealth = 100;
    public float currentHealth;
    public HealthBar healthBar;
    public float movementTime = 1f;
    public float returnDelay = 1f;
    public float rotationSpeed = 1f;
    public float endOfTurnDelay = 1f;
    public TurnTracker turnTracker;
    bool playerTurn = true;
    public float damage = 20.0f;
    public GameObject enemyMonster;
    bool attackInitiated = false;
    BattleMonster battleMonster;
    Animator animator;

    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = gameObject.transform.position;
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
        //currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
        if (enemyMonster == null)
            return;
        else
        {
            battleMonster = enemyMonster.GetComponent<BattleMonster>();
        }
        animator = GetComponent<Animator>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            StartCoroutine(PlayerDeath());

        }
        UpdatePlayerTurn();
        if (turnTracker == null)
            return;
        else
        {
            if (turnTracker.GetPlayerTurn() == true)
            {
                animator.SetBool("isBattling", true);
            }
        }
        PlayerAttack();
        
    }

    IEnumerator PlayerDeath()
    {
        animator.SetBool("isDead", true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }
    public void UpdatePlayerTurn()
    {
        if (turnTracker == null)
            return;
        else
        {
            playerTurn = turnTracker.GetPlayerTurn();
        }

    }

    public void TakeDamage(float damage)
    {
        maxHealth -= damage;
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }


    public void PlayerAttack()
    {
        
        if (Input.GetMouseButtonDown(0) && playerTurn == true && attackInitiated == false)
        {
            print("mouse click occured");
            StartCoroutine(PlayerAttackCoroutine());
            attackInitiated = true;
        }
    }


    IEnumerator PlayerAttackCoroutine()
    {

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        print("Coroutine started");
        if (Physics.Raycast(ray, out hit, 50, clickableInCombat))
        {
            if (hit.collider != null)
            {
                motor.MoveToEnemy(hit.transform.Find("attackPosition").transform.position);
                animator.SetBool("isMoving", true);
                yield return new WaitForSeconds(movementTime);
                
            }
            

            if (Vector3.Distance(hit.transform.Find("attackPosition").transform.position, gameObject.transform.position) < 0.1f)
            {
                animator.SetBool("isMoving", false);
                yield return new WaitForSeconds(returnDelay);
                animator.SetBool("isAttacking", true);
                battleMonster.TakeDamage(damage);
            }

            yield return new WaitForSeconds(returnDelay);
            animator.SetBool("isAttacking", false);
            animator.SetBool("isMoving", true);
            motor.MoveToPoint(startPosition);
            yield return new WaitForSeconds(movementTime);
            animator.SetBool("isMoving", false);

            var oldRotation = transform.rotation;
            //transform.Rotate(0, 180, 0);
            //var eulerRotation = new Vector3(0, -90, 0);
            var newRotation = Quaternion.Euler(0, -90, 0);
            for(float t = 0; t <= 1.0; t+= Time.deltaTime * rotationSpeed)
            {
                transform.rotation = Quaternion.Slerp(oldRotation, newRotation, t);
                yield return null;
            }
            transform.rotation = newRotation;

            yield return new WaitForSeconds(endOfTurnDelay);
            turnTracker.NextTurn();
            attackInitiated = false;
        }

    }

    public object CaptureState()
    {
        return currentHealth;
    }

    public void RestoreState(object state)
    {
        currentHealth = (float)state;
    }
}
