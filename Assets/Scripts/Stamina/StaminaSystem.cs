using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LandscapeTileDetector))]
public class StaminaSystem : MonoBehaviour
{
    public delegate void StaminaChanged(float stamine, float maxStamina);
    public event StaminaChanged staminaChanged = delegate { };

    [SerializeField] [Min(0)] private float maxStamina = 10;
    [SerializeField] [Min(0)] private float staminaRegenIdle = 1;
    [SerializeField] [Min(0)] private float staminaRegenWalk = 0.5f;
    [SerializeField] [Min(0)] private float staminaConsume = 1;
    [SerializeField] [Min(0)] private float stamina = 0;
    [SerializeField] private BiomeMultiplier[] biomeRegenMultiplier;
    [SerializeField] private BiomeMultiplier[] biomeUsageMultiplier;

    private LandscapeTileDetector landscapeTileDetector;

    private void Start()
    {
        landscapeTileDetector = GetComponent<LandscapeTileDetector>();
    }

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
        stamina = Mathf.Clamp(stamina + staminaRegenIdle * Time.deltaTime * StaminaMultiplier(biomeRegenMultiplier), 0, maxStamina);
        staminaChanged(stamina, maxStamina);
    }

    public void AddStaminaWalk()
    {
        stamina = Mathf.Clamp(stamina + staminaRegenWalk * Time.deltaTime * StaminaMultiplier(biomeRegenMultiplier), 0, maxStamina); ;
        staminaChanged(stamina, maxStamina);
    }

    public void RemoveStamina()
    {
        stamina = Mathf.Clamp(stamina - staminaConsume * Time.deltaTime * StaminaMultiplier(biomeRegenMultiplier), 0, maxStamina);
        staminaChanged(stamina, maxStamina);
    }

    public void SetMaxStamina(float maxStamina)
    {
        this.stamina = maxStamina;
        staminaChanged(stamina, maxStamina);
    }

    public float StaminaMultiplier(BiomeMultiplier[] biomeMultipliers)
    {
        if (GameManager.instance.GameLoading) return 1;
        Biome biome = landscapeTileDetector.FindBiome();
        foreach (var biomeMultiplier in biomeMultipliers)
        {
            if (biomeMultiplier.biome == biome)
            {
                return biomeMultiplier.multiplier;
            }
        }
        return 1;
    }
}
