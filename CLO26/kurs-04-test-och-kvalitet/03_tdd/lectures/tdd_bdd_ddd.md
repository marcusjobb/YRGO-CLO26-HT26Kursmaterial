---

title: tdd_bdd_ddd
author: Marcus Ackre Medina
type: lecture
topic: testing
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/2025/csharp/4_test/lectures/03_tdd/tdd_bdd_ddd.md"
description: "Att navigera bland utvecklingsmetoder som TDD (Test-Driven Development), DDD (Domain-Driven Design) och BDD (Behavior-Driven Development) kan kännas som att försöka hitta rätt väg i en labyrint. Men o"
tags: ["bdd", "csharp", "ddd", "rider", "tdd", "testing", "visual-studio"]
week_fit: []
---

### **Skillnaden mellan TDD, DDD och BDD – och hur de kompletterar varandra**

Att navigera bland utvecklingsmetoder som TDD (Test-Driven Development), DDD (Domain-Driven Design) och BDD (Behavior-Driven Development) kan kännas som att försöka hitta rätt väg i en labyrint. Men oroa dig inte, vi ska reda ut detta på ett enkelt och strukturerat sätt. Tänk på TDD som att bygga en bil, DDD som att designa trafiksystemet, och BDD som att ta reda på exakt hur våra passagerare vill resa. Låt oss bryta ner det!

_ärligt snott från TjatGPT_

---

### **Vad är TDD?**

#### **Test-Driven Development**

**TDD** handlar om att skriva tester innan man skriver själva koden. Fokus ligger på att skapa små, buggfria komponenter genom att arbeta i korta cykler:

1. **Skriv ett test först** – Ett enkelt test beskriver vad en specifik funktion ska göra (t.ex. ”en bil ska starta när jag vrider om nyckeln”).
2. **Kör testet** – Det ska misslyckas, eftersom funktionaliteten inte finns än.
3. **Skriv den enklaste koden** som får testet att passera.
4. **Refaktorisera koden** – Städa upp, optimera, och gör den mer lättförståelig.
5. **Repetera** för nästa funktion.

**Användningsområde:** Perfekt för att bygga robust, testbar kod, en metod eller klass i taget.

**Metafor:** TDD är som att bygga varje del av en bil – motorn, bromsarna – och testa varje komponent för sig innan allt monteras.

---

### **Vad är BDD?**

#### **Behavior-Driven Development**

**BDD** bygger vidare på TDD och lägger till ett lager av samarbete mellan teamet och intressenterna genom att fokusera på hur systemet ska bete sig. Det handlar om att skriva tester i ett lättförståeligt format som alla kan delta i.

1. **Använd scenarios** skrivna i Gherkin-syntax, t.ex.:

   ```gherkin
   Feature: Starta bilen
   Scenario: Starta motorn
     Given bilen är avstängd
     When jag vrider om nyckeln
     Then ska motorn starta
   ```

2. **Definiera beteenden** som är lätta att testa och kommunicera med andra.
3. **Utveckla kod som uppfyller dessa beteenden**.

**Användningsområde:** Perfekt för att bygga broar mellan affärsfolk och utvecklare, samt för att skapa tester som beskriver systemets beteende.

**Metafor:** BDD är som att fråga passagerarna hur de vill resa och designa bilturen baserat på deras svar.

---

### **Vad är DDD?**

#### **Domain-Driven Design**

**DDD** fokuserar på att förstå och modellera systemet baserat på affärsdomänen det ska lösa problem för. Det handlar inte bara om kod, utan om att skapa en struktur där systemet reflekterar verklighetens behov och begrepp.

1. **Jobba med domänexperter** – Identifiera viktiga koncept och regler från affärsområdet.
2. **Skapa ett gemensamt språk** („ubiquitous language”) – Ett gemensamt vokabulär för utvecklare och affärsparter.
3. **Använd Bounded Contexts** – Dela upp systemet i mindre delar som är lättare att förstå och hantera.
4. **Bygg kring domänmodellen** – Håll affärslogik och kod så samstämda som möjligt.

**Användningsområde:** Använd DDD för komplexa affärsproblem där det är kritiskt att koden speglar den verkliga verksamheten.

