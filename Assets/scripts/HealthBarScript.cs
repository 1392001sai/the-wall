using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Image HealthBar;
    public Image BlinkerBar;
    float MaxHealth;
    public float CurHealth;
    public Animator Blinker;

    public void SetMaxHealth(float MaxHp)
    {
        MaxHealth = MaxHp;
        CurHealth = MaxHp;
    }
    
    public void UpdateHealth()
    {
        //CurHealth = health;
        Debug.Log(CurHealth / MaxHealth);
        HealthBar.fillAmount = CurHealth / MaxHealth;
        Blinker.SetTrigger("Blink");
    }

    public  void Heal(float healAmount)
    {
        CurHealth += healAmount;
        if (CurHealth > MaxHealth)
        {
            CurHealth = MaxHealth;
        }
        //Debug.Log(CurHealth / MaxHealth);
        HealthBar.fillAmount = CurHealth / MaxHealth;
        UpdateBlinker();
    }

    void UpdateBlinker()
    {
        BlinkerBar.fillAmount = CurHealth / MaxHealth;
    }
}
