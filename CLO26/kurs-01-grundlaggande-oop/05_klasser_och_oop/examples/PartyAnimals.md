# Exempel — Party Animals (utan arv)

Det här exemplet fungerar — men det har tydliga problem.  
Läs igenom det och fundera på vad som upprepas och vad som saknas.

> Jämför med `DjurArv.md` för att se vad arv löser.

---

## Program.cs

```csharp
Console.WriteLine("Hello, Party Animals!");

// Tre olika sätt att skapa objekt och sätta Name — alla fungerar
Vovve vovve = new() { Name = "Pixi" };       // object initializer

Kattskrälle katt = new();
katt.Name = "Misse";                          // sätt property efteråt

Kanin nin = new Kanin();
nin.Name = "Nosferatu";                       // klassiskt new + tilldelning

vovve.Hello();
vovve.Play();

katt.Hello();
// katt.Play() — finns inte! Katten kan inte leka

nin.Hello();
nin.Play();
```

---

## Klasserna

```csharp
// Tre separata klasser — ingen gemensam grund
// Name och Hello() upprepas i varje klass (copy-paste)

class Vovve
{
    public string Name { get; set; }  // upprepas i alla tre klasser

    public void Hello()
    {
        Console.WriteLine($"{Name} säger Vov vov");
    }

    public void Play()
    {
        Console.WriteLine($"{Name} tuggar sönder en sko");
    }
}

class Kattskrälle
{
    public string Name { get; set; }  // upprepas igen

    public void Hello()
    {
        Console.WriteLine($"{Name} säger Mjauuuuuuuuuuu");
    }

    // ingen Play() — katten kan inte leka
    // men det vet inte kompilatorn, och ingen varnar dig
}

class Kanin
{
    public string Name { get; set; }  // upprepas igen

    public void Hello()
    {
        Console.WriteLine($"{Name} säger nomnomnom");
    }

    public void Play()
    {
        Console.WriteLine($"{Name} jagar sladdar att förstöra");
    }
}
```

---

## Förväntad output

```
Hello, Party Animals!
Pixi säger Vov vov
Pixi tuggar sönder en sko
Misse säger Mjauuuuuuuuuuu
Nosferatu säger nomnomnom
Nosferatu jagar sladdar att förstöra
```

---

## Vad är problemen?

| Problem | Vad det innebär |
|---------|-----------------|
| `Name` kopieras i varje klass | Ändrar du typen måste du ändra på tre ställen |
| Ingen gemensam typ | Du kan inte skapa en `List<???>` med alla djur |
| `Play()` saknas i `Kattskrälle` | Inget hindrar dig från att glömma den — ingen kontrakt |
| Ingen koppling mellan klasserna | `Vovve`, `Kattskrälle` och `Kanin` vet inte om varandra |

> **Nästa steg:** flytta `Name` och `Hello()` till en basklass `Djur`.  
> Varje djur ärver det gemensamma — och lägger bara till det som är unikt.
