# LINQ: beskriv vad du vill ha

Termer finns i `termer/linq.md`. Övningen finns i `exercises/ovning_linq_parkering.md`.

Hittills har vi arbetat med objekt: klasser som har data och metoder, och som ändrar sig själva. Nu ska vi lära oss ett annat sätt att tänka, som du kommer att använda varje dag i EF.

Exemplet är ett bostadsrättsområde i Malmö med parkeringsplatser.

---

## Problemet

Vi har en lista med parkeringsplatser. Några är lediga, några är upptagna. Vi vill ha de lediga.

```cs
var spots = new List<ParkingSpot>
{
    new("P1", 1), new("P2", 1), new("P3", 2),
    new("P4", null), new("P5", 3), new("P6", null)
};

record ParkingSpot(string Number, int? ResidentId);
```

Varje `new(...)` fyller i en parkeringsplats: `new(Number, ResidentId)`. `new("P1", 1)` är plats P1 som tillhör boende nummer 1. `new("P4", null)` är en plats som ingen har, alltså ledig.

Lägg märke till att `record ParkingSpot` står **sist** i filen. Filen har ingen `Main`-metod, utan raderna överst körs i tur och ordning. Då kräver C# att all kod som körs står före deklarationer av nya typer som `record` och `class`. Lägger du en `record` mitt bland raderna som körs får du ett kompileringsfel. Därför hamnar typerna alltid längst ned.

En plats utan boende (`ResidentId` är `null`) är ledig. Så här löser vi det med en loop:

```cs
// Så här gör man med en loop: vi berättar HUR, steg för steg
var free = new List<ParkingSpot>();
foreach (var spot in spots)
{
    if (spot.ResidentId == null)
        free.Add(spot);
}
```

Lägg märke till att loopen beskriver hur datorn ska arbeta: gå igenom en och en, titta, lägg till i en ny lista.

---

## Samma sak med LINQ

```cs
// Så här gör man med LINQ: vi berättar VAD vi vill ha
var free = spots.Where(spot => spot.ResidentId == null).ToList();
```

Nu kan vi se att koden läses nästan som en mening: "ur `spots`, ta de där `ResidentId` är `null`". Vi beskriver vad vi vill ha. Datorn bestämmer hur.

---

## Hur en LINQ-sökning är uppbyggd

En sökning har alltid samma form: en **källa**, ett eller flera **steg**, och ett **resultat**.

```cs
var names = spots                       // källa: listan vi söker i
    .Where(s => s.ResidentId == null)   // steg 1: behåll de lediga
    .OrderBy(s => s.Number)             // steg 2: sortera efter nummer
    .Select(s => s.Number)              // steg 3: plocka ut bara numret
    .ToList();                          // resultat: gör en ny lista
```

Läs den uppifrån och ned. Varje steg tar emot en lista och ger en ny lista vidare till nästa steg. Det är därför vi kan sätta ihop flera steg efter varandra i en kedja.

Hur de vanligaste stegen fungerar:

| Steg | Vad det gör |
|---|---|
| `Where` | Behåller de som uppfyller ett villkor |
| `Select` | Plockar ut eller omvandlar en del av varje objekt |
| `OrderBy` | Sorterar |
| `Count`, `Any` | Räknar, eller svarar ja och nej |
| `First` | Tar det första |

---

## Pilen `=>`

`s => s.ResidentId == null` kallas en **lambda**. Tänk på den som en liten funktion utan namn. Läs den så här: "för varje `s`, titta på `s.ResidentId == null`". Det namnet före pilen (`s`) väljer du själv.

---

## LINQ ändrar ingenting

Det här är den största skillnaden mot det vi gjort tidigare. När en hjälte tar skada ändrar objektet sig självt: `hero.Health` blir lägre. Det är objektorienterat.

LINQ gör tvärtom. Listan `spots` ändras aldrig. Varje steg ger en **ny** lista tillbaka. Den stilen kallas **funktionell programmering**: funktioner som tar in data och ger ny data ut, utan att ändra något på vägen.

| Objektorienterat | Funktionellt (LINQ) |
|---|---|
| Objekt med data och metoder | Funktioner som tar in data och ger data tillbaka |
| Objektet ändrar sig självt | Originalet ändras inte, du får en ny lista |
| "Hjälten tar skada" | "Ge mig alla hjältar med mer än 50 i hälsa" |

Du behöver inte välja sida. Ett C#-program använder båda, och det är just det som gör LINQ så smidigt att ha i verktygslådan.

---

## Samma sökning mot en databas

I EF skriver du precis samma sak, men källan är en tabell i databasen:

```cs
var free = db.ParkingSpots.Where(s => s.ResidentId == null).ToList();
```

EF översätter din LINQ till SQL åt dig och hämtar svaret. Du behöver alltså inte skriva SQL själv.

Tänk dock på att ToList() ska helst vara så långt bak i kommandolistan som möjligt, för den tvingar fram körning av anrop till databasen, så om du har flera ToList() i en sökning kommer du att utföra flera sökningar, vilket påverkar prestandan om många skulle köra samma program samtidigt.

---

## Det här kommer tillbaka

- Kedjan `.Where(...).Select(...)` med punkter är samma idé som **Builder**, där varje anrop ger tillbaka något som du kan fortsätta bygga på.
- `Where` och `Select` är i själva verket **extension methods**. Det är ett sätt att lägga till metoder på en befintlig typ, och vi ser hur det fungerar när vi kommer dit.

---

## Hela koden

```cs
var spots = new List<ParkingSpot>
{
    new("P1", 1), new("P2", 1), new("P3", 2),
    new("P4", null), new("P5", 3), new("P6", null)
};

// Med loop
var freeLoop = new List<ParkingSpot>();
foreach (var spot in spots)
{
    if (spot.ResidentId == null)
        freeLoop.Add(spot);
}

// Med LINQ
var free = spots.Where(spot => spot.ResidentId == null).ToList();

// En kedja med flera steg
var names = spots
    .Where(s => s.ResidentId == null)
    .OrderBy(s => s.Number)
    .Select(s => s.Number)
    .ToList();

Console.WriteLine(string.Join(", ", names)); // P4, P6

// Typer som record läggs sist i filen: all kod som körs måste stå före dem
record ParkingSpot(string Number, int? ResidentId);
```
