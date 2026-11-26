# Regex: små övningar för att känna igen mönster

**Mål:** Skriva reguljära uttryck (regex) som kontrollerar och hittar text, och granska om en kodbit gör det den ska.

**Tid:** ca 90 minuter.

**Förkunskap:** Strängar, `if`-satser, `foreach` och tupler i C#. Ingen tidigare erfarenhet av regex behövs.

**Berättelsen:** På Hjulet ska kunderna lämna sina uppgifter i ett formulär. Receptionen vill att programmet kontrollerar uppgifterna innan de sparas. Koden för det har vuxit till en massa `if`-satser som ingen vågar röra. Din uppgift är att först förstå vilka regler koden ska följa, sedan granska om den gör det, och till sist skriva om kontrollen som ett regex.

## Så här gör du

Varje del har fyra steg:

1. **Regel:** vad koden ska garantera, på vanlig svenska.
2. **C#-kod:** en kontroll utan regex. Den är krånglig, och den har fel. Läs den först.
3. **Testlista:** exempel som är giltiga (`true`) eller ogiltiga (`false`). Kör din kod mot listan innan du läser vidare.
4. **Uppgift:** skriv ett regex som klarar hela testlistan. Tipsen finns i `<details>`, och lösningen också. Titta på lösningen först när du har försökt själv.

Så här kan du köra en testlista i C#:

```csharp
var testlista = new[]
{
    ("anna@skola.se", true),
    ("anna@skola", false),
};

foreach (var (text, förväntat) in testlista)
{
    bool resultat = ÄrEpostOk(text);
    Console.WriteLine($"{text}: {(resultat == förväntat ? "OK" : "FEL")}");
}
```

I C# används regex så här:

```csharp
using System.Text.RegularExpressions;

bool ärGiltig = Regex.IsMatch(indata, mönster);
```

`mönster` är ditt regex som text. `indata` är det användaren skrivit in.

## Några tecken att känna igen

| Tecken | Betyder |
|---|---|
| `^` | början av texten |
| `$` | slutet av texten |
| `\d` | en siffra |
| `\p{Lu}` | en stor bokstav, även å, ä, ö |
| `\p{Ll}` | en liten bokstav |
| `[abc]` | en av tecknen a, b eller c |
| `[^abc]` | vilket tecken som helst utom a, b och c |
| `+` | en eller flera |
| `*` | noll eller flera |
| `{8,}` | minst åtta |
| `( )` | en grupp, som du kan plocka ut värden ur |

---

## Del 1: 🟢 E-postadress

**Regel:** En e-postadress har exakt ett `@`. Det ska finnas något före och något efter `@`. Domänen ska ha en punkt, och efter den sista punkten ska det finnas minst två tecken som inte är punkt. Adressen får inte innehålla mellanslag.

### Koden i C# (utan regex)

```csharp
static bool ÄrEpostOk(string indata)
{
    if (indata == null) return false;
    if (indata.Length < 5) return false;
    if (indata.Contains(' ')) return false;

    int antalSnabel = 0;
    foreach (char tecken in indata)
    {
        if (tecken == '@') antalSnabel++;
    }
    if (antalSnabel != 1) return false;

    int snabel = indata.IndexOf('@');
    if (snabel == 0) return false;
    if (snabel == indata.Length - 1) return false;

    string lokaldel = indata.Substring(0, snabel);
    string domän = indata.Substring(snabel + 1);

    if (lokaldel.StartsWith(".") || lokaldel.EndsWith(".")) return false;

    int punkt = domän.LastIndexOf('.');
    if (punkt <= 0) return false;
    if (punkt == domän.Length - 1) return false;
    if (domän.Substring(punkt + 1).Length < 2) return false;

    foreach (char tecken in indata)
    {
        if (!(char.IsLetterOrDigit(tecken) || tecken == '.' || tecken == '@'
              || tecken == '-' || tecken == '_' || tecken == '+'))
        {
            return false;
        }
    }
    return true;
}
```

### Testlista

| Text | Giltig? |
|---|---|
| `anna@skola.se` | true |
| `anna.k@skola.se` | true |
| `anna@skola` | false |
| `@skola.se` | false |
| `anna@@skola.se` | false |
| `anna skola@x.se` | false |
| `anna@skola.s` | false |
| `anna@skola.se.` | false |

