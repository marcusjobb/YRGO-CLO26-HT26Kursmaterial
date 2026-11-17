# Kodgranskning med checklista

**Mål:** Granska kod systematiskt med en checklista. Du hittar fel genom att köra koden, testa gränsfallen och jämföra mot kraven.

**Tid:** ca 2 timmar.

**Förkunskap:** Code smells och SOLID/DRY/KISS från notes/.

Koden nedan är skriven med medvetna fel. Det är meningen. Du ska hitta dem, på samma sätt som du ska hitta fel i era egna inlämningar. Gå tillbaka till listan över *vad man inte ska göra på inlämningar* och tänk på hur du testar det.

## Projekt A: Förseningsavgift på bibliotekslån

**Kravspec:**
- Ett lån gäller i **14 dagar**.
- Varje dag efter förfallodagen kostar **5 kr**.
- Avgiften är högst **100 kr**.
- Lämnas boken i tid är avgiften **0 kr**.

**Varningar:**
- Avgiften får **aldrig vara negativ**. En bok som lämnas tidigt ger ingen återbetalning.
- Programmet får **inte krascha** på felaktig inmatning.
- Hårdkoda inte 14, 5 eller 100 i beräkningen. Det ska gå att ändra på ett ställe.

Startkoden ligger i repot: https://github.com/marcusjobb/clo26-kodgranskning-biblioteket (kör med `dotnet run`).

## Projekt B: Parkeringsavgift

**Kravlista att verifiera:**
1. Parkering under **15 minuter** är gratis.
2. Den första timmen kostar **20 kr**.
3. Varje **påbörjad** timme efter den första kostar **15 kr**. En påbörjad timme räknas som en hel timme, alltså är 61 minuter två timmar.
4. Avgiften är högst **150 kr**.
5. Negativ tid ska ge ett felmeddelande.

Startkoden ligger i repot: https://github.com/marcusjobb/clo26-kodgranskning-parkering (kör med `dotnet run`).

## Del 1: 🟢 Kör båda

1. Kopiera varje program till ett eget projekt och kör det.
2. Skriv ner vad som händer med de vanliga värdena. Stämmer utskriften?

## Del 2: 🟡 Testa gränsfallen

Testa minst dessa indata, och skriv ner vad programmet gör och vad det *borde* göra:

- **Förseningsavgiften:** `14`, `15`, `34`, `35`, `-3`, och `abc`.
- **Parkeringsavgiften:** `14`, `15`, `60`, `61`, `120`, `-30`, och `abc`.

Vilka av dem ger ett fel som du kan se i utskriften? Vilka ger fel utan att du märker det?

## Del 3: 🔴 Skriv din egen checklista

Skriv en checklista med **minst fem punkter** som du själv skulle använda vid kodgranskning. Varje punkt ska gå att kontrollera med ja eller nej. Exempel: "Stämmer gränsen mot kravet, med rätt jämförelse?"

Använd din checklista på båda programmen. Skriv ner varje fel du hittar, och vilken punkt i checklistan som fångade det.

## Del 4: 🔴 Par: granska tillsammans

Jobba i par. Byt roll efter varje projekt.

- **Förare:** kör programmet och skriver in indata. Förklarar vad som händer.
- **Granskare:** följer checklistan och ställer frågor. Skriver ner felen.

**Projekt A:** den ena är förare, den andra granskare. Byt sedan.
**Projekt B:** samma sak, med rollerna omvända.

Efter båda projekten:
1. Jämför era checklistor. Vilka punkter har ni olika?
2. Enas om en gemensam lista med felen.
3. Skriv **en motivering per fel**: varför är det ett fel, och vad är konsekvensen om det släpps igenom? Det är samma sak som en riktig kodgranskning kräver av dig.
4. Om ni är oense om ett fel, skriv båda åsikterna och argumentera för din. Ni behöver inte vara överens, men ni ska kunna förklara varför.

## Reflektion

Vilket fel var svårast att se utan att köra koden? Vilken punkt i din checklista var mest användbar?

---

<details markdown="1">
<summary>Lösningsförslag: fel i förseningsavgiften</summary>

1. **Negativ avgift:** Lämnas boken före förfallodagen blir `daysLate` negativt, och då blir avgiften negativ. Kravet är 0 kr.
2. **Ingen kontroll av inmatning:** `int.Parse` kraschar på `abc`.
3. **Hårdkodade värden:** 14, 5 och 100 står mitt i beräkningen.

**Rättat:**
```csharp
const int LoanDays = 14;
const int FeePerDay = 5;
const int MaxFee = 100;

Console.Write("Antal dagar boken var utlånad: ");
if (!int.TryParse(Console.ReadLine(), out int daysBorrowed) || daysBorrowed < 0)
{
    Console.WriteLine("Felaktigt antal dagar.");
    return;
}

int daysLate = Math.Max(0, daysBorrowed - LoanDays);
int fee = Math.Min(daysLate * FeePerDay, MaxFee);
Console.WriteLine($"Avgift: {fee} kr");
```

</details>

<details markdown="1">
<summary>Lösningsförslag: fel i parkeringsavgiften</summary>

1. **Påbörjad timme räknas inte:** `minutes / 60` avrundar nedåt. 61 minuter ger en timme, och kravet säger två.
2. **Negativ tid godtas:** `-30` ger 0 kr och ett felmeddelande saknas. Kravet säger felmeddelande.
3. **Ingen felhantering:** `abc` kraschar programmet.

**Rättat:**
```csharp
Console.Write("Parkerad tid i minuter: ");
if (!int.TryParse(Console.ReadLine(), out int minutes) || minutes < 0)
{
    Console.WriteLine("Felaktig tid. Ange minuter, 0 eller större.");
    return;
}

if (minutes < 15)
{
    Console.WriteLine("Avgift: 0 kr");
    return;
}

int startedHours = (minutes + 59) / 60;
int fee = Math.Min(20 + (startedHours - 1) * 15, 150);
Console.WriteLine($"Avgift: {fee} kr");
```

</details>
