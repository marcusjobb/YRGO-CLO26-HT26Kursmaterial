---

title: Vad är TDD?
author: Marcus Ackre Medina
type: lecture
topic: testing
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/2025/4_test_and_quality_assurance/lectures/tdd_intro/tdd_intro_marp.md"
description: "Test-Driven Development = Testdriven utveckling"
tags: ["csharp", "marp", "tdd", "tdd?", "testing", "visual-studio"]
week_fit: []
---

# Vad är TDD?

🟢


## Test-Driven Development

**Skriv testet först, koden sen**

---

## Vad är TDD?

**Test-Driven Development = Testdriven utveckling**

> Skriv ett test som misslyckas,
> Skriv kod som får testet att lyckas,
> Förbättra koden.

**Testa INNAN du kodar! 🔴 → 🟢 → ♻️**

---

## Varför TDD?

**Traditionellt:**

```
Skriv kod → Testa manuellt → Hittar bug → Fixa → Testa igen
```

**Med TDD:**

```
Skriv test (rött) → Skriv kod (grönt) → Refactor → Repeat
```

**Fördelar:**

- ✅ Färre buggar
- ✅ Bättre design
- ✅ Trygg refactoring
- ✅ Levande dokumentation

---

## RED-GREEN-REFACTOR Cykeln

```
     🔴 RED
      ↓
   Skriv test
   (misslyckas)
      ↓
     🟢 GREEN
      ↓
  Skriv minsta
  möjliga kod
      ↓
     ♻️ REFACTOR
      ↓
   Förbättra kod
   (tester gröna)
      ↓
   Repeat! 🔁
```

---

## Steg 1: 🔴 RED - Skriv Test Först

**Innan du skriver någon kod, skriv testet:**

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

**Detta kompilerar INTE än! Calculator finns inte! 🔴**

---

## Steg 2: 🟢 GREEN - Minsta Möjliga Kod

**Skriv minsta kod för att få testet grönt:**

```csharp
public class Calculator
{
    public int Add(int a, int b)
    {
        return 5;  // Hårdkodat! Men testet är grönt! 🟢
    }
}
```

**Ja, detta känns dumt. Men det är poängen!**
Vi skriver minsta möjliga kod.

---

## Steg 2b: 🟢 GREEN - Lägg Till Fler Tester

**Lägg till fler tester för att tvinga fram rätt logik:**

```csharp
[Theory]
[InlineData(2, 3, 5)]
[InlineData(10, 5, 15)]
[InlineData(-1, 1, 0)]
public void Add_TwoNumbers_ReturnsSum(int a, int b, int expected)
{
    var calculator = new Calculator();
    var result = calculator.Add(a, b);
    Assert.Equal(expected, result);
}
```

**Nu måste vi skriva riktig logik! 🔴 → 🟢**

---

## Steg 2c: 🟢 GREEN - Riktig Implementering

```csharp
public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;  // Äntligen riktig kod! 🟢
    }
}
```

**Alla tester är gröna! ✅**

---

## Steg 3: ♻️ REFACTOR - Förbättra Kod

**Nu när tester är gröna, förbättra koden:**

```csharp
// Kanske extrahera logik?
// Kanske bättre namn?
// Kanske bryt ut i mindre metoder?

public int Add(params int[] numbers)
{
    return numbers.Sum();  // Mer flexibel!
}
```

**Tester är fortfarande gröna = trygg refactoring! 🟢**

---

## TDD i Praktiken - Calculator Exempel

**1. RED - Skriv test för Subtract:**

```csharp
[Fact]
public void Subtract_TwoNumbers_ReturnsDifference()
{
    var calc = new Calculator();
    var result = calc.Subtract(5, 3);
    Assert.Equal(2, result);
}
```

**🔴 Kompilerar inte! Subtract finns inte.**

---

## TDD i Praktiken - Calculator (forts.)

**2. GREEN - Minsta kod:**

```csharp
public int Subtract(int a, int b)
{
    return 2;  // Hårdkodat! Men grönt! 🟢
}
```

**3. Lägg till fler tester:**

```csharp
[Theory]
[InlineData(5, 3, 2)]
[InlineData(10, 5, 5)]
[InlineData(0, 0, 0)]
public void Subtract_TwoNumbers_ReturnsDifference(int a, int b, int expected)
```

