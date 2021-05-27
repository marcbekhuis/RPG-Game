using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public delegate void OnDied();
    public delegate void HealthChanged(float health, float maxHealth);
    public event HealthChanged healthChanged = delegate { };
    public event OnDied onDied = delegate { };
    [SerializeField] [Min(0)] private float maxHealth = 10;
    [SerializeField] [Min(0)] private float health = 0;
    bool dead = false;

    public void ResetHealth()
    {
        if (dead) return;
        health = maxHealth;
        healthChanged(health, maxHealth);
        if (health <= 0) Died();
    }

    public void SetHealth(float health)
    {
        if (dead) return;
        this.health = Mathf.Clamp(health, 0, maxHealth);
        healthChanged(this.health, maxHealth);
        if (this.health <= 0) Died();
    }

    public void AddHealth(float addedHealth)
    {
        if (dead) return;
        health = Mathf.Clamp(health + addedHealth, 0, maxHealth);
        healthChanged(health, maxHealth);
        if (health <= 0) Died();
    }

    public void RemoveHealth(float removedHealth)
    {
        if (dead) return;
        health = Mathf.Clamp(health - removedHealth, 0, maxHealth);
        healthChanged(health, maxHealth);
        if (health <= 0) Died();
    }

    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
        healthChanged(health, maxHealth);
    }

    public void Died()
    {
        dead = true;
        onDied();
    }
}
