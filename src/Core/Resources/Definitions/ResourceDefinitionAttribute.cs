namespace LastArmourStanding.Core.Resources.Definitions;

[System.AttributeUsage(System.AttributeTargets.Class)]
public class ResourceDefinitionAttribute : System.Attribute
{
    private readonly string _namespace;
    private readonly string _name;
    
    public ResourceDefinitionAttribute(string ns, string name)
    {
        _namespace = ns;
        _name = name;
    }
    
    public string FullName => $"{_namespace}:{_name}";
    
    public override string ToString() => FullName;
}