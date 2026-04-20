using Godot;
using LastArmourStanding.Game.Contexts.Gameplay;
using LastArmourStanding.Game.States.Gameplay;
using LastArmourStanding.Scripts.Managers.App;
using LastArmourStanding.Scripts.Managers.Gameplay.Game;
using LastArmourStanding.Scripts.Managers.Gameplay.Interface;
using LastArmourStanding.Scripts.Managers.Gameplay.Interface.Cursor;
using LastArmourStanding.Scripts.Managers.Gameplay.Interface.Hud;
using LastArmourStanding.Scripts.Managers.Gameplay.Renderer;

namespace LastArmourStanding.Scripts.Managers.Gameplay;

public partial class GameplayManager : CanvasLayer
{
    private GameplayContext _gameplayContext;

    [ExportCategory("Roots")]
    [Export] private GameplayRenderer _gameplayRenderer;

    [ExportCategory("Game Roots")]
    [Export] private GameCamera _gameCameraRoot;
    [Export] private GameEnvironment _gameEnvironmentRoot;
    [Export] private GameWorldMap _gameWorldMapRoot;
    [Export] private GameWorldEntities _gameWorldEntitiesRoot;

    [ExportCategory("Interface Roots")]
    [Export] private InterfaceCursor _interfaceCursorRoot;
    [Export] private InterfaceHud _interfaceHudRoot;

    private void SetGameplayContext(GameplayContext gameplayContext)
    {
        _gameplayContext = gameplayContext;
        _gameWorldEntitiesRoot.SetGameplayContext(gameplayContext);
    }

    public override void _EnterTree()
    {
        base._EnterTree();

        var interfaceContext = new GameplayInterfaceContext(_interfaceCursorRoot);

        SetGameplayContext(new GameplayContextBuilder()
            .WithAppContext(AppManager.Instance.AppContext)
            .WithState(new GameplayState())
            .WithCamera(_gameCameraRoot)
            .WithInterface(interfaceContext)
            .Build());
    }

    public override void _Ready()
    {
        base._Ready();

        // DEBUG

        _gameWorldEntitiesRoot._DEBUG_SpawnPlayerUnit();
    }
}
