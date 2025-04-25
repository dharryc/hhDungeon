using System.Text.Json;

namespace hhDungeon;

public class Tests
{
    [SetUp]
    public void Setup()
    {
        List<Room> rooms = [];
        for (int i = 0; i < 20; i++)
        {
            rooms.Add(new Room(1, false, false));
        }
        foreach (var i in rooms) Console.WriteLine(Convert.ToString(i.type));
        foreach (var i in rooms)
        {
            if (i.type == RoomType.enemy)
            {
                Dungeon dungeon = new Dungeon(new Player(), 15, 15);
                dungeon.currentRoom = i;
                FightingUI.FightEnimies(dungeon);
            }
        }
    }

    [Test]
    public void Test1()
    {
        Setup();
    }
}
