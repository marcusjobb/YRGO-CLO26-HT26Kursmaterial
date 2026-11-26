# GDPR: vilka uppgifter behöver hotellet?

**Mål:** Granska fälten i en gästregistrering och motivera dina val. Det är samma arbete som GDPR-avsnittet i Inlämning 1.

**Tid:** ca 60 minuter.

**Förkunskap:** GDPR-grunderna från kurs 2 vecka 43.

Hotellchefen vill ha följande fält för varje gäst. Listan är sorterad i bokstavsordning, precis som egenskaperna i en klass:

| Fält | Typ | Varför hotellchefen vill ha det |
|------|-----|----------------------------------|
| Address | `string` | "Om gästen vill ha postkort efter besöket" |
| Age | `int` | "För att kunna erbjuda rabatt till pensionärer" |
| BankAccount | `string` | "Så vi kan betala tillbaka om något blir fel" |
| BirthDate | `DateTime` | "Så vi kan fira dem om de kommer på sin födelsedag" |
| Email | `string` | "För bekräftelser och nyhetsbrev" |
| Gender | `string` | "Vi vill kunna skicka rätt välkomstbrev" |
| HairColor | `string` | "Kan vara bra att veta" |
| HeightCm | `int` | "För rummets sängar" |
| IsFakeitarian | `bool` | "Låtsas vara veggo men äter allt" |
| IsGlutenIntolerant | `bool` | "Så köket vet vad de ska undvika" |
| IsLactoseIntolerant | `bool` | "Så köket vet vad de ska undvika" |
| IsVegan | `bool` | "Frukosten behöver kunna anpassas" |
| IsVegetarian | `bool` | "Frukosten behöver kunna anpassas" |
| Name | `string` | "Vi behöver veta vem som bor här" |
| Phone | `string` | "Om något händer under vistelsen" |
| Religion | `string` | "Vi vill kunna anpassa maten" |
| ShoeSize | `int` | "För spa-tofflor" |

Så här skulle klassen se ut, med samma fält:

```csharp
class Guest
{
    public string Address { get; set; }
    public int Age { get; set; }
    public string BankAccount { get; set; }
    public DateTime BirthDate { get; set; }
    public string Email { get; set; }
    public string Gender { get; set; }
    public string HairColor { get; set; }
    public int HeightCm { get; set; }
    // Hotellet dömer ingen. Men köket gör det.
    public bool IsFakeitarian { get; set; }
    public bool IsGlutenIntolerant { get; set; }
    public bool IsLactoseIntolerant { get; set; }
    public bool IsVegan { get; set; }
    public bool IsVegetarian { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Religion { get; set; }
    public int ShoeSize { get; set; }
}
```

Du är utvecklaren. Det är ditt jobb att granska listan innan du bygger.

## Del 1: 🟢 Frågorna

Svara på dessa frågor för varje fält, med ett eller två ord:

1. Behöver vi det här för att genomföra bokningen?
2. Vad händer om uppgiften läcker ut?
3. Hur länge behöver vi den?

## Del 2: 🟡 Känsliga uppgifter

Fundera på vilka uppgifter som kan vara känsliga. Läs om [artikel 9 i GDPR](https://www.privacy-regulation.eu/sv/9.htm) och jämför med fälten ovan. Vilka fält faller under den?

<details markdown="1">
<summary>Tips</summary>

Börja med frågan: "Behöver vi det här?" Det är principen om dataminimering, artikel 5.1 c. Om svaret är nej, bygg inte in fältet.

Fråga dig också hur länge uppgiften behövs. Det är principen om lagringsbegränsning, artikel 5.1 e.

</details>

## Del 3: 🔴 Skriv GDPR-avsnittet

Skriv ett kort avsnitt med rubriken **GDPR** till README-filen i din egen Inlämning 1. Ta med:

- Vilka fält du tar bort, eller behåller, och varför.
- Vilka fält du tycker är känsliga, och vad du gör för att skydda dem.
- Hur länge du sparar uppgifterna.

Lösningsförslaget ligger under. Motivera ditt eget val i inlämningen, även om det skiljer sig från förslaget.

<details markdown="1">
<summary>Lösningsförslag</summary>

| Fält | Behåll? | Varför | Hur länge |
|------|---------|--------|-----------|
| Address | Nej | Ingen faktura och inget syfte för postkort | Ej aktuellt |
| Age | Nej | Ålder ska inte lagras, den blir fel. Räkna ut den från `BirthDate` | Ej aktuellt |
| BankAccount | Nej | Betalning sker via betaltjänst. Spara i stället en betalningsreferens | Ej aktuellt |
| BirthDate | Ja, om firandet/rabatten görs | Ålder räknas ut från födelsedatumet. Behålls bara med samtycke | Till utcheckning, om inget annat samtycke finns |
| Email | Ja | Behövs för bokningsbekräftelse | Till bekräftelsen, eller tills samtycket till nyhetsbrev dras tillbaka |
| Gender | Nej | Välkomstbrev behöver inte kön | Ej aktuellt |
| HairColor | Nej | Ingen funktion i hotellet | Ej aktuellt |
| HeightCm | Nej | Sängar väljs via rumstyp, inte via gästens längd | Ej aktuellt |
| IsFakeitarian | Nej | Hotellet behöver inte veta vad gästen låtsas. Om köket behöver vegetariskt är det `IsVegetarian` | Ej aktuellt |
| IsGlutenIntolerant | Ja | Köket behöver veta, men det är hälsouppgift (art. 9) | Under vistelsen, raderas vid utcheckning |
| IsLactoseIntolerant | Ja | Köket behöver veta, och det är hälsouppgift (art. 9) | Under vistelsen, raderas vid utcheckning |
| IsVegan | Ja | Köket behöver veta. Kostval kan säga något om övertygelse, så behandla det försiktigt | Under vistelsen |
| IsVegetarian | Ja | Samma som IsVegan | Under vistelsen |
| Name | Ja | Behövs för bokningen | Till bokningen är klar. Fakturauppgifter sparas 7 år enligt bokföringslagen |
| Phone | Ja, under vistelsen | Nödkontakt om något händer | Till utcheckning |
| Religion | Nej | Känslig uppgift (art. 9). Kostval löser samma behov | Ej aktuellt |
| ShoeSize | Ja, om gästen vill ha spa-tofflor | Behövs bara för tofflorna | Till utcheckning |

**Dessutom:** Samla bara in det som behövs (dataminimering), och spara det inte längre än nödvändigt (lagringsbegränsning). Känsliga uppgifter som hälsa och kostval kräver uttryckligt samtycke.

</details>

## Reflektion

Vad var det svåraste i att motivera ett fält? Hade du svarat annorlunda om hotellet bara hade bokningar, utan gäster?

<!-- nion-copyright -->
<sub>© 2025–2026 Nion Education. Alla rättigheter förbehållna. Fritt att läsa, men får inte användas av andra skolor eller utbildningsanordnare utan skriftligt tillstånd från Nion Education. Delar bygger på källor som anges i respektive dokument.</sub>
