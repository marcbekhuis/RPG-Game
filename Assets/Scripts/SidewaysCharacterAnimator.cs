using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidewaysCharacterAnimator : MonoBehaviour
{
    [SerializeField] private string[] states = new string[] { "IDLE", "WALK", "RUN" };
    [SerializeField] private Animator[] animators;
    [SerializeField] private SpriteRenderer[] spriteRenderers;
    [SerializeField] private Rigidbody2D rigidbody;

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
        foreach (var spriteRenderer in spriteRenderers)
        {
            spriteRenderer.flipX = flipped;
        }
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
