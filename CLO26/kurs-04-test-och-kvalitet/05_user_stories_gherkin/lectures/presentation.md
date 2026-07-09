<!-- _class: lead -->

# BDD & Gherkin

🟢


---

## Given-When-Then

**Test och Kvalitetssäkring**
Campus Mölndal • 2025-12-17

---

## Vad är BDD?

**Behavior-Driven Development**
Vi beskriver systemets **beteende** INNAN vi kodar

```
❌ Traditionellt:
"Skriv en funktion som validerar email"

✅ BDD:
"GIVEN en användare, WHEN de anger en giltig email,
 THEN accepteras den"
```

---

## Gherkin-syntax

**Given-When-Then** = Språk för att beskriva beteende

```gherkin
Scenario: Användare kan logga in med giltiga uppgifter
  Given en registrerad användare med email "test@example.com"
  And lösenordet är "Secret123"
  When användaren försöker logga in
  Then ska inloggningen lyckas
  And användaren ska omdirigeras till startsidan
```

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.

---

## Struktur

```gherkin
Feature: Vad bygger vi?

Scenario: Ett specifikt användningsfall
  Given startvillkor (Arrange)
  When handling (Act)
  Then förväntat resultat (Assert)
```

**Känner igen det?** → AAA-mönstret!

---

## Exempel: Shopping Cart

```gherkin
Feature: Shopping Cart Discount

Scenario: Rabattkod ger 10% rabatt
  Given en kundvagn med totalpris 1000 kr
  When användaren anger rabattkoden "SAVE10"
  Then ska totalpriset bli 900 kr
  And rabattkoden ska markeras som använd
```

---

## Exempel: Felflöde

```gherkin
Scenario: Ogiltig rabattkod ger inget avdrag
  Given en kundvagn med totalpris 1000 kr
  When användaren anger rabattkoden "INVALID"
  Then ska totalpriset fortfarande vara 1000 kr
  And ett felmeddelande ska visas
```

---

## Från Gherkin till Test

**Utan SpecFlow** → Gherkin är dokumentation

```csharp
[Fact]
public void GivenCartWith1000_WhenApplySAVE10_ThenPriceIs900()
{
    // Given - en kundvagn med totalpris 1000 kr
    var cart = new ShoppingCart();
    cart.AddItem(new Item("Product", 1000));

    // When - användaren anger rabattkoden "SAVE10"
    var result = cart.ApplyDiscount("SAVE10");

    // Then - ska totalpriset bli 900 kr
    Assert.Equal(900, cart.TotalPrice);
    Assert.True(result.IsSuccess);
}
```

---

## Fördelar med Gherkin

✅ **Tydlig kommunikation** - alla förstår
✅ **Testbar specifikation** - blir naturligt till tester
✅ **Dokumentation** - beskriver faktiskt beteende
✅ **Non-tech kan läsa** - Product Owner förstår
✅ **Acceptance Criteria** - Definition of Done

---

## BDD-flöde idag

1. Skriv **User Story** med acceptance criteria i Gherkin
2. Diskutera **scenarion** i teamet
3. Skriv **test** baserat på Given-When-Then
4. **Implementera** med TDD (Red-Green-Refactor)
5. **Verifiera** att alla scenarios är gröna

---

## Tips för bra Gherkin

✅ **Fokusera på beteende**, inte implementation
✅ **Använd domänspråk** (business language)
✅ **Håll scenarios korta** och fokuserade
✅ **Ett scenario testar EN sak**

❌ Inte: "Klicka på blå knappen i övre vänstra hörnet"
✅ Istället: "Användaren sparar formuläret"

---

<!-- _class: lead -->

# Live Demo

---

## Skriv Gherkin → Test → Implementation

---

## Dagens Mål

Alla scenarios från er User Story ska vara:

- ✅ Dokumenterade i Gherkin
- ✅ Implementerade som tester
- ✅ Gröna

---

## Resurser

📖 **Must-Read:**
- [Cucumber: Gherkin Reference](https://cucumber.io/docs/gherkin/reference/)

🎯 **Övning:**
- `exercises/gherkin_tdd_kata.md`
- `exercises/wednesday_sprint_simulation.md`

---

<!-- _class: lead -->

# Frågor?

_BDD Without the Boilerplate_

---
Och kom ihåg: allt vi gått igenom här är grunden. Resten bygger på det. Så var inte rädd att experimentera.
