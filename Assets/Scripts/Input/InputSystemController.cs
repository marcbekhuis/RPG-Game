using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystemController : IInputSystem
{
    private Dictionary<InputActions, KeyCode> keybinds = new Dictionary<InputActions, KeyCode>
    {
        { InputActions.Sprint, KeyCode.JoystickButton8},
        { InputActions.UseTool, KeyCode.JoystickButton5},
        { InputActions.DropItem, KeyCode.Q},
        { InputActions.OpenConsole, KeyCode.F1},
        { InputActions.OpenInventory, KeyCode.JoystickButton0},
        { InputActions.HotbarSlot1, KeyCode.Alpha1},
        { InputActions.HotbarSlot2, KeyCode.Alpha2},
        { InputActions.HotbarSlot3, KeyCode.Alpha3},
        { InputActions.HotbarSlot4, KeyCode.Alpha4},
        { InputActions.HotbarSlot5, KeyCode.Alpha5},
        { InputActions.HotbarSlot6, KeyCode.Alpha6},
        { InputActions.HotbarSlot7, KeyCode.Alpha7},
        { InputActions.HotbarSlot8, KeyCode.Alpha8},
        { InputActions.HotbarSlot9, KeyCode.Alpha9},
        { InputActions.MoveItem, KeyCode.JoystickButton9},
    };

    public Dictionary<InputActions, KeyCode> Keybinds => keybinds;

    public float GetAxis(InputAxis axis)
    {
        switch (axis)
        {
            case InputAxis.Horizontal:
                {
                    if (keybinds.ContainsKey(InputActions.MoveLeft) && keybinds.ContainsKey(InputActions.MoveRight))
                    {
                        float output = 0;
                        if (GetKey(InputActions.MoveLeft)) output -= 1;
                        if (GetKey(InputActions.MoveRight)) output += 1;
                        return output;
                    }
                    else return Input.GetAxis(axis.ToString());
                }
            case InputAxis.Vertical:
                {
                    if (keybinds.ContainsKey(InputActions.MoveBackward) && keybinds.ContainsKey(InputActions.MoveForward))
                    {
                        float output = 0;
                        if (GetKey(InputActions.MoveBackward)) output -= 1;
                        if (GetKey(InputActions.MoveForward)) output += 1;
                        return output;
                    }
                    else return Input.GetAxis(axis.ToString());
                }
            default:
                return 0;
        }
    }

    public bool GetKey(InputActions action)
    {
        return Input.GetKey(keybinds[action]);
    }

    public bool GetKeyDown(InputActions action)
    {
        return Input.GetKeyDown(keybinds[action]);
    }

    public bool GetKeyUp(InputActions action)
    {
        return Input.GetKeyUp(keybinds[action]);
    }

    public void SetKeybind(InputActions action, KeyCode keyCode)
    {
        if (keybinds.ContainsKey(action)) keybinds[action] = keyCode;
        else keybinds.Add(action, keyCode);
    }
}
