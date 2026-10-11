using Spectre.Console;

class Game
{
    public List<Player>players = new List<Player>();  
    public GameState Status {get; private set;}

    
    public int getValidNumber()
    {
        while (true)
        {
            string input = Console.ReadLine();
            
            if(int.TryParse(input, out int number))
            { 
                if (number >= 1 && number <= 10)
                {
                    return number;
                }
            } 
            Console.WriteLine("Please enter a number between 1 and 10");
        }
    }

    public void startGame()
    {   
        List<(int x, int y)> botHits = new List<(int x, int y)>();
        List<(int x, int y)> botShots = new List<(int x, int y)>();
        List<(int x, int y)> botTargets = new List<(int x, int y)>();
        int turnCount = 0;
        while(Status == GameState.Playing)
        {
            int playersTurn = turnCount % 2;
            int Enemy = (turnCount+1) % 2;
            bool ShotIsValid = false;
            (bool validShot, bool hit, bool sunk) Shot;

            while (!ShotIsValid)
            {
                if (players[playersTurn].playerKind == PlayerType.Human)
                {
                    Console.Clear();
                    Table myBoard = players[playersTurn].board.BuildBoard(true);
                    Table enemyBoard = players[Enemy].board.BuildBoard(false);
                    var boards = new Columns(
                        new Panel(myBoard).Header("YOUR FLEET"),
                        new Panel(enemyBoard).Header("ENEMY WATERS")
                    );

                    AnsiConsole.Write(boards);
                    Console.WriteLine($"{players[playersTurn].name} Enter row:" );
                    int row = getValidNumber();
                    Console.WriteLine("And Column:");
                    int column = getValidNumber();
                    Shot = players[Enemy].board.Missile(row-1,column-1);
                    ShotIsValid = Shot.validShot;
                }

                else if (players[playersTurn].playerKind == PlayerType.Bot)
                    {
                        Random random = new Random();
                        int row; 
                        int column;
                       if (botTargets.Count == 0)
                        {
                            row = random.Next(0, 10);
                            column = random.Next(0, 10);
                        }
                        else
                        {
   
                            row = botTargets[0].x;
                            column = botTargets[0].y;

                            botTargets.RemoveAt(0);
                        }
                        if (botShots.Contains((row, column)))
                        {
                            continue;
                        }
                        botShots.Add((row, column));

                        Shot = players[Enemy].board.Missile(row, column);
                        
                        if (Shot.hit)
                        {
                            botHits.Add((row, column));
                            List<(int x, int y)> newTarges = GetBotTargets(row, column);
                            foreach (var target in newTarges)
                                {
                                    if (!botShots.Contains(target) && !botTargets.Contains(target))
                                    {
                                        botTargets.Add(target);
                                    }
                                }
                        }

                        
                        if (Shot.sunk)
                        {
                            botHits.Clear();
                            botTargets.Clear(); 
                        }
                        ShotIsValid = Shot.validShot;
                    }
        }
        bool noShips = players[Enemy].board.AllShipsSunk();                    
                    if (noShips)
                    {
                        game_status(GameState.GameOver);
                        Console.WriteLine("All ships are discoverd, thanks for playing");
                    }
         turnCount++;
        }
    }

    public void game_status(GameState Status)
    {
        this.Status = Status;
    }

    public List<(int x, int y)> GetBotTargets(int x,int y)
    {
        List<(int x, int y)> botTargets = new List<(int x, int y)>();

        List<(int x, int y)> directions = new List<(int x, int y)>
        {
            (1, 0),
            (-1, 0),
            (0, 1),
            (0, -1)
        };

        foreach (var item in directions)
        {
            int newX = x +item.x;
            int newY = y +item.y;
            if(newX >=0 && newX <10 &&  newY >=0 && newY <10)
                botTargets.Add((newX,newY));
        }

        return botTargets;
    } 


    public void PlayerPlacement(Player player)
    {
        if (player.playerKind == PlayerType.Human)
        {
            for (int i = 0; i < player.board.ships.Count; i++)
            {
                bool placed = false;
                while (!placed)
                {
                    Console.Clear();
                    Table myBoard = player.board.BuildBoard(true);
                    AnsiConsole.Write(myBoard);
                    Console.WriteLine(player.board.ships[i].name);
                    Console.WriteLine("Row:");
                    int row = getValidNumber();
                    Console.WriteLine("Column:");
                    int column = getValidNumber();
                    Console.WriteLine("Direction: \n 1 = Horizontal \n 2 =Vertical");
                    string directionInput = Console.ReadLine();
                    Direction direction;
                    if (directionInput == "1")
                        direction = Direction.Horizontal;
                    else
                        direction = Direction.Vertical;
                    placed = player.board.CheckBoard(row-1, column -1, player.board.ships[i], direction);

                }
            }
        }
        else if (player.playerKind == PlayerType.Bot)
        {
            Random random = new Random();

            for (int i = 0; i < player.board.ships.Count; i++)
            {
                bool placed = false;
                while (!placed)
                {
                    int row = random.Next(0, 10);
                    int column = random.Next(0, 10);
                    int dir = random.Next(1, 3);
                    Direction direction;
                    if (dir == 1)
                        direction = Direction.Horizontal;
                    else
                        direction = Direction.Vertical;
                    placed = player.board.CheckBoard(row, column , player.board.ships[i], direction);

                }
            }
        }
    }

 
    public Game(string player1, string player2)
    {
        players.Add(new Player(player1, PlayerType.Human));
        players.Add(new Player(player2, PlayerType.Bot ));
        this.Status = GameState.Placement;
    }
}


class Player
{
    public string name{get; private set;}
    public PlayerType playerKind{get;private set;}
    public Board board{get;private set;}
    
    public Player(string name, PlayerType playerKind)
    {
        this.name = name;
        this.playerKind = playerKind;
        this.board = new Board();
    }
}


enum GameState
{
    Placement,
    Playing,
    GameOver
}

enum PlayerType
{
    Human,
    Bot,
    Network
}