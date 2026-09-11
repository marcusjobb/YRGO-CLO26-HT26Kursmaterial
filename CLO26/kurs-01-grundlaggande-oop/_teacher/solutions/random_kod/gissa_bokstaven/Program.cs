// Lösning: gissa_bokstaven.md
Random slump = new Random();
char hemligBokstav = (char)('A' + slump.Next(0, 26));
int antalFörsök = 0;
bool rätt = false;

Console.WriteLine("Jag tänker på en stor bokstav — kan du gissa?");

while (!rätt)
{
    Console.Write("\nDin gissning: ");
    char gissning = Console.ReadLine()!.ToUpper()[0];
    antalFörsök++;

    if (gissning == hemligBokstav)
    {
        Console.WriteLine($"Rätt! Bokstaven var {hemligBokstav}. Det tog {antalFörsök} försök.");
        rätt = true;
    }
    else if (gissning < hemligBokstav)
    {
        Console.WriteLine("Senare i alfabetet!");
    }
    else
    {
        Console.WriteLine("Tidigare i alfabetet!");
    }
}
