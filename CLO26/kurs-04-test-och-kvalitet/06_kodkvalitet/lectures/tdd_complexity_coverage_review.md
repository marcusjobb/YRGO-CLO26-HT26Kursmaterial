# Snabbrepetition

🟢


## TDD Tänkandet, Code Complexity & Code Coverage

---

# TDD Tänkandet - Del 1

## Red-Green-Refactor Cykeln

**🔴 RED** - Skriv ett test som fallerar

- Testet beskriver önskat beteende
- Koden finns inte än
- Testet SKA fallera

---

# TDD Tänkandet - Del 1

## Red-Green-Refactor Cykeln

**🟢 GREEN** - Skriv minsta möjliga kod för att testet går igenom

- Fokus på att få testet grönt
- Inte på "perfekt" kod

---

# TDD Tänkandet - Del 1

## Red-Green-Refactor Cykeln

**🔵 REFACTOR** - Förbättra koden utan att ändra beteende

- Testerna garanterar att funktionalitet bevaras

---

# TDD Tänkandet - Del 2

## Varför TDD?

✅ **Mindre buggar** - Problem upptäcks tidigt
✅ **Bättre design** - Testbar kod = välstrukturerad kod
✅ **Levande dokumentation** - Tester visar hur kod används
✅ **Trygghet vid ändringar** - Refaktorera utan rädsla
✅ **Fokus** - En sak i taget

---

# TDD Tänkandet - Del 3

## TDD Mindset

**Test First** - Tänk på vad koden ska göra INNAN du skriver den

**Baby Steps** - Små steg, ofta

- Ett test i taget
- Enklaste implementation först
- Bygg upp komplexitet gradvis

**Fail Fast** - Låt tester fallera snabbt

- Tydliga felmeddelanden
- Ett assertion per test (idealt)

---

# Code Complexity - Del 1

## Vad är Komplexitet?

**Cyclomatic Complexity** - Antal oberoende vägar genom koden

```csharp
// Complexity = 1 (enkel)
public int Add(int a, int b) => a + b;

// Complexity = 3 (mer komplex)
public string Grade(int score) {
    if (score >= 90) return "A";      // väg 1
    if (score >= 80) return "B";      // väg 2
    return "F";                        // väg 3
}
```

**Räkna**: 1 + antal beslutspunkter (if, while, case, &&, ||)

---

# Code Complexity - Del 2

**Hög komplexitet = Problem**

⚠️ Svårt att förstå
⚠️ Svårt att testa
⚠️ Mer buggar
⚠️ Dyr underhåll

**Gränsvärden**

- 1-10: Enkel, bra
- 11-20: Måttlig, ok
- 21-50: Komplex, överväg refaktorering
- 50+: Mycket komplex, MÅSTE refaktoreras

---

# Code Complexity - Del 3

## Sänk Komplexiteten

**Strategier**

1. **Extract Method** - Bryt ut logik till separata metoder
2. **Early Return** - Returnera tidigt istället för nästlade if
3. **Strategy Pattern** - Byt komplex if/switch mot polymorfism
4. **Guard Clauses** - Hantera edge cases först

---

# Code Complexity - Del 3

## Sänk Komplexiteten

**Strategier**

```csharp
// Före: Complexity = 5
public void Process(User user) {
    if (user != null) {
        if (user.IsActive) {
            // kod...
        }
    }
}
```

---

# Code Complexity - Del 3

## Sänk Komplexiteten

**Strategier**

```csharp
// Efter: Complexity = 2
public void Process(User user) {
if (user == null) return;
if (!user.IsActive) return;
// kod...
}
```

---

# Code Coverage - Del 1

## Vad är Code Coverage?

**Procentandel av koden som exekveras av tester**

**Typer av Coverage**

- **Line Coverage** - Andel kodrader som körs
- **Branch Coverage** - Andel beslutspunkter (if/else) som testas
- **Method Coverage** - Andel metoder som anropas
- **Statement Coverage** - Andel statements som körs

```bash
dotnet test --collect:"XPlat Code Coverage"
```

---

# Code Coverage - Del 2

## Coverage Mål

**Är 100% coverage bra?**

❌ **NEJ** - 100% är inte alltid målet!

✅ **70-80%** - Bra för de flesta projekt
✅ **80-90%** - Mycket bra, kritiska system
✅ **90%+** - Medicin, finans, säkerhetskritiskt

---

# Code Coverage - Del 2

## Coverage Mål

**Viktigare än procent:**

- Täck kritisk business logic
- Täck komplexa algoritmer
- Täck edge cases
- Täck error handling

---

# Code Coverage - Del 3

## Coverage Fällor

**Hög coverage ≠ Bra tester**

```csharp
// 100% coverage men värdelöst test!
[Fact]
public void TestCalculate() {
    var calc = new Calculator();
    calc.Add(2, 2); // Inget assertion!
    // Test passerar men verifierar inget
}
```

---

# Code Coverage - Del 2

## Coverage Mål

**Fokusera på:**
✅ Meningsfulla assertions
✅ Edge cases och gränsvärden
✅ Error scenarios
✅ Business logic

**Coverage är ett verktyg, inte ett mål!**

---

# Sammanfattning

**TDD** - Red, Green, Refactor. Test först, kod sen.

**Complexity** - Håll det enkelt (<10). Extract, refactor.

**Coverage** - 70-80% räcker ofta. Kvalitet > kvantitet.

**Alla tre tillsammans** = Hållbar, kvalitetskod

---

<center>

# 💻 Koda vilt nu! 😁

</center>

---
Det här är grunden. Öva på den, lek med koden, gör misstag. Det är så du lär dig.
