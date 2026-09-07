# Tentafrågor — Villkor och loopar

Öva inför tentan. Varje fråga har fyra alternativ — ett rätt, ett lite roligt, och två som verkar rimliga men inte stämmer.

---

## Fråga 1 — else if

Vad betyder `else if` i en if-sats?

A) Det är ett alternativt villkor som testas om det föregående villkoret var falskt
B) Det är ett villkor som alltid körs sist, oavsett vad som hände tidigare
C) Det är samma sak som `else`, men med ett extra nyckelord för tydlighet
D) Det är ett Java-specifikt nyckelord som råkade hamna i C# av misstag 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt — `else if` låter dig testa ytterligare ett villkor, men bara om det föregående var falskt.
**B)** Fel — det som alltid körs sist (om inget annat matchade) är `else`, inte `else if`.
**C)** Fel — `else if` kräver ett villkor i parenteser, `else` gör det inte.
**D)** C# och Java liknar varandra på många ställen, men `else if` är helt hemma i C#.

</details>

---

## Fråga 2 — switch vs if

När är `switch` ett bättre val än `if`?

A) När du jämför en variabel mot ett antal kända, fasta värden
B) När du behöver jämföra två olika variabler med varandra
C) När villkoret innehåller en beräkning, till exempel `x * 2 > 10`
D) När du vill imponera på din handledare med ovanlig syntax 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt — `switch` passar perfekt när du vet vilka möjliga värden som finns, till exempel veckodagar eller menyval.
**B)** Fel — `switch` jämför en enda variabel mot fasta värden, inte två variabler mot varandra.
**C)** Fel — `switch` fungerar inte med uttryck som `x * 2 > 10`, det kräver konkreta värden att matcha mot.
**D)** Handledaren är säkert imponerad oavsett — men det är inte ett kriterium för att välja `switch`.

</details>

---

## Fråga 3 — while-loopens risk

Vad är den största risken med en `while`-loop?

A) Att den aldrig slutar köra för att villkoret alltid förblir sant
B) Att den körs en gång för lite jämfört med en `for`-loop
C) Att den inte kan använda en räknarvariabel
D) Att datorn blir trött och börjar protestera med felmeddelanden 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt — om villkoret aldrig blir falskt kör loopen för evigt, vilket kallas en oändlig loop.
**B)** Fel — `while` och `for` kör lika många gånger om de är skrivna rätt; det handlar inte om antal.
**C)** Fel — du kan absolut använda en räknarvariabel i en `while`-loop, du deklarerar den bara utanför.
**D)** Datorn protesterar inte — den kör bara på tills programmet avslutas utifrån eller kraschar.

</details>

---

## Fråga 4 — for vs foreach

Vad är den viktigaste skillnaden mellan `for` och `foreach`?

A) `for` använder ett index och räknar, `foreach` går igenom varje element utan att du hanterar indexet
B) `foreach` är snabbare än `for` och bör alltid användas istället
C) `for` fungerar bara med tal, `foreach` fungerar bara med strängar
D) `foreach` uppfanns för att `for` var för lätt att stava fel till 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt — `for` ger dig kontroll över index och antal steg, `foreach` ger dig varje element direkt utan att du behöver tänka på positionen.
**B)** Fel — det är inte en generell sanning; vilket som är bättre beror på situationen.
**C)** Fel — `for` fungerar med allt som kan räknas, och `foreach` fungerar med alla samlingar, inte bara strängar.
**D)** Stavningen av `for` är nog inte vad som avgjorde designbesluten i C#.

</details>

---

## Fråga 5 — &&-operatorn

Vad betyder `&&` i ett villkor?

A) Logiskt OCH — båda delvillkoren måste vara sanna för att hela uttrycket ska bli sant
B) Logiskt ELLER — minst ett av delvillkoren måste vara sant
C) Jämförelse — kontrollerar om två värden är exakt lika
D) En speciell operator som dubblar lyckan i koden 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt — `&&` är logiskt OCH, och kräver att båda sidor är sanna.
**B)** Fel — det du beskriver är `||` (logiskt ELLER).
**C)** Fel — jämförelse för likhet skrivs med `==`, inte `&&`.
**D)** Lyckan i koden mäts tyvärr inte i operatorer.

</details>

---

## Fråga 6 — utdata från en for-loop

Vad skriver följande kod ut?

```csharp
for (int i = 0; i < 3; i++)
    Console.WriteLine(i);
```

A) 0, 1, 2 (tre rader)
B) 1, 2, 3 (tre rader)
C) 0, 1, 2, 3 (fyra rader)
D) Tre rader med texten "i" — datorn vet ju inte vad i är för värde 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt — `i` börjar på 0, loopen kör så länge `i < 3`, alltså för värdena 0, 1 och 2.
**B)** Fel — `i` startar på 0, inte 1. Loopen skriver ut 0 första gången.
**C)** Fel — villkoret är `i < 3`, så 3 skrivs aldrig ut. Loopen stannar innan dess.
**D)** `i` är en variabel med ett riktigt heltalsvärde — C# skriver ut det, inte variabelns namn.

</details>

---
