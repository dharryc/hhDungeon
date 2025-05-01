using System.Text.Json;

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
    public List<(ItemType _type, Items item, int cost)> storeCosts = [];
    public List<Enemies> enemies = [];
    readonly Random rnd = new();

    public Room(int incomingDifficulty, bool seenStairs, bool lastRoom)
    {
        //in need of touchup
        // enemy 60% chance, loot 10% chance, store 5% chance, stair 5% chance, empty 20% chance
        if (lastRoom && !seenStairs)
        {
            StairRoom();
        }
        else
        {
            int roomOdds = 101;
            if (seenStairs)
                roomOdds = 96;

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
                LootRoom(incomingDifficulty);
                roomChoice = 101;
            }
            if (roomChoice < 96)
            {
                StoreRoom(incomingDifficulty);
            }
        }
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
                enemyRange = 82;
                if (enemyList.Count >= 3)
                    i = -1;
            }
            if (enemyChoice < 70)
            {
                enemyList.Add(new Slime(dif));
                enemyChoice = 101;
                enemyRange = 82;
            }
            if (enemyChoice < 83)
            {
                enemyList.Add(new Skeleton(dif));
                enemyChoice = 101;
                enemyRange = 82;
                if (enemyList.Count >= 3)
                    i = -1;
            }
            if (enemyChoice < 93)
            {
                enemyList.Add(new Orc(dif));
                enemyChoice = 101;
                enemyRange = 93;
                if (enemyList.Count >= 3)
                    i = -1;
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
            if (bigLoneEnemy)
                i = -1;
        }
        foreach (var i in enemyList)
            enemies.Add(i);
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
            if (itemType < 6)
            {
                AddPotion();
                itemType = 12;
            }
            if (itemType < 9)
            {
                AddWeapon(dif);
            }
            else
                AddArmor(dif);
        }
    }

    public void MakeEmpty()
    {
        type = RoomType.empty;
    }

    private void AddArmor(int dif)
    {
        int armorChoice = rnd.Next(0, 4);
        if (armorChoice == 0)
            itemsInRoom.Add(new Armor(ArmorType.boots, rnd.Next(5, 15)));
        if (armorChoice == 1)
            itemsInRoom.Add(new Armor(ArmorType.helmet, rnd.Next(5, 15)));
        if (armorChoice == 2)
            itemsInRoom.Add(new Armor(ArmorType.leggings, rnd.Next(5, 15)));
        if (armorChoice == 3)
            itemsInRoom.Add(new Armor(ArmorType.chestplate, rnd.Next(5, 15)));
    }

    private void AddWeapon(int difficulty)
    {
        // public Weapon(WeaponType type, int base_damage = 3, int _size = 2, int _durability = 15)
        int weaponChoice = rnd.Next(0, 5);
        if (weaponChoice == 0)
            itemsInRoom.Add(
                new Weapon(WeaponType.rib, rnd.Next(5, 11), rnd.Next(3, 3 * difficulty + 5))
            );
        if (weaponChoice == 1)
            itemsInRoom.Add(
                new Weapon(WeaponType.club, rnd.Next(5, 11), rnd.Next(3, 3 * difficulty + 5))
            );
        if (weaponChoice == 2)
            itemsInRoom.Add(
                new Weapon(WeaponType.sword, rnd.Next(5, 11), rnd.Next(3, 3 * difficulty + 5))
            );
        if (weaponChoice == 3)
            itemsInRoom.Add(
                new Weapon(WeaponType.dagger, rnd.Next(5, 11), rnd.Next(3, 3 * difficulty + 5))
            );
        if (weaponChoice == 4)
            itemsInRoom.Add(
                new Weapon(WeaponType.shortsword, rnd.Next(5, 11), rnd.Next(3, 3 * difficulty + 5))
            );
    }

    private void AddPotion()
    {
        List<Effects> goodPool = [Effects.regeneration, Effects.strength, Effects.defenseBoost];
        Potion generatedPotion = new(goodPool[rnd.Next(0, 3)], rnd.Next(1, 4));
        itemsInRoom.Add(generatedPotion);
    }

    public void StoreRoom(int dif)
    {
        int itemChoice;
        type = RoomType.store;
        for (int i = 0; i < 5; i++)
        {
            itemChoice = rnd.Next(0, 3);
            if (itemChoice == 0)
                AddArmor(dif);
            if (itemChoice == 1)
                AddWeapon(dif);
            if (itemChoice == 2)
                AddPotion();
        }
        foreach (var item in itemsInRoom)
            storeCosts.Add((item.TypeOfItem, item, dif * rnd.Next(1, 5)));
    }

    public void StairRoom()
    {
        type = RoomType.stair;
    }

    public void EmptyRoom()
    {
        type = RoomType.empty;
    }

    public Room(RoomType inType)
    {
        type = inType;
    }
}
