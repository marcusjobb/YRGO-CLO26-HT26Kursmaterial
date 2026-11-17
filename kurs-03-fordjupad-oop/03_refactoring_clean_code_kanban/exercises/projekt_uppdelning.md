# Projekt: dela upp bokningsappen i klasser och metoder

**Mål:** Ta en beskrivning av ett litet projekt och dela upp det i klasser och metoder, med en kort text om vad varje del ansvarar för.

**Tid:** ca 90 minuter.

**Förkunskap:** SRP, SoC och de tidigare övningarna om refaktorering och Builder/Factory.

**Berättelsen:** Hjulet vill ha en enkel bokningsapp. Kunder bokar en tid, verkstaden ser dagens bokningar och fakturan skrivs när jobbet är klart. Appen är i konsolen och ska vara enkel nog att en ny kollega kan förstå den första dagen.

## Projektbeskrivning

Appen ska kunna:

1. Ta emot en bokning: kundens namn, registreringsnummer, bilmodell och vilket jobb som ska göras.
2. Lista dagens bokningar i en ordning, sorterat på tid.
3. Räkna ut priset för en bokning, med moms.
4. Spara bokningarna i en fil, så att de finns kvar när programmet startas om.
5. Skriva ut en enkel faktura för en avslutad bokning.

## Del 1: 🟢 Hitta substantiven

Läs beskrivningen. Skriv upp alla substantiv som verkar bli en klass. Stryk de som är samma sak med andra ord. Det ska bli 3 till 6 klasser.

<details markdown="1">
<summary>Tips</summary>

Ett substantiv som bara är ett tal eller en text, till exempel "pris", blir oftast ett fält i en klass och inte en egen klass.

</details>

## Del 2: 🟡 Dela upp i klasser och metoder

För varje klass: skriv klassnamnet, 2 till 4 metoder med en signatur, och en mening om vad klassen ansvarar för. Om en klass har mer än ett ansvar, dela den.

Använd den här mallen:

```markdown
### Klassnamn
**Ansvar:** Vad klassen gör, i en mening.

| Metod | Vad den gör |
|---|---|
| `MetodNamn(parametrar)` | Vad metoden gör |
```

<details markdown="1">
<summary>Tips</summary>

Om du har skrivit "och" i ansvarsmeningen har du troligen två klasser. Delning är rätt när en av delarna kan ändras utan att den andra påverkas.

</details>

## Del 3: 🔴 Granska mot SRP och SoC

Gå igenom din uppdelning och svara på tre frågor:

1. Vilken klass ändras oftast om reglerna för moms ändras? Ska den klassen göra något mer än momsen?
2. Vilken klass vet om konsolen? Borde den vara den enda?
3. Om du lägger till betalning med kort, vilka klasser behöver ändras, och vilka behöver inte ändras?

## Reflektion

Skriv tre meningar: Vilken klass var svårast att avgränsa? Hade du gjort uppdelningen annorlunda utan SRP? Vad skulle du ändra om Hjulet ville ha en webbversion?

<details markdown="1">
<summary>Lösningsförslag</summary>

Det här är ett exempel. Din uppdelning kan se annorlunda ut, så länge varje klass har ett ansvar och du kan motivera det.

### Booking
**Ansvar:** Håller uppgifterna om en bokning, och vet inte hur den sparas eller skrivs ut.

| Metod | Vad den gör |
|---|---|
| `Booking(customerName, licensePlate, carModel, job, time)` | Skapar en bokning |

### BookingBuilder
**Ansvar:** Bygger en `Booking` steg för steg, med kontroller.

| Metod | Vad den gör |
|---|---|
| `Build()` | Kontrollerar att kund finns och returnerar en `Booking` |

### PriceCalculator
**Ansvar:** Räknar ut priset för ett jobb, inklusive moms. Den är den enda som känner till momssatsen.

| Metod | Vad den gör |
|---|---|
| `CalculateTotal(job)` | Returnerar priset inklusive moms |

### BookingRepository
**Ansvar:** Sparar och läser bokningar från fil. Den enda som vet att det är en fil.

| Metod | Vad den gör |
|---|---|
| `Save(booking)` | Sparar en bokning |
| `LoadToday()` | Hämtar dagens bokningar, sorterade på tid |

### InvoicePrinter
**Ansvar:** Skriver ut en faktura för en bokning.

| Metod | Vad den gör |
|---|---|
| `Print(booking, total)` | Skriver ut fakturatexten |

### ConsoleMenu
**Ansvar:** Visar menyn och tar emot val från användaren. Den är den enda klassen som använder `Console`.

| Metod | Vad den gör |
|---|---|
| `Show()` | Visar menyn och kör vald funktion |

**Varför så här?** Om momsen ändras påverkas bara `PriceCalculator`. Om vi byter från fil till databas ändras bara `BookingRepository`. Om vi byter från konsol till webb ändras bara `ConsoleMenu`. Det är SoC i praktiken.

</details>
