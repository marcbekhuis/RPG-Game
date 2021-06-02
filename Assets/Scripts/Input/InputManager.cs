using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public IInputSystem inputSystemKeyboard = new InputSystemKeyboard();
    public IInputSystem inputSystemController = new InputSystemController();

    public static InputManager instance;
    public IInputSystem lastUsedInputSystem;

    public Dictionary<InputActions, KeyCode> Keybinds
    {
        get
        {
            if (lastUsedInputSystem != null) return lastUsedInputSystem.Keybinds;
            else return inputSystemKeyboard.Keybinds;
        }
    }

    private void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }

    public float GetAxis(InputAxis axis)
    {
        float controllerAxis = inputSystemController.GetAxis(axis);
        float keyboardAxis = inputSystemKeyboard.GetAxis(axis);

        if (Mathf.Abs(controllerAxis) > Mathf.Abs(keyboardAxis))
        {
            lastUsedInputSystem = inputSystemController;
            return controllerAxis;
        }
        else
        {
            lastUsedInputSystem = inputSystemKeyboard;
            return keyboardAxis;
        }
    }

    public bool GetKey(InputActions action)
    {
        bool output = false;
        if (inputSystemController.Keybinds.ContainsKey(action))
            if (Input.GetKey(inputSystemController.Keybinds[action])) {
                lastUsedInputSystem = inputSystemController;
                output = true;
            }
        if (Input.GetKey(inputSystemKeyboard.Keybinds[action]))
        {
            lastUsedInputSystem = inputSystemKeyboard;
            output = true;
        }
        return output;
    }

    public bool GetKeyDown(InputActions action)
    {
        bool output = false;
        if (inputSystemController.Keybinds.ContainsKey(action)) 
            if (Input.GetKeyDown(inputSystemController.Keybinds[action])) {
                lastUsedInputSystem = inputSystemController;
                output = true;
            }
        if (Input.GetKeyDown(inputSystemKeyboard.Keybinds[action]))
        {
            lastUsedInputSystem = inputSystemKeyboard;
            output = true;
        }
        return output;
    }

    public bool GetKeyUp(InputActions action)
    {
        bool output = false;
        if (inputSystemController.Keybinds.ContainsKey(action))
            if (Input.GetKeyUp(inputSystemController.Keybinds[action]))
            {
                lastUsedInputSystem = inputSystemController;
                output = true;
            }
        if (Input.GetKeyUp(inputSystemKeyboard.Keybinds[action]))
        {
            lastUsedInputSystem = inputSystemKeyboard;
            output = true;
        }
        return output;
    }
}
