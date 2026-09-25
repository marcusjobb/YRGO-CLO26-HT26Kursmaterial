# List\<T\> — instansiering och foreach

Den här artikeln bygger på live-demo från lektion v39.  
Vi skapade en `List<Hero>` och testade fyra olika sätt att skapa objekt — och två sätt att loopa igenom listan.

---

## Skapa listan

```csharp
List<Hero> heroes = new();
```

`new()` utan typnamnet kallas *target-typed new* och fungerar i C# 9 och senare. Kompilatorn ser att variabeln är `List<Hero>` och räknar ut resten. Det är exakt samma sak som `new List<Hero>()`.

---

## Fyra sätt att instansiera ett objekt

Alla fyra varianter nedan skapar ett likvärdigt `Hero`-objekt. Det är en stilfråga — välj det som passar situationen.

**Variant 1 — ny variabel + objektinitierare:**

```csharp
Hero batman = new() { Name = "Bruce Wayne", Alias = "Batman" };
heroes.Add(batman);
```

Bra när du behöver referera till objektet senare i koden.

**Variant 2 — ny variabel + rad för rad:**

```csharp
Hero superman = new();
superman.Name = "Clark Kent";
superman.Alias = "Superman";
heroes.Add(superman);
```

Tydligast att läsa, men mest kod att skriva. Bra för nybörjare eller när en property beräknas separat.

**Variant 3 — objektinitierare på flera rader:**

```csharp
Hero supergirl = new Hero()
{
    Name = "Kara Danvers",
    Alias = "Supergirl"
};
heroes.Add(supergirl);
```

Snygg när objektet har många properties — lättare att läsa än en lång rad.

**Variant 4 — direkt i Add-anropet (inline):**

```csharp
heroes.Add(new Hero() { Alias = "Jokern", Name = "Jack Napier" });
```

Kortast möjliga syntax. Bra när du inte behöver referera till objektet igen.

---

## Insert — lägg till på en specifik position

`Add` lägger alltid till sist. `Insert` låter dig välja index:

```csharp
heroes.Insert(0, new Hero() { Alias = "The Flash", Name = "Barry Allen" });
```

Index 0 betyder längst fram i listan. Alla befintliga element skjuts ett steg bakåt.

---

## Count och Sort

```csharp
Console.WriteLine(heroes.Count);
```

`Count` (inte `Length` — det är för arrayer) returnerar antalet element.

```csharp
heroes.Sort((a, b) => string.Compare(a.Name, b.Name));
```

Sorterar listan alfabetiskt på `Name`. Lambdan `(a, b) => ...` berättar för Sort hur två element jämförs — mer om det när vi går igenom LINQ nästa kurs.

---

## Två sätt att loopa

**Vanlig foreach:**

```csharp
foreach (var hero in heroes)
{
    Console.WriteLine(hero.Name + " is " + hero.Alias);
}
```

Klar och tydlig. Fungerar alltid.

**ForEach-oneliner med lambda:**

```csharp
heroes.ForEach(hero => Console.WriteLine(hero.Name + " is " + hero.Alias));
```

`ForEach` är en metod på `List<T>`. Den tar en lambda — ett kort anonymt kodblock.

`hero =>` säger: "för varje element i listan, kalla det `hero` och kör det som kommer efter pilen".

Det är egentligen samma sak som:

```csharp
heroes.ForEach(hero =>
{
    Console.WriteLine(hero.Name + " is " + hero.Alias);
});
```

Klamrarna är valfria när det bara är en rad. Pilen `=>` är lambda-symbolen — du kommer se den ofta i C#.

---

## Hela koden från lektionen

```csharp
Console.WriteLine("Hello, Generic list!");

List<Hero> heroes = new();

Hero batman = new() { Name = "Bruce Wayne", Alias = "Batman" };
heroes.Add(batman);

Hero superman = new();
superman.Name = "Clark Kent";
superman.Alias = "Superman";
heroes.Add(superman);

Hero supergirl = new Hero()
{
    Name = "Kara Danvers",
    Alias = "Supergirl"
};
heroes.Add(supergirl);

heroes.Add(new Hero() { Alias = "Jokern", Name = "Jack Napier" });
heroes.Insert(0, new Hero() { Alias = "The Flash", Name = "Barry Allen" });
heroes.Add(new Hero() { Name = "Arthur Curry", Alias = "Aquaman" });
heroes.Add(new Hero() { Name = "Diana Prince", Alias = "Wonder Woman" });
heroes.Add(new Hero() { Name = "Dick Grayson", Alias = "Nightwing" });
heroes.Add(new Hero() { Name = "Tim Drake", Alias = "Robin" });

heroes.Sort((a, b) => string.Compare(a.Name, b.Name));

Console.WriteLine("Heroes in the list");
Console.WriteLine(heroes.Count);

heroes.ForEach(hero => Console.WriteLine(hero.Name + " is " + hero.Alias));

class Hero
{
    public string Name { get; set; }
    public string Alias { get; set; }
}
```
