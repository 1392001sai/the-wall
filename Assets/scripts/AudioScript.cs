using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public float MaxVolume = 1f;
    public float MaxDistance = 20f;
    public Transform LocalPlayer;
    AudioSource audioSource;



    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (LocalPlayer != null)
        {
            audioSource.volume = MaxVolume * (MaxDistance - Vector2.Distance(transform.position, LocalPlayer.position)) / MaxDistance;
        }
    }

    public void StartAudio()
    {
        
        audioSource.Play();
    }
}
