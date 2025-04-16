namespace hhDungeon;

<<<<<<< HEAD
public class Potion(Effects effectIn, int durationIn) : Items
{
    public ItemType potion = ItemType.potion;
    public Effects effect = effectIn;
    public int duration = durationIn;
=======
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
>>>>>>> 1fdf93002a368f4acde9589078a9582cf2ca0b9f
}