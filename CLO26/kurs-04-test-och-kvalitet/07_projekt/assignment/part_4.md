---

title: 🎄 DEL 4: TESTING & SUPER EVIL MAGE MARCUS SPECIAL 🎅
author: Marcus Ackre Medina
type: assignment
topic: testing
difficulty: 3
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/2025/csharp/4_test/Assignments/xmas_adventureawaits/part_4.md"
description: "_VIKTIGT! Denna guide är uppdelad i flera delar som bygger på varandra. Följ dem i ordning!_"
tags: ["csharp", "evil", "installation", "mage", "marcus", "part", "special", "super", "testing", "verktyg"]
week_fit: []
---

# 🎄 DEL 4: TESTING & SUPER EVIL MAGE MARCUS SPECIAL 🎅

🔴


_VIKTIGT! Denna guide är uppdelad i flera delar som bygger på varandra. Följ dem i ordning!_

### 📚 Guidens Delar

1. [**Del 1** - Introduktion & Projektstruktur](./part_1.md)
2. [**Del 2** - Speldesign & Grundläggande Kod](./part_2.md)
3. [**Del 3** - Avancerad Funktionalitet](./part_3.md)
4. [**Del 4** - Testing & Super Evil Mage Marcus Special](./part_4.md)
5. [**Del 5** - Extra Resources & Bonusmaterial](./part_5.md)

Gruppuppdelning: [Gruppindelning](./group.md)

## 🧪 TESTNING (SUPER VIKTIGT!)

### Grundläggande Testning

```csharp
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PlayerTests
{
    private Player _player;
    private Item _magicItem;

    // Setup körs före varje test för att förbereda testobjekten
    [TestInitialize]
    public void Setup()
    {
        _player = new Player("TestTomte");
        _magicItem = new Item("Magisk Klocka", "Ringer själv vid midnatt")
        {
            IsMagical = true,
            Weight = 5
        };
    }

    // Testar att lägga till ett föremål när inventariet inte är fullt
    [TestMethod]
    public void AddItem_WhenInventoryNotFull_ReturnsTrue()
    {
        // Act - utför handlingen som ska testas
        var result = _player.AddItem(_magicItem);

        // Assert - verifierar att resultatet är som förväntat
        Assert.IsTrue(result, "Tomten måste kunna bära några presenter!");
        CollectionAssert.Contains(_player.Inventory, _magicItem);
    }

    // Testar att lägga till ett föremål när inventariet är fullt
    [TestMethod]
    public void AddItem_WhenInventoryFull_ReturnsFalse()
    {
        // Arrange - förbereder testet genom att fylla inventariet
        for (int i = 0; i < 5; i++)
        {
            _player.AddItem(new Item($"Present {i}", "En julklapp"));
        }

        // Act - utför handlingen som ska testas
        var result = _player.AddItem(_magicItem);

        // Assert - verifierar att resultatet är som förväntat
        Assert.IsFalse(result, "Ho ho NO! Tomten kan inte bära oändligt många saker!");
    }
}
```

### Tomtenisse-Tester

````csharp
```csharp
[TestClass]
public class ElfTests
{
    private WorkshopElf _elf;

    // Setup körs före varje test för att förbereda testobjekten
    [TestInitialize]
    public void Setup()
    {
        _elf = new WorkshopElf("Glader");
    }

    // Testar att skapa en leksak när tomtenissen inte är trött
    [TestMethod]
    public void MakeToy_WhenNotTired_CreatesToy()
    {
        // Act - utför handlingen som ska testas
        var toy = _elf.MakeToy();

        // Assert - verifierar att resultatet är som förväntat
        Assert.IsNotNull(toy, "Tomtenissen måste skapa en leksak!");
        Assert.IsTrue(toy.Quality >= 8, "Tomtenissar gör alltid bra leksaker!");
    }

    // Testar att skapa en leksak när tomtenissen är utmattad
    [TestMethod]
    public void MakeToy_WhenExhausted_ThrowsException()
    {
        // Arrange - förbereder testet genom att utmatta tomtenissen
        for (int i = 0; i < 100; i++)
        {
            _elf.MakeToy();
        }

        // Act & Assert - utför handlingen och verifierar att rätt undantag kastas
        var ex = Assert.ThrowsException<ExhaustedElfException>(
            () => _elf.MakeToy());

        Assert.IsTrue(ex.Message.Contains("fika"), "Trötta tomtenissar behöver fika!");
    }
}
````

## 😈 SUPER EVIL MAGE MARCUS SPECIAL TESTS

```csharp
[TestClass]
public class SuperEvilTests
{
    [TestMethod]
    public void EvilMagic_CannotInfectChristmasSpirit()
    {
        // Arrange - förbereder testet genom att skapa en julstämningsmätare och en ond trollformel
        var spirit = new ChristmasSpiritMeter();
        var evilSpell = new Spell { Type = SpellType.Evil };

        // Act - utför handlingarna som ska testas
        spirit.AddSpirit(50);  // Lägg till lite julstämning
        evilSpell.Cast(spirit);  // Marcus försöker förstöra julstämningen

        // Assert - verifierar att julstämningen inte är helt förstörd
        Assert.IsTrue(spirit.Level > 0,
            "MWAHAHAHA! Jag förstörde julstämningen! 😈");
    }

