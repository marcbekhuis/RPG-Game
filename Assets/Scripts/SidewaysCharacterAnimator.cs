using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidewaysCharacterAnimator : MonoBehaviour
{
    [SerializeField] private string[] states = new string[] { "IDLE", "WALK", "RUN" };
    [SerializeField] private Animator[] animators;
    [SerializeField] private SpriteRenderer[] spriteRenderers;
    [SerializeField] private Rigidbody2D rigidbody;

    private string activeState = "";
    private bool activeDirection = false;

    private void Start()
    {
        activeDirection = spriteRenderers[0];
    }

    private void Update()
    {
        CalculateDirection();
    }

    public void CalculateDirection()
    {
        if (rigidbody.velocity.x > 0) SetDirection(false);
        else if (rigidbody.velocity.x < 0) SetDirection(true);
    }

    public void SetDirection(bool flipped)
    {
        if (activeDirection == flipped) return;
        activeDirection = flipped;
        foreach (var spriteRenderer in spriteRenderers)
        {
            spriteRenderer.flipX = flipped;
        }
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
        activeState = "DIED";
        foreach (var animator in animators)
        {
            animator.SetTrigger("DIED");
        }
    }
}
