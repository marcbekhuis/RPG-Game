using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVictory : Singleton<GameVictory>
{
    [SerializeField] private GameObject victoryPanel;

    public void Victory()
    {
        GameManager.instance.gameEnd = true;
        victoryPanel.SetActive(true);
    }
}
