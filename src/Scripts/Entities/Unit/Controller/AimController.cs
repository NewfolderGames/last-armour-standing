using System;
using Godot;
using LastArmourStanding.Game.Contexts.Gameplay;

namespace LastArmourStanding.Scripts.Entities.Unit.Controller;

public partial class AimController : Node3D, IGameplayContextConsumer
{
    private GameplayContext _gameplayContext;

    [ExportCategory("Designation")]
    [Export] private bool _enableDesignation;
    [Export] private Node3D _designationTargetNode;
    [ExportSubgroup("Screen to World")]
    [Export] private bool _useScreenToWorldDesignation;
    [Export(PropertyHint.Layers3DPhysics)] private uint _screenToWorldCollisionMask = 1;
    [Export] private bool _pushScreenToWorldDesignationToUi;

    [ExportCategory("Aim Rotation")]
    [Export] private Node3D _aimRotationRootNode;
    [Export] private Node3D _aimRotationNode;
    [Export] private float _aimRotationSpeed = 0.125f;
    [Export] private float _aimRotationTarget;

    [ExportCategory("Aim Point")]
    [Export] private Node3D _aimPointRootNode;
    [Export] private Node3D _aimPointNode;
    [Export] private Node3D _aimPointDesiredNode;
    [Export(PropertyHint.Layers3DPhysics)] private uint _aimPointCollisionMask = 1;
    [Export] private float _aimPointMaxDistance = 10f;

    [ExportCategory("States")]
    [Export] private bool _isRotating;
    [Export] private Vector3 _designatedPosition;

    public void SetGameplayContext(GameplayContext gameplayContext) => _gameplayContext = gameplayContext;

    public override void _Process(double delta)
    {
        base._Process(delta);
        var designatedPosition = GetDesignationPosition();
        if (designatedPosition.HasValue)
        {
            _designatedPosition = designatedPosition.Value;
            RotateRotationNodeTowardPoint(designatedPosition.Value, delta);
        }
        MoveAimPoint();
        UpdateScreenToWorldDesignationUi();
    }

    private Vector3? GetDesignationPosition()
    {
        if (!_enableDesignation) return null;
        if (!_useScreenToWorldDesignation) return _designationTargetNode?.GlobalPosition;

        // Viewport

        var camera = GetViewport().GetCamera3D();
        if (camera == null) return null;

        // Mouse

        var viewportMousePosition = GetViewport().GetMousePosition();
        var viewportFrom = camera.ProjectRayOrigin(viewportMousePosition);
        var viewportTo = viewportFrom + camera.ProjectRayNormal(viewportMousePosition) * 1000f;

        // Get cast point

        var spaceState = GetViewport().FindWorld3D().DirectSpaceState;
        var query = PhysicsRayQueryParameters3D.Create(viewportFrom, viewportTo);
        query.SetCollisionMask(_screenToWorldCollisionMask);
        var result = spaceState.IntersectRay(query);

        if (result.Count > 0)
        {
            return (Vector3)result["position"];
        }

        return null;
    }

    private void RotateRotationNodeTowardPoint(Vector3 point, double delta)
    {
        if (_aimRotationRootNode == null || _aimRotationNode == null)
        {
            _isRotating = false;
            return;
        }

        var direction = new Vector3(point.X - _aimRotationRootNode.GlobalPosition.X, 0, point.Z - _aimRotationRootNode.GlobalPosition.Z);
        if (direction.LengthSquared() <= 0.001f)
        {
            _isRotating = false;
            return;
        }

        var targetRotation = Mathf.Atan2(direction.X, direction.Z) + Mathf.Pi;
        var currentRotation = _aimRotationNode.Rotation.Y;
        var newRotation = Mathf.RotateToward(currentRotation, targetRotation, delta * _aimRotationSpeed * Mathf.Pi);

        _isRotating = Math.Abs(newRotation - currentRotation) > 0.01f;
        _aimRotationTarget = targetRotation;
        _aimRotationNode.Rotation = new Vector3(0, (float)newRotation, 0);
    }

    private void MoveAimPoint()
    {
        if (_aimPointRootNode == null || _aimPointNode == null || _aimRotationNode == null || _aimPointDesiredNode == null) return;

        var spaceState = GetWorld3D().DirectSpaceState;

        // Actual point

        var point = (Vector3.Forward * _aimPointMaxDistance).Rotated(Vector3.Up, _aimRotationNode.Rotation.Y);
        var pointQuery = PhysicsRayQueryParameters3D.Create(_aimPointRootNode.GlobalPosition, _aimPointRootNode.GlobalPosition + point);
        pointQuery.SetCollisionMask(_aimPointCollisionMask);
        var pointResult = spaceState.IntersectRay(pointQuery);

        if (pointResult.Count > 0) _aimPointNode.GlobalPosition = (Vector3)pointResult["position"];
        else _aimPointNode.GlobalPosition = _aimPointRootNode.GlobalPosition + point;

        // Desired point

        var pointDesired = (Vector3.Forward * _aimPointMaxDistance).Rotated(Vector3.Up, _aimRotationTarget);
        var pointDesiredQuery = PhysicsRayQueryParameters3D.Create(_aimPointRootNode.GlobalPosition, _aimPointRootNode.GlobalPosition + pointDesired);
        pointDesiredQuery.SetCollisionMask(_aimPointCollisionMask);
        var pointDesiredResult = spaceState.IntersectRay(pointDesiredQuery);

        if (pointDesiredResult.Count > 0) _aimPointDesiredNode.GlobalPosition = (Vector3)pointDesiredResult["position"];
        else _aimPointDesiredNode.GlobalPosition = _aimPointRootNode.GlobalPosition + pointDesired;
    }

    private void UpdateScreenToWorldDesignationUi()
    {
        if (!_enableDesignation || !_useScreenToWorldDesignation || !_pushScreenToWorldDesignationToUi || _gameplayContext == null) return;

        var camera = GetViewport().GetCamera3D();
        if (camera == null) return;

        _gameplayContext.Interface.SetCursorAimPoint(camera, _aimPointRootNode.GlobalPosition, _aimPointNode.GlobalPosition, _aimPointDesiredNode.GlobalPosition);
    }

}
