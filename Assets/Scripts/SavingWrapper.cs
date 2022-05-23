using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

public class SavingWrapper : MonoBehaviour
{

    const string defaultSaveFile = "save";
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }
    }

    public void Load()
    {
        print("loaded healthpoints");
        GetComponent<SavingSystem>().Load(defaultSaveFile);
    }
    public void Save()
    {
        print("saved healthpoints");
        GetComponent<SavingSystem>().Save(defaultSaveFile);
    }
}
