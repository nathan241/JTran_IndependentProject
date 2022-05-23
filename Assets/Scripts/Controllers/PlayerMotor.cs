using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Saving;

public class PlayerMotor : MonoBehaviour//ISaveable
{
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }

    public void MoveToEnemy(Vector3 attackPosition)
    {
        agent.SetDestination(attackPosition);
    }

/*    public object CaptureState()
    {
        return new SerializableVector3(transform.position);
    }

    public void RestoreState(object state)
    {
        SerializableVector3 position = (SerializableVector3)state;
        GetComponent<NavMeshAgent>().enabled = false;
        transform.position = position.ToVector();
        GetComponent<NavMeshAgent>().enabled = true;
    }*/
}
