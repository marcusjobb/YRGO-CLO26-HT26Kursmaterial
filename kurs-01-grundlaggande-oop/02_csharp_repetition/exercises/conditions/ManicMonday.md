# Övning — Manic Monday

> 🗺️ **Rita ett flödesschema innan du kodar.** Skissa upp dominoeffekten på papper — vilken händelse leder till nästa? Rita klart, lägg ner pennan, öppna sedan VS Code.

🔴

Det är måndag. Allt som kan gå fel, går fel — en händelse i taget.

Du ska modellera en orsakskedja med booleska variabler. Varje händelse beror på den föregående. Programmet skriver ut berättelsen baserat på vad som faktiskt hände.

Ändra `vaknarForSent` och se hur hela kedjan förändras.

> Sitter du fast i mer än 15 minuter? Be om hjälp — det är inte ett tecken på att du inte kan, det är precis vad 15-minutersregeln är till för.

---

## Uppgiften

### Steg 1 — Deklarera orsakskedjan

```csharp
// Startpunkt — denna kan du ändra
bool vaknarForSent = true;

// Konsekvenskedjan — tilldelas från föregående bool
bool missarBussen  = vaknarForSent;
bool kommerForSent = missarBussen;
bool farStannar    = kommerForSent;   // läraren ringer hem, föräldern stannar dig
bool missarTentan  = farStannar;

// Alltid sant — det är alltid måndag
bool onskadeDetVoreFrisdag = true;
```

### Steg 2 — Skriv ut berättelsen med if/else

Skriv en `if`-sats för varje händelse. Om händelsen är `true` — skriv ut vad som hände.

**Den sista händelsen i kedjan måste ha en `else`:**

```
true  → "Du missar tentan. Det är manic måndag."
false → "Du hinner precis. Phew."
```

**Avsluta alltid med** (oavsett vad som hänt):

```
"Du önskar att det vore fredag. Det är alltid måndag."
```

---

## Förväntad output (vaknarForSent = true)

```plaintext
Du vaknar för sent. Larmet ringde inte.
Du missar bussen. Nästa går om 40 minuter.
Du kommer för sent till skolan.
Läraren ringer hem. Föräldern stannar dig.
Du missar tentan. Det är manic måndag.
Du önskar att det vore fredag. Det är alltid måndag.
```

## Förväntad output (vaknarForSent = false)

```plaintext
Du hinner precis. Phew.
Du önskar att det vore fredag. Det är alltid måndag.
```

---

<details><summary>Tips: kedjan kollapsar från start</summary>

Om `vaknarForSent` är `false` så är alla efterföljande booleans också `false` — de är tilldelade från varandra. Testa det! Hela historien förändras av ett enda värde.

</details>

<details><summary>Tips: sista if/else + alltid-raden</summary>

`missarTentan` behöver en `else`. Raden om fredag skrivs alltid ut — den behöver ingen `if`.

```csharp
if (missarTentan)
    Console.WriteLine("Du missar tentan. Det är manic måndag.");
else
    Console.WriteLine("Du hinner precis. Phew.");

Console.WriteLine("Du önskar att det vore fredag. Det är alltid måndag.");
```

</details>

<details><summary>Lösningsförslag</summary>

```csharp
bool vaknarForSent = true;

bool missarBussen  = vaknarForSent;
bool kommerForSent = missarBussen;
bool farStannar    = kommerForSent;
bool missarTentan  = farStannar;

bool onskadeDetVoreFrisdag = true;

if (vaknarForSent)
    Console.WriteLine("Du vaknar för sent. Larmet ringde inte.");

if (missarBussen)
    Console.WriteLine("Du missar bussen. Nästa går om 40 minuter.");

if (kommerForSent)
    Console.WriteLine("Du kommer för sent till skolan.");

if (farStannar)
    Console.WriteLine("Läraren ringer hem. Föräldern stannar dig.");

if (missarTentan)
    Console.WriteLine("Du missar tentan. Det är manic måndag.");
else
    Console.WriteLine("Du hinner precis. Phew.");

Console.WriteLine("Du önskar att det vore fredag. Det är alltid måndag.");
```

</details>
