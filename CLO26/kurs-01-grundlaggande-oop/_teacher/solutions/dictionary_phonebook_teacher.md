# Lärarfacit — Telefonkatalog med Dictionary

## Lösning

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Dictionary<string, string> phonebook = new Dictionary<string, string>();

        AddContact(phonebook, "Anna Andersson", "070-1234567");
        AddContact(phonebook, "Erik Eriksson", "070-2345678");
        AddContact(phonebook, "Maria Larsson", "070-3456789");
        AddContact(phonebook, "Johan Johansson", "070-4567890");

        Console.WriteLine("=== TELEFONKATALOG ===");
        ShowAllContacts(phonebook);
        SearchContact(phonebook, "Maria Larsson");
        SearchContact(phonebook, "Nils Nilsson");
        UpdateContact(phonebook, "Anna Andersson", "070-9999999");
        RemoveContact(phonebook, "Erik Eriksson");
        ShowAllContacts(phonebook);
        ShowStats(phonebook);
    }

    static void AddContact(Dictionary<string, string> phonebook, string name, string phone)
    {
        if (phonebook.ContainsKey(name))
            Console.WriteLine($"{name} finns redan. Använd UpdateContact för att ändra numret.");
        else
            phonebook[name] = phone;
    }

    static void SearchContact(Dictionary<string, string> phonebook, string name)
    {
        Console.WriteLine($"Söker efter {name}...");
        if (phonebook.TryGetValue(name, out string phone))
            Console.WriteLine($"{name}: {phone}");
        else
            Console.WriteLine("Kontakt ej hittad.");
        Console.WriteLine();
    }

    static void UpdateContact(Dictionary<string, string> phonebook, string name, string newPhone)
    {
        if (phonebook.ContainsKey(name))
        {
            phonebook[name] = newPhone;
            Console.WriteLine($"{name}s nummer uppdaterat till {newPhone}");
        }
        else
            Console.WriteLine($"{name} finns inte i katalogen.");
        Console.WriteLine();
    }

    static void RemoveContact(Dictionary<string, string> phonebook, string name)
    {
        if (phonebook.Remove(name))
            Console.WriteLine($"{name} har tagits bort från katalogen.");
        else
            Console.WriteLine($"{name} finns inte i katalogen.");
        Console.WriteLine();
    }

    static void ShowAllContacts(Dictionary<string, string> phonebook)
    {
        Console.WriteLine("\nAlla kontakter:");
        foreach (var contact in phonebook.OrderBy(kvp => kvp.Key))
            Console.WriteLine($"{contact.Key}: {contact.Value}");
        Console.WriteLine();
    }

    static void ShowStats(Dictionary<string, string> phonebook)
    {
        Console.WriteLine("Statistik:");
        Console.WriteLine($"Antal kontakter: {phonebook.Count}");
    }
}
```

## Pedagogisk poäng

`TryGetValue` är att föredra framför `ContainsKey` + indexering i `SearchContact` — det är en atomär operation och undviker race conditions i flertrådad kod. För den här kursnivån är det främst en stilfråga, men bra vana.

`phonebook.Remove(name)` returnerar `bool` — visa att man kan använda returvärdet direkt i if-satsen, det är idiomatisk C#.

Vanlig fallgrop: att loopa med `foreach` och försöka modifiera Dictionary:n inuti loopen — det ger `InvalidOperationException`. `Remove` ska göras utanför en aktiv foreach-loop.
