using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BattleTransition : MonoBehaviour
{
    [SerializeField] int sceneToLoad = -1;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public IEnumerator Transition()
    {
        DontDestroyOnLoad(gameObject);
        yield return SceneManager.LoadSceneAsync(sceneToLoad);
        print("scene loaded");
        Destroy(gameObject);
    }
}
