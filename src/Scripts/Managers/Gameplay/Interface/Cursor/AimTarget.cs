using Godot;

namespace LastArmourStanding.Scripts.Managers.Gameplay.Interface.Cursor;

public partial class AimTarget : Control
{
    public override void _Process(double delta)
    {
        base._Process(delta);
        QueueRedraw();
    }

    public override void _Draw()
    {
        base._Draw();
        var position = GetGlobalMousePosition();
        DrawCircle(position, 3f, new Color(1, 1, 1));
        DrawCircle(position, 3f, new Color(0, 0, 0), false, 1);
    }
}
