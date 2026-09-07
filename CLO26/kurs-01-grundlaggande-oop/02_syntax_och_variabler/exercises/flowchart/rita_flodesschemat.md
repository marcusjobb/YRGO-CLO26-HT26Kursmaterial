# Övning — Rita flödesschemat

> 🗺️ **Rita ett flödesschema innan du kodar.** Skissa upp programflödet på papper — vilka steg tas? Vilka beslut fattas? Rita klart, lägg ner pennan, öppna sedan VS Code.

**Den här övningen handlar om att tänka, inte koda.**  
Penna och papper. Ingen dator.

---

## Vad är ett flödesschema?

Ett flödesschema visar hur ett program flödar — steg för steg, beslut för beslut. Det är det vanligaste sättet att planera kod innan man skriver den.

Tre grundformer räcker långt:

| Symbol | Betyder |
|--------|---------|
| Rektangel | En åtgärd (deklarera, räkna, skriva ut) |
| Romb | Ett beslut (`if`-sats — ja eller nej) |
| Pil | Flödesriktning |

---

## Uppgift 1 — Morgonrutinen

Rita ett flödesschema för din morgon, från larm till att du lämnar hemmet.

Tänk på:
- Vilka beslut fattar du? (`if` det regnar → ta paraply)
- Vilka steg är alltid samma? (tvätta tänder)
- Finns det loopar? (snooze-knappen…)

Minst 5 steg, minst 2 beslut.

---

## Uppgift 2 — Kaffeautomaten

Rita ett flödesschema för hur en kaffeautomat fungerar.

1. Kunden väljer dryck
2. Kunden matar in mynt
3. Automaten kontrollerar om det räcker
4. Automaten levererar dryck (eller ger tillbaka pengarna)

Tänk på: vad händer om mynten inte räcker? Vad händer om drycken är slut?

---

## Uppgift 3 — Busskorten

Rita ett flödesschema för ett system som kontrollerar om en resenär får åka med bussen.

- Resenären scannar kortet
- Systemet kontrollerar saldot
- Om saldot räcker → dra av priset, öppna grindarna
- Om saldot inte räcker → neka, visa fel

---

## Kopplingen till kod

Titta på ditt flödesschema för uppgift 3. Varje rektangel är en kodrad. Varje romb är en `if`-sats.

Hur många rader kod tror du att programmet kräver?  
Skriv ner din gissning. Vi återkommer.

<details><summary>Hur ser det ut i kod?</summary>

```csharp
int saldo = 85;
int prisBuss = 39;

Console.WriteLine($"Saldo: {saldo} kr");

if (saldo >= prisBuss)
{
    saldo -= prisBuss;
    Console.WriteLine("Välkommen ombord!");
    Console.WriteLine($"Nytt saldo: {saldo} kr");
}
else
{
    Console.WriteLine("Otillräckligt saldo. Fyll på kortet.");
}
```

Stämde din gissning?

</details>
