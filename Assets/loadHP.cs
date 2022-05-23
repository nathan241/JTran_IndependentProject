using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

public class loadHP : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
        
        wrapper.Load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