**Fråga:** Kör testlistan mot C#-koden. Vilket fall klarar den inte, om något? Vilken regel är det som bryts? Hur skriver du ett regex som klarar hela listan?

<details markdown="1">
<summary>Tips</summary>

- Börja med `^` och sluta med `$`, så att hela texten måste matcha.
- `[^@\s]+` betyder "en eller flera tecken som inte är `@` eller mellanslag".
- Efter den sista punkten behövs minst två tecken som inte är punkt: `[^@\s.]{2,}`. Då godkänns inte `se.` med en punkt sist.

</details>

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
static bool ÄrEpostOk(string indata)
{
    return Regex.IsMatch(indata, @"^[^@\s]+@[^@\s]+\.[^@\s.]{2,}$");
}
```

- `[^@\s]+` före `@`: minst ett tecken som inte är `@` eller mellanslag.
- `[^@\s]+` efter `@`, sedan en riktig punkt `\.`.
- `[^@\s.]{2,}` efter punkten: minst två tecken, och ingen punkt.

Den här versionen kontrollerar inte allt som ett verkligt e-postformat gör. Den kontrollerar det som regeln säger. Det är skillnaden mellan att följa en regel och att skriva ett komplett standardformat.

</details>

---

## Del 2: 🟢 Hitta domänen

**Regel:** Domänen är det som står efter `@`. Adressen ska ha precis ett `@`, något före det, och en domän med en punkt som inte sitter sist. Om adressen inte följer regeln ska resultatet vara en tom sträng.

### Koden i C# (utan regex)

```csharp
static string HämtaDomän(string epost)
{
    if (epost == null) return "";

    int antalSnabel = 0;
    int position = -1;
    for (int i = 0; i < epost.Length; i++)
    {
        if (epost[i] == '@')
        {
            antalSnabel++;
            position = i;
        }
    }
    if (antalSnabel != 1) return "";
    if (position == 0) return "";
    if (position == epost.Length - 1) return "";

    string domän = epost.Substring(position + 1);
    if (domän.Contains(' ')) return "";
    if (!domän.Contains('.')) return "";
    if (domän.StartsWith(".") || domän.EndsWith(".")) return "";

    return domän;
}
```

### Testlista

| Text | Förväntat resultat |
|---|---|
| `anna@skola.se` | `skola.se` |
| `anna@skola` | `` (tom sträng) |
| `anna@skola.se.` | `` (tom sträng) |
| `@skola.se` | `` (tom sträng) |
| `anna@a@b.se` | `` (tom sträng) |

**Fråga:** Vilket fall klarar C#-koden inte? Vilken regel bryts? Hur skriver du ett regex som tar ut domänen och bara den?

<details markdown="1">
<summary>Tips</summary>

- Förankra mönstret med `^` och `$`, så att både texten före `@` och efter den måste matcha.
- Sätt parenteser runt domänen: `( )`. Läs ut den med `Groups[1].Value`.
- Domänen behöver en punkt som inte sitter sist: `[^@\s]+\.[^@\s.]+`.

</details>

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
static string HämtaDomän(string epost)
{
    Match träff = Regex.Match(epost, @"^[^@\s]+@([^@\s]+\.[^@\s.]+)$");
    return träff.Success ? träff.Groups[1].Value : "";
}
```

`Groups[1]` är det som står inom parenteserna, alltså domänen. Grupp 0 hade varit hela adressen.

</details>

---

## Del 3: 🟢 Telefonnummer

**Regel:** Ett svenskt mobilnummer börjar med `07` och har tio siffror. Användaren får använda bindestreck eller mellanslag mellan siffrorna, men inte i början, inte i slutet och inte två i rad.

### Koden i C# (utan regex)

```csharp
static bool ÄrMobilnummer(string indata)
{
    if (indata == null) return false;

    int antalSiffror = 0;
    for (int i = 0; i < indata.Length; i++)
    {
        char tecken = indata[i];

        if (char.IsDigit(tecken))
        {
            antalSiffror++;

            if (antalSiffror == 1 && tecken != '0') return false;
            if (antalSiffror == 2 && tecken != '7') return false;
            if (antalSiffror > 10) return false;
        }
        else if (tecken == '-' || tecken == ' ')
        {
            if (i == 0) return false;
            if (i == indata.Length - 1) return false;
            if (indata[i - 1] == '-' || indata[i - 1] == ' ') return false;
            if (antalSiffror < 2) return false;
        }
        else
        {
            return false;
        }
    }
    return antalSiffror == 10;
}
```

