# Tentafrågor — JSON och data

Öva inför tentan. Varje fråga har fyra alternativ: ett rätt, ett lite roligt och två som verkar rimliga men inte stämmer.

---

## Fråga 1 — JSON

Vad är JSON?

A) Ett textformat för data, där information skrivs som nycklar och värden
B) Ett programmeringsspråk
C) En databas som lagrar data i tabeller
D) En kompis som heter Jason 🧑

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt. JSON är läsbart för både människor och program, och används för att spara och skicka data.
**B)** JSON är ett dataformat, inte ett språk. Du kan inte skriva ett program i JSON.
**C)** JSON är text. Det kan sparas i en fil eller i en databas, men är inte en databas.
**D)** Trevlig kompis, men fel svar.

</details>

---

## Fråga 2 — Nyckel och värde

I JSON-raden `"Name": "Mira"`, vad är `"Mira"`?

A) Hjältens lösenord 🔑
B) Värdet (value)
C) Nyckeln (key)
D) En klass

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**A)** Ett värde är bara data, inte ett lösenord.
**B)** Rätt. Nyckeln är `"Name"`, det som beskriver uppgiften. Värdet är `"Mira"`.
**C)** Nyckeln är `"Name"`, alltså namnet på uppgiften.
**D)** Klasser finns i C#-koden, inte i JSON-texten.

</details>

---

## Fråga 3 — Serialisera

Vad betyder det att serialisera ett objekt?

A) Att göra om objektet till text, till exempel JSON
B) Att köra objektet i en loop tills det blir klart 😄
C) Att numrera alla objekt i en lista
D) Att göra om text till ett objekt

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt. Objektet i minnet blir en text som kan sparas eller skickas.
**B)** Loopar gör inget objekt klart.
**C)** Serialisera handlar om att göra data till text, inte om siffror.
**D)** Det är motsatsen och heter deserialisera.

</details>

---

## Fråga 4 — Deserialisera

Du har en JSON-text från en fil. Vad kallas det när du gör om den till ett objekt?

A) Kompilera
B) Serialisera
C) Deserialisera
D) Dekorera

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: C**

**A)** Kompilera gör om din kod till ett program, inte JSON till objekt.
**B)** Det går åt andra hållet: objekt blir text.
**C)** Rätt. Text blir objekt. I C# gör `JsonSerializer.Deserialize<T>()` det jobbet.
**D)** Dekorera är ett designmönster och ett helt annat ämne.

</details>

---

## Fråga 5 — JSON-symboler

Vilka tecken använder JSON för ett objekt respektive en lista?

A) Hakparenteser för objekt och klammerparenteser för listor
B) Klammerparenteser `{ }` för objekt och hakparenteser `[ ]` för listor
C) Vanliga parenteser `( )` för både objekt och listor
D) Smileys :) för alla 🙂

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**A)** Det är tvärtom.
**B)** Rätt. Ett objekt skrivs `{ "Name": "Mira" }` och en lista skrivs `[ ... ]`.
**C)** Vanliga parenteser används inte för objekt eller listor i JSON.
**D)** Glada tecken, men fel syntax.

</details>

---

## Fråga 6 — WriteIndented

Vad gör inställningen `WriteIndented`? (eller Formatting.Intended i Newtonsoft)

A) Krypterar JSON-texten
B) Sparar JSON-texten till en fil
C) Ger JSON med radbrytningar och indrag, så den går att läsa
D) Gör texten kursiv 💅

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: C**

**A)** Texten blir inte hemlig, bara snyggare.
**B)** Det gör `File.WriteAllText`. `WriteIndented` påverkar bara utseendet.
**C)** Rätt. Utan den hamnar allt på en enda rad.
**D)** Det finns ingen kursiv text i JSON.

</details>
