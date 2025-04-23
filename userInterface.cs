
namespace hhDungeon;

public class Program
{
    public static string Greeting = "Welcome to our dungeon this dungeon was made by Harry and Himni \nThis dungeon works such that there will be shops along your way. \nThere will also be many enimies these enimies include the goblins, slimes, orcs (\"the goblins big brothers\"), Trolls, and the lengendary Dragons\nThere will also be armor and weapons that you can equip.\nPress any key to continue";
    Player player;
    public static Dungeon RunningDungeon;
    public static bool RunningGame;
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

    public void StartDungeon()
    {
        string Instructions = "Welcome to the first room your available options are currently" + GetCurrentOptions();
    }


    public string GetCurrentOptions()
    {
        switch (RunningDungeon.currentRoom.type)
        {
            case RoomType.empty:
                return GetAvailableDoors();
            case RoomType.store:
                GetStoreItems();
                break;
            case RoomType.enemy:
                return FightingUI.GetEnemyList(RunningDungeon);
        }
        return "wheee";
    }

    private void GetStoreItems()
    {
        throw new NotImplementedException();
    }

    private string GetAvailableDoors()
    {
        throw new NotImplementedException();
    }
    public static void MainGameLoop()
    {
        while (RunningGame)
        {
            switch (RunningDungeon.currentRoom.type)
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
                    StoreRoomUi(RunningDungeon.currentRoom);
                    break;
            }
        }
        EndGame();
    }

    private static void StoreRoomUi(Room store)
    {
        Console.Clear();
        Console.WriteLine("You've entered a small shop! The items avalible to purchase are:");
        Console.WriteLine("   ITEM TYPE   |   ITEM COST   |   ");
        foreach (var workingItem in store.storeCosts)
        {
            switch (workingItem._type)
            {
                case ItemType.armor:
                    Armor workingArmor = (Armor)workingItem.item;
                    Console.WriteLine("A ");
                    switch (workingArmor.GetType())
                    {
                        case ArmorType.boots:
                            Console.Write("pair of Boots that costs ");
                            Console.Write(workingItem.cost);
                            Console.Write("gold");
                            break;
                        case ArmorType.chestplate:
                            Console.Write("Chestplate that costs ");
                            Console.Write(workingItem.cost);
                            Console.Write("gold");
                            break;
                        case ArmorType.helmet:
                            Console.Write("Helmet that costs ");
                            Console.Write(workingItem.cost);
                            Console.Write("gold");
                            break;
                        case ArmorType.leggings:
                            Console.Write("set of Leggings that costs ");
                            Console.Write(workingItem.cost);
                            Console.Write("gold");
                            break;
                        }
                    break;
                case ItemType.potion:
                    Potion workingPotion = (Potion)workingItem.item;
                    Console.WriteLine("A ");
                    switch (workingPotion.effect)
                    {
                        case Effects.:
                            Console.Write("pair of Boots that costs ");
                            Console.Write(workingItem.cost);
                            Console.Write("gold");
                            break;
                        case ArmorType.chestplate:
                            Console.Write("Chestplate that costs ");
                            Console.Write(workingItem.cost);
                            Console.Write("gold");
                            break;
                        case ArmorType.helmet:
                            Console.Write("Helmet that costs ");
                            Console.Write(workingItem.cost);
                            Console.Write("gold");
                            break;
                        case ArmorType.leggings:
                            Console.Write("set of Leggings that costs ");
                            Console.Write(workingItem.cost);
                            Console.Write("gold");
                            break;
                        }
                    break;
                case ItemType.weapon:
                    break;

            }
        }
    }
}