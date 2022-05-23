using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BattleTransition : MonoBehaviour
{
    [SerializeField] int sceneToLoad = -1;
    [SerializeField] float fadeOutTime = 2f;
    [SerializeField] float fadeInTime = 1f;
    [SerializeField] float fadeWaitTime = 3f;
    SavingWrapper wrapper;
    // Start is called before the first frame update
    void Start()
    {
        wrapper = FindObjectOfType<SavingWrapper>();
    }
    void Update()
    {
        
    }

    public IEnumerator Transition()
    {
        
        DontDestroyOnLoad(gameObject);
        

        wrapper.Save();



        yield return SceneManager.LoadSceneAsync(sceneToLoad);

        

        Destroy(gameObject);
    }
    public IEnumerator TransitionToBattle()
    {

        DontDestroyOnLoad(gameObject);

        Fader fader = FindObjectOfType<Fader>();

        yield return fader.FadeOut(fadeOutTime);

        

        wrapper.Save();


        yield return SceneManager.LoadSceneAsync(sceneToLoad);

        

        yield return new WaitForSeconds(fadeWaitTime);
        yield return fader.FadeIn(fadeInTime);

        Destroy(gameObject);
    }
}
