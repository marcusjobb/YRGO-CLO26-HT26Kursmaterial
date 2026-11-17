---
marp: true
theme: nion-dark
paginate: true
---

# Vanliga misstag med designmönster

**Det som ser rätt ut men inte är det**

Mönster är verktyg. Det är inte en poäng i sig att använda dem.

---

## Mönster löser ett problem

- Varje mönster finns för att lösa ett problem.
- Saknas problemet blir mönstret bara extra kod.
- Fråga först: *vad gör den här koden svår att ändra?*
- Sedan: *vilket mönster löser just det?* Flödesschemat i `notes/` hjälper till.

---

## 1. Builder: metoderna returnerar inte `this`

```csharp
public void Named(string name)      // void: ingen this
{
    this.name = name;
}

Character c = new CharacterBuilder().Named("Mira").IsEvil().Build();   // Fel
```

Kedjan bryts direkt, eftersom `void` inte lämnar tillbaka något att anropa nästa metod på.

**Rätt:** varje steg returnerar `CharacterBuilder` och slutar med `return this;`.

---

## 2. Builder: samma lista i två objekt

```csharp
var builder = new PizzaBuilder().WithTopping("skinka");
Pizza först = builder.Build();

builder.WithTopping("ost");
Pizza andra = builder.Build();

// först innehåller nu också "ost"
```

`Build()` gav `Pizza` en referens till samma lista som builden använder.

**Rätt:** kopiera listan i `Build()`, till exempel med `new List<string>(toppings)`.

---

## 3. Builder för två parametrar

```csharp
// Onödigt
Person p = new PersonBuilder().Named("Mira").Aged(30).Build();

// Räcker gott
Person p = new Person("Mira", 30);
```

Builder lönar sig när det finns många valfria värden, eller när en konstruktor blir svår att läsa. Två parametrar är inte ett problem.

---

## 4. Factory: `new` kvar överallt

```csharp
// Fabriken finns, men...
Monster m = new Goblin();          // ...den här raden skapar fortfarande ett monster direkt
```

Om alla `new` ligger i koden utanför fabriken, har fabriken ingen uppgift. Då finns den bara för att mönstret finns.

**Rätt:** all kod som väljer typ går via fabriken. Då ändras ett ställe när en ny typ kommer.

---

## 5. Factory: okänd typ ger `null`

```csharp
public static Monster Create(string type) => type switch
{
    "goblin" => new Goblin(),
    "troll" => new Troll(),
    _ => null!                      // Fel: felet dyker upp långt senare
};
```

Nästa rad som använder monstret kraschar med `NullReferenceException`, och felet syns inte där det uppstod.

**Rätt:** `_ => throw new ArgumentException(...)`, som i övningen.

---

## 6. Factory och Builder blandas ihop

| | Factory | Builder |
|---|---|---|
| Fråga | *Vilken* typ ska skapas? | *Hur* ska objektet sättas ihop? |
| Exempel | Pizzerians kök väljer rätt pizza | Du väljer tilltugg själv |

Factory väljer. Builder bygger steg för steg. Ibland används de tillsammans, men de löser olika problem.

---

## 7. Abstract Factory: blandade familjer

```csharp
Knapp knapp = new LjusKnapp();
Textfalt falt = new MorktTextfalt();   // Fel: ljus knapp, mörkt fält
```

Det är precis det problem Abstract Factory ska förhindra. Den som väljer tema väljer hela familjen på en gång.

**Rätt:** en `ITemaFabrik`. Kod som använder den vet inte vilken variant den får.

---

## 8. Strategy: valet kvar i den som använder strategin

```csharp
class Hero
{
    public string Attack()
    {
        if (Weapon is SwordAttack) ...    // Fel: Hero känner till vapnen
        else if (Weapon is BowAttack) ...
    }
}
```

Då har du både strategi och if-kedja. Strategin gör ingenting för dig, eftersom `Hero` ändå måste ändras när ett vapen tillkommer.

**Rätt:** `Hero` anropar bara `Weapon.Attack(Name)`. Valet av strategi görs utanför, till exempel i en fabrik.

---

## 9. Strategy med bara en implementation

```csharp
interface IRouteStrategy { string Describe(double km); }
class WalkStrategy : IRouteStrategy { ... }   // Den enda som någonsin finns
```

Ett interface med en implementation är ofta onödigt. Lägg till strategin när det finns en andra variant, eller när valet ska kunna bytas under körning.

---

## 10. Observer: glömd prenumeration

```csharp
Hero mira = new Hero("Mira", 100);
mira.Subscribe(new SoundPlayer());   // Fortsätter att lyssna när den inte behövs mer
```

Lyssnaren hålls kvar av objektet som berättar. Den kan aldrig städas bort, och den fortsätter ta emot händelser.

**Rätt:** ta bort lyssnaren med `Unsubscribe` när den inte ska lyssna längre.

---

## 11. Observer: ändra listan under `foreach`

```csharp
foreach (IHealthObserver o in observers)
{
    if (o.ShouldStop) observers.Remove(o);   // Fel: kraschar med InvalidOperationException
}
```

Du kan inte ändra en lista medan du går igenom den.

**Rätt:** gå baklänges med en `for`-loop, eller gör en kopia att gå igenom: `foreach (var o in observers.ToList())`.

---

## 12. Event: glöm `?.` när ingen lyssnar

```csharp
public event EventHandler<BankEventArgs>? Deposited;

Deposited(this, new BankEventArgs(...));    // Fel: null om ingen prenumererar
```

Ett event utan prenumeranter är `null`. Anropet kraschar med `NullReferenceException`, även om det inte finns något fel i logiken.

**Rätt:** `Deposited?.Invoke(this, new BankEventArgs(...));`

---

## 13. Blanda ihop de strukturella mönstren

| Mönster | Gör | Exempel |
|---|---|---|
| **Adapter** | Översätter mellan två interface som inte passar ihop | Gammalt betalningssystem med XML |
| **Facade** | Förenklar ett system med många delar | `OrderFacade.LäggOrder()` |
| **Decorator** | Lägger till beteende utanpå ett objekt | Loggning runt en `OrderService` |

Adapter ändrar gränssnittet. Facade döljer delarna. Decorator lägger på något nytt utan att ändra originalet.

---

## Checklista innan du använder ett mönster

1. Vilket problem löser det här?
2. Skulle koden vara enklare utan mönstret?
3. Kan jag förklara varför det passar, i en mening?
4. Går det att ta bort mönstret utan att något annat går sönder?

Om svaret på 2 är *ja*, låt bli.

---

## Sammanfattning

- Mönster är svar på ett problem, inte mål i sig.
- Varje mönster har ett eget sätt att gå fel på. Känn igen dem.
- Flödesschemat och exemplen i `examples/` visar hur det ska se ut.
