# IT-nördiska — variabler och datatyper 🦆

🟢

Du ska bygga en utvecklarprofil med variabler.
Innehållet är hämtat från IT-världens eget ordförråd.

---

## Del 1 — Skapa din utvecklarprofil

Fyll i variablerna med dina egna värden och skriv ut dem.

```csharp
using System;

class Program
{
    static void Main()
    {
        // Vad du kallar dig (text)
        string roll = "___";             // "fullstack", "backend", "frontend", ...

        // Ditt favoritprogrammeringsspråk
        string favoritSpråk = "___";     // "C#", "Python", "JavaScript", ...

        // Hur många commits du har gjort idag (heltal)
        int commits = 0;

        // Antal olösta buggar just nu (heltal)
        int buggar = 0;

        // Är du i "flow" just nu? (sant eller falskt)
        bool iFlow = false;

        // Hur många koppar kaffe idag (heltal)
        int kaffe = 0;

        // Hur många procent av koden har du testat (decimaltal 0.0–100.0)
        double testtäckning = 0.0;

        Console.WriteLine("=== UTVECKLARPROFIL ===");
        Console.WriteLine($"Roll:          {roll}");
        Console.WriteLine($"Språk:         {favoritSpråk}");
        Console.WriteLine($"Commits idag:  {commits}");
        Console.WriteLine($"Buggar kvar:   {buggar}");
        Console.WriteLine($"I flow:        {iFlow}");
        Console.WriteLine($"Kaffe:         {kaffe} koppar");
        Console.WriteLine($"Testtäckning:  {testtäckning}%");
    }
}
```

---

## Förväntat resultat

```
=== UTVECKLARPROFIL ===
Roll:          fullstack
Språk:         C#
Commits idag:  3
Buggar kvar:   7
I flow:        True
Kaffe:         2 koppar
Testtäckning:  42.5%
```

---

## Del 2 — Beräkna

Lägg till dessa rader efter utskriften:

```csharp
// Total commits den här veckan (du har jobbat 5 dagar lika mycket)
int commitsVeckan = commits * 5;
Console.WriteLine($"\nCommits den här veckan: {commitsVeckan}");

// Buggar fixade idag (du fixar hälften per dag)
double buggarFixadeIdag = buggar / 2.0;
Console.WriteLine($"Buggar fixade idag (uppskattning): {buggarFixadeIdag}");

// Behöver du mer kaffe? (sant om du har druckit färre än 3 koppar)
bool merKaffe = kaffe < 3;
Console.WriteLine($"Behöver mer kaffe: {merKaffe}");
```

---

## Del 3 — Gummianka-debugging 🦆

> **Gummianka-debugging** är en riktig teknik som programmerare använder.
> Du förklarar din kod högt för en gummiana — rad för rad.
> Ofta hittar du buggen själv när du hör dig säga det högt.

**Övningen:**
1. Hitta en bugg i koden nedan — vad är fel?

```csharp
int a = 10;
int b = 3;
double resultat = a / b;
Console.WriteLine($"10 delat med 3 är {resultat}");
```

2. Förklara för din granne (eller en imaginär anka) vad koden gör, rad för rad.
3. Ser du felet nu?

<details>
<summary>💡 Ledtråd</summary>

`a` och `b` är båda `int`. Division mellan två `int` ger ett `int` — decimaldelen kastas bort.
`10 / 3` blir `3`, inte `3.333...`

Fixa det genom att skriva `double resultat = a / (double)b;`

</details>

---

## Ordlista — IT-nördiska

| Term | Vad det betyder |
|------|-----------------|
| `frontend` | Det användaren ser — knappar, sidor, layout |
| `backend` | Servern, logiken, databasen bakom kulisserna |
| `fullstack` | Jobbar med både frontend och backend |
| `commit` | Spara en version av koden i Git |
| `bug` | Ett fel i koden som gör att det beter sig fel |
| `deploy` | Publicera koden så att riktiga användare ser den |
| `algoritm` | En steg-för-steg-plan för att lösa ett problem |
| `metod` | Ett namngivet kodblock som gör en sak |
| `refaktorera` | Skriva om kod utan att ändra vad den gör |
| `testtäckning` | Hur stor del av koden som testas automatiskt |
| `flow` | Tillståndet när man är djupt koncentrerad och koden bara rinner |
