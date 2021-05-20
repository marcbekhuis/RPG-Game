using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCodeDetector : MonoBehaviour
{
    public void Update()
    {
        if (!Input.anyKeyDown) return;
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(kcode))
            {
                Debug.LogError(kcode);
            }
        }
    }
}
