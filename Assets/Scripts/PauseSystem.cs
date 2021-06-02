using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject[] closePanels;

    private void Update()
    {
        if (InputManager.instance.GetKeyDown(InputActions.Pause)) if(ClosePanels()) TogglePause(!pausePanel.activeSelf);
    }

    public void TogglePause(bool status)
    {
        pausePanel.SetActive(status);
        GameManager.instance.gamePaused = status;
    }

    public bool ClosePanels()
    {
        bool ClosedNoPanels = true;
        GameManager.instance.consoleOpen = false;
        GameManager.instance.inventoryOpen = false;
        foreach (var closePanel in closePanels)
        {
            if (closePanel.activeSelf) ClosedNoPanels = false;
            closePanel.SetActive(false);
        }
        return ClosedNoPanels;
    }
}
