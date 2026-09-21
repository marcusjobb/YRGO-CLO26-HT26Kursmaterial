# Lästext — Enum

## Vad är enum?

Föreställ dig att du skriver ett program som hanterar årstider. Du behöver lagra vilken årstid det är. Du _kan_ använda strängar:

```csharp
string årstid = "Sommar";
```

Men strängar är oprecisa. Ingenting hindrar att du råkar skriva `"sommar"` (liten bokstav), `"SOMMAR"`, eller `"Sommmar"` med ett extra m. Kompilatorn ser ingenting fel — men programmet beter sig fel.

Det finns ett bättre verktyg: **enum**. En enum är en uppräkning av namngivna konstanter. Du definierar en begränsad uppsättning tillåtna värden, och kompilatorn ser till att du bara kan använda dem.

```csharp
enum Säsong
{
    Vår,
    Sommar,
    Höst,
    Vinter
}
```

Nu är `Säsong.Sommar` ett giltigt värde. `Säsong.Sommmar` är ett kompileringsfel. Det felet hittar du direkt — inte en timme in i felsökning.

---

## Varför inte bara string?

Det är en rimlig fråga. Strängar är flexibla och lätta att förstå. Men flexibiliteten är också problemet.

**Strängar:**
- Kompilatorn kan inte kontrollera dem — ett stavfel syns inte förrän programmet kör
- Ingen IntelliSense-hjälp — du måste komma ihåg exakt vad du skrivit
- Lätta att råka ändra — `"Sommar"` och `"sommar"` är inte lika

**Enum:**
- Kompilatorn kontrollerar varje användning — ogiltiga värden ger kompileringsfel
- IntelliSense listar alla tillåtna värden åt dig
- Omöjligt att stava fel — du skriver `Säsong.Sommar`, inte en fri sträng

```csharp
// Med sträng — kompilatorn ser inget fel, men programmet kanske beter sig fel
string väder = "regnigt";   // Korrekt är "Regnigt" med stor bokstav

// Med enum — kompilatorn stoppar dig direkt om du skriver fel
Väderlek väder = Väderlek.Regnigt;   // Enda möjliga stavningen
```

Enums är också tydligare att läsa. `Väderlek.Soligt` berättar mer än `"soligt"` — du vet direkt att det tillhör en definierad kategori.

---

## Definiera en enum

Du definierar en enum utanför klassen, på samma nivå som `class`. Konventionen är PascalCase för både enum-namnet och varje värde.

```csharp
// Definiera enum-typen
enum Säsong
{
    Vår,
    Sommar,
    Höst,
    Vinter
}

// Definiera enum för väderlek (från exempelkoden i kursen)
enum Väderlek
{
    Soligt,
    Molnigt,
    Regnigt,
    Snöigt
}
```

Du använder sedan enum-typen som vilken annan typ som helst — för att deklarera en variabel, ta ett argument, eller returnera ett värde.

```csharp
Säsong nuvarandeSäsong = Säsong.Höst;
Console.WriteLine(nuvarandeSäsong);   // Höst
```

<details>
<summary>Vad är enum egentligen under huven?</summary>

En enum är i grunden ett heltal. Varje värde mappas till ett nummer som börjar på 0:

```csharp
// Implicit:
// Vår   = 0
// Sommar = 1
// Höst   = 2
// Vinter = 3
```

Det betyder att du kan casta mellan enum och int:

```csharp
int nummer = (int)Säsong.Sommar;    // 1
Säsong säsong = (Säsong)2;          // Höst
```

I praktiken gör du sällan det här. Det är mer en förklaring till varför enums finns och fungerar som de gör — de är ett säkert lager ovanpå tal.

</details>

---

## Använda i switch

Enums och `switch` är gjorda för varandra. När du har en begränsad uppsättning möjliga värden kan en `switch` hantera varje fall.

```csharp
Väderlek dagensVäder = Väderlek.Regnigt;

switch (dagensVäder)
{
    case Väderlek.Soligt:
        Console.WriteLine("Ta med solglasögon!");
        break;
    case Väderlek.Molnigt:
        Console.WriteLine("Det är grått ute, men torrt.");
        break;
    case Väderlek.Regnigt:
        Console.WriteLine("Ta med ett paraply!");
        break;
    case Väderlek.Snöigt:
        Console.WriteLine("Klä dig varmt och ta på vinterskorna!");
        break;
}
```

Fördelen mot att använda strängar i en switch är att du inte kan missa ett case av misstag — om du lägger till ett nytt värde i enum:en och glömmer att lägga till ett case i switch:en kan verktyg varna dig om det.

---

## Enum i praktiken

Enums dyker upp naturligt när ett värde tillhör en känd, begränsad uppsättning alternativ.

```csharp
enum Säsong { Vår, Sommar, Höst, Vinter }

class Program
{
    static void Main()
    {
        Säsong säsong = Säsong.Vinter;

        // Skriv ut säsongens namn direkt
        Console.WriteLine("Aktuell säsong: " + säsong);   // Vinter

        // Reagera på värdet
        switch (säsong)
        {
            case Säsong.Vår:
                Console.WriteLine("Det börjar bli varmt igen.");
                break;
            case Säsong.Sommar:
                Console.WriteLine("Semester!");
                break;
            case Säsong.Höst:
                Console.WriteLine("Löven faller.");
                break;
            case Säsong.Vinter:
                Console.WriteLine("Plocka fram vinterkläderna.");
                break;
        }
    }
}
```

Vanliga användningsfall för enum:

- **Riktningar** — `Norr`, `Söder`, `Öster`, `Väster`
- **Status** — `Aktiv`, `Inaktiv`, `Väntande`
- **Kortfärger** — `Hjärter`, `Ruter`, `Spader`, `Klöver`
- **Svårighetsgrad** — `Lätt`, `Medel`, `Svår`

Varje gång du ser dig själv skriva en sträng som ett av ett begränsat antal alternativ — fundera på om det är ett enum i förklädnad.

**Se även:** [programmeringstermer/listor.md](../programmeringstermer/listor.md)
