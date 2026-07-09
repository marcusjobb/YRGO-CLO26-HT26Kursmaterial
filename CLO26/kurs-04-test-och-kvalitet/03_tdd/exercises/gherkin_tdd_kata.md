# Gherkin → TDD Kata - Gift Registry

🟢


## 🎯 Syfte

Träna på att:

1. Skriva Gherkin scenarios
2. Översätta till xUnit-tester
3. Implementera med TDD
4. Skapa UAT-plan

**Tid:** 45-60 minuter

---

## 📖 User Story

```
Som gästlista-ansvarig
Vill jag hantera gästers presentönskemål
Så att ingen köper samma present två gånger
```

---

## ✏️ Steg 1: Skriv Gherkin (15 min)

### Din uppgift

Skriv Gherkin scenarios för följande funktioner:

1. **Lägg till önskemål**

   - Gäst kan lägga till presentönskemål
   - Varje önskemål har namn och pris

2. **Reservera present**

   - Någon kan reservera att köpa en present
   - Reserverad present ska markeras

3. **Visa tillgängliga presenter**

   - Lista alla presenter som inte är reserverade

4. **Kolla budget**
   - Se totalpris för alla presenter man reserverat

### Mall

```gherkin
Feature: Gift Registry
  Som gästlista-ansvarig
  Vill jag hantera presentönskemål
  Så att ingen köper samma present två gånger

Scenario: [Ditt scenario-namn]
  Given [startvillkor]
  When [handling]
  Then [resultat]
```

<details>
<summary>💡 Facit - Klicka för att se exempel</summary>

```gherkin
Feature: Gift Registry
  Som gästlista-ansvarig
  Vill jag hantera presentönskemål
  Så att ingen köper samma present två gånger

Scenario: Lägg till önskemål
  Given en tom gästlista
  When gästen "Anna" lägger till önskemålet "Bok" för 150 kr
  Then ska önskemålet finnas i listan
  And ska vara märkt som ej reserverat

Scenario: Reservera tillgänglig present
  Given gästen "Anna" har önskemålet "Bok" för 150 kr
  And önskemålet är inte reserverat
  When "Bob" reserverar "Bok"
  Then ska önskemålet vara reserverat av "Bob"

Scenario: Kan inte reservera redan reserverad present
  Given gästen "Anna" har önskemålet "Bok" för 150 kr
  And "Bob" har redan reserverat "Bok"
  When "Carl" försöker reservera "Bok"
  Then ska ett felmeddelande visas
  And önskemålet ska fortfarande vara reserverat av "Bob"

Scenario: Visa endast tillgängliga presenter
  Given följande önskemål finns:
    | Gäst  | Present | Pris | Reserverad av |
    | Anna  | Bok     | 150  | -             |
    | Anna  | Spel    | 300  | Bob           |
    | Carl  | Vas     | 200  | -             |
  When man listar tillgängliga presenter
  Then ska "Bok" och "Vas" visas
  And ska "Spel" inte visas

Scenario: Beräkna total budget för reserverade presenter
  Given "Bob" har reserverat:
    | Present | Pris |
    | Bok     | 150  |
    | Spel    | 300  |
  When Bob kollar sin budget
  Then ska totalen vara 450 kr
```

</details>

---

## 🧪 Steg 2: Skriv Tester (20 min)

### Scenario 1: Lägg till önskemål

```csharp
[Fact]
public void GivenEmptyList_WhenAddWish_ThenWishIsAvailable()
{
    // Given - en tom gästlista
    var registry = new GiftRegistry();

    // When - gästen "Anna" lägger till önskemålet "Bok" för 150 kr
    registry.AddWish("Anna", "Bok", 150);

    // Then - ska önskemålet finnas i listan
    var wishes = registry.GetAllWishes();
    Assert.Single(wishes);
    Assert.Equal("Bok", wishes[0].Name);
    Assert.False(wishes[0].IsReserved);
}
```

### Din tur

Skriv tester för:

- Reservera tillgänglig present
- Kan inte reservera redan reserverad
- Visa endast tillgängliga
- Beräkna budget

<details>
<summary>💡 Facit - Test för Scenario 2</summary>

