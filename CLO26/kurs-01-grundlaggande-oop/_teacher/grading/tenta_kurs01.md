# Tenta — Grundläggande OOP i C#

*Lärarversion med svar. Dela ALDRIG denna fil.*

**Datum:** Fredag vecka 5 (2 okt 2026)  
**Tid:** 09:00–11:00 (2 timmar)  
**Hjälpmedel:** Inga  
**Betygsgräns:** G = 60p av 100p | VG = 85p av 100p

---

## Del 1 — Begrepp (30p)

*Förklara med egna ord. Inga kodexempel behövs — men du får använda dem om det hjälper.*  
*3p per fråga.*

**1.** Vad är en klass? Vad är ett objekt?

> **Svar:** En klass är en mall/ritning. Ett objekt är en konkret instans av den mallen. En klass `Dog` beskriver hur en hund ser ut. `Fido` och `Bella` är objekt — faktiska hundar skapade från den mallen.

**2.** Vad är arv? Ge ett exempel med egna ord (inte kod).

> **Svar:** Arv innebär att en klass ärver egenskaper och beteenden från en annan. En `GoldenRetriever` är en typ av `Dog` — den har allt en hund har, plus sina egna egenskaper. Man skriver inte om det som redan finns i basklassen.

**3.** Vad är en subklass? Vad är en basklass?

> **Svar:** Basklassen (föräldraklassen) innehåller det gemensamma. Subklassen (barnklassen) ärver från basklassen och kan lägga till eller specialisera beteende.

**4.** Vad innebär det att en variabel eller metod är `private`?

> **Svar:** Den är bara åtkomlig inifrån den egna klassen. Ingen annan klass kan läsa eller ändra den direkt.

**5.** Vad är en property? Hur skiljer den sig från ett vanligt fält?

> **Svar:** En property är ett styrt sätt att komma åt ett fält — ofta via `get` och `set`. Det ger kontroll: du kan validera värden i `set`, eller göra ett fält läsbart utifrån men inte skrivbart (`private set`). Ett vanligt fält har ingen sådan kontroll.

**6.** Vad är skillnaden mellan `int`, `string`, `bool` och `double`?

> **Svar:** `int` = heltal (42), `string` = text ("hej"), `bool` = sant/falskt (true/false), `double` = decimaltal (3.14). De lagrar olika typer av data och kan inte blandas utan konvertering.

**7.** Vad är en konstruktor? Varför används den?

> **Svar:** En konstruktor körs när ett objekt skapas med `new`. Den används för att sätta startvärden — se till att objektet är i ett giltigt tillstånd från början.

**8.** Vad menas med inkapsling (encapsulation)?

> **Svar:** Att dölja intern data och bara exponera det som behövs. Man gör fält privata och exponerar dem via properties eller metoder. Det skyddar objektets tillstånd från felaktig användning utifrån.

**9.** Vad är polymorfism? (VG-fråga — ingår i 85p-gränsen)

> **Svar:** Att objekt av olika typer kan behandlas som samma bastyp. En lista av `Monster` kan innehålla Goblins och Trolls — och man kan anropa `monster.Attack()` på alla utan att veta vilken subtyp det är. Varje subtyp ger sitt eget svar.

**10.** Varför delar man upp kod i klasser? Vad är syftet?

> **Svar:** För att organisera och separera ansvar. Varje klass ansvarar för en sak. Det gör koden lättare att läsa, testa, ändra och återanvända.

---

## Del 2 — Vad är fel? (40p)

*Titta på kodsnutten. Förklara vad som är fel och varför det inte kompilerar eller fungerar.*  
*5p per fråga — 3p för rätt identifierat fel, 2p för korrekt förklaring.*

---

**Fråga 1** *(5p)*

```csharp
string name = 42;
```

Vad är fel? Förklara.

> **Svar:** Man försöker tilldela ett heltal (`42`) till en `string`-variabel. C# är ett statiskt typat språk — du kan inte tilldela fel typ. `42` är ett `int`, inte en `string`. Rätt: `string name = "42";` eller `int age = 42;`

---

**Fråga 2** *(5p)*

```csharp
int x = "hello";
```

Vad är fel? Förklara.

> **Svar:** `"hello"` är en `string`, men `x` är deklarerad som `int`. Man kan inte stoppa text i en heltalsvariabel. Rätt: `string x = "hello";`

---

**Fråga 3** *(5p)*

```csharp
class Animal
{
    private string name;
}

class Program
{
    static void Main()
    {
        Animal a = new Animal();
        Console.WriteLine(a.name);
    }
}
```

Vad är fel? Förklara.

> **Svar:** `name` är `private` och kan inte nås utifrån klassen. `Program` är inte `Animal`, så `a.name` är otillåtet. Lösning: Gör en `public` property eller en `public` getter-metod.

---

**Fråga 4** *(5p)*

```csharp
class Dog
{
    public string Name { get; set; }
}

class GoldenRetriever : Dog
{
    public string Name { get; set; }
}
```

Koden kompilerar men något är designmässigt fel. Vad?

> **Svar:** `GoldenRetriever` deklarerar om `Name` som redan finns i basklassen `Dog`. Det bryter mot arv-principen — ärvd kod ska inte dupliceras. Rätt: Ta bort `Name` från `GoldenRetriever`, den ärver den automatiskt.

---

**Fråga 5** *(5p)*

```csharp
bool isAlive = "true";
```

Vad är fel? Förklara.

