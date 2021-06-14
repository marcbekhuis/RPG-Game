using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool gameEnd = false;
    public bool GameLoading = true;
    public bool inventoryOpen = false;
    public bool consoleOpen = false;
    public bool gamePaused = false;
    public bool dialogOpen = false;

    private void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }

    public bool GamePaused()
    {
        return gameEnd || GameLoading || consoleOpen || gamePaused;
    }

    public bool PlayerMovementBlocked()
    {
        return inventoryOpen || dialogOpen;
    }
}