```csharp
[Fact]
public void GivenAvailableWish_WhenReserve_ThenWishIsReserved()
{
    // Given - gästen "Anna" har önskemålet "Bok" för 150 kr
    var registry = new GiftRegistry();
    registry.AddWish("Anna", "Bok", 150);

    // When - "Bob" reserverar "Bok"
    var result = registry.Reserve("Bok", "Bob");

    // Then - ska önskemålet vara reserverat av "Bob"
    Assert.True(result.IsSuccess);
    var wish = registry.GetWish("Bok");
    Assert.True(wish.IsReserved);
    Assert.Equal("Bob", wish.ReservedBy);
}

[Fact]
public void GivenReservedWish_WhenReserveAgain_ThenFails()
{
    // Given - "Bob" har redan reserverat "Bok"
    var registry = new GiftRegistry();
    registry.AddWish("Anna", "Bok", 150);
    registry.Reserve("Bok", "Bob");

    // When - "Carl" försöker reservera "Bok"
    var result = registry.Reserve("Bok", "Carl");

    // Then - ska felmeddelande visas, fortfarande reserverad av Bob
    Assert.False(result.IsSuccess);
    Assert.Contains("already reserved", result.ErrorMessage);

    var wish = registry.GetWish("Bok");
    Assert.Equal("Bob", wish.ReservedBy);
}

[Fact]
public void GivenMixedWishes_WhenGetAvailable_ThenOnlyUnreserved()
{
    // Given - flera önskemål, några reserverade
    var registry = new GiftRegistry();
    registry.AddWish("Anna", "Bok", 150);
    registry.AddWish("Anna", "Spel", 300);
    registry.AddWish("Carl", "Vas", 200);
    registry.Reserve("Spel", "Bob");

    // When - man listar tillgängliga
    var available = registry.GetAvailableWishes();

    // Then - endast ej reserverade visas
    Assert.Equal(2, available.Count);
    Assert.Contains(available, w => w.Name == "Bok");
    Assert.Contains(available, w => w.Name == "Vas");
    Assert.DoesNotContain(available, w => w.Name == "Spel");
}

[Fact]
public void GivenReservedWishes_WhenCalculateBudget_ThenCorrectTotal()
{
    // Given - Bob har reserverat flera
    var registry = new GiftRegistry();
    registry.AddWish("Anna", "Bok", 150);
    registry.AddWish("Anna", "Spel", 300);
    registry.Reserve("Bok", "Bob");
    registry.Reserve("Spel", "Bob");

    // When - Bob kollar sin budget
    var total = registry.GetBudgetFor("Bob");

    // Then - totalen ska vara 450
    Assert.Equal(450, total);
}
```

</details>

---

## 💻 Steg 3: Implementera (20 min)

### Red-Green-Refactor

För varje test:

#### 1. RED ❌

```bash
dotnet test
# Test fails - method doesn't exist
```

#### 2. GREEN ✅

Skriv minsta möjliga implementation:

```csharp
public class GiftRegistry
{
    private List<Wish> _wishes = new();

    public void AddWish(string guest, string name, decimal price)
    {
        _wishes.Add(new Wish
        {
            GuestName = guest,
            Name = name,
            Price = price,
            IsReserved = false
        });
    }

    public List<Wish> GetAllWishes() => _wishes;
}

public class Wish
{
    public string GuestName { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public bool IsReserved { get; set; }
    public string? ReservedBy { get; set; }
}
```

#### 3. REFACTOR 🔄

Förbättra när alla tester är gröna.

<details>
<summary>💡 Facit - Komplett implementation</summary>

