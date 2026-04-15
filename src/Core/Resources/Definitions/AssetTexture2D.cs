using System;
using System.Xml.Serialization;
using Godot;

namespace LastArmourStanding.Core.Resources.Definitions;

[GlobalClass]
[Serializable]
[ResourceDefinition("core", "$texture")]
public partial class AssetTexture2D : ResourceDefinition
{
    [Export]
    [XmlElement("path")]
    public string Path { get; private set; }
}