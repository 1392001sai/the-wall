using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public Animator CameraAnim;
    public void CameraShake()
    {
        int ch = Random.Range(0, 3);
        if (ch == 0)
        {
            CameraAnim.SetTrigger("Shake1");

        }
        else if (ch == 1)
        {
            CameraAnim.SetTrigger("Shake2");
        }
        else if (ch == 3)
        {
            CameraAnim.SetTrigger("Shake3");
        }
    }
}
