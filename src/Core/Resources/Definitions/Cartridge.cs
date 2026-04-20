using System;
using System.Xml.Serialization;

namespace LastArmourStanding.Core.Resources.Definitions;

[Serializable]
[ResourceDefinition("core", "cartridge")]
public partial class Cartridge : ResourceDefinition
{
	[XmlElement("display")]
	public CartridgeDisplay Display;
	[XmlElement("projectile")]
	public CartridgeProjectile Projectile;
}

[Serializable]
public class CartridgeUse
{
	[XmlElement("recoil")]
	public float Recoil;
	[XmlElement("projectileCount")]
	public int ProjectileCount;
}

[Serializable]
public class CartridgeProjectile
{
	[XmlElement("tracer")]
	public CartridgeProjectileTracer Tracer;
	[XmlElement("damage")]
	public CartridgeProjectileDamage Damage;
	[XmlElement("speed")]
	public float Speed;
}

[Serializable]
public class CartridgeProjectileDamage
{
	[XmlElement("flat")]
	public float Flat;
	[XmlElement("penetration")]
	public float Penetration;
}

[Serializable]
public class CartridgeProjectileTracer
{
	[XmlElement("visible")]
	public bool Visible = true;
	[XmlElement("aimSpreadReduction")]
	public float AimSpreadReduction;
}

[Serializable]
public class CartridgeDisplay
{
	[XmlElement("uiIconSprite")]
	public string UiIconSprite;
	[XmlElement("uiHudWeaponSprite")]
	public string UiHudWeaponSprite;
}
