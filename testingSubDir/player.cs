namespace hhTestDungeon;
public class Player
{
    public Random rnd = new();
    public int playerLevel = 1;
    public int XP;
    public int MaxHealth = 15;
    public int CurrentHealth = 15;
    public List<Items> items = [];
    public List<(Effects effect, int duration)> currentEffects = [];
    public Weapon? EquippedWeapon;
    public int Gold;
    public int MaxInventorySpace;
    public int NewLevelXPThreshold;
    public (Armor equippedChestplate, int durability) chestplate = (new Armor(ArmorType.chestplate, 0, 0), 0);
    public (Armor equippedLeggings, int durability) leggings = (new Armor(ArmorType.leggings, 0, 0), 0);
    public (Armor equippedHelmet, int durability) helmet = (new Armor(ArmorType.helmet, 0, 0), 0);
    public (Armor equippedBoots, int durability) boots = (new Armor(ArmorType.boots, 0, 0), 0);
    public int CurrentLevel;
    public double BaseATK = 2;
    public double CurrentATK = 2;
    public int critOdds = 9;
    public double BaseDefense;
    public double CurrentDefense;
    public (double damage, bool crit) Attack()
    {
        bool criticalHit = false;
        int hitCrit = rnd.Next(1, critOdds);
        double critMult = 1;
        if (hitCrit % (critOdds - 1) == 0) critMult += rnd.NextDouble() * 0.3;
        if (EquippedWeapon != null)
        {
            return ((CurrentATK + EquippedWeapon.AttackWith()) * critMult, criticalHit);
        }
        else
        {
            return (CurrentATK * critMult, criticalHit);
        }
    }
    public (int Health, List<Effects> effects) GetStatus()
    {
        List<Effects> returnedEffects = [];
        foreach (var (effect, duration) in currentEffects) returnedEffects.Add(effect);
        return (CurrentHealth, returnedEffects);
    }
    public void CheckStateBasedActions()
    {
        if (XP > NewLevelXPThreshold) LevelUp();
        foreach (var i in currentEffects)
        {
            if (i.duration <= 0) currentEffects.Remove(i);
            else CheckEffect(i.effect);
        }
        if (EquippedWeapon?.Durability <= 0)
        {
            EquippedWeapon = null;
        }
        BaseDefense += chestplate.equippedChestplate._defence;
        BaseDefense += leggings.equippedLeggings._defence;
        BaseDefense += helmet.equippedHelmet._defence;
        BaseDefense += boots.equippedBoots._defence;
    }
    public void CheckEffect(Effects effect)
    {
        switch (effect)
        {
            case Effects.strength:
                CurrentATK = BaseATK; //make sure you're starting at normal
                CurrentATK = BaseATK * 1.2;
                break;
            case Effects.weakness:
                CurrentATK = BaseATK; //make sure you're starting at normal
                CurrentATK = BaseATK * 0.9;
                break;
            case Effects.weakness2:
                CurrentATK = BaseATK; //make sure you're starting at normal
                CurrentATK = BaseATK * 0.8;
                break;
            case Effects.weakness3:
                CurrentATK = BaseATK; //make sure you're starting at normal
                CurrentATK = BaseATK * 0.75;
                break;
            case Effects.regeneration:
                CurrentHealth += 5;
                break;
            case Effects.poison:
                CurrentHealth -= 5;
                break;
            case Effects.defenseDown:
                CurrentDefense = BaseDefense; //make sure you're starting at base
                CurrentDefense = BaseDefense * 0.9;
                break;
            case Effects.defenseBoost:
                CurrentDefense = BaseDefense; //make sure you're starting at base
                CurrentDefense = BaseDefense * 1.15;
                break;
        }
    }
    public void LevelUp()
    {
        BaseATK *= 1.5;
        BaseDefense *= 1.5;
        double newHealth = MaxHealth * 1.5;
        MaxHealth = (int)newHealth;
    }
}
