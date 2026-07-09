---
marp: true
theme: nion-dark
paginate: true
---

<!-- _class: title -->

# Dictionary och enum

### Nyckel och värde — kategorier i kod

_Kurs 01 · Vecka 6 · Nion Education_

---

## Dictionary — som en telefonbok

En `Dictionary<TKey, TValue>` lagrar par av **nyckel och värde**.
Du slår upp med nyckeln — precis som i en telefonbok.

```
Telefonbok:
  "Alex"    →  070-123 45 67
  "Bella"   →  073-987 65 43
  "Carlos"  →  076-555 00 11
```

```csharp
Dictionary<string, string> telefonbok = new Dictionary<string, string>();
telefonbok["Alex"]   = "070-123 45 67";
telefonbok["Bella"]  = "073-987 65 43";
telefonbok["Carlos"] = "076-555 00 11";
```

Nyckeln är unik. Lägger du in samma nyckel igen — skriver du över det gamla värdet.

---

## Skapa och lägga till

Två sätt att skapa och fylla ett Dictionary:

```csharp
// Sätt 1 — skapa tomt, sedan Add()
Dictionary<string, int> poäng = new Dictionary<string, int>();
poäng.Add("Alex", 42);
poäng.Add("Bella", 87);
poäng.Add("Carlos", 65);

// Sätt 2 — skapa och fyll direkt (collection initializer)
Dictionary<string, int> poäng = new Dictionary<string, int>
{
    { "Alex", 42 },
    { "Bella", 87 },
    { "Carlos", 65 }
};
```

Båda ger exakt samma resultat. Välj det som känns tydligast i sammanhanget.

---

## Slå upp ett värde

Du hämtar ett värde genom att skriva nyckeln inom hakparenteser:

```csharp
Dictionary<string, int> poäng = new Dictionary<string, int>();
poäng.Add("Alex", 42);
poäng.Add("Bella", 87);
poäng.Add("Carlos", 65);

Console.WriteLine(poäng["Alex"]);    // 42
Console.WriteLine(poäng["Bella"]);   // 87
```

```
Utskrift:
42
87
```

> 💬 _"Slår du upp en nyckel som inte finns — kraschar programmet. Det är därför du alltid bör kontrollera med ContainsKey() innan du hämtar."_

---

## ContainsKey och Remove

```csharp
Dictionary<string, int> poäng = new Dictionary<string, int>();
poäng.Add("Alex", 42);
poäng.Add("Bella", 87);

// Kontrollera innan du hämtar
if (poäng.ContainsKey("Alex"))
{
    Console.WriteLine("Alex har " + poäng["Alex"] + " poäng");
}

// Ta bort ett par
poäng.Remove("Bella");

Console.WriteLine("Antal kvar: " + poäng.Count);  // 1
```

```
Utskrift:
Alex har 42 poäng
Antal kvar: 1
```

---

## foreach på ett Dictionary

`foreach` på ett Dictionary ger dig ett `KeyValuePair` per varv:

```csharp
Dictionary<string, int> poäng = new Dictionary<string, int>();
poäng.Add("Alex", 42);
poäng.Add("Bella", 87);
poäng.Add("Carlos", 65);

foreach (KeyValuePair<string, int> par in poäng)
{
    Console.WriteLine(par.Key + " har " + par.Value + " poäng");
}
```

```
Utskrift:
Alex har 42 poäng
Bella har 87 poäng
Carlos har 65 poäng
```

---

## Dictionary eller List — när väljer du vad?

| Situation | Välj |
|-----------|------|
| Du vill slå upp med ett namn eller en kod | `Dictionary<K, V>` |
| Du loopar igenom i ordning | `List<T>` |
| Du behöver koppla ihop två saker (t.ex. namn → poäng) | `Dictionary<K, V>` |
| Du bara vill samla ihop flera värden av samma typ | `List<T>` |

En `List<T>` svarar på: _"Vad finns på plats nummer 3?"_
Ett `Dictionary` svarar på: _"Vad har Alex för poäng?"_

---

## enum — namngivna konstanter

En `enum` låter dig definiera en **uppsättning namngivna val**.

```csharp
enum Riktning
{
    Norr,
    Söder,
    Öst,
    Väst
}
```

Nu kan du använda `Riktning.Norr` i koden — i stället för siffran `0` eller strängen `"norr"`.

```csharp
Riktning kurs = Riktning.Norr;
Console.WriteLine(kurs);  // Norr
```

---

## Varför enum — inte strängar?

```csharp
// Utan enum — farligt
string riktning = "norre";   // stavfel — inga felmeddelanden

// Med enum — säkert
Riktning riktning = Riktning.Norr;   // kompilatorn fångar stavfel
```

Tre fördelar med enum:
- Kompilatorn kontrollerar att du väljer ett giltigt alternativ
- Intellisense visar alla möjliga val direkt i editorn
- Koden berättar vad som menas — inte ett magiskt tal eller en sträng

> 💬 _"Magic strings är ett av de vanligaste felen i kod som verkar fungera men sedan spricker på konstiga sätt. Enum löser det helt."_

---

## switch på enum

`switch` och `enum` passar perfekt ihop:

```csharp
enum Riktning { Norr, Söder, Öst, Väst }

Riktning vald = Riktning.Öst;

switch (vald)
{
    case Riktning.Norr:
        Console.WriteLine("Du åker norrut");
        break;
    case Riktning.Söder:
        Console.WriteLine("Du åker söderut");
        break;
    case Riktning.Öst:
        Console.WriteLine("Du åker österut");
        break;
    case Riktning.Väst:
        Console.WriteLine("Du åker västerut");
        break;
}
```

```
Utskrift:
Du åker österut
```

---

## Allt tillsammans

```csharp
// Dictionary — nyckel och värde
Dictionary<string, int> poäng = new Dictionary<string, int>();
poäng.Add("Alex", 42);
poäng.Add("Bella", 87);

if (poäng.ContainsKey("Alex"))
    Console.WriteLine("Alex: " + poäng["Alex"]);

foreach (KeyValuePair<string, int> par in poäng)
    Console.WriteLine(par.Key + " → " + par.Value);

// Enum — namngivna val
enum Årstid { Vår, Sommar, Höst, Vinter }

Årstid nu = Årstid.Sommar;
Console.WriteLine("Det är " + nu);  // Det är Sommar
```

---

<!-- _class: title -->

# Nu är det din tur

### Övningar finns i `exercises/`

🟢 `dictionary_ovning_latt.md` — bygg ditt CV med Dictionary  
🟡 `dictionary_phonebook.md` — skapa och söka i en telefonbok

_Ta det steg för steg. Använd tipsen om du fastnar._
