using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private StatBarDisplay healthDisplay;
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private PlayerDeath playerDeath;
    [SerializeField] private MeleeAttack meleeAttack;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private StatBarDisplay staminaDisplay;
    [SerializeField] private StaminaSystem staminaSystem;
    [SerializeField] private PlayerInventory playerInventory;

    private void Start()
    {
        healthSystem.healthChanged += healthDisplay.SetStatBar;
        healthSystem.ResetHealth();
        healthSystem.died += playerDeath.PlayerDied;
        playerController.attack += meleeAttack.AttackCall;
        staminaSystem.staminaChanged += staminaDisplay.SetStatBarFinish;
        staminaSystem.ResetStamina();
        playerController.toggleInventory += playerInventory.ToggleInventory;
    }
}
