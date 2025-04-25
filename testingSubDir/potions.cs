namespace hhDungeon;

public class Potion : Items
{
    public Effects effect {  get; }
    public int duration {  get;}
    public Potion(Effects effectIn, int durationIn)
    {
        effect = effectIn;
        duration = durationIn;
        size = 1;
        _Durability = 1;
        _type = ItemType.potion;
    }
}