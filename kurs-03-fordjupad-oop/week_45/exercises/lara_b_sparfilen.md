# Läxa B: Hjältens äventyr, kapitel 2

**Till:** torsdag 5 november · **Tid:** ca 3–4 timmar för 🟢 + 🟡 · **Gör den:** ensam

## Berättelsen

Mira har korsat bron och står på andra sidan floden. Där sitter en gammal
skrivare vid namn Tilda i ett litet torn med tusen pergament.

"Du har gjort stora saker," säger Tilda. "Men om du somnar i natt är allt
glömt i morgon. Ska jag skriva ner det åt dig?"

Mira nickar. Tilda sträcker fram en penna. Koden är pennan.

Du har inte bråttom, och du behöver inte göra allt.

## Så här fungerar läxan

| Märke | Betyder | Hur många |
|-------|---------|-----------|
| 🟢 Obligatorisk | Alla ska klara dessa | 2 per del |
| 🟡 Mer övning | De flesta klarar dessa | 2 per del |
| 🥷 Ninja | Frivillig utmaning | 1 till 2 per del |

- Två delar: **spara hjälten** och **spara hela spelläget**. Totalt 11 uppgifter.
- Skapa ett **nytt konsolprojekt per del** (`Del1`, `Del2`).
- Lägg klasserna **under** koden i `Program.cs`. Står de ovanför får du felet CS8803.
- Alla klasser använder **properties** med `{ get; set; }`. `System.Text.Json` sparar inga vanliga fält, men alla properties.
- Överst behöver du `using System.Text.Json;`.
- Skriv koden själv. Det är samma steg som på onsdagen, så du kan titta i din egen övning.
- Förväntad output visar raderna för uppgiften. Filerna skapas i projektmappen när du kör `dotnet run`.
- Identifierare skrivs på engelska. Utskrifterna är på svenska.
- Fastnar du: öppna ledtråden. Fastnar du fortfarande: skriv ner frågan och ta med den till fredag.

Klassen du utgår från i båda delarna:

```csharp
class Hero
{
    public string Name { get; set; } = "";
    public int Health { get; set; }
    public int Level { get; set; }
}
```

---

# Del 1: Spara hjälten

*Tilda skriver ner det viktigaste: vem Mira är.*

### Uppgift 1.1 🟢 Skriv ner Mira

1. Skapa Mira med 100 i hälsa och nivå 1.
2. Spara henne som JSON i `hjalte.json` (använd `WriteIndented = true`).
3. Skriv ut en rad om att hjälten är sparad, och sedan JSON-texten.

**Förväntad output:**
```
Sparade hjälten i hjalte.json
{
  "Name": "Mira",
  "Health": 100,
  "Level": 1
}
```

<details markdown="1">
<summary>Ledtråd</summary>

Tre steg: `JsonSerializer.Serialize(...)` gör texten, `File.WriteAllText(...)` skriver den till filen.
Alternativen skickar du med som andra argument: `new JsonSerializerOptions { WriteIndented = true }`.

</details>

### Uppgift 1.2 🟢 Läs upp henne igen

1. I ett nytt projekt eller efter att du kommenterat bort sparandet: läs `hjalte.json`.
2. Gör om texten till ett `Hero`-objekt och skriv ut den.

**Förväntad output:**
```
Laddade Mira: 100 hälsa, nivå 1.
```

<details markdown="1">
<summary>Ledtråd</summary>

`File.ReadAllText("hjalte.json")` ger texten. `JsonSerializer.Deserialize<Hero>(json)!` ger objektet. Utropstecknet säger att du vet att filen är giltig.

</details>

### Uppgift 1.3 🟡 Finns det en sparfil?

Första gången finns ingen fil. Då ska en ny hjälte skapas. Annars ska hjälten laddas.

1. Använd `File.Exists("hjalte.json")`.
2. Finns filen: ladda hjälten och skriv `Välkommen tillbaka, Mira! Nivå 1.`
3. Finns den inte: skapa Mira och skriv `Ingen sparfil hittades. En ny hjälte föds.`
4. Testa båda vägarna. Ta bort `hjalte.json` för att testa den första.

**Förväntad output (utan fil):**
```
Ingen sparfil hittades. En ny hjälte föds.
```

**Förväntad output (med fil):**
```
Välkommen tillbaka, Mira! Nivå 1.
```

<details markdown="1">
<summary>Ledtråd</summary>

Deklarera hjälten före `if`: `Hero hero;`. Då kan båda grenarna fylla den, och koden efter `if` kan använda den.

</details>

### Uppgift 1.4 🟡 Mira växer

Nu ska äventyret faktiskt sparas. Bygg vidare på 1.3.

1. Efter `if`/`else`: öka hjältens `Level` med 1.
2. Skriv `Mira nådde nivå X!`
3. Spara hjälten i filen igen.
4. Ta bort `hjalte.json` och kör programmet tre gånger i rad.

**Förväntad output, körning 1:**
```
Ingen sparfil hittades. En ny hjälte föds.
Mira nådde nivå 2!
```

**Körning 2:**
```
Välkommen tillbaka, Mira! Nivå 2.
Mira nådde nivå 3!
```

**Körning 3:**
```
Välkommen tillbaka, Mira! Nivå 3.
Mira nådde nivå 4!
```

<details markdown="1">
<summary>Ledtråd</summary>

Spara-raden hamnar sist i programmet, efter `hero.Level++`. Då sparas den nya nivån, och nästa körning börjar där den här slutade.

</details>

### Uppgift 1.5 🥷 Spara det som behövs

Lägg till en property i `Hero` som räknas ut från andra värden:

```csharp
public bool IsAlive => Health > 0;
```

