# Övning 2: arv

**Hur ni jobbar:** ni sitter i par. **En skriver, en läser högt.** Byt roller efter varje deluppgift.
Fortsätt i samma projekt som förra övningen, eller skapa ett nytt: `dotnet new console -o Arv`.

> Obs! Klasserna skrivs **efter** koden som körs i `Program.cs`.

Målet är att både hjältar och monster delar `Name` och `Health` från en gemensam basklass, men svarar olika.

---

## 🟢 Uppgift 1: Character, Hero och Monster

1. Skapa klassen `Character` med `Name`, `Health` och en konstruktor.
2. Skapa `Hero` och `Monster` som ärver från `Character` (`: Character`).
3. Ge varje barnklass en konstruktor som skickar vidare till basklassen med `: base(name, health)`.

<details markdown="1">
<summary>Ledtråd</summary>

```cs
public Hero(string name, int health) : base(name, health) { }
```

Klammerparenteserna är tomma. Basklassen sköter allt arbete.

</details>

## 🟢 Uppgift 2: olika svar med override

1. Lägg till `public virtual string Speak()` i `Character`. Den returnerar `"..."`.
2. Skriv över metoden i `Hero` med `override`. Hjälten svarar `Jag räddar dagen!`.
3. Skriv över den i `Monster`. Monstret svarar `GRRR!`.
4. I `Program.cs`: skapa en `List<Character>` med Mira (hälsa 100) och Gruff (hälsa 60). Skriv ut `Namn: svar` för var och en.

**Förväntad output:**
```
Mira: Jag räddar dagen!
Gruff: GRRR!
```

<details markdown="1">
<summary>Ledtråd</summary>

`virtual` står i basklassen och betyder att barnen får ändra metoden. `override` står i barnklassen.

</details>

## 🟡 Uppgift 3: Wizard

1. Skapa en tredje barnklass, `Wizard`, som ärver från `Character`.
2. Låt den svara `Abrakadabra!`.
3. Lägg till Zelda (hälsa 70) i scenen.

**Förväntad output:**
```
Mira: Jag räddar dagen!
Gruff: GRRR!
Zelda: Abrakadabra!
```

<details markdown="1">
<summary>Ledtråd</summary>

Du behöver inte ändra något annat än att lägga till klassen och Zelda. Listan tar emot vilken `Character` som helst.

</details>

---

## Prata med din partner

Vad fick ni Zelda att säga? Det finns inget rätt svar. Välj det som gör er glada.
