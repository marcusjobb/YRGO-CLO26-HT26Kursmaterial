# Vikingakaraktären 🪓

🟢

Du ska skapa en vikingakaraktär med hjälp av variabler.
Varje egenskap hos vikingen är en variabel av rätt datatyp.

---

## Uppdraget

Fyll i variablerna nedan och skriv ut dem med `Console.WriteLine`.

```csharp
using System;

class Program
{
    static void Main()
    {
        // Vikingaens namn (text)
        string namn = "___";

        // Antal år gammal (heltal)
        int ålder = 0;

        // Vikt i kg (decimaltal)
        double vikt = 0.0;

        // Har vikingen en yxa? (sant eller falskt)
        bool harYxa = false;

        // Vikingaens favoritvapen (text)
        string vapen = "___";

        // Antal guldmynt (heltal)
        int guld = 0;

        // Skriv ut karaktären här
        Console.WriteLine("=== VIKINGAKARAKTÄR ===");
        Console.WriteLine($"Namn:    {namn}");
        Console.WriteLine($"Ålder:   {ålder} år");
        Console.WriteLine($"Vikt:    {vikt} kg");
        Console.WriteLine($"Yxa:     {harYxa}");
        Console.WriteLine($"Vapen:   {vapen}");
        Console.WriteLine($"Guld:    {guld} mynt");
    }
}
```

---

## Förväntat resultat

Exakt utseende beror på dina värden, men det ska se ut ungefär så här:

```
=== VIKINGAKARAKTÄR ===
Namn:    Björn Järnsida
Ålder:   34 år
Vikt:    112.5 kg
Yxa:     True
Vapen:   Tvåhandssvärd
Guld:    847 mynt
```

---

## Steg 2 — Beräkna

Lägg till dessa rader **efter** utskriften ovan:

```csharp
// Vikingen hittar 100 guldmynt till
int nyttGuld = guld + 100;
Console.WriteLine($"\nEfter plundringen: {nyttGuld} mynt");

// Om tio år
int åldernOmTioÅr = ålder + 10;
Console.WriteLine($"Om 10 år är {namn} {åldernOmTioÅr} år gammal.");
```

---

## Steg 3 — Nördat till max 🎮

Välj ett av dessa teman istället för vikingen och gör om hela övningen:

| Tema | Förslag på variabler |
|------|----------------------|
| **Minecraft** | spelnamn, antal diamanter, hälsopoäng, harDragon, svårighetsgrad |
| **Pippi Långstrump** | namn, hästensNamn, antalGuldmynt, kanLyfta Häst, favoritlek |
| **Pokémon** | tränarNamn, startPokémon, badges, harPokédex, tränarÅlder |
| **Star Wars** | namn, sida ("Ljussidan" / "Mörkersidan"), midiklorianCount, harLjussabel, rank |

Samma struktur, annat tema. Välj det du tycker är roligast.
