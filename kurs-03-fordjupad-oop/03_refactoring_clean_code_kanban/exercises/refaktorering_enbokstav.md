# Refaktorering: från enbokstavsvariabler till namngivna delar

**Mål:** Gör en fungerande klass läsbar genom att byta namn, dela upp metoder och ersätta hårdkodade värden med en enum och konstanter.

**Tid:** ca 60 minuter.

**Förkunskap:** Extract Method och Rename från övningen om refaktorering.

**Berättelsen:** Nils skrev kalkylatorn för offerter på Hjulet. Den räknar rätt, men ingen förstår den. Elin måste ändra momssatsen nästa vecka, och hon vet inte vilket tal som är momsen.

## Startkod

Koden fungerar. Kör den och kontrollera utskriften. Den ska vara densamma efter varje steg.

```csharp
class K
{
    public decimal C(int h, bool s, decimal d)
    {
        decimal a = h * 650m;
        if (s == true) a = a * 0.9m;
        decimal b = a + d;
        decimal c = b * 0.25m;
        return b + c;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine(new K().C(3, true, 500m));   // 2818,75
    }
}
```

## Del 1: 🟢 Byt namn

Byt namn på klassen, metoden, parametrarna och variablerna så att en läsare förstår vad de gör. Kör programmet efter varje byte.

<details markdown="1">
<summary>Tips</summary>

Ett bra namn säger vad värdet *är*, inte vad det *räknas ut från*. `a` är arbetskostnaden, även om den innan var det första steget.

</details>

## Del 2: 🟡 Ersätt bool med en enum

Parametern `bool s` säger inte vad den betyder. Skapa en `enum CustomerType` med värdena `Regular` och `Loyal`, och använd den i stället för `bool`.

<details markdown="1">
<summary>Tips</summary>

Vid anropet ser `true` ut som ett svar på en fråga du inte hittar. `CustomerType.Loyal` förklarar sig själv.

</details>

## Del 3: 🟡 Dela upp i metoder

Flytta uträkningen av arbetskostnad, moms och total till egna metoder. Varje metod ska göra en sak och ha ett namn som beskriver den.

## Del 4: 🔴 Ersätt hårdkodade värden med konstanter

Värdena `650m`, `0.25m` och `0.9m` ska ligga på ett ställe, med namn. Om momsen ändras ska det räcka att ändra en rad.

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
enum CustomerType
{
    Regular,
    Loyal
}

class QuoteCalculator
{
    private const decimal HourlyRate = 650m;
    private const decimal VatRate = 0.25m;
    private const decimal LoyalDiscountRate = 0.10m;

    public decimal CalculateTotal(int hours, CustomerType customerType, decimal partsPrice)
    {
        decimal labor = CalculateLabor(hours, customerType);
        decimal subtotal = labor + partsPrice;
        decimal vat = CalculateVat(subtotal);
        return subtotal + vat;
    }

    private decimal CalculateLabor(int hours, CustomerType customerType)
    {
        decimal labor = hours * HourlyRate;
        return customerType == CustomerType.Loyal
            ? labor * (1 - LoyalDiscountRate)
            : labor;
    }

    private decimal CalculateVat(decimal amount)
    {
        return amount * VatRate;
    }
}

class Program
{
    static void Main()
    {
        var calculator = new QuoteCalculator();
        Console.WriteLine(calculator.CalculateTotal(3, CustomerType.Loyal, 500m));   // 2818,75
    }
}
```

Utskriften ska vara samma som i startkoden. Kontrollera det innan du går vidare.

</details>

## Reflektion

Skriv tre meningar: Vilket steg gav mest i läsbarhet? Vilket namn var svårast att hitta? Hur lång tid tog det att ändra momsen i början, jämfört med nu?

<!-- nion-copyright -->
<sub>© 2025–2026 Nion Education. Alla rättigheter förbehållna. Fritt att läsa, men får inte användas av andra skolor eller utbildningsanordnare utan skriftligt tillstånd från Nion Education. Delar bygger på källor som anges i respektive dokument.</sub>
