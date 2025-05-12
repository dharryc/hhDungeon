namespace hhDungeon;

public class FightingUI
{
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
            dungeon
                .currentRoom.enemies[choice]
                .TakeDamage((int)dungeon.currentPlayer.Attack().damage);
            enemiesAlive = AreEnimiesAlive(dungeon);

            AttackPlayer(dungeon);
        }

        LootRoom(dungeon);
    }

    public static void LootRoom(Dungeon dungeon)
    {
        Console.WriteLine(GetRoomLootInToString(dungeon.currentRoom));
        Console.WriteLine(
            "do you wish to grab the loot, if so, press 1 and enter otherwise press enter"
        );
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
            foreach (var enemy in room.enemies)
            {
                Gold += enemy.GoldFromKill;
                foreach (var loot in enemy.Potential_Loot)
                {
                    Loot += loot.TypeOfItem.ToString() + " and ";
                }
            }
        }
        return Loot;
    }

    public static void GrabLootFromRoom(Dungeon dungeon)
    {
        foreach (var emeny in dungeon.currentRoom.enemies)
        {
            List<Items> itemsToGrab = emeny.Potential_Loot;
            foreach (var item in itemsToGrab)
            {
                if (item is not null)
                {
                    dungeon.currentPlayer.items.Add(item);
                }
            }
        }
    }

    public static void AttackPlayer(Dungeon dungeon)
    {
        foreach (var Enimy in dungeon.currentRoom.enemies)
        {
            dungeon.currentPlayer.CurrentHealth -= Enimy.Attack(
                dungeon.currentPlayer.MaxHealth,
                dungeon.currentPlayer.CurrentLevel,
                dungeon.DifficultyLevel
            );
        }
    }

    public static bool AreEnimiesAlive(Dungeon dungeon)
    {
        int enimiesDead = 0;
        foreach (var emeny in dungeon.currentRoom.enemies)
        {
            if (emeny.GetHealth() <= 0)
            {
                enimiesDead++;
            }
        }
        return enimiesDead == dungeon.currentRoom.enemies.Count();
    }

    public static string BuildEnemyStringFormat(Dungeon dungeon)
    {
        string ToReturn =
            "There are " + dungeon.currentRoom.enemies.Count() + " enemies in this room";
        int enemyNum = 0;
        foreach (var enemy in dungeon.currentRoom.enemies)
        {
            ToReturn += "\n" + enemyNum + ") " + enemy.GetType().ToString();
            enemyNum++;
            for (int i = enemy.GetType().ToString().ToCharArray().Length; i < 12; i++)
            {
                ToReturn += " ";
            }
            ToReturn += "| " + enemy.GetHealth();
        }
        return ToReturn;
    }

    public static int GetChoice(int n)
    {
        while (true)
        {
            int choice;
            int.TryParse(Console.ReadLine(), out choice);
            if (choice <= n && n > 0)
            {
                return choice;
            }
            Console.WriteLine("Please enter a number between 1 and " + n);
        }
    }
}
