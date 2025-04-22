using System.Text.Json;
namespace hhDungeon;
public enum Direction
{
    north, south, east, west,
}

public enum Effects
{
    strength, weakness, defenseBoost, poison, defenseDown, regeneration, weakness2, weakness3,
}
public class Dungeon(Player? player, int firstFloorSize, int baseDifficulty)
{
    public Dictionary<(int x, int y), Room> coordMap = [];
    int X = 0;
    int Y = 0;
    public (int x, int y) North => (X, Y + 1);
    public (int x, int y) South => (X, Y - 1);
    public (int x, int y) East => (X + 1, Y);
    public (int x, int y) West => (X - 1, Y);
    int DifficultyLevel = baseDifficulty;
    public Player currentPlayer = player ?? new Player();
    public Room currentRoom = new(RoomType.empty);
    int RoomsExplored;
    readonly int MaxRooms = firstFloorSize;
    bool seenStairs = false;

    public Room MoveRooms(Direction direction)
    {
        switch (direction)
        {
            case Direction.east:
                if (coordMap.TryGetValue(East, out Room? returnRoom))
                {
                    X += 1;
                    return returnRoom;
                }
                else
                {
                    RoomsExplored += 1;
                    Room nextRoom = new(DifficultyLevel, seenStairs, MaxRooms > RoomsExplored);
                    coordMap.Add(East, nextRoom);
                    X += 1;
                    seenStairs = nextRoom.type == RoomType.stair;
                    return nextRoom;
                }
            case Direction.west:
                if (coordMap.TryGetValue(West, out returnRoom))
                {
                    X -= 1;
                    return returnRoom;
                }
                else
                {
                    RoomsExplored += 1;
                    Room nextRoom = new(DifficultyLevel, seenStairs, MaxRooms > RoomsExplored);
                    coordMap.Add(West, nextRoom);
                    X -= 1;
                    seenStairs = nextRoom.type == RoomType.stair;
                    return nextRoom;
                }
            case Direction.north:
                if (coordMap.TryGetValue(North, out returnRoom))
                {
                    Y += 1;
                    return returnRoom;
                }
                else
                {
                    RoomsExplored += 1;
                    Room nextRoom = new(DifficultyLevel, seenStairs, MaxRooms > RoomsExplored);
                    coordMap.Add(North, nextRoom);
                    Y += 1;
                    seenStairs = nextRoom.type == RoomType.stair;
                    return nextRoom;
                }
            case Direction.south:
                if (coordMap.TryGetValue(South, out returnRoom))
                {
                    Y -= 1;
                    return returnRoom;
                }
                else
                {
                    RoomsExplored += 1;
                    Room nextRoom = new(DifficultyLevel, seenStairs, MaxRooms > RoomsExplored);
                    coordMap.Add(South, nextRoom);
                    Y -= 1;
                    seenStairs = nextRoom.type == RoomType.stair;
                    return nextRoom;
                }
        }
        return new Room(RoomType.empty);
    }
    public void SaveGame(List<Dungeon>? savedGames)
    {
        if (!File.Exists("./savedGames")) File.Create("./savedGames");
        savedGames ??= [];
        savedGames.Add(this);
        File.WriteAllText("./savedGames", JsonSerializer.Serialize(savedGames));
    }

    public static Dungeon LoadGame()
    {
        if (!File.Exists("./savedGames"))
        {
            Dungeon savedDungeon = JsonSerializer.Deserialize<Dungeon>(File.ReadAllText("./savedGames"));
            if (savedDungeon != null) return savedDungeon;
        }
        return new Dungeon(new Player(), 50, 1);
    }
}