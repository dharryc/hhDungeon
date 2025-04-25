namespace hhDungeon;


public enum EnemyType { goblin, slime, orc, troll, skeleton, dragon };
public abstract class Enemies
{
    protected int Difficulty { get; set; }
    protected int Health { get; set; }
    public List<Items> Potential_Loot = [];
    protected Weapon? equipedWeapon { get; set; }
    protected int defense { get; set; }
    protected int goldFromKill { get; set; }

    public EnemyType _Type;

    protected int XPDrop { get; set; }
    public bool Defeated { get; private set; }
    //Methods
    public void TakeDamage(int Amount)
    {
        Health -= Amount;
        if (Health <= 0)
        {
            Defeated = true;
        }
    }

    public abstract int Attack(int player_maxhealth, int player_level, int difficulty);

    public EnemyType GetType()
    {
        return _Type;
    }
    public object ViewLoot()
    {
        if (Defeated)
        {
            return Potential_Loot;
        }
        else
            return null;
    }

    public object? GrabLoot()
    {
        if (Defeated)
        {
            List<Items> Grabed_Loot = Potential_Loot;
            Potential_Loot.Clear();
            return Grabed_Loot;
        }
        else
            return null;
    }

    public int GetHealth()
    { return Health; }
}
public class Goblin : Enemies
{
    static int min_damage_rel_player = 12;
    static int max_damage_rel_player = 7;
    Random rnd = new();
    static int gold_cap = 10;
    static int min_gold = 0;
    static int max_gold = 3;
    public override int Attack(int player_maxhealth, int player_level, int difficulty) // returns the damage.
    {
        int diff_level = difficulty * player_level;
        if (diff_level > gold_cap)
        {
            diff_level = gold_cap;
        }
        int total = (rnd.Next(max_damage_rel_player, min_damage_rel_player + 1) * player_maxhealth) * difficulty / 3;
        if (equipedWeapon is not null)
        {
            total += equipedWeapon.AttackWith();
        }
        return total;
    }

    public int GrabGold(int difficulty, int player_level)
    {
        int diff_level = difficulty * player_level;
        if (diff_level > gold_cap)
        {
            diff_level = gold_cap;
        }
        return (difficulty * rnd.Next(min_gold, max_gold) + diff_level);
    }

    public Goblin(int difficulty)
    {
        _Type = EnemyType.goblin;
        Difficulty = difficulty;
        Health = 3 ^ (difficulty / 4);
        defense = rnd.Next(0, 3);
        if (rnd.Next(0, 9 / difficulty) == 0)
        {
            equipedWeapon = new Weapon(WeaponType.dagger, difficulty, 1);
            Potential_Loot.Add(equipedWeapon);
        }
        else { equipedWeapon = null; }

        if (rnd.Next(0, (9 / difficulty) + 2) == 0)
        {
            Potential_Loot.Add(new Potion(Effects.regeneration, 2));
        }

        if (rnd.Next(0, 9) == 8)
        {
            Potential_Loot.Add(new Armor(ArmorType.chestplate, 2, 3));
        }

    }
}


public class Slime : Enemies
{
    int min_damage_rel_player = 20;
    int max_damage_rel_player = 15;
    Random rnd = new();
    int gold_cap = 5;
    int min_gold = 0;
    int max_gold = 1;
    public override int Attack(int player_maxhealth, int player_level, int difficulty) // returns the damage.
    {
        int diff_level = difficulty * player_level;
        if (diff_level > gold_cap)
        {
            diff_level = gold_cap;
        }

        int total = (rnd.Next(max_damage_rel_player, min_damage_rel_player + 1) * player_maxhealth + diff_level) * difficulty / 5;
        if (equipedWeapon is not null)
        {
            total += equipedWeapon.AttackWith();
        }
        return total;
    }

    public int GrabGold(int difficulty, int player_level)
    {
        int diff_level = difficulty * player_level;
        if (diff_level > gold_cap)
        {
            diff_level = gold_cap;
        }

        return (difficulty * rnd.Next(min_gold, max_gold + 1) + diff_level);
    }