### Testlista

| Text | Giltig? |
|---|---|
| `070-1234567` | true |
| `070 123 45 67` | true |
| `0701234567` | true |
| `08-123456` | false |
| `070-12345` | false |
| `abc` | false |
| `070--1234567` | false |
| `07012345678` | false |
| ` 070-1234567` | false |
| `070-1234567 ` | false |

**Fråga:** Vilka fall är svåra att se i koden? Vilken regel tycker du att koden bryter mot, om någon? Hur skriver du ett regex som klarar listan?

<details markdown="1">
<summary>Tips</summary>

- Börja med `^07` och en siffra till.
- Sedan sju siffror, och varje siffra kan ha ett valfritt bindestreck eller mellanslag före sig: `([ -]?[0-9])`.
- Upprepa gruppen exakt sju gånger med `{7}`.

</details>

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
static bool ÄrMobilnummer(string indata)
{
    return Regex.IsMatch(indata, @"^07[0-9]([ -]?[0-9]){7}$");
}
```

Efter `07` kommer en siffra, och sedan sju grupper där varje grupp är en siffra med ett valfritt separatortecken före sig. Då får du tio siffror, och mellanslag eller bindestreck i början eller slutet blir ogiltigt.

</details>

---

## Del 4: 🟡 Förnamn och efternamn

**Regel:** Ett namn har minst två delar. Varje del börjar med stor bokstav, och resten är små bokstäver. Delarna skiljs åt av mellanslag eller bindestreck. Bokstäverna kan vara å, ä och ö.

### Koden i C# (utan regex)

```csharp
static bool ÄrNamnOk(string indata)
{
    if (indata == null) return false;
    if (indata.Length < 3) return false;
    if (!char.IsUpper(indata[0])) return false;

    int antalDelar = 0;
    bool nyDel = true;

    for (int i = 0; i < indata.Length; i++)
    {
        char tecken = indata[i];

        if (nyDel)
        {
            if (!char.IsUpper(tecken)) return false;
            antalDelar++;
            nyDel = false;
        }
        else if (char.IsLetter(tecken))
        {
            if (char.IsUpper(tecken)) return false;
        }
        else if (tecken == ' ' || tecken == '-')
        {
            if (i == indata.Length - 1) return false;
            if (!char.IsUpper(indata[i + 1])) return false;
            nyDel = true;
        }
        else
        {
            return false;
        }
    }
    return antalDelar >= 2;
}
```

### Testlista

| Text | Giltig? |
|---|---|
| `Anna Lindqvist` | true |
| `Anna-Lena Berg` | true |
| `Örjan Ståhl` | true |
| `anna lindqvist` | false |
| `Anna` | false |
| `ANNA LINDQVIST` | false |
| `Anna-lena Berg` | false |
| `Anna Berg-` | false |
| `Anna  Berg` | false |
| `van der Kolk` | false |

Lägg märke till den sista raden. Ett riktigt efternamn kan innehålla små bokstäver i början. Det är ett fall där regeln är för snäv.

**Fråga:** Kör testlistan. Vilket fall är en riktig bugg, och vilket fall är ett fel i regeln? Hur skriver du ett regex som klarar listan, och vad skulle du ändra för `van der Kolk`?

<details markdown="1">
<summary>Tips</summary>

- En stor bokstav med `\p{Lu}`, sedan en eller flera små med `\p{Ll}+`.
- Upprepa gruppen `[ -]\p{Lu}\p{Ll}+` en eller flera gånger, så att det finns minst två delar.
- `\p{Lu}` och `\p{Ll}` fungerar med å, ä och ö, men `[A-Z]` gör inte det.

</details>

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
static bool ÄrNamnOk(string indata)
{
    return Regex.IsMatch(indata, @"^\p{Lu}\p{Ll}+([ -]\p{Lu}\p{Ll}+)+$");
}
```

Hela C#-koden ryms i ett mönster: en stor bokstav, små bokstäver, och sedan minst en gång till mellanslag eller bindestreck följt av samma sorts del.

