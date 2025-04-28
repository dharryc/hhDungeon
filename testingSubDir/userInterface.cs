namespace hhTestDungeon;

public class Program
{
    public static string Greeting = "Welcome to our dungeon! This dungeon was made by Harry and Himni \nThis dungeon works such that there will be shops along your way. \nThere will also be many enemies. These enemies include the goblins, slimes, orcs (\"the goblins big brothers\"), Trolls, and *the LEGENDARY* Dragons\nThere will also be armor and weapons that you can equip.\nPress any key to continue";
    public static Dungeon RunningDungeon;
    public static bool RunningGame = true;
    public static List<Items>? Inventory => RunningDungeon.currentPlayer.items;
    public static Player CurrentPlayer => RunningDungeon.currentPlayer;
    public static void Main()
    {
        var o = new Player();
        o.Gold = 50000;
        Console.Clear();
        RunningDungeon = new(o, 15, 1);
        GreetPlayer();
    }

    public static void GreetPlayer()
    {
        Console.WriteLine(Greeting);
        Console.ReadKey();
        Console.Clear();
        MainGameLoop();
    }

    public static void MainGameLoop()
    {
        while (RunningGame)
        {
            switch (RunningDungeon?.currentRoom.type)
            {
                case RoomType.empty:
                    EmptyRoomUi(RunningDungeon.currentRoom);
                    break;
                case RoomType.loot:
                    //loot room stuff
                    break;
                case RoomType.enemy:
                    FightingUI.FightEnimies(RunningDungeon);
                    break;
                case RoomType.stair:
                    //stair stuff
                    break;
                case RoomType.store:
                    Console.Clear();
                    StoreRoomUi(RunningDungeon.currentRoom);
                    break;
            }
        }
        // EndGame();
    }
    private static void RoomNavigation()
    {
        Console.WriteLine("You may:");
        Console.WriteLine(1 + ") Go North");
        Console.WriteLine(2 + ") Go East");
        Console.WriteLine(3 + ") Go South");
        Console.WriteLine(4 + ") Go West");
        Console.WriteLine(5 + ") View Inventory");
        if (RunningDungeon.currentRoom.type == RoomType.store) Console.WriteLine(6 + ") Re-enter store");
        try
        {
            var keyPressed = Console.ReadKey();
            int roomChoice = 0;
            if (char.IsDigit(keyPressed.KeyChar))
            {
                roomChoice = int.Parse(keyPressed.KeyChar.ToString());
                switch (roomChoice)
                {
                    case 1:
                        RunningDungeon?.MoveRooms(Direction.north);
                        break;
                    case 2:
                        RunningDungeon?.MoveRooms(Direction.east);
                        break;
                    case 3:
                        RunningDungeon?.MoveRooms(Direction.south);
                        break;
                    case 4:
                        RunningDungeon?.MoveRooms(Direction.west);
                        break;
                    case 5:
                        InventoryUI();
                        break;
                    case 6:
                        if (RunningDungeon.currentRoom.type == RoomType.store)
                        {
                            Console.Clear();
                            StoreRoomUi(RunningDungeon.currentRoom);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Please choose a valid option");
                            RoomNavigation();
                        }
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Please choose a valid option");
                        RoomNavigation();
                        break;

                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Please choose a valid option");
                RoomNavigation();
            }
        }
        catch
        {
            Console.Clear();
            RoomNavigation();
        }
    }

    private static void EmptyRoomUi(Room currentRoom)
    {
        Console.WriteLine("You're in an empty room");
        RoomNavigation();
    }

    private static void DisplayArmor(int i, (ItemType _type, Items item, int cost) workingItem)
    {
        Armor workingArmor = (Armor)workingItem.item;
        Console.WriteLine();
        Console.Write(i + "|   ARMOR      |");
        switch (workingArmor.TypeOfArmor)
        {
            case ArmorType.boots:
                Console.Write("    BOOTS     |");
                Console.Write("      " + workingItem.cost + "       |");
                Console.Write("        " + workingItem.item.Durability + "           |");
                break;
            case ArmorType.chestplate:
                Console.Write(" CHESTPLATE   |");
                Console.Write("      " + workingItem.cost + "       |");
                Console.Write("        " + workingItem.item.Durability + "           |");
                break;
            case ArmorType.helmet:
                Console.Write("    HELMET    |");
                Console.Write("      " + workingItem.cost + "       |");
                Console.Write("        " + workingItem.item.Durability + "           |");
                break;
            case ArmorType.leggings:
                Console.Write("   LEGGINGS   |");
                Console.Write("      " + workingItem.cost + "       |");
                Console.Write("        " + workingItem.item.Durability + "           |");
                break;
        }

    }
    private static void DisplayWeapon(int i, (ItemType _type, Items item, int cost) workingItem)
    {

        //    ITEM TYPE   |   SUBTYPE   |   ITEM COST   |   ITEM DURABILITY   |
        //     WEAPON     |    SWORD    |      22       |        22           |
        Weapon workingWeapon = (Weapon)workingItem.item;
        Console.WriteLine();
        Console.Write(i + "|   WEAPON     |");
        switch (workingWeapon.TypeOfWeapon)
        {

            case WeaponType.club:
                Console.Write("    CLUB      |");
                Console.Write("      " + workingItem.cost + "       |");
                Console.Write("        " + workingItem.item.Durability + "           |");
                break;
            case WeaponType.dagger:
                Console.Write("   DAGGER     |");
                Console.Write("      " + workingItem.cost + "       |");
                Console.Write("        " + workingItem.item.Durability + "           |");
                break;
            case WeaponType.rib:
                Console.Write("     RIB      |");
                Console.Write("      " + workingItem.cost + "       |");
                Console.Write("        " + workingItem.item.Durability + "           |");
                break;
            case WeaponType.shortsword:
                Console.Write(" SHORTSWORD   |");
                Console.Write("      " + workingItem.cost + "       |");
                Console.Write("        " + workingItem.item.Durability + "           |");
                break;
            case WeaponType.sword:
                Console.Write("    SWORD     |");
                Console.Write("      " + workingItem.cost + "       |");
                Console.Write("        " + workingItem.item.Durability + "           |");
                break;
        }
    }
    private static void DisplayPotion(int i, (ItemType _type, Items item, int cost) workingItem)
    {
        //    ITEM TYPE   |   SUBTYPE   |   ITEM COST   |   ITEM DURABILITY   |
        //     POTION     | DEFENCE UP  |      22       |        22           |
        Potion workingPotion = (Potion)workingItem.item;
        Console.WriteLine();
        Console.Write(i + "|   POTION     |");
        switch (workingPotion.effect)
        {
            case Effects.regeneration:
                Console.Write(" REGENERATION |");
                Console.Write("      " + workingItem.cost + "       |");
                Console.Write("        " + workingItem.item.Durability + "           |");
                break;
            case Effects.strength:
                Console.Write("  STRENGTH    |");
                Console.Write("      " + workingItem.cost + "       |");
                Console.Write("        " + workingItem.item.Durability + "           |");
                break;
            case Effects.defenseBoost:
                Console.Write(" DEFENCE UP   |");
                Console.Write("      " + workingItem.cost + "       |");
                Console.Write("        " + workingItem.item.Durability + "           |");
                break;
        }
    }
    private static void StoreRoomUi(Room store)
    {
        if (store.storeCosts.Count == 0)
        {
            Console.WriteLine("It looks like this store is empty!\nPress any key to continue");
            Console.ReadKey();
            Console.Clear();
            RoomNavigation();
        }
        else
        {
            Console.WriteLine("You've entered a small shop! The items avalible to purchase are:");
            Console.WriteLine("#| ITEM TYPE    |   SUBTYPE   |   ITEM COST   |   ITEM DURABILITY  |");
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
            Console.WriteLine("\n");
            StorePurchase(store);
        }
    }

    private static void StorePurchase(Room store)
    {
        Console.WriteLine("Choose an item you'd like to purchase by number, or press 0 to exit the store");
        try
        {
            var i = Convert.ToInt32(Console.ReadKey().KeyChar.ToString());
            if (i != -1 && i != 0 && i <= store.storeCosts.Count) PurchaseItem(store, i - 1);
            else if (i == 0)
            {
                Console.Clear();
                RoomNavigation();
            }
            else
            {
                Console.Clear();
                StoreRoomUi(store);
            }
        }
        catch
        {
            Console.Clear();
            StoreRoomUi(store);
        }
    }


    public static void DisplayInventoryItem(Items? item, int i)
    {
        switch (item?.TypeOfItem)
        {
            case ItemType.armor:
                DisplayArmor(i, (item.TypeOfItem, item, 0));
                break;
            case ItemType.potion:
                DisplayPotion(i, (item.TypeOfItem, item, 0));
                break;
            case ItemType.weapon:
                DisplayWeapon(i, (item.TypeOfItem, item, 0));
                break;
        }
    }
    private static void InventoryUI()
    {
        Console.Clear();
        if (Inventory?.Count > 0)
        {
            int i = 0;
            Console.WriteLine("    ITEM TYPE     |   SUBTYPE   |   ITEM DURABILITY   |");
            foreach (var item in Inventory)
            {
                DisplayInventoryItem(item, i);
                i++;
            }
            Console.WriteLine();
            Console.WriteLine("You currently have {0:n0} Gold", CurrentPlayer.Gold);
            Console.WriteLine("To use or equip, select an item by number, then press enter, or press e to exit");
            var selectedItem = Console.ReadLine();
            try
            {
                if (selectedItem?.ToString() == "e" || selectedItem?.ToString() == "E") RoomNavigation();
                else EquipOrConsume(int.Parse(selectedItem.ToString()));
            }
            catch
            {
                InventoryUI();
            }
        }
        else
        {
            Console.WriteLine("It looks like your inventory is empty. Press any key to continue");
            Console.ReadKey();
            Console.Clear();
        }
    }
    private static void EquipOrConsume(int itemChoice)
    {
        var itemToUse = Inventory?[itemChoice];
        Console.Clear();
        Console.WriteLine("The item you've chosen to use is:");
        DisplayInventoryItem(itemToUse, itemChoice);
        Console.WriteLine("\n Is this correct (y/n)?");
        var confirm = Console.ReadKey();
        if (char.IsAscii(confirm.KeyChar))
        {
            if (confirm.KeyChar == 'n' || confirm.KeyChar == 'N')
            {
                InventoryUI();
            }
            else if (confirm.KeyChar == 'y' || confirm.KeyChar == 'Y')
            {
                switch (itemToUse?.TypeOfItem)
                {
                    case ItemType.armor:
                        EquipArmor(itemToUse);
                        break;
                    case ItemType.potion:
                        drinkPotion(itemToUse);
                        break;
                    case ItemType.weapon:
                        EquipWeapon(itemToUse);
                        break;
                }
            }
        }
        else
        {
            EquipOrConsume(itemChoice);
        }
    }

    private static void EquipWeapon(Items itemToUse)
    {
        CurrentPlayer.EquippedWeapon = (Weapon)itemToUse;
    }

    private static void drinkPotion(Items itemToUse)
    {
        Potion potion = (Potion)itemToUse;
        CurrentPlayer.currentEffects.Add((potion.effect, potion.duration));
    }

    private static void EquipArmor(Items itemToUse)
    {
        var armor = (Armor)itemToUse;
        switch (armor.TypeOfArmor)
        {
            case ArmorType.boots:
                CurrentPlayer.boots = (armor, armor.Durability);
                Console.Clear();
                Console.WriteLine("You've equipped the Boots! Press any key to continue");
                Console.ReadKey();
                break;
            case ArmorType.helmet:
                CurrentPlayer.helmet = (armor, armor.Durability);
                Console.Clear();
                Console.WriteLine("You've equipped the Helmet! Press any key to continue");
                Console.ReadKey();
                break;
            case ArmorType.leggings:
                CurrentPlayer.leggings = (armor, armor.Durability);
                Console.Clear();
                Console.WriteLine("You've equipped the Leggings! Press any key to continue");
                Console.ReadKey();
                break;
            case ArmorType.chestplate:
                CurrentPlayer.chestplate = (armor, armor.Durability);
                Console.Clear();
                Console.WriteLine("You've equipped the Chestplate! Press any key to continue");
                Console.ReadKey();
                break;
        }
    }

    private static void PurchaseItem(Room store, int itemToBuy)
    {
        Console.Clear();
        if (CurrentPlayer.Gold >= store.storeCosts[itemToBuy].cost)
        {
            Console.WriteLine("Success!");
            Console.WriteLine("The " + store.storeCosts[itemToBuy].item.TypeOfItem.ToString().ToUpper() + " has been added to your inventory\nPress any key to continue");
            CurrentPlayer.Gold -= store.storeCosts[itemToBuy].cost;
            Inventory?.Add(store.storeCosts[itemToBuy].item);
            store.storeCosts.Remove(store.storeCosts[itemToBuy]);
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("It looks like you don't have enough gold to purchase that item!");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            Console.Clear();
            StoreRoomUi(store);
        }
    }
}