using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hhDungeon;

internal class FightingUI
{
   public static string GetEnimyList(Dungeon dungeon)
    {
        string enimies = string.Empty;
        foreach( var enemy in dungeon.currentRoom.enemies )
        {
            enimies += enemy.GetType().ToString();
            if (enemy != dungeon.currentRoom.enemies.Last())
            {
                enimies += " and ";
            }
        }
        return enimies;
    }
}
