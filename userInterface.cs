
namespace hhDungeon;

public class Program
{
    public static string Greeting = "Welcome to our dungeon this dungeon was made by Harry and Himni \nThis dungeon works such that there will be shops along your way. \nThere will also be many enimies these enimies include the goblins, slimes, orcs (\"the goblins big brothers\"), Trolls, and the lengendary Dragons\nThere will also be armor and weapons that you can equip.\nPress any key to continue";
    Player? player;
    public static Dungeon? RunningDungeon;
    public bool RunningGame;
    public void Main(string[] args)
    {
        RunningDungeon = Dungeon.LoadGame();
        player = RunningDungeon.currentPlayer;
        GreetPlayer();
    }

    public void GreetPlayer()
    {
        Console.WriteLine(Greeting);
        Console.ReadKey();
    }

    public void MainGameLoop()
    {
        while (RunningGame)
        {
            switch (RunningDungeon?.currentRoom.type)
            {
                case RoomType.empty:
                    EmptyRoomUi(RunningDungeon.currentRoom);
                    break;
                case RoomType.loot:
                    LootRoomUi(RunningDungeon.currentRoom);
                    break;
                case RoomType.enemy:
                    EnemyRoomUi(RunningDungeon.currentRoom);
                    break;
                case RoomType.stair:
                    StairRoomUi(RunningDungeon.currentRoom);
                    break;
                case RoomType.store:
                    Console.Clear();
                    Console.WriteLine("You've entered a small shop! The items avalible to purchase are:");
                    StoreRoomUi(RunningDungeon.currentRoom);
                    break;
            }
        }
        EndGame();
    }
    private static void DisplayArmor(int i, (ItemType _type, Items item, int cost) workingItem)
    {
        Armor workingArmor = (Armor)workingItem.item;
        Console.WriteLine(i + "    ARMOR      |");
        switch (workingArmor.GetType())
        {
            case ArmorType.boots:
                Console.Write("    BOOTS    |");
                Console.Write("      " + workingItem.cost + "       |");
                Console.Write("         " + workingItem.item.Durability() + "          |");
                break;
            case ArmorType.chestplate:
                Console.Write(" CHESTPLATE  |");
                Console.Write("      " + workingItem.cost + "       |");
                Console.Write("         " + workingItem.item.Durability() + "          |");
                break;
            case ArmorType.helmet:
                Console.Write("    HELMET    |");
                Console.Write("      " + workingItem.cost + "       |");
                Console.Write("         " + workingItem.item.Durability() + "          |");
                break;
            case ArmorType.leggings:
                Console.Write("    PANTS    |");
                Console.Write("      " + workingItem.cost + "       |");
                Console.Write("         " + workingItem.item.Durability() + "          |");
                break;
        }

    }
    private static void DisplayWeapon(int i, (ItemType _type, Items item, int cost) workingItem)
    {

        //    ITEM TYPE   |   SUBTYPE   |   ITEM COST   |   ITEM DURABILITY   |
        //     WEAPON     |    SWORD    |      22       |        22           |
        Weapon workingWeapon = (Weapon)workingItem.item;
        Console.WriteLine(i + "    WEAPON     |");
        switch (workingWeapon.Get_Type())
        {

            case WeaponType.club:
                Console.Write("    CLUB     |");
                Console.Write("      " + workingItem.cost + "       |");
                Console.Write("      " + workingItem.item.Durability() + "           |");
                break;
            case WeaponType.dagger:
                Console.Write("   DAGGER    |");
                Console.Write("      " + workingItem.cost + "       |");
                Console.Write("      " + workingItem.item.Durability() + "           |");
                break;
            case WeaponType.rib:
                Console.Write("     RIB     |");
                Console.Write("      " + workingItem.cost + "       |");
                Console.Write("      " + workingItem.item.Durability() + "           |");
                break;
            case WeaponType.shortsword:
                Console.Write(" SHORTSWORD  |");
                Console.Write("      " + workingItem.cost + "       |");
                Console.Write("      " + workingItem.item.Durability() + "           |");
                break;
            case WeaponType.sword:
                Console.Write("    SWORD    |");
                Console.Write("      " + workingItem.cost + "       |");
                Console.Write("      " + workingItem.item.Durability() + "           |");
                break;
        }
    }
    private static void DisplayPotion(int i, (ItemType _type, Items item, int cost) workingItem)
    {
        //    ITEM TYPE   |   SUBTYPE   |   ITEM COST   |   ITEM DURABILITY   |
        //     POTION     | DEFENCE UP  |      22       |        22           |
        Potion workingPotion = (Potion)workingItem.item;
        Console.WriteLine(i + "    POTION     |");
        switch (workingPotion.effect)
        {
            case Effects.regeneration:
                Console.Write(" REGENERATION|");
                Console.Write("      " + workingItem.cost + "       |");
                Console.Write("      " + workingItem.item.Durability() + "           |");
                break;
            case Effects.strength:
                Console.Write("  STRENGTH   |");
                Console.Write("      " + workingItem.cost + "       |");
                Console.Write("      " + workingItem.item.Durability() + "           |");
                break;
            case Effects.defenseBoost:
                Console.Write(" DEFENCE UP  |");
                Console.Write("      " + workingItem.cost + "       |");
                Console.Write("      " + workingItem.item.Durability() + "           |");
                break;
        }
    }
    private static void StoreRoomUi(Room store)
    {
        Console.WriteLine("   ITEM TYPE   |   SUBTYPE   |   ITEM COST   |   ITEM DURABILITY   |");
        int i = 1;
        foreach (var workingItem in store.storeCosts)
        {
            switch (workingItem._type)
            {
                case ItemType.armor:
                    DisplayArmor(i, workingItem);
                    break;
                case ItemType.potion:
                    DisplayPotion(i, workingItem);
                    break;
                case ItemType.weapon:
                    DisplayWeapon(i, workingItem);
                    break;

            }
            i++;
        }
        Console.Write("\n \n \n \n");
        StorePurchase(store);
    }

