using System.Text.Json;

namespace hhDungeon;

public class Tests
{
    [SetUp]
    public void Setup()
    {
        List<Room> rooms = [];
        for (int i = 0; i < 200; i++)
        {
            rooms.Add(new Room(1, false, false));
        }
        foreach (var i in rooms) Console.WriteLine(Convert.ToString(i.type));
        foreach (var i in rooms)
        {
            if (i.type == RoomType.store)
            {
                foreach (var b in i.storeCosts) Program.DisplayInventoryItem(b.item, 1);
            }
        }
    }

    [Test]
    public void Test1()
    {
        Setup();
    }
}
