using System.Collections.Generic;
using Godot;
using LastArmourStanding.Core.Game.Weapon;
using LastArmourStanding.Core.Resources.Definitions;
using LastArmourStanding.Game.Contexts.Gameplay;

namespace LastArmourStanding.Scripts.Entities.Unit.Controller;

public partial class WeaponController : Node3D
{
    private GameplayContext _gameplayContext;
    private readonly List<WeaponProcessor> _processors = [];

    [ExportCategory("Initial Settings")]
    [Export] private bool _useWeaponFromGamePlayContext;

    [ExportCategory("Weapon Selection")]
    [Export] private int _weaponIndex;
    
    public WeaponProcessor SelectedWeapon => _processors[_weaponIndex % _processors.Count];
    public int SelectedWeaponIndex => _weaponIndex;
    
    public void SetGameplayContext(GameplayContext gameplayContext) => _gameplayContext = gameplayContext;

    public override void _Ready()
    {
        if (_useWeaponFromGamePlayContext) UseWeaponsFromGameplayContext();
    }

    public void UseWeapon()
    {
        var used = SelectedWeapon.Use();
        //asdf
    }

    public void SelectWeapon(int index)
    {
        _weaponIndex = index;
        // asdfsfd
    }
    
    private void UseWeaponsFromGameplayContext()
    {
        _processors.Clear();
        _weaponIndex = 0;

        foreach (var weapon in _gameplayContext.State.Weapon.Weapons)
        {
            _processors.Add(new WeaponProcessor(weapon));
        }
    }
    
}