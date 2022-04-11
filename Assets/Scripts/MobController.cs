using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class MobController : MonoBehaviour
{
    [SerializeField] float chaseDistance = 5f;
    [SerializeField] float suspiciousTime = 2f;
    [SerializeField] PatrolPath patrolPath;
    [SerializeField] float waypointTolerance = 2f;
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] float patrolSpeedFraction = .2f;
    [SerializeField] Animator animator;
    [SerializeField] GameObject gameOver;


    GameObject player;
    NavMeshAgent navMeshAgent;


    Vector3 guardPosition;
    float timeSinceLastSawPlayer = Mathf.Infinity;
    int currentWaypointIndex = 0;


    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();



        //mover = GetCompnent<Mover>();
        guardPosition = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (InRangeOfPlayer())
        {
            //print(gameObject.name + " Chase");
            //chase
            animator.SetBool("isChasing", true);
            navMeshAgent.SetDestination(player.transform.position);
            if (gameOver.activeSelf){
                animator.SetBool("isAttacking", true);
            }
            
            timeSinceLastSawPlayer = 0;


        }
        else if (timeSinceLastSawPlayer < suspiciousTime)
        {
            SuspiciousBehavior();
        }
        

        else
        {

            PatrolBehavior();


        }

       timeSinceLastSawPlayer += Time.deltaTime;
    }

    private bool InRangeOfPlayer()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        return distanceToPlayer < chaseDistance;
    }

    private void SuspiciousBehavior()
    {
        navMeshAgent.isStopped = true;
        timeSinceLastSawPlayer += Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);

    }

    private void PatrolBehavior()
    {
       Vector3 nextPosition = guardPosition;
       if (patrolPath != null)
        {
         if (AtWaypoint() == true)
         {
            CycleWaypoint();
         }
            nextPosition = GetCurrentWaypoint();

            navMeshAgent.isStopped = false;
            MoveTo(nextPosition, patrolSpeedFraction);
        }


    }

    private bool AtWaypoint()
    {
        float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
        return distanceToWaypoint < waypointTolerance;
        
    }

    private void CycleWaypoint()
    {
        currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
    }

    private Vector3 GetCurrentWaypoint()
    {
        return patrolPath.GetWaypoint(currentWaypointIndex);
    }

    public void MoveTo(Vector3 destination, float speedFraction)
    {
        navMeshAgent.SetDestination( destination);
        navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
        navMeshAgent.isStopped = false;
    }
}

