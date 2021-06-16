using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIDeath : MonoBehaviour
{
    [HideInInspector] public AIController aiController;
    [SerializeField] private LayerMask layerMaskOnDead;
    [SerializeField] private float decayTime = 60;
    [SerializeField] public UnityEvent OnDead = new UnityEvent();
    [SerializeField] private SpriteRenderer[] spriteRenderers;

    private bool dead = false;
    private float timeOfDead = 0;
    private float decayStart = 0;
    private float decayedTime = 0;

    public void AIDied()
    {
        aiController.disabled = true;
        OnDead.Invoke();
        gameObject.layer = (int)Mathf.Log(layerMaskOnDead.value, 2);
        dead = true;
        timeOfDead = Time.timeSinceLevelLoad;
        decayStart = Time.timeSinceLevelLoad + decayTime * 0.75f;
        decayedTime = Time.timeSinceLevelLoad + decayTime;
    }

    private void Update()
    {
        if (!dead) return;
        if (Time.timeSinceLevelLoad >= decayStart)
        {
            float opacity = (decayedTime - Time.timeSinceLevelLoad) / (decayTime * 0.75f);
            foreach (var spriteRenderer in spriteRenderers)
            {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, opacity);
            }
        }
        if (Time.timeSinceLevelLoad >= decayedTime)
        {
            DayNightCycle.instance.RemoveShadow(transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>());
            Destroy(gameObject);
        }
    }
}
