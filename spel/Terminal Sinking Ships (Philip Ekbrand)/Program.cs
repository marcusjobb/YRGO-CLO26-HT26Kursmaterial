using Spectre.Console;
AnsiConsole.MarkupLine("[green]Spectre fungerar![/]");


Console.Clear();

Console.WriteLine("Helllo! \n\n its time to play shinkships!");

Console.WriteLine("Please write youre name:");
string input = Console.ReadLine();

Console.WriteLine($"Hello {input}! lets play!");

Game game = new Game(input,"bot");

for(int i =0;i< game.players.Count;i++)
    {
        game.PlayerPlacement(game.players[i]);
    }

game.game_status(GameState.Playing);
game.startGame();


