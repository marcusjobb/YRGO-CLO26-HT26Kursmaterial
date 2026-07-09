# Övning: Strängmetoder 🟡

I den här övningen ska du skapa metoder som arbetar med strängar.
Du får öva på att ta emot text som indata, bearbeta den och returnera ett resultat.

> Fastnar du i mer än 15 minuter? Fråga klassen → AI → Marcus. I den ordningen.

## Din uppgift

Skapa följande statiska metoder:

- `static int CountVowels(string text)` — räknar antalet vokaler i texten (a, e, i, o, u, å, ä, ö). Räkna både små och stora bokstäver.
- `static string Reverse(string text)` — returnerar strängen baklänges
- `static bool IsPalindrome(string text)` — returnerar `true` om texten är ett palindrom (läses likadant framifrån och bakifrån). Tips: använd `Reverse`.
- `static string ToTitleCase(string text)` — returnerar texten med första bokstaven som versal

Testa metoderna i `Main` med egna exempelvärden.

## Förväntad output

```
"programmering" har 5 vokaler
"hejsan" baklänges: nasjeh
Är "racecar" ett palindrom? True
Är "hejsan" ett palindrom? False
"välkommen" med stor bokstav: Välkommen
```

## Lösning

<details>
<summary>Visa lösning</summary>

```csharp
class StringMethods
{
    static int CountVowels(string text)
    {
        int count = 0;
        string vowels = "aeiouåäö";

        foreach (char character in text.ToLower())
        {
            if (vowels.Contains(character))
            {
                count++;
            }
        }

        return count;
    }

    static string Reverse(string text)
    {
        string reversed = "";

        for (int i = text.Length - 1; i >= 0; i--)
        {
            reversed = reversed + text[i];
        }

        return reversed;
    }

    static bool IsPalindrome(string text)
    {
        string reversed = Reverse(text);
        return text == reversed;
    }

    static string ToTitleCase(string text)
    {
        if (text.Length == 0)
        {
            return text;
        }

        return char.ToUpper(text[0]) + text.Substring(1);
    }

    static void Main()
    {
        string word1 = "programmering";
        int vowelCount = CountVowels(word1);
        Console.WriteLine("\"" + word1 + "\" har " + vowelCount + " vokaler");

        string word2 = "hejsan";
        string reversed = Reverse(word2);
        Console.WriteLine("\"" + word2 + "\" baklänges: " + reversed);

        string word3 = "racecar";
        bool isPalindrome1 = IsPalindrome(word3);
        Console.WriteLine("Är \"" + word3 + "\" ett palindrom? " + isPalindrome1);

        bool isPalindrome2 = IsPalindrome(word2);
        Console.WriteLine("Är \"" + word2 + "\" ett palindrom? " + isPalindrome2);

        string word4 = "välkommen";
        string titleCase = ToTitleCase(word4);
        Console.WriteLine("\"" + word4 + "\" med stor bokstav: " + titleCase);
    }
}
```

</details>
