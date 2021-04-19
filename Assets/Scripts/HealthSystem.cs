using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public delegate void HealthChanged(float health, float maxHealth);
    public event HealthChanged healthChanged;
    [SerializeField] private float maxHealth = 10;
    [SerializeField] private float health = 0;

    public void ResetHealth()
    {
        health = maxHealth;
        healthChanged(health, maxHealth);
    }

    public void SetHealth(float health)
    {
        this.health = health;
        healthChanged(this.health, maxHealth);
    }

    public void AddHealth(float addedHealth)
    {
        health += addedHealth;
        healthChanged(health, maxHealth);
    }

    public void RemoveHealth(float removedHealth)
    {
        health -= removedHealth;
        healthChanged(health, maxHealth);
    }

    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
    }
}
