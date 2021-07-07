using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileHTPScript : MonoBehaviour
{
    Rigidbody2D rb;
    public float Speed;
    public GameObject smoke;
    GameObject SmokeInstance;
    public Transform SmokePoint;
    public float SmokeScaleFactor;

    void Start()
    {
        SmokeInstance = Instantiate(smoke, SmokePoint.position, SmokePoint.rotation);
        SmokeInstance.transform.localScale *= SmokeScaleFactor;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * Speed;

    }
    private void Update()
    {
        transform.rotation = Quaternion.FromToRotation(Vector3.right, rb.velocity);
        if (SmokeInstance != null && SmokePoint != null)
        {
            SmokeInstance.transform.position = SmokePoint.position;
            SmokeInstance.transform.rotation = SmokePoint.rotation;
        }
    }
    
}
