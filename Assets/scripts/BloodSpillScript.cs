using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSpillScript : MonoBehaviour
{
    public float FadeWait;
    IEnumerator Waittime()
    {
        yield return new WaitForSeconds(FadeWait);
        GetComponent<Animator>().SetTrigger("FadeOut");
    }

    void DestroyObj()
    {
        Destroy(gameObject);
    }
}
