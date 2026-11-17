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

## Fråga 6 — namngivning av variabler

Vilken namngivningskonvention används för lokala variabler i C#?

A) PascalCase — `AntalSpelares`
B) camelCase — `antalSpelare`
C) snake_case — `antal_spelare`
D) UPPERCASE — `ANTALSPELARE`, för att man ska märka dem på avstånd 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**A)** PascalCase används för klasser, metoder och properties — inte för lokala variabler.
**B)** Rätt. Lokala variabler och parametrar skrivs med camelCase i C#: `int antalSpelare`, `string spelarNamn`.
**C)** snake_case är Pythons konvention och används inte i C#.
**D)** UPPERCASE är reserverat för konstanter (`const int MAX_SPELARE = 4`) i de sammanhang det används — och det ser man sällan ens då i C#.

</details>

---

## Fråga 7 — Single Responsibility Principle

Vad innebär Single Responsibility Principle (SRP)?

A) En klass ska ha exakt en metod
B) En klass ska ha ett och samma ansvarsområde — en enda anledning att ändras
C) Varje metod får max ett metodanrop inuti sig
D) Principen om att alltid byta namngivning en gång per sprint 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**A)** SRP handlar om ansvar, inte antalet metoder. En klass med ett ansvar kan ha många metoder.
**B)** Rätt. En `BankAccount`-klass ska sköta kontologik — inte skriva ut kvitton, skicka mejl och räkna moms. Varje av de sakerna är ett separat ansvar som hör hemma i en separat klass.
**C)** Ingen sådan regel finns — det vore ohållbart i praktiken.
**D)** Det är inte SRP — det är en källa till kaos.

</details>

---

## Fråga 8 — när skriver man kommentarer?

När är det lämpligt att skriva en kommentar i koden?

A) Alltid — varje rad ska ha en kommentar som förklarar vad den gör
B) Aldrig — koden ska tala för sig själv utan kommentarer
C) När något inte är uppenbart: en dold begränsning, ett knepigt val, eller en workaround för ett specifikt problem
D) Bara på fredagar, så kommentarerna får vila över helgen 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: C**

**A)** En kommentar som förklarar *vad* tydlig kod gör är brus — den läggs ovanpå något som redan syns.
**B)** Ibland är *varför* inte uppenbart från koden — det är precis det en kommentar ska förklara.
**C)** Rätt. Skriv kommentarer för det som koden inte kan berätta själv: "Vi halverar beloppet här pga bankens API-begränsning på 500 kr/transaktion." Det är värdefull information.
**D)** Kommentarer tar aldrig ledigt, men de behöver inte arbeta onödigt heller.

</details>

---