    public Slime(int difficulty)
    {
        //protected int Difficulty { get; set; }
        //protected int Health { get; set; }
        //protected List<Items> Potential_Loot { get; private set; }
        //protected object equipedWeapon { get; set; }
        //protected int defense { get; set; }
        _Type = EnemyType.slime;
        Difficulty = difficulty;
        Health = 2 ^ (difficulty / 5);
        defense = rnd.Next(0, 3);
        if (rnd.Next(0, 4 / difficulty) == 0)
        {
            Potion pP1 = new Potion(Effects.strength, 3);
            Potion pP2 = new Potion(Effects.regeneration, 4);
            Potion pP3 = new Potion(Effects.defenseBoost, 2);
            for (int i = 0; i < rnd.Next(0, 4); i++)
            {
                int chosen = rnd.Next(0, 3);
                switch (chosen)
                {
                    case 0:
                        Potential_Loot.Add(pP1);
                        break;
                    case 1:
                        Potential_Loot.Add(pP2);
                        break;
                    case 2:
                        Potential_Loot.Add(pP3);
                        break;
                }
            }

        }

    }

}


public class Orc : Enemies
{
    int min_damage_rel_player = 9;
    int max_damage_rel_player = 6;
    Random rnd = new();
    int gold_cap = 20;
    int min_gold = 1;
    int max_gold = 4;
    public override int Attack(int player_maxhealth, int player_level, int difficulty) // returns the damage.
    {
        int diff_level = difficulty * player_level;
        if (diff_level > gold_cap)
        {
            diff_level = gold_cap;
        }


        int total = (rnd.Next(max_damage_rel_player, min_damage_rel_player + 1) * player_maxhealth + diff_level) * difficulty / 4;
        if (equipedWeapon is not null)
        {
            total += equipedWeapon.AttackWith();
        }
        return total;
    }

    public int GrabGold(int difficulty, int player_level)
    {
        int diff_level = difficulty * player_level;
        if (diff_level > gold_cap)
        {
            diff_level = gold_cap;
        }

        return (difficulty * rnd.Next(min_gold, max_gold + 1) + diff_level);
    }


    public Orc(int difficulty)
    {
        _Type = EnemyType.orc;
        Difficulty = difficulty;
        Health = 6 ^ (difficulty / 5);
        defense = rnd.Next(0, 3);
        if (rnd.Next(0, 9 / difficulty) == 0)
        {
            equipedWeapon = new Weapon(WeaponType.shortsword, difficulty, 1);
            Potential_Loot.Add(equipedWeapon);
        }
        else { equipedWeapon = new Weapon(WeaponType.dagger, 2, 1); }

        if (rnd.Next(0, 9) == 8)
        {
            Potential_Loot.Add(new Armor(ArmorType.chestplate, 4, 3));
        }

    }

}

public class Troll : Enemies
{

    int min_damage_rel_player = 6;
    int max_damage_rel_player = 3;
    Random rnd = new();
    int gold_cap = 30;
    int min_gold = 0;
    int max_gold = 7;
    public override int Attack(int player_maxhealth, int player_level, int difficulty) // returns the damage.
    {
        int diff_level = difficulty * player_level;
        if (diff_level > gold_cap)
        {
            diff_level = gold_cap;
        }
        int total = (rnd.Next(max_damage_rel_player, min_damage_rel_player + 1) * player_maxhealth + diff_level) * difficulty / 5;
        if (equipedWeapon is not null)
        {
            total += equipedWeapon.AttackWith();
        }
        return total;
    }

    public int GrabGold(int difficulty, int player_level)
    {
        int diff_level = difficulty * player_level;
        if (diff_level > gold_cap)
        {
            diff_level = gold_cap;
        }

        return (difficulty * rnd.Next(min_gold, max_gold + 1) + diff_level);
    }


