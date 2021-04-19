using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur : MonoBehaviour
{
    [SerializeField] private HealthDisplay healthDisplay;
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private AIController minotaurController;
    [SerializeField] private MeleeAttack meleeAttack;

    private void Start()
    {
        healthSystem.healthChanged += healthDisplay.SetHealthBar;
        minotaurController.attack += meleeAttack.AttackCall;
        healthSystem.ResetHealth();
    }
}
