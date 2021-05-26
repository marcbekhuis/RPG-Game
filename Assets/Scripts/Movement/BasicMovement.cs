using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BasicMovement : MonoBehaviour, IMovement
{
    [SerializeField] [Min(0)] private float walkSpeed = 4;
    [SerializeField] [Min(0)] private float sprintSpeed = 6;
    [SerializeField] private BiomeSpeed[] biomeSpeeds;

    private Rigidbody2D rigidbody;
    private StaminaSystem staminaSystem;

    [System.Serializable]
    public class BiomeSpeed 
    {
        public Biome biome;
        public float speedMultiplier = 1;
    }


    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        staminaSystem = GetComponent<StaminaSystem>();
    }

    public void Move(Vector2 direction, bool sprint)
    {
        if (sprint && staminaSystem.HasStamina())
        {
            rigidbody.velocity = direction * sprintSpeed * SpeedMultiplier();
            if (direction != Vector2.zero) staminaSystem.RemoveStamina();
        }
        else
        {
            rigidbody.velocity = direction * walkSpeed * SpeedMultiplier();
            if (direction == Vector2.zero) staminaSystem.AddStaminaIdle();
            else staminaSystem.AddStaminaWalk();
        }
    }

    public float SpeedMultiplier()
    {
        if (GameManager.instance.GamePaused()) return 1;
        Vector2Int gridPosition = Vector2Int.RoundToInt((Vector2)transform.position - Vector2.one * 0.5f);
        Biome biome = MapData.instance.landscapeTiles[gridPosition.x, gridPosition.y].biome;
        foreach (var biomeSpeed in biomeSpeeds)
        {
            if(biomeSpeed.biome == biome)
            {
                return biomeSpeed.speedMultiplier;
            }
        }
        return 1;
    }

    public void Frozen(bool active)
    {
        if (active) rigidbody.bodyType = RigidbodyType2D.Static;
        else rigidbody.bodyType = RigidbodyType2D.Dynamic;
    }
}
