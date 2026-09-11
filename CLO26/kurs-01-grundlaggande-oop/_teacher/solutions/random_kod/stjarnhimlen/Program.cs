// Lösning: stjarnhimlen.md
Random slump = new Random();
Console.Clear();
Console.CursorVisible = false;

int antalStjärnor = 80;

int meddelandeVal = slump.Next(0, 4);
string meddelande;
switch (meddelandeVal)
{
    case 0:
        meddelande = "✨ du är fantastisk ✨";
        break;
    case 1:
        meddelande = "🌟 solen lyser för dig 🌟";
        break;
    case 2:
        meddelande = "💫 drömmar tar tid, men de händer 💫";
        break;
    default:
        meddelande = "🌙 god natt, stjärnsamlare 🌙";
        break;
}
// Lägg till fler case-grenar och höj Next(0, X) för fler meddelanden

for (int i = 0; i < antalStjärnor; i++)
{
    int x = slump.Next(0, Console.WindowWidth - 1);
    int y = slump.Next(0, Console.WindowHeight - 4);

    int färgval = slump.Next(0, 3);
    switch (färgval)
    {
        case 0:
            Console.ForegroundColor = ConsoleColor.White;
            break;
        case 1:
            Console.ForegroundColor = ConsoleColor.Yellow;
            break;
        default:
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            break;
    }

    Console.SetCursorPosition(x, y);
    Console.Write("*");
}

int meddelandeX = (Console.WindowWidth - meddelande.Length) / 2;
int meddelandeY = Console.WindowHeight / 2;

Console.ForegroundColor = ConsoleColor.Cyan;
Console.SetCursorPosition(meddelandeX, meddelandeY);
Console.Write(meddelande);

Console.ResetColor();
Console.CursorVisible = true;
Console.SetCursorPosition(0, Console.WindowHeight - 2);
