using LastArmourStanding.Core.Resources.Definitions;

namespace LastArmourStanding.Core.Game.Weapon;

public class BeltProcessor
{
    private Belt _belt;
    private int _beltIndex;
    
    public BeltProcessor(Belt belt)
    {
        _belt = belt;
    }
    
    public int Index => _beltIndex;
    public int TrueIndex => _beltIndex % _belt.Count;
    public int Count => _belt.Count;
    public Cartridge Current => _belt.GetCartridge(_beltIndex);

    public Cartridge NextCartridge()
    {
        return _belt.GetCartridge(++_beltIndex);
    }
    
    public void Reset() => _beltIndex = 0;

    public void SetBelt(Belt belt)
    {
        _belt = belt;
        Reset();
    }
}