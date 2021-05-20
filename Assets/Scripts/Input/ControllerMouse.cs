using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Drawing;

public class ControllerMouse : MonoBehaviour
{
    private void Update()
    {
        MoveMouse();
    }

    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int X, int Y);
    [DllImport("user32.dll")]
    static extern bool GetCursorPos(ref Point lpPoint);

    private void MoveMouse()
    {
        Vector2 addedPosition = new Vector2(Input.GetAxis("Axis 4"), Input.GetAxis("Axis 5"));
        if (addedPosition.Equals(Vector2.zero)) return;
        Point lpPoint;
        GetCursorPos(ref lpPoint);
        Vector2 position = new Vector2(lpPoint.X, lpPoint.Y) + addedPosition;
        SetCursorPos((int)position.x, (int)position.y);
    }
}
