---
marp: true
theme: nion-dark
paginate: true
---

<!-- _class: title -->

# Arrayer och listor

### Lagra många värden på ett ställe

_Kurs 01 · Vecka 6 · Nion Education_

---

## Problemet utan arrayer

Tänk dig att du ska hålla koll på alla veckodagar:

```csharp
string dag1 = "Måndag";
string dag2 = "Tisdag";
string dag3 = "Onsdag";
string dag4 = "Torsdag";
string dag5 = "Fredag";

Console.WriteLine(dag1);
Console.WriteLine(dag2);
// ... och så vidare
```

Fem variabler för fem dagar. Vad händer när du har 100 produkter?
Eller alla kommuner i Sverige?

**Det finns ett bättre sätt.**

---

## Array — fast storlek, samma typ

En array är en **ordnad samling av värden av samma typ**.
Storleken bestäms när du skapar den — och ändras aldrig.

```csharp
string[] veckodagar = { "Måndag", "Tisdag", "Onsdag", "Torsdag", "Fredag" };
```

```
Index:        0          1          2          3          4
         ┌─────────┬──────────┬──────────┬──────────┬──────────┐
         │ Måndag  │ Tisdag   │ Onsdag   │ Torsdag  │ Fredag   │
         └─────────┴──────────┴──────────┴──────────┴──────────┘
```

Fem dagar. En variabel. En rad kod.

> 💬 _"En array är som en parkeringsplats med numrerade rutor. Platsen finns — oavsett om det står en bil där eller inte."_

---

## Indexering — hämta ett värde

Varje plats i arrayen har ett **index** som börjar på noll.

```csharp
string[] veckodagar = { "Måndag", "Tisdag", "Onsdag", "Torsdag", "Fredag" };

Console.WriteLine(veckodagar[0]);  // Måndag
Console.WriteLine(veckodagar[1]);  // Tisdag
Console.WriteLine(veckodagar[4]);  // Fredag
```

```
Utskrift:
Måndag
Tisdag
Fredag
```

**Nollindexering** är en av de absolut vanligaste källorna till förvirring i programmering.
Första elementet är alltid index `0` — inte `1`.

---

## Längd och for-loop

`Length` berättar hur många element arrayen har.
Kombinerat med `for` kan du gå igenom varje element:

```csharp
string[] veckodagar = { "Måndag", "Tisdag", "Onsdag", "Torsdag", "Fredag" };

Console.WriteLine(veckodagar.Length);  // 5

for (int i = 0; i < veckodagar.Length; i++)
{
    Console.WriteLine("Dag " + (i + 1) + ": " + veckodagar[i]);
}
```

```
Utskrift:
5
Dag 1: Måndag
Dag 2: Tisdag
Dag 3: Onsdag
Dag 4: Torsdag
Dag 5: Fredag
```

---

## foreach på en array

`foreach` är enklare att läsa när du inte behöver indexet:

```csharp
string[] veckodagar = { "Måndag", "Tisdag", "Onsdag", "Torsdag", "Fredag" };

foreach (string dag in veckodagar)
{
    Console.WriteLine("Dag: " + dag);
}
```

```
Utskrift:
Dag: Måndag
Dag: Tisdag
Dag: Onsdag
Dag: Torsdag
Dag: Fredag
```

Läs det högt: _"För varje `dag` i `veckodagar` — skriv ut den."_

> 💬 _"Använd `for` när du behöver indexet. Använd `foreach` när du bara vill komma åt värdena."_

---

## List\<T\> — dynamisk storlek

En `List<T>` fungerar som en array — men du kan lägga till och ta bort element när som helst.

```csharp
List<string> shoppinglista = new List<string>();

shoppinglista.Add("Mjölk");
shoppinglista.Add("Bröd");
shoppinglista.Add("Ägg");

Console.WriteLine(shoppinglista.Count);  // 3
```

```
Utskrift:
3
```

`T` är typen du vill lagra — `List<string>`, `List<int>`, `List<double>`.

---

## List — metoder

De viktigaste metoderna du behöver känna till:

```csharp
List<string> shoppinglista = new List<string>();

shoppinglista.Add("Mjölk");       // Lägg till
shoppinglista.Add("Bröd");
shoppinglista.Add("Ägg");

shoppinglista.Remove("Mjölk");    // Ta bort ett specifikt värde

bool hittad = shoppinglista.Contains("Bröd");  // true

Console.WriteLine("Antal: " + shoppinglista.Count);   // 2
Console.WriteLine("Finns Bröd? " + hittad);           // True
```

```
Utskrift:
Antal: 2
Finns Bröd? True
```

---

## foreach på en lista

`foreach` fungerar exakt likadant på en `List<T>` som på en array:

```csharp
List<string> shoppinglista = new List<string>();
shoppinglista.Add("Mjölk");
shoppinglista.Add("Bröd");
shoppinglista.Add("Ägg");

foreach (string vara in shoppinglista)
{
    Console.WriteLine("Köp: " + vara);
}
```

```
Utskrift:
Köp: Mjölk
Köp: Bröd
Köp: Ägg
```

Det spelar ingen roll om du loopar en array eller lista — syntaxen är densamma.

---

## Array eller List — när väljer du vad?

| Situation | Välj |
|-----------|------|
| Antalet element är känt och ändras inte | `Array` |
| Du lägger till eller tar bort element | `List<T>` |
| Du loopar igenom och läser värden | Båda fungerar |
| Prestanda är kritisk och storleken är fast | `Array` |
| Du är osäker | `List<T>` — det är det säkrare valet |

De flesta program du skriver nu kommer att använda `List<T>`.
Array är fortfarande viktigt att förstå — det finns överallt i äldre kod.

---

## Allt tillsammans

```csharp
// Array — fast storlek
string[] veckodagar = { "Måndag", "Tisdag", "Onsdag", "Torsdag", "Fredag" };
Console.WriteLine(veckodagar[0]);      // Måndag
Console.WriteLine(veckodagar.Length);  // 5

for (int i = 0; i < veckodagar.Length; i++)
    Console.WriteLine(veckodagar[i]);

// List — dynamisk storlek
List<string> shoppinglista = new List<string>();
shoppinglista.Add("Mjölk");
shoppinglista.Add("Bröd");
shoppinglista.Remove("Mjölk");
Console.WriteLine(shoppinglista.Count);  // 1

foreach (string vara in shoppinglista)
    Console.WriteLine(vara);
```

---

<!-- _class: title -->

# Nu är det din tur

### Övningar finns i `exercises/`

🟢 `array_exercises.md` — skapa en array och loopa igenom den  
🟡 `array_exercises.md` — manipulera en array med slumptal och beräkningar  
🔴 `array_exercises.md` — sök i en array med egna metoder

_Ta det steg för steg. Använd tipsen om du fastnar._
