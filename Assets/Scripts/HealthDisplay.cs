using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private RectTransform HealthIndicator;

    private RectTransform parentTransform;

    private void Awake()
    {
        parentTransform = HealthIndicator.parent.GetComponent<RectTransform>();
    }

    public void SetHealthBar(float health, float maxHealth)
    {
        float width = parentTransform.sizeDelta.x / maxHealth * health;
        HealthIndicator.sizeDelta = new Vector2(width, parentTransform.sizeDelta.y);
    }
}
