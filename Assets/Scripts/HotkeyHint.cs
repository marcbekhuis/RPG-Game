using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class HotkeyHint : MonoBehaviour
{
    private Text text;

    public void SetText(string textBeforeHotkey, string textAfterHotkey, InputManager.Actions hotkey)
    {
        if (!text) text = GetComponent<Text>();
        text.text = textBeforeHotkey + InputManager.instance.keybinds[hotkey].ToString() + textAfterHotkey;
    }
}
