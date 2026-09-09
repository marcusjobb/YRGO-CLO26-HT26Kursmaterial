# Övning — Betty

> 🗺️ **Rita ett flödesschema innan du kodar.** Skissa upp orsakskedjan på papper — vilka beslut leder till vilka konsekvenser? Rita klart, lägg ner pennan, öppna sedan VS Code.

🔴

En kärlekstriangel i tre akter — inspirerad av ett välkänt tonårsdrama.

Du ska modellera en händelsekedja med booleska variabler. Varje händelse beror på en tidigare. Programmet skriver ut berättelsen baserat på vad som faktiskt hände.

Ändra startvärdena och se hur hela historien förändras.

> Sitter du fast i mer än 15 minuter? Be om hjälp — det är inte ett tecken på att du inte kan, det är precis vad 15-minutersregeln är till för.

---

## Uppgiften

### Steg 1 — Deklarera orsakskedjan

```csharp
// Startpunkter — dessa kan du ändra
bool bettyDansarMedNagon   = true;
bool jamesLamnarAugustine  = true;
bool bettyForlaterJames    = true;

// Konsekvenskedjan — tilldelas från andra bools
// Man kan se det som så, varje val har konsekvens
bool jamesSerDem              = bettyDansarMedNagon;
bool augustinePickarUppJames  = jamesSerDem;
bool jamesOchAugustineHookupp = augustinePickarUppJames;
bool inezSerDem               = jamesOchAugustineHookupp;
bool inezBeratterForBetty     = inezSerDem;
bool jamesDykerUppVidGrinden  = jamesLamnarAugustine;
```

### Steg 2 — Skriv ut berättelsen med if/else

Skriv en `if`-sats för varje händelse. Om händelsen är `true` — skriv ut vad som hände. Om den är `false` — skriv ut att händelsen inte inträffade (eller hoppa bara över den).

**Den sista händelsen måste ha en `else`:**

```
true  → "Betty tar hans hand. De dansar igen."
false → "Betty stänger grinden. 'Gå härifrån, James.'"
```

---

## Förväntad output (alla startvariabler = true)

```plaintext
Betty dansar med någon annan på festen.
James ser dem. Hjärtat brister.
Augustine hittar James ensam utanför. "Du ser ledsen ut."
De tillbringar sommaren tillsammans — James och Augustine.
Inez ser dem. Hon kan inte hålla tyst.
Betty får reda på allt. Via Inez. Såklart.
James lämnar Augustine. Han ångrar sig.
James dyker upp vid Bettys grind. Han är nervös.
Betty tar hans hand. De dansar igen.
```

## Förväntad output (bettyForlaterJames = false)

```plaintext
...
James dyker upp vid Bettys grind. Han är nervös.
Betty stänger grinden. "Gå härifrån, James."
```

## Förväntad output (bettyDansarMedNagon = false)

```plaintext
Betty stannade hemma. Ingenting hände.
```

---

<details><summary>Tips: kedjan startar och slutar</summary>

Om `bettyDansarMedNagon` är `false` så är alla konsekvenser också `false` — ingen if-sats längre ned i kedjan triggas. Det kallas kortslutning i logiken: orsaken saknas, effekten uteblir.

Testa! Sätt `bettyDansarMedNagon = false` och se vad som händer med resten.

</details>

<details><summary>Tips: sista if/else</summary>

```csharp
if (bettyForlaterJames && jamesDykerUppVidGrinden)
    Console.WriteLine("Betty tar hans hand. De dansar igen.");
else if (jamesDykerUppVidGrinden)
    Console.WriteLine("Betty stänger grinden. \"Gå härifrån, James.\"");
```

</details>

<details><summary>Lösningsförslag</summary>

```csharp
bool bettyDansarMedNagon   = true;
bool jamesLamnarAugustine  = true;
bool bettyForlaterJames    = true;

bool jamesSerDem              = bettyDansarMedNagon;
bool augustinePickarUppJames  = jamesSerDem;
bool jamesOchAugustineHookupp = augustinePickarUppJames;
bool inezSerDem               = jamesOchAugustineHookupp;
bool inezBeratterForBetty     = inezSerDem;
bool jamesDykerUppVidGrinden  = jamesLamnarAugustine;

if (bettyDansarMedNagon)
    Console.WriteLine("Betty dansar med någon annan på festen.");
else
    Console.WriteLine("Betty stannade hemma. Ingenting hände.");

if (jamesSerDem)
    Console.WriteLine("James ser dem. Hjärtat brister.");

if (augustinePickarUppJames)
    Console.WriteLine("Augustine hittar James ensam utanför. \"Du ser ledsen ut.\"");

if (jamesOchAugustineHookupp)
    Console.WriteLine("De tillbringar sommaren tillsammans — James och Augustine.");

if (inezSerDem)
    Console.WriteLine("Inez ser dem. Hon kan inte hålla tyst.");

if (inezBeratterForBetty)
    Console.WriteLine("Betty får reda på allt. Via Inez. Såklart.");

if (jamesLamnarAugustine)
    Console.WriteLine("James lämnar Augustine. Han ångrar sig.");

if (jamesDykerUppVidGrinden)
    Console.WriteLine("James dyker upp vid Bettys grind. Han är nervös.");

if (bettyForlaterJames && jamesDykerUppVidGrinden)
    Console.WriteLine("Betty tar hans hand. De dansar igen.");
else if (jamesDykerUppVidGrinden)
    Console.WriteLine("Betty stänger grinden. \"Go F**k yourself James.\"");
```

</details>