`van der Kolk` är ett fall där regeln är för snäv. Mönstret och koden säger nej, eftersom det inte finns någon regel för partiklar som `van` och `der`. Den som granskar ska säga det högt: det här är en medveten avgränsning, eller ett fel som ska rättas i regeln.

</details>

---

## Del 5: 🟡 Lösenord i nivåer

**Regel:** Lösenord har tre nivåer:
- **För kort:** under 8 tecken.
- **Svag:** minst 8 tecken.
- **Medel:** minst 8 tecken, med minst en stor bokstav, en liten bokstav och en siffra.
- **Stark:** minst 12 tecken, med minst en stor bokstav, en liten bokstav, en siffra och ett specialtecken.

Ett stark lösenord är också ett medel. Nivån ska vara den högsta som lösenordet klarar.

### Koden i C# (utan regex)

```csharp
static string LösenordsNivå(string lösenord)
{
    if (lösenord == null) return "Ogiltigt";

    int längd = lösenord.Length;
    bool harStor = false;
    bool harLiten = false;
    bool harSiffra = false;
    bool harSpecial = false;

    foreach (char tecken in lösenord)
    {
        if (char.IsUpper(tecken))
        {
            harStor = true;
        }
        else if (char.IsLower(tecken))
        {
            harLiten = true;
        }
        else if (char.IsDigit(tecken))
        {
            harSiffra = true;
        }
        else
        {
            harSpecial = true;
        }
    }

    if (längd < 8) return "För kort";

    if (längd >= 12)
    {
        if (harStor)
        {
            if (harLiten)
            {
                if (harSiffra)
                {
                    if (harSpecial) return "Stark";
                }
            }
        }
    }

    if (harStor && harLiten && harSiffra) return "Medel";
    return "Svag";
}
```

### Testlista

| Text | Nivå |
|---|---|
| `abc` | För kort |
| `abcdefgh` | Svag |
| `Abcdefg1` | Medel |
| `Abcdefg1!` | Medel |
| `Abcdefgh1234` | Medel |
| `Abcdefgh1!xy` | Stark |

**Fråga:** Vilken nivå ger koden fel, om någon? Vilken regel för nivåerna är det som bryts? Hur kan du skriva tre regex, en per nivå, och vilken ordning ska de kontrolleras i?

<details markdown="1">
<summary>Tips</summary>

- Ett regex kan kräva att något finns någonstans i texten med lookahead: `(?=.*\d)` betyder "det finns en siffra någonstans framåt".
- Lägg lookaheads först, direkt efter `^`, och sedan `.{8,}$` för längden.
- Kontrollera den högsta nivån först. Ett stark lösenord är också ett medel.

