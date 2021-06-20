using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapDisplay : MonoBehaviour
{
    [SerializeField] private GameObject display;

    // Update is called once per frame
    void Update()
    {
        if (InputManager.instance.GetKeyDown(InputActions.Minimap)) display.SetActive(!display.activeSelf);
    }
}
