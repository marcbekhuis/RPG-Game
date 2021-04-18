using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class InputSetting : MonoBehaviour
{
    [SerializeField] private InputManager.Actions action;
    [SerializeField] private Text actionText;

    private Button button;
    private Text keybindText;
    private bool listening = false;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => ActivateListening());
        keybindText = button.GetComponentInChildren<Text>();
        if(InputManager.instance.keybinds.ContainsKey(action)) keybindText.text = InputManager.instance.keybinds[action].ToString();
        if (actionText) actionText.text = action.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (listening) ListeningKeybind();
    }

    private void ListeningKeybind()
    {
        if (!Input.anyKeyDown) return;
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(kcode))
            {
                InputManager.instance.SetKeybind(action, kcode);
                keybindText.text = kcode.ToString();
                listening = false;
                button.interactable = true;
            }
        }
    }

    private void ActivateListening()
    {
        listening = true;
        keybindText.text = "Press a key";
        button.interactable = false;
    }
}
