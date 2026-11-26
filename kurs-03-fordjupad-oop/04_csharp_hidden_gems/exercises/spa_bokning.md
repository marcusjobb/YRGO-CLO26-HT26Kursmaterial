# Spabokning med två entiteter

**Mål:** Koppla en bokning till en behandling, och visa båda i samma fråga med `Include`.

**Tid:** ca 90 minuter.

**Förkunskap:** Relationer och migrationer från föregående vecka.

Spa Grandioso säljer behandlingar. En gäst bokar en behandling på ett datum. Varje bokning pekar alltså på en behandling, och det gör den med ett främmande nyckel.

## Modell

```csharp
class Treatment
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int DurationMinutes { get; set; }
    public decimal Price { get; set; }
}

class SpaBooking
{
    public int Id { get; set; }
    public int GuestId { get; set; }
    public DateTime Date { get; set; }
    public int TreatmentId { get; set; }
    public Treatment Treatment { get; set; }
}
```

## Del 1: 🟢 Lägg in behandlingarna

1. Skapa en `DbContext` med `Treatments` och `SpaBookings`.
2. Lägg in tre behandlingar i databasen, till exempel Massage (60 min, 950 kr) och Pedikyr (stortån, 350 kr).
3. Gör en migration och kör den.

Kontrollera: finns det tre rader i tabellen `Treatments`?

## Del 2: 🟡 Boka och visa

1. Skriv en metod som bokar en behandling för en gäst på ett givet datum. Metoden tar emot `TreatmentId`, inte hela objektet.
2. Visa alla spabokningar med behandlingens namn och pris. Använd `Include`.

<details markdown="1">
<summary>Tips</summary>

Utan `Include` är `Treatment` `null` när du läser `SpaBooking`. Det är ett av de vanligaste felen.

</details>

## Del 3: 🔴 Kontrollera tiden

En behandling får inte bokas två gånger samma dag för samma gäst. Skriv en kontroll som stoppar det, och förklara i en kommentar på svenska varför den gör det.

## Reflektion

Varför lagrar vi `TreatmentId` i `SpaBooking` i stället för hela behandlingens namn och pris? Vad händer om priset ändras?

<!-- nion-copyright -->
<sub>© 2025–2026 Nion Education. Alla rättigheter förbehållna. Fritt att läsa, men får inte användas av andra skolor eller utbildningsanordnare utan skriftligt tillstånd från Nion Education. Delar bygger på källor som anges i respektive dokument.</sub>
