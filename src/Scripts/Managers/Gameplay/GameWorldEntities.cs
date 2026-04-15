using Godot;
using LastArmourStanding.Game.Contexts.Gameplay;
using LastArmourStanding.Scripts.Entities.Unit;

namespace LastArmourStanding.Scripts.Managers.Gameplay;

public partial class GameWorldEntities : Node3D, IGameplayContextConsumer
{
    private GameplayContext _gameplayContext;
    
    public void SetGameplayContext(GameplayContext gameplayContext) => _gameplayContext = gameplayContext;
    
    [ExportCategory("Debug")]
    [Export] private PackedScene _debugPlayerScene;
    
    public void _DEBUG_SpawnPlayerUnit()
    {
        var unit = _debugPlayerScene.Instantiate<Unit>();
        unit.SetGameplayContext(_gameplayContext);
        
        AddChild(unit);
    }
}