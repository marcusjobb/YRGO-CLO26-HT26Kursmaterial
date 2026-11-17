# Övning — Nice Methods

🟢 Grundläggande

> Fastnar du i mer än 15 minuter? Fråga klassen → AI → Marcus. I den ordningen.

---

## Bakgrunden

Du har ett namn och ett efternamn. Din uppgift är att skriva fyra metoder som presenterar namnet på olika sätt — som det är, i formellt format, och filtrerat på vokaler respektive konsonanter.

---

## Uppgiften

Deklarera dessa två variabler i `Main`:

```csharp
string name = "Taylor";
string lastname = "Swift";
```

Implementera sedan de fyra metoderna nedan och anropa dem i ordning.

### Metoderna

| Metod | Vad den ska skriva ut |
|-------|----------------------|
| `PrintName(name, lastname)` | `Taylor Swift` |
| `PrintProper(name, lastname)` | `Swift, Taylor` |
| `PrintVocals(name, lastname)` | `Vokaler: aoi` |
| `PrintConsonants(name, lastname)` | `Konsonanter: TylrSwft` |

### Förväntad output

```
Taylor Swift
Swift, Taylor
Vokaler: aoi
Konsonanter: TylrSwft
```

---

<details>
<summary>Tips 1 — PrintName och PrintProper</summary>

Interpolerade strängar räcker. `PrintProper` skriver ut efternamnet först, följt av ett kommatecken och ett mellanslag, sedan förnamnet.

</details>

<details>
<summary>Tips 2 — Hur hittar du vokalerna?</summary>

Slå ihop `name + lastname` till en sträng. Loopa igenom varje tecken med `foreach`. Kolla om tecknet finns i strängen `"aeiouAEIOU"` med `.Contains(c)`.

</details>

<details>
<summary>Tips 3 — Hur hittar du konsonanterna?</summary>

Samma loop som för vokaler, men omvänt villkor: tecknet ska vara en bokstav (`char.IsLetter(c)`) OCH inte finnas i vokalsträngen.

</details>

<details>
<summary>Lösningsförslag</summary>

```csharp
string name = "Taylor";
string lastname = "Swift";

PrintName(name, lastname);
PrintProper(name, lastname);
PrintVocals(name, lastname);
PrintConsonants(name, lastname);

void PrintName(string name, string lastname)
{
    Console.WriteLine($"{name} {lastname}");
}

void PrintProper(string name, string lastname)
{
    Console.WriteLine($"{lastname}, {name}");
}

void PrintVocals(string name, string lastname)
{
    string fullName = name + lastname;
    string result = "";
    foreach (char c in fullName)
    {
        if ("aeiouAEIOU".Contains(c))
            result += c;
    }
    Console.WriteLine($"Vokaler: {result}");
}

void PrintConsonants(string name, string lastname)
{
    string fullName = name + lastname;
    string result = "";
    foreach (char c in fullName)
    {
        if (char.IsLetter(c) && !"aeiouAEIOU".Contains(c))
            result += c;
    }
    Console.WriteLine($"Konsonanter: {result}");
}
```

</details>

---

## Utmaning

- Testa med ditt eget namn — stämmer vokalerna?
- Lägg till en femte metod `PrintReversed(name, lastname)` som skriver ut hela namnet baklänges.
- Vad händer om du byter ut `result += c` mot en `StringBuilder`? Är det snabbare?
