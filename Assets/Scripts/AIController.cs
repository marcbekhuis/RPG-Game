using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public delegate void Attack(Vector2 direction, float attackdamage);
    public event Attack attack;

    [SerializeField] private float targetAcceptDistance = 0.1f;
    [SerializeField] private RangeDetection attackRange;
    [SerializeField] private RangeDetection targetRange;
    [SerializeField] private float attackCooldown = 1;

    private float cooldownTime = 0;
    private Movement movement;

    private void Start()
    {
        movement = GetComponent<Movement>();
    }

    private void Update()
    {
        Vector3 targetPosition = targetRange.DetectRangePosition(Vector2.up);
        if (targetPosition == null) return;
        if (attackRange.DetectRange(Vector2.up) && Time.timeSinceLevelLoad > cooldownTime)
        {
            attack(targetPosition - transform.position, 2);
            Debug.LogError("Attack");
            cooldownTime = Time.timeSinceLevelLoad + attackCooldown;
        }
        else if(Vector2.Distance(transform.position, targetPosition) > targetAcceptDistance)
        {
            movement.Move((targetPosition - transform.position).normalized);
        }
        else
        {
            movement.Move(Vector2.zero);
        }
    }
}
