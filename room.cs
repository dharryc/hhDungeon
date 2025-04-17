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
    public (int x, int y) coord;
    public Room NorthRoom;
    public Room SouthRoom;
    public Room EastRoom;
    public Room WestRoom;
    public (bool NorthDoor, bool SouthDoor, bool EastDoor, bool WestDoor) doors;
    public int X => coord.x;
    public int Y => coord.y;
    public Room((int x, int y) workingCoordinate, int incomingDifficulty, Dictionary<(int x, int y), Room> coordMap, int MaxRooms, int RoomsExplored)
    {
        coord.x = workingCoordinate.x;
        coord.y = workingCoordinate.y;
        NorthRoom = coordMap[(X + 1, Y)];

    }
    public Room(RoomType type)
    {

    }
    public static Room EnemyRoom()
    {
        return new Room(RoomType.enemy);
    }
    public static Room LootRoom()
    {
        return new Room(RoomType.loot);
    }
    public Room StoreRoom()
    {
        return new Room(RoomType.store);
    }
    public Room StairRoom()
    {
        return new Room(RoomType.stair);
    }
    public Room EmptyRoom()
    {
        return new Room(RoomType.empty);
    }

}