```csharp
public class GiftRegistry
{
    private readonly List<Wish> _wishes = new();

    public void AddWish(string guest, string giftName, decimal price)
    {
        var wish = new Wish(guest, giftName, price);
        _wishes.Add(wish);
    }

    public Result Reserve(string giftName, string buyerName)
    {
        var wish = GetWish(giftName);

        if (wish == null)
            return Result.Failure("Gift not found");

        if (wish.IsReserved)
            return Result.Failure($"Gift already reserved by {wish.ReservedBy}");

        wish.Reserve(buyerName);
        return Result.Success();
    }

    public Wish? GetWish(string name)
        => _wishes.FirstOrDefault(w => w.Name == name);

    public List<Wish> GetAllWishes() => _wishes;

    public List<Wish> GetAvailableWishes()
        => _wishes.Where(w => !w.IsReserved).ToList();

    public decimal GetBudgetFor(string buyerName)
        => _wishes
            .Where(w => w.ReservedBy == buyerName)
            .Sum(w => w.Price);
}

public class Wish
{
    public string GuestName { get; }
    public string Name { get; }
    public decimal Price { get; }
    public bool IsReserved { get; private set; }
    public string? ReservedBy { get; private set; }

    public Wish(string guest, string name, decimal price)
    {
        GuestName = guest;
        Name = name;
        Price = price;
        IsReserved = false;
    }

    public void Reserve(string buyer)
    {
        IsReserved = true;
        ReservedBy = buyer;
    }
}

public class Result
{
    public bool IsSuccess { get; }
    public string ErrorMessage { get; }

    private Result(bool success, string error = "")
    {
        IsSuccess = success;
        ErrorMessage = error;
    }

    public static Result Success() => new(true);
    public static Result Failure(string error) => new(false, error);
}
```

</details>

---

## 📋 Steg 4: UAT-plan (10 min) (User Acceptance Testing)

### Skriv UAT-testfall

```markdown
# UAT-plan: Gift Registry

## UAT-001: Lägg till önskemål

Syfte: Verifiera att gäster kan lägga till presentönskemål

Förutsättningar:

- Gift Registry är igång
- Minst en gäst har tillgång

Testperson: Gäst (icke-teknisk)

Teststeg:

1. Öppna Gift Registry
2. Klicka "Lägg till önskemål"
3. Fyll i presentnamn: "Bok"
4. Fyll i pris: "150"
5. Klicka "Spara"

Förväntat resultat:

- Önskemålet syns i listan
- Märkt som "Tillgänglig"
- Pris visas korrekt

Acceptanskriterier:

- [ ] Process tar <30 sekunder
- [ ] Tydlig bekräftelse när sparat
- [ ] Önskemål syns omedelbart

Status: ⬜
```

### Din tur

Skriv UAT för:

- Reservera present
- Visa tillgängliga presenter
- Kolla budget

---

## ✅ Definition of Done

- [ ] Alla 5 Gherkin-scenarios skrivna
- [ ] Minst 5 xUnit-tester implementerade
- [ ] Alla tester gröna
- [ ] Code coverage >80%
- [ ] 3 UAT-testfall dokumenterade
- [ ] Kod är refactored

---

## 🎯 Extrautmaningar (VG)

### 1. Avboka reservation

```gherkin
Scenario: Avboka reservation
  Given "Bob" har reserverat "Bok"
  When "Bob" avbokar "Bok"
  Then ska "Bok" vara tillgänglig igen
```

### 2. Budget-limit

```gherkin
Scenario: Varna om budget överskrids
  Given "Bob" har en budgetgräns på 500 kr
  And har redan reserverat presenter för 400 kr
  When "Bob" försöker reservera present för 150 kr
  Then ska en varning visas
  And totalen skulle bli 550 kr
```

### 3. Prioritering

```gherkin
Scenario: Markera önskemål som viktigt
  Given "Anna" har önskemålet "Bok"
  When "Anna" markerar "Bok" som viktigt
  Then ska "Bok" visas först i listan
```

---

## 💡 Reflektionsfrågor

Efter övningen, diskutera:

1. **Hur hjälpte Gherkin?**

   - Var det lättare att förstå kraven?
   - Blev testerna tydligare?

2. **TDD-processen**

   - Hur kändes det att skriva test först?
   - Förändrades designen jämfört med att koda först?

3. **UAT-perspektivet**
   - Vad skiljer UAT från unit test?
   - Vilka problem hittar UAT som unit test missar?

---

## 🔗 Nästa Steg

Nu är ni redo för **ert riktiga projekt**!

Applicera samma process:

1. Ta en User Story från er backlog
2. Skriv Gherkin scenarios
3. TDD-implementering
4. UAT-plan

Se `wednesday_sprint_simulation.md` för full guide.

---

_© Campus Mölndal 2025 • From Story to Code in 60 Minutes_

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
