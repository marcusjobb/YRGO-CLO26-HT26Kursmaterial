// Lösning: ja_nej_kanske.md
Random slump = new Random();
int val = slump.Next(0, 3);

Console.WriteLine("🎱 Den magiska bollen säger:");

switch (val)
{
    case 0:
        Console.WriteLine("Ja, absolut!");
        break;
    case 1:
        Console.WriteLine("Nej, definitivt inte.");
        break;
    default:
        Console.WriteLine("Kanske... det beror på.");
        break;
}
