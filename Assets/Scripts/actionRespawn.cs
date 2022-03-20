using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actionRespawn : MonoBehaviour
{
    public Vector3[] positionArray = new Vector3[81];
    public GameObject[] treeArray;
    public GameObject[] oreArray;
    public GameObject[] resourceType;

    public float respawnTimer = 10;
    int randomNumber;

    // Start is called before the first frame update
    void Start()
    {
        treeArray = GameObject.FindGameObjectsWithTag("treeAction");
        oreArray = GameObject.FindGameObjectsWithTag("oreAction");
    }

    // Update is called once per frame
    void Update()
    {
        treeArray = GameObject.FindGameObjectsWithTag("treeAction");
        oreArray = GameObject.FindGameObjectsWithTag("oreAction");
        RespawnRandom();
     
    }

    public void UpdatePositionArray(Vector3 xyzCoords)
    {
        
        for (int i = 0; i < positionArray.Length; i++)
        {
            if (positionArray[i] == Vector3.zero)
            {
                positionArray[i] = xyzCoords;
                break;
            }
        }

    }

    public void RespawnRandom()
    {

        //check if posArray has coords
        //if yes then start respawn timer
        //randomize respawn timer
        //grab coords in 1st position array and instantiate ore or tree
        //set position to 000 (remove set of coords from array)
        for (int i = 0; i < positionArray.Length; i++) {
            if (positionArray[i] != Vector3.zero)
            {
                if (respawnTimer > 0)
                {
                    respawnTimer -= Time.deltaTime;                
                }
                else
                {
                    randomNumber = Random.Range(0, 2);
                    if (randomNumber == 0)
                    {
                        Instantiate(resourceType[0], positionArray[i], Quaternion.identity);
                        positionArray[i] = Vector3.zero;
                        respawnTimer = 10;
                    }
                    else
                    {
                        Instantiate(resourceType[1], positionArray[i], Quaternion.identity);
                        positionArray[i] = Vector3.zero;
                        respawnTimer = 10;
                    }
                }
            }

        }
    }
}
