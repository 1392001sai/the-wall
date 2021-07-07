using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Obsolete]
public class Parachute : NetworkBehaviour
{
    public Animator ParachuteAnim;
    public GameObject ParachutSprite;
    public GameObject powerUp;
    public GameObject dust;
    public Transform dustPointl;
    public Transform dustPointr;

    private void Start()
    {

        powerUp.GetComponent<PowerUp>().IsServer = isServer;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Projectile")
        {
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().isKinematic = true;
            powerUp.GetComponent<BoxCollider2D>().enabled = true;
            powerUp.GetComponent<Rigidbody2D>().isKinematic = false;     
            ParachuteAnim.enabled = false;
            ParachutSprite.SetActive(false);
            if (collision.gameObject.tag == "ground")
            {
                GameObject d1, d2;
                d1 = Instantiate(dust, dustPointl.position, dustPointl.rotation);
                d2 = Instantiate(dust, dustPointr.position, dustPointr.rotation);

            }

        }
    }
}
