---
marp: true
theme: nion-dark
paginate: true
---

<!-- _class: title -->

# Loopar

### Upprepa utan att copy-pasta

_Kurs 01 · Vecka 3 · Nion Education_

---

## Varför loopar?

Tänk dig att du ska skriva ut alla dagar i en vecka:

```csharp
Console.WriteLine("Måndag");
Console.WriteLine("Tisdag");
Console.WriteLine("Onsdag");
// ... och så vidare
```

Det fungerar — men vad händer när du har 100 saker?
Eller 10 000?

**En loop gör samma sak, men med en handfull rader.**

> 💬 _"Koden ska arbeta hårt, inte du. Det är precis vad loopar är till för."_

---

## while — kör så länge villkoret är sant

```csharp
int nedräkning = 5;

while (nedräkning > 0) {
    Console.WriteLine("T minus " + nedräkning + "...");
    nedräkning--;
}

Console.WriteLine("Avfärd!");
```

```
Utskrift:
T minus 5...
T minus 4...
T minus 3...
T minus 2...
T minus 1...
Avfärd!
```

---

## while — hur den tänker

```
┌─────────────────────────────────┐
│  int nedräkning = 5             │
└────────────┬────────────────────┘
             │
      ┌──────▼──────┐
      │ nedräkning  │
      │   > 0?      │
      └──┬────┬─────┘
     Sant│    │Falskt
         │    └──────────────────────► kör vidare
         ▼
  ┌──────────────────┐
  │  Skriv ut värdet │
  │  nedräkning--    │
  └──────┬───────────┘
         │
         └──────► (tillbaka till villkoret)
```

Loopen kollar villkoret **innan** varje runda.

---

## Infinite loop — fällan som väntar

```csharp
int nedräkning = 5;

while (nedräkning > 0) {
    Console.WriteLine("T minus " + nedräkning + "...");
    // ← nedräkning-- saknas!
}
```

`nedräkning` ändras aldrig → villkoret är alltid sant → programmet fastnar.

**En infinite loop är ett av de vanligaste misstagen med while.**

Kom ihåg: något **inuti loopen** måste förändra villkoret.
Här är det `nedräkning--` som är nyckeln.

> 💬 _"Om programmet verkar hänga — tryck Ctrl+C för att avbryta. Leta sedan efter vad som aldrig förändras."_

---

## for — när du vet exakt hur många gånger

```csharp
for (int i = 1; i <= 10; i++)
    Console.WriteLine("3 × " + i + " = " + (3 * i));
```

```
Utskrift:
3 × 1 = 3
3 × 2 = 6
...
3 × 10 = 30
```

Tre delar i ett svep — initialvärde, villkor och steg.

---

## for — de tre delarna

```csharp
for (int i = 1; i <= 10; i++)
```

```
      ┌─────────────┬────────────────┬──────────┐
      │  int i = 1  │   i <= 10      │   i++    │
      │             │                │          │
      │  Startvärde │  Kör så länge  │  Ändring │
      │  (en gång)  │  detta stämmer │  per varv│
      └─────────────┴────────────────┴──────────┘
```

- `int i = 1` — körs en gång innan loopen startar
- `i <= 10` — kontrolleras före varje runda
- `i++` — körs efter varje runda

> 💬 _"Alla tre delar är valfria — men lämnar du dem tomma måste du se till att loopen tar slut på något annat sätt."_

---

## foreach — enklast när du har en samling

```csharp
string[] veckodagar = { "Måndag", "Tisdag", "Onsdag", "Torsdag", "Fredag" };

foreach (string dag in veckodagar)
    Console.WriteLine("Dag: " + dag);
```

```
Utskrift:
Dag: Måndag
Dag: Tisdag
Dag: Onsdag
Dag: Torsdag
Dag: Fredag
```

`foreach` tar hand om allt: start, villkor och steg.
Du behöver aldrig hålla koll på en räknare.

---

## foreach — hur den läses

```csharp
foreach (string dag in veckodagar)
```

Läs det som:

> _"För varje `dag` i samlingen `veckodagar` — gör det här."_

`dag` är en temporär variabel som håller **ett värde åt gången**.
Den skapas automatiskt och finns bara inuti loopen.

> 💬 _"Foreach är den tydligaste av looparna att läsa högt. Om du kan läsa koden som en mening — är det ett bra tecken."_

---

## Jämförelsetabell — när väljer du vilken?

| Situation | Välj |
|-----------|------|
| Vet exakt antal varv | `for` |
| Kör tills ett villkor ändras | `while` |
| Går igenom en samling (array, lista) | `foreach` |
| Vet inte hur många varv, men vill alltid köra minst en | `do/while` |

De flesta loopar du skriver i det här kursmomentet
är antingen `for` eller `foreach`.

---

## Allt tillsammans

```csharp
// while — nedräkning
int nedräkning = 3;
while (nedräkning > 0) {
    Console.WriteLine("T minus " + nedräkning + "...");
    nedräkning--;
}

// for — multiplikationstabell
for (int i = 1; i <= 5; i++)
    Console.WriteLine("2 × " + i + " = " + (2 * i));

// foreach — samling
string[] frukter = { "Äpple", "Banan", "Päron" };
foreach (string frukt in frukter)
    Console.WriteLine("Frukt: " + frukt);
```

---

<!-- _class: title -->

# Nu är det din tur

### Övningar finns i `exercises/`

🟢 `loops/countdown.md` — bygg en nedräkning med while  
🟡 `loops/pirate_game.md` — håll ett spel igång med loop och villkor

_Ta det steg för steg. Använd tipsen om du fastnar._
