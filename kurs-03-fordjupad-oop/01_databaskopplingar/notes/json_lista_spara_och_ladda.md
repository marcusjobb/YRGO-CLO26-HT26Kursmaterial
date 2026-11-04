# Spara och ladda en lista — JSON steg för steg

Den här artikeln går igenom koden från onsdagens livekodning, ett steg i taget.
Termerna `serialize` och `deserialize` dyker upp igen när vi arbetar med XML — det är samma ord, samma tanke.

---

## Vad vi ska åstadkomma

Vi har tre hjältar i minnet. Vi vill:

1. Spara dem till en fil så att de överlever när programmet stängs
2. Starta om programmet och läsa tillbaka dem — med alla värden intakta

---

## Steg 1 — Skapa hjältarna och samla dem i en lista

```csharp
var mira  = new Hero { Name = "Mira",  Health = 100, Level = 3 };
var tobbe = new Hero { Name = "Tobbe", Health = 80,  Level = 2 };
var zelda = new Hero { Name = "Zelda", Health = 70,  Level = 5 };

var heroes = new List<Hero>()
{
    mira,
    tobbe,
    zelda
};
```

Inget nytt här — vi skapar objekt och lägger dem i en lista precis som vanligt.

---

## Vad JSON egentligen är

JSON är bara text. En hjälte ser ut så här:

```json
{
  "Name": "Mira",
  "Health": 100,
  "Level": 3
}
```

Varje rad är ett **key-value-par** — en nyckel och ett värde:

| Key (nyckel) | Value (värde) |
|---|---|
| `"Name"` | `"Mira"` |
| `"Health"` | `100` |
| `"Level"` | `3` |

**Key** = namnet på egenskapen (alltid inom citattecken i JSON).  
**Value** = värdet. Text får citattecken. Tal och `true`/`false` får inga citattecken.

En lista av hjältar ser ut så här — hakparenteser `[ ]` betyder "lista":

```json
[
  {
    "Name": "Mira",
    "Health": 100,
    "Level": 3
  },
  {
    "Name": "Tobbe",
    "Health": 80,
    "Level": 2
  },
  {
    "Name": "Zelda",
    "Health": 70,
    "Level": 5
  }
]
```

---

## Steg 2 — Välj hur JSON-texten ska se ut

```csharp
var options = new JsonSerializerOptions
{
    WriteIndented = true
};
```

`WriteIndented = true` gör att JSON-texten formateras med radbrytningar och indrag, som ovan.
Utan den får du allt på en enda rad — korrekt men omöjlig att läsa.

Du behöver inte ha med `options` om du inte bryr dig om hur texten ser ut.

---

## Steg 3 — Serialize: C#-objekt → text

```csharp
string json = JsonSerializer.Serialize(heroes, options);
```

**Serialize** betyder "packa ihop". Vi tar listan med C#-objekt och packar ihop den till en vanlig textsträng.

Resultatet i `json` är precis det du ser i JSON-exemplet ovan — en lång textsträng.

Samma ord används för XML, YAML och andra format: serialize = packa ihop data till ett format som kan sparas eller skickas.

---

## Steg 4 — Spara till fil

```csharp
File.WriteAllText("hjaltar.json", json);

Console.WriteLine($"Sparade {heroes.Count} hjältar i hjaltar.json");
Console.WriteLine(json);
```

`File.WriteAllText` tar ett filnamn och en sträng, och skriver strängen till filen.
Filen hamnar bredvid programmet (`bin/Debug/net10.0/`). Du kan öppna den i Notepad och se exakt vad som sparades.

---

## Steg 5 — Ladda och deserialize: text → C#-objekt

```csharp
string text = File.ReadAllText("hjaltar.json");
List<Hero> loadedHeroes = JsonSerializer.Deserialize<List<Hero>>(text)!;
```

Två saker händer:

1. `File.ReadAllText` läser hela filen och returnerar innehållet som en sträng — precis det `json` innehöll i steg 4.
2. `JsonSerializer.Deserialize<List<Hero>>(text)` tar textsträngen och skapar C#-objekt av den.

**Deserialize** betyder "packa upp". Vi tar texten och gör om den till vanliga C#-objekt vi kan använda.

`<List<Hero>>` är typen vi förväntar oss. JSON-läsaren behöver veta *vad* den ska skapa.

Utropstecknet `!` i slutet säger till C# "jag lovar att detta inte är null".

---

## Hela flödet i ett diagram

```
C#-lista           →  Serialize  →  JSON-text  →  WriteAllText  →  fil på disk
fil på disk  →  ReadAllText  →  JSON-text  →  Deserialize  →  C#-lista
```

---

## Samma ord i XML

När vi arbetar med XML senare ser du:

```csharp
XmlSerializer serializer = new XmlSerializer(typeof(List<Hero>));
serializer.Serialize(stream, heroes);     // packa ihop
serializer.Deserialize(stream);           // packa upp
```

Namnen är identiska. Tanken är densamma. Bara formatet på texten skiljer sig åt.

---

## Hela koden

```csharp
using System.Text.Json;

var mira  = new Hero { Name = "Mira",  Health = 100, Level = 3 };
var tobbe = new Hero { Name = "Tobbe", Health = 80,  Level = 2 };
var zelda = new Hero { Name = "Zelda", Health = 70,  Level = 5 };

var heroes = new List<Hero>()
{
    mira,
    tobbe,
    zelda
};

var options = new JsonSerializerOptions
{
    WriteIndented = true
};

string json = JsonSerializer.Serialize(heroes, options);

File.WriteAllText("hjaltar.json", json);

Console.WriteLine($"Sparade {heroes.Count} hjältar i hjaltar.json");
Console.WriteLine(json);

//---------

string text = File.ReadAllText("hjaltar.json");
List<Hero> loadedHeroes = JsonSerializer.Deserialize<List<Hero>>(text)!;

Console.WriteLine($"Laddade {loadedHeroes.Count} hjältar:");
foreach (var h in loadedHeroes)
{
    Console.WriteLine($"- {h.Name}, nivå {h.Level}");
}
```

**Output:**
```
Sparade 3 hjältar i hjaltar.json
[
  {
    "Name": "Mira",
    "Health": 100,
    "Level": 3
  },
  ...
]
Laddade 3 hjältar:
- Mira, nivå 3
- Tobbe, nivå 2
- Zelda, nivå 5
```
