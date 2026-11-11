# 2. Repository Pattern Implementation

🟢


Quiz:

1. Vad är huvudsyftet med Repository Pattern?

A) Att förbättra applikationens prestanda
B) Att skapa en abstraktionsnivå mellan dataåtkomst och affärslogik
C) Att ersätta traditionella databaser
D) Att förenkla användningen av REST API:er

<details>
<summary>Svar:</summary>
Rätt svar: B

```text
A) Fel - Detta är inte huvudsyftet med mönstret
B) Rätt - Repository Pattern skapar en abstraktionsnivå mellan dataåtkomst och affärslogik
C) Fel - Mönstret ersätter inte databaser utan abstraherar åtkomsten till dem
D) Fel - Detta är inte relaterat till Repository Patterns huvudsyfte
```

</details>

---

2. Varför används Nullable<T> eller Task<T?> i repository-mönstret?

A) För att förbättra prestandan
B) För att spara minnesutrymme
C) För att hantera null-värden på ett säkert sätt
D) För att konvertera mellan olika datatyper

<details>
<summary>Svar:</summary>
Rätt svar: C

```text
A) Fel - Nullable<T> används inte för prestandaförbättringar
B) Fel - Nullable<T> är inte designat för minnesoptimering
C) Rätt - Nullable<T> och null-checking operators (?., ??) används för att hantera null-värden säkert
D) Fel - Nullable<T> används inte primärt för typkonvertering
```

</details>

---

3. Vilken metod i IRepository-interfacet används för att kontrollera om en entitet existerar utan att hämta data?

A) GetByIdAsync()
B) GetAllAsync()
C) SaveAsync()
D) ExistsAsync()

<details>
<summary>Svar:</summary>
Rätt svar: D

```text
A) Fel - GetByIdAsync() hämtar hela entiteten
B) Fel - GetAllAsync() hämtar alla entiteter
C) Fel - SaveAsync() sparar eller uppdaterar en entitet
D) Rätt - ExistsAsync() kontrollerar endast existens utan att hämta data vilket gör den mer effektiv
```

</details>

---

4. Vad är syftet med TransactionScope i repository-implementationen?

A) Att optimera databasförfrågningar
B) Att hantera användarautentisering
C) Att säkerställa dataintegritet genom att hantera transaktioner
D) Att cachelagra databasresultat

<details>
<summary>Svar:</summary>
Rätt svar: C

```text
A) Fel - TransactionScope optimerar inte förfrågningar
B) Fel - TransactionScope hanterar inte autentisering
C) Rätt - TransactionScope säkerställer ACID-egenskaper genom att hantera databastransaktioner
D) Fel - TransactionScope hanterar inte cachelagring
```

</details>

---

5. Vilket syfte fyller den abstrakta metoden MapToEntity i AbstractRepository<TEntity>?

A) Att hantera databaskopplingar
B) Att validera inkommande data
C) Att konvertera databasrader till entitetsobjekt
D) Att generera SQL-frågor

<details>
<summary>Svar:</summary>
Rätt svar: C

```text
A) Fel - Databaskopplingar hanteras av DbContext
B) Fel - Metoden validerar inte data
C) Rätt - Metoden konverterar databasrader (IDataReader) till starkt typade entitetsobjekt
D) Fel - SQL-frågor genereras inte av denna metod
```

</details>

---
Sådärja. Nu har du koll på det här. Nästa steg — testa själv. Det är då det fastnar.
