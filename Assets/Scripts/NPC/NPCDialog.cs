using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialog : MonoBehaviour
{
    [SerializeField] private Trading trading;
    [SerializeField] private Questing questing;

    public void OpenDialog()
    {
        NPCDialogDisplay.instance.DisplayDialog(trading, questing);
    }
}
