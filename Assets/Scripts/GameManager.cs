using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool playerDied = false;
    public bool GameLoading = true;
    public bool inventoryOpen = false;
    public bool consoleOpen = false;

    private void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }
}
