# Spara och ladda en lista — JSON steg för steg

Den här artikeln går igenom koden från onsdagens livekodning, ett steg i taget.
Termerna `serialize` och `deserialize` dyker upp igen när vi arbetar med XML — det är samma ord, samma tanke.

---

## Problemet vi löser

När programmet stängs töms minnet. Mira, Tobbe och Zelda är borta — som om de aldrig funnits.

Det enda sättet att spara dem är att skriva ner värdena någonstans utanför programmet. En textfil duger bra. Men hur gör man ett C#-objekt till text, och sedan tillbaka till ett C#-objekt igen?

Det är precis vad det här handlar om.

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

Inget nytt här. Vi skapar tre hjältar precis som vanligt och samlar dem i en lista.

Anledningen till att vi använder en lista och inte tre separata variabler är att listan är *ett enda objekt*. Det är enklare att spara ett sak än att hålla reda på tre separata. Om vi hade hundra hjältar hade vi inte velat ha hundra separata variabler heller.

---

## Vad JSON egentligen är

Innan vi fortsätter med koden — vad är egentligen JSON?

JSON är bara text. Vanlig text, som du kan öppna i Notepad. Det speciella är att texten är formaterad på ett bestämt sätt som datorer förstår. Tanken är att ta ett C#-objekt och skriva ner alla dess värden i den texten.

En hjälte ser ut så här i JSON:

```json
{
  "Name": "Mira",
  "Health": 100,
  "Level": 3
}
```

Jämför med C#:

```csharp
var mira = new Hero { Name = "Mira", Health = 100, Level = 3 };
```

Värdena är exakt desamma. Det enda som skiljer sig är *formatet* — hur texten är skriven.

### Key och value

Varje rad i JSON är ett **key-value-par** — en nyckel och ett värde:

| Key (nyckel) | Value (värde) |
|---|---|
| `"Name"` | `"Mira"` |
| `"Health"` | `100` |
| `"Level"` | `3` |

**Key** är namnet på egenskapen. I JSON skrivs den alltid inom citattecken — `"Name"`, `"Health"`, `"Level"`. Det är samma namn som property-namnet i din C#-klass.

**Value** är värdet. Här gäller en enkel regel:
- Text ska ha citattecken: `"Mira"`
- Tal ska inte ha citattecken: `100`
- Sant/falskt ska inte ha citattecken: `true`, `false`

### JSON-symbolerna

| Symbol | Vad den betyder |
|--------|-----------------|
| `{ }` | Ett **objekt** — som en klass i C#. Allt innanför klamrarna är ett och samma objekt. |
| `[ ]` | En **array/lista** — som `List<T>` i C#. Håller ihop flera objekt. |
| `"text"` | **Text** — alla texter i JSON ska ha citattecken, både nycklar och textvärden. |
| `:` | **Värdet definieras** — separerar nyckeln från värdet. Läs det som "heter". `"Name": "Mira"` = Name heter Mira. |
| `,` | **Nästa** — separerar key-value-par från varandra, och objekt i en lista från varandra. |

### En lista i JSON

En lista av hjältar ser ut så här — hakparenteser `[ ]` betyder "detta är en lista":

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

Ytterst: `[ ]` — listan.
Inuti varje `{ }` — ett objekt, en hjälte.
Varje objekt separeras med komma.

---

## Steg 2 — Välj hur JSON-texten ska se ut

```csharp
var options = new JsonSerializerOptions
{
    WriteIndented = true
};
```

`JsonSerializerOptions` är en klass vars enda syfte är att hålla inställningar för hur JSON-texten ska formateras. Du skapar ett objekt av den och sätter de inställningar du vill ha.

`WriteIndented = true` betyder "skriv med radbrytningar och indrag". Utan den ser JSON-texten ut så här:

```json
[{"Name":"Mira","Health":100,"Level":3},{"Name":"Tobbe","Health":80,"Level":2},{"Name":"Zelda","Health":70,"Level":5}]
```

Tekniskt korrekt, men omöjlig att läsa. Med `WriteIndented = true` får du den snygga versionen med en rad per key-value-par.

Du behöver inte ha med `options` alls om du inte bryr dig om hur texten ser ut. Utan options-parametern fungerar koden, men JSON-texten blir en enda lång rad.

---

## Steg 3 — Serialize: C#-objekt → text

```csharp
string json = JsonSerializer.Serialize(heroes, options);
```

**Serialize** är engelska och kan översättas ungefär som "packa ihop". Vi tar listan med C#-objekt och packar ihop dem till en vanlig textsträng.

Tänk på det som att du ska skicka ett möbel-kit med posten. Stolen kan inte skickas som den är — du demonterar den, lägger bitarna platt i en kartong och skickar kartongen. JSON-texten är kartongen. C#-objekten är möbeln.

