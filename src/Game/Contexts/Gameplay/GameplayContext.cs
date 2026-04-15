using Godot;
using LastArmourStanding.Game.Contexts.App;
using LastArmourStanding.Game.States.Gameplay;
using LastArmourStanding.Scripts.Managers.Gameplay;

namespace LastArmourStanding.Game.Contexts.Gameplay;

public class GameplayContext
{
    public AppContext AppContext { get; private set; }
    public GameplayState State { get; private set; }
    public GameCamera Camera { get; private set; }
    
    public GameplayContext(AppContext appContext, GameplayState state, GameCamera camera)
    {
        AppContext = appContext;
        State = state;
        Camera = camera;
    }
}

public class GameplayContextBuilder
{
    private AppContext _appContext;
    private GameplayState _state;
    private GameCamera _camera;
    
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

    public GameplayContext Build()
    {
        return new GameplayContext(_appContext, _state, _camera);
    }
}