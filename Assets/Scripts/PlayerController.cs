using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Movement movement;

    private void Start()
    {
        movement = GetComponent<Movement>();
    }

    private void Update()
    {
        movement.Move(MovementInput());
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
