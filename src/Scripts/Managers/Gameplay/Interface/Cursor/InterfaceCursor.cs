using Godot;

namespace LastArmourStanding.Scripts.Managers.Gameplay.Interface.Cursor;

public partial class InterfaceCursor : Control
{
    [ExportCategory("Aim")]
    [Export] private AimTarget _aimTarget;
    [Export] private AimPoint _aimPoint;

    public void SetAimPoint(Camera3D camera, Vector3 center, Vector3 target, Vector3 desired) => _aimPoint.SetPoints(camera, center, target, desired);
    public void SetAimPointSpread(float spread) => _aimPoint.SetSpread(spread);
}