    [TestMethod]
    public void EvilBugs_CannotCrashGame()
    {
        // Arrange - förbereder testet genom att skapa ett spel och en lista med onda kommandon
        var game = new ChristmasGame();
        var evilCommands = new[] {
            "crash", "null", "undefined", "break", "MWAHAHAHA"
        };

        // Act & Assert - utför varje kommando och verifierar att spelet inte kraschar
        foreach (var command in evilCommands)
        {
            Assert.DoesNotThrow(() => game.ProcessCommand(command),
                $"BUSTED! Spelet kraschade på kommando: {command}");
        }
    }
}
```

### Marcus Evil Magic Handler

```csharp
public class EvilMagicHandler
{
    public bool TryDetectEvilMagic(Item item)
    {
        // Marcus försöker vara listig!
        if (item.Name.Contains("Evil") ||
            item.Description.Contains("MWAHAHAHA") ||
            item.Weight == 666)
        {
            return true;  // Evil magic detected!
        }

        // Kolla efter dolda förbannelser
        return CheckForHiddenCurses(item);
    }

    private bool CheckForHiddenCurses(Item item)
    {
        // Marcus är smart - vi måste vara smartare!
        var cursedWords = new[] {
            "curse", "hex", "evil", "marcus", "bug"
        };

        return cursedWords.Any(word =>
            item.Name.ToLower().Contains(word) ||
            item.Description.ToLower().Contains(word));
    }
}
```

## Täckning

All kod måste ha täckning, kvalitet och testa särskilda områden... annars blir det otäckt! 😈

Med täckning menar jag att den ska testa ALLT, inklusive edge cases och felhantering. Testerna ska vara läsbara och testa EN sak per test. Bra felmeddelanden är ett MÅSTE! Faktum är att till och med POCO-klasser MÅSTE ha tester.... inte nog med det... Privata metoder MÅSTE testas också, detta sker genom att testa publika metoder som använder de privata metoderna.

## 📝 TESTRAPPORT FÖR SUPER EVIL MAGE MARCUS

### Vad Marcus Kollar Efter

1. **Täckning**

   - Alla publika metoder MÅSTE ha tester
   - Edge cases MÅSTE testas
   - Felhantering MÅSTE testas

2. **Kvalitet**

   - Tester MÅSTE vara läsbara
   - Varje test testar EN sak
   - Bra felmeddelanden är ett MÅSTE

3. **Särskilda Områden**
   - Inventariehantering
   - Spelarsystem
   - Rumsnavigation
   - Föremålsinteraktioner

### Marcus Testchecklista

```plaintext
[ ] Alla POCO-klasser har tester
[ ] Alla publika metoder är testade
[ ] Edge cases är testade
[ ] Felhantering är testad
[ ] Testnamn är beskrivande
[ ] AAA-mönstret används
[ ] Magiska nummer undviks
[ ] Mockade beroenden används korrekt
```

## 🎁 BONUS: MARCUS FAVORITBUGGAR

### Vanliga Misstag

1. **Glömda Null-Checks**

   ```csharp
   // DÅLIGT - Marcus gillar detta!
   public void UseItem(Item item)
   {
       Console.WriteLine(item.Name);  // BOOM om item är null!
   }

   // BRA - Marcus blir besviken...
   public void UseItem(Item item)
   {
       if (item == null)
           throw new ArgumentNullException(nameof(item));

       Console.WriteLine(item.Name);
   }
   ```

2. **Ohantertade Edge Cases**

   ```csharp
   // DÅLIGT - Marcus plan lyckas!
   public void AddToInventory(Item item)
   {
       _inventory.Add(item);  // Vad händer när inventory är fullt?
   }

   // BRA - Marcus plan misslyckas
   public bool AddToInventory(Item item)
   {
       if (_inventory.Count >= MaxItems)
           return false;

       _inventory.Add(item);
       return true;
   }
   ```

## 🎯 SLUTLIGA TESTTIPS

1. Testa ALLT
2. _You better watch out, you better not cry_

_Fortsättning följer i [Del 5: Extra Resources & Bonusmaterial](./part_5.md)..._
