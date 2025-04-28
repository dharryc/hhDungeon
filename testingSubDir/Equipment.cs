namespace hhTestDungeon;

public enum ItemType { potion, weapon, armor };
public enum WeaponType { dagger, sword, club, shortsword, rib };

public enum ArmorType { chestplate, leggings, boots, helmet };
public abstract class Items
{
    public ItemType TypeOfItem { get; set; }
    public int size { get; set; }
    public int _Durability { get; set; }

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
    public int _damage { get; }

    public int AttackWith()
    {
        _Durability -= 1;
        return _damage;
    }

    public WeaponType Get_Type()
    { return TypeOfWeapon; }

    public Weapon(WeaponType type, int base_damage = 3, int _size = 2, int _durability = 15)
    {
        TypeOfItem = ItemType.weapon;
        TypeOfWeapon = type;
        _damage = base_damage;
        size = _size;
        _Durability = _durability;

    }
}

public class Armor : Equipment
{
    public ArmorType TypeOfArmor { get; }
    public int _defence { get; }

    public int Defend()
    {
        _Durability -= 1;
        return _defence;
    }

    public ArmorType GetType()
    { return TypeOfArmor; }

    public Armor(ArmorType type, int base_defense = 3, int _size = 2)
    {
        TypeOfItem = ItemType.armor;
        TypeOfArmor = type;
        _defence = base_defense;
        size = _size;
    }
}