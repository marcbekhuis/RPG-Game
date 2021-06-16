using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildingDoorOut : MonoBehaviour
{
    [SerializeField] private string sceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Player") return;
        SceneManager.MoveGameObjectToScene(collision.gameObject, SceneManager.GetSceneByName("GameScene"));
        foreach (var gameObject in SceneManager.GetSceneByName("GameScene").GetRootGameObjects())
        {
            gameObject.SetActive(true);
        }
        SceneManager.UnloadSceneAsync(sceneName);
        LeaveRoomSpawnPoint.instance.SetSpawnPoint(collision.gameObject);
    }
}