`JsonSerializer.Serialize` är metoden som demonterar. Den går igenom varje property i varje objekt i listan och skriver ner värdena i JSON-format.

Resultatet lagras i variabeln `json`, som är en vanlig `string`. Du kan skriva ut den, skicka den till en server, spara den i en fil — vad du vill.

**Samma ord används för XML, YAML och andra format.** Serialize = packa ihop data till ett format som kan sparas eller skickas. Det spelar ingen roll vilket format.

---

## Steg 4 — Spara till fil

```csharp
File.WriteAllText("hjaltar.json", json);

Console.WriteLine($"Sparade {heroes.Count} hjältar i hjaltar.json");
Console.WriteLine(json);
```

`File.WriteAllText` tar två saker: ett filnamn och en textsträng. Den skapar filen om den inte finns, eller skriver över den om den redan finns, och stoppar in textsträngen.

Filnamnet `"hjaltar.json"` är en relativ sökväg. Det betyder att filen hamnar bredvid det körande programmet, i mappen `bin/Debug/net10.0/`. Du kan navigera dit i Utforskaren och öppna filen i Notepad för att se att det faktiskt är JSON-texten som ligger där.

Vill du se hela sökvägen kan du lägga till:

```csharp
Console.WriteLine(Path.GetFullPath("hjaltar.json"));
```

Viktigt att förstå: det är bara texten i `json`-strängen som sparas. `File.WriteAllText` vet ingenting om C#-klasser eller JSON — den tar en sträng och stoppar in den i en fil. Det är serialize-steget ovan som konverterade objekten till text.

---

## Steg 5 — Ladda och deserialize: text → C#-objekt

```csharp
string text = File.ReadAllText("hjaltar.json");
List<Hero> loadedHeroes = JsonSerializer.Deserialize<List<Hero>>(text)!;
```

Här sker två saker i rad.

### Läs texten från filen

```csharp
string text = File.ReadAllText("hjaltar.json");
```

`File.ReadAllText` läser hela innehållet i filen och returnerar det som en sträng. Variabeln `text` innehåller nu exakt samma text som vi sparade i steg 4 — JSON-texten med alla hjältar.

### Gör om texten till C#-objekt

```csharp
List<Hero> loadedHeroes = JsonSerializer.Deserialize<List<Hero>>(text)!;
```

**Deserialize** är motsatsen till serialize — "packa upp". Vi tar textsträngen och gör om den till vanliga C#-objekt vi kan använda.

Analogin med möbelkitet: det är som att öppna kartongen, läsa monteringsanvisningen och sätta ihop stolen igen. Samma stol, men den kom som text.

`<List<Hero>>` är typen vi förväntar oss. JSON-läsaren behöver veta *vad* den ska skapa. Den kan inte gissa att `"Name"`, `"Health"` och `"Level"` tillhör klassen `Hero` — vi måste säga det. Hade vi haft en lista med `Product` istället hade vi skrivit `<List<Product>>`.

Utropstecknet `!` i slutet är vår utfästelse till C#: "Jag lovar att det här inte är null". `Deserialize` kan teoretiskt returnera `null` om texten är tom eller ogiltig. Med `!` säger vi att vi är säkra på att det fungerade. (I verklig kod kontrollerar man det ordentligt, men för övning duger `!`.)

---

## Hela flödet i ett diagram

```
SPARA
  C#-lista
      ↓  Serialize
  JSON-text
      ↓  File.WriteAllText
  fil på disk

LADDA
  fil på disk
      ↓  File.ReadAllText
  JSON-text
      ↓  Deserialize
  C#-lista
```

Serialize och deserialize är de enda stegen som faktiskt *förstår* JSON. `WriteAllText` och `ReadAllText` vet bara om text och filer — de bryr sig inte om vad texten innehåller.

---

## Samma ord i XML

När vi arbetar med XML senare ser du:

```csharp
XmlSerializer serializer = new XmlSerializer(typeof(List<Hero>));
serializer.Serialize(stream, heroes);     // packa ihop
serializer.Deserialize(stream);           // packa upp
```

Namnen är identiska. Tanken är densamma — ta C#-objekt och gör om dem till text, och tillbaka. Det som skiljer sig är hur texten ser ut. JSON använder `{ }` och `[ ]`. XML använder taggar som HTML:

```xml
<Hero>
  <Name>Mira</Name>
  <Health>100</Health>
  <Level>3</Level>
</Hero>
```

Samma data, annat format. Samma termer.

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
Laddade 3 hjältar:
- Mira, nivå 3
- Tobbe, nivå 2
- Zelda, nivå 5
```
