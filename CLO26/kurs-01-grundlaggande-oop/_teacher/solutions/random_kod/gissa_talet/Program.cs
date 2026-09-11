// Lösning: gissa_talet.md
Random slump = new Random();
int hemligTal = slump.Next(1, 101);
int antalFörsök = 0;
bool rätt = false;

Console.WriteLine("Jag tänker på ett tal mellan 1 och 100.");

while (!rätt)
{
    Console.Write("\nDin gissning: ");
    int gissning = int.Parse(Console.ReadLine()!);
    antalFörsök++;

    if (gissning < hemligTal)
    {
        Console.WriteLine("För lågt!");
    }
    else if (gissning > hemligTal)
    {
        Console.WriteLine("För högt!");
    }
    else
    {
        Console.WriteLine($"Rätt! Det tog {antalFörsök} försök.");
        rätt = true;
    }
}
