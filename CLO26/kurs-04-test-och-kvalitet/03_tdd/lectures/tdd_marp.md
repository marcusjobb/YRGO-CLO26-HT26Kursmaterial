---
marp: true
theme: nion-dark
paginate: true
---

# Test-Driven Development

**Kurs:** Test och kvalitetssäkring
**Modul:** 03 — TDD
Marcus Ackre Medina · YRGO · CLO26

---

## Vad är TDD?

**Testdriven Utveckling** — skriv testet INNAN du skriver koden.

Det vänder upp och ner på det du är van vid.

> Skriv ett test som misslyckas.
> Skriv koden som får det att lyckas.
> Förbättra koden.

**🔴 → 🟢 → ♻️ — upprepa.**

---

## Traditionell vs TDD

**Traditionellt:**
```
Skriv kod → Manuellt testa → Hitta bug → Fixa → Testa igen
```

**TDD:**
```
Skriv test (🔴) → Skriv kod (🟢) → Refactor (♻️) → Repeat
```

TDD definierar **vad** koden ska göra innan du bestämmer **hur**.

---

## Red-Green-Refactor

```
     🔴 RED
      ↓
  Skriv ett test som misslyckas
      ↓
     🟢 GREEN
      ↓
  Skriv minsta möjliga kod för att
  få testet att passera
      ↓
     ♻️ REFACTOR
      ↓
  Förbättra koden — tester ska
  fortfarande vara gröna
      ↓
  Repeat 🔁
```

---

## Steg 1 — 🔴 RED: Skriv testet

```csharp
[Fact]
public void Add_TwoPositiveNumbers_ReturnsSum()
{
    // Arrange
    var calculator = new Calculator();

    // Act
    var result = calculator.Add(2, 3);

    // Assert
    Assert.Equal(5, result);
}
```

**Detta kompilerar inte ännu. Calculator finns inte. Det är OK.**

---

## Steg 2 — 🟢 GREEN: Minsta möjliga kod

```csharp
public class Calculator
{
    public int Add(int a, int b)
    {
        return 5; // Hårdkodat — men testet är grönt!
    }
}
```

Det känns dumt. Det är meningen.

Nu lägger vi till fler testfall för att tvinga fram riktig logik.

---

## Steg 2b — Fler testfall pressar koden

```csharp
[Theory]
[InlineData(2, 3, 5)]
[InlineData(10, 5, 15)]
[InlineData(-1, 1, 0)]
public void Add_VariousInputs_ReturnsCorrectSum(
    int a, int b, int expected)
{
    var calculator = new Calculator();
    Assert.Equal(expected, calculator.Add(a, b));
}
```

**Nu håller inte `return 5` längre. Vi måste skriva riktig logik.**

---

## Steg 2c — Riktig implementation

```csharp
public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b; // Äntligen riktig kod! 🟢
    }
}
```

Alla tester är gröna.

---

## Steg 3 — ♻️ REFACTOR: Förbättra

```csharp
// Kanske bättre metodnamn?
// Kanske extrahera logik?
// Kanske göra den mer generell?

public int Sum(params int[] numbers)
{
    return numbers.Sum(); // Mer flexibel
}
```

Tester är fortfarande gröna — det är din säkerhetsnät.

---

## Kents tre lagar

Kent Beck, uppfinnaren av TDD:

1. Skriv **ingen produktionskod** förrän du har ett misslyckande test
2. Skriv **bara tillräckligt** test för att det ska misslyckas
3. Skriv **bara tillräckligt** kod för att testet ska lyckas

**Håll dig till dessa tre = du kör TDD.**

---

## Varför TDD ger bättre design

Du tvinkas tänka på **hur koden ska användas** innan du designar den.

Resultat:
- Kod med tydliga, testbara gränssnitt
- Metoder med ett enda ansvar (SRP naturligt)
- Beroenden via interface (DI naturligt)
- Inga onödiga features

---

## TDD anti-patterns

❌ Skriva tester EFTER att koden är klar (det är inte TDD)
❌ Skriva tio tester innan något är grönt
❌ Skriva komplex kod direkt istället för minsta möjliga
❌ Hoppa över refactor-steget (koden försämras)
❌ Stora, komplexa tester (håll dem fokuserade)

---

## Regression protection

Varje nytt test är en grindvakt.

Om du ändrar koden och ett test blir rött: du vet exakt var du gick sönder något.

```
Dag 1: 3 gröna tester
Dag 5: 15 gröna tester
Dag 20: 40 gröna tester — och du sover gott
```

---

## TDD och dokumentation

Tester är levande dokumentation.

```csharp
// Testet berättar hur klassen ska användas:
[Fact]
public void BudgetCalculator_WithOverspend_LogsError()
{
    var budget = new BudgetCalculator();
    budget.SetIncome(10000);
    budget.AddExpense("Hyra", 12000);
    Assert.Contains("Övertrasserat", budget.Errors);
}
```

Ingen README behövs — testet visar allt.

---

## Övning: Calculator Kata

Vi bygger en Calculator med TDD:

1. **Add** — börja här
2. **Subtract**
3. **Multiply**
4. **Divide** — glöm inte division med noll!

**För varje metod:**
🔴 Skriv test → 🟢 Minsta kod → ♻️ Refactor → 🔁 Repeat

---

## Sammanfattning

- ✅ TDD = testa INNAN du kodar
- ✅ Red-Green-Refactor är cykeln
- ✅ Minsta möjliga kod för att bli grön
- ✅ Refactor utan rädsla — tester fångar regressioner
- ✅ Bättre design som bonus

**Nästa: SCRUM och agilt arbetssätt**
