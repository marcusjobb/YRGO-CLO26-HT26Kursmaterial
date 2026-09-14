// Vad gör det här programmet:
//   Visar hur "Extract Method"-refaktorering fungerar i praktiken.
//   En upprepad kodsekvens bryts ut till en egen metod.
//
// Koncept som visas:
//   - void-metod utan parametrar
//   - Extract Method (Refactoring-menyn i Rider/VS Code)
//   - DRY — kod som anropas på flera ställen ändras på ett

// Prova själv:
//   1. Skriv Console.Write("-") i en loop direkt i Main — tre gånger
//   2. Markera ett av blocken
//   3. Högerklicka → Refactoring → Extract Method
//   4. Döp metoden till Linje — se hur IDE:n skapar metoden åt dig

Linje();
Console.WriteLine("Hello, Fulkod!");
Linje();

// Metoden skapar en horisontell linje med 40 streck.
// Console.Write (utan ln) skriver utan radbrytning — loopen bygger linjen tecken för tecken.
// Console.WriteLine() efteråt lägger till radbrytningen.
static void Linje()
{
    for (int x = 0; x < 40; x++)
        Console.Write("-");

    Console.WriteLine();
}
