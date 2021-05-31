using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookDirection : MonoBehaviour
{
    [SerializeField] private GameObject indicator;

    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        indicator.transform.LookAt(camera.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);
    }
}
