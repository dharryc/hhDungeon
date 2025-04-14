namespace hhDungeon;
public enum RoomType
{
    enemy, loot, store, stair, empty,
}

public class Room
{
    public RoomType type;
    public List<Enemies> enemies; 
    public Coordinate coord;
    public Room NorthRoom;
    public Room SouthRoom;
    public Room EastRoom;
    public Room WestRoom;
    public (bool )
    public Room()
    {

    }
}