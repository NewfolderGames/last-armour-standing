using System;

namespace LastArmourStanding.Core.Game.Weapon;

public class WeaponProcessor
{
    public Resources.Definitions.Weapon Weapon { get; private set; }

    private int _ammo;
    
    public WeaponProcessor() {}

    public WeaponProcessor(Resources.Definitions.Weapon weapon)
    {
        Weapon = weapon;
    }
    
    public void SetWeapon(Resources.Definitions.Weapon weapon)
    {
        Weapon = weapon;
    }

    public int Use()
    {
        var left = _ammo - Weapon.Use.AmmoCount;
        _ammo = ValidateAmmo(left);
        return Weapon.Use.AmmoCount + (left < 0 ? left : 0);
    }
    
    public void Reload()
    {
        _ammo = ValidateAmmo(_ammo + Weapon.Ammo.ClipSize);
    }

    private int ValidateAmmo(int count)
    {
        if (count < 0) return 0;
        return !Weapon.Ammo.AllowOverflow
            ? Math.Min(_ammo, Weapon.Ammo.ClipSize + Weapon.Ammo.ChamberSize)
            : count;
    }
}