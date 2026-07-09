# 🎄 DEL 2: SPELDESIGN & GRUNDLÄGGANDE KOD 🎅

🔴


_VIKTIGT! Denna guide är uppdelad i flera delar som bygger på varandra. Följ dem i ordning!_

### 📚 Guidens Delar

1. [**Del 1** - Introduktion & Projektstruktur](./part_1.md)
2. [**Del 2** - Speldesign & Grundläggande Kod](./part_2.md)
3. [**Del 3** - Avancerad Funktionalitet](./part_3.md)
4. [**Del 4** - Testing & Super Evil Mage Marcus Special](./part_4.md)
5. [**Del 5** - Extra Resources & Bonusmaterial](./part_5.md)

Gruppuppdelning: [Gruppindelning](./group.md)

## 🏰 SPELVÄRLDEN

### Exempel på rum

1. **🏠 Tomteverkstaden**

   ```csharp
   public class Workshop : Room
   {
       public Workshop()
       {
           Name = "Tomteverkstaden";
           Description = "En mysig verkstad full med leksaker och verktyg. " +
                        "Det doftar kanel och gran.";
           Items = new List<Item>
           {
               new Item("Magisk Hammare", "Perfekt för att bygga julklappar!")
           };
       }
   }
   ```

2. **🦌 Renstallet**
   ```csharp
   public class ReindeerStable : Room
   {
       public ReindeerStable()
       {
           Name = "Renstallet";
           Description = "Ett varmt stall där renarna vilar. " +
                        "Rudolfs näsa lyser upp rummet.";
           Items = new List<Item>
           {
               new Item("Morot", "Rudolfs favorit!")
           };
       }
   }
   ```

### Grundläggande Klasser

#### Spelar-klass (POCO)

```csharp
public class Player
{
    public string Name { get; set; }
    public List<Item> Inventory { get; private set; }
    public Room CurrentRoom { get; set; }

    public Player(string name)
    {
        Name = name;
        Inventory = new List<Item>();
    }

    public bool AddItem(Item item)
    {
        if (Inventory.Count >= 5) // Du bestämmer själv maxgränsen
        {
            return false; // Inventory fullt!
        }
        Inventory.Add(item);
        return true;
    }
}
```

#### Förslag på Föremåls-klass (POCO)

```csharp
public class Item
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsMagical { get; set; }

    public Item(string name, string description)
    {
        Name = name;
        Description = description;
        IsMagical = false;
    }
}
```

## 🎮 SPELLOGIK

### Kommandohanterare

```csharp
public class CommandHandler
{
    private readonly GameEngine _game;

    public CommandHandler(GameEngine game)
    {
        _game = game;
    }

    public string HandleCommand(string input)
    {
        // Plocka ut första ordet i kommandot
        var command = input.ToLower().Split(' ');

        switch (command[0])
        {
            case "gå":
                return HandleMovement(command);

            /* fyll på med fler kommandon */

            case "hjälp":
                return ShowHelp();
            default:
                return "Ho ho ho! Jag förstod inte det kommandot!";
        }
    }

    private string HandleMovement(string[] command)
    {
        if (command.Length < 2)
            return "Vart vill du gå?";

        return _game.MovePlayer(command[1]);
    }
}
```

### Förslag på Huvudmeny (UI)

```csharp
public class MainMenu
{
    public void ShowMenu()
    {
        Console.Clear();
        Console.WriteLine("=== 🎄 JULTOMTENS KODÄVENTYR 🎄 ===");
        Console.WriteLine("1. Starta Nytt Spel");
        Console.WriteLine("2. Ladda Spel");
        Console.WriteLine("3. Hjälp");
        Console.WriteLine("4. Avsluta");
        Console.WriteLine("================================");
    }

    public void HandleMainMenu()
    {
        var running = true;
        while (running)
        {
            ShowMenu();
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    StartNewGame();
                    break;
                case "2":
                    LoadGame();
                    break;
                case "3":
                    ShowHelp();
                    break;
                case "4":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Ogiltigt val!");
                    break;
            }
        }
    }
}
```

