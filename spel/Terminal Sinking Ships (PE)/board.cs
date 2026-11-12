using Spectre.Console;

class Board
{
    public string[,] board = new string[10, 10];
    public List <Ship> ships = new List <Ship>();

    public (bool validShot, bool hit, bool sunk) Missile (int x, int y)
    {
        bool sunk = false; 
        string status = board[x,y];
        (int x, int y) cordinats = (x + 1, y + 1);

        if (status == "x"||status == "o")
        {
            Console.WriteLine($"This cordinats ({cordinats}) was already shot at");
            return (false,false,false);
        }
        else if (status is null)
        {
            board[x,y] = "o";
            Console.WriteLine($"Miss at cordinats ({cordinats})");
            return (true,false, false);
        }
        else if(status == "s")
        {
            board[x,y] = "x";
            Console.WriteLine($"HIT at cordinats ({cordinats})");
            for (int i =0; i<ships.Count();i++)
            {
                for (int j =0; j<ships[i].positions.Count();j++)
                {
                    if(ships[i].positions[j].row == x && ships[i].positions[j].column == y)
                    {
                        sunk = ships[i].hit();
                    }
                }
                
            }
            return (true,true,sunk);
        }
            return (false,false,false);
        
    }


    public bool AllShipsSunk()
    {
        for (int i =0; i<ships.Count();i++)
        {
            if (ships[i].hits < ships[i].length)
            return false;
        }
        return true;
    }

    // This method built with ai, hade an easier method earlier but replace it with this because i was occupied with gamelogic(can be found in earlier commit).
    // The ai also choose "Spectre.Console", but was nice to this minimal project.

    public Table BuildBoard(bool myView)
    {
        Table table = new Table();

        table.AddColumn(new TableColumn("[bold]#[/]"));

        for (int column = 1; column <= 10; column++)
        {
            table.AddColumn(
                new TableColumn($"[bold]{column}[/]").Centered()
            );
        }

        for (int row = 0; row < 10; row++)
        {
            List<string> cells = new List<string>();

            char rowLetter = (char)('A' + row);

            cells.Add($"[bold]{rowLetter}[/]");

            for (int column = 0; column < 10; column++)
            {
                string status = board[row, column];

                if (status == "x")
                {
                    // Träff ska alltid synas
                    cells.Add("[red]X[/]");
                }
                else if (status == "o")
                {
                    // Miss ska alltid synas
                    cells.Add("[grey]O[/]");
                }
                else if (status == "s" && myView)
                {
                    // Visa bara egna skepp
                    cells.Add("[green]■[/]");
                }
                else
                {
                    // Vatten ELLER dolt fiendeskepp
                    cells.Add("[blue]~[/]");
                }
            }
            table.AddRow(cells.ToArray());
        }
        return table;
    }


    public bool CheckBoard(int x, int y, Ship ship,Direction direction)
    {
        List <(int x,int y)> cordinats = new List <(int x, int y)>();
        if(direction == Direction.Horizontal)
        {
            for (int i = 0; i < ship.length; i++) 
            {
                int newY = y-i;
                if (newY < 0 || newY >= 10)
                {
                    Console.WriteLine("Ship was placed outside board");
                    return false;
                }
                if(board[x,newY] != null)
                {
                    Console.WriteLine($"the spot{(x,newY)} was taken");
                    return false;
                }
                cordinats.Add((x,newY));
            }
        }
        else if(direction == Direction.Vertical)
        {
            for (int i = 0; i < ship.length; i++)
            {
                int newX = x-i;
                if (newX < 0 || newX >= 10)
                {
                    Console.WriteLine("Ship was placed outside board");
                    return false;
                } 
                if(board[newX,y] !=null)
                    {
                        Console.WriteLine($"the spot{(newX,y)} was taken");
                        return false;
                    }
                cordinats.Add((newX,y));
            }
        }
        for (int i =0; i < cordinats.Count;i++)
            {
                ship.ShipCordinats(cordinats[i].x,cordinats[i].y);
                board[cordinats[i].x,cordinats[i].y] = "s";
            }
        return true;
    }

    public Board()
    {
        ships.Add(new Ship("Carrier", 5));
        ships.Add(new Ship("Battleship", 4));
        ships.Add(new Ship("Cruiser", 3));
        ships.Add(new Ship("Submarine", 3));
        ships.Add(new Ship("Destroyer", 2));
    }   
}