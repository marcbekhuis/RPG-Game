using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildingDoorIn : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private Vector3 spawnDistanceFromDoor = new Vector3(0, 1, 0);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Player") return;
        LeaveRoomSpawnPoint.instance.spawnpoint = this.transform.position - spawnDistanceFromDoor;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        SceneManager.MoveGameObjectToScene(collision.gameObject, SceneManager.GetSceneByName(sceneName));
        foreach (var gameObject in SceneManager.GetSceneByName("GameScene").GetRootGameObjects())
        {
            if (gameObject.name.Contains("Canvas") || gameObject.name.Contains("GameManager") || gameObject.name.Contains("EventSystem")) continue;
            gameObject.SetActive(false);
        }
    }
}
