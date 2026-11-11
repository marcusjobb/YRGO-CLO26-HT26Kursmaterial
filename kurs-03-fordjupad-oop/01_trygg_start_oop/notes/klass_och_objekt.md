# Klass och objekt

Läsversion av första passet. Allt bygger på hjälten Mira. Nästa del: `arv.md`.

## Klass och objekt

En **klass** är en ritning. Ett **objekt** är huset som byggs av ritningen.

```csharp
class Hero
{
    public string Name;
    public int Health;

    public Hero(string name, int health)
    {
        Name = name;
        Health = health;
    }

    public void Greet()
    {
        Console.WriteLine($"Hej, jag heter {Name}!");
    }
}

Hero mira = new Hero("Mira", 100);
Hero tobbe = new Hero("Tobbe", 80);
mira.Greet();
tobbe.Greet();
```

**Output:**
```
Hej, jag heter Mira!
Hej, jag heter Tobbe!
```

Två objekt av samma klass. Ändrar du Mira, ändras inte Tobbe. Konstruktorn körs i samma ögonblick som `new` bygger objektet.
