using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public Dictionary<Actions, KeyCode> keybinds = new Dictionary<Actions, KeyCode>
    {
        { Actions.MoveForward, KeyCode.W},
        { Actions.MoveBackward, KeyCode.S},
        { Actions.MoveLeft, KeyCode.A},
        { Actions.MoveRight, KeyCode.D},
        { Actions.UseTool, KeyCode.Mouse0},
        { Actions.DropItem, KeyCode.Q},
    };

    public enum Actions
    {
        MoveForward,
        MoveBackward,
        MoveLeft,
        MoveRight,
        UseTool,
        DropItem,
    }

    private void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }

    public void SetKeybind(Actions action, KeyCode keyCode)
    {
        if (keybinds.ContainsKey(action)) keybinds[action] = keyCode;
        else keybinds.Add(action, keyCode);
    }

    public bool GetKeyDown(Actions action)
    {
        return Input.GetKeyDown(keybinds[action]);
    }

    public bool GetKey(Actions action)
    {
        return Input.GetKey(keybinds[action]);
    }

    public bool GetKeyUp(Actions action)
    {
        return Input.GetKeyUp(keybinds[action]);
    }
}
