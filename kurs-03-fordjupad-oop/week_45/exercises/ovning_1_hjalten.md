# Övning 1: din egen hjälte

**Hur ni jobbar:** ni sitter i par. **En skriver, en läser högt.** Byt roller efter varje deluppgift.
Skapa ett nytt konsolprojekt: `dotnet new console -o Hjalten`.

Mål: klara 🟢 på 15 minuter. Är ni klara tidigt, fortsätt med 🟡.

> Obs! Skriv klassen **efter** koden som körs i `Program.cs`. Ligger klassen före får ni felet CS8803.

---

## 🟢 Uppgift 1: klassen Hero

1. Skapa klassen `Hero`.
2. Ge den två fält: `public string Name;` och `public int Health;`.
3. Skapa en konstruktor som tar `name` och `health` och sparar dem i fälten.

<details markdown="1">
<summary>Ledtråd</summary>

Konstruktorn heter exakt som klassen och har ingen returtyp. Inuti skriver du `Name = name;`.

</details>

## 🟢 Uppgift 2: två hjältar som hälsar

1. Lägg till metoden `Greet()` i `Hero`. Den skriver ut `Hej, jag heter ...!` med hjältens namn.
2. Högst upp i `Program.cs`: skapa Mira (hälsa 100) och Tobbe (hälsa 80) med `new`.
3. Låt båda hälsa.

**Förväntad output:**
```
Hej, jag heter Mira!
Hej, jag heter Tobbe!
```

<details markdown="1">
<summary>Ledtråd</summary>

`Hero mira = new Hero("Mira", 100);` skapar ett objekt. Anropa metoden med `mira.Greet();`.

</details>

## 🟡 Uppgift 3: Mira får ont

1. Lägg till metoden `TakeDamage(int amount)` som minskar `Health` med `amount`.
2. Låt Mira ta 30 i skada.
3. Skriv ut hur mycket hälsa Mira och Tobbe har kvar.

**Förväntad output (efter hälsningarna):**
```
Mira har 70 hälsa kvar.
Tobbe har 80 hälsa kvar.
```

<details markdown="1">
<summary>Ledtråd</summary>

`Health -= amount;` är samma sak som `Health = Health - amount;`. Tobbe ska inte påverkas, eftersom han är ett eget objekt.

</details>

---

## Prata med din partner

Förklara för varandra med egna ord: vad gör `new`?
