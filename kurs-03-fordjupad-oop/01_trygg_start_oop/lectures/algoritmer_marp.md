---
marp: true
theme: nion-dark
paginate: true
---

# Algoritmer och komplexitet

**Kurs:** Fördjupad OOP
**Modul:** 01 — Algoritmer

Marcus Ackre Medina · YRGO · CLO26

---

## Vad ska vi lära oss idag?

- Vad är en algoritm egentligen?
- Hur mäter vi hur bra en algoritm är?
- Big O — läsa och tolka utan att svimma
- Linjärsökning vs binärsökning — live
- Bubble Sort och Selection Sort — live
- Varför det spelar roll när din app växer

---

## En algoritm är bara ett recept

Du har gjort det tusen gånger.

1. Ta fram ingredienserna
2. Blanda i rätt ordning
3. Koka i rätt temperatur
4. Servera

Det är ett **recept** — en sekvens av steg som löser ett problem.  
En algoritm är samma sak, fast för kod.

> Alla program du skriver kör algoritmer.  
> Frågan är bara: *hur bra är de?*

---

## Hur mäter vi "bra"?

Tre vanliga mått:

| Mått | Frågan det svarar |
|------|-------------------|
| **Tid** | Hur länge tar det? |
| **Minne** | Hur mycket RAM används? |
| **Steg** | Hur många operationer? |

Vi fokuserar på **steg** — det är maskinoberoende.  
En algoritm som tar 3 steg är bättre än en som tar 3 000 000 steg.

---

## Big O — vad det egentligen betyder

**Big O** beskriver hur *antalet steg växer* när inputen växer.

Det är inte exakt. Det är en *storleksordning*.

```
n = antalet element vi söker igenom
```

| Notation | Utläses | Betyder |
|----------|---------|---------|
| O(1) | konstant | alltid lika lång tid |
| O(log n) | logaritmisk | halveras för varje steg |
| O(n) | linjär | dubbel input = dubbel tid |
| O(n²) | kvadratisk | dubbel input = fyra gånger längre |

---

## O(1) — konstant

```csharp
// Hämta ett element på en specifik plats
string name = names[42];
```

Spelar ingen roll om listan har 10 eller 10 miljoner namn.  
Det tar alltid **ett steg**.

---

## O(n) — linjär

```csharp
// Kolla varje element ett i taget
foreach (string name in names)
{
    if (name == "Marcus") return true;
}
```

10 namn → max 10 steg  
1 000 namn → max 1 000 steg  
1 000 000 namn → max 1 000 000 steg

Input dubbleras → tid dubbleras.

---

## O(log n) — logaritmisk

```csharp
// Halvera sökytan för varje steg (binärsökning)
int low = 0, high = names.Length - 1;
while (low <= high)
{
    int mid = (low + high) / 2;
    // ...
}
```

1 000 000 namn → max **20 steg**  
Dubbel input → **ett steg till**.

---

## O(n²) — kvadratisk

```csharp
// Jämför varje element mot varje annat element
for (int i = 0; i < names.Length; i++)
{
    for (int j = 0; j < names.Length; j++)
    {
        // ...
    }
}
```

10 namn → 100 steg  
1 000 namn → 1 000 000 steg  
Input dubbleras → tid fyrdubblas.

---

## Visuellt: hur de växer

```
Steg
│                                          O(n²)
│                                    ╱
│                               ╱
│                         ╱
│                   ╱
│             ╱           O(n)
│       ╱──────────────────────────────────
│ ╱
│ ─────────────────────────────────────── O(log n)
│ ═════════════════════════════════════════ O(1)
└──────────────────────────────────────── n
```

Skillnaden är **dramatisk** vid stora datamängder.

---

<!-- WOW-MOMENT: Linjärsökning vs binärsökning på 1 miljon namn — visa gissningsspelet live. Studerande ska gissa antalet steg. -->

## Live: söka i en lista

```csharp
// Linjärsökning — kolla ett i taget
static int LinearSearch(string[] names, string target)
{
    int steps = 0;
    foreach (string name in names)
    {
        steps++;
        if (name == target) return steps;
    }
    return -1;
}
```

**Fråga till klassen:**  
Vi har en lista med 20 namn.  
Värst möjliga fall — hur många steg?

---

## Binärsökning — förutsättning: sorterad lista

```csharp
// Binärsökning — halvera sökytan varje gång
static int BinarySearch(string[] names, string target)
{
    int low = 0, high = names.Length - 1, steps = 0;

    while (low <= high)
    {
        steps++;
        int mid = (low + high) / 2;
        int cmp = string.Compare(names[mid], target);

        if (cmp == 0) return steps;
        if (cmp < 0) low = mid + 1;
        else high = mid - 1;
    }
    return -1;
}
```

---

## Varför halvera?

Listan är sorterad. Om vi pekar på mitten:

- Om vi söker efter "Marcus" och mitten är "Olle" — Marcus är **längre bak**
- Allt framför Olle kan vi **stryka direkt**

Vi kastar hälften av listan efter varje steg.

```
[A ... M ... O ... Ö]
          ↑ mid = "Olle"
"Marcus" < "Olle" → kolla vänster halva
[A ... M]
```

---

## En miljon namn

```
Linjärsökning:   upp till 1 000 000 steg
Binärsökning:    upp till        20 steg
```

> **45 000 gånger snabbare.**

