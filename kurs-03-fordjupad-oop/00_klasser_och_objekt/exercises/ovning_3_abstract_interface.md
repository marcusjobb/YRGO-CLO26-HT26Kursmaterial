# Övning 3: abstract och interface

**Hur ni jobbar:** ni sitter i par. **En skriver, en läser högt.** Byt roller efter varje deluppgift.
Utgå från projektet från övning 2, där `Character`, `Hero`, `Monster` och `Wizard` redan finns.

> Obs! Klasserna och interfacet skrivs **efter** koden som körs i `Program.cs`.

Det här är sista övningen för dagen. Hinner ni inte klart fortsätter ni i läxan.

---

## 🟢 Uppgift 1: abstract och Attack

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

1. Skapa `interface IHealer` med metoden `void Heal(Character target);`.
2. Låt `Wizard` använda interfacet: `class Wizard : Character, IHealer`.
3. Implementera `Heal`: öka målets `Health` med 20 och skriv `Zelda läker Mira.` (med rätt namn).

<details markdown="1">
<summary>Ledtråd</summary>

Basklassen står först i listan, sedan interfacet. Metoden måste vara `public`.

</details>

## 🟡 Uppgift 3: filtrera ut dem som kan läka

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