    private static void StorePurchase(Room store)
    {
        Console.WriteLine("Choose an item you'd like to purchase by number, or press 0 to exit the store");
        int i;
        try
        {
            i = Convert.ToInt32(Console.ReadKey());
            if (i > store.storeCosts.Count + 1)
            {
                Console.WriteLine("Please enter a valid number");
                Thread.Sleep(3000);
                StoreRoomUi(store);
            }
            else
            {
                if (i != 0) PurchaseItem(store, i);
                else RoomNavigation();
            }
        }
        catch
        {
            Console.WriteLine("Please enter a valid number");
            Thread.Sleep(3000);
            StoreRoomUi(store);
        }
    }

    private static void RoomNavigation()
    {
        Console.Clear();
        Console.WriteLine("You may:");
        int i = 0;
        List<Direction> CurrentOptions = [];
        if (RunningDungeon?.coordMap[RunningDungeon.North] is not null)
        {
            Console.WriteLine(i + ") Go North");
            CurrentOptions.Add(Direction.north);
        }
        if (RunningDungeon?.coordMap[RunningDungeon.East] is not null)
        {
            Console.WriteLine(i + ") Go East");
            CurrentOptions.Add(Direction.east);
        }
        if (RunningDungeon?.coordMap[RunningDungeon.South] is not null)
        {
            Console.WriteLine(i + ") Go South");
            CurrentOptions.Add(Direction.south);
        }
        if (RunningDungeon?.coordMap[RunningDungeon.West] is not null)
        {
            Console.WriteLine(i + ") Go West");
            CurrentOptions.Add(Direction.west);
        }
        try
        {
            int roomChoice = Convert.ToInt32(Console.ReadKey());

        }
        catch
        {

        }
    }

    private static void PurchaseItem(Room store, int itemToBuy)
    {
        if (RunningDungeon?.currentPlayer.Gold > store.storeCosts[itemToBuy].cost)
        {
            RunningDungeon.currentPlayer.Gold -= store.storeCosts[itemToBuy].cost;
            RunningDungeon.currentPlayer.items.Add(store.storeCosts[itemToBuy].item);
            store.storeCosts.Remove(store.storeCosts[itemToBuy]);
        }
        else
        {
            Console.WriteLine("It looks like you don't have enough gold to purchase that item!");
            Thread.Sleep(2000);
            StoreRoomUi(store);
        }
    }
}