using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool playerDied = false;
    public bool GameLoading = true;

    private void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }
}
