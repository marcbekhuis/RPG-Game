using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatBarDisplay : MonoBehaviour
{
    [SerializeField] private float statBarDelay = 1;
    [SerializeField] private float statBarDuration = 1;
    [SerializeField] private RectTransform statBarIndicator;

    private RectTransform parentTransform;
    private RectTransform whiteTransform;

    private Coroutine DelayedstatBarEffect;
    private float lastStat;
    private float maxStat;

    private void Awake()
    {
        parentTransform = statBarIndicator.parent.GetComponent<RectTransform>();
        whiteTransform = parentTransform.GetChild(0).GetComponent<RectTransform>();
    }

    public void SetStatBar(float health, float maxHealth)
    {
        lastStat = health;
        this.maxStat = maxHealth;
        float width = parentTransform.rect.width / maxHealth * health;
        statBarIndicator.sizeDelta = new Vector2(width, parentTransform.rect.height);
        if (DelayedstatBarEffect == null) DelayedstatBarEffect = StartCoroutine(StatDelayEffect());
        else
        {
            StopCoroutine(DelayedstatBarEffect);
            DelayedstatBarEffect = StartCoroutine(StatDelayEffect());
        }
    }

    public void SetStatBarFinish(float health, float maxHealth)
    {
        lastStat = health;
        this.maxStat = maxHealth;
        float width = parentTransform.rect.width / maxHealth * health;
        statBarIndicator.sizeDelta = new Vector2(width, parentTransform.rect.height);
        if (DelayedstatBarEffect == null) DelayedstatBarEffect = StartCoroutine(StatDelayEffect());
    }

    private IEnumerator StatDelayEffect()
    {
        if(statBarDelay > 0) yield return new WaitForSeconds(statBarDelay);
        float width = parentTransform.rect.width / maxStat * lastStat;
        float oldWidth = whiteTransform.rect.width;
        for (float i = 0; i < statBarDuration; i += Time.deltaTime)
        {
            float animatedWidth = Mathf.Lerp(oldWidth, width, 1 / statBarDuration * i);
            whiteTransform.sizeDelta = new Vector2(animatedWidth, parentTransform.rect.height);
            yield return null;
        }
        whiteTransform.sizeDelta = new Vector2(width, parentTransform.rect.height);
        DelayedstatBarEffect = null;
    }

    public void HideStatBar()
    {
        parentTransform.gameObject.SetActive(false);
    }
}
