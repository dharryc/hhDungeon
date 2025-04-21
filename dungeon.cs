using System.Security.Cryptography.X509Certificates;
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
public class Dungeon
{
    public Dictionary<(int x, int y), Room> coordMap;
    int X = 0;
    int Y = 0;
    public (int x, int y) north => (X, Y + 1);
    public (int x, int y) south => (X, Y - 1);
    public (int x, int y) east => (X + 1, Y);
    public (int x, int y) west => (X - 1, Y);
    int DifficultyLevel;
    public Player currentPlayer;
    public Room currentRoom;
    int RoomsExplored;
    int MaxRooms;
    bool seenStairs = false;
    public Dungeon(Player? player, int firstFloorSize, int baseDifficulty)
    {
        coordMap = [];
        DifficultyLevel = baseDifficulty;
        currentRoom = new Room(RoomType.empty);
        currentPlayer = player ?? new Player();
        MaxRooms = firstFloorSize;
    }
    public Room MoveRooms(Direction direction)
    {
        switch (direction)
        {
            case Direction.east:
                if (coordMap.ContainsKey(east))
                {
                    Room returnRoom = coordMap[east];
                    X += 1;
                    return returnRoom;
                }
                else
                {
                    RoomsExplored += 1;
                    Room nextRoom = new(DifficultyLevel, seenStairs, MaxRooms > RoomsExplored);
                    coordMap.Add(east, nextRoom);
                    X += 1;
                    seenStairs = nextRoom.type == RoomType.stair;
                    return nextRoom;
                }
            case Direction.west:
                if (coordMap.ContainsKey(west))
                {
                    Room returnRoom = coordMap[west];
                    X -= 1;
                    return returnRoom;
                }
                else
                {
                    RoomsExplored += 1;
                    Room nextRoom = new(DifficultyLevel, seenStairs, MaxRooms > RoomsExplored);
                    coordMap.Add(west, nextRoom);
                    X -= 1;
                    seenStairs = nextRoom.type == RoomType.stair;
                    return nextRoom;
                }
            case Direction.north:
                if (coordMap.ContainsKey(north))
                {
                    Room returnRoom = coordMap[north];
                    Y += 1;
                    return returnRoom;
                }
                else
                {
                    RoomsExplored += 1;
                    Room nextRoom = new(DifficultyLevel, seenStairs, MaxRooms > RoomsExplored);
                    coordMap.Add(north, nextRoom);
                    Y += 1;
                    seenStairs = nextRoom.type == RoomType.stair;
                    return nextRoom;
                }
            case Direction.south:
                if (coordMap.ContainsKey(south))
                {
                    Room returnRoom = coordMap[south];
                    Y -= 1;
                    return returnRoom;
                }
                else
                {
                    RoomsExplored += 1;
                    Room nextRoom = new(DifficultyLevel, seenStairs, MaxRooms > RoomsExplored);
                    coordMap.Add(south, nextRoom);
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