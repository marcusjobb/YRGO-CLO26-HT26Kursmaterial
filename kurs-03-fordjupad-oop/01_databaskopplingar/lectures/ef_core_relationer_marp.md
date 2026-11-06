---
marp: true
theme: nion-dark
paginate: true
---

# Mira tar uppdrag

**Kurs:** Fördjupad OOP i C#
**Modul:** 01 Spara och ladda data (EF Core, relationer)

Marcus Ackre Medina · YRGO · CLO26

---

## En hjälte, många uppdrag

```text
  Heroes                         Quests
  | Id | Name |                  | Id | Title        | HeroId |
  |  1 | Mira |  ◄────────────── |  1 | Rädda bron   |   1    |
                 ◄────────────── |  2 | Hitta kartan |   1    |
```

`HeroId` säger vem uppdraget hör till. Det kallas **främmande nyckel**.

Det här ritade ni i databaskursen som ett ER-diagram. Nu blir det C#.

Samma projekt och samma paket som förra gången. Inget nytt att installera.

---

## Två klasser som pekar på varandra

```csharp
class Hero
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public List<Quest> Quests { get; set; } = new();   // en hjälte har många uppdrag
}

class Quest
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public DateOnly Start { get; set; }
    public DateOnly End { get; set; }

    public int HeroId { get; set; }          // främmande nyckel: vem uppdraget hör till
    public Hero Hero { get; set; } = null!;  // navigering tillbaka till hjälten
}
```

---

## Kontexten får en tabell till

```csharp
class GameContext : DbContext
{
    public DbSet<Hero> Heroes => Set<Hero>();
    public DbSet<Quest> Quests => Set<Quest>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=game.db");
}
```

Ny klass betyder ny ritning:

```bash
dotnet ef migrations add AddQuests
```

**DB Browser:** kör programmet, tryck uppdatera. Ny tabell `Quests` med kolumnen `HeroId`.

---

## Spara en hjälte med uppdrag

```csharp
// En hjälte med två uppdrag. EF fyller i HeroId åt oss.
var mira = new Hero { Name = "Mira" };
mira.Quests.Add(new Quest { Title = "Rädda bron", Start = new DateOnly(2026, 11, 9), End = new DateOnly(2026, 11, 12) });
mira.Quests.Add(new Quest { Title = "Hitta kartan", Start = new DateOnly(2026, 11, 15), End = new DateOnly(2026, 11, 18) });
db.Heroes.Add(mira);
db.SaveChanges();
```

Du lägger uppdragen i Miras lista. EF sparar båda tabellerna.

---

## Include: hämta det som hör ihop

```csharp
// Include hämtar uppdragen tillsammans med hjälten
foreach (Hero hero in db.Heroes.Include(h => h.Quests).ToList())
{
    Console.WriteLine($"{hero.Name} har {hero.Quests.Count} uppdrag:");
    foreach (Quest quest in hero.Quests)
    {
        int days = quest.End.DayNumber - quest.Start.DayNumber;
        Console.WriteLine($"  {quest.Title}: {quest.Start:yyyy-MM-dd} till {quest.End:yyyy-MM-dd} ({days} dagar)");
    }
}
```

```text
Mira har 2 uppdrag:
  Rädda bron: 2026-11-09 till 2026-11-12 (3 dagar)
  Hitta kartan: 2026-11-15 till 2026-11-18 (3 dagar)
```

Utan `Include` är listan tom. EF hämtar bara det du ber om.

---

<!-- WOW-MOMENT: En enda LINQ-rad stoppar alla dubbelbokningar. Rita de fyra fallen på tavlan först och låt gruppen gissa hur många if-satser som behövs. -->

## Kan Mira vara på två ställen samtidigt?

Mira är upptagen 9–12 november. Någon vill ge henne ett uppdrag 11–14 november.

Hur många sätt kan två perioder krocka på?

---

## Fyra sätt att krocka

```text
Befintligt:        |=========|
1. Börjar inne:          |=========|
2. Slutar inne:  |=========|
3. Ligger inne:      |===|
4. Täcker allt: |=================|
```

Fyra fall. Fyra if-satser? Kanske åtta?

---

## En rad

```csharp
// En rad fångar alla sätt två perioder kan krocka på
bool busy = db.Quests.Any(q => q.HeroId == mira.Id && q.Start < newEnd && newStart < q.End);
Console.WriteLine(busy ? "Upptagen! Mira är redan på ett uppdrag då." : "Ledig, uppdraget är bokat.");
```

**De krockar om det nya börjar innan det gamla slutar, och det gamla börjar innan det nya slutar.**

Det gäller alla fyra fallen. Och ett uppdrag får börja samma dag som det förra slutar.

---

## Samma rad räddar ett hotell

Margareta i receptionen på Hotel Grandioso bokar rum 305 åt två gäster samma helg.

```csharp
bool taken = db.Bookings.Any(b => b.RoomId == roomId && b.CheckIn < checkOut && checkIn < b.CheckOut);
```

Det är samma rad. Den kommer ni att få nytta av i Inlämning 1.

---

## Det här tar du med dig

1. Relation = en lista på ena sidan, `XxxId` + navigering på andra sidan.
2. Ny klass betyder ny migration.
3. `Include` hämtar det som hör ihop.
4. Krock: `a.Start < b.End && b.Start < a.End`.

**Nu:** Övning, *Mira tar uppdrag*.
