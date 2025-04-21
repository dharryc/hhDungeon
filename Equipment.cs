using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace hhDungeon;

public enum ItemType { potion, weapon, armor };
public enum WeaponType { dagger, sword, club, shortsword,rib };

public enum ArmorType { chestplate, leggings, boots, helmet };
public abstract class Items
{
    public ItemType _type { get; set; }
    protected int size { get; set; }
    protected int _Durability { get; set; }

    public int GetSize()
    {
        return size;
    }

    public int Durability()
    {
        return _Durability;
    }

}

public class Equipment : Items
{
    protected bool _Equipped { get; set; }

    public bool AmIEquipped()
    { return _Equipped; }

    public bool ToggleEquiped()
    {
        _Equipped = !_Equipped;
        return _Equipped;
    }
}



public class Weapon : Equipment
{
    protected WeaponType _weaponType { get; }
    protected int _damage { get; }

    public int AttackWith()
    {
        _Durability -= 1;
        return _damage;
    }

    public WeaponType Get_Type()
    { return _weaponType; }

    public Weapon(WeaponType type, int base_damage = 3, int _size = 2, int _durability = 15)
    {
        _weaponType = type;
        _damage = base_damage;
        size = _size;
        _Durability = _durability;

    }
}

public class Armor : Equipment
{
    public ArmorType _Type { get; }
    protected int _defence { get; }

    public int Defend()
    {
        _Durability -= 1;
        return _defence;
    }

    public ArmorType GetType()
    { return _Type; }


    public int GetDefence() => _defence;

    public Armor(ArmorType type, int base_defense = 3, int _size = 2)
    {
        _Type = type;
        _defence = base_defense;
        size = _size;
    }
}