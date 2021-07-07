using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmScript : MonoBehaviour
{
    static BgmScript instance;
    public Animator anim;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void VolDec()
    {
        anim.SetTrigger("Dec");
    }

    public void VolInc()
    {
        anim.SetTrigger("Inc");
    }

 

}
