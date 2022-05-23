using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public spawnManager spawnManager;
    public LayerMask movementMask;
    Camera cam;
    PlayerMotor motor;
    bool isMoving;
    NavMeshAgent navMeshAgent;
    public ParticleSystem dust;
    public float speedBoost;
    public AudioClip footSteps;
    private AudioSource asPlayer;
    public GameObject powerUp;
    Animator animator;


    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        asPlayer = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        animator.SetBool("isBattling", false);

    }

    void Update()
    {
        CheckMovement();
        FootPrints();
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit, 50, movementMask))
            {
                motor.MoveToPoint(hit.point);
            }
        }

        if (navMeshAgent.velocity.magnitude >= .00001f)
        {
            
            animator.SetBool("isMoving", true);
        }
        else
        {
            
            animator.SetBool("isMoving", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PowerUp")
        {
            navMeshAgent.speed = 15;
            Destroy(other.gameObject);
            StartCoroutine(PowerUp());
            
        }
    }
    IEnumerator PowerUp()
    {
        yield return new WaitForSeconds(7);
        navMeshAgent.speed = 5;
    }

    void CheckMovement()
    {
        if (navMeshAgent.velocity.magnitude <= 0.1)  
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
    }
    void FootPrints()
    {
        if (isMoving == true)
        {
            CreateDust();
            if (!asPlayer.isPlaying)
            {
                asPlayer.PlayOneShot(footSteps, 0.5f);

            }
        }
        else
        {
            StopDust();
        }
    }

    void CreateDust()
    {
        dust.Play();
    }

    void StopDust()
    {
        dust.Stop();
    }

}
