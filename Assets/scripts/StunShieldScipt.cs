using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Obsolete]
public class StunShieldScipt : MonoBehaviour
{
    public float health;
    public Animator DamageIndicator;
    public shooting PlayerConnection;
    public bool IsServer;
    public void OnHit(float damage)
    {
        
        health -= damage;
        if (health <= 0)
        {
            GetComponent<Animator>().SetTrigger("Exit");
        }
        else
        {
            Debug.Log("shield hit");
            DamageIndicator.SetTrigger("IsHit");
        }
    }

    void Exit()
    {
        if (IsServer == true)
        {
            PlayerConnection.ShieldActive = false;
        }
        Debug.Log(1);
        Destroy(gameObject);
    }


}
