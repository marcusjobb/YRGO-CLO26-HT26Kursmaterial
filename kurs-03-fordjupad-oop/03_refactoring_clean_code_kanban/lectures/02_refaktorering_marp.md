---
marp: true
theme: nion-dark
paginate: true
---

## Hjulet idag

Elin bryter ut en metod och byter namn på `x` till `hours`. Nils ser över axeln: "Det var det jag menade."

---

# Refaktorering: Extract Method och Rename

**Vecka 47 · Måndag · Lektion 2 (10:00)**

---

## Regeln

Refaktorering ändrar hur koden ser ut, inte vad den gör. Efter varje steg ska utskriften vara densamma.

---

## Extract Method

Flytta en del till en egen metod med ett namn som säger vad den gör.

```csharp
decimal CalculateVat(decimal sum) => sum * VatRate;
```

Kör programmet efter varje steg och jämför utskriften.

---

## Rename: namnet är dokumentation

```csharp
int x = 3;           // Vad är x?
int ticketCount = 3; // Det är biljetter.
```

Byt namn innan du skriver en kommentar. Ofta behövs ingen kommentar alls.

---

## Live: en metod, tre jobb

Visa en lång metod på skärmen. Låt salen säga vad den gör, ett jobb i taget.

Dela sedan upp den live. Samma utskrift, men varje del har ett namn.

---

## Övning

Gå till `exercises/refaktorering_extract_method.md`.

🟢 Extract Method · 🟡 Rename · 🔴 Kombinera
