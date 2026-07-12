using System.Reflection.Metadata.Ecma335;

class Player
{
    private int health  { get; set; }
    private int mana    { get; set; } 
}

class Dungeon
{
    private string title { get; set; }
    private string description { get; set; }

    private DungeonRoom entranceRoom { get; set; }
}

class DungeonRoom
{
    private string title       { get; set; }
    private string description { get; set; }

    private DungeonRoom leftRoom { get; set; }
    private DungeonRoom rightRoom { get; set; }
    private DungeonRoom forwardRoom { get; set; }
    private DungeonRoom previousRoom { get; set; }
}

class Game
{
    private Dictionary<char, Action> inputActions = new Dictionary<char, Action>();

    public Game()
    {
        inputActions['w'] = MoveForward;
        inputActions['a'] = MoveLeft;
        inputActions['s'] = MoveBackward;
        inputActions['d'] = MoveRight;
        inputActions['q'] = QuitGame;
    }

    private void MoveForward()
    {
        
    }

    private void MoveLeft()
    {
        
    }

    private void MoveBackward()
    {
        
    }

    private void MoveRight()
    {
        
    }

    private void QuitGame()
    {
        
    }

    public void RunGame()
    {
        bool gameRunning = true;
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