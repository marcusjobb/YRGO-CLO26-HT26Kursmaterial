---

title: 🎄 DEL 4: TESTING & SUPER EVIL MAGE MARCUS SPECIAL 🎅
author: Marcus Ackre Medina
type: assignment
topic: testing
difficulty: 3
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/2025/csharp/4_test/Assignments/xmas_adventureawaits/part_3.md"
description: "_VIKTIGT! Denna guide är uppdelad i flera delar som bygger på varandra. Följ dem i ordning!_"
tags: ["csharp", "evil", "installation", "mage", "marcus", "part", "special", "super", "testing", "visual-studio"]
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

Här är lite exempel på testning

```csharp
[TestClass]
public class PlayerTests
{
    private Player _player;
    private Item _magicItem;

    [TestInitialize]
    public void Setup()
    {
        // Skapar en ny spelare och en magisk klocka innan varje test
        _player = new Player("TestTomte");
        _magicItem = new Item("Magisk Klocka", "Ringer själv vid midnatt")
        {
            IsMagical = true,
            Weight = 5
        };
    }

    [TestMethod]
    public void AddItem_WhenInventoryNotFull_ReturnsTrue()
    {
        // Arrange är redan fixat i Setup

        // Act - Försöker lägga till en magisk klocka i spelarens inventarie
        var result = _player.AddItem(_magicItem);

        // Assert - Kontrollera att resultatet är sant och att klockan finns i inventariet
        Assert.IsTrue(result, "Tomten måste kunna bära några presenter!");
        CollectionAssert.Contains(_player.Inventory, _magicItem);
    }

    [TestMethod]
    public void AddItem_WhenInventoryFull_ReturnsFalse()
    {
        // Arrange - Fyller spelarens inventarie med 5 presenter
        for (int i = 0; i < 500; i++)
        {
            _player.AddItem(new Item($"Present {i}", "En julklapp"));
        }

        // Act - Försöker lägga till en magisk klocka när inventariet är fullt
        var result = _player.AddItem(_magicItem);

        // Assert - Kontrollera att resultatet är falskt eftersom inventariet är fullt
        Assert.IsFalse(result, "Ho ho NO! Tomten kan inte bära oändligt många saker!");
    }
}
```

### Tomtenisse-Tester

```csharp
[TestClass]
public class ElfTests
{
    private WorkshopElf _elf;

    // Denna metod körs innan varje test för att ställa in nödvändiga objekt
    [TestInitialize]
    public void Setup()
    {
        // Skapar en ny tomtenisse med namnet "Glader"
        _elf = new WorkshopElf("Glader");
    }

    // Testar att tomtenissen kan skapa en leksak när den inte är trött
    [TestMethod]
    public void MakeToy_WhenNotTired_CreatesToy()
    {
        // Act - Försöker skapa en leksak
        var toy = _elf.MakeToy();

        // Assert - Kontrollera att leksaken inte är null och att dess kvalitet är minst 8
        Assert.IsNotNull(toy, "Leksaken ska inte vara null");
        Assert.IsTrue(toy.Quality >= 8, "Tomtenissar gör alltid bra leksaker!");
    }

    // Testar att ett undantag kastas när tomtenissen är utmattad
    [TestMethod]
    public void MakeToy_WhenExhausted_ThrowsException()
    {
        // Arrange - Låter tomtenissen skapa 100 leksaker för att göra den trött
        for (int i = 0; i < 100; i++)
        {
            _elf.MakeToy();
        }

        // Act & Assert - Försöker skapa en leksak och förväntar sig ett undantag
        var ex = Assert.ThrowsException<ExhaustedElfException>(
            () => _elf.MakeToy());

        // Kontrollera att undantagsmeddelandet innehåller ordet "fika"
        StringAssert.Contains(ex.Message, "fika", "Trötta tomtenissar behöver fika!");
    }
}
```

## 😈 SUPER EVIL MAGE MARCUS SPECIAL TESTS

### Marcus Test Regler

````csharp
```csharp
[TestClass]
public class SuperEvilTests
{
    [TestMethod]
    public void EvilMagic_CannotInfectChristmasSpirit()
    {
        // Arrange - Skapar en julstämningsmätare och en ond trollformel
        var spirit = new ChristmasSpiritMeter();
        var evilSpell = new Spell { Type = SpellType.Evil };

        // Act - Lägg till lite julstämning och kasta den onda trollformeln
        spirit.AddSpirit(50);  // Lite julstämning
        evilSpell.Cast(spirit);  // Marcus försöker förstöra

        // Assert - Kontrollera att julstämningen fortfarande är över noll
        Assert.IsTrue(spirit.Level > 0,
            "MWAHAHAHA! Jag förstörde julstämningen! 😈");
    }

    [TestMethod]
    public void EvilBugs_CannotCrashGame()
    {
        // Arrange - Skapar ett julspel och en lista med onda kommandon
        var game = new ChristmasGame();
        var evilCommands = new[] {
            "crash", "null", "undefined", "break", "MWAHAHAHA"
        };

        // Act & Assert - Försök att köra varje kommando och kontrollera att spelet inte kraschar
        foreach (var command in evilCommands)
        {
            Assert.DoesNotThrow(() => game.ProcessCommand(command),
                $"BUSTED! Spelet kraschade på kommando: {command}");
        }
    }
}
````

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
       // Vad händer om item är null?
   }

   // BRA - Marcus plan misslyckas
   public bool AddToInventory(Item item)
   {
       if (item == null)
           return false;

       if (_inventory.Count >= MaxItems)
           return false;

       _inventory.Add(item);
       return true;
   }
   ```

## 🎯 SLUTLIGA TESTTIPS

1. Testa ALLT för att Marcus kommer att göra det.
2. Använd bra namn på tester och metoder.
3. Kommentera dina tester för att förklara varför.
4. Testa edge cases och felhantering.
5. Använd AAA-mönstret för att göra tester läsbara.
6. Var noga med att testa alla publika metoder.

_Fortsättning följer i [Del 4: Testing & Super Evil Mage Marcus Special](./part_4.md)..._