**Metafor:** DDD är som att designa hela trafiksystemet – vägar, skyltar, och regler – så att allt fungerar smidigt och effektivt.

---

### **Skillnader och likheter**

| **Fokus**         | **TDD**                       | **DDD**                           | **BDD**                                     |
| ----------------- | ----------------------------- | --------------------------------- | ------------------------------------------- |
| **Vad styrs av?** | Kodens funktionalitet.        | Affärslogik och domänbegrepp.     | Systemets beteende ur användarens synpunkt. |
| **Primärt mål**   | Bygga testbar, korrekt kod.   | Modellera ett relevant system.    | Kommunicera krav och beteenden.             |
| **Vem deltar?**   | Utvecklare.                   | Utvecklare och domänexperter.     | Utvecklare, domänexperter och intressenter. |
| **Uttrycksform**  | Kodtest (t.ex., JUnit/NUnit). | Domänmodeller och språk.          | Scenarios (t.ex., Gherkin).                 |
| **Detaljnivå**    | Mikro (klasser, metoder).     | Makro (system och affärsdomäner). | Från mikro till makro.                      |

---

### **Hur de kompletterar varandra**

De tre metoderna är inte konkurrenter, utan snarare bitar i ett pussel:

1. **Börja med DDD** för att förstå domänen och modellera systemet korrekt.
2. Använd **BDD** för att beskriva systemets beteenden ur ett användarperspektiv.
3. Implementera detaljerna med **TDD**, så att varje del av koden är robust och vältestad.

Viktigt att förstå att ingen del ersätter en annan, de kompletterar varandra för att skapa en solid grund för att bygga mjukvara. Genom att använda alla tre metoder kan du skapa kod som är korrekt, testbar och lätt att förstå för alla inblandade. Det är dock viktigt att ha balans i hur mycket tid och resurser du lägger på varje metod, beroende på projektets behov och komplexitet.

Man skulle kunna skriva tusentals tester, men det är bättre att skriva de rätta testerna. Och det är där DDD och BDD kommer in i bilden – de hjälper dig att fokusera på rätt saker och skapa en gemensam förståelse för vad systemet ska göra och hur det ska bete sig. Man kan aldrig testa ALLT, så välj testerna klokt och använd metoder som hjälper dig att prioritera rätt.

---

### **Praktiskt exempel: En webshop**

#### **DDD:**

- Förstå att webshoppen har viktiga domänbegrepp som "Order", "Kund", och "Produkter".
- Modellera regler, som att "En order över 500 kr ska ge fri frakt".

#### **BDD:**

- Beskriv beteendet ur ett användarperspektiv med scenarios:

  ```gherkin
  Feature: Order och frakt
  Scenario: Fri frakt vid hög orderbelopp
    Given en kund har lagt produkter i varukorgen
    And totalbeloppet är 600 kr
    When kunden går till kassan
    Then ska fraktkostnaden vara 0 kr
  ```

- Detta scenario är enkelt för affärsfolk att förstå och för utvecklare att implementera.

#### **TDD:**

- Bygg och testa specifika funktioner baserat på scenariot. Exempel på ett test i C#:

  ```csharp
  [Test]
  public void FreeShipping_WhenOrderExceeds500()
  {
      // Arrange
      var order = new Order();
      order.AddProduct(new Product("Laptop", 600));

      // Act
      var shippingCost = order.CalculateShipping();

      // Assert
      Assert.AreEqual(0, shippingCost);
  }
  ```

- Detta test fokuserar på en enda regel: fri frakt vid orderbelopp över 500 kr. Om testet passerar kan du gå vidare till nästa funktion.

---

### **Sammanfattning**

I detta exempel:

1. **DDD** hjälper oss att modellera webshoppen baserat på affärsregler och domänbegrepp.
2. **BDD** ser till att vi beskriver beteenden som är viktiga för kunder och intressenter.
3. **TDD** implementerar och säkerställer att varje regel fungerar korrekt.

Genom att kombinera dessa metoder bygger vi ett robust system som är både välmodellat och noggrant testat.

---
Det här är grunden. Öva på den, lek med koden, gör misstag. Det är så du lär dig.
