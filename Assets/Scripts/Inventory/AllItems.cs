using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllItems : MonoBehaviour
{
    public static AllItems instance;
    public Item[] items;

    void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }
}
