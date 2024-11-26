using UnityEngine;

public class InputReader
{
    private const string ScrollWheel = "Mouse ScrollWheel";

    public float ScrollDelta { get { return Input.GetAxis(ScrollWheel); } }
    public bool IsRightMouseButton { get { return Input.GetMouseButton(1); } }
    public Vector2 MousePosition { get { return Input.mousePosition; } }
}