# Polymorfism med Item/Weapon/Food — samma mönster, spelkontext

> Det här är **inte** en transkriberad livekodning — det är en uppföljning som bygger på `2026-09-23_polymorfism.md` (Djur/Hund/Katt) och fyller i de tomma `Item`/`Weapon`/`Food`-stubbarna från den sessionen med riktig kod. Tanken är att göra kopplingen till spelprojekten (Skogsäventyret, Dungeon Crawler) helt konkret.

I Djur-exemplet var `LåtaLjud()` poängen — olika djur låter olika. I ett spel är mönstret exakt detsamma, men frågan blir: vad gör ett föremål när du **använder** det?

## Basklassen

```csharp
class Item
{
    public string Namn { get; set; }
    public virtual void Använd() { }
}
```

Samma recept som `Djur`: ett gemensamt fält (`Namn`), och en `virtual`-metod som subklasserna får skriva över. `Item` vet inte själv vad "använda" betyder — det är upp till varje sorts föremål.

## Subklasserna

```csharp
class Weapon : Item
{
    public int Skada { get; set; }
    public override void Använd() => Console.WriteLine($"Du svingar {Namn} och gör {Skada} skada!");
}

class Food : Item
{
    public int Läkning { get; set; }
    public override void Använd() => Console.WriteLine($"Du äter {Namn} och läker {Läkning} HP.");
}
```

Ett vapen "använder man" genom att slå med det — skada. En matvara "använder man" genom att äta den — läkning. Samma metodnamn, `Använd()`, helt olika resultat.

## Inventoryt — en lista, olika sorters saker

```csharp
List<Item> inventory = new()
{
    new Weapon { Namn = "Rostigt svärd", Skada = 8 },
    new Food   { Namn = "Läkande ört",   Läkning = 15 },
    new Weapon { Namn = "Eldstav",       Skada = 20 },
};

foreach (Item item in inventory)
{
    item.Använd();
}
```

```
Du svingar Rostigt svärd och gör 8 skada!
Du äter Läkande ört och läker 15 HP.
Du svingar Eldstav och gör 20 skada!
```

En `List<Item>`, en `foreach`-loop — precis som `List<Djur>`. Lägg till en tredje itemtyp (`Vapendetalj`, `Trollformel`, vad som helst) och den här loopen ändras fortfarande aldrig. Det är exakt det som gör `List<Item>`/`List<Monster>` användbart i Skogsäventyret och Dungeon Crawler: inventoryt eller monsterlistan bryr sig inte om vad som läggs till, bara att det är en `Item`/`Monster`.

## När du behöver mer än basklassen erbjuder

Precis som `Bit()` bara fanns på `Hund`, kan en subklass ha egna metoder som inte finns på `Item`:

```csharp
class Weapon : Item
{
    public int Skada { get; set; }
    public override void Använd() => Console.WriteLine($"Du svingar {Namn} och gör {Skada} skada!");

    public void Slipa()
    {
        Skada += 2;
        Console.WriteLine($"{Namn} är nu vassare! (+2 skada)");
    }
}
```

```csharp
foreach (Item item in inventory)
{
    item.Använd();
    if (item is Weapon vapen)
        vapen.Slipa();   // bara vapen kan slipas
}
```

`if (item is Weapon vapen)` gör två saker på en gång: kollar att `item` faktiskt är ett `Weapon`, **och** skapar en ny variabel `vapen` av typen `Weapon` du kan använda direkt i blocket — ingen separat cast-rad behövs (samma resultat som `((Weapon)item).Slipa()`, men kortare och vanligare skrivsätt i modern C#).

## Kopplingen till spelprojektet

I `Speltips.md` för Skogsäventyret pratar vi om `List<Monster>` med `Goblin`/`Troll`/`Drake`. Exakt samma tänk:

- **Monster-basklass** ⟷ `Item`-basklass här
- **Goblin/Troll/Drake** ⟷ `Weapon`/`Food` här
- **`monster.Attack(player)`** ⟷ `item.Använd()` här
- **`List<Monster> fiender`** ⟷ `List<Item> inventory` här

Bygger du ett `Item`-system i ditt spel (svärd, nycklar, potions, vad temat nu kräver) — det är samma tre steg varje gång: en basklass med en `virtual`-metod, subklasser som skriver `override`, och en `List<BasKlass>` som håller dem alla.
