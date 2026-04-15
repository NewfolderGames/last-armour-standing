namespace LastArmourStanding.Scripts.Entities.Unit.Controller;

using Godot;
using System;

public partial class PlayerUnitCameraController : CameraController
{
    [ExportCategory("States")]
    [Export] private float _cameraDistance = 10f;
    [Export] private float _cameraDistanceFrom = 10f;
    [Export] private float _cameraDistanceTo = 10f;
    [Export] private float _cameraDistanceMin = 5f;
    [Export] private float _cameraDistanceMax = 100f;
    
    [ExportCategory("Input")]
    [Export] private bool _enableMouseLook;
    [Export] private bool _enableMouseZoom;
    [Export] private float _mouseLookSensitivity;
    [Export] private float _mouseZoomSize;
    
    [ExportCategory("Input States")]
    [Export] private bool _isMouseLookTransitioning;
    [Export] private bool _isMouseZoomTransitioning;
    [Export] private float _mouseZoomTransitionTime;
    [Export] private float _mouseZoomTransitionTimeRemaining;

    public override void _Process(double delta)
    {
        if (_enableMouseZoom && _isMouseZoomTransitioning) ProcessZoomTransition(delta);
    }

    public override void _Input(InputEvent @event)
    {
        if (_enableMouseLook) HandleMouseLook(@event);
        if (_enableMouseZoom) HandleMouseZoom(@event);
    }

    private void ProcessZoomTransition(double delta)
    {
        _mouseZoomTransitionTimeRemaining = MathF.Max(0, _mouseZoomTransitionTimeRemaining - (float)delta);
        _cameraDistance = Mathf.Lerp(_cameraDistanceFrom, _cameraDistanceTo, 1 - Mathf.Pow(_mouseZoomTransitionTimeRemaining / _mouseZoomTransitionTime, 2));
        CameraAnchor.SetPosition(Vector3.Up * _cameraDistance);
        if (_mouseZoomTransitionTimeRemaining <= 0)
        {
            _isMouseZoomTransitioning = false;
        }
    }
    
    private void HandleMouseLook(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseButton)
        {
            _isMouseLookTransitioning = mouseButton.ButtonIndex == MouseButton.Right && mouseButton.Pressed;
        }
        if (_isMouseLookTransitioning && @event is InputEventMouseMotion mouseMotion)
        {
            CameraAnchorRoot.RotationDegrees = new Vector3(
                Mathf.Clamp(CameraAnchorRoot.RotationDegrees.X + _mouseLookSensitivity * mouseMotion.ScreenRelative.Y, -60, 0),
                CameraAnchorRoot.RotationDegrees.Y - _mouseLookSensitivity * mouseMotion.ScreenRelative.X,
                0);
        }
    }
    
    private void HandleMouseZoom(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseButton)
        {
            if (mouseButton.ButtonIndex == MouseButton.WheelUp && mouseButton.Pressed && _cameraDistance > _cameraDistanceMin)
            {
                _isMouseZoomTransitioning = true;
                _mouseZoomTransitionTimeRemaining = _mouseZoomTransitionTime;
                _cameraDistanceTo = Mathf.Max(_cameraDistanceMin, _cameraDistance - _mouseZoomSize);
                _cameraDistanceFrom = _cameraDistance;
            }
            if (mouseButton.ButtonIndex == MouseButton.WheelDown && mouseButton.Pressed && _cameraDistance < _cameraDistanceMax)
            {
                _isMouseZoomTransitioning = true;
                _mouseZoomTransitionTimeRemaining = _mouseZoomTransitionTime;
                _cameraDistanceTo = Mathf.Min(_cameraDistanceMax, _cameraDistance + _mouseZoomSize);
                _cameraDistanceFrom = _cameraDistance;
            }
        }
    }
    
}
