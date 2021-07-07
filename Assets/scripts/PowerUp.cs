using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Obsolete]
public class PowerUp : MonoBehaviour
{
    [HideInInspector]
    public int PowerUpIndex;
    public GameObject[] PowerUps;
    public bool IsServer;

    [Header("Stun Shield")]

    public float StunSheildTime;

    [Header("Heal")]

    public float healAmount;



    public void SetIndex(int index)
    {
        PowerUpIndex = index;
        PowerUps[PowerUpIndex].SetActive(true);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (IsServer == true)
            {
                if (PowerUpIndex == 0)
                {
                    collision.gameObject.GetComponent<shooting>().ApplyStunShield(StunSheildTime);

                }
            }
            if (PowerUpIndex == 0)
            {
                GetComponent<Animator>().SetTrigger("CollectedShield");
            }
            else if (PowerUpIndex == 1)
            {
                collision.gameObject.GetComponent<shooting>().ApplyHealthPowerUp(healAmount);
                GetComponent<Animator>().SetTrigger("CollectedHeal");
            }

        }
    }

    void Collected()
    {
        Destroy(transform.parent.gameObject);
    }
}
