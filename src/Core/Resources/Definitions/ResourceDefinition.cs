using System;
using System.Xml.Serialization;
using Godot;

namespace LastArmourStanding.Core.Resources.Definitions;

[GlobalClass]
[Serializable]
public abstract partial class ResourceDefinition : Resource
{

	[Export] [XmlAttribute("type")] public string Type;
	[Export] [XmlAttribute("namespace")] public string Namespace;
	[Export] [XmlAttribute("name")] public string Name;
	
	public string FullName => $"{Namespace}:{Name}";
	public string ResourceFullName => $"{Type}#{FullName}";

	public static string DefFullNameOf<T>() where T : ResourceDefinition =>
		(Attribute.GetCustomAttribute(typeof(T), typeof(ResourceDefinitionAttribute)) as ResourceDefinitionAttribute)
		?.FullName;
	
}
