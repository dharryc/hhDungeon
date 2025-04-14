namespace hhDungeon;
public enum RoomType
{
    enemy, loot, store, stair, empty,
}

public class Room
{
    public RoomType type;
    public List<Items>? itemsInRoom;
    public List<(Items, int cost)>? storeCosts;
    public List<Enemies>? enemies;
    public Coordinate coord;
    public Room NorthRoom;
    public Room SouthRoom;
    public Room EastRoom;
    public Room WestRoom;
    public (bool NorthDoor, bool SouthDoor, bool EastDoor, bool WestDoor) doors;
    public int X => coord.x;
    public int Y => coord.y;
    public Room((int x, int y) workingCoordinate, int incomingDifficulty, Dictionary<Coordinate, Room> coordMap)
    {
        coord.x = workingCoordinate.x;
        coord.y = workingCoordinate.y;
        NorthRoom = coordMap[new Coordinate(X + 1, Y)];
    }
}

// Room nextRoom = new Room((currentRoom.x + 1, currentRoom.y), DifficultyLevel);