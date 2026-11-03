# Övning 3: abstract och interface

**Hur ni jobbar:** ni sitter i par. **En skriver, en läser högt.** Byt roller efter varje deluppgift.
Utgå från projektet från övning 2, där `Character`, `Hero`, `Monster` och `Wizard` redan finns.

> Obs! Klasserna och interfacet skrivs **efter** koden som körs i `Program.cs`.

Det här är sista övningen för dagen. Hinner ni inte klart fortsätter ni i läxan.

Varje uppgift börjar med en ruta: **Vad** det är, **när** man använder det och **hur** man skriver det.
Läs rutan först, gör uppgiften sedan.

---

## 🟢 Uppgift 1: abstract och Attack

> **Vad:** en `abstract class` är en ritning som aldrig blir ett objekt.
> **När:** när alla barn är *samma sorts sak* och delar fält, men ska svara olika. En karaktär i allmänhet finns inte, bara hjältar och monster.
> **Hur:** `abstract class Character` och `public abstract string Attack();`. Barnen skriver `override`.

1. Gör `Character` abstrakt: `abstract class Character`.
2. Lägg till `public abstract string Attack();` i `Character`.
3. Låt `Hero`, `Monster` och `Wizard` skriva över `Attack()` med `override`.
   - `Hero`: `Mira svingar svärdet!` (med hjältens namn)
   - `Monster`: `Gruff morrar och visar tänderna!`
   - `Wizard`: `Zelda kastar en gnista!`
4. Skriv ut `Attack()` för alla i listan.

Prova också att skriva `new Character("???", 10);`. Vad säger kompilatorn? Ta bort raden igen.

**Förväntad output:**
```
Mira svingar svärdet!
Gruff morrar och visar tänderna!
Zelda kastar en gnista!
```

<details markdown="1">
<summary>Ledtråd</summary>

Använd `$"{Name} svingar svärdet!"` så att namnet följer med. Glömmer en barnklass `Attack()` får ni ett kompileringsfel, och det är meningen.

</details>

## 🟢 Uppgift 2: IHealer och Wizard

> **Vad:** ett `interface` är ett löfte om vad en klass *kan göra*.
> **När:** när en förmåga kan finnas hos helt olika sorters saker. Trollkarlen kan läka, men hjälten och monstret kan inte det.
> **Hur:** `interface IHealer { void Heal(Character target); }`. Klassen skriver `: Character, IHealer` och bestämmer själv hur den läker.

1. Skapa `interface IHealer` med metoden `void Heal(Character target);`.
2. Låt `Wizard` använda interfacet: `class Wizard : Character, IHealer`.
3. Implementera `Heal`: öka målets `Health` med 20 och skriv `Zelda läker Mira.` (med rätt namn).

<details markdown="1">
<summary>Ledtråd</summary>

Basklassen står först i listan, sedan interfacet. Metoden måste vara `public`.

</details>

## 🟡 Uppgift 3: filtrera ut dem som kan läka

> **Vad:** `OfType<T>()` plockar ur en lista det som är av typen `T`.
> **När:** när listan innehåller blandade sorter och du bara vill åt dem som kan en viss sak.
> **Hur:** `scene.OfType<IHealer>()`. Det fungerar på ett interface och på en basklass.

1. Använd `scene.OfType<IHealer>()` i en `foreach` för att hitta alla som kan läka.
2. Låt var och en läka den första i listan, `scene[0]`.
3. Skriv ut hur mycket hälsa Mira har efteråt.

**Förväntad output (efter attackerna):**
```
Zelda läker Mira.
Mira har 120 hälsa.
```

<details markdown="1">
<summary>Ledtråd</summary>

```cs
foreach (IHealer healer in scene.OfType<IHealer>())
{
    healer.Heal(scene[0]);
}
```

`OfType<IHealer>()` plockar ut dem som kan läka. Hjälten och monstret hoppas över.

</details>

---

## Prata med din partner

När väljer man `abstract class`, och när väljer man `interface`? Tänk på "är en sorts" och "kan göra".

---

## Avsluta: byt kod och ge en gåva 🎁

Byt med ett annat par. Ni läser varandras kod och ger varandra återkoppling. Ta ca 10 minuter, fem åt varje håll.

1. Öppna det andra parets projekt och läs koden.
2. **Testkör programmet.** Skriver det ut samma sak som den förväntade outputen i uppgifterna?
3. **Leta efter faror.** Fundera på vad som händer när programmet inte får det det förväntar sig.
   Prova gärna i en **kopia** av projektet, så att originalet är kvar. Några frågor att utgå från:
   - Listan är tom och koden skriver `scene[0]`. Vad händer?
   - `Heal` får `null` i stället för en karaktär. Vad händer?
   - Mira har 120 hälsa efter att Zelda läkt henne. Borde hälsan kunna gå över 100?
   - Programmet läste in hälsan med `Console.ReadLine()`, och användaren skrev text i stället för en siffra. Vad skulle hända med `int.Parse("abc")`?

   Ni behöver inte *fixa* något. Det räcker att hitta och skriva ner det. Det är precis sådant som tester
   fångar i kursen om unit testing senare.
4. Berätta för dem:
   - 🟢 **Något som fungerar bra**, och var du ser det. ("Jag gillar att namnen på klasserna visar vad de gör.")
   - ❓ **En fråga** som du undrar över. ("Varför lade ni den metoden i basklassen?")
   - ⚠️ **En fara du hittade**, formulerad som en fråga. ("Vad tror ni händer om listan är tom?")
   - 💡 **Ett förslag**, formulerat som en möjlighet. ("Ni skulle kunna prova att ...")
5. Tacka varandra.

**Feedback är en gåva.** Den som ger den delar något den har tänkt på, för att den vill väl.
Den som tar emot den bestämmer själv vad gåvan ska bli: något att använda nu, något att fundera på
till senare, eller något att lämna kvar. Ingen gåva är ett krav, och ingen behöver försvara sig.
Det räcker att säga "tack".

**Att tänka på när du ger:** tala om koden, inte om personen ("koden gör ..." i stället för "du gör ..."),
och var konkret. Det som går att peka på går också att använda.
