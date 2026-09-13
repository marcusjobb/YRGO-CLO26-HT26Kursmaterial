# Tentafrågor — Klasser och OOP

Öva inför tentan. Varje fråga har fyra alternativ — ett rätt, ett lite roligt, och två som verkar rimliga men inte stämmer.

---

## Fråga 1 — Klass vs objekt

Vad är skillnaden mellan en klass och ett objekt?

A) En klass är ett objekt som har skapats med `new`
B) En klass är en mall som beskriver hur något ska se ut — ett objekt är en konkret instans av den mallen
C) Klasser används bara i Java, inte i C#
D) Det är samma sak, men klasser har ett fint namn 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**A)** Tvärtom — ett objekt skapas med `new`, inte klassen.
**B)** Rätt. Klassen `BankAccount` är ritningen; `new BankAccount("Alex", 1000)` är objektet.
**C)** C# använder klasser precis som Java — det är en grundsten i OOP.
**D)** De är definitivt inte samma sak, men det är ett tappert försök.

</details>

---

## Fråga 2 — Konstruktorn

Vad är en konstruktor?

A) En metod som anropas automatiskt när ett objekt skapas
B) En metod som förstör ett objekt när programmet stängs av
C) En privat variabel som lagrar objektets startläge
D) En person som bygger hus men råkade hamna i fel kurs 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt. I `BankAccount` körs konstruktorn direkt vid `new BankAccount("Alex", 1000)` och sätter `Ägare`, `Saldo` och `ÄrAktivt`.
**B)** Det beskriver en destruktor — något annat, och i C# sköts minnesfrigöring av garbage collectorn.
**C)** Det är en beskrivning av ett fält, inte en konstruktor.
**D)** Fel kurs, rätt energi.

</details>

---

## Fråga 3 — Privata fält

Varför brukar fält i en klass vara privata?

A) För att kompilatorn kräver det — `public` fält ger kompileringsfel
B) Så att koden körs snabbare
C) För att skydda objektets interna tillstånd och styra hur det ändras — via metoder eller properties
D) Ingen vet riktigt varför, men alla gör det ändå 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: C**

**A)** Fel — `public` fält är tillåtet i C#, men det är dålig praxis.
**B)** Privata fält påverkar inte prestanda.
**C)** Rätt. Inkapsling innebär att klassen kontrollerar sina egna data. Ingen utifrån kan råka sätta `Saldo = -9999` direkt.
**D)** Det finns faktiskt en bra anledning — och nu vet du den.

</details>

---

## Fråga 4 — Property med private set

Vad betyder `{ get; private set; }` på en property?

A) Propertyn kan läsas och ändras var som helst i programmet
B) Propertyn kan läsas utifrån klassen, men bara ändras inifrån klassen
C) Propertyn är helt privat och kan inte läsas utifrån
D) Det är ett stavfel — rätt syntax är `{ get; set; private; }` 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**A)** Det är `{ get; set; }` utan `private` som tillåter ändringar utifrån.
**B)** Rätt. `public double Saldo { get; private set; }` gör att vem som helst kan läsa `konto.Saldo`, men bara metoder inuti `BankAccount` kan ändra värdet.
**C)** `private` gäller bara `set` — `get` är fortfarande `public`.
**D)** Det är inte ett stavfel, men kompilatorn skulle inte hålla med om det alternativet.

</details>

---

## Fråga 5 — Nyckelordet new

Vad händer när man skriver `BankAccount konto = new BankAccount("Alex", 1000);`?

A) Klassen `BankAccount` laddas om från disken
B) Ett nytt objekt av typen `BankAccount` skapas i minnet och konstruktorn körs med argumenten `"Alex"` och `1000`
C) Variabeln `konto` blir en kopia av klassen `BankAccount`
D) Datorn frågar banken om tillstånd att öppna kontot 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**A)** Klasser laddas inte om — det är inte hur C# fungerar.
**B)** Rätt. `new` allokerar minne för objektet, och konstruktorn körs direkt med de givna argumenten.
**C)** `konto` är en referens till objektet, inte en kopia av klassen.
**D)** Banken svarar inte i tid — det skulle ge en timeout.

</details>

---

## Fråga 6 — Inkapsling

Vad innebär inkapsling (encapsulation)?

A) Att alla klasser i ett program är lagrade i samma fil
B) Att ett objekt döljer sin interna data och bara exponerar det som är nödvändigt utåt
C) Att klassen ärver egenskaper från en annan klass
D) Att koden är så välskriven att den kan stängas in i en kapsel och skickas till Mars 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**A)** Det har ingenting med inkapsling att göra — det är bara dålig filstruktur.
**B)** Rätt. `BankAccount` exponerar `Saldo` som läsbart via `get`, men skyddar det från direkta ändringar via `private set`. Uttag sker via `TaUt()` som också validerar beloppet.
**C)** Det är arv — ett annat OOP-koncept.
**D)** Koden är bra, men rymdfärden kräver nog lite mer än inkapsling.

</details>

---

## Fråga 7 — Arv

Vad innebär arv i objektorienterad programmering?

A) Att ett objekt kopierar ett annat objekts värden när det skapas
B) Att en klass tar över egenskaper och beteenden från en annan klass och kan lägga till eller specialisera dem
C) Att alla klasser i C# automatiskt delar samma konstruktor
D) Att föräldrarna i familjen `Animal` äntligen får erkännande 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**A)** Det beskriver kopiering av värden — inte arv. Arv handlar om klassrelationer, inte objektvärden.
**B)** Rätt. `class Hund : Djur` ärver allt `Djur` har — namn, ålder, `Andas()` — och kan lägga till `Skäll()` eller specialisera `Presentera()`.
**C)** Klasser ärver inte konstruktorer automatiskt; du måste anropa basklassens konstruktor explicit med `base(...)`.
**D)** Rätt erkänt, fel svar.

</details>

---

## Fråga 8 — Nyckelordet för arv i C#

Hur anger man att klassen `Goblin` ärver från klassen `Monster` i C#?

A) `class Goblin extends Monster`
B) `class Goblin inherits Monster`
C) `class Goblin : Monster`
D) `class Goblin(Monster)` — det är Python-syntax men det ser ändå fräckt ut 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: C**

**A)** `extends` är Java-syntax. C# använder kolon.
**B)** `inherits` är Visual Basic. C# använder kolon.
**C)** Rätt. `: Monster` efter klassnamnet anger basklassen. `class Goblin : Monster { }` är korrekt C#-syntax.
**D)** Python-korrekt, C#-fel — men nästan poäng för stil.

</details>

---

## Fråga 9 — override

Vad gör `override` på en metod i en subklass?

A) Det gör att metoden körs dubbelt snabbt
B) Det ersätter basklassens implementation av metoden med subklassens egna version
C) Det låser metoden så att inga fler subklasser kan ändra den
D) Det tvingar alla programmers att jobba övertid 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**A)** `override` påverkar inte prestanda — det handlar om beteende.
**B)** Rätt. Om `Monster` har `virtual string Presentera()`, kan `Goblin` skriva `override string Presentera()` och ge en helt annan implementation — utan att bryta mot basklassens kontrakt.
**C)** Det är `sealed` som låser ytterligare överskrivning.
**D)** Det är inget vi kan bekräfta.

</details>

---
