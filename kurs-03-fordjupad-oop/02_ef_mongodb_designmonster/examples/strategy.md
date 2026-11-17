# Strategy: exempel med förklaringar

Projekten ligger i `source/repos`: `Designmonster_Strategy` (alla delar), `Designmonster_Strategy_Hero` (bara hjälten och vapnen) och `Designmonster_Strategy_Resa` (bara reseappen).

**Idén:** samma uppgift kan göras på flera sätt. Varje sätt är en egen klass som följer ett interface, och den som använder uppgiften har en referens till interfacet. Då kan sättet bytas utan att klassen som använder det ändras. Frågor för när mönstret passar finns i [flödesschemat](../notes/flodesschema_designmonster.md#strategy).

---

## 1. `Designmonster_Strategy` (alla delar)

### 1.1 Huvudprogrammet

```csharp
using Designmonster.Strategy.Demos;

// Övning: Strategy. Varje del ligger i sin egen demo-klass.
CombatDemo.RunBasics();        // Uppgift 1 och 2
TravelDemo.Run();              // Uppgift 3
CombatDemo.RunWeaponChoice();  // Uppgift 4
```

**Vad händer:** Programmet kör tre demoklasser i ordning. Varje uppgift har sin egen klass, så det går att köra en i taget.

**Stil:** `Program.cs` är en innehållsförteckning, precis som i Factory-exemplet. Kommentarerna visar vilken uppgift som körs.

### 1.2 Interfacet för vapen

```csharp
namespace Designmonster.Strategy.Interfaces;

/// <summary>Ett sätt att attackera. Varje vapen är en egen implementation.</summary>
public interface IAttackStrategy
{
    string Attack(string attackerName);
}
```

**Vad händer:** Ett enda kontrakt: alla vapen måste kunna svara på `Attack` med ett namn.

**Stil:** Interfacets namn börjar med `I`, som är C#-konventionen för interface. Det syns direkt när man läser `IAttackStrategy` att det inte är en konkret klass.

### 1.3 Hjälten

```csharp
public class Hero
{
    public string Name { get; }
    public IAttackStrategy Weapon { get; set; }

    public Hero(string name, IAttackStrategy weapon)
    {
        Name = name;
        Weapon = weapon;
    }

    public string Attack() => Weapon.Attack(Name);
}
```

**Vad händer:** Hjälten har en referens till vapnet, och skickar attacken vidare till det. Hjälten innehåller inga `if` för vapen.

**Stil:** Samma mönster som i Factory-exemplet. Det är en av poängerna med Strategy: `Hero` ändras inte när ett nytt vapen läggs till.

### 1.4 Demon: Mira byter vapen

```csharp
public static void RunBasics()
{
    Hero mira = new("Mira", new SwordAttack());
    Console.WriteLine(mira.Attack());

    mira.Weapon = new BowAttack();   // Bytet är en rad. Hero ändras inte.
    Console.WriteLine(mira.Attack());

    mira.Weapon = new IceAttack();
    Console.WriteLine(mira.Attack());
}
```

**Vad händer:** Mira svingar ett svärd, skjuter med pilbåge och fryser med is. Tre attacker, samma `Attack()`-anrop.

**Stil:** Kommentaren `Bytet är en rad` beskriver själva idén. Den som läser koden ser att `Hero` inte är med i ändringen.

### 1.5 Ett nytt vapen utan att ändra något annat

```csharp
namespace Designmonster.Strategy.Strategies.Attack;

/// <summary>Lades till utan att någon annan klass behövde ändras.</summary>
public class IceAttack : IAttackStrategy
{
    public string Attack(string attackerName) => $"{attackerName} fryser fienden med is! (8 skada)";
}
```

**Vad händer:** En ny klass som följer interfacet. Ingen annan klass ändras. Det är det som Strategy ger.

**Stil:** En klass per fil, i en egen mapp (`Strategies/Attack/`). Namnet säger vad klassen gör. Metoden är en expression-bodied member (`=>`), eftersom den bara har en rad.

### 1.6 Uppgift 4: spelaren väljer

```csharp
public static void RunWeaponChoice()
{
    Console.WriteLine("Välj vapen: 1 = svärd, 2 = pilbåge, 3 = eldklot");
    string? input = Console.ReadLine();

    Hero chosen = new("Mira", ChooseWeapon(input));
    Console.WriteLine(chosen.Attack());
}

private static IAttackStrategy ChooseWeapon(string? input) => input switch
{
    "2" => new BowAttack(),
    "3" => new FireballAttack(),
    _ => new SwordAttack()   // Svärdet är standard, även vid okänt val
};
```

**Vad händer:** Användaren väljer ett alternativ. `ChooseWeapon` skapar rätt vapen. Hjälten får det och attackerar.

**Stil:**
- `string?` betyder att värdet kan vara `null`. `Console.ReadLine()` kan returnera `null`, så typen måste säga det.
- `_ =>` är standardgrenen. Okänt val ger svärdet, och det står i kommentaren.
- `ChooseWeapon` är en fabrik i liten skala. Kommentaren i koden säger det: den flyttas till en fabrik i Factory-övningen. Det är en bra koppling mellan de två mönstren.

### 1.7 Interfacet för resor

```csharp
namespace Designmonster.Strategy.Interfaces;

/// <summary>Ett sätt att resa. Varje reseform är en egen implementation.</summary>
public interface IRouteStrategy
{
    string Describe(double km);
}
```

**Vad händer:** Samma idé som för vapen, men med en annan uppgift: beskriva en resa. Interfacet är nytt, och det beror inte på vapeninterfacet.

**Stil:** Två interface i samma projekt, med samma form. Det visar att mönstret är samma oavsett vad det används till.

### 1.8 Reseplanerare

```csharp
public class TripPlanner
{
    public IRouteStrategy Strategy { get; set; }

    public TripPlanner(IRouteStrategy strategy)
    {
        Strategy = strategy;
    }

    public string Plan(double km) => Strategy.Describe(km);
}
```

**Vad händer:** `TripPlanner` vet inte hur resan beräknas. Den frågar strategin.

**Stil:** `Strategy` är en `set`-property, så den kan bytas efter att planeraren skapats. Det är samma sak som `Weapon` i `Hero`.

### 1.9 Tre reseformer

```csharp
public class CarStrategy : IRouteStrategy
{
    public string Describe(double km) => $"Bil: {km * 2 + 5} minuter (inklusive parkering)";
}
```

**Vad händer:** Bilen tar 2 minuter per km och lägger till 5 minuter för parkering.

**Stil:** Konstanterna är inbakade i metoden. I ett större projekt hade man flyttat dem till egna fält eller en konfiguration. Här är det tydligare att se beräkningen direkt.

Promenad och cykel har samma form. Promenad tar 12 minuter per km, och cykel 4 minuter per km.

### 1.10 Demon: samma uppgift, olika sätt

```csharp
public static void Run()
{
    TripPlanner planner = new(new WalkStrategy());
    Console.WriteLine(planner.Plan(3));

    planner.Strategy = new BikeStrategy();
    Console.WriteLine(planner.Plan(3));

    planner.Strategy = new CarStrategy();
    Console.WriteLine(planner.Plan(3));
}
```

**Vad händer:** Samma `Plan(3)` ger tre olika svar, eftersom strategin byts mellan anropen.

**Stil:** Samma mönster som i stridsdemon. Kodens form är densamma, bara uppgiften skiljer sig.

---

## 2. `Designmonster_Strategy_Hero` (övningsdelen)

Den här mappen innehåller bara hjälten och vapnen, alltså uppgifterna 1, 2 och 4. Den är en kopia av första delen av `Designmonster_Strategy`.

**Stil:** Två projekt med samma kod är bra för övningen. Man kan börja om från en ren kopia utan att ta med resten. Samma sak gäller `Designmonster_Strategy_Resa`, som bara innehåller reseappen.

---

## 3. `Designmonster_Strategy_Resa` (reseappen)

Den här mappen innehåller bara `IRouteStrategy`, de tre reseformerna, `TripPlanner` och `TravelDemo`. Den är en kopia av reseappdelen i `Designmonster_Strategy`.

**Vad händer:** Samma kod som i avsnitt 1.7 till 1.10.

**Stil:** Samma som i avsnitt 2: en mapp per övning gör det lätt att se just den övningens kod.
