namespace hhDungeon;

public class Tests
{
    [SetUp]
    public void Setup()
    {
        Program.RunningDungeon = new Dungeon(new Player(), 15, 5);
        Program program = new();
        program.GreetPlayer();
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
}