1. Spara hjälten och titta i filen. Vad står det om `IsAlive`?
2. Varför är det onödigt att spara den?
3. Hitta ett attribut från `System.Text.Json.Serialization` som döljer en property. Lägg det på `IsAlive` och spara igen.

**Förväntad output (före attributet):**
```
{
  "Name": "Mira",
  "Health": 100,
  "Level": 1,
  "IsAlive": true
}
```

<details markdown="1">
<summary>Ledtråd</summary>

Attributet heter `[JsonIgnore]` och skrivs på raden över propertyn. Det hör till `using System.Text.Json.Serialization;`.

</details>

---

# Del 2: Spara hela spelläget

*Tilda vill skriva ner mer än Mira: hennes guld och vad hon bär på.*

Skapa ett nytt konsolprojekt. Lägg till klassen `GameState` bredvid `Hero`:

```csharp
class GameState
{
    public Hero Hero { get; set; } = new Hero();
    public int Gold { get; set; }
    public List<string> Items { get; set; } = new List<string>();
}
```

Ett `GameState` innehåller ett `Hero`-objekt och en lista. JSON klarar det: objekt kan ligga inuti objekt.

### Uppgift 2.1 🟢 Spara hela spelet

1. Skapa ett `GameState`: Mira (100, nivå 1), 25 guld och sakerna `Karta` och `Fackla`.
2. Spara det i `spelstate.json` (`WriteIndented = true`).
3. Skriv ut en rad om att spelet är sparat, och sedan JSON-texten.

**Förväntad output:**
```
Sparade spelet i spelstate.json
{
  "Hero": {
    "Name": "Mira",
    "Health": 100,
    "Level": 1
  },
  "Gold": 25,
  "Items": [
    "Karta",
    "Fackla"
  ]
}
```

<details markdown="1">
<summary>Ledtråd</summary>

Det är samma steg som i 1.1. Skillnaden är att du serialiserar ett `GameState` i stället för ett `Hero`. Objektet inuti följer med automatiskt.

</details>

### Uppgift 2.2 🟢 Ladda hela spelet

1. Läs `spelstate.json` och gör om texten till ett `GameState`.
2. Skriv ut hjältens namn och nivå, guldet och sakerna.

**Förväntad output:**
```
Laddade spelet: Mira (nivå 1), 25 guld.
Saker: Karta, Fackla
```

<details markdown="1">
<summary>Ledtråd</summary>

`string.Join(", ", state.Items)` gör om en lista till en text med komma emellan.

</details>

### Uppgift 2.3 🟡 Nytt äventyr eller fortsätt?

1. Finns `spelstate.json`: ladda det och skriv `Välkommen tillbaka, Mira!`
2. Finns den inte: skapa ett nytt spel med samma värden som i 2.1 och skriv `Nytt äventyr börjar.`

**Förväntad output (utan fil):**
```
Nytt äventyr börjar.
```

**Förväntad output (med fil):**
```
Välkommen tillbaka, Mira!
```

<details markdown="1">
<summary>Ledtråd</summary>

Samma mönster som 1.3: `GameState state;` före `if`, och en gren som laddar och en som skapar.

</details>

### Uppgift 2.4 🟡 En runda äventyr

1. Efter `if`/`else`: hjälten tjänar 10 guld och går upp en nivå.
2. Skriv `Mira har X guld och är nivå Y.`
3. Spara spelet.
4. Ta bort `spelstate.json` och kör programmet två gånger.

**Förväntad output, körning 1:**
```
Nytt äventyr börjar.
Mira har 35 guld och är nivå 2.
```

**Körning 2:**
```
Välkommen tillbaka, Mira!
Mira har 45 guld och är nivå 3.
```

<details markdown="1">
<summary>Ledtråd</summary>

`state.Gold += 10;` och `state.Hero.Level++;`. Hjälten ligger inuti spelet, så du når nivån via `state.Hero`.

</details>

### Uppgift 2.5 🥷 En trasig sparfil

Ibland blir en fil trasig, till exempel om någon redigerar den för hand. Då kraschar programmet.

1. Öppna `spelstate.json` och skriv in `{ trasig` som enda innehåll. Kör programmet. Vad säger felet?
2. Fånga felet med `try`/`catch`. Vilken sorts exception kastas?
3. Vid fel: skriv `Sparfilen är trasig. Börjar om.` och starta ett nytt spel.

**Förväntad output:**
```
Sparfilen är trasig. Börjar om.
Mira har 35 guld och är nivå 2.
```

<details markdown="1">
<summary>Ledtråd</summary>

Felet är en `JsonException`. Lägg `Deserialize` inuti `try` och skapa det nya spelet i `catch (JsonException)`. Lägg gärna skapandet av ett nytt spel i en egen metod, så slipper du skriva det två gånger.

</details>

### Uppgift 2.6 🥷 Vart tog å, ä och ö vägen?

1. Spara en lista med saken `Svärd`. Öppna filen. Vad står det?
2. Sök efter hur du får `System.Text.Json` att skriva å, ä och ö som de är. Titta på `JsonSerializerOptions` och `Encoder`.

**Förväntad output (första försöket):**
```
[
  "Svärd"
]
```

**Förväntad output (efter lösningen):**
```
[
  "Svärd"
]
```

<details markdown="1">
<summary>Ledtråd</summary>

Standardinställningen skriver om tecken utanför ASCII till `\u`-koder, så att filen fungerar överallt. Du kan byta `Encoder` till `System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping`.

</details>

---

## Frågor till fredagens handledning

Ta med ett svar eller en fråga på var och en:

1. Varför blev filen tom (`{}`) när `Hero` hade vanliga fält i stället för properties?
2. Vad tror du händer om du lägger till en ny property i `Hero` och laddar en gammal sparfil som saknar den?
