# Tentafrågor — Metoder

Öva inför tentan. Varje fråga har fyra alternativ — ett rätt, ett lite roligt, och två som verkar rimliga men inte stämmer.

---

## Fråga 1 — void som returtyp

Vad betyder `void` som returtyp på en metod?

A) Metoden utför något men returnerar inget värde
B) Metoden returnerar ett tomt strängobjekt
C) Metoden är privat och kan inte anropas utanför klassen
D) Det är latin för "jag lovar att bete mig" 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt — `void` betyder att metoden gör sitt jobb men inte skickar tillbaka något till den som anropade den.
**B)** Fel — `void` returnerar ingenting alls, inte ens en tom sträng. Det är en annan sak.
**C)** Fel — synlighet (privat/publik) styrs av `private` och `public`, inte av returtypen.
**D)** Löften i Latin hade troligen inte funkat lika bra i en kompilator.

</details>

---

## Fråga 2 — parameter vs argument

Vad är skillnaden mellan en parameter och ett argument?

A) En parameter är platshållaren i metodens definition, ett argument är det faktiska värdet som skickas in vid anropet
B) Parameter och argument är två ord för exakt samma sak
C) Ett argument definieras inuti metoden, en parameter skickas utifrån
D) En parameter är trevligare — argument låter som om det bråkar 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt — i `void Hälsa(string namn)` är `namn` en parameter. När du anropar `Hälsa("Anna")` är `"Anna"` argumentet.
**B)** Fel — de används ofta i vardagsspråk omväxlande, men tekniskt sett har de olika roller.
**C)** Fel — det är tvärtom. Parametern finns i definitionen, argumentet kommer utifrån vid anropet.
**D)** Inom programmering bråkar de faktiskt rätt sällan med varandra.

</details>

---

## Fråga 3 — namngivningskonvention

Vilken namngivningskonvention används för metodnamn i C#?

A) PascalCase — varje ord börjar med stor bokstav, till exempel `BeräknaTotal`
B) camelCase — första ordet med liten bokstav, resten med stor, till exempel `beräknaTotal`
C) snake_case — ord separeras med understreck, till exempel `beräkna_total`
D) SCREAMING_SNAKE_CASE — för att visa att metoden menar allvar 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt — C# använder PascalCase för metoder, i enlighet med Microsofts officiella riktlinjer.
**B)** Fel — camelCase används i C# för lokala variabler och parametrar, inte för metodnamn.
**C)** Fel — snake_case är vanligt i Python men används inte för metodnamn i C#.
**D)** SCREAMING_SNAKE_CASE är reserverat för konstanter i vissa andra språk — och för metoder som verkligen har något att säga.

</details>

---

## Fråga 4 — vad gör return?

Vad händer när en metod kör `return`?

A) Metoden avslutas och skickar tillbaka ett värde till den som anropade den
B) Metoden börjar om från början med samma värden
C) Programmet hoppar till nästa metod i filen
D) Koden tar ett djupt andetag och funderar på vad den gjort 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt — `return` avslutar metoden direkt och skickar tillbaka det angivna värdet (om returtypen inte är `void`).
**B)** Fel — om du vill köra om en metod måste du anropa den igen, `return` gör inte det automatiskt.
**C)** Fel — `return` hoppar tillbaka till anropsplatsen, inte till nästa metod i filen.
**D)** Det vore mysigt, men tyvärr är det inte hur kompilatorn fungerar.

</details>

---

## Fråga 5 — varför bryta ut kod i metoder?

Varför är det bra att bryta ut kod i egna metoder?

A) Det undviker upprepning, gör koden lättare att läsa och möjliggör återanvändning på flera ställen
B) Det gör att programmet körs snabbare eftersom kompilatorn optimerar metoder separat
C) Det är ett krav i C# — kod som inte ligger i en metod kompilerar inte
D) Så att man har fler ställen att lägga in kommentarer om vad man åt till lunch 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt — metoder följer DRY-principen (Don't Repeat Yourself), ökar läsbarheten och gör det enkelt att återanvända logik.
**B)** Fel — prestandavinsten är inte anledningen till att man bryter ut metoder; det handlar om struktur och underhållbarhet.
**C)** Fel — all kod i C# ligger visserligen inuti en klass, men du väljer själv hur du organiserar metoderna.
**D)** Kommentarer om lunch är alltid välkomna, men de räknas inte som ett designmönster.

</details>

---