Testa live: [Gissningsspelet](https://claude.ai/artifact/8Cj8a1KHkGqA5AerH6We3f)

Välj "Del 3 — Skala upp" och kör 1M-läget.

---

## Dictionary — O(1) sökning

```csharp
// Bygg upp en Dictionary en gång — O(n)
var lookup = new Dictionary<string, int>();
foreach (string name in names)
    lookup[name] = 1;

// Sök sedan — O(1)
bool found = lookup.ContainsKey("Marcus");
```

För **upprepade sökningar** i samma data:  
Bygg en Dictionary och sök i den.  
Tar mer minne — men sökningen är konstant.

---

## Mäta steg i kod

```csharp
// Räkna faktiska steg — bra för att förstå komplexiteten
static (bool found, int steps) LinearSearch(string[] names, string target)
{
    int steps = 0;
    foreach (string name in names)
    {
        steps++;
        if (name == target) return (true, steps);
    }
    return (false, steps);
}
```

Övning 1 handlar om just det.

---

## Bubble Sort — "bubbla upp det största"

```csharp
// Jämför grannar, byt om de är i fel ordning
static void BubbleSort(int[] arr)
{
    for (int i = 0; i < arr.Length - 1; i++)
    {
        for (int j = 0; j < arr.Length - 1 - i; j++)
        {
            if (arr[j] > arr[j + 1])
            {
                // Byt plats
                (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
            }
        }
    }
}
```

**Komplexitet:** O(n²)

---

## Bubble Sort — vad händer?

```
Innan:  [5, 3, 8, 1, 9, 2]

Pass 1: [3, 5, 1, 8, 2, 9]  ← 9 är på plats
Pass 2: [3, 1, 5, 2, 8, 9]  ← 8 är på plats
Pass 3: [1, 3, 2, 5, 8, 9]  ← 5 är på plats
Pass 4: [1, 2, 3, 5, 8, 9]  ← klar
```

Det största elementet "bubblar upp" till slutet för varje pass.

---

## Selection Sort — "hitta det minsta"

```csharp
// Hitta minsta elementet, flytta det till rätt plats
static void SelectionSort(int[] arr)
{
    for (int i = 0; i < arr.Length - 1; i++)
    {
        int minIndex = i;

        for (int j = i + 1; j < arr.Length; j++)
        {
            if (arr[j] < arr[minIndex])
                minIndex = j;
        }

        // Byt minsta mot nuvarande position
        (arr[i], arr[minIndex]) = (arr[minIndex], arr[i]);
    }
}
```

**Komplexitet:** O(n²)

---

## Selection Sort — vad händer?

```
Innan:  [5, 3, 8, 1, 9, 2]

Pass 1: [1, 3, 8, 5, 9, 2]  ← 1 är på plats
Pass 2: [1, 2, 8, 5, 9, 3]  ← 2 är på plats
Pass 3: [1, 2, 3, 5, 9, 8]  ← 3 är på plats
Pass 4: [1, 2, 3, 5, 9, 8]  ← 5 är på plats
Pass 5: [1, 2, 3, 5, 8, 9]  ← klar
```

Varje pass väljer ut det minsta återstående elementet.

---

## Bubble Sort vs Selection Sort

| | Bubble Sort | Selection Sort |
|---|---|---|
| Komplexitet | O(n²) | O(n²) |
| Byten | Många (varje steg) | Få (en per pass) |
| Läsbarhet | Tydligare logik | Tydligare intention |
| Praktisk användning | Sällan | Sällan |

Båda är **pedagogiska verktyg** — i produktion använder vi `Array.Sort()`.

---

## LINQ — inbyggd och optimerad

```csharp
string[] names = { "Zane", "Marcus", "Anna", "Bella" };

// Sortera — Array.Sort använder IntroSort (O(n log n))
Array.Sort(names);

// LINQ — FirstOrDefault söker linjärt
string? found = names.FirstOrDefault(n => n == "Marcus");

// LINQ — OrderBy sorterar hela samlingen
var sorted = names.OrderBy(n => n).ToList();
```

`FirstOrDefault` på en osorterad lista → **O(n)**  
`FirstOrDefault` på en sorterad lista + `BinarySearch` → **O(log n)**

---

## Välj rätt datastruktur

| Behov | Välj | Komplexitet |
|-------|------|-------------|
| Upprepad sökning | `Dictionary<K,V>` | O(1) |
| Sorterad sökning | Sorterad lista + `BinarySearch` | O(log n) |
| En sökning, liten lista | `List<T>.Find()` | O(n) |
| Sortering | `Array.Sort()` eller `.OrderBy()` | O(n log n) |

---

## Sammanfattning

- **O(1)** — konstant, bäst möjliga
- **O(log n)** — logaritmisk, binärsökning, halveras varje steg
- **O(n)** — linjär, en genomgång av hela listan
- **O(n²)** — kvadratisk, undvik på stora listor

> En algoritm som känns "tillräckligt snabb" med 100 element  
> kan ta **evigheter** med 1 000 000 element.

---

## Det här är inte teori — det är verktyg

Du kommer att välja mellan `List.Find()` och `Dictionary` på ditt första jobb.  
Du kommer att förklara varför en funktion är seg i produktion.  
Du kommer att läsa kod och tänka: *"det där är O(n²) — det skalerar inte."*

Det är precis vad en junior developer behöver kunna.

---

## Övningar idag

- **Övn 1** — Implementera `LinearSearch`, räkna steg
- **Övn 2** — Implementera `BubbleSort`, mäta körtid vs `Array.Sort()`
- **Övn 3** — Sök i 100 000 namn: linjär, binär och Dictionary — jämför steg och tid
