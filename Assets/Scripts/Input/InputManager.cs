using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public IInputSystem inputSystemKeyboard = new InputSystemKeyboard();
    public IInputSystem inputSystemController = new InputSystemController();

    public static InputManager instance;

    private void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }

    public float GetAxis(InputAxis axis)
    {
        float controllerAxis = inputSystemController.GetAxis(axis);
        float keyboardAxis = inputSystemKeyboard.GetAxis(axis);

        if (Mathf.Abs(controllerAxis) > Mathf.Abs(keyboardAxis)) return controllerAxis;
        else return keyboardAxis;
    }

    public bool GetKey(InputActions action)
    {
        bool output = false;
        if(inputSystemController.Keybinds.ContainsKey(action)) 
            if (Input.GetKey(inputSystemController.Keybinds[action])) output = true;
        if (Input.GetKey(inputSystemKeyboard.Keybinds[action])) output = true;
        return output;
    }

    public bool GetKeyDown(InputActions action)
    {
        bool output = false;
        if (inputSystemController.Keybinds.ContainsKey(action)) 
            if (Input.GetKeyDown(inputSystemController.Keybinds[action])) output = true;
        if (Input.GetKeyDown(inputSystemKeyboard.Keybinds[action])) output = true;
        return output;
    }

    public bool GetKeyUp(InputActions action)
    {
        bool output = false;
        if (inputSystemController.Keybinds.ContainsKey(action))
            if (Input.GetKeyUp(inputSystemController.Keybinds[action])) output = true;
        if (Input.GetKeyUp(inputSystemKeyboard.Keybinds[action])) output = true;
        return output;
    }
}
