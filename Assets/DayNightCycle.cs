using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Tilemaps;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField][Range(0,24)] private float timeOfDay = 12;
    [SerializeField][Tooltip("Time in minutes")][Range(0.05f,60)] private float dayNightCylceDuration = 1;
    [SerializeField] private bool stopTime = false;
    [Space]
    [SerializeField] private AnimationCurve shadowPositionX;
    [SerializeField] private AnimationCurve shadowPositionY;
    [SerializeField] private AnimationCurve shadowOpacity;
    [Space]
    [SerializeField] private AnimationCurve lightBrightness;
    [Space]
    [SerializeField] private Light2D light;
    [SerializeField] private Tilemap[] shadowsTilemaps;
    [SerializeField] private SpriteRenderer[] shadowsSpriteRenderers;

    // Update is called once per frame
    void Update()
    {
        if (!stopTime) SetTime();
        TimeAppearance();
    }

    private void SetTime()
    {
        timeOfDay += Time.deltaTime / (dayNightCylceDuration * 60f);
        if (timeOfDay > 24) timeOfDay -= 24;
    }

    private void TimeAppearance()
    {
        foreach (var shadow in shadowsTilemaps)
        {
            shadow.transform.localPosition = new Vector2(shadowPositionX.Evaluate(timeOfDay), shadowPositionY.Evaluate(timeOfDay));
            shadow.color = new Color(0, 0, 0, shadowOpacity.Evaluate(timeOfDay));
        }
        foreach (var shadow in shadowsSpriteRenderers)
        {
            shadow.transform.localPosition = new Vector2(shadowPositionX.Evaluate(timeOfDay), shadowPositionY.Evaluate(timeOfDay));
            shadow.color = new Color(0, 0, 0, shadowOpacity.Evaluate(timeOfDay));
        }

        light.intensity = lightBrightness.Evaluate(timeOfDay);
    }
}
