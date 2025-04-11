using System.Text.Json;

namespace hhDungeon;
public enum Direction
{
    north, south, east, west,
}
public enum RoomType
{
    enemy, loot, store, stair, empty,
}

public enum Effects
{
    strength, weakness, defenseBoost, healthBoost, poison, defenseDown, healthDown, attackDown, regeneration,
}
public record Coordinate { int x; int y; }
public class Dungeon
{
    Dictionary<Coordinate, Room> coordMap;
    int DifficultyLevel;
    Player currentPlayer;
    Room currentRoom;
    int RoomsExplored;
    int MaxRooms;
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
        RoomsExplored += 1;
        switch (direction)
        {
            case Direction.east:
                return new Room((currentRoom.x, currentRoom.y), RoomsExplored, DifficultyLevel, );
            case Direction.west:
                return new Room((currentRoom.x, currentRoom.y), RoomsExplored, DifficultyLevel, );
            case Direction.north:
                return new Room((currentRoom.x, currentRoom.y), RoomsExplored, DifficultyLevel, );
            case Direction.south:
                return new Room((currentRoom.x, currentRoom.y), RoomsExplored, DifficultyLevel, );
        }
        return new Room();
    }
    public void SaveGame(List<Dungeon>? savedGames)
    {
        if (!File.Exists("./savedGames")) File.Create("./savedGames");
        savedGames ??= [];
        savedGames.Add(this);
        File.WriteAllText("./savedGames", JsonSerializer.Serialize(savedGames));
    }
}