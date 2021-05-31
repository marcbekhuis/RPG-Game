using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    private void Update()
    {
        if (InputManager.instance.GetKeyDown(InputActions.Pause)) TogglePause(!pausePanel.activeSelf);
    }

    public void TogglePause(bool status)
    {
        pausePanel.SetActive(status);
        GameManager.instance.gamePaused = status;
    }
}
