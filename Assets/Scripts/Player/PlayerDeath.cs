using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private GameObject[] activateGameObjects;
    [SerializeField] private GameObject[] deactivateGameObjects;

    public void PlayerDied()
    {
        foreach (var activateGameObject in activateGameObjects)
        {
            activateGameObject.SetActive(true);
        }
        foreach (var deactivateGameObject in deactivateGameObjects)
        {
            deactivateGameObject.SetActive(false);
        }
        GameManager.instance.playerDied = true;
    }
}
