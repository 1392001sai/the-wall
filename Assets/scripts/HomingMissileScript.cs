using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Obsolete]
public class HomingMissileScript : MonoBehaviour
{
    GameObject target;
    public float MyTeam;
    bool activate = false;
    Vector2 TargetVelocity;
    public Rigidbody2D rb;
    public bool isServer;
    public float homingTime;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "missile")
        {
            if (collision.gameObject.tag == "Player" && isServer == true)
            {
                Debug.Log(1);
                if (transform.position.x > 0 && MyTeam == 0 || transform.position.x < 0 && MyTeam == 1)
                {
                    Debug.Log(2);
                    if (collision.gameObject.GetComponent<PlayerData>().team != MyTeam)
                    {
                        Debug.Log(3);
                        target = collision.gameObject;
                        activate = true;
                    }
                }
            }
        }
    }

    private void Update()
    {
        if (activate == true)
        {
            TargetVelocity = (target.transform.position - transform.position).normalized * rb.velocity.magnitude;
            rb.velocity = Vector3.Slerp(rb.velocity, TargetVelocity, homingTime);

        }
    }
}
