using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class actionEvent : MonoBehaviour
{
    public actionRespawn ar;
    public GameObject[] destroyedObjects;
    public GameObject ActionEvent;
    public GameObject player;
    Vector3 position;
    public GameObject canvas;
    public Slider slider;
    NavMeshAgent agent;
    bool insideTrigger = false;

    private void Start()
    {
        player = GameObject.Find("Player");
        ar = GameObject.Find("ActionManager").GetComponent<actionRespawn>();
        position = this.transform.position;
        canvas.SetActive(false);
        agent = player.GetComponent<NavMeshAgent>();


        
    }

    private void Update()
    {
        if (insideTrigger == true)
        {
            HarvestAction();
        }
    }

    private void HarvestAction()
    {
        if (Input.GetKey(KeyCode.G))
        {

            agent.SetDestination(player.transform.position);
            player.transform.LookAt(position);

            slider.value += Time.deltaTime;

        }
        if (slider.value > 0 && !Input.GetKey(KeyCode.G))
        {
            slider.value -= Time.deltaTime;
        }
        if (slider.value == slider.maxValue)
        {
            ar.UpdatePositionArray(transform.position);
            slider.value = slider.minValue;
            Destroy(ActionEvent);

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            canvas.SetActive(true);
            insideTrigger = true;

        }
            
     }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canvas.SetActive(false);
            insideTrigger = false;
        }
    }


}
