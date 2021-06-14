using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialog : MonoBehaviour
{
    [SerializeField] private Trading trading;

    public void OpenDialog()
    {
        NPCDialogDisplay.instance.DisplayDialog(trading);
    }
}
