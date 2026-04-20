using Godot;
using LastArmourStanding.Scripts.Managers.Gameplay.Interface.Cursor;

namespace LastArmourStanding.Game.Contexts.Gameplay;

public class GameplayInterfaceContext
{
    private readonly InterfaceCursor _interfaceCursor;

    public GameplayInterfaceContext(InterfaceCursor interfaceCursor)
    {
        _interfaceCursor = interfaceCursor;
    }

    public void SetCursorAimPoint(Camera3D camera, Vector3 center, Vector3 target, Vector3 desired) => _interfaceCursor.SetAimPoint(camera, center, target, desired);
    public void SetCursorAimSpread(float aimSpread) => _interfaceCursor.SetAimPointSpread(aimSpread);
}
