# Refaktorering: Extract Method och Rename

**Mål:** Förbättra kod utan att ändra vad den gör. Du använder två säkra steg: **Extract Method** och **Rename**.

**Tid:** ca 45 minuter.

**Förkunskap:** Code smells från lektion 01.

**Berättelsen:** Nils har skrivit offertkoden i bokningsappen på Hjulet. Den fungerar, men Elin måste kunna ändra den utan att vara rädd. Det är hennes uppgift i dag.

## Regeln

Refaktorering ändrar hur koden ser ut, inte vad den gör. Efter varje steg ska utskriften vara densamma. Kontrollera det innan du går vidare.

## Del 1: 🟢 Extract Method

Nils offertkod gör för mycket i en metod:

```csharp
void PrintQuote(string customerName, int hours, decimal partsPrice)
{
    decimal labor = hours * 650m;
    decimal sum = labor + partsPrice;
    decimal vat = sum * 0.25m;
    decimal total = sum + vat;

    Console.WriteLine($"Kund: {customerName}");
    Console.WriteLine($"Arbete: {labor} kr");
    Console.WriteLine($"Delar: {partsPrice} kr");
    Console.WriteLine($"Moms: {vat} kr");
    Console.WriteLine($"Att betala: {total} kr");
}
```

**Uppgift:**

1. Flytta uträkningen av momsen till en egen metod som heter `CalculateVat`.
2. Flytta uträkningen av totalen till en egen metod som heter `CalculateTotal`.
3. Kör programmet och kontrollera att utskriften är densamma.

<details markdown="1">
<summary>Tips</summary>

En ny metod får ett namn som beskriver *vad* den gör. Om du inte kan namnge den, har du inte förstått vad du flyttar.

</details>

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
void PrintQuote(string customerName, int hours, decimal partsPrice)
{
    decimal labor = hours * 650m;
    decimal sum = labor + partsPrice;
    decimal vat = CalculateVat(sum);
    decimal total = CalculateTotal(sum, vat);

    Console.WriteLine($"Kund: {customerName}");
    Console.WriteLine($"Arbete: {labor} kr");
    Console.WriteLine($"Delar: {partsPrice} kr");
    Console.WriteLine($"Moms: {vat} kr");
    Console.WriteLine($"Att betala: {total} kr");
}

decimal CalculateVat(decimal sum)
{
    return sum * 0.25m;
}

decimal CalculateTotal(decimal sum, decimal vat)
{
    return sum + vat;
}
```

Det kan vara fler lösningar. Det viktiga är att utskriften är densamma.

</details>

## Del 2: 🟡 Rename

Elin hittar den här raden i Nils kod. Vad tror du den räknar ut?

```csharp
int t = 3;
decimal p = 650m;
decimal s = t * p;
```

<details markdown="1">
<summary>Svar</summary>

Det är arbetskostnaden för tre timmar à 650 kr. `s` är 1 950 kr, innan moms och delar.

</details>

**Uppgift:** Byt `t`, `p` och `s` till namn som beskriver vad de innehåller. Kör programmet efter varje byte.

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
int hours = 3;
decimal hourlyRate = 650m;
decimal labor = hours * hourlyRate;
```

</details>

## Del 3: 🔴 Kombinera

Ta offertkoden från del 1, och byt namn på allt som inte är tydligt. Lägg till en metod som räknar ut rabatt för stamkunder, och förklara i en kommentar varför namnet är bättre än det gamla.

## Reflektion

Skriv tre meningar: Vilket steg kändes säkrast? Var det svårt att hitta bra namn? När skulle du *inte* göra Extract Method?