</details>

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
static string LösenordsNivå(string lösenord)
{
    if (Regex.IsMatch(lösenord, @"^(?=.*\p{Ll})(?=.*\p{Lu})(?=.*\d)(?=.*[^\p{L}\d]).{12,}$"))
        return "Stark";
    if (Regex.IsMatch(lösenord, @"^(?=.*\p{Ll})(?=.*\p{Lu})(?=.*\d).{8,}$"))
        return "Medel";
    if (Regex.IsMatch(lösenord, @"^.{8,}$"))
        return "Svag";
    return "För kort";
}
```

Ordningen spelar roll. Stark kontrolleras först, eftersom ett stark lösenord också är ett medel.

Observera: `Abcdefg1!` är bara 9 tecken, så det är medel och inte stark. Det är en av sakerna som lätt blir fel i C#-koden om man glömmer längdkravet för stark.

</details>

---

## Del 6: 🟡 Postadress

**Regel:** En svensk postadress har formen `Gatunamn nummer, postnummer ort`. Husnumret är siffror, och kan ha en bokstav efter, till exempel `5B`. Postnumret är fem siffror, med eller utan mellanslag efter de tre första. Efter kommatecken ska det vara ett mellanslag. Orten ska finnas.

### Koden i C# (utan regex)

```csharp
static bool ÄrAdressOk(string indata)
{
    if (indata == null) return false;

    int komma = indata.IndexOf(',');
    if (komma < 0) return false;

    string gata = indata.Substring(0, komma);
    string resten = indata.Substring(komma + 1);

    if (!resten.StartsWith(" ")) return false;
    resten = resten.Substring(1);

    int sistaMellanslag = gata.LastIndexOf(' ');
    if (sistaMellanslag <= 0) return false;

    string husnummer = gata.Substring(sistaMellanslag + 1);
    if (husnummer.Length == 0) return false;
    if (!char.IsDigit(husnummer[0])) return false;

    for (int i = 0; i < husnummer.Length; i++)
    {
        if (!char.IsDigit(husnummer[i]))
        {
            if (i != husnummer.Length - 1) return false;
            if (!char.IsLetter(husnummer[i])) return false;
        }
    }

    string gatunamn = gata.Substring(0, sistaMellanslag);
    foreach (char tecken in gatunamn)
    {
        if (!(char.IsLetter(tecken) || tecken == ' ' || tecken == '.' || tecken == '-')) return false;
    }

    int pos = 0;
    for (int i = 0; i < 3; i++)
    {
        if (pos >= resten.Length || !char.IsDigit(resten[pos])) return false;
        pos++;
    }
    if (pos < resten.Length && resten[pos] == ' ') pos++;
    for (int i = 0; i < 2; i++)
    {
        if (pos >= resten.Length || !char.IsDigit(resten[pos])) return false;
        pos++;
    }

    if (pos >= resten.Length || resten[pos] != ' ') return false;
    pos++;
    if (pos >= resten.Length) return false;

    foreach (char tecken in resten.Substring(pos))
    {
        if (!(char.IsLetter(tecken) || tecken == ' ' || tecken == '-')) return false;
    }
    return true;
}
```

### Testlista

| Text | Giltig? |
|---|---|
| `Storgatan 12, 123 45 Stockholm` | true |
| `Ringvägen 5B, 11223 Stockholm` | true |
| `Storgatan 12, 12345 Stockholm` | true |
| `Storgatan 12 Stockholm` | false |
| `Storgatan, 12345 Stockholm` | false |
| `Storgatan 12,123 45 Stockholm` | false |
| `Storgatan 12, 123 45` | false |

**Fråga:** Vilken rad i testlistan kan du tänka dig att koden ser rätt ut för, men som ändå bryter mot regeln? Hur skriver du ett regex för gatan, husnumret, postnumret och orten i ett enda mönster?

<details markdown="1">
<summary>Tips</summary>

- Gatan är bokstäver och mellanslag, och slutar med ett nummer: `\d+`. Ibland finns en bokstav efter, till exempel `5B`: `[A-Za-z]?`.
- Efter kommatecken kommer ett mellanslag, sedan postnumret. Mellanslaget i postnumret är valfritt: `\s?`.
- Orten är bokstäver och ibland mellanslag. Använd `\p{L}` så att ö och ä fungerar.

</details>

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
static bool ÄrAdressOk(string indata)
{
    return Regex.IsMatch(indata, @"^[\p{L}\s.-]+ \d+[A-Za-z]?, \d{3}\s?\d{2} [\p{L}\s-]+$");
}
```

Mönstret läser ut som en mening: gatunamn, mellanslag, nummer, komma, postnummer och ort.

</details>

---

## Del 7: 🔴 Hitta alla e-postadresser i en text

**Regel:** En e-postadress i en text är en sekvens utan mellanslag, med ett `@` och en domän med punkt. Skiljetecken direkt efter adressen, som punkt eller utropstecken, ingår inte i adressen.

### Koden i C# (utan regex)

```csharp
string text = "Hej! Kontakta anna.k@skola.se eller erik@firma.co.uk om bokningen. " +
              "Skicka inte till ej-en-adress@.";

List<string> hittade = new List<string>();

foreach (string ord in text.Split(' '))
{
    string rent = ord.Trim('.', ',', '!', '?');

    int snabel = rent.IndexOf('@');
    if (snabel <= 0) continue;
    if (snabel == rent.Length - 1) continue;

    string domän = rent.Substring(snabel + 1);
    if (!domän.Contains('.')) continue;
    if (domän.EndsWith(".")) continue;

    hittade.Add(rent);
}

foreach (string adress in hittade)
{
    Console.WriteLine(adress);
}
```

**Fråga:** Vilka adresser hittar koden, och vilka borde den hitta? Hur skriver du ett mönster som hittar adresserna men inte tar med punkten efter `.se` eller det avslutande tecknet i meningen?

<details markdown="1">
<summary>Tips</summary>

