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

    public WeaponProcessor SelectedWeapon => _processors.Count == 0 ? null : _processors[_weaponIndex % _processors.Count];
    public int SelectedWeaponIndex => _weaponIndex;

    public void SetGameplayContext(GameplayContext gameplayContext) => _gameplayContext = gameplayContext;

    public override void _Ready()
    {
        if (_useWeaponFromGamePlayContext) UseWeaponsFromGameplayContext();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (SelectedWeapon != null)
        {
            SelectedWeapon.Process(delta);
            _gameplayContext?.Interface.SetCursorAimSpread(SelectedWeapon.Spread);
        }
    }

    public void UseWeapon()
    {
        if (SelectedWeapon == null) return;
        var used = SelectedWeapon.Use();
        _gameplayContext?.Interface.SetCursorAimSpread(SelectedWeapon.Spread);
    }

    public void SelectWeapon(int index)
    {
        if (_processors.Count == 0)
        {
            _weaponIndex = 0;
            return;
        }
        _weaponIndex = index % _processors.Count;
        _gameplayContext?.Interface.SetCursorAimSpread(SelectedWeapon.Spread);
    }

    private void UseWeaponsFromGameplayContext()
    {
        if (_gameplayContext == null) return;

        _processors.Clear();
        foreach (var weapon in _gameplayContext.State.Weapon.Weapons)
        {
            _processors.Add(new WeaponProcessor(weapon));
        }
        SelectWeapon(0);
    }

}
