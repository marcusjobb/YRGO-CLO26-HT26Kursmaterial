# Livekodning: från hjälte till fil

**Lektion 4 nov 2026** · Kurs-03 Fördjupad OOP

Vi kodade oss igenom hela kedjan idag: en enkel hjälteklass, arv, abstract, interface,
JSON och slutligen en refaktorering som separerade ansvaren. Det händde mycket på en gång.
Det här är kartan — läs den i lugn takt.

Koden finns på GitHub: [Mira_hero_01_livekodning](https://github.com/marcusjobb/Mira_hero_01_livekodning)

---

## Steg 1: En klass med konstruktor och properties

Vi börjar enkelt. Vi vill representera en hjälte i koden.

```csharp
using Mira_hero_01_live.Poco;

Console.WriteLine("Hello, Mira!");

var mira = new Hero("Mira", 100);
var pelle = new Hero("Pelle", 100);
Hero tobbe = new Hero("", 0);

tobbe.Name = "Tobbe";
tobbe.Health = 80;

tobbe.Speak();
mira.Speak();
```

Och klassen ser ut så här:

```csharp
namespace Mira_hero_01_live.Poco
{
    public class Hero(string name, int health)
    {
        public string Name { get; set; } = name;
        public int Health { get; set; } = health;

        public void Speak()
        {
            Console.WriteLine($"Hello! I'm {Name} and I am Cool");
        }
    }
}
```

**Vad är ett namespace?**

`namespace Mira_hero_01_live.Poco` är klassens adress i kod-trädet. Precis som en postadress
skiljer den din `Hero` från en `Hero` i ett annat bibliotek. Om du inte har ett namespace och
ett annat paket du installerar också har en klass som heter `Hero` — krockar de och koden
kompilerar inte.

`using Mira_hero_01_live.Poco;` uppe i Program.cs betyder: "importera allt från den adressen
så jag kan skriva `Hero` direkt utan att skriva hela adressen varje gång."

**Poco** = Plain Old C# Object. En enkel dataklass utan tung logik — bara data och enkla
hjälpmetoder. Det är en konvention, inte ett C#-nyckelord.

**Vad är `{ get; set; }` och varför inte bara `public string Name;`?**

`public string Name;` är ett publikt fält — vem som helst kan läsa och skriva direkt.
`public string Name { get; set; }` är en property — det ser likadant ut utifrån, men det
ger dig kontroll:

- Du kan lägga till logik senare: `set { if (value != "") _name = value; }` utan att ändra
  alla ställen som använder `Name`.
- Du kan göra `{ get; private set; }` = alla kan läsa, men bara klassen själv kan ändra.
- Många ramverk (JSON, databaser, UI-bindning) kräver properties, inte fält.

Tumregel: använd alltid `{ get; set; }` på data du exponerar från en klass.

**Primary constructor**

`public class Hero(string name, int health)` — konstruktorns parametrar skrivs direkt i
klassrubriken. Det är C# 12-syntax och exakt samma som att skriva:

```csharp
public class Hero
{
    public Hero(string name, int health)
    {
        Name = name;
        Health = health;
    }
    public string Name { get; set; }
    public int Health { get; set; }
}
```

Den kortare formen sparar rader och är lättare att läsa.

---

## Steg 2: Flera hjältar i en lista

```csharp
var party = new List<Hero>() { mira, pelle, tobbe };

foreach (var hero in party)
{
    Console.WriteLine($"{hero.Name} has health {hero.Health}");
}
```

En `List<Hero>` håller alla hjältar. `foreach` loopar igenom dem en i taget.
Men vad händer när vi vill ha monster med i listan? Monster är inte hjältar — de kan inte
stoppas in i en `List<Hero>`.

Det är problemet som leder oss till arv.

---

## Steg 3: Arv — gemensam basklass

**Hur vet man att det är dags att skapa en basklass?**

Code smell: du skriver samma sak på flera ställen. `Hero` och `Monster` skulle båda ha
`Name`, `Health` och en `Attack()`-metod. Om vi ändrar hur `Health` fungerar måste vi
ändra på två ställen — det är ett tecken (DRY: Don't Repeat Yourself).

Flytta det gemensamma till `Character`. Både `Hero` och `Monster` ärver därifrån.

**Hur extraherar man en basklass i IDE?**

Du behöver inte göra det för hand. IDEt hjälper dig:

- **Visual Studio:** Klicka på klassnamnet → `Ctrl+.` → "Extract base class..." →
  namnge och välj vilka members som ska med.
- **Rider:** Högerklicka på klassnamnet → Refactor → Extract Superclass...
  ELLER `Ctrl+Alt+Shift+T` → "Extract Superclass".
- **VS Code (C# Dev Kit):** Högerklicka → Quick Fix → "Extract base class"
  (fungerar inte alltid — VS eller Rider är bättre för den här typen av refaktorering).

Klassen `Character` efter extraheringen:

```csharp
namespace Mira_hero_01_live.Poco
{
    public class Character(string name, int health)
    {
        public string Name { get; set; } = name;
        public int Health { get; set; } = health;
    }
}
```

`Hero` ärver nu från `Character` med `: Character(name, health)`:

```csharp
namespace Mira_hero_01_live.Poco
{
    public class Hero(string name, int health) : Character(name, health)
    {
        public override void Attack(Character enemy)
        {
            Console.WriteLine($"Hjälten svänger svärdet och skadar {enemy.Name}");
            enemy.Health -= 10;
        }

        public override void Speak()
        {
            Console.WriteLine($"Hello! I'm {Name} and I am Cool");
        }
    }
}
```

```csharp
namespace Mira_hero_01_live.Poco
{
    public class Monster(string name, int health) : Character(name, health)
    {
        public override void Attack(Character enemy)
        {
            Console.WriteLine($"Monstret biter och skadar {enemy.Name}");
            enemy.Health -= 20;
        }

        public override void Speak()
        {
            Console.WriteLine($"Grrr!");
        }
    }
}
```

Nu kan vi ha en lista av `Character` som innehåller både hjältar och monster:

```csharp
var characters = new List<Character>() { mira, monster };

foreach (var thing in characters)
{
    thing.Speak(); // Rätt Speak() körs beroende på typ
    Console.WriteLine(thing.Name + " has " + thing.Health + " health");
}
```

Att rätt `Speak()` körs automatiskt beroende på om `thing` är en `Hero` eller `Monster`
kallas **polymorfism** — en av de viktigaste idéerna i OOP.

---

## Steg 4: Abstract — tvinga subklasserna

Problemet: om vi lägger till en ny klass `Dragon` och glömmer skriva `Speak()` kompilerar
koden ändå. `Dragon` ärver en eventuell standard-`Speak()` från `Character` — eller kraschar
vid körning om ingen finns.

Lösningen: gör `Character` abstrakt. Då tvingas varje subklass att implementera metoderna.

```csharp
using System.Text.Json.Serialization;

namespace Mira_hero_01_live.Poco
{
    public abstract class Character(string name, int health)
    {
        [JsonPropertyName("namn")]
        public string Name { get; set; } = name;

        [JsonPropertyName("health")]
        public int Health { get; set; } = health;

        [JsonPropertyName("level")]
        public int Level { get; set; } = 1;

        [JsonPropertyName("inventory")]
        public List<string> Inventory { get; set; } = new List<string>() { "Knife", "food", "bow", "arrows(5)" };

        [JsonPropertyName("generic_greeting")]
        public string Greeting { get; set; } = "Hello";

        public abstract void Speak();         // Inget innehåll — subklassen MÅSTE implementera
        public abstract void Attack(Character enemy);
    }
}
```

`abstract class` = den här klassen finns bara för att ärvas, aldrig för att skapas direkt:

```csharp
var c = new Character("?", 0); // Kompileringsfel — kan inte instansiera abstract klass
```

**Vad är `[JsonPropertyName("namn")]`?**

C# namnger properties med PascalCase: `Name`, `Health`, `Level`.
JSON-konventionen är camelCase eller lowercase: `name`, `health`, `level`.

Utan attributet: JSON-filen får nyckeln `"Name"` (precis som C#-propertyn).
Med `[JsonPropertyName("namn")]`: du bestämmer vad nyckeln heter i JSON-filen,
utan att döpa om propertyn i koden.

```json
{
  "namn": "Mira",
  "health": 99,
  "level": 1,
  "inventory": ["Knife", "food", "bow", "arrows(5)"],
  "generic_greeting": "Hello"
}
```

Det gör JSON-filen läsbar för externa system och API:er som förväntar sig lowercase — utan
att du behöver skriva `character.namn` i din C#-kod.

---

## Steg 5: Interface — en extra förmåga

Problemet: bara `Wizard` ska kunna hela. Vi kan inte lägga `Heal()` i `Character` —
alla karaktärer skulle få den då. Och `Wizard` ärver redan `Character`, så ett andra arv
fungerar inte i C# (enkelt arv).

Lösningen: ett interface. Det är inte en klass utan ett **kontrakt** — en lista av metoder
som en klass lovar att implementera.

```csharp
namespace Mira_hero_01_live.Poco
{
    public interface IHealer
    {
        void Heal(Character wounded);

        public void SayHello()
        {
            Console.WriteLine("Hello");
        }
    }
}
```

Namnkonvention: interfaces börjar alltid med stor `I` (Microsofts regel).

`Wizard` ärver från `Character` OCH implementerar `IHealer` — semikolonet separerar:

```csharp
namespace Mira_hero_01_live.Poco
{
    public class Wizard(string name, int health) : Character(name, health), IHealer
    {
        public void Heal(Character wounded)
        {
            Console.WriteLine("Hokus Pokus, I cannot focus!");
            Console.WriteLine($"{wounded.Name} health before: {wounded.Health}");
            wounded.Health = 100;
            Console.WriteLine($"{wounded.Name} health after: {wounded.Health}");
        }

        public override void Attack(Character enemy)
        {
            Console.WriteLine($"Hjälten smäller sin trollstav i huvudet på {enemy.Name}");
            enemy.Health -= 5;
        }

        public override void Speak()
        {
            Console.WriteLine($"Hello! I'm {Name} and I am Wiz");
        }
    }
}
```

Vi kan kolla i runtime om en karaktär råkar vara en healer med `is`:

```csharp
foreach (var thing in characters)
{
    thing.Speak();
    Console.WriteLine(thing.Name + " has " + thing.Health + " health");

    if (thing is IHealer)
    {
        (thing as IHealer).Heal(mira);
    }
    else
        Console.WriteLine("...not a healer");
}
```

`thing is IHealer` — frågar om detta objekt implementerar interfacet, oavsett vilken klass
det är. Det är runtime-typkontroll.

---

## Steg 6: Spara hjälten till JSON

Nu kan vi köra programmet och skapa Mira — men nästa gång vi startar är hon borta. Vi vill
spara henne till disk.

`System.Text.Json` är det inbyggda JSON-biblioteket i .NET. Det serialiserar (konverterar
ett objekt till text) och deserialiserar (konverterar text tillbaka till ett objekt).

```csharp
using System.Text.Json;
using Mira_hero_01_live.Poco;

Console.WriteLine("Hello, Json!");

var mira = new Hero("Mira", 99);

var json = JsonSerializer.Serialize(mira,
    new JsonSerializerOptions { WriteIndented = true, IndentSize = 2 });

Console.WriteLine("Sparar:");
Console.WriteLine(json);
```

`WriteIndented = true` gör JSON-filen läsbar för människor med indragningar.
Utan det: `{"namn":"Mira","health":99,"level":1,...}` — tekniskt korrekt men svårläst.

Resultatet i konsolen:

```json
{
  "namn": "Mira",
  "health": 99,
  "level": 1,
  "inventory": ["Knife", "food", "bow", "arrows(5)"],
  "generic_greeting": "Hello"
}
```

Spara till fil:

```csharp
string file = Path.Combine("c:\\git\\", "Mira.json");
File.WriteAllText(file, json);
Console.WriteLine("Filen sparades i :" + file);
```

`File.WriteAllText` skapar filen om den inte finns, skriver över den om den finns.
`Path.Combine` sätter ihop sökvägar på ett plattformssäkert sätt — det spelar ingen roll
om du kör Windows (`\`) eller Linux (`/`).

---

## Steg 7: Läsa tillbaka hjälten

```csharp
Console.ReadKey(); // Paus — vänta på tangenttryck
Console.Clear();

Console.WriteLine("Läs filen");

string mira2 = File.ReadAllText(file);
Console.WriteLine(mira2);

var Mira2 = JsonSerializer.Deserialize<Hero>(mira2);
```

**`?` och `??` — null-säkerhet**

`JsonSerializer.Deserialize<Hero>()` returnerar `Hero?` (med frågetecken). Det betyder:
"resultatet kan vara null — var beredd på det."

Null är frånvaron av ett värde. I C# är det vanligaste kraschen `NullReferenceException` —
att försöka använda ett objekt som är null. `Hero?` är kompilatorns sätt att säga:
"den här kan vara null, kolla innan du använder den."

```csharp
if (Mira2 is not null)
{
    Console.WriteLine(Mira2.Name);
    Console.WriteLine(Mira2.Health);
    Console.WriteLine(Mira2.Level);
    foreach (string item in Mira2.Inventory)
        Console.Write(item + " ");
    Console.WriteLine();
}
```

`if (Mira2 is not null)` är det moderna sättet att kolla null i C# — läsbarare än
`if (Mira2 != null)`.

`??` (null-coalescing) dyker upp i nästa steg — det är ett kortare sätt att hantera null
utan ett helt if-block.

---

## Steg 8: Refaktorering — SRP och SoC

Programmet fungerar. Men `Program.cs` vet nu för mycket: det hanterar användargränssnitt
(vad som skrivs ut) OCH filsystem (hur hjälten sparas). Det är ett problem.

**SRP — Single Responsibility Principle**

En klass ska ha ett enda ansvar — ett enda skäl att förändras.

- `Program.cs` som hanterar UI OCH filer = två skäl att ändras = brott mot SRP.
  Om vi byter filformat måste vi röra `Program.cs`. Om vi byter utskriftsformat måste vi
  också röra `Program.cs`. Det är för mycket på ett ställe.

**SoC — Separation of Concerns**

Separata "bekymmer" ska hanteras av separata delar av koden.

- UI-kod = vad användaren ser = `Program.cs`
- Lagrings-kod = hur data sparas = en egen klass

Lösning: flytta all fillogik till en `HeroStorage`-klass.

```csharp
using System.Text.Json;
using Mira_hero_01_live.Poco;

class HeroStorage
{
    public string SavePath { get; set; } = "";

    public void SaveHero(Hero hero)
    {
        string json = JsonSerializer.Serialize(hero);
        string filename = GetFilename(hero.Name);
        File.WriteAllText(filename, json);
    }

    private string GetFilename(string name)
    {
        return Path.Combine(SavePath, name + ".json");
    }

    public Hero? LoadHero(string name)
    {
        string filename = GetFilename(name);
        string json = File.ReadAllText(filename);
        return JsonSerializer.Deserialize<Hero>(json);
    }

    public bool HeroExists(string name)
    {
        string filename = GetFilename(name);
        return File.Exists(filename);
    }
}
```

`GetFilename` är `private` — den är ett internt hjälpmedel, inget utanför `HeroStorage`
behöver veta om den. Det är inkapsling: gömmer implementationsdetaljer.

`Hero?` på `LoadHero` — frågetecknet berättar att metoden kan returnera null (om filen
inte kan läsas eller JSON är felaktig). Det tvingar anroparen att kolla.

`Program.cs` med `HeroStorage` — nu minimal:

```csharp
using Mira_hero_01_live.Poco;

var hs = new HeroStorage() { SavePath = @"c:\git" };

Console.Clear();
Console.WriteLine("Vad heter din hjälte?");
string name = Console.ReadLine() ?? "";
Hero? hero;

if (!hs.HeroExists(name))
{
    Console.WriteLine("Välkommen newbie!");
    hero = new Hero(name, 100);
}
else
{
    hero = hs.LoadHero(name);
}

if (hero is null)
{
    Console.Write("Något gick fel!");
    return;
}

Console.WriteLine(hero.Name + " has " + hero.Health + " and is level " + hero.Level);
```

**`??` i praktiken:**

```csharp
string name = Console.ReadLine() ?? "";
```

`Console.ReadLine()` kan returnera null (t.ex. om inmatningsströmmen stängs). Utan `??`
riskerar du null i `name`. Med `??` säger du: "om vänster sida är null, använd `""` istället."

Motsvarigheten utan `??`:

```csharp
string? input = Console.ReadLine();
string name = input != null ? input : "";
```

`??` är kortare och tydligare.

**`Hero?` och null-guard:**

```csharp
Hero? hero;
// ...
hero = hs.LoadHero(name); // kan returnera null

if (hero is null)
{
    Console.Write("Något gick fel!");
    return; // avbryt programmet
}

// Härifrån vet kompilatorn att hero inte är null
Console.WriteLine(hero.Name + " ...");
```

`return` inne i ett program utan return-värde avbryter körningen direkt. Det är ett
"early exit" — hantera felet tidigt och gå inte vidare med trasig data.

---

## Hela resan i korthet

| Steg | Vad vi lärde oss | Varför |
|------|-----------------|--------|
| Klass + properties | `{ get; set; }` ger kontroll över data | Inkapsling |
| Namespace | Adress i kod-trädet, hindrar namnkrockar | Organisation |
| Lista | `List<Hero>` samlar flera objekt | Iteration |
| Arv | Flytta gemensam kod till basklass | DRY |
| Abstract | Tvinga subklasser att implementera | Kontrakt |
| Interface | Extra förmåga utan extra arv | Flexibilitet |
| `[JsonPropertyName]` | Styr JSON-nyckelnamn utan att döpa om property | Snygg JSON |
| `?` och `??` | Null-säkerhet på rätt ställen | Undvika kraschar |
| HeroStorage / SRP | En klass, ett ansvar | Underhållbarhet |
| SoC | UI och lagring hålls isär | Testbarhet |

Koden: [Mira_hero_01_livekodning](https://github.com/marcusjobb/Mira_hero_01_livekodning)
