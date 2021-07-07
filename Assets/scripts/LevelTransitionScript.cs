using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

[System.Obsolete]
public class LevelTransitionScript : MonoBehaviour
{
    public int ToLevel;
    public float TimeGap;

    IEnumerator Transition()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(ToLevel);
    }

    public void CallTransition()
    {
        StartCoroutine(Transition());
    }
}
