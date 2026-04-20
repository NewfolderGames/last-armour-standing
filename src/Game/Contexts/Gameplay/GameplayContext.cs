using Godot;
using LastArmourStanding.Game.Contexts.App;
using LastArmourStanding.Game.States.Gameplay;
using LastArmourStanding.Scripts.Managers.Gameplay.Game;

namespace LastArmourStanding.Game.Contexts.Gameplay;

public class GameplayContext
{
    public AppContext App { get; private set; }
    public GameplayState State { get; private set; }
    public GameCamera Camera { get; private set; }
    public GameplayInterfaceContext Interface { get; private set; }

    public GameplayContext(AppContext app, GameplayState state, GameCamera camera, GameplayInterfaceContext interfaceContext)
    {
        App = app;
        State = state;
        Camera = camera;
        Interface = interfaceContext;
    }
}

public class GameplayContextBuilder
{
    private AppContext _appContext;
    private GameplayState _state;
    private GameCamera _camera;
    private GameplayInterfaceContext _interfaceContext;

    public GameplayContextBuilder WithAppContext(AppContext appContext)
    {
        _appContext = appContext;
        return this;
    }

    public GameplayContextBuilder WithState(GameplayState state)
    {
        _state = state;
        return this;
    }

    public GameplayContextBuilder WithCamera(GameCamera camera)
    {
        _camera = camera;
        return this;
    }

    public GameplayContextBuilder WithInterface(GameplayInterfaceContext interfaceContext)
    {
        _interfaceContext = interfaceContext;
        return this;
    }

    public GameplayContext Build()
    {
        return new GameplayContext(_appContext, _state, _camera, _interfaceContext);
    }
}
