using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputSystem
{
    Dictionary<InputActions, KeyCode> Keybinds { get; }

    void SetKeybind(InputActions action, KeyCode keyCode);

    bool GetKeyDown(InputActions action);

    bool GetKey(InputActions action);

    bool GetKeyUp(InputActions action);

    float GetAxis(InputAxis axis);
}
