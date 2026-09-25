# Lästext — Dictionary

## Vad är ett dictionary?

En lista är bra för att hålla en sekvens av värden. Men ibland behöver du inte en _rad_ med data — du behöver en _uppslagstabell_.

Tänk på en telefonbok. Du letar inte igenom den från början till slut för att hitta Annas nummer. Du slår upp "Anna" och läser av numret direkt. Det är precis vad ett **dictionary** gör i kod.

Ett dictionary lagrar värden i **nyckel-värde-par**. Varje värde har en tillhörande nyckel — ett unikt ID du använder för att hämta värdet.

```csharp
// Nyckel: namn (string), Värde: telefonnummer (string)
Dictionary<string, string> telefonbok = new Dictionary<string, string>();
```

Telefonboksanalygin håller hela vägen: precis som ett namn i en telefonbok måste vara unikt (eller åtminstone tydligt), måste nyckeln i ett dictionary vara unik. Två poster kan inte ha samma nyckel.

**Se även:** [programmeringstermer/dictionary.md](../programmeringstermer/dictionary.md)

---

## Skapa och lägga till

Du skapar ett tomt dictionary och lägger sedan till par med `Add()`.

```csharp
Dictionary<string, string> telefonbok = new Dictionary<string, string>();

telefonbok.Add("Anna", "070-123 45 67");
telefonbok.Add("Björn", "073-987 65 43");
telefonbok.Add("Camilla", "076-555 00 11");
```

Varje anrop till `Add()` tar två argument: nyckeln och värdet. Ordningen de läggs till spelar ingen roll — du hämtar dem alltid via nyckeln, inte via position.

```mermaid
flowchart LR
    K1["Nyckel\n'Anna'"] --> V1["Värde\n'070-123 45 67'"]
    K2["Nyckel\n'Björn'"] --> V2["Värde\n'073-987 65 43'"]
    K3["Nyckel\n'Camilla'"] --> V3["Värde\n'076-555 00 11'"]
    style K1 fill:#1a5276,stroke:#154360,color:#fff
    style K2 fill:#1a5276,stroke:#154360,color:#fff
    style K3 fill:#1a5276,stroke:#154360,color:#fff
    style V1 fill:#2e86c1,stroke:#1a5276,color:#fff
    style V2 fill:#2e86c1,stroke:#1a5276,color:#fff
    style V3 fill:#2e86c1,stroke:#1a5276,color:#fff
```

Du kan också använda indexer-syntaxen `dict["nyckel"] = värde` för att lägga till eller uppdatera ett par. Skillnaden mot `Add()` är att indexer-syntaxen _skriver över_ om nyckeln redan finns, medan `Add()` kastar ett undantag.

```csharp
// Indexer — lägger till om nyckeln inte finns, uppdaterar om den finns
telefonbok["Anna"] = "070-999 00 00";
```

---

## Slå upp ett värde

Du hämtar ett värde med samma hakparentes-syntax som du använder för arrayer — men istället för ett numeriskt index anger du nyckeln.

```csharp
Dictionary<string, string> telefonbok = new Dictionary<string, string>
{
    { "Anna", "070-123 45 67" },
    { "Björn", "073-987 65 43" }
};

string annasNummer = telefonbok["Anna"];
Console.WriteLine(annasNummer);   // 070-123 45 67
```

Det viktiga att veta: om nyckeln _inte finns_ kraschar programmet med ett `KeyNotFoundException`. Det är precis som att söka i en telefonbok efter ett namn som inte finns där — men istället för ett tomt svar får du ett fel.

<details>
<summary>Säker hämtning med TryGetValue</summary>

När du inte är säker på om nyckeln finns är `TryGetValue` det säkra alternativet. Den returnerar `true` om nyckeln hittades och lägger värdet i en `out`-variabel — annars returnerar den `false` utan att krascha.

```csharp
string nummer;
if (telefonbok.TryGetValue("David", out nummer))
{
    Console.WriteLine("Hittade: " + nummer);
}
else
{
    Console.WriteLine("David finns inte i telefonboken.");
}
```

Det är det rekommenderade sättet när du inte kan garantera att nyckeln finns.

</details>

---

## ContainsKey och Remove

Innan du slår upp ett värde kan du kontrollera om nyckeln existerar med `ContainsKey()`. Du tar bort ett par med `Remove()`.

```csharp
Dictionary<string, string> telefonbok = new Dictionary<string, string>
{
    { "Anna", "070-123 45 67" },
    { "Björn", "073-987 65 43" }
};

// Kolla om en nyckel finns
Console.WriteLine(telefonbok.ContainsKey("Anna"));    // True
Console.WriteLine(telefonbok.ContainsKey("David"));   // False

// Ta bort ett par
telefonbok.Remove("Björn");
Console.WriteLine(telefonbok.ContainsKey("Björn"));   // False
```

`Remove()` returnerar `true` om nyckeln hittades och togs bort, och `false` om nyckeln inte fanns. Det kastar inget undantag om nyckeln saknas.

---

## Loopa med foreach

För att gå igenom alla par i ett dictionary använder du `foreach` med typen `KeyValuePair<TKey, TValue>`. Via `.Key` och `.Value` kommer du åt nyckel respektive värde.

```csharp
Dictionary<string, int> highscores = new Dictionary<string, int>
{
    { "Anna", 9500 },
    { "Björn", 7200 },
    { "Carina", 11400 }
};

foreach (KeyValuePair<string, int> post in highscores)
{
    Console.WriteLine(post.Key + ": " + post.Value + " poäng");
}
```

En viktig detalj: ordningen i ett dictionary är **inte garanterad**. Om du behöver en bestämd ordning måste du sortera resultatet separat. Det är en av skillnaderna mot en lista, där elementen alltid är i den ordning du lade till dem.

---

## När använder man Dictionary?

Välj dictionary när du vill **slå upp** ett värde med ett meningsfullt ID — ett namn, en produktkod, en stad. Välj lista när du vill ha en **sekvens** av värden och ordningen spelar roll.

| | List\<T\> | Dictionary\<TKey, TValue\> |
|---|---|---|
| Hämta element | Via position: `list[2]` | Via nyckel: `dict["Anna"]` |
| Kolla om något finns | `Contains(värde)` | `ContainsKey(nyckel)` |
| Typisk användning | Shoppinglista, highscore-lista | Telefonbok, konfiguration, räknare |

Några vanliga användningsfall för dictionary:

- **Räknare** — räkna hur många gånger ett ord förekommer i en text (`Dictionary<string, int>`)
- **Konfiguration** — mappa inställningsnamn till värden (`Dictionary<string, string>`)
- **Snabb uppslagning** — hämta en produkt via artikelnummer utan att loopa igenom hela listan

**Se även:** [programmeringstermer/dictionary.md](../programmeringstermer/dictionary.md)
