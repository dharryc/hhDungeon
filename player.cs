namespace hhDungeon;
public class Player
{
    public Random rnd = new();
    public int XP;
    public int MaxHealth;
    public int CurrentHealth;
    public List<Item> items = [];
    public List<(Effects effect, int duration)> currentEffects = [];
    public Item EquippedWeapon;
    public int Gold;
    public int MaxInventorySpace;
    public int NewLevelXPThreshold;
    public int CurrentLevel;
    public int BaseATK;
    public double CurrentATK;
    public int critOdds = 9;
    public double Attack()
    {
        int hitCrit = rnd.Next(1, critOdds);
        double critMult = 1;
        if (hitCrit % (critOdds - 1) == 0) critMult += rnd.NextDouble() * 0.3;
        return (CurrentATK + EquippedWeapon.Damage) * critMult;
    }
    public (int Health, List<Effects> effects) getStatus()
    {
        List<Effects> returnedEffects = [];
        foreach (var i in currentEffects) returnedEffects.Add(i.effect);
        return (CurrentHealth, returnedEffects);
    }
    public void CheckStateBasedActions()
    {
        if (XP > NewLevelXPThreshold) levelUp();
        if (CurrentHealth < 0) //deathSomething;
            foreach (var i in currentEffects)
            {
                if (i.duration <= 0) currentEffects.Remove(i);
                else checkEffect(i.effect);
            }
    }
    public void checkEffect(Effects effect)
    {
        switch (effect)
        {
            case Effects.strength:
                CurrentATK = BaseATK * 1.5;
                break;
        }
    }
    public void levelUp()
    {

    }
}
//     levelUp() : void
