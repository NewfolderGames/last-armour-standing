using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using LastArmourStanding.Core.Resources.Definitions;

namespace LastArmourStanding.Core.Resources;

public class ResourceRegistry {

	private readonly Dictionary<string, ResourceDefinition> _definitions = new();
	private readonly Dictionary<string, XmlSerializer> _serializers = new();

	public ResourceDefinition Get(string fullName) => _definitions[ResolveDefFullName(fullName)];

	public T Get<T>(string resourceFullName) where T : ResourceDefinition
	{
		if (_definitions.TryGetValue(ResolveFullName<T>(resourceFullName), out var definition) && definition is T typedDefinition) {
			return typedDefinition;
		}
		return null;
	}

	public ResourceDefinition Add(ResourceDefinition definition)
	{
		if (definition == null) {
			return null;
		}

		if (_definitions.TryGetValue(definition.ResourceFullName, out var existingDefinition)) {
			_definitions[definition.ResourceFullName] = definition;
			return existingDefinition;
		}

		_definitions[definition.ResourceFullName] = definition;
		return null;
	}

	public ResourceDefinition LoadFromStream(Stream stream)
	{
		using var reader = XmlReader.Create(stream);

		reader.MoveToContent();
		if (reader.Name != "Def") throw new XmlException("Root element must be Def");

		var definitionType = reader.GetAttribute("type");
		var serializer = _serializers.GetValueOrDefault(definitionType);

		return serializer == null
			? throw new XmlException($"No serializer registered for type '{definitionType}'")
			: Add(serializer.Deserialize(reader) as ResourceDefinition);
	}

	public void RegisterDefinitionType<T>() where T : ResourceDefinition
	{
		var type = typeof(T);
		var name = ResourceDefinition.DefFullNameOf<T>();
		if (name == null) throw new InvalidOperationException($"Type '{type.FullName}' does not have a ResourceDefinitionAttribute");

		var serializer = new XmlSerializer(type, new XmlRootAttribute("Def"));

		_serializers.Add(name, serializer);
	}

	public static string ResolveDefFullName(string name)
	{
        ArgumentNullException.ThrowIfNull(name);

        var defNamespaceIndex = name.IndexOf(':');
		var defNamespace = defNamespaceIndex == -1 ? "core" : name[..defNamespaceIndex];
		var defName = name[(defNamespaceIndex + 1)..];

		return $"{defNamespace}:{defName}";
	}

	public static string ResolveResourceFullName(string name)
	{
		ArgumentNullException.ThrowIfNull(name);

		var resourceNamespaceIndex = name.IndexOf(':');
		var resourceNamespace = resourceNamespaceIndex == -1 ? "core" : name[..resourceNamespaceIndex];
		var resourceName = name[(resourceNamespaceIndex + 1)..];

		return $"{resourceNamespace}:{resourceName}";
	}

	public static string ResolveFullName(string name)
	{
        ArgumentNullException.ThrowIfNull(name);
        var index = name.IndexOf('#');
		return index == -1
			? throw new ArgumentException("Name must contain a def type and resource name", nameof(name))
			: $"{ResolveDefFullName(name[..index])}#{ResolveResourceFullName(name[(index + 1)..])}";
	}

	public static string ResolveFullName(string defFullName, string resourceName) => ResolveFullName($"{defFullName}#{resourceName}");

	public static string ResolveFullName<T>(string resourceName) where T : ResourceDefinition => ResolveFullName(ResourceDefinition.DefFullNameOf<T>(), resourceName);

}
