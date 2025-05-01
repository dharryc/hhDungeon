namespace hhDungeon;

public enum ItemType { potion, weapon, armor };
public enum WeaponType { dagger, sword, club, shortsword, rib };

public enum ArmorType { chestplate, leggings, boots, helmet };
public abstract class Items
{
    public ItemType TypeOfItem { get; set; }
    public int Size { get; set; }
    public int Durability { get; set; }
}

public class Equipment : Items
{
    public bool _Equipped { get; set; }

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
    public WeaponType TypeOfWeapon { get; }
    public int Damage { get; }

    public int AttackWith()
    {
        Durability -= 1;
        return Damage;
    }

    public Weapon(WeaponType type, int base_damage = 3, int _durability = 15)
    {
        TypeOfItem = ItemType.weapon;
        TypeOfWeapon = type;
        Damage = base_damage;
        Size = 2;
        Durability = _durability;

    }
}

public class Armor : Equipment
{
    public ArmorType TypeOfArmor { get; }
    public int _defence { get; }

    public int Defend()
    {
        Durability -= 1;
        return _defence;
    }

    public Armor(ArmorType type, int base_defense = 3, int _size = 2)
    {
        TypeOfItem = ItemType.armor;
        TypeOfArmor = type;
        _defence = base_defense;
        Size = 2;
        Durability = _size;
    }
}