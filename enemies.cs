// in Attack methods, weapon damage isn't caculated yet. // to do after weapons implemented.

namespace hhDungeon;


public enum EnemyType { goblin, slime, orc, troll, skeleton };
public abstract class Enimies
{
    protected int Difficulty { get; set; }
    protected int Health { get; set; }
    protected List<object> Potential_Loot { get; private set; }
    protected object equipedweapon { get; set; }
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
public class Goblin : Enimies
{
    //    Goblin is the most "basic" enemy type.Very common, a small little green guy.Will be fairly easy to deal with on almost every difficulty, usually only holding a dagger or shortsword, sometimes not even that.
    //##### Damage
    //    ((1/(7-12) * maxPlayerHP) + base weapon damage) * floor/3
    //##### Gold range
    //    ((dif + 1) * (0 - 2)) + (floor* player level)capped at 10
    //##### Item pool:
    //- Dagger
    //- Shortsword
    //- +10 health potion
    //- leather armor
    int min_damage_rel_player = 12;
    int max_damage_rel_player = 7;
    Random rnd = new();
    int max_diff_level = 10;
    int min_gold = 0;
    int max_gold = 3;
    public override int Attack(int player_maxhealth, int player_level, int difficulty) // returns the damage.
    {
        int diff_level = difficulty * player_level;
        if (diff_level > max_diff_level)
        {
            diff_level = max_diff_level;
        }
        return (rnd.Next(max_damage_rel_player, min_damage_rel_player + 1) * player_maxhealth)*difficulty/3;
    }

    public int GrabGold(int difficulty, int player_level)
    {
        int diff_level = difficulty * player_level;
        if (diff_level > max_diff_level)
        {
            diff_level = max_diff_level;
        }
        return (difficulty * rnd.Next(min_gold, max_gold) + diff_level);
    }
}


public class Slime : Enimies
{

    //### Slime

    //    Literally a slime creature.Very tough and hard to kill, but they do very little damage.The risk of slimes is the effects that they can give you.You can also have many more per room
    //    ##### Damage
    //    ((1/(15-20) * maxPlayerHP) + base weapon damage) * floor/5
    //    EFFECTS(tbd)
    //##### Gold range
    //    ((dif + 1) * (0 - 1)) + (floor* player level)capped at 5
    //##### Item pool:
    //- potions(tbd)
    int min_damage_rel_player = 20;
    int max_damage_rel_player = 15;
    Random rnd = new();
    int max_diff_level = 5;
    int min_gold = 0;
    int max_gold = 1;
    public override int Attack(int player_maxhealth, int player_level, int difficulty) // returns the damage.
    {
        int diff_level = difficulty * player_level;
        if (diff_level > max_diff_level)
        {
            diff_level = max_diff_level;
        }
        return (rnd.Next(max_damage_rel_player, min_damage_rel_player + 1) * player_maxhealth + diff_level)*difficulty/5;
    }

    public int GrabGold(int difficulty, int player_level)
    {
        int diff_level = difficulty * player_level;
        if (diff_level > max_diff_level)
        {
            diff_level = max_diff_level;
        }

        return (difficulty * rnd.Next(min_gold, max_gold +1) + diff_level);
    }

}

// Fields:
// - +Difficulty : int // used in constructor to determine other fields.
// - +Health : int
// - +Potential_Loot : <item> // I assume singular item per enimy
// - EquippedWeapon : item
// - +defense: int // reduces incoming damage? if wanted.
// - +goldfromKill: int
// - +Creature type : <enum>
// - +XPDrop : int

// Methods:
// - +takeDamage(int amount) // this could just take in the amount or it could take in the weapon status and then calculate it.
// - +Attak(Player)
// - +ViewLoot(): <item> int gold // if dead will return possible loot
// - +GrabLoot(): <item> int gold // and set those to 0.
// - +GetType(): <enum> type // if types added becomes nessasary.
// - +WeakenArmor(): void // small chance to weeken its defence if desired.