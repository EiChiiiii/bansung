using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth; // HP toi da
    public int currentHealth; // HP hien tai

    public HealthBar healthBar;
    private float safeTime;
    public float safeTimeDuration = 0f;
    public Animator animator;
    public bool isDead = false;

    public GameManage gameManage;

    
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        currentHealth = maxHealth;

        if (healthBar != null)
            healthBar.UpdateHealth(currentHealth, maxHealth);
    }

    void Update()
    {
        if (safeTime > 0)
        {
            safeTime -= Time.deltaTime;
        }
    }

    public void addDamage(int damage)
    {
        if (damage <= 0)
        return;

        if (safeTime <= 0)
        {
            currentHealth -= damage;

        if(currentHealth <= 0)
        makeDead ();
        }

        if (healthBar != null)
                healthBar.UpdateHealth(currentHealth, maxHealth);

            safeTime = safeTimeDuration;
    }

    void makeDead()
    {
        isDead = true;
        animator.SetBool("Death", true);
        gameManage.GameOver();
    }
}   
