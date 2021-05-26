using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryCaller : MonoBehaviour
{
    public void Victory()
    {
        GameVictory.instance.Victory();
    }
}