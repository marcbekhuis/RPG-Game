using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RandomSprite : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private SpriteRenderer[] spriteRenderers;

    // Start is called before the first frame update
    void Start()
    {
        Sprite sprite = sprites[Random.Range(0, sprites.Length)];
        foreach (var spriteRenderer in spriteRenderers)
        {
            spriteRenderer.sprite = sprite;
        }
    }
}
