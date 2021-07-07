using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Obsolete]

public class MissileProp : NetworkBehaviour
{
    public float StunTime;
    float team;
    public float damage;
    public GameObject[] desObjects;
    //public GameObject explosion180;
    public GameObject explosion360;
    public GameObject ExplosionPoint;
    GameObject exp;
    //bool firstCol;
    bool firstShield;
    bool myshield;
    public float destroyTime;
    public Transform LocalPlayer;
    public GameObject[] Sounds;
    GameObject sound;

    private void Start()
    {
        //firstCol = true;
        firstShield = true;
        myshield = false;
        if (transform.position.x < 0)
        {
            team = 0;
        }
        else
        {
            team = 1;
        }
        
    }

    public void StartSounds()
    {
        sound = Instantiate(Sounds[0], transform);
        sound.GetComponent<AudioScript>().LocalPlayer = LocalPlayer;
        sound = Instantiate(Sounds[1], transform);
        sound.GetComponent<AudioScript>().LocalPlayer = LocalPlayer;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag != "missile" && collision.gameObject.name != "HomingMissile" && collision.gameObject.tag != "Boundary")
        {
            if (collision.gameObject.tag == "Player")
            {
                if (collision.gameObject.GetComponent<PlayerData>().team != team)
                {
                    if (isServer == true)
                    {
                        Debug.Log("reduce health on server");
                        //collision.gameObject.GetComponent<PlayerData>().IsStunned = true;
                        //collision.gameObject.GetComponent<shooting>().UnstunCaller(StunTime);
                        collision.gameObject.GetComponent<shooting>().CmdReduceHealth(damage);
                    }
                    Debug.Log("1");
                    //collision.gameObject.GetComponent<shooting>().Stunned();
                    explosion(collision.gameObject.name);

                    //Debug.Log(1);
                }
                //firstCol = false;
            }
            else if (collision.gameObject.tag == "StunShield")
            {
                
                if (collision.transform.position.x < 0 && team == 0 || collision.transform.position.x > 0 && team == 1)
                {
                    myshield = true;
                }
                if (myshield == true && firstShield == true)
                {
                    firstShield = false;
                }
                else
                {
                    collision.gameObject.GetComponent<StunShieldScipt>().OnHit(damage);
                    explosion(collision.gameObject.name);
                    //Debug.Log(1);
                }
            }
            else if (collision.gameObject.tag == "Parachute" || collision.gameObject.tag == "PowerUp")
            {
                Destroy(collision.gameObject);
                explosion(collision.gameObject.name);
            }
            else
            {
                //Debug.Log(collision.gameObject.tag);
                //Debug.Log(3);
                explosion(collision.gameObject.name);
            }
        }
        
        
    }

    void explosion(string CollidedName)
    {
        sound = Instantiate(Sounds[2], transform.position, Quaternion.identity);
        sound.GetComponent<AudioScript>().LocalPlayer = LocalPlayer;
        exp = Instantiate(explosion360, ExplosionPoint.transform.position, Quaternion.identity);
        exp.GetComponent<SplashDamage>().CollidedName = CollidedName;
        //NetworkServer.Spawn(exp);
        Destroy(gameObject);
    }

    
}
