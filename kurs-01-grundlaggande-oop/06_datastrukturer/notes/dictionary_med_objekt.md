# Dictionary med objekt som värde

Den här artikeln bygger på live-demos från lektion v39.  
Vi tog steget från `Dictionary<string, string>` till `Dictionary<string, Hero>` — och snubblade på ett klassiskt fällfall längs vägen.

---

## Från primitiv till objekt

Hittills har vi använt Dictionary för enkla värden:

```csharp
Dictionary<string, int> poäng = new Dictionary<string, int>();
poäng.Add("Alex", 42);
```

Men värdet kan vara vilket typ som helst — även en klass vi skapat själva:

```csharp
Dictionary<string, Hero> flying = new Dictionary<string, Hero>();
```

Nyckeln är aliaset (det vi söker på), värdet är ett fullt `Hero`-objekt.

---

## Objektinitierare

Istället för att sätta varje property på en egen rad kan vi använda objektinitierarsyntax:

```csharp
Hero superman = new Hero() { Name = "Clark Kent", Alias = "Superman" };
```

Det är exakt samma sak som:

```csharp
Hero superman = new Hero();
superman.Name = "Clark Kent";
superman.Alias = "Superman";
```

Båda fungerar — objektinitieraren är bara kortare att skriva.

---

## Korsreferenser — två objekt som pekar på varandra

Vår `Hero`-klass har en property som heter `Cousin`. Den är av typen `Hero`, alltså en referens till ett annat objekt.

```csharp
class Hero
{
    public string Name { get; set; }
    public string Alias { get; set; }
    public Hero Cousin { get; set; }
}
```

Det ger oss möjligheten att skapa Superman och Supergirl som pekar på varandra:

```csharp
Hero superman = new Hero() { Name = "Clark Kent", Alias = "Superman" };

Hero supergirl = new Hero()
    { Name = "Kara Danvers", Alias = "Supergirl", Cousin = superman };

superman.Cousin = supergirl;
```

Notera ordningen: vi sätter `supergirl.Cousin = superman` direkt i objektinitieraren — för superman existerar redan på den raden.  
`superman.Cousin = supergirl` måste däremot sättas **efter** att supergirl skapats.

---

## Upplsagning via Dictionary

När båda objekten lagts till i Dictionary kan vi slå upp dem via alias:

```csharp
flying.Add("Superman", superman);
flying.Add("Supergirl", supergirl);

Console.WriteLine($"Supergirl: {flying["Supergirl"].Name}");
// → Supergirl: Kara Danvers

Console.WriteLine($"Hennes kusin: {flying["Supergirl"].Cousin.Name}");
// → Hennes kusin: Clark Kent
```

Varje `.Cousin` är en referens — vi följer pekaren till nästa objekt.

---

## Cirkulärreferensen 🫣

Eftersom supergirl pekar på superman OCH superman pekar på supergirl kan vi följa kedjan hur långt vi vill:

```csharp
Console.WriteLine($"Kusinens kusin: {flying["Supergirl"].Cousin.Cousin.Name}");
// → Kusinens kusin: Kara Danvers  (vi är tillbaka på supergirl)

Console.WriteLine($"Kusinens kusins kusin: {flying["Supergirl"].Cousin.Cousin.Cousin.Name}");
// → Kusinens kusins kusin: Clark Kent  (och tillbaka till superman)
```

Det kallas en **cirkulärreferens** — en sluten ring av objektreferenser. Det är helt lagligt i C# och kraschar inte, men en `while`-loop som följer `.Cousin` utan stoppregel skulle loopa för evigt.

### Varför händer det?

Objekt i C# lagras **inte** som kopior i variabler — variabeln håller en *referens* (en adress i minnet). När vi skriver `superman.Cousin = supergirl` lagrar vi adressen till supergirl-objektet, inte en kopia av det.

```
superman ──────► [ Clark Kent, Cousin ──► ]
                                          │
supergirl ◄───────────────────────────────┘
     │
     └──► [ Kara Danvers, Cousin ──► superman ]
```

De två objekten pekar på varandra. Att följa kedjan är bara att hoppa fram och tillbaka mellan dem.

---

## Hela koden från lektionen

```csharp
Dictionary<string, Hero> flying = new Dictionary<string, Hero>();

Hero superman = new Hero() { Name = "Clark Kent", Alias = "Superman" };
Hero supergirl = new Hero()
    { Name = "Kara Danvers", Alias = "Supergirl", Cousin = superman };
superman.Cousin = supergirl;

flying.Add("Superman", superman);
flying.Add("Supergirl", supergirl);

Console.WriteLine($"Supergirl: {flying["Supergirl"].Name}");
Console.WriteLine($"Hennes kusin: {flying["Supergirl"].Cousin.Name}");
Console.WriteLine($"Kusinens kusin: {flying["Supergirl"].Cousin.Cousin.Name}");
Console.WriteLine($"Kusinens kusins kusin: {flying["Supergirl"].Cousin.Cousin.Cousin.Name}");

class Hero
{
    public string Name { get; set; }
    public string Alias { get; set; }
    public Hero Cousin { get; set; }
}
```
