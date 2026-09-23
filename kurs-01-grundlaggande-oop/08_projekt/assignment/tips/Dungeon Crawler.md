# Dungeon Crawler

## 1. Planera kartan

Rita kartan innan du kodar — vilka rum finns, och vilka vägar (N/S/Ö/V) leder mellan dem?

```
                       ┌─────────────────────┐
                       │     Spelet slutar     │
                       └───────────▲───────────┘
                                   │ N
                       ┌───────────┴───────────┐
                       │ Ingången till slottet   │
                       └───────────┬───────────┘
                                   │ S
                       ┌───────────▼───────────┐
                       │    Inne i slottet       │
                       └──┬────────────────────┬──┘
               V ◄────────┘          │ N        └────────► Ö
    ┌─────────────────────┐          │          ┌─────────────────────┐
    │ Bibliotek (nyckel)    │          │          │      Vapenlager       │
    └─────────────────────┘          │          └─────────────────────┘
                                      │
                       ┌──────────────▼───────────┐
                       │        Tronsalen           │
                       │    (hemlig låst dörr)      │
                       └──┬────────────────────┬──┘
       V ◄── hemlig dörr ──┘                    └── hemlig dörr ──► Ö
    ┌─────────────────────┐                    ┌─────────────────────┐
    │      Dödsfälla         │                    │     Skattkammaren      │
    └─────────────────────┘                    └─────────────────────┘
```

Varje rum i diagrammet är ett rum i spelet — och blir ett värde i ett `enum` nedan. `GameOver` är inte ett rum, utan vad som händer om spelaren går norrut från ingången.

---

## 2. Saker spelaren kan plocka upp

- Nyckel
- Bok (helt meningslös — spelaren kommer ändå tro att den är viktig)

---

## 3. TUI (Terminal User Interface)

Så här kan en runda se ut för spelaren:

```
Du befinner dig vid ingången till slottet.
Här kan du gå: Nord, Syd

Vad vill du göra >
```

```
Du befinner dig i biblioteket.
Här kan du gå: Öst
Här ser du: en nyckel

Vad vill du göra >
```

---

## 4. Kodmässigt

Det här är **pseudokod** — strukturen är rätt, men du fyller i detaljerna själv.

### Rum-ID:n — ett enum istället för text eller index

Ge varje rum ett namn i ett `enum`. Det gör att exits pekar på **rätt rum** istället för en textsträng eller ett listindex du måste hålla reda på själv:

```csharp
enum RoomId
{
    Entrance,
    InsideCastle,
    Library,
    Armory,
    ThroneRoom,
    DeathTrap,
    Treasure
}
```

### Rummen och sakerna

```csharp
class Item
{
    public string Namn;
}

class Location
{
    public RoomId Id;
    public string Titel;                // t.ex. "Biblioteket"
    public RoomId? ExitNorth;
    public RoomId? ExitSouth;
    public RoomId? ExitEast;
    public RoomId? ExitWest;
}

List<Item> inventory = new();           // saker spelaren bär på

Dictionary<RoomId, Location> locations = new()
{
    [RoomId.Entrance] = new Location
    {
        Id = RoomId.Entrance,
        Titel = "Ingången till slottet",
        ExitSouth = RoomId.InsideCastle
        // Nord finns inte som exit här — se spelloopen, "gå nord" avslutar spelet
    },
    [RoomId.InsideCastle] = new Location
    {
        Id = RoomId.InsideCastle,
        Titel = "Inne i slottet",
        ExitNorth = RoomId.ThroneRoom,
        ExitWest  = RoomId.Library,
        ExitEast  = RoomId.Armory
    },
    // ... Library, Armory, ThroneRoom, DeathTrap, Treasure på samma sätt
};
```

**Tips:** `RoomId?` (med frågetecken) betyder "antingen ett RoomId, eller inget alls" — det är hur du uttrycker "ingen väg åt det hållet" utan en tom sträng.

### Spelloopen

```csharp
RoomId currentId = RoomId.Entrance;

while (true)
{
    Location currentLocation = locations[currentId];
    Console.WriteLine(currentLocation.Titel);

    if (currentLocation.ExitNorth != null)
        Console.WriteLine("Nord: " + currentLocation.ExitNorth);
    // upprepa för Syd, Öst, Väst

    Console.Write("Vad vill du göra > ");
    string input = Console.ReadLine();
    string[] words = input.Split(' ').ToLower();

    if (words[0] == "gå")
    {
        if (words[1] == "nord" && currentId == RoomId.Entrance)
        {
            Console.WriteLine("Spelet är slut!");
            break;
        }
        else if (words[1] == "nord" && currentLocation.ExitNorth != null)
        {
            currentId = currentLocation.ExitNorth.Value;
        }
        else
        {
            Console.WriteLine("Du sprang in i en vägg.");
        }
    }
}
```

### Vanliga buggar att se upp för

- `=` betyder **tilldelning**, `==` betyder **jämförelse**. `if (words[1] == "nord")`, inte `if (words[1] = "nord")`.
- `Dictionary<TKey, TValue>` och `List<T>` läses med hakparenteser: `locations[RoomId.Library]`, inte `locations(RoomId.Library)`.
- Namngivning: välj **ett** sätt att skriva fält och properties (t.ex. `PascalCase`: `ExitNorth`) och håll dig till det överallt — `Exit_north` och `exit_north` i samma klass är inte samma variabel.
- `RoomId?` är nullable — du måste läsa ut värdet med `.Value` (eller mönstermatcha) innan du kan använda det som ett vanligt `RoomId`.

---

## Checklista innan du börjar koda

- [ ] Kartan är ritad, alla rum och vägar är bestämda
- [ ] `Location`-klassen har alla fält du behöver (titel, exits, ev. saker i rummet)
- [ ] Du har bestämt hur ett rum "vet" vilket rum som ligger i varje riktning
- [ ] Spelloopen läser input, tolkar kommandot, och uppdaterar `currentLocation`
- [ ] Du har testat vad som händer om spelaren skriver en riktning som inte finns
