namespace hhTestDungeon;

public class Potion : Items
{
    public Effects effect { get; }
    public int duration { get; }
    public Potion(Effects effectIn, int durationIn)
    {
        effect = effectIn;
        duration = durationIn;
        Size = 1;
        Durability = 1;
        TypeOfItem = ItemType.potion;
    }
}