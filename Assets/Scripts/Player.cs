using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private HealthDisplay healthDisplay;
    [SerializeField] private HealthSystem healthSystem;

    private void Start()
    {
        healthSystem.healthChanged += healthDisplay.SetHealthBar;
        healthSystem.ResetHealth();
    }
}
