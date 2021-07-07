using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeScript : MonoBehaviour
{
    public GameObject smoke;
    GameObject SmokeInstance;
    private void Start()
    {
        SmokeInstance = Instantiate(smoke, transform.position, transform.rotation);
    }

    private void Update()
    {
        if (transform != null && SmokeInstance != null)
        {
            SmokeInstance.transform.position = transform.position;
            SmokeInstance.transform.rotation = transform.rotation;
        }
    }
}
