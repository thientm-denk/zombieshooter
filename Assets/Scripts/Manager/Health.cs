using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;

    public int currentHealth;

    public bool isNpc = false;


    void Start()
    {
        OnInit();
    }
    public void OnInit()
    {
        currentHealth = maxHealth;
    }
    public void OnInit(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = maxHealth;
    }
    public void GotHit(int damage)
    {
        if(currentHealth < damage)
        {
            currentHealth = 0;
        }
        else
        {
            currentHealth -= damage;
        }  
    }


}
