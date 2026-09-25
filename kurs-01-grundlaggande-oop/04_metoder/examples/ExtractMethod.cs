// Vad gör det här programmet:
//   Visar hur "Extract Method"-refaktorering fungerar i praktiken.
//   En upprepad kodsekvens bryts ut till en egen metod.
//
// Koncept som visas:
//   - void-metod utan parametrar
//   - Extract Method (Refactoring-menyn i Rider/VS Code)
//   - DRY — kod som anropas på flera ställen ändras på ett

// ── Prova själv ──────────────────────────────────────────────────────────────
//
//   Steg 0: Skriv loopen direkt i Main tre gånger (utan metoden).
//           Markera sedan ett av blocken och följ stegen för din IDE nedan.
//
//   JetBrains Rider
//   ───────────────
//   Markera koden → högerklicka → Refactor → Extract Method
//   Kortkommando: Ctrl+Alt+M  (Windows/Linux)  |  ⌘⌥M  (Mac)
//   Rider föreslår ett namn och skapar metoden automatiskt.
//
//   Visual Studio Code  (med C# Dev Kit / C# extension)
//   ────────────────────────────────────────────────────
//   Markera koden → klicka på den gula glödlampan (💡) som dyker upp
//   eller tryck  Ctrl+.  (Windows/Linux)  |  ⌘.  (Mac)
//   Välj "Extract method" i listan.
//
// ─────────────────────────────────────────────────────────────────────────────

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
