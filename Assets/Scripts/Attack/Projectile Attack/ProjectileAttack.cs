using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour, IAttack
{
    [SerializeField] [Range(0, 360)] private float arc = 90;
    [SerializeField] [Min(1)] private int numberProjectiles = 1;

    private GameObject owner;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileBase;

    private void Start()
    {
        owner = this.gameObject;
    }

    public void SetSettings(float arc, int numberProjectiles, GameObject projectilePrefab)
    {
        this.arc = arc;
        this.numberProjectiles = numberProjectiles;
        this.projectilePrefab = projectilePrefab;
    }

    public void AttackCall(Vector2 mousePosition)
    {
        float distanceBetweenProjectiles = arc / (numberProjectiles + 1);
        float halfArc = arc / 2f;
        Vector2 direction = mousePosition - (Vector2)projectileBase.position;
        float centerAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float startAngle = centerAngle - halfArc;

        Debug.DrawLine(projectileBase.position, mousePosition, Color.blue, 1);

        for (int i = 0; i < numberProjectiles; i++)
        {
            IProjectile projectile = Instantiate(projectilePrefab, projectileBase.position, new Quaternion(0,0,0,0)).GetComponent<IProjectile>();

            float angle = startAngle + distanceBetweenProjectiles + (distanceBetweenProjectiles * i);
            float rad = angle * Mathf.Deg2Rad;
            Vector2 projectileDirection = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

            Debug.DrawLine(projectileBase.position, (Vector2)projectileBase.position + projectileDirection, Color.green, 1);

            projectile.Setup(owner, projectileDirection, angle);
        }
    }
}
