using System.Reflection.Metadata.Ecma335;

namespace hhDungeon;
public class Dungeon
{
    static int baseDif = 1;
    Dictionary<(int x, int y), Room> coordMap;
    int DifficultyLevel;
    Player currentPlayer;
    Room currentRoom;
    int RoomsExplored;
    int MaxRooms;
    public enum direction
    {
        north,
        south,
        east,
        west,
    }
    public Dungeon(Player? player)
    {
        coordMap = [];
        DifficultyLevel = baseDif;
        currentRoom = new Room();
        currentPlayer = player ?? new Player();
    }
    public Room MoveRooms(direction Direction)
    {
        Room returnRoom = new();
        Direction switch
        {
            direction.east => returnRoom = new Room(),
            
        }
        return new Room();
    }
}