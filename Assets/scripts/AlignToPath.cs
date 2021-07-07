using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Obsolete]
public class AlignToPath : NetworkBehaviour
{
    Rigidbody2D rb;
    public bool StartAlign = false;
    public HomingMissileScript homingMissileScript;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        homingMissileScript.rb = rb;
        homingMissileScript.isServer = isServer;
        
    }

    void Update()
    {
        if (StartAlign == true)
        {
            transform.rotation = Quaternion.FromToRotation(Vector3.right, rb.velocity);
        }
    }

 
}
