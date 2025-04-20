namespace hhDungeon;

public class Program
{
    public static string Greeting = "Welcome to our dungeon this dungeon was made by Harry and Himni \nThis dungeon works such that there will be shops along your way. \nThere will also be many enimies these enimies include the goblins, slimes, orcs (\"the goblins big brothers\"), Trolls, and the lengendary Dragons\nThere will also be armor and weapons that you can equip.\nPress any key to continue";
    Player player;
    Dungeon dungeon;
    public void Main(string[] args)
    {
        dungeon = Dungeon.LoadGame();
        player = dungeon.currentPlayer;
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
        switch (dungeon.currentRoom.type)
        {
            case RoomType.empty:
                return GetAvailableDoors();
                break;
            case RoomType.store:
                GetStoreItems();
                break;
            case RoomType.enemy:
                return FightingUI.GetEnemyList(dungeon);
                break;
        }
    }

    public string GetAvailableDoors()
    {

        string availableDoors = "The available doors are ";

        if (dungeon.currentRoom.DoorLinks.NorthRoom is not null)
        {
            availableDoors += "North ";
        }
        if (dungeon.currentRoom.DoorLinks.EastRoom is not null)
        {
            availableDoors += "East ";
        }
        if (dungeon.currentRoom.DoorLinks.SouthRoom is not null)
        {
            availableDoors += "South ";
        }
        if (dungeon.currentRoom.DoorLinks.WestRoom is not null)
        {
            availableDoors += "West ";
        }

        return availableDoors;
    }

    public string GetStoreItems()
    {
        dungeon.currentRoom.
    }
}