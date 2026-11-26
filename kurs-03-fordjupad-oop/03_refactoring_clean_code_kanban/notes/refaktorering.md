# Refaktorering: Extract Method och Rename

Refaktorering är att ändra hur koden ser ut utan att ändra vad den gör. Det är den viktigaste regeln i hela avsnittet. Efter varje steg ska programmets utskrift vara densamma som innan.

Den här artikeln går igenom två tekniker: **Extract Method** och **Rename**. Båda är små steg som är lätta att göra och lätta att kontrollera.

## Regeln

Om utskriften ändras har du inte refaktorerat. Du har ändrat beteendet, och då är det en annan sorts arbete.

Kör därför programmet efter varje steg och jämför med det du såg innan du började. Det tar några sekunder, och det är det som gör att du vågar ändra koden.

## Extract Method

Extract Method betyder att du flyttar en del av en metod till en egen metod, med ett namn som säger vad den gör.

Här är en metod som räknar ut moms i en offert:

```csharp
decimal CalculateVat(decimal sum) => sum * VatRate;
```

Namnet `CalculateVat` säger vad raden gör. Den som läser metoden där den används behöver inte räkna ut vad `sum * VatRate` betyder. Det räcker att läsa namnet.

Så här gör du:

1. Markera en del av metoden som gör en tydlig sak.
2. Flytta den till en ny metod.
3. Ge metoden ett namn som beskriver vad den gör, inte hur.
4. Anropa den nya metoden från den gamla platsen.
5. Kör programmet och jämför utskriften.

## Rename: namnet är dokumentation

Ett bra namn behöver ingen kommentar. Jämför:

```csharp
int x = 3;           // Vad är x?
int ticketCount = 3; // Det är biljetter.
```

Kommentaren i den första raden behövs för att förstå koden. I den andra behövs ingen, eftersom namnet säger det. Byt namn innan du skriver en kommentar. Ofta visar det sig att kommentaren inte behövs alls.

Använd din editor för att byta namn. Då uppdateras alla ställen där variabeln används. Kör sedan programmet igen.

## Så hänger det ihop

Extract Method och Rename brukar användas tillsammans. Du flyttar en del till en metod, och ger både metoden och de variabler den använder bra namn. Resultatet är kod som går att läsa utan att någon behöver fråga vad den gör.

## Övning

Gå till `exercises/refaktorering_extract_method.md`. Övningen har tre nivåer:

- 🟢 Extract Method
- 🟡 Rename
- 🔴 Kombinera båda

<!-- nion-copyright -->
<sub>© 2025–2026 Nion Education. Alla rättigheter förbehållna. Fritt att läsa, men får inte användas av andra skolor eller utbildningsanordnare utan skriftligt tillstånd från Nion Education. Delar bygger på källor som anges i respektive dokument.</sub>
