using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllItems : MonoBehaviour
{
    public static AllItems instance;
    public ItemInformation[] items;

    void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }
}
