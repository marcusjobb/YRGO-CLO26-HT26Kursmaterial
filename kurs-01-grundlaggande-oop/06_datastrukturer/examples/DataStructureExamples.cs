// Exempel på datastrukturer i C#
// Modul 06 — Datastrukturer
// Visar: Array, List<T>, Dictionary<TKey, TValue> och enum

// Definierar ett enum för väderlek utanför klassen (global scope)
enum Väderlek
{
    Soligt,
    Molnigt,
    Regnigt,
    Snöigt
}

class DataStructureExamples
{
    static void Main()
    {
        // =============================================
        // ARRAY — fast storlek, indexbaserad åtkomst
        // =============================================
        Console.WriteLine("=== ARRAY ===");

        // En array med veckodagar (bara måndag–fredag)
        string[] veckodagar = { "Måndag", "Tisdag", "Onsdag", "Torsdag", "Fredag" };

        // Loopa med for-loop för att visa index och värde
        for (int i = 0; i < veckodagar.Length; i++)
        {
            Console.WriteLine($"  Dag {i + 1}: {veckodagar[i]}");
        }

        Console.WriteLine();

        // =============================================
        // ARRAY med matematik — summa och medelvärde
        // =============================================
        Console.WriteLine("=== ARRAY MED MATEMATIK ===");

        int[] poäng = { 42, 17, 88, 56, 73 };

        // Beräkna summa
        int summa = 0;
        for (int i = 0; i < poäng.Length; i++)
        {
            summa += poäng[i];
        }

        // Beräkna medelvärde (division ger double)
        double medelvärde = (double)summa / poäng.Length;

        Console.WriteLine($"  Poäng: {string.Join(", ", poäng)}");
        Console.WriteLine($"  Summa: {summa}");
        Console.WriteLine($"  Medelvärde: {medelvärde:F2}");

        Console.WriteLine();

        // =============================================
        // LIST<string> — dynamisk storlek
        // =============================================
        Console.WriteLine("=== LISTA ===");

        // Skapa en shoppinglista
        List<string> shoppinglista = new List<string>();

        // Lägg till varor med Add()
        shoppinglista.Add("Mjölk");
        shoppinglista.Add("Bröd");
        shoppinglista.Add("Ägg");
        shoppinglista.Add("Smör");
        shoppinglista.Add("Ost");

        Console.WriteLine($"  Antal varor från början: {shoppinglista.Count}");

        // Ta bort en vara med Remove()
        shoppinglista.Remove("Bröd");
        Console.WriteLine($"  Tog bort 'Bröd'. Antal varor kvar: {shoppinglista.Count}");

        // Kontrollera om en vara finns med Contains()
        bool harÄgg = shoppinglista.Contains("Ägg");
        Console.WriteLine($"  Innehåller listan 'Ägg'? {harÄgg}");

        // Loopa igenom listan med foreach
        Console.WriteLine("  Shoppinglista:");
        foreach (string vara in shoppinglista)
        {
            Console.WriteLine($"    - {vara}");
        }

        Console.WriteLine();

        // =============================================
        // DICTIONARY<string, int> — nyckel och värde
        // =============================================
        Console.WriteLine("=== DICTIONARY ===");

        // Skapa ett dictionary med highscores
        Dictionary<string, int> highscores = new Dictionary<string, int>();

        // Lägg till tre poster
        highscores["Anna"] = 9500;
        highscores["Björn"] = 7200;
        highscores["Carina"] = 11400;

        // Slå upp ett specifikt värde via nyckel
        int annasPoäng = highscores["Anna"];
        Console.WriteLine($"  Annas highscore: {annasPoäng}");

        // Loopa igenom alla poster med foreach
        Console.WriteLine("  Alla highscores:");
        foreach (KeyValuePair<string, int> post in highscores)
        {
            Console.WriteLine($"    {post.Key}: {post.Value} poäng");
        }

        Console.WriteLine();

        // =============================================
        // ENUM — begränsad uppsättning namngivna värden
        // =============================================
        Console.WriteLine("=== ENUM ===");

        // Tilldela ett enum-värde
        Väderlek dagensVäder = Väderlek.Regnigt;

        Console.WriteLine($"  Dagens väder: {dagensVäder}");

        // Använd switch för att reagera på värdet
        switch (dagensVäder)
        {
            case Väderlek.Soligt:
                Console.WriteLine("  Ta med solglasögon!");
                break;
            case Väderlek.Molnigt:
                Console.WriteLine("  Det är grått ute, men torrt.");
                break;
            case Väderlek.Regnigt:
                Console.WriteLine("  Ta med ett paraply!");
                break;
            case Väderlek.Snöigt:
                Console.WriteLine("  Klä dig varmt och ta på vinterskorna!");
                break;
        }

        Console.WriteLine();
        Console.WriteLine("--- Klart! ---");
    }
}
