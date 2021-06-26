using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private string[] states = new string[] { "IDLE", "WALK", "RUN"};
    [SerializeField] private Animator[] animators;
    [SerializeField] private Rigidbody2D rigidbody;

    private string activeDirection = "";
    private string activeState = "";

    private void Update()
    {
        CalculateDirection();
    }

    public void CalculateDirection()
    {
        if (rigidbody.velocity.y > 0) SetDirection("UP");
        else if (rigidbody.velocity.y < 0) SetDirection("DOWN");
        else if (rigidbody.velocity.x > 0) SetDirection("RIGHT");
        else if (rigidbody.velocity.x < 0) SetDirection("LEFT");
    }

    public void SetDirection(string currentDirection) 
    {
        if (activeDirection == currentDirection) return;
        foreach (var animator in animators)
        {
            animator.SetTrigger(currentDirection);
        }
        activeDirection = currentDirection;
    }

    public void SetState(string currentState)
    {
        if (activeState == currentState) return;
        activeState = currentState;
        foreach (var state in states)
        {
            if (state == currentState) continue;
            foreach (var animator in animators)
            {
                animator.SetBool(state, false);
            }
        }

        foreach (var animator in animators)
        {
            animator.SetBool(currentState, true);

        }
    }

    public void DiedAnimation()
    {
        foreach (var animator in animators)
        {
            animator.SetTrigger("DIED");
        }
    }
}
