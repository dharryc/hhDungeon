namespace hhDungeon;


public enum EnemyType { goblin, slime, orc, troll, skeleton, dragon };
public abstract class Enemies
{
    protected int Difficulty { get; set; }
    protected int Health { get; set; }
    protected List<object> Potential_Loot { get; private set; }
    protected object equipedWeapon { get; set; }
    protected int defense { get; set; }
    protected int goldFromKill { get; set; }

    protected EnemyType _Type { get; private set; }

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
            List<object> Grabed_Loot = Potential_Loot;
            Potential_Loot.Clear();
            return Grabed_Loot;
        }
        else
            return null;
    }
}
public class Goblin : Enemies
{
    int min_damage_rel_player = 12;
    int max_damage_rel_player = 7;
    Random rnd = new();
    int gold_cap = 10;
    int min_gold = 0;
    int max_gold = 3;
    public override int Attack(int player_maxhealth, int player_level, int difficulty) // returns the damage.
    {
        int diff_level = difficulty * player_level;
        if (diff_level > gold_cap)
        {
            diff_level = gold_cap;
        }
        return (rnd.Next(max_damage_rel_player, min_damage_rel_player + 1) * player_maxhealth) * difficulty / 3;
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
        return (rnd.Next(max_damage_rel_player, min_damage_rel_player + 1) * player_maxhealth + diff_level) * difficulty / 5;
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
        return (rnd.Next(max_damage_rel_player, min_damage_rel_player + 1) * player_maxhealth + diff_level) * difficulty / 4;
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
        return (rnd.Next(max_damage_rel_player, min_damage_rel_player + 1) * player_maxhealth + diff_level) * difficulty / 5;
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
        return (rnd.Next(max_damage_rel_player, min_damage_rel_player + 1) * player_maxhealth + diff_level) * difficulty / 3;
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
        return (rnd.Next(max_damage_rel_player, min_damage_rel_player + 1) * player_maxhealth + diff_level) * difficulty / 10;
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

}