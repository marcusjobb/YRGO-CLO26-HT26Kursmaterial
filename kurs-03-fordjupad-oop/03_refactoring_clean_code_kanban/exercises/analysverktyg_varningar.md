# Städa med analysverktygen: warnings och info

**Mål:** Använda editorns analyser (warnings och info) som en granskningspartner. Förstå varje varning innan du åtgärdar den.

**Tid:** ca 60 minuter.

**Förkunskap:** Refaktorering med namn och metoder.

**Berättelsen:** Offertkoden från Hjulet ser ut att fungera. Men editorn är inte nöjd. Den visar gula streck, grå dämpade rader och små gröna lampor. Det är inte fel, det är information.

## Så här gör du

**Visual Studio:** Öppna *Felsökning* eller *Kodanalys* och titta på varningslistan. Grått dämpad kod är en info.

**VS Code:** Installera **C# Dev Kit**. Öppna panelen *Problems* (Ctrl+Shift+M). Titta även på gröna lampan i marginalen.

## Startkod

```csharp
using System;

class Quote
{
    private int count = 0;

    public decimal Calculate(int hours, decimal partsPrice, string customer)
    {
        decimal labor = hours * 650m;
        decimal unused = 10m;
        unused = 20m;
        decimal vat = (labor + partsPrice) * 0.25m;
        return labor + partsPrice + vat;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine(new Quote().Calculate(3, 500m, "Sara"));
    }
}
```

## Del 1: 🟢 Lista alla varningar

Kör analysen och skriv en tabell med varje varning: rad, text och vad du tror den betyder. Kolla inte lösningen än.

<details markdown="1">
<summary>Tips</summary>

Börja med den varning som editorn markerar tydligast. Om du inte förstår en varning, sök på dess kod (till exempel `IDE0059`) i dokumentationen.

</details>

## Del 2: 🟡 Åtgärda med förståelse

Åtgärda varje varning, men skriv en mening om varför den fanns. Om en varning inte stämmer i ditt fall, skriv varför och låt den vara kvar.

## Del 3: 🔴 Ställ in regler själv

Skapa en fil `.editorconfig` i projektmappen. Välj en regel som du tycker är viktig, till exempel att en oanvänd variabel ska ge en varning och inte bara en info. Dokumentera vad du ändrade och varför.

## Reflektion

Skriv tre meningar: Vilken varning var den mest användbara? Vilken varning höll du inte med om? Vad är skillnaden mellan en varning och en info?

<details markdown="1">
<summary>Lösningsförslag</summary>

Exakt vilka varningar du får beror på verktyg och inställningar. Här är de vanligaste i startkoden:

| Rad | Kod | Vad det betyder |
|---|---|---|
| `private int count` | IDE0052 | Fältet läses aldrig. Ta bort det. |
| `string customer` | IDE0060 | Parametern används inte. Ta bort den eller använd den. |
| `decimal unused = 10m;` | IDE0059 | Värdet används aldrig. Ta bort raden. |
| `unused = 20m;` | IDE0059 | Tilldelningen används aldrig. Ta bort. |

Så kan koden se ut efter åtgärd (parametern `customer` är borttagen, och metoden gör bara en sak):

```csharp
using System;

class Quote
{
    private const decimal HourlyRate = 650m;
    private const decimal VatRate = 0.25m;

    public decimal Calculate(int hours, decimal partsPrice)
    {
        decimal labor = hours * HourlyRate;
        decimal vat = (labor + partsPrice) * VatRate;
        return labor + partsPrice + vat;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine(new Quote().Calculate(3, 500m));   // 3062,50
    }
}
```

Det kan finnas fler varningar i din editor, till exempel om formatering eller om en variabel kan skrivas om. Det är bra att förstå dem, men alla behöver inte åtgärdas. Det viktiga är att du kan säga varför.

</details>

<!-- nion-copyright -->
<sub>© 2025–2026 Nion Education. Alla rättigheter förbehållna. Fritt att läsa, men får inte användas av andra skolor eller utbildningsanordnare utan skriftligt tillstånd från Nion Education. Delar bygger på källor som anges i respektive dokument.</sub>
