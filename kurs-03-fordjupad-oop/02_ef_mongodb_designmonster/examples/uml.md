# UML för skojs skull

Alla mönster i ett diagram per mönster, och sist ett sekvensdiagram över bankens event. Diagrammen är bara ritade för att se strukturen. Bra att jämföra med `UML.md`-filerna i varje projekt i `source/repos`.

---

## Builder

```mermaid
classDiagram
    class CharacterBuilder {
        -string name
        -CharacterKind kind
        -bool isEvil
        +Named(name) CharacterBuilder
        +IsA(kind) CharacterBuilder
        +IsEvil() CharacterBuilder
        +Speaks(speech) CharacterBuilder
        +Likes(likes) CharacterBuilder
        +WeaponOfChoice(weapon) CharacterBuilder
        +Titled(title) CharacterBuilder
        +Build() Character
    }
    class Character {
        +string Name
        +CharacterKind Kind
        +bool IsEvil
        +ToString() string
    }
    CharacterBuilder ..> Character : skapar
```

---

## Factory

```mermaid
classDiagram
    class MonsterFactory {
        <<static>>
        +Create(type) Monster
    }
    class Monster {
        <<abstract>>
        +string Name
        +int Health
        +Roar() string*
    }
    class Goblin
    class Troll
    class Skeleton
    Monster <|-- Goblin
    Monster <|-- Troll
    Monster <|-- Skeleton
    MonsterFactory ..> Monster : skapar
```

---

## Strategy (vapen)

```mermaid
classDiagram
    class IAttackStrategy {
        <<interface>>
        +Attack(attackerName) string
    }
    class Hero {
        +string Name
        +IAttackStrategy Weapon
        +Attack() string
    }
    class SwordAttack
    class BowAttack
    class FireballAttack
    class IceAttack
    IAttackStrategy <|.. SwordAttack
    IAttackStrategy <|.. BowAttack
    IAttackStrategy <|.. FireballAttack
    IAttackStrategy <|.. IceAttack
    Hero o--> IAttackStrategy : Weapon
```

---

## Strategy (resa)

```mermaid
classDiagram
    class IRouteStrategy {
        <<interface>>
        +Describe(km) string
    }
    class TripPlanner {
        +IRouteStrategy Strategy
        +Plan(km) string
    }
    class WalkStrategy
    class BikeStrategy
    class CarStrategy
    IRouteStrategy <|.. WalkStrategy
    IRouteStrategy <|.. BikeStrategy
    IRouteStrategy <|.. CarStrategy
    TripPlanner o--> IRouteStrategy : Strategy
```

---

## Observer (Mira)

```mermaid
classDiagram
    class IHealthObserver {
        <<interface>>
        +OnHealthChanged(heroName, health)
    }
    class Hero {
        +string Name
        +int Health
        +Subscribe(observer)
        +TakeDamage(amount)
    }
    class HealthBar
    class SoundPlayer
    IHealthObserver <|.. HealthBar
    IHealthObserver <|.. SoundPlayer
    Hero o--> "*" IHealthObserver : observers
```

---

## Observer med event (bankkonto)

```mermaid
classDiagram
    class BankAccount {
        +string Name
        +double Balance
        +Deposited
        +Withdrawn
        +WithdrawFailed
        +Deposit(amount)
        +Withdraw(amount)
    }
    class BankEventArgs {
        +string Account
        +double Amount
        +double Balance
        +string? Reason
    }
    class BankLogger {
        +OnDeposited(sender, e)
        +OnWithdrawn(sender, e)
        +OnWithdrawFailed(sender, e)
    }
    BankAccount ..> BankEventArgs : skickar
    BankLogger ..> BankEventArgs : tar emot
    BankAccount --> BankLogger : prenumereras av
```

---

## Sekvens: ett uttag som misslyckas

Det här är den enda händelsen i kedjan som ändras över tid. Ordningen är viktig.

```mermaid
sequenceDiagram
    participant Program
    participant Konto as BankAccount
    participant Logger as BankLogger

    Program->>Konto: Withdraw(200)
    Konto->>Konto: kontrollerar beloppet
    Note right of Konto: Ja, saldot är 70
    Konto-->>Logger: WithdrawFailed(Mira, 200, 70, "Otillräckligt saldo")
    Logger->>Logger: Log(...)
    Konto-->>Program: returnerar (saldot ändras inte)
```
