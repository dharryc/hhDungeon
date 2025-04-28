
using System.Text.Json;

namespace hhDungeon;

public class Program
{
    public static string Greeting = "Welcome to our dungeon! This dungeon was made by Harry and Himni \nThis dungeon works such that there will be shops along your way. \nThere will also be many enemies. These enemies include the goblins, slimes, orcs (\"the goblins big brothers\"), Trolls, and *the LEGENDARY* Dragons\nThere will also be armor and weapons that you can equip.\nPress any key to continue";
    public static Dungeon? RunningDungeon;
    public static bool RunningGame = true;
    public static List<Items>? Inventory => RunningDungeon?.currentPlayer.items;
    // public static void Main()
    // {
    //     RunningDungeon = new(new Player(), 15, 1);
    //     GreetPlayer();
    // }

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
                    Console.WriteLine("You've entered a small shop! The items avalible to purchase are:");
                    StoreRoomUi(RunningDungeon.currentRoom);
                    break;
            }
        }
        // EndGame();
    }

    private static void EmptyRoomUi(Room currentRoom)
    {
        RoomNavigation();
    }

    private static void DisplayArmor(int i, (ItemType _type, Items item, int cost) workingItem)
    {
        Armor workingArmor = (Armor)workingItem.item;
        Console.WriteLine();
        Console.Write(i + "    ARMOR      |");
        switch (workingArmor.GetType())
        {
            case ArmorType.boots:
                Console.Write("    BOOTS     |");
                Console.Write("      " + workingItem.cost + "       |");
                Console.Write("        " + workingItem.item.Durability() + "           |");
                break;
            case ArmorType.chestplate:
                Console.Write(" CHESTPLATE   |");
                Console.Write("      " + workingItem.cost + "       |");
                Console.Write("         " + workingItem.item.Durability() + "          |");
                break;
            case ArmorType.helmet:
                Console.Write("    HELMET    |");
                Console.Write("      " + workingItem.cost + "       |");
                Console.Write("         " + workingItem.item.Durability() + "          |");
                break;
            case ArmorType.leggings:
                Console.Write("    PANTS     |");
                Console.Write("      " + workingItem.cost + "       |");
                Console.Write("        " + workingItem.item.Durability() + "           |");
                break;
        }

    }
    private static void DisplayWeapon(int i, (ItemType _type, Items item, int cost) workingItem)
    {

        //    ITEM TYPE   |   SUBTYPE   |   ITEM COST   |   ITEM DURABILITY   |
        //     WEAPON     |    SWORD    |      22       |        22           |
        Weapon workingWeapon = (Weapon)workingItem.item;
        Console.WriteLine();
        Console.Write(i + "    WEAPON     |");
        switch (workingWeapon.Get_Type())
        {

            case WeaponType.club:
                Console.Write("    CLUB      |");
                Console.Write("      " + workingItem.cost + "       |");
                Console.Write("        " + workingItem.item.Durability() + "          |");
                break;
            case WeaponType.dagger:
                Console.Write("   DAGGER     |");
                Console.Write("      " + workingItem.cost + "       |");
                Console.Write("        " + workingItem.item.Durability() + "          |");
                break;
            case WeaponType.rib:
                Console.Write("     RIB     |");
                Console.Write("      " + workingItem.cost + "       |");
                Console.Write("      " + workingItem.item.Durability() + "           |");
                break;
            case WeaponType.shortsword:
                Console.Write(" SHORTSWORD   |");
                Console.Write("      " + workingItem.cost + "       |");
                Console.Write("        " + workingItem.item.Durability() + "          |");
                break;
            case WeaponType.sword:
                Console.Write("    SWORD     |");
                Console.Write("      " + workingItem.cost + "       |");
                Console.Write("        " + workingItem.item.Durability() + "          |");
                break;
        }
    }
    private static void DisplayPotion(int i, (ItemType _type, Items item, int cost) workingItem)
    {
        //    ITEM TYPE   |   SUBTYPE   |   ITEM COST   |   ITEM DURABILITY   |
        //     POTION     | DEFENCE UP  |      22       |        22           |
        Potion workingPotion = (Potion)workingItem.item;
        Console.WriteLine();
        Console.Write(i + "    POTION     |");
        switch (workingPotion.effect)
        {
            case Effects.regeneration:
                Console.Write(" REGENERATION |");
                Console.Write("      " + workingItem.cost + "       |");
                Console.Write("        " + workingItem.item.Durability() + "           |");
                break;
            case Effects.strength:
                Console.Write("  STRENGTH    |");
                Console.Write("      " + workingItem.cost + "       |");
                Console.Write("        " + workingItem.item.Durability() + "           |");
                break;
            case Effects.defenseBoost:
                Console.Write(" DEFENCE UP   |");
                Console.Write("      " + workingItem.cost + "       |");
                Console.Write("        " + workingItem.item.Durability() + "           |");
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

    public static void DisplayInventoryItem(Items? item, int i)
    {

        switch (item?.TypeOfItem)
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
                Console.WriteLine("{2})      ARMOR     | {0}  |        {1}          |", armor.TypeOfArmor, armor.Durability(), i);
                break;
        }
    }
    private static void InventoryUI()
    {
        Console.Clear();
        if (Inventory?.Count > 0)
        {
            int i = 0;
            Console.WriteLine("    ITEM TYPE   |   SUBTYPE   |   ITEM DURABILITY   |");
            foreach (var item in Inventory)
            {
                DisplayInventoryItem(item, i);
                i++;
            }
            Console.WriteLine("To equip or use an item, select it by number");
            var selectedItem = Console.ReadKey();
            if (char.IsDigit(selectedItem.KeyChar))
            {
                EquipOrConsume(int.Parse(selectedItem.KeyChar.ToString()));
            }
            else
            {
                Console.WriteLine("It looks like your inventory is empty right now!");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
        }
    }
    private static void EquipOrConsume(int itemChoice)
    {
        Console.Clear();
        Console.WriteLine("The item you've chosen to use is:");
        DisplayInventoryItem(Inventory?[itemChoice], itemChoice);
        Console.WriteLine("\n Is this correct (y/n)?");
        var confirm = Console.ReadKey();
        if (char.IsDigit(confirm.KeyChar))
        {
            if (confirm.KeyChar == 'n' || confirm.KeyChar == 'N')
            {
                InventoryUI();
            }
            else
            {

            }
        }
        else
        {
            Console.WriteLine("Please enter y/n");
            Thread.Sleep(1000);
            EquipOrConsume(itemChoice);
        }
    }

    private static void PurchaseItem(Room store, int itemToBuy)
    {
        if (RunningDungeon?.currentPlayer.Gold > store.storeCosts[itemToBuy].cost)
        {
            RunningDungeon.currentPlayer.Gold -= store.storeCosts[itemToBuy].cost;
            Inventory?.Add(store.storeCosts[itemToBuy].item);
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