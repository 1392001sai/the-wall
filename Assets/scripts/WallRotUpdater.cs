using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRotUpdater : MonoBehaviour
{
    public WalRotation walRotation;
    public float rotfactor;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "missile")
        {
            if (gameObject.name == "walll")
            {
                walRotation.TargetRotation = transform.parent.transform.rotation.eulerAngles;
                walRotation.TargetRotation.z -= rotfactor;
            }
            if (gameObject.name == "wallr")
            {
                walRotation.TargetRotation = transform.parent.transform.rotation.eulerAngles;
                walRotation.TargetRotation.z += rotfactor;
            }

        }

    }
}
