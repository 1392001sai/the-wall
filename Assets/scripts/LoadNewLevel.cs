using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadNewLevel : MonoBehaviour
{

    Animator TransitionAnim;
    bool loading;
    float waittime;
    float startTime;
    public float time;
    public int loadScene;

    private void Start() 
    {
        startTime = 0;
        TransitionAnim=gameObject.GetComponent<Animator>();
        waittime=TransitionAnim.runtimeAnimatorController.animationClips[0].length;
        loading=false;
    }
    private void Update() 
    {
        startTime += Time.deltaTime;
        if(startTime>time)
        {
            if ( loading == false)
            {
                StartCoroutine(LoadLevel());
                loading = true;
            }

        }
        
    }

    public IEnumerator LoadLevel()
    {
        
        TransitionAnim.SetTrigger("Start");
        yield return new WaitForSeconds(waittime);
        SceneManager.LoadScene(loadScene);
    }
    
}
