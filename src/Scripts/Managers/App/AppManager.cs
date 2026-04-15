using Godot;
using LastArmourStanding.Core.Resources;
using LastArmourStanding.Core.Resources.Definitions;
using LastArmourStanding.Game.Contexts.App;

namespace LastArmourStanding.Scripts.Managers.App;

public partial class AppManager : Node
{
    public static AppManager Instance { get; private set; }
    public readonly AppContext AppContext = new();
    public readonly ResourceRegistry ResourceRegistry = new();
    
    public AppManager()
    {
        Instance = this;
        
        // Resource Registration
        
        ResourceRegistry.RegisterDefinitionType<Cartridge>();
        ResourceRegistry.RegisterDefinitionType<Weapon>();
    }
}