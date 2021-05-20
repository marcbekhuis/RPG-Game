using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public delegate void Attack(Vector2 direction);
    public event Attack attack = delegate { };

    public delegate void ToggleInventory(Inventory sideInventory);
    public event ToggleInventory toggleInventory = delegate { };

    [SerializeField] [Min(0)] private float attackCooldown = 1;
    [SerializeField] [Min(0)] private float movementCooldown = 1;
    [SerializeField] private OpenStorage openStorage;

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
        if (GameManager.instance.GameLoading || GameManager.instance.playerDied || GameManager.instance.consoleOpen)
        {
            movement.Move(Vector2.zero);
            return;
        }
        if (!GameManager.instance.inventoryOpen)
        {
            if (InputManager.instance.GetKeyDown(InputActions.UseTool) && Time.timeSinceLevelLoad > attackCooldownTime)
            {
                attack(camera.ScreenToWorldPoint(Input.mousePosition));
                attackCooldownTime = Time.timeSinceLevelLoad + attackCooldown;
                movementCooldownTime = Time.timeSinceLevelLoad + movementCooldown;
            }
            if (Time.timeSinceLevelLoad > movementCooldownTime) movement.Move(MovementInput(), InputManager.instance.GetKey(InputActions.Sprint));
        }
        else
        {
            movement.Move(Vector2.zero);
        }
        if (InputManager.instance.GetKeyDown(InputActions.OpenInventory))
        {
            toggleInventory(openStorage.StorageInRange());
        }
    }

    private Vector2 MovementInput()
    {
        float vertical = InputManager.instance.GetAxis(InputAxis.Vertical);
        float horizontal = InputManager.instance.GetAxis(InputAxis.Horizontal);

        return new Vector2(horizontal, vertical).normalized;
    }
}
