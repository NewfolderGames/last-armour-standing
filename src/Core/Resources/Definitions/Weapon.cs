using System;
using System.Xml.Serialization;

namespace LastArmourStanding.Core.Resources.Definitions;

[Serializable]
[ResourceDefinition("core", "weapon")]
public partial class Weapon : ResourceDefinition
{
    [XmlElement("display")]
    public WeaponDisplay Display;
    [XmlElement("ammo")]
    public WeaponAmmo Ammo;
    [XmlElement("use")]
    public WeaponUse Use;
}

[Serializable]
public class WeaponDisplay
{
    [XmlElement("uiIconSprite")]
    public string UiIconSprite;
}

[Serializable]
public class WeaponAmmo
{
    [XmlElement("clipSize")]
    public int ClipSize;
    [XmlElement("chamberSize")]
    public int ChamberSize;
    [XmlElement("reloadSize")]
    public int ReloadSize;
    [XmlElement("reloadTime")]
    public int ReloadTime;
    [XmlElement("allowOverflow")]
    public bool AllowOverflow;
}

[Serializable]
public class WeaponUse
{
    [XmlElement("ammoCount")]
    public int AmmoCount;
    [XmlElement("projectileCount")]
    public int ProjectileCount;
    [XmlElement("cooldownTime")]
    public int CooldownTime;
    [XmlElement("recoilMultiplier")]
    public float RecoilMultiplier;
}
