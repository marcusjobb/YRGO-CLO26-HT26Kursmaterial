---
marp: true
theme: nion-dark
paginate: true
---

<!-- _class: title -->

# switch

### När du vet exakt vilka värden som gäller

_Kurs 01 · Vecka 3 · Nion Education_

---

## När switch slår if/else

`if/else` är bra när du jämför intervall eller komplexa villkor.
`switch` är bättre när du har **ett fåtal kända, fasta värden**.

```csharp
// if/else — fungerar, men ordigt
if (dag == "Måndag") Console.WriteLine("Veckostart!");
else if (dag == "Fredag") Console.WriteLine("Snart helg!");
else if (dag == "Lördag") Console.WriteLine("Helg!");
else Console.WriteLine("Vanlig dag.");

// switch — tydligare struktur
switch (dag) {
    case "Måndag": Console.WriteLine("Veckostart!"); break;
    case "Fredag": Console.WriteLine("Snart helg!"); break;
    case "Lördag": Console.WriteLine("Helg!"); break;
    default: Console.WriteLine("Vanlig dag."); break;
}
```

---

## switch — grundstruktur

```csharp
int betyg = 4;

switch (betyg) {
    case 5:
        Console.WriteLine("Utmärkt!");
        break;
    case 4:
        Console.WriteLine("Bra jobbat!");
        break;
    case 3:
        Console.WriteLine("Godkänt.");
        break;
    default:
        Console.WriteLine("Ogiltigt betyg.");
        break;
}
```

`case` matchar ett exakt värde. `default` körs om inget case stämmer.

> 💬 _"`default` är som `else` — det är säkerhetsventilen när inget annat passar."_

---

## break — varför den måste vara där

`break` avslutar `switch`-blocket och hoppar ut.

```csharp
switch (betyg) {
    case 5:
        Console.WriteLine("Utmärkt!");
        break;   // ← utan den här fortsätter C# ner till case 4
    case 4:
        Console.WriteLine("Bra jobbat!");
        break;
}
```

```
Utan break:
  case 5 matchar → skriver "Utmärkt!"
                 → faller igenom till case 4
                 → skriver "Bra jobbat!" också  ← fel!
```

**Glömd `break` är ett av de vanligaste nybörjarmisstagen.**

---

## Fall-through — när det ser ut som en fälla

I vissa språk (som C) är fall-through avsiktligt.
I C# ger kompilatorn ett **fel** om du försöker:

```csharp
switch (betyg) {
    case 5:
        Console.WriteLine("Utmärkt!");
        // KOMPILERINGSFEL — C# kräver break eller return
    case 4:
        Console.WriteLine("Bra!");
        break;
}
```

Det enda undantaget: tomma case-grenar kan staplas:

```csharp
case 4:
case 5:
    Console.WriteLine("Högt betyg!");   // körs för 4 och 5
    break;
```

---

## switch expression — modern C#

C# 8 introducerade ett kortare sätt att skriva switch:

```csharp
// Klassisk switch (sats)
switch (betyg) {
    case 5: return "Utmärkt";
    case 4: return "Bra";
    default: return "Ogiltigt";
}

// Switch expression (uttryck)
string omdöme = betyg switch {
    5 => "Utmärkt",
    4 => "Bra",
    _ => "Ogiltigt"
};
```

`_` är wildcard — motsvarar `default`.
Switch expression **returnerar ett värde** direkt.

> 💬 _"Den moderna versionen är snyggare, men båda fungerar. Välj den du förstår bäst just nu."_

---

## switch expression — jämförelse sida vid sida

```csharp
// Klassisk — fler rader, tydligare struktur
switch (dag) {
    case "Lördag":
    case "Söndag":
        Console.WriteLine("Helg!");
        break;
    default:
        Console.WriteLine("Vardag.");
        break;
}

// Modern — kortare, returnerar värde
string typ = dag switch {
    "Lördag" or "Söndag" => "Helg!",
    _                    => "Vardag."
};
Console.WriteLine(typ);
```

---

## switch vs if — när väljer du vad?

| Situation | Välj |
|-----------|------|
| Exakta, kända värden (1, 2, 3 eller "a", "b") | `switch` |
| Intervall (`>= 70`, `< 18`) | `if/else` |
| Komplexa villkor med `&&` och `||` | `if/else` |
| Vill ha ett kort uttryck som returnerar värde | `switch expression` |
| Fler än ~5-6 grenar med fasta värden | `switch` — lättare att läsa |

> 💬 _"Båda lösningarna är ofta rätt. Switch signalerar till läsaren: 'det här är ett fast antal välkända alternativ'."_

---

## Sammanfattning

```csharp
switch (variabel) {
    case värde1: ... break;
    case värde2: ... break;
    default:     ... break;
}

// Modern form
string resultat = variabel switch {
    värde1 => "...",
    värde2 => "...",
    _      => "..."
};
```

Tre regler:
1. Glöm inte `break` i klassisk switch
2. `default` / `_` är din säkerhetsventil
3. Switch passar fasta värden — if/else passar intervall

---

<!-- _class: title -->

# Nu är det din tur

### Övningar finns i `exercises/`

🟡 `conditions/character_class.md` — klassifiera ett tecken med switch

_Ta det steg för steg. Använd tipsen om du fastnar._
