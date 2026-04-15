using Godot;
using LastArmourStanding.Game.Contexts.Gameplay;
using LastArmourStanding.Scripts.Entities.Unit.Controller;

namespace LastArmourStanding.Scripts.Entities.Unit;

public partial class Unit : CharacterBody3D
{
	private GameplayContext _gameplayContext;
	
    [ExportCategory("Controllers")]
    [Export] private AimController _aimController;
    [Export] private CameraController _cameraController;
    [Export] private WeaponController _weaponController;
    
    [ExportCategory("Initial Settings")]
    [Export] private bool _initialSetCameraTarget;
    
    [ExportCategory("Meta")]
    [Export] private string _unitName = "Unit";
    [Export] private string _unitTeam = "Unknown";
    [Export] private bool _isPlayerUnit;
        
    [ExportCategory("Movement")]
    [Export] private bool _enableMovement;
    
    public void SetGameplayContext(GameplayContext gameplayContext)
    {
	    _gameplayContext = gameplayContext;
	    _aimController.SetGameplayContext(gameplayContext);
	    _cameraController.SetGameplayContext(gameplayContext);
	    _weaponController.SetGameplayContext(gameplayContext);
    }

    public override void _Ready()
    {
	    base._Ready();
	    
	    if (_initialSetCameraTarget) _gameplayContext.Camera.SetTarget(_cameraController.CameraAnchor);
    }
}
