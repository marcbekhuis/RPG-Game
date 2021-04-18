using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class FireLight : MonoBehaviour
{
    [SerializeField] private AnimationCurve outerRadius;
    [SerializeField] private AnimationCurve brightness;
    private Light2D light;

    private void Start()
    {
        light = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float animationTimeRadius = Time.timeSinceLevelLoad - outerRadius.keys[outerRadius.length - 1].time * Mathf.Floor(Time.timeSinceLevelLoad / outerRadius.keys[outerRadius.length - 1].time);
        float animationTimeBrightness = Time.timeSinceLevelLoad - brightness.keys[brightness.length - 1].time * Mathf.Floor(Time.timeSinceLevelLoad / brightness.keys[brightness.length - 1].time);

        light.pointLightOuterRadius = outerRadius.Evaluate(animationTimeRadius);
        light.intensity = brightness.Evaluate(animationTimeRadius);
    }
}
