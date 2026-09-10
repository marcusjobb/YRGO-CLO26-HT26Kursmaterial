# Övning — Filmkvällen

> 🗺️ **Rita ett flödesschema innan du kodar.** Skissa upp programflödet på papper — vilka steg tas? Vilka beslut fattas? Rita klart, lägg ner pennan, öppna sedan VS Code.

Du och fyra kompisar ska ha filmkväll hemma hos dig. Någon måste hålla koll på vad det kostar — och det blir du.

---

## Flödesschema

![Diagram](diagrams/filmkvaellen_1.png)

<!-- mermaid: diagrams/filmkvaellen_1.mmd -->

## Kodning

---

## Steg 1: Definiera kvällen

```csharp
int antalPersoner = 5;
int popcorn = 89;
int chips = 45;
int lask = 35;      // per person
int pizzaTotal = 650;
```

Skriv ut vad varje sak kostar.

### Förväntad output
```plaintext
Popcorn (stor hink):  89 kr
Chips (stor påse):    45 kr
Läsk per person:      35 kr
Pizza (delat på 5): 650 kr
```

---

## Steg 2: Total dryckeskostnad

Räkna ut vad läsken kostar totalt för alla fem.

### Förväntad output
```plaintext
Total läsk: ### kr
```

<details><summary>Tips</summary>

```csharp
int totalLask = lask * antalPersoner;
```

</details>

---

## Steg 3: Totalkostnad för kvällen

Räkna ihop allt — popcorn, chips, all läsk och pizzan.

### Förväntad output
```plaintext
Total kvällskostnad: ### kr
```

---

## Steg 4: Per person

Vad kostar kvällen per person om ni delar lika?

### Förväntad output
```plaintext
Per person: ### kr
```

<details><summary>Division — vad händer med resten?</summary>

```csharp
int perPerson = totalt / antalPersoner;
```

Om totalt inte är jämnt delbart försvinner decimalen (heltalsdivision).  
Hur mycket "försvinner"? Prova räkna `totalt % antalPersoner` — `%` ger resten.

</details>

---

## Steg 5: Kvittot

Skriv ut ett snyggt kvitto.

### Förväntad output
```plaintext
=== Filmkvällen ===
Popcorn:    89 kr
Chips:      45 kr
Läsk (x5): ### kr
Pizza:     650 kr
-----------------
Totalt:    ### kr
Per person: ### kr
```

<details><summary>Lösningsförslag</summary>

```csharp
int antalPersoner = 5;
int popcorn = 89;
int chips = 45;
int lask = 35;
int pizzaTotal = 650;

int totalLask = lask * antalPersoner;
int totalt = popcorn + chips + totalLask + pizzaTotal;
int perPerson = totalt / antalPersoner;

Console.WriteLine("=== Filmkvällen ===");
Console.WriteLine($"Popcorn:    {popcorn} kr");
Console.WriteLine($"Chips:      {chips} kr");
Console.WriteLine($"Läsk (x{antalPersoner}): {totalLask} kr");
Console.WriteLine($"Pizza:     {pizzaTotal} kr");
Console.WriteLine("-----------------");
Console.WriteLine($"Totalt:    {totalt} kr");
Console.WriteLine($"Per person: {perPerson} kr");
```

</details>

## Bonusuppgift

En av dina kompisar är vegetarian och vill ha en egen pizza för 120 kr extra. En annan dricker inte läsk och betalar bara pizza och snacks. Hur ändrar du beräkningen?

(Ledning: du behöver inte en if-sats — bara fler variabler.)
