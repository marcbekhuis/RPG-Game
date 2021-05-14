using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIDeath : MonoBehaviour
{
    [HideInInspector] public AIController aiController;
    [SerializeField] private LayerMask layerMaskOnDead;
    [SerializeField] private UnityEvent OnDead = new UnityEvent();

    public void AIDied()
    {
        aiController.disabled = true;
        OnDead.Invoke();
        gameObject.layer = (int)Mathf.Log(layerMaskOnDead.value, 2); ;
    }
}
