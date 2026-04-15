using System.Collections;
using System.Collections.Generic;
using LastArmourStanding.Core.Resources.Definitions;

namespace LastArmourStanding.Core.Game.Weapon;

public class Belt : IEnumerable<Cartridge>
{
    private readonly List<Cartridge> _cartridges = [];
    
    public int Count => _cartridges.Count;
    
    public void AddCartridge(Cartridge cartridge) => _cartridges.Add(cartridge);
    
    public Cartridge GetCartridge(int index) => _cartridges[index % _cartridges.Count];
    
    public IEnumerator<Cartridge> GetEnumerator() => _cartridges.GetEnumerator();
    
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}