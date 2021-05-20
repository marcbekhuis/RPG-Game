using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public delegate void Died();
    public delegate void HealthChanged(float health, float maxHealth);
    public event HealthChanged healthChanged = delegate { };
    public event Died died = delegate { };
    [SerializeField] [Min(0)] private float maxHealth = 10;
    [SerializeField] [Min(0)] private float health = 0;

    public void ResetHealth()
    {
        health = maxHealth;
        healthChanged(health, maxHealth);
        if (health <= 0) died();
    }

    public void SetHealth(float health)
    {
        this.health = Mathf.Clamp(health, 0, maxHealth);
        healthChanged(this.health, maxHealth);
        if (this.health <= 0) died();
    }

    public void AddHealth(float addedHealth)
    {
        health = Mathf.Clamp(health + addedHealth, 0, maxHealth);
        healthChanged(health, maxHealth);
        if (health <= 0) died();
    }

    public void RemoveHealth(float removedHealth)
    {
        health = Mathf.Clamp(health - removedHealth, 0, maxHealth);
        healthChanged(health, maxHealth);
        if (health <= 0) died();
    }

    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
        healthChanged(health, maxHealth);
    }
}
