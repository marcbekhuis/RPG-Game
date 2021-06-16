using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveRoomSpawnPoint : Singleton<LeaveRoomSpawnPoint>
{
    public Vector3 spawnpoint;

    public void SetSpawnPoint(GameObject gameObject)
    {
        gameObject.transform.position = spawnpoint;
    }
}