---

## TDD i Praktiken - Calculator (forts.)

**4. GREEN - Riktig kod:**

```csharp
public int Subtract(int a, int b)
{
    return a - b;  // Äntligen riktig logik! 🟢
}
```

**5. REFACTOR - Förbättra om behövs**
(I detta fall är det redan enkelt!)

**Repeat för Multiply, Divide, etc! 🔁**

---

## TDD Rules (Kent Beck)

**3 lagar för TDD:**

1. **Skriv INGEN produktionskod** förrän du har ett misslyckande test
2. **Skriv BARA tillräckligt** test för att misslyckas (inklusive kompileringsfel)
3. **Skriv BARA tillräckligt** produktionskod för att få testet att lyckas

**Håll dig till dessa regler = TDD! 🎯**

---

## TDD Anti-Patterns (Undvik!)

❌ **Skriva tester EFTER koden** (det är inte TDD)
❌ **Skriva flera tester innan någon kod** (RED-GREEN-REFACTOR en i taget)
❌ **Skriva komplex kod direkt** (börja med enklaste lösningen)
❌ **Hoppa över refactoring** (kod blir smutsig!)
❌ **Stora tester** (små, fokuserade tester!)

---

## TDD Benefits - Varför är det värt det?

**Design:**

- Tänker på hur koden SKA användas (API-design först)
- Tvingar fram SOLID-principer
- Små, testbara metoder

**Kvalitet:**

- Färre buggar (täckning från start)
- Trygg refactoring (tester fångar regressions)
- Levande dokumentation (tester visar användning)

**Dopamin:\*\*\*\***

- 🟢 Gröna tester = framsteg!
- Snabb feedback-loop
- Tydligt när du är klar

---

## TDD vs. Test-After

**Test-After (traditionellt):**

```
Kod → Manuell test → Bug → Fixa → Test igen
```

- Testar vad koden **GÖR**
- Ofta glömmer edge cases
- Svårt att testa i efterhand

**TDD:**

```
Test → Kod → Refactor → Repeat
```

- Definierar vad koden SKA göra
- Edge cases från början
- Kod designad för testbarhet

---

## Idag: Calculator Kata med TDD

**Vi ska bygga en Calculator med TDD:**

1. **Add** - Börja här!
2. **Subtract**
3. **Multiply**
4. **Divide** (glöm inte division by zero!)

**För varje metod:**

1. 🔴 Skriv test (rött)
2. 🟢 Skriv minsta kod (grönt)
3. ♻️ Refactor (om behövs)
4. Repeat!

---

## Tips för TDD-Nybörjare

**Börja smått:**

- En metod i taget
- En feature i taget
- Enklaste testet först

**Tänk i baby steps:**

- Inte för stora hopp
- Minsta möjliga kod
- Refactor i små steg

**Trust the process:**

- Det känns konstigt först
- Håll dig till RED-GREEN-REFACTOR
- Dopamin kommer med gröna tester! 🟢

---

## Sammanfattning

✅ **TDD = Test-Driven Development**
✅ **RED-GREEN-REFACTOR cykeln**
✅ **Skriv test FÖRST**
✅ **Minsta möjliga kod för grönt**
✅ **Refactor med trygghet**
✅ **Repeat! 🔁**

**TDD = Bättre design + Färre buggar + Trygg refactoring**

---

## Läs mer

📚 [Kent Beck: Test-Driven Development by Example](https://www.amazon.com/Test-Driven-Development-Kent-Beck/dp/0321146530)
📚 [Martin Fowler: Is TDD Dead?](https://martinfowler.com/articles/is-tdd-dead/)
📚 [Uncle Bob: The Three Rules of TDD](http://www.butunclebob.com/ArticleS.UncleBob.TheThreeRulesOfTdd)

**Nu kör vi TDD! 🚀**

---

## Nu är det er tur!

**Calculator Kata väntar! 💻**

**Kom ihåg:**

1. 🔴 RED - Skriv test
2. 🟢 GREEN - Minsta kod
3. ♻️ REFACTOR - Förbättra
4. 🔁 Repeat!

**Lycka till! 🎯**

---
Det här är grunden. Öva på den, lek med koden, gör misstag. Det är så du lär dig.
