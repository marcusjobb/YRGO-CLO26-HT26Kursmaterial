# Lärarfacit — Strängmetoder

## Lösning

```csharp
class StringMethods
{
    static int CountVowels(string text)
    {
        int count = 0;
        string vowels = "aeiouåäö";
        foreach (char c in text.ToLower())
            if (vowels.Contains(c)) count++;
        return count;
    }

    static string Reverse(string text)
    {
        string reversed = "";
        for (int i = text.Length - 1; i >= 0; i--)
            reversed += text[i];
        return reversed;
    }

    static bool IsPalindrome(string text) => text == Reverse(text);

    static string ToTitleCase(string text)
    {
        if (text.Length == 0) return text;
        return char.ToUpper(text[0]) + text.Substring(1);
    }

    static void Main()
    {
        Console.WriteLine($"\"programmering\" har {CountVowels("programmering")} vokaler");
        Console.WriteLine($"\"hejsan\" baklänges: {Reverse("hejsan")}");
        Console.WriteLine($"Är \"racecar\" ett palindrom? {IsPalindrome("racecar")}");
        Console.WriteLine($"Är \"hejsan\" ett palindrom? {IsPalindrome("hejsan")}");
        Console.WriteLine($"\"välkommen\" med stor bokstav: {ToTitleCase("välkommen")}");
    }
}
```

## Pedagogisk poäng

`IsPalindrome` återanvänder `Reverse` — ett tydligt exempel på att metoder kan anropa varandra.
Lyfta det som ett designval: en metod gör en sak, och andra metoder kombinerar dem.

Vanlig fallgrop i `CountVowels`: att glömma `å`, `ä`, `ö` eller att inte köra `.ToLower()` → stora vokaler räknas inte.
