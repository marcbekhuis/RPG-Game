using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownDisplay : MonoBehaviour
{
    [SerializeField] private Image cooldownImage;
    [SerializeField] private Text cooldownText;

    private float startTime = 0;
    private float endTime = 0;
    private float duration = 0;

    private void Start()
    {
        cooldownImage.gameObject.SetActive(false);
        cooldownText.gameObject.SetActive(false);
    }

    public void SetCooldown(float endTime)
    {
        startTime = Time.timeSinceLevelLoad;
        this.endTime = endTime;
        duration = endTime - startTime;
        StartCoroutine(Display());
    }

    private IEnumerator Display()
    {
        cooldownImage.gameObject.SetActive(true);
        cooldownText.gameObject.SetActive(true);
        for (float i = 0; i < duration; i += Time.deltaTime)
        {
            float timeRemaining = endTime - Time.timeSinceLevelLoad;
            float procent = timeRemaining / duration;
            cooldownText.text = timeRemaining.ToString("F1");
            cooldownImage.color = new Color(0, 0, 0, procent);
            cooldownImage.fillAmount = procent;
            yield return null;
        }
        cooldownImage.gameObject.SetActive(false);
        cooldownText.gameObject.SetActive(false);
    }
}
