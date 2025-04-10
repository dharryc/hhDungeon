namespace hhDungeon;
public enum Direction
{
    north,
    south,
    east,
    west,
}

public record Coordinate { (int x, int y) }
public class Dungeon
{
    static int baseDif;
    Dictionary<(int x, int y), Room> coordMap;
    int DifficultyLevel;
    Player currentPlayer;
    Room currentRoom;
    int RoomsExplored;
    int MaxRooms;
    public Dungeon(Player? player, int firstFloorSize, int baseDifficulty)
    {
        coordMap = [];
        DifficultyLevel = baseDif;
        currentRoom = new Room();
        currentPlayer = player ?? new Player();
        MaxRooms = firstFloorSize;
        baseDif = baseDifficulty;
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
        if(!File.Exists("./savedGames")) File.Create("./savedGames");
        if(savedGames is null) savedGames = new List<Dungeon>();
        savedGames.Add(this);
        
    }
}