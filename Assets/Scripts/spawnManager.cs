using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    public GameObject[] fish;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("FishSpawn", 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
     //30 -80 for z -30 80 for x, y = 0
    void FishSpawn()
    {
        float randX = Random.Range(-30, 80);
        float randZ = Random.Range(-80, 30);
        float Y = 0;
        Vector3 randPos = new Vector3(randX, Y, randZ);
        Instantiate(fish[0], randPos, fish[0].transform.rotation);
    }
}
