using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Obsolete]
public class SplashDamage : MonoBehaviour
{
    int team;
    public float damage;
    public string CollidedName;
    private void Start()
    {
        if (transform.position.x < 0)
        {
            team = 0;
        }
        else
        {
            team = 1;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.name != CollidedName)
        {
            if (collision.gameObject.GetComponent<PlayerData>().team != team)
            {
                collision.gameObject.GetComponent<shooting>().CmdReduceHealth(damage);
            }
        }
    }
}
