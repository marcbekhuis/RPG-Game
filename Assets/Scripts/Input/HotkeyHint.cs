using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class HotkeyHint : MonoBehaviour
{
    private Text text;

    public void SetText(string textBeforeAction, string textAfterAction, InputActions action)
    {
        if (!text) text = GetComponent<Text>();
        text.text = textBeforeAction + InputManager.instance.inputSystemKeyboard.Keybinds[action].ToString() + textAfterAction;
    }
}
