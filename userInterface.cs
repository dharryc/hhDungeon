namespace hhDungeon;

public class Program
{
    public static string Greeting = "Welcome to our dungeon this dungeon was made by Harry and Himni \nThis dungeon works such that there will be shops along your way. \nThere will also be many enimies these enimies include the goblins, slimes, orcs (\"the goblins big brothers\"), Trolls, and the lengendary Dragons\nThere will also be armor and weapons that you can equip.";
    public void Main(string[] args)
    {
        Player player;
        Dungeon dungeon;

        if (args.Length > 0)
        {
            (player, dungeon) = LoadGame(args[0]);
        }
        else
        {
            WelcomeNewPlayer();
        }
    }

    public void WelcomeNewPlayer()
    {
        Console.WriteLine(Greeting);
    }

}
