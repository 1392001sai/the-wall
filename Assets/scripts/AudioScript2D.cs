using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript2D : MonoBehaviour
{
    public void StartAudio()
    {
        GetComponent<AudioSource>().Play();
    }
}
