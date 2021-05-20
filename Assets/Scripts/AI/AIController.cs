using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public delegate void Attack(Vector2 direction);
    public event Attack attack;
    public bool disabled = false;

    [SerializeField] [Min(0)] private float targetAcceptDistance = 0.1f;
    [SerializeField] private RangeDetection attackRange;
    [SerializeField] private RangeDetection targetRange;
    [SerializeField] [Min(0)] private float attackCooldown = 1;
    [SerializeField] [Min(0)] private float movementCooldown = 1;
    [SerializeField] [Range(0, 1)] private float runProcentDistance = 0.5f;

    private float attackCooldownTime = 0;
    private float movementCooldownTime = 0;
    private IMovement movement;

    private void Start()
    {
        movement = GetComponent<IMovement>();
    }

    private void Update()
    {
        if (disabled)
        {
            movement.Move(Vector2.zero);
            movement.Frozen(true);
            return;
        }
        if (GameManager.instance.GameLoading || GameManager.instance.playerDied || GameManager.instance.consoleOpen)
        {
            movement.Move(Vector2.zero);
            return;
        }
        Vector3 targetPosition = targetRange.DetectRangePosition();
        if (targetPosition == transform.position)
        {
            movement.Move(Vector2.zero);
            return;
        }
        if (attackRange.DetectRange() && Time.timeSinceLevelLoad > attackCooldownTime)
        {
            attack(targetPosition);
            attackCooldownTime = Time.timeSinceLevelLoad + attackCooldown;
            movementCooldownTime = Time.timeSinceLevelLoad + movementCooldown;
        }
        else if(Vector2.Distance(transform.position, targetPosition) > targetAcceptDistance && Time.timeSinceLevelLoad > movementCooldownTime)
        {
            if(Vector2.Distance(transform.position, targetPosition) > targetRange.GetRadius() * runProcentDistance) movement.Move((targetPosition - transform.position).normalized, true);
            else movement.Move((targetPosition - transform.position).normalized);
        }
        else
        {
            movement.Move(Vector2.zero);
        }
    }
}