## 🎯 EXEMPEL SPELKOMMANDON

### Kommandon (exempel)

1. **Förflyttning**

   - `gå norr`
   - `gå söder`
   - `gå öster`
   - `gå väster`
   - `gå upp`
   - `gå ner`
   - `gå till [rum]`
   - `gå på dass` 🚽

2. **Föremål**

   - `ta [föremål]`
   - `släpp [föremål]`
   - `använd [föremål]`
   - `undersök [föremål]`
   - `titta på [föremål]`

3. **Information**
   - `titta`
   - `inventory`
   - `hjälp`

### Exempel på Implementering

```csharp
/// <summary>
/// Represents the main menu of the game "JULTOMTENS KODÄVENTYR".
/// </summary>
public class MainMenu
{
    /// <summary>
    /// Displays the main menu options to the console.
    /// </summary>
    public void ShowMenu()
    {
        // Clears the console screen.
        Console.Clear();
        // Displays the title and menu options.
        Console.WriteLine("=== 🎄 JULTOMTENS KODÄVENTYR 🎄 ===");
        Console.WriteLine("1. Starta Nytt Spel");
        Console.WriteLine("2. Ladda Spel");
        Console.WriteLine("3. Hjälp");
        Console.WriteLine("4. Avsluta");
        Console.WriteLine("================================");
    }

    /// <summary>
    /// Handles the user's input for the main menu and performs the corresponding actions.
    /// </summary>
    public void HandleMainMenu()
    {
        // Variable to keep the menu running.
        var running = true;
        while (running)
        {
            // Displays the menu.
            ShowMenu();
            // Reads the user's choice.
            var choice = Console.ReadLine();

            // Executes the corresponding action based on the user's choice.
            switch (choice)
            {
                case "1":
                    // Starts a new game.
                    StartNewGame();
                    break;
                case "2":
                    // Loads an existing game.
                    LoadGame();
                    break;
                case "3":
                    // Shows help information.
                    ShowHelp();
                    break;
                case "4":
                    // Exits the menu.
                    running = false;
                    break;
                case "1337":
                    // Easter egg for fun.
                    Console.WriteLine("🎅 Ho ho ho! 🎅");
                    break;
                case "666":
                    // Easter egg for fun.
                    Console.WriteLine("👹 The number of the beast! 👹");
                    break;
                case "20041225":
                    // Godmode cheat code.
                    player.Godmode = true;
                    player.MaxHealth = 999;
                    player.Health = 999;
                    player.Damage = 999;
                    player.Defense = 999;
                    player.MaxInventory = 999;
                    player.Inventory.Add(new Item("🔥 God Sword", "A legendary sword that can slay anything!"));
                    Console.WriteLine("💪 God mode activated! 💪");
                    break;
                default:
                    // Informs the user of an invalid choice.
                    Console.WriteLine("Ogiltigt val!");
                    break;
            }
        }
    }
}

/// <summary>
/// Command to move the player in the game.
/// </summary>
public class MoveCommand : ICommandHandler
{
    // The current state of the game.
    // This is where the player's position and the game world are stored.
    private readonly GameState _gameState;

    /// <summary>
    /// Initializes a new instance of the <see cref="MoveCommand"/> class.
    /// </summary>
    /// <param name="gameState">The current state of the game.</param>
    public MoveCommand(GameState gameState)
    {
        _gameState = gameState;
    }

    /// <summary>
    /// Executes the move command with the given arguments.
    /// </summary>
    /// <param name="args">The arguments for the move command.</param>
    /// <returns>A string result of the move command execution.</returns>
    public string Execute(string[] args)
    {
        if (args.Length < 2)
            return "Vart vill du gå?"; // "Where do you want to go?" in Swedish

        var direction = args[1].ToLower();
        return _gameState.MovePlayer(direction);
    }
}
```

_Fortsättning följer i [Del 3: Avancerad Funktionalitet](part_3.md)..._
