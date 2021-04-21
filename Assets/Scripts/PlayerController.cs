using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public delegate void Attack(Vector2 direction);
    public event Attack attack = delegate { };

    [SerializeField] [Min(0)] private float attackCooldown = 1;
    [SerializeField] [Min(0)] private float movementCooldown = 1;

    private IMovement movement;
    new private Camera camera;

    private float attackCooldownTime = 0;
    private float movementCooldownTime = 0;

    private void Start()
    {
        movement = GetComponent<IMovement>();
        camera = Camera.main;
    }

    private void Update()
    {
        if (GameManager.instance.GameLoading || GameManager.instance.playerDied) return;
        if (InputManager.instance.GetKeyDown(InputManager.Actions.UseTool) && Time.timeSinceLevelLoad > attackCooldownTime)
        {
            attack(camera.ViewportToWorldPoint(Input.mousePosition));
            attackCooldownTime = Time.timeSinceLevelLoad + attackCooldown;
            movementCooldownTime = Time.timeSinceLevelLoad + movementCooldown;
        }
        else if(Time.timeSinceLevelLoad > movementCooldownTime) movement.Move(MovementInput(), InputManager.instance.GetKey(InputManager.Actions.Sprint));
    }

    private Vector2 MovementInput()
    {
        float vertical = 0;
        float horizontal = 0;
        if (InputManager.instance.GetKey(InputManager.Actions.MoveForward)) vertical += 1;
        if (InputManager.instance.GetKey(InputManager.Actions.MoveBackward)) vertical -= 1;
        if (InputManager.instance.GetKey(InputManager.Actions.MoveRight)) horizontal += 1;
        if (InputManager.instance.GetKey(InputManager.Actions.MoveLeft)) horizontal -= 1;

        return new Vector2(horizontal, vertical).normalized;
    }
}
