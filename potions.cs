namespace hhDungeon;

public class Potion(Effects effectIn, int durationIn) : Items
{
    public ItemType potion = ItemType.potion;
    public Effects effect = effectIn;
    public int duration = durationIn;
}