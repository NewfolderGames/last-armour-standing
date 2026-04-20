using System;
using Godot;

namespace LastArmourStanding.Core.Game.Weapon;

public class WeaponProcessor
{
    public Resources.Definitions.Weapon Weapon { get; private set; }

    public int Ammo { get; private set; }
    public float Spread { get; private set; }

    public WeaponProcessor(Resources.Definitions.Weapon weapon)
    {
        SetWeapon(weapon);
    }

    public void Process(double delta)
    {

    }

    public void SetWeapon(Resources.Definitions.Weapon weapon)
    {
        Weapon = weapon;
        Spread = weapon.Use.SpreadMin;
    }

    public int Use()
    {
        var left = Ammo - Weapon.Use.AmmoCount;
        Ammo = ValidateAmmo(left);
        return Weapon.Use.AmmoCount + (left < 0 ? left : 0);
    }

    public void Reload()
    {
        Ammo = ValidateAmmo(Ammo + Weapon.Ammo.ClipSize);
    }

    private int ValidateAmmo(int count)
    {
        if (count < 0) return 0;
        return !Weapon.Ammo.AllowOverflow
            ? Math.Min(Ammo, Weapon.Ammo.ClipSize + Weapon.Ammo.ChamberSize)
            : count;
    }

    public void AddRecoil(float recoil, bool ignoreMultiplier = false)
    {
        Spread = Mathf.Clamp(Spread + recoil * (ignoreMultiplier ? 1 : Weapon.Use.RecoilMultiplier), Weapon.Use.SpreadMin, Weapon.Use.SpreadMax);
    }
}
