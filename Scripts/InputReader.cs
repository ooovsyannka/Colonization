using UnityEngine;

public static class InputReader
{
    private const string ScrollWheel = "Mouse ScrollWheel";

    public static float ScrollDelta { get { return Input.GetAxis(ScrollWheel); } }
    public static bool IsRightMouseButton { get { return Input.GetMouseButton(1); } }
    public static Vector2 MousePosition { get { return Input.mousePosition; } }
}