using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaSystem : MonoBehaviour
{
    public delegate void StaminaChanged(float stamine, float maxStamina);
    public event StaminaChanged staminaChanged = delegate { };

    [SerializeField] [Min(0)] private float maxStamina = 10;
    [SerializeField] [Min(0)] private float staminaRegenIdle = 1;
    [SerializeField] [Min(0)] private float staminaRegenWalk = 0.5f;
    [SerializeField] [Min(0)] private float staminaConsume = 1;
    [SerializeField] [Min(0)] private float stamina = 0;

    public void ResetStamina()
    {
        stamina = maxStamina;
        staminaChanged(stamina, maxStamina);
    }

    public float GetStamina()
    {
        return stamina;
    }

    public bool HasStamina()
    {
        return stamina > 0;
    }

    public void SetStamina(float stamina)
    {
        this.stamina = Mathf.Clamp(stamina , 0, maxStamina);
        staminaChanged(this.stamina, maxStamina);
    }

    public void AddStaminaIdle()
    {
        stamina = Mathf.Clamp(stamina + staminaRegenIdle * Time.deltaTime, 0, maxStamina);
        staminaChanged(stamina, maxStamina);
    }

    public void AddStaminaWalk()
    {
        stamina = Mathf.Clamp(stamina + staminaRegenWalk * Time.deltaTime, 0, maxStamina);
        staminaChanged(stamina, maxStamina);
    }

    public void RemoveStamina()
    {
        stamina = Mathf.Clamp(stamina - staminaConsume * Time.deltaTime, 0, maxStamina);
        staminaChanged(stamina, maxStamina);
    }

    public void SetMaxStamina(float maxStamina)
    {
        this.stamina = maxStamina;
        staminaChanged(stamina, maxStamina);
    }
}
