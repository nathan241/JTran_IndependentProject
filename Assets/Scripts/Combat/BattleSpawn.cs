using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSpawn : MonoBehaviour
{

    int numberOfMonsters;
    public GameObject position1;
    public GameObject position2;
    public GameObject position3;
    public GameObject monster;
    bool monstersSpawned = false;

    
    void Start()
    {
        numberOfMonsters = Random.Range( 1, 2);
        Transform transform1 = position1.transform;
        Transform transform2 = position2.transform;
        Transform transform3 = position3.transform;

    }

    // Update is called once per frame
    void Update()
    {
        if(numberOfMonsters == 1 && monstersSpawned == false)
        {
            Instantiate(monster, position1.transform);
            monstersSpawned = true;
        }
        else if (numberOfMonsters == 2 && monstersSpawned == false)
        {
            Instantiate(monster, position1.transform);
            Instantiate(monster, position2.transform);
            monstersSpawned = true;
        }
        else if (numberOfMonsters == 3 && monstersSpawned == false)
        {
            Instantiate(monster, position1.transform);
            Instantiate(monster, position2.transform);
            Instantiate(monster, position3.transform);
            monstersSpawned = true;
        }


    }
    public int GetNumberOfMonsters()
    {
        return numberOfMonsters;
    }
}
