using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private string[] directions = new string[] { "UP", "DOWN", "RIGHT", "LEFT"};
    [SerializeField] private string[] states = new string[] { "IDLE", "WALK", "RUN"};
    [SerializeField] private Animator[] animators;
    [SerializeField] private Rigidbody2D rigidbody;

    private Dictionary<string, bool> animationDirections = new Dictionary<string, bool>();

    private void Start()
    {
        foreach (var direction in directions)
        {
            animationDirections.Add(direction, false);
        }
    }

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
        foreach (var direction in directions)
        {
            if (direction != currentDirection) animationDirections[direction] = false; 
        }

        if (animationDirections[currentDirection]) return;
        foreach (var animator in animators)
        {
            animator.SetTrigger(currentDirection);
        }
        animationDirections[currentDirection] = true;
    }

    public void SetState(string currentState)
    {
        foreach (var state in states)
        {
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
