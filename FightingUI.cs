using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        Console.WriteLine(BuildEnemyStringFormat(dungeon));

    }

    public static string BuildEnemyStringFormat(Dungeon dungeon)
    {
        string ToReturn = "There are " + dungeon.currentRoom.enemies.Count() + " enemies in this room, they are ";
        foreach (var enemy in dungeon.currentRoom.enemies)
        {
            ToReturn += enemy.GetType().ToString() + " at " + enemy.GetHealth() + " health";
        }
        return ToReturn;

    }}
