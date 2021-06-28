using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIParent : MonoBehaviour
{
    [SerializeField] private StatBarDisplay healthDisplay;
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private AIController minotaurController;
    [SerializeField] private IAttack attack;
    [SerializeField] private AIDeath aiDeath;
    [SerializeField] private StaminaSystem staminaSystem;
    [SerializeField] private SidewaysCharacterAnimator sidewaysCharacterAnimator;

    private void Start()
    {
        attack = GetComponent<IAttack>();
        healthSystem.healthChanged += healthDisplay.SetStatBar;
        minotaurController.attack += attack.AttackCall;
        healthSystem.ResetHealth();
        aiDeath.aiController = minotaurController;
        healthSystem.onDied += aiDeath.AIDied;
        healthSystem.onDied += healthDisplay.HideStatBar;
        if(sidewaysCharacterAnimator) healthSystem.onDied += sidewaysCharacterAnimator.DiedAnimation;
        staminaSystem.ResetStamina();
    }
}
