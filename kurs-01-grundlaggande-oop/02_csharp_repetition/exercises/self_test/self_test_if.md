# Testa dig själv — If-satser

Välj det alternativ du tror stämmer — öppna sen förklaringen och se om du hade rätt.

---

### Fråga 1: Du ska kontrollera om `x` är större än `y`. Vilket alternativ är rätt?

- A) `if (x > y)`
- B) `if (x < y)`
- C) `if (x = y)`
- D) `if (x >= y)`

<details><summary>Svar</summary>

**Rätt svar: A**

**A) `if (x > y)`** — Rätt. `>` betyder "strikt större än". Om x är 10 och y är 5 är villkoret sant.

**B) `if (x < y)`** — Fel. `<` betyder "mindre än" — precis tvärtom.

**C) `if (x = y)`** — Fel, och vanlig fallgrop. `=` är en *tilldelning*, inte en jämförelse. Det ger kompileringsfel i if-satser.

**D) `if (x >= y)`** — Nära, men fel. `>=` är sant även när x och y är lika. Frågan sa "större än", inte "större än eller lika med".

</details>

---

### Fråga 2: Du ska kontrollera om `pris` är exakt lika med `cash`. Vilket alternativ är rätt?

- A) `if (cash = pris)`
- B) `if (cash != pris)`
- C) `if (cash == pris)`
- D) `if (cash === pris)`

<details><summary>Svar</summary>

**Rätt svar: C**

**A) `if (cash = pris)`** — Fel. `=` är tilldelning, inte jämförelse. Kompilatorn klagar (eller värre: i vissa fall ändrar det faktiskt värdet utan att du märker).

**B) `if (cash != pris)`** — Fel. `!=` betyder "inte lika med" — villkoret är sant när de *skiljer sig*, inte när de är lika.

**C) `if (cash == pris)`** — Rätt. Dubbla likhetstecken `==` är jämförelseoperatorn i C#.

**D) `if (cash === pris)`** — Fel (och lite kul). Det är JavaScript-syntax. I C# finns inte `===` — det ger kompileringsfel.

</details>

---

### Fråga 3: Vad kontrollerar `if (poäng >= 50)`?

- A) Om poäng är exakt 50
- B) Om poäng är mer än 50 men inte 50 exakt
- C) Om poäng är 50 eller mer
- D) Om poäng inte är 50

<details><summary>Svar</summary>

**Rätt svar: C**

**A) Om poäng är exakt 50** — Fel. Det vore `if (poäng == 50)`.

**B) Om poäng är mer än 50 men inte 50 exakt** — Fel. Det vore `if (poäng > 50)`. `>=` inkluderar gränsvärdet.

**C) Om poäng är 50 eller mer** — Rätt. `>=` betyder "större än *eller lika med*". 50, 51, 100 — alla passerar.

**D) Om poäng inte är 50** — Fel. Det vore `if (poäng != 50)`.

</details>

---

### Fråga 4: Vad är skillnaden mellan `=` och `==` i C#?

- A) Det är exakt samma sak, `==` är bara mer tydlig
- B) `=` jämför två värden, `==` tilldelar ett värde
- C) `=` tilldelar ett värde, `==` jämför två värden
- D) `==` är för att dubbelkolla att `=` gick rätt

<details><summary>Svar</summary>

**Rätt svar: C**

**A) Det är exakt samma sak** — Fel. De gör fundamentalt olika saker. Att blanda ihop dem är ett av de vanligaste nybörjarfelen.

**B) `=` jämför, `==` tilldelar** — Fel. Tvärtom. Lätt att blanda ihop — kom ihåg: ett likhetstecken = sätt in, dubbla == kolla.

**C) `=` tilldelar ett värde, `==` jämför två värden** — Rätt. `x = 5` ger x värdet 5. `x == 5` ställer frågan "är x lika med 5?"

**D) `==` är för att dubbelkolla att `=` gick rätt** — Fel, men kreativt tänkt.

</details>

---

### Fråga 5: Du har `int ålder = 17`. Vad händer?

```csharp
if (ålder >= 18)
    Console.WriteLine("Välkommen in!");
```

- A) "Välkommen in!" skrivs ut
- B) Koden kraschar eftersom det saknas ett `else`
- C) Ingenting skrivs ut
- D) "Välkommen in!" skrivs ut men med ett varningsmeddelande

<details><summary>Svar</summary>

**Rätt svar: C**

**A) "Välkommen in!" skrivs ut** — Fel. 17 >= 18 är falskt. Villkoret uppfylls inte.

**B) Koden kraschar eftersom det saknas `else`** — Fel. `else` är valfritt. Om det saknas och villkoret är falskt händer helt enkelt ingenting.

**C) Ingenting skrivs ut** — Rätt. Villkoret är falskt, raden innanför hoppas över. Programmet fortsätter med nästa rad efter if-satsen.

**D) Skrivs ut med ett varningsmeddelande** — Fel. C# lägger inte på varningsmeddelanden i Console.WriteLine.

</details>

---

### Fråga 6: Vilket av dessa är korrekt C#-syntax för en if-sats?

- A) `if x > 5 { }`
- B) `IF (x > 5) { }`
- C) `if [x > 5] { }`
- D) `if (x > 5) { }`

<details><summary>Svar</summary>

**Rätt svar: D**

**A) `if x > 5 { }`** — Fel. Parenteserna kring villkoret är obligatoriska i C#.

**B) `IF (x > 5) { }`** — Fel (och klassisk fälla). C# är skiftlägeskänsligt. `IF` med stora bokstäver är inte nyckelordet `if`.

**C) `if [x > 5] { }`** — Fel. Hakparenteser `[]` används för arrayer i C#, inte för villkor.

**D) `if (x > 5) { }`** — Rätt. Nyckelord med liten bokstav, villkoret inom parentes, kroppen inom klammerparenteser.

</details>
