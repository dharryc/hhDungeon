namespace hhDungeon;

public class FightingUI
{
    private string EnemyFightInstructions = "";
    public static string GetEnimyList(Dungeon dungeon)
    {
        string enimies = string.Empty;
        foreach (var enemy in dungeon.currentRoom.enemies)
        {
            enimies += enemy.GetType().ToString();
            if (enemy != dungeon.currentRoom.enemies.Last())
            {
                enimies += " and ";
            }
        }
        return enimies;
    }

    public static void FightEnimies(Dungeon dungeon)
    {
        bool enemiesAlive = true;
        while (enemiesAlive)
        {

            Console.WriteLine(BuildEnemyStringFormat(dungeon));
            Console.WriteLine("Which Enemy do you want to atack?");
            int choice = GetChoice(dungeon.currentRoom.enemies.Count());
            dungeon.currentRoom.enemies[choice - 1].TakeDamage((int)dungeon.currentPlayer.Attack());
            enemiesAlive = AreEnimiesAlive(dungeon);

            AttackPlayer(dungeon);

        }

        LootRoom(dungeon);
    }

    public static void LootRoom(Dungeon dungeon)
    {
        Console.WriteLine(GetRoomLootInToString(dungeon.currentRoom));
        Console.WriteLine("do you wish to grab the loot, if so, press 1 and enter otherwise press enter");
        bool ShouldContinue = Console.ReadLine() == "1";
        if (ShouldContinue)
        {
            GrabLootFromRoom(dungeon);
            dungeon.currentRoom.MakeEmpty();
        }
    }

    public static string GetRoomLootInToString(Room room)
    {
        string Loot = "Type of Item  |  Durability  | Size";
        if (room.enemies is not null)
        {

            int Gold = 0;
            foreach (var enimy in room.enemies)
            {
                Gold += enimy.goldFromKill;
                foreach (var loot in enimy.ViewLoot())
                {
                    Loot = "\n " + loot.GetType().Name ;
                    for (int i = loot.GetType().ToString().Length; i < 15; i++)
                    {
                        Loot += " ";
                    }
                    Loot += "|   " + loot.Durability();
                    for (int i = loot.Durability().ToString().Length; i < 15; i++)
                    {
                        Loot += " ";
                    }
                    Loot += "|   " + loot.GetSize();

                }
            }
        }
        return Loot;
    }

    public static void GrabLootFromRoom(Dungeon dungeon)
    {
        foreach (var emeny in dungeon.currentRoom.enemies)
        {
            List<Items> itemsToGrab = emeny.GrabLoot();
            foreach (var item in itemsToGrab)
            {

                dungeon.currentPlayer.items.Add(item);
            }
        }
    }

    public static void AttackPlayer(Dungeon dungeon)
    {
        foreach (var Enimy in dungeon.currentRoom.enemies)
        {
            dungeon.currentPlayer.CurrentHealth -= Enimy.Attack(dungeon.currentPlayer.MaxHealth, dungeon.currentPlayer.CurrentLevel, dungeon.DifficultyLevel);
        }
    }


    public static bool AreEnimiesAlive(Dungeon dungeon)
    {
        int enimiesDead = 0;
        foreach (var emeny in dungeon.currentRoom.enemies)
        {
            if (emeny.GetHealth() < 0)
            {
                enimiesDead++;
            }
        }
        return enimiesDead == dungeon.currentRoom.enemies.Count();

    }

    public static string BuildEnemyStringFormat(Dungeon dungeon)
    {
        string ToReturn = "There are " + dungeon.currentRoom.enemies.Count() + " enemies in this room";
        foreach (var enemy in dungeon.currentRoom.enemies)
        {
            ToReturn += "\n" + enemy.GetType().ToString();
            for (int i = enemy.GetType().ToString().ToCharArray().Length; i < 12;)
            {
                ToReturn += " ";
            }
            ToReturn += "| " + enemy.GetHealth();
        }
        return ToReturn;

    }

    public static string GetEnemyList(Dungeon dungeon)
    {
        throw new NotImplementedException();
    }

    public static int GetChoice(int n = 1)
    {
        int choice;
        int.TryParse(Console.ReadLine(), out choice);
        if (choice <= n && n > 0)
        {
            return choice;
        }
        else
        {
            Console.WriteLine("Please enter a number between 1 and " + n);
            return GetChoice(n);
        }
    }
}
