using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxHealth = 100;

    [Range(0f, 1f)]
    public float currentHealthPercent;
    public float currentHealth;
    private float lastCurrentHealth;
    private float lastCurrentHealthPercent;

    public HealthBar healthBar;

    public bool playerDead = false;


    private void Start()
    {
        SetHealth();
    }

    public void HealPlayer(float amount)
    {
        if(amount < 0)
        {
            Debug.Log("Invalid Heal Amount!");
            return;
        }
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {   
            currentHealth = maxHealth;
        }
        SetHealth();
    }

    public void DamagePlayer(float amount)
    {
        if (amount < 0)
        {
            Debug.Log("Invalid Damage Amount!");
            return;
        }
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            playerDead = true;
        }
        SetHealth();
    }

    public void SetHealth()
    {
        // Prevent division by zero
        if (maxHealth <= 0)
            return;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            playerDead = true;
        }

        // Check which value changed last and update the other
        if (currentHealth != lastCurrentHealth)
        {
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            currentHealthPercent = currentHealth / maxHealth;
        }
        // If currentHealthPercent was changed, update currentHealth
        else if (currentHealthPercent != lastCurrentHealthPercent)
        {
            currentHealthPercent = Mathf.Clamp01(currentHealthPercent);
            currentHealth = currentHealthPercent * maxHealth;
        }

        // Store the last values to track changes
        lastCurrentHealth = currentHealth;
        lastCurrentHealthPercent = currentHealthPercent;



        healthBar.SetHealth(currentHealthPercent);
    }

    void OnValidate()
    {
        if (!Application.isPlaying) return; // Prevent UI errors in Edit Mode
        SetHealth();
    }
}
