namespace hhDungeon;
public enum RoomType
{
    enemy, //60% chance
    loot, //10% chance
    store, //5% chance
    stair, //5% chance
    empty, //20% chance
}

public class Room
{
    public RoomType type;
    public List<Items>? itemsInRoom;
    public List<(Items, int cost)>? storeCosts;
    public List<Enemies>? enemies;
    public (int x, int y) coord;
    public (Room? NorthRoom, Room? SouthRoom, Room? EastRoom, Room? WestRoom) DoorLinks;
    public int X => coord.x;
    public int Y => coord.y;
    readonly Random rnd = new();
    public Room((int x, int y) workingCoordinate, int incomingDifficulty, Dictionary<(int x, int y), Room> coordMap, bool seenStairs)
    {
        // enemy 60% chance, loot 10% chance, store 5% chance, stair 5% chance, empty 20% chance
        coord.x = workingCoordinate.x;
        coord.y = workingCoordinate.y;
        DoorLinks.NorthRoom = coordMap[(X + 1, Y)];
        DoorLinks.EastRoom = coordMap[(X, Y + 1)];
        DoorLinks.SouthRoom = coordMap[(X - 1, Y)];
        DoorLinks.WestRoom = coordMap[(X, Y - 1)];
        if (seenStairs)
        {
            int roomChoice = rnd.Next(0, 96); //not a magic number, there's no chance of getting another stair room if you've seen the stairs
            if (roomChoice < 40) EnemyRoom(incomingDifficulty);
        }
    }
    public Room(RoomType type)
    {

    }
    public void EnemyRoom(int dif)
    {
        // goblin 40%, slime 30%, orc 10%, troll 5%, skeleton 13%, dragon 2%
        bool bigLoneEnemy = false;
        List<Enemies> enemyList = [];
        int enemyChoice;
        int enemyRange = 101;
        for (int i = 5; i > 0; i--)
        {
            enemyChoice = rnd.Next(0, enemyRange);
            if (enemyChoice < 40)
            {
                enemyList.Add(new Goblin(dif));
                enemyChoice = 101;
                enemyRange = 93;
                if (enemyList.Count >= 3) i = -1;
            }
            if (enemyChoice < 70)
            {
                enemyList.Add(new Slime(dif));
                enemyChoice = 101;
                enemyRange = 93;
            }
            if (enemyChoice < 83)
            {
                enemyList.Add(new Skeleton(dif));
                enemyChoice = 101;
                enemyRange = 93;
                if (enemyList.Count >= 3) i = -1;
            }
            if (enemyChoice < 93)
            {
                enemyList.Add(new Orc(dif));
                enemyChoice = 101;
                enemyRange = 93;
                if (enemyList.Count >= 3) i = -1;
            }
            if (enemyChoice < 98)
            {
                enemyList.Add(new Troll(dif));
                enemyChoice = 101;
                bigLoneEnemy = true;
            }
            if (enemyChoice < 100)
            {
                enemyList.Add(new Dragon(dif));
                bigLoneEnemy = true;
            }
            if(bigLoneEnemy) i = -1;
        }
    }
    public void LootRoom()
    {
        return new Room(RoomType.loot);
    }
    public void StoreRoom()
    {
        return new Room(RoomType.store);
    }
    public void StairRoom()
    {
        return new Room(RoomType.stair);
    }
    public void EmptyRoom()
    {

    }

}