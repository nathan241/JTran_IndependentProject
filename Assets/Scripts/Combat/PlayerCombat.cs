using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    Camera cam;
    PlayerMotor motor;
    public LayerMask clickableInCombat;
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public float movementTime = 1f;
    public float returnDelay = 1f;
    public float rotationSpeed = 1f;
    public float endOfTurnDelay = 1f;
    public TurnTracker turnTracker;
    bool playerTurn = true;
    public GameObject enemyMonster;
    BattleMonster battleMonster;

    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = gameObject.transform.position;
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        battleMonster = enemyMonster.GetComponent<BattleMonster>();
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerTurn();
        PlayerAttack();
    }

    public void UpdatePlayerTurn()
    {
        playerTurn = turnTracker.GetPlayerTurn();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }


    public void PlayerAttack()
    {
        if (Input.GetMouseButtonDown(0) && playerTurn == true)
        {
            StartCoroutine(PlayerAttackCoroutine()); 
        }
    }


    IEnumerator PlayerAttackCoroutine()
    {

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 50, clickableInCombat))
        {
            if (hit.collider != null)
            {
                motor.MoveToEnemy(hit.transform.Find("attackPosition").transform.position);
                yield return new WaitForSeconds(movementTime);
            }

            if (Vector3.Distance(hit.transform.Find("attackPosition").transform.position, gameObject.transform.position) < 0.1f)
            {
                battleMonster.TakeDamage(50);
                print("damagetaken");
            }

            yield return new WaitForSeconds(returnDelay);
            motor.MoveToPoint(startPosition);
            yield return new WaitForSeconds(movementTime);

            var oldRotation = transform.rotation;
            transform.Rotate(0, 180, 0);
            var newRotation = transform.rotation;
            for(float t = 0; t <= 1.0; t+= Time.deltaTime * rotationSpeed)
            {
                transform.rotation = Quaternion.Slerp(oldRotation, newRotation, t);
                yield return null;
            }
            transform.rotation = newRotation;

            yield return new WaitForSeconds(endOfTurnDelay);
            turnTracker.NextTurn();
        }

    }
}