    public Troll(int difficulty)
    {
        _Type = EnemyType.troll;
        Difficulty = difficulty;
        Health = 9 ^ ((difficulty + 5) / 7);
        defense = rnd.Next(0, 3);
        if (rnd.Next(0, (7 / difficulty) + 2) == 0)
        {
            equipedWeapon = new Weapon(WeaponType.sword, 5, 2);
            Potential_Loot.Add(equipedWeapon);
        }
        else { equipedWeapon = new Weapon(WeaponType.club, 3, 2); }

        if (rnd.Next(0, 9) == 8)
        {
            Potential_Loot.Add(new Armor(ArmorType.chestplate, 5, 4));
        }


        if (rnd.Next(0, 5 / difficulty) <= 1)
        {
            Potion pP1 = new Potion(Effects.strength, 3);
            Potion pP2 = new Potion(Effects.regeneration, 4);
            Potion pP3 = new Potion(Effects.defenseBoost, 2);
            Potion pP4 = new Potion(Effects.strength, 5);
            Potion pP5 = new Potion(Effects.regeneration, 7);
            for (int i = 0; i < rnd.Next(0, 7); i++)
            {
                int chosen = rnd.Next(0, 5);
                switch (chosen)
                {
                    case 0:
                        Potential_Loot.Add(pP1);
                        break;
                    case 1:
                        Potential_Loot.Add(pP2);
                        break;
                    case 2:
                        Potential_Loot.Add(pP3);
                        break;
                    case 3:
                        Potential_Loot.Add(pP4);
                        break;
                    case 4:
                        Potential_Loot.Add(pP5);
                        break;
                }
            }

        }

    }
}




public class Skeleton : Enemies
{
    int min_damage_rel_player = 12;
    int max_damage_rel_player = 7;
    Random rnd = new();
    int gold_cap = 15;
    int min_gold = 0;
    int max_gold = 2;
    public override int Attack(int player_maxhealth, int player_level, int difficulty) // returns the damage.
    {
        int diff_level = difficulty * player_level;
        if (diff_level > gold_cap)
        {
            diff_level = gold_cap;
        }
        int total = (rnd.Next(max_damage_rel_player, min_damage_rel_player + 1) * player_maxhealth + diff_level) * difficulty / 3;
        if (equipedWeapon is not null)
        {
            total += equipedWeapon.AttackWith();
        }
        return total;
    }

    public int GrabGold(int difficulty, int player_level)
    {
        int diff_level = difficulty * player_level;
        if (diff_level > gold_cap)
        {
            diff_level = gold_cap;
        }

        return (difficulty * rnd.Next(min_gold, max_gold + 1) + diff_level);
    }

    public Skeleton(int difficulty)
    {
        _Type = EnemyType.skeleton;
        Difficulty = difficulty;
        Health = 2 ^ (difficulty / 5);
        defense = rnd.Next(0, 3);
        if (rnd.Next(0, (7 / difficulty) + 2) == 0)
        {
            equipedWeapon = new Weapon(WeaponType.rib, 1, 2);
            Potential_Loot.Add(equipedWeapon);
        }


    }

}


public class Dragon : Enemies
{
    int min_damage_rel_player = 5;
    int max_damage_rel_player = 3;
    Random rnd = new();
    int gold_cap = 20;
    int min_gold = 15;
    int max_gold = 80;
    public override int Attack(int player_maxhealth, int player_level, int difficulty) // returns the damage.
    {
        int diff_level = difficulty * player_level;
        if (diff_level > gold_cap)
        {
            diff_level = gold_cap;
        }
        int total = (rnd.Next(max_damage_rel_player, min_damage_rel_player + 1) * player_maxhealth + diff_level) * difficulty / 10;
        if (equipedWeapon is not null)
        {
            total += equipedWeapon.AttackWith();
        }
        return total;
    }

    public int GrabGold(int difficulty, int player_level)
    {
        int diff_level = difficulty * player_level;
        if (diff_level > gold_cap)
        {
            diff_level = gold_cap;
        }

        return (difficulty * rnd.Next(min_gold, max_gold + 1) + diff_level);
    }

    public Dragon(int difficulty)
    {
        _Type = EnemyType.dragon;
        Difficulty = difficulty;
        Health = 10 ^ ((difficulty + 7) / 7);
        defense = rnd.Next(0, 3);

    }

}