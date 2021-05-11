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
        { Actions.Sprint, KeyCode.LeftShift},
        { Actions.UseTool, KeyCode.Mouse0},
        { Actions.DropItem, KeyCode.Q},
        { Actions.OpenConsole, KeyCode.F1},
        { Actions.OpenInventory, KeyCode.E},
        { Actions.HotbarSlot1, KeyCode.Alpha1},
        { Actions.HotbarSlot2, KeyCode.Alpha2},
        { Actions.HotbarSlot3, KeyCode.Alpha3},
        { Actions.HotbarSlot4, KeyCode.Alpha4},
        { Actions.HotbarSlot5, KeyCode.Alpha5},
        { Actions.HotbarSlot6, KeyCode.Alpha6},
        { Actions.HotbarSlot7, KeyCode.Alpha7},
        { Actions.HotbarSlot8, KeyCode.Alpha8},
        { Actions.HotbarSlot9, KeyCode.Alpha9},
    };

    public enum Actions
    {
        MoveForward,
        MoveBackward,
        MoveLeft,
        MoveRight,
        Sprint,
        UseTool,
        DropItem,
        OpenConsole,
        OpenInventory,
        HotbarSlot1,
        HotbarSlot2,
        HotbarSlot3,
        HotbarSlot4,
        HotbarSlot5,
        HotbarSlot6,
        HotbarSlot7,
        HotbarSlot8,
        HotbarSlot9,
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