- `Regex.Matches(text, mönster)` ger en samling av träffar. Gå igenom den med `foreach`.
- Ett tecken som `\w` betyder en bokstav, en siffra eller understreck.
- Krav på att det finns något efter varje punkt i domänen: `(\.[\w-]+)+`.

</details>

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
using System.Text.RegularExpressions;

string text = "Hej! Kontakta anna.k@skola.se eller erik@firma.co.uk om bokningen. " +
              "Skicka inte till ej-en-adress@.";

foreach (Match träff in Regex.Matches(text, @"[\w.+-]+@[\w-]+(\.[\w-]+)+"))
{
    Console.WriteLine(träff.Value);
}
// anna.k@skola.se
// erik@firma.co.uk
```

Den sista adressen, `ej-en-adress@.`, matchar inte eftersom det inte finns någon domän efter `@`.

</details>

---

## Del 8: 🔴 Maskera telefonnummer

**Regel:** Ett mobilnummer i en logg ska maskeras. De tre sista delarna blir `XXX XX XX`, så att bara `07` och riktnumret syns: `070-123 45 67` blir `070-XXX XX XX`. Övrig text ska vara oförändrad.

### Koden i C# (utan regex)

```csharp
string logg = "Kund ringde från 070-123 45 67 om tiden.";

string resultat = "";

for (int i = 0; i < logg.Length; i++)
{
    // Ser det ut som början på ett mobilnummer?
    if (i + 2 < logg.Length && logg[i] == '0' && logg[i + 1] == '7' && char.IsDigit(logg[i + 2]))
    {
        // Samla siffror och separatorer tills vi har tio siffror
        int siffror = 0;
        int j = i;
        while (j < logg.Length && siffror < 10)
        {
            if (char.IsDigit(logg[j]))
            {
                siffror++;
            }
            else if (logg[j] != '-' && logg[j] != ' ')
            {
                break;
            }
            j++;
        }

        if (siffror == 10)
        {
            resultat += logg.Substring(i, 3) + "-XXX XX XX";
            i = j - 1;
            continue;
        }
    }

    resultat += logg[i];
}

Console.WriteLine(resultat);
```

### Testlista

| Text | Förväntat resultat |
|---|---|
| `Kund ringde från 070-123 45 67 om tiden.` | `Kund ringde från 070-XXX XX XX om tiden.` |
| `Ring 0701234567 nu.` | `Ring 070-XXX XX XX nu.` |
| `Inget nummer här.` | `Inget nummer här.` |

**Fråga:** Vilken av raderna i testlistan är svårast för koden? Hur byter du ut bara en del av texten med ett regex, och vilka delar behöver du komma ihåg?

<details markdown="1">
<summary>Tips</summary>

- `Regex.Replace(text, mönster, ersättning)` byter ut träffar.
- Sätt parenteser runt den del du vill behålla, till exempel `(07\d)`.
- I ersättningen kan du använda `$1` för första gruppen.

</details>

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
string logg = "Kund ringde från 070-123 45 67 om tiden.";

string maskerad = Regex.Replace(
    logg,
    @"\b(07\d)[- ]?\d{3}[- ]?\d{2}[- ]?\d{2}\b",
    "$1-XXX XX XX");

Console.WriteLine(maskerad);
// Kund ringde från 070-XXX XX XX om tiden.
```

`$1` är den första gruppen, alltså `070`. Det som ligger utanför gruppen byts ut. `\b` gör att ett längre tal inte matchar av misstag.

</details>

---

## Reflektion

Skriv tre meningar:
1. Vilket testfall hittade du som koden inte klarade? Vilken regel bröt den mot?
2. Vilket testfall saknas i listan för den del du tyckte var svårast? Skriv ett eget fall och säg vad det ska ge.
3. När är en vanlig `if`-kontroll bättre än ett regex?

## Tips för att läsa regex

- Läs ett regex från vänster till höger, en del i taget.
- Testa i ett testprogram innan du lägger in det i koden. Kör hela testlistan varje gång du ändrar något.
- Börja med ett enkelt mönster och lägg till krav ett i taget. Det är lättare att hitta felet då.

<!-- nion-copyright -->
<sub>© 2025–2026 Nion Education. Alla rättigheter förbehållna. Fritt att läsa, men får inte användas av andra skolor eller utbildningsanordnare utan skriftligt tillstånd från Nion Education. Delar bygger på källor som anges i respektive dokument.</sub>
