
namespace hhDungeon;

public class Program
{
    public static string Greeting = "Welcome to our dungeon! This dungeon was made by Harry and Himni \nThis dungeon works such that there will be shops along your way. \nThere will also be many enemies. These enemies include the goblins, slimes, orcs (\"the goblins big brothers\"), Trolls, and *the LEGENDARY* Dragons\nThere will also be armor and weapons that you can equip.\nPress any key to continue";
    Player? player;
    public static Dungeon? RunningDungeon;
    public static bool RunningGame = true;
    public static List<Items> Inventory => RunningDungeon.currentPlayer.items;
    public static void Main()
    {
        RunningDungeon = new(new Player(), 15, 1);
        GreetPlayer();
    }

    public static void GreetPlayer()
    {
        Console.WriteLine(Greeting);
        Console.ReadKey();
        MainGameLoop();
    }

    public static void MainGameLoop()
    {
        while (RunningGame)
        {
            switch (RunningDungeon?.currentRoom.type)
            {
                case RoomType.empty:
                    Console.WriteLine("You're in an empty Room");
                    Thread.Sleep(2000);
                    EmptyRoomUi(RunningDungeon.currentRoom);
                    break;
                case RoomType.loot:
                    Console.WriteLine("You're in a loot Room");
                    Thread.Sleep(2000);
                    // LootRoomUi(RunningDungeon.currentRoom);
                    break;
                case RoomType.enemy:
                    Console.WriteLine("You're in an enemy Room");
                    Thread.Sleep(2000);
                    // EnemyRoomUi(RunningDungeon.currentRoom);
                    break;
                case RoomType.stair:
                    Console.WriteLine("You're in a stair Room");
                    Thread.Sleep(2000);
                    // StairRoomUi(RunningDungeon.currentRoom);
                    break;
                case RoomType.store:
                    Console.Clear();
                    Console.WriteLine("You've entered a small shop! The items avalible to purchase are:");
                    StoreRoomUi(RunningDungeon.currentRoom);
                    break;
            }
        }
        // EndGame();
    }

    private static void EmptyRoomUi(Room currentRoom)
    {
        Console.WriteLine("AAAAA");
        Thread.Sleep(2000);
        RoomNavigation();
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
                Console.Clear();
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
            Console.Clear();
            StoreRoomUi(store);
        }
    }

    private static void RoomNavigation()
    {
        Console.Clear();
        Console.WriteLine("You may:");
        Console.WriteLine(1 + ") Go North");
        Console.WriteLine(2 + ") Go East");
        Console.WriteLine(3 + ") Go South");
        Console.WriteLine(4 + ") Go West");
        Console.WriteLine(5 + ") View Inventory");
        try
        {
            var keyPressed = Console.ReadKey();
            int roomChoice = 1;
            if (char.IsDigit(keyPressed.KeyChar))
            {
                roomChoice = int.Parse(keyPressed.KeyChar.ToString());
                switch (roomChoice)
                {
                    case 1:
                        RunningDungeon.currentRoom = RunningDungeon.MoveRooms(Direction.north);
                        break;
                    case 2:
                        Console.Write("youdidmakeithere");
                        RunningDungeon.currentRoom = RunningDungeon.MoveRooms(Direction.east);
                        break;
                    case 3:
                        RunningDungeon.currentRoom = RunningDungeon.MoveRooms(Direction.south);
                        break;
                    case 4:
                        RunningDungeon.currentRoom = RunningDungeon.MoveRooms(Direction.west);
                        break;
                    case 5:
                        InventoryUI();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Please choose a valid option");
                Thread.Sleep(500);
                RoomNavigation();
            }
        }
        catch
        {
            Console.WriteLine("Please choose a valid option");
            Thread.Sleep(500);
            RoomNavigation();
        }
    }

    private static void InventoryUI()
    {
        if (Inventory.Count > 0)
        {
            int i = 0;
            Console.WriteLine("    ITEM TYPE   |   SUBTYPE   |   ITEM DURABILITY   |");
            foreach (var item in Inventory)
            {
                switch (item._type)
                {
                    case ItemType.potion:
                        var potion = (Potion)item;
                        Console.WriteLine("{2})     POTION     | {0}  |        {1}          |", potion.effect, potion.duration, i);
                        break;
                    case ItemType.weapon:
                        var weapon = (Weapon)item;
                        Console.WriteLine("{2})     WEAPON     | {0}  |        {1}          |", weapon.Get_Type(), weapon.Durability(), i);
                        break;
                    case ItemType.armor:
                        var armor = (Armor)item;
                        Console.WriteLine("{2})      ARMOR     | {0}  |        {1}          |", armor._Type, armor.Durability(), i);
                        break;
                }
                i++;
            }
            Console.WriteLine("To equip or use an item, select it by number");
            var selectedItem = Console.ReadKey();
        }
    }

    private static void PurchaseItem(Room store, int itemToBuy)
    {
        if (RunningDungeon?.currentPlayer.Gold > store.storeCosts[itemToBuy].cost)
        {
            RunningDungeon.currentPlayer.Gold -= store.storeCosts[itemToBuy].cost;
            Inventory.Add(store.storeCosts[itemToBuy].item);
            store.storeCosts.Remove(store.storeCosts[itemToBuy]);
        }
        else
        {
            Console.WriteLine("It looks like you don't have enough gold to purchase that item!");
            Console.WriteLine("Press any key to continue");
            Console.Clear();
            StoreRoomUi(store);
        }
    }
}