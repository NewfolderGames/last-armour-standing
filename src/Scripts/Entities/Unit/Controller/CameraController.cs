using Godot;
using LastArmourStanding.Game.Contexts.Gameplay;

namespace LastArmourStanding.Scripts.Entities.Unit.Controller;

public partial class CameraController : Node3D, IGameplayContextConsumer
{
    protected GameplayContext GameplayContext;
    
    [ExportCategory("Nodes")]
    [Export] public Node3D CameraAnchorRoot { get; protected set; }
    [Export] public Node3D CameraAnchor  { get; protected set; }
    
    public void SetGameplayContext(GameplayContext gameplayContext) => GameplayContext = gameplayContext;
}