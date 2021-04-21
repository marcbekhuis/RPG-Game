using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDeath : MonoBehaviour
{
    [HideInInspector] public AIController aiController;

    public void AIDied()
    {
        aiController.disabled = true;
    }
}
