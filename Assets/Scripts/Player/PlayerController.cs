using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public delegate void Attack(Vector2 direction, float cooldown);
    public event Attack attack = delegate { };

    public delegate void Consume();
    public event Consume consume = delegate { };

    public delegate void ToggleInventory(Inventory sideInventory);
    public event ToggleInventory toggleInventory = delegate { };

    public delegate void OnCooldown(float endTime);
    public event OnCooldown OnAttackCooldown = delegate { };
    public event OnCooldown OnMovementCooldown = delegate { };

    [SerializeField] [Min(0)] private float attackCooldown = 1;
    [SerializeField] [Min(0)] private float movementCooldown = 1;
    [SerializeField] private OpenStorage openStorage;
    [SerializeField] private NPCTalk npcTalk;
    [SerializeField] private CharacterAnimator characterAnimator;

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
        if (GameManager.instance.GamePaused())
        {
            movement.Move(Vector2.zero);
            characterAnimator.SetState("IDLE");
            return;
        }
        if (!GameManager.instance.PlayerMovementBlocked())
        {
            if (InputManager.instance.GetKeyDown(InputActions.UseTool))
            {
                if (Time.timeSinceLevelLoad > attackCooldownTime)
                {
                    if (attack.GetInvocationList().Length > 1)
                    {
                        attack(camera.ScreenToWorldPoint(Input.mousePosition), attackCooldown);
                        SetCooldownTimes();
                    }
                    else if (consume.GetInvocationList().Length > 1)
                    {
                        consume();
                        SetCooldownTimes();
                    }
                }
            }
            if (Time.timeSinceLevelLoad > movementCooldownTime)
            {
                movement.Move(MovementInput(), InputManager.instance.GetKey(InputActions.Sprint));
                if (MovementInput() == Vector2.zero) characterAnimator.SetState("IDLE");
                else if (InputManager.instance.GetKey(InputActions.Sprint)) characterAnimator.SetState("RUN");
                else characterAnimator.SetState("WALK");
            }
            else
            {
                movement.Move(Vector2.zero);
                characterAnimator.SetState("IDLE");
            }
        }
        else
        {
            movement.Move(Vector2.zero);
            characterAnimator.SetState("IDLE");
        }
        Interaction();
    }

    private void Interaction()
    {
        if (InputManager.instance.GetKeyDown(InputActions.OpenInventory))
        {
            NPCDialog npcDialog = npcTalk.DialogInRange();
            if (GameManager.instance.dialogOpen) return;
            else if (npcDialog)
            {
                npcDialog.OpenDialog();
                return;
            }
            toggleInventory(openStorage.StorageInRange());
        }
    }

    private Vector2 MovementInput()
    {
        float vertical = InputManager.instance.GetAxis(InputAxis.Vertical);
        float horizontal = InputManager.instance.GetAxis(InputAxis.Horizontal);

        return new Vector2(horizontal, vertical).normalized;
    }

    public void SetCooldowns(float attackCooldown, float movementCooldown)
    {
        this.attackCooldown = attackCooldown;
        this.movementCooldown = movementCooldown;
    }

    public void SetCooldownTimes()
    {
        attackCooldownTime = Time.timeSinceLevelLoad + attackCooldown;
        movementCooldownTime = Time.timeSinceLevelLoad + movementCooldown;
        OnAttackCooldown(attackCooldownTime);
        OnMovementCooldown(movementCooldownTime);
    }
}