> **Svar:** `"true"` är en `string` (textliteral), inte ett `bool`-värde. `bool` tar `true` eller `false` utan citattecken. Rätt: `bool isAlive = true;`

---

**Fråga 6** *(5p)*

```csharp
class Monster
{
    public int HP { get; set; }
    public int Attack { get; set; }
}

class Goblin : Monster
{
    public Goblin()
    {
        HP = 30;
        Attack = 5;
    }
}

class Troll : Monster
{
    public Troll()
    {
        HP = 30;
        Attack = 5;
    }
}
```

Koden kompilerar och fungerar. Vad är ändå problematiskt?

> **Svar:** `Goblin` och `Troll` har identiska värden — de är inte faktiskt olika. Arv används men subklasserna tillför ingenting unikt. Dessutom saknas `XPReward`, `name` och andra egenskaper man förväntar sig av ett monster. Designmässigt: om alla monster är likadana, varför ha subklasser alls?

---

**Fråga 7** *(5p)*

```csharp
class Character
{
    public int HP;
    public int MaxHP;

    public void LevelUp()
    {
        MaxHP += 20;
        HP = MaxHP; // återställ HP
    }
}
```

Koden kompilerar och verkar fungera. Men det finns ett subtilt fel i systemet. Vad?

> **Svar:** `HP` och `MaxHP` är publika fält — de kan ändras var som helst utifrån utan kontroll. Rätt design: gör dem privata och exponera via properties. Annars kan vilken som helst kod sätta `character.HP = 9999;` och systemet bryts.  
> *(Godkänt svar: nämner att fält borde vara privata/properties. Bonuspoäng om de också nämner att `LevelUp` borde ha mer logik.)*

---

**Fråga 8** *(5p)* — VG-fråga

```csharp
List<Monster> monsters = new List<Monster>();
monsters.Add(new Goblin());
monsters.Add(new Troll());

foreach (Monster m in monsters)
{
    m.Attack(); // finns inte på Monster-klassen
}
```

Vad krävs för att det här ska fungera? Förklara.

> **Svar:** `Attack()` måste finnas som metod på `Monster`-basklassen — antingen som en vanlig metod, en `virtual` metod (som subklasserna kan `override`:a), eller som en `abstract` metod (om Monster aldrig instansieras direkt). Utan det ser kompilatorn ingen `Attack()`-metod på typen `Monster`.

---

## Del 3 — Läs koden, förstå systemet (30p)

*Inga svar att skriva. Läs koden och svara på frågorna med egna ord.*

---

```csharp
class Weapon
{
    public string Name { get; private set; }
    public int AttackBonus { get; private set; }
    public int Price { get; private set; }

    public Weapon(string name, int attackBonus, int price)
    {
        Name = name;
        AttackBonus = attackBonus;
        Price = price;
    }
}

class Sword : Weapon
{
    public Sword() : base("Svärd", 10, 50) { }
}

class Bow : Weapon
{
    public Bow() : base("Pilbåge", 7, 35) { }
}
```

**Fråga 1** *(10p)*  
Förklara vad `: base("Svärd", 10, 50)` gör. Varför är det där?

> **Svar:** `: base(...)` anropar basklassens konstruktor (`Weapon`). Eftersom `Weapon` kräver tre parametrar i sin konstruktor måste `Sword` skicka dem vidare. Det är ett sätt att initiera det ärvda innehållet utan att upprepa logiken. `"Svärd"`, `10` och `50` sätter namn, attack-bonus och pris för just detta vapen.

**Fråga 2** *(10p)*  
Varför är `AttackBonus` och `Price` deklarerade med `private set`?

> **Svar:** `private set` gör att egenskapen kan läsas utifrån (via `get`) men bara sättas inifrån klassen. Det skyddar värdena från att ändras efter att objektet skapats. En `Sword` ska alltid ha `AttackBonus = 10` — ingen ska kunna ändra det utifrån.

**Fråga 3** *(10p)*  
Någon föreslår att lägga till fler vapen så här:

```csharp
class MagicStaff : Weapon
{
    public MagicStaff() : base("Trollstav", 15, 80) { }
    public int MagicBonus { get; private set; } = 20;
}
```

Vad tillför `MagicBonus` som inte `Sword` eller `Bow` har? Är det rätt sätt att göra det?

> **Svar:** `MagicBonus` är en extra egenskap som bara `MagicStaff` har — en form av specialisering. Det är ett legitimt sätt att använda subklasser: basklassen har det gemensamma, subklassen lägger till det unika. Om spelet ska använda `MagicBonus` i strid krävs dock att man vet att man har en `MagicStaff` (typkontroll), vilket kan göra koden mer komplex. Rätt eller fel beror på design — men strukturen är korrekt.

---

## Poängöversikt

| Del | Maxpoäng |
|-----|---------|
| Del 1 — Begrepp (8 frågor × 3p + 1 VG × 3p) | 27p |
| Del 2 — Vad är fel? (7 frågor × 5p + 1 VG × 5p) | 40p |
| Del 3 — Läs koden (3 frågor × 10p) | 30p |
| **Totalt** | **97p** |

*Avrundat: G = 58p, VG = 82p*

---

## Rättningstips

- Fråga alltid dig själv: "Förstår de principen, eller har de memorerat formuleringen?"
- Acceptera svar med andra ord — det är förståelsen vi mäter
- VG-frågorna (9 i Del 1, 8 i Del 2) är märkta — räkna bort dem för G-gränsen
- Del 3 är designfokuserad — ge poäng för resonemang, inte bara rätt svar
