// Vad gör det här programmet:
//   Fortsättning på Extract Method — visar vad som händer när den markerade koden
//   använder en lokal variabel. IDE:n ser det och gör variabeln till en parameter.
//
// Koncept som visas:
//   - Extract Method skapar parametrar automatiskt från lokala variabler
//   - break avslutar loopen — inte metoden
//   - return (i en void-metod) avslutar hela metoden direkt

// ── Prova själv ──────────────────────────────────────────────────────────────
//
//   Steg 0: Skriv loopen direkt i Main med variabeln length.
//   Steg 1: Markera hela loopen (inklusive Console.WriteLine).
//   Steg 2: Kör Extract Method.
//
//   IDE:n ser att loopen använder 'length' och gör den till en parameter
//   i den nya metoden — du behöver inte göra det manuellt.
//
//   JetBrains Rider:   Ctrl+Alt+M  (Win/Linux)  |  ⌘⌥M  (Mac)
//   Visual Studio Code: Ctrl+.      (Win/Linux)  |  ⌘.    (Mac)  → Extract method
//
// ─────────────────────────────────────────────────────────────────────────────

int length = 70;

Linje(length);
Console.WriteLine("Hello, World!");

// Ritar en linje med 'length' antal streck.
// break och return gör INTE samma sak:
//   break  — avslutar loopen, metoden fortsätter efter for-blocket
//   return — avslutar hela metoden omedelbart (ingenting efter körs)
static void Linje(int length)
{
    for (int i = 0; i < length; i++)
    {
        if (i > 5) break;   // loopen stannar vid 6 tecken — metoden lever vidare

        Console.Write("-");
    }
    Console.WriteLine();    // den här raden körs tack vare break, inte return
}
