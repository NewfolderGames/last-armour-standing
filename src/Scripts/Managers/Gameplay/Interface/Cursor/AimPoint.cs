using Godot;

namespace LastArmourStanding.Scripts.Managers.Gameplay.Interface.Cursor;

public partial class AimPoint : Control
{
    [Export] private Camera3D _camera;
    [Export] private Vector3 _center;
    [Export] private Vector3 _target;
    [Export] private Vector3 _desired;
    [Export] private float _spread;

    [ExportCategory("Config")]
    [Export] private int _borderMargin = 16;

    public override void _Process(double delta)
    {
        base._Process(delta);
        QueueRedraw();
    }

    public override void _Draw()
    {
        base._Draw();
        if (_camera == null) return;

        // Calculation

        var spread = Mathf.Clamp(Mathf.Abs(_spread), 1.5f, 180) % 180;

        DrawTarget(_desired, spread, 0.5f);
        DrawTarget(_target, spread);
    }

    private void DrawTarget(Vector3 target, float spread, float alpha = 1.0f)
    {
        var position = _camera.UnprojectPosition(target);

        var distance = _center.DistanceTo(target);
        var halfAngleRad = Mathf.DegToRad(spread / 2.0f);
        var worldRadius = distance * Mathf.Tan(halfAngleRad);

        var edgePosition3D = target + (_camera.GlobalTransform.Basis.X * worldRadius);
        var edgePosition2D = _camera.UnprojectPosition(edgePosition3D);

        var radius = position.DistanceTo(edgePosition2D);

        DrawCircle(position, radius, new Color(0, 0, 0, alpha), false, 4);
        DrawCircle(position, radius, new Color(1, 1, 1, alpha), false, 2);
    }

    public void SetPoints(Camera3D camera, Vector3 center, Vector3 target, Vector3 desired)
    {
        _camera = camera;
        _center = center;
        _target = target;
        _desired = desired;
    }

    public void SetSpread(float spread) => _spread = spread;
}
