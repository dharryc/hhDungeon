using System.Text.Json;

namespace hhDungeon;

public class Tests
{
    [SetUp]
    public void Setup()
    {
        Program.GreetPlayer();
    }

    [Test]
    public void Test1()
    {
        Setup();
    }
}
