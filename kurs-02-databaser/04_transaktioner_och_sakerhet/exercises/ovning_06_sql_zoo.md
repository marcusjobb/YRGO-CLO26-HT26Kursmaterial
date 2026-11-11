# Övning 6 — SQL Zoo 🟡

Den här övningen är inte en koduppgift — den är träning på SQL direkt i webbläsaren.

SQL Zoo är en interaktiv sajt där du skriver riktiga SQL-queries mot riktiga databaser och ser resultaten direkt. Inget att installera, inget att konfigurera.

**Gå till:** [sqlzoo.net](https://sqlzoo.net)

---

## Vad du ska träna på

Dessa delar är det som krävs för VG på inlämningsuppgiften. Gör dem i ordning.

### 1. SELECT within SELECT (subqueries)

Tränar på att använda en SELECT-sats inuti en annan — precis som `WHERE Längd > (SELECT AVG(Längd) FROM Låtar)`.

👉 **SELECT within SELECT Tutorial** på SQL Zoo

Fokusera på frågorna som använder `>`, `IN` och `ALL` mot en innerfråga.

---

### 2. The JOIN operation

Tränar på att hämta data från två tabeller i samma query med `INNER JOIN` och `LEFT JOIN`.

👉 **The JOIN operation** på SQL Zoo

Lägg märke till `ON`-villkoret — det är hur tabellerna kopplas ihop.

---

### 3. More JOIN operations

Fortsättning på JOIN med fler tabeller inblandade.

👉 **More JOIN operations** på SQL Zoo

---

## Tips medan du tränar

- Testa att köra en query som du *vet* är fel — se vad felmeddelandet säger
- Skriv om queries med alias (`AS`) för att göra dem mer läsbara
- Tänk: "Hur skulle jag skriva den här queryn mot min musikdatabas?"

---

## Koppling till inlämningen

När du känner att JOIN och subquery sitter — gå tillbaka till din musiklistapp och lägg in ett av dem i koden. Det är VG-kravet.

Det är samma SQL, bara i C# istället för i webbläsaren.
