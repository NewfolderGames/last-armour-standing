using Godot;
using LastArmourStanding.Game.Contexts.App;
using LastArmourStanding.Game.Contexts.Gameplay;
using LastArmourStanding.Scripts.Managers.App;

namespace LastArmourStanding.Scripts.Managers.Gameplay;

public partial class GameplayManager : CanvasLayer
{
    private GameplayContext _gameplayContext;
    
    [ExportCategory("Game World Roots")]
    [Export] private GameCamera _gameCameraRoot;
    [Export] private GameEnvironment _gameEnvironmentRoot;
    [Export] private GameWorldMap _gameWorldMapRoot;
    [Export] private GameWorldEntities _gameWorldEntitiesRoot;
    
    private void SetGameplayContext(GameplayContext gameplayContext)
    {
        _gameplayContext = gameplayContext;
        _gameWorldEntitiesRoot.SetGameplayContext(gameplayContext);
    }

    public override void _EnterTree()
    {
        base._EnterTree();

        SetGameplayContext(new GameplayContextBuilder()
            .WithAppContext(AppManager.Instance.AppContext)
            .WithState(null)
            .WithCamera(_gameCameraRoot)
            .Build());
    }

    public override void _Ready()
    {
        base._Ready();
        
        // DEBUG
        
        _gameWorldEntitiesRoot._DEBUG_SpawnPlayerUnit();
    }
}