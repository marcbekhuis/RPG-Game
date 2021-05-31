using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LandscapeTileDetector))]
[RequireComponent(typeof(Rigidbody2D))]
public class BasicMovement : MonoBehaviour, IMovement
{
    [SerializeField] [Min(0)] private float walkSpeed = 4;
    [SerializeField] [Min(0)] private float sprintSpeed = 6;
    [SerializeField] private BiomeMultiplier[] biomeSpeeds;

    private Rigidbody2D rigidbody;
    private StaminaSystem staminaSystem;
    private LandscapeTileDetector landscapeTileDetector;


    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        staminaSystem = GetComponent<StaminaSystem>();
        landscapeTileDetector = GetComponent<LandscapeTileDetector>();
    }

    public void Move(Vector2 direction, bool sprint)
    {
        if (sprint && staminaSystem.HasStamina())
        {
            rigidbody.velocity = direction * sprintSpeed * SpeedMultiplier();
            if (direction != Vector2.zero) staminaSystem.RemoveStamina();
        }
        else
        {
            rigidbody.velocity = direction * walkSpeed * SpeedMultiplier();
            if (direction == Vector2.zero) staminaSystem.AddStaminaIdle();
            else staminaSystem.AddStaminaWalk();
        }
    }

    public float SpeedMultiplier()
    {
        if (GameManager.instance.GamePaused()) return 1;
        foreach (var biomeMulitplier in biomeSpeeds)
        {
            if(biomeMulitplier.biome == landscapeTileDetector.biome)
            {
                return biomeMulitplier.multiplier;
            }
        }
        return 1;
    }

    public void Frozen(bool active)
    {
        if (active) rigidbody.bodyType = RigidbodyType2D.Static;
        else rigidbody.bodyType = RigidbodyType2D.Dynamic;
    }
}
