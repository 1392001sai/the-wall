using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalRotation : MonoBehaviour
{
    
    public Vector3 TargetRotation;
    public float speed = 1f;


    private void Start()
    {
        TargetRotation = transform.rotation.eulerAngles;
    }


    private void Update()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(TargetRotation), Time.deltaTime * speed);

    }

}
