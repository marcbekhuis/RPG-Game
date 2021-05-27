using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private StatBarDisplay healthDisplay;
    [SerializeField] public HealthSystem healthSystem;
    [SerializeField] private PlayerDeath playerDeath;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private StatBarDisplay staminaDisplay;
    [SerializeField] private StaminaSystem staminaSystem;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private CooldownDisplay attackCooldownDisplay;
    [SerializeField] private CooldownDisplay movementCooldownDisplay;

    private void Start()
    {
        healthSystem.healthChanged += healthDisplay.SetStatBar;
        healthSystem.ResetHealth();
        healthSystem.onDied += playerDeath.PlayerDied;
        staminaSystem.staminaChanged += staminaDisplay.SetStatBarFinish;
        staminaSystem.ResetStamina();
        playerController.toggleInventory += playerInventory.ToggleInventory;
        playerController.OnAttackCooldown += attackCooldownDisplay.SetCooldown;
        playerController.OnMovementCooldown += movementCooldownDisplay.SetCooldown;
    }
}
