using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _borderScreen;
    [SerializeField] private float _zoomSpeed;
    [SerializeField] private Camera _camera;

    private Vector2 _screenCenter;
    private Vector2 _lastMousePosition;
    private Vector3 _moveDirection;
    private float _screenWidthCenter;
    private float _screenHeightCenter;

    private void Awake()
    {
        int dividingBorderScreen = 2;

        _screenWidthCenter = Screen.width / dividingBorderScreen;
        _screenHeightCenter = Screen.height / dividingBorderScreen;

        _screenCenter = new Vector2(_screenWidthCenter, _screenHeightCenter);
    }

    private void Update()
    {  
        if (InputReader.IsRightMouseButton)
        {
            Rotation();

            return;
        } 
        
        Move();
        Zoom();
    }

    private void Rotation()
    {
        Vector2 mouseDelta = InputReader.MousePosition - _lastMousePosition;

        transform.Rotate(Vector3.up * MathF.Sign( mouseDelta.x) * _rotationSpeed * Time.deltaTime, Space.World);

        _lastMousePosition = Input.mousePosition;
    }

    private void Move()
    {
        float horizontalDistance = InputReader.MousePosition.x - _screenCenter.x;
        float verticalDistance = InputReader.MousePosition.y - _screenCenter.y;

        _moveDirection.x = Mathf.Abs(horizontalDistance) >= _screenWidthCenter - _borderScreen ? Mathf.Sign(horizontalDistance) : 0;
        _moveDirection.z = Mathf.Abs(verticalDistance) >= _screenHeightCenter - _borderScreen ? Mathf.Sign(verticalDistance) : 0;

        transform.Translate(_moveDirection * _speed * Time.deltaTime, Space.Self);
    }

    private void Zoom()
    {
       _camera.transform.Translate(Vector3.forward * InputReader.ScrollDelta * _zoomSpeed);
    }
}
