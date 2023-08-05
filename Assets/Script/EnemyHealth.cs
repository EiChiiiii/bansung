using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth; // HP toi da
    public int currentHealth; // HP hien tai
    
    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        
    }

    public void addDamage(int damage)
    {
        if (damage <= 0)
        return;

        currentHealth -= damage;

        if(currentHealth <= 0)
        makeDead ();
    }

    void makeDead()
    {
        Destroy (gameObject);
    }
}
