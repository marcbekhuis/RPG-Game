using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class InputSetting : MonoBehaviour
{
    //[serializefield] private inputactions action;
    //[serializefield] private text actiontext;

    //private button button;
    //private text keybindtext;
    //private bool listening = false;

    //private void start()
    //{
    //    button = getcomponent<button>();
    //    button.onclick.addlistener(() => activatelistening());
    //    keybindtext = button.getcomponentinchildren<text>();
    //    if(inputmanager.instance.keybinds.containskey(action)) keybindtext.text = inputmanager.instance.keybinds[action].tostring();
    //    if (actiontext) actiontext.text = action.tostring();
    //}

    //// update is called once per frame
    //void update()
    //{
    //    if (listening) listeningkeybind();
    //}

    //private void listeningkeybind()
    //{
    //    if (!input.anykeydown) return;
    //    foreach (keycode kcode in enum.getvalues(typeof(keycode)))
    //    {
    //        if (input.getkey(kcode))
    //        {
    //            inputmanager.instance.setkeybind(action, kcode);
    //            keybindtext.text = kcode.tostring();
    //            listening = false;
    //            button.interactable = true;
    //        }
    //    }
    //}

    //private void activatelistening()
    //{
    //    listening = true;
    //    keybindtext.text = "press a key";
    //    button.interactable = false;
    //}
}
