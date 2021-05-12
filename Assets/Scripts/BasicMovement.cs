using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BasicMovement : MonoBehaviour, IMovement
{
    [SerializeField] [Min(0)] private float walkSpeed = 4;
    [SerializeField] [Min(0)] private float sprintSpeed = 6;

    private Rigidbody2D rigidbody;
    private StaminaSystem staminaSystem;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        staminaSystem = GetComponent<StaminaSystem>();
    }

    public void Move(Vector2 direction, bool sprint)
    {
        if (sprint && staminaSystem.HasStamina())
        {
            rigidbody.velocity = direction * sprintSpeed;
            if(direction != Vector2.zero) staminaSystem.RemoveStamina();
        }
        else
        {
            rigidbody.velocity = direction * walkSpeed;
            if(direction == Vector2.zero) staminaSystem.AddStaminaIdle();
            else staminaSystem.AddStaminaWalk();
        }

    }

    public void Frozen(bool active)
    {
        if (active) rigidbody.bodyType = RigidbodyType2D.Static;
        else rigidbody.bodyType = RigidbodyType2D.Dynamic;
    }
}
