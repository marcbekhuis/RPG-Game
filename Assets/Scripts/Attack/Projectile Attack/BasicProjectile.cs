using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class BasicProjectile : MonoBehaviour, IProjectile
{
    [SerializeField][Min(0)] private float damage = 1;
    [SerializeField] [Min(0)] private float speed = 1;

    private GameObject owner;
    private Vector2 direction;
    private Rigidbody2D rigidbody;

    public void Setup(GameObject owner, Vector2 direction, float angle)
    {
        this.owner = owner;
        this.direction = direction;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        rigidbody.velocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == owner)
            return;
        if (collision.TryGetComponent<HealthSystem>(out HealthSystem healthSystem))
            healthSystem.RemoveHealth(damage);
        Destroy(this.gameObject);
    }
}
