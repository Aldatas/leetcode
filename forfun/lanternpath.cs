using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

class Info
{
    public string Title { get; set; }
    public string Description { get; set; }

    public Info(string title, string description)
    {
        Title = title;
        Description = description;
    }
}

class Player
{
    Info info = new Info("Player", "The Hero of this story");
    public int Health  { get; set; }
    public int Mana    { get; set; }
    public List<Spell> Spells { get; set; }
}

class Spell
{
    Info info = new Info("Spell", "N/A");
    public int ManaCost { get; set; }
    public int Effect { get; set; }
    public bool IsAoe { get; set; }
}

class Dungeon
{
    Info info = new Info("Dungeon", "N/A");
    public DungeonRoom EntranceRoom { get; set; }
}

class DungeonRoom
{
    Info info = new Info("DungeonRoom", "N/A");
    public Dictionary<Direction, DungeonRoom> AdjacentRooms { get; set; } = new Dictionary<string, DungeonRoom>();
    public Fight Enemies { get; set; }
}

class Enemy
{
    Info info = new Info("Enemy", "N/A");
}

class Fight
{
    List<Enemy> enemies;
    // loot n recovery n stuff
}

enum Direction
{
    North,
    East,
    South,
    West
}

class Game
{
    private Dungeon dungeon;
    private DungeonRoom currentRoom;
    private Dictionary<char, Action> inputActions = new Dictionary<char, Action>();
    private Dictionary<char, Direction> keyToDirection = new Dictionary<char, Direction>
    {
        { 'w', Direction.North },
        { 'a', Direction.West },
        { 's', Direction.South },
        { 'd', Direction.East }
    };

    public Game(Dungeon dungeon)
    {
        this.dungeon = dungeon;
        currentRoom = dungeon.EntranceRoom;
        InitializeInputActions();
    }

    public void InitializeInputActions()
    {
        foreach (var kvp in keyToDirection)
        {
            inputActions[kvp.Key] = () => Move(currentRoom, kvp.Value);
        }
        inputActions['q'] = QuitGame;
    }

    private void Move(DungeonRoom room, Direction direction)
    {
        string directionKey = direction.ToString().ToLower();
        if (room.AdjacentRooms.ContainsKey(directionKey))
        {
            room.AdjacentRooms.TryGetValue(directionKey, room);
        }
        else
        {
            Console.WriteLine($"No room in {direction}.");
        }
    }

    private void QuitGame()
    {
        
    }

    public void RunGame()
    {
        bool gameRunning = true;
        string screen = "/// LanternPath ///";
        string screenSeparator = "\n--------------------------------\n";
        string inputKeyMapMenu = "s) Start Game\nq) Quit Game\n";
        string inputLine = "\n>> ";

        while (gameRunning)
        {
            Console.Write(screen + screenSeparator + screeninputKeyMapMenu + inputLine);
            var key = Console.ReadKey(true); // true to suppress the character from being printed
            char inputChar = key.KeyChar;

            if (inputActions.ContainsKey(inputChar))
            {
                inputActions[inputChar]();
            }
        }
    }
}


class Program
{
    static void Main()
    {
        Game game = new();
        game.RunGame();

        return;
    }
}