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
public record Coordinate
{
    public int x; public int y;
    public Coordinate(int X, int Y)
    {
        x = X;
        y = Y;
    }
}
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
        bool lastRoom = RoomsExplored < MaxRooms;
        if (!lastRoom)
        {
            switch (direction)
            {
                case Direction.east:
                    if (currentRoom.EastRoom is not null) return currentRoom.EastRoom;
                    else
                    {
                        RoomsExplored += 1;
                        Room nextRoom = new Room((currentRoom.X + 1, currentRoom.Y), DifficultyLevel);
                        coordMap.Add(nextRoom.Coordinate, nextRoom);
                        return nextRoom;
                    }
                case Direction.west:
                    if (currentRoom.WestRoom is not null) return currentRoom.WestRoom;
                    else
                    {
                        RoomsExplored += 1;
                        Room nextRoom = new Room((currentRoom.X - 1, currentRoom.Y), DifficultyLevel);
                        coordMap.Add(nextRoom.Coordinate, nextRoom);
                        return nextRoom;
                    }
                case Direction.north:
                    if (currentRoom.NorthRoom is not null) return currentRoom.NorthRoom;
                    else
                    {
                        RoomsExplored += 1;
                        Room nextRoom = new Room((currentRoom.X, currentRoom.Y + 1), DifficultyLevel);
                        coordMap.Add(nextRoom.Coordinate, nextRoom);
                        return nextRoom;
                    }
                case Direction.south:
                    if (currentRoom.SouthRoom is not null) return currentRoom.SouthRoom;
                    else
                    {
                        RoomsExplored += 1;
                        Room nextRoom = new Room((currentRoom.X, currentRoom.Y - 1), DifficultyLevel);
                        coordMap.Add(nextRoom.Coordinate, nextRoom);
                        return nextRoom;
                    }
            }
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