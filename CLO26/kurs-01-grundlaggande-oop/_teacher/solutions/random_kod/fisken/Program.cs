// Lösning: fisken.md
Random slump = new Random();
Console.Clear();
Console.CursorVisible = false;

int färgval = slump.Next(0, 3);
switch (färgval)
{
    case 0:
        Console.ForegroundColor = ConsoleColor.Cyan;
        break;
    case 1:
        Console.ForegroundColor = ConsoleColor.Green;
        break;
    default:
        Console.ForegroundColor = ConsoleColor.Magenta;
        break;
}

int centerX   = 38;
int centerY   = 13;
int halvLängd = 25;
int halvHöjd  = 5;
int totalLängd = halvLängd * 2;

// Övre kroppen: sin(0→π) → böjer uppåt från noll till noll
for (int t = 0; t <= totalLängd; t++)
{
    int x = centerX - halvLängd + t;
    int y = centerY - (int)(Math.Sin(t * Math.PI / totalLängd) * halvHöjd);
    Console.SetCursorPosition(x, y);
    Console.Write("~");
}

// Undre kroppen: spegelbild nedåt
for (int t = 0; t <= totalLängd; t++)
{
    int x = centerX - halvLängd + t;
    int y = centerY + (int)(Math.Sin(t * Math.PI / totalLängd) * halvHöjd);
    Console.SetCursorPosition(x, y);
    Console.Write("~");
}

// Huvud (höger — där kurvan möts igen vid t=totalLängd)
Console.SetCursorPosition(centerX + halvLängd + 1, centerY);
Console.Write(">");

// Stjärt (vänster — där kurvan möts vid t=0)
Console.SetCursorPosition(centerX - halvLängd - 3, centerY - 2);
Console.Write("\\");
Console.SetCursorPosition(centerX - halvLängd - 2, centerY - 1);
Console.Write("\\");
Console.SetCursorPosition(centerX - halvLängd - 2, centerY + 1);
Console.Write("/");
Console.SetCursorPosition(centerX - halvLängd - 3, centerY + 2);
Console.Write("/");

// Bubblor (slumpmässiga, ovanför fisken)
Console.ForegroundColor = ConsoleColor.White;
for (int i = 0; i < 15; i++)
{
    int bx = slump.Next(centerX, centerX + halvLängd + 3);
    int by = slump.Next(2, centerY - halvHöjd - 1);
    if (by >= 1 && bx < Console.WindowWidth - 1)
    {
        Console.SetCursorPosition(bx, by);
        Console.Write("o");
    }
}

Console.ResetColor();
Console.CursorVisible = true;
Console.SetCursorPosition(0, Console.WindowHeight - 2);
Console.WriteLine("[tryck Enter]");
Console.ReadLine();
Console.ResetColor();
Console.Clear();
