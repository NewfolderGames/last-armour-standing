using Godot;

namespace LastArmourStanding.Scripts.Managers.Gameplay;

public partial class GameCamera : Node3D
{
    [ExportCategory("Camera")]
    [Export] private Camera3D _camera;
    
    [ExportCategory("Target")]
    [Export] private bool _followTarget;
    [Export] private Node3D _targetNode;

    [ExportCategory("Shake")]
    [Export] private bool _enableShake;
    
    [ExportCategory("States")]
    [Export] private bool _isFollowingTarget;
    [Export] private Vector3 _currentOffset;
    [Export] private float _shakeStrength;

    public override void _Process(double delta)
    {
        base._Process(delta);
        
        // Follow target

        if (_followTarget && _targetNode != null)
        {
            GlobalPosition = _targetNode.GlobalPosition;
            GlobalRotation = _targetNode.GlobalRotation;
        }
        
        // Offset

        var newOffset = _currentOffset;
        
        // Shake

        if (_shakeStrength > 0 && _enableShake)
        {
            newOffset += new Vector3(GD.Randf() * _shakeStrength, GD.Randf() * _shakeStrength, GD.Randf() * _shakeStrength);
            _shakeStrength = Mathf.Max(0, _shakeStrength - (float)delta);
        }
        
        // Set position

        _camera.Position = newOffset;
    }
    
    public void SetTarget(Node3D target)
    {
        _targetNode = target;
    }

    public void Punch(float shakeStrength)
    {
        if (shakeStrength > _shakeStrength)
        {
            _shakeStrength = shakeStrength;
        }
    }
    
}