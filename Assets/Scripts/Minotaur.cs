using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur : MonoBehaviour
{
    [SerializeField] private StatBarDisplay healthDisplay;
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private AIController minotaurController;
    [SerializeField] private MeleeAttack meleeAttack;
    [SerializeField] private AIDeath aiDeath;
    [SerializeField] private StaminaSystem staminaSystem;

    private void Start()
    {
        healthSystem.healthChanged += healthDisplay.SetStatBar;
        minotaurController.attack += meleeAttack.AttackCall;
        healthSystem.ResetHealth();
        aiDeath.aiController = minotaurController;
        healthSystem.died += aiDeath.AIDied;
        healthSystem.died += healthDisplay.HideStatBar;
        staminaSystem.ResetStamina();
    }
}
