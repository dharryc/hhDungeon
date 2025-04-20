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
    public List<Items> itemsInRoom = [];
    public List<(ItemType _type, Items item, int cost)>? storeCosts;
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

        if (coordMap.ContainsKey((X + 1, Y))) DoorLinks.NorthRoom = coordMap[(X + 1, Y)];
        else DoorLinks.NorthRoom = null;
        if (coordMap.ContainsKey((X, Y + 1))) DoorLinks.EastRoom = coordMap[(X, Y + 1)];
        else DoorLinks.EastRoom = null;
        if (coordMap.ContainsKey((X - 1, Y))) DoorLinks.SouthRoom = coordMap[(X - 1, Y)];
        else DoorLinks.SouthRoom = null;
        if (coordMap.ContainsKey((X, Y - 1))) DoorLinks.WestRoom = coordMap[(X, Y - 1)];
        else DoorLinks.WestRoom = null;

        int roomOdds = 101;
        if (seenStairs) roomOdds = 96;

        int roomChoice = rnd.Next(0, roomOdds);

        if (roomChoice < 61)
        {
            EnemyRoom(incomingDifficulty);
            roomChoice = 101;
        }
        if (roomChoice < 81)
        {
            EmptyRoom();
            roomChoice = 101;
        }
        if (roomChoice < 91)
        {
            LootRoom();
            roomChoice = 101;
        }
        if (roomChoice < 96)
        {
            StoreRoom();
            roomChoice = 101;
        }
        else StairRoom();
    }
    public void EnemyRoom(int dif)
    {
        // goblin 40%, slime 30%, orc 10%, troll 5%, skeleton 13%, dragon 2%
        type = RoomType.enemy;
        bool bigLoneEnemy = false;
        List<Enemies> enemyList = [];
        int enemyChoice;
        int enemyRange = 101;
        int numEnemies = rnd.Next(1, 6);
        for (int i = numEnemies; i > 0; i--)
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
            if (bigLoneEnemy) i = -1;
        }
    }
    public void LootRoom(int dif)
    {
        type = RoomType.loot;
        // ItemType { potion, weapon, armor }   50% chance for a potion, 30% for a weapon, 20% for armor
        // WeaponType { dagger, sword, club, shortsword }
        // ArmorType { chestplate, leggings, boots, helmet }
        // potionType { strength, defenseBoost, regeneration }
        double lootOdds = -(dif / (dif ^ 2)) + 1; //loot is more common as game gets harder
        double getLoot = rnd.NextDouble();
        if (getLoot > lootOdds)
        {
            int itemType = rnd.Next(0, 11);
            if(itemType < 6)
            {
                addPotion();
                itemType = 12;
            }
            if(itemType < 9)
            {
                addWeapon();
            }
            else addArmor();
        }
    }

    private void addPotion()
    {
        itemsInRoom.Add(new Potion((Effects)Enum.ToObject(typeof(Effects) , rnd.Next(0, 4)), rnd.Next(1,5)));
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
        type = RoomType.empty;
    }
}