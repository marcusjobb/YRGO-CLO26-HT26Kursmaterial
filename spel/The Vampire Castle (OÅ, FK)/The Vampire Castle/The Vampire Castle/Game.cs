class Game
{
    private Player player;
    private Room room7;
    private bool playing;

    public Game()
    {
        CreateWorld();
        playing = true;
    }

    public void CreateWorld()
    {
        Room hallway = new Room(
            "Hallway",
            "A cold hallway. A free key sits on the floor.",
            null
        );

        Room room1 = new Room(
            "Room 1: Dracula's Chamber",
            "A damp stone chamber.",
            new Vampire("Dracula", "What is 3 + 4?", 7)
        );

        Room room2 = new Room(
            "Room 2: Carmilla's Room",
            "Cobwebs cover the corners.",
            new Vampire("Carmilla", "What is 9 - 2?", 7)
        );

        Room room3 = new Room(
            "Room 3: Lestat's Room",
            "Candles flicker on the walls.",
            new Vampire("Lestat", "What is 6 * 2?", 12)
        );

        Room room4 = new Room(
            "Room 4: Damon's Room",
            "A cold draft passes through.",
            new Vampire("Damon", "What is 20 / 4?", 5)
        );

        Room room5 = new Room(
            "Room 5: Stefan's Room",
            "Bones are scattered on the floor.",
            new Vampire("Stefan", "What is 8 + 8?", 16)
        );

        Room room6 = new Room(
            "Room 6: Elena's Room",
            "A locked cabinet stands in the corner.",
            new Vampire("Elena", "What is 10 * 10?", 100)
        );

        room7 = new Room(
            "Room 7: The Rescue Room",
            "Your friend lies weakly against the wall.",
            null
        );

        // Connect rooms

        hallway.AddExit("north", room1);

        room1.AddExit("south", hallway);
        room1.AddExit("north", room2);

        room2.AddExit("south", room1);
        room2.AddExit("north", room3);

        room3.AddExit("south", room2);
        room3.AddExit("north", room4);

        room4.AddExit("south", room3);
        room4.AddExit("north", room5);

        room5.AddExit("south", room4);
        room5.AddExit("north", room6);

        room6.AddExit("south", room5);
        room6.AddExit("north", room7);

        room7.AddExit("south", room6);

        // Items

        Item rustyKey = new Item(
            "Rusty Key",
            "An old rusty key."
        );

        Item bronzeKey = new Item(
            "Bronze Key",
            "A small bronze key."
        );

        Item ironKey = new Item(
            "Iron Key",
            "A heavy iron key."
        );

        Item silverKey = new Item(
            "Silver Key",
            "A shiny silver key."
        );

        Item goldenKey = new Item(
            "Golden Key",
            "A valuable golden key."
        );

        Item antidote = new Item(
            "Antidote",
            "Medicine that can save your friend."
        );

        hallway.AddItem(rustyKey);
        room1.AddItem(bronzeKey);
        room2.AddItem(ironKey);
        room3.AddItem(silverKey);
        room4.AddItem(goldenKey);
        room6.AddItem(antidote);

        // NPCs

        NPC ghost = new NPC(
            "Ghost",
            new List<string>
            {
                "I have wandered here for centuries...",
                "The cold bites at my legs, even though I have none.",
                "Have you seen my missing hand?"
            }
        );

        NPC prisoner = new NPC(
            "Prisoner",
            new List<string>
            {
                "They locked me in here long ago. I don't remember why.",
                "The guards stopped coming years ago.",
                "Do you have food? No? ...I thought not."
            }
        );

        hallway.AddNpc(ghost);
        room4.AddNpc(prisoner);

        player = new Player(hallway);
    }


    private static void InitTheme()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.Title = "Vampire Castle";
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Clear();
    }

  
    private static void Say(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ForegroundColor = ConsoleColor.Gray;
    }


    private static void Label(string label, string value)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write(label + " ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(value);
        Console.ForegroundColor = ConsoleColor.Gray;
    }


    private static void Prompt(string text)
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.Write(text + " ");
        Console.ForegroundColor = ConsoleColor.White;
    }

  
    private static void Box(string text, ConsoleColor color)
    {
        int width = Math.Max(text.Length + 6, 36);
        int left = (width - text.Length) / 2;
        string middle = new string(' ', left) + text;

        Console.ForegroundColor = color;
        Console.WriteLine("╔" + new string('═', width) + "╗");
        Console.WriteLine("║" + middle.PadRight(width) + "║");
        Console.WriteLine("╚" + new string('═', width) + "╝");
        Console.ForegroundColor = ConsoleColor.Gray;
    }

   
    private static void VampireFace()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(@"        .-------.");
        Console.WriteLine(@"       /  _   _  \");
        Console.WriteLine(@"      |  (o) (o)  |");
        Console.WriteLine(@"      |     ^     |");
        Console.WriteLine(@"      |   \/^^\/  |");
        Console.WriteLine(@"       \   '--'  /");
        Console.WriteLine(@"        '-.___.-'");
        Console.ForegroundColor = ConsoleColor.Gray;
    }

 //GAME

    public void Start()
    {
        InitTheme();

        Box("V A M P I R E   C A S T L E", ConsoleColor.Red);
        Console.WriteLine();

        Say(
            "Your friend is dying. Find the antidote and bring it to Room 7.",
            ConsoleColor.Gray
        );

        Console.WriteLine();
        Say("Type 'help' to see the available commands.", ConsoleColor.DarkGray);
        Console.WriteLine();

        while (playing)
        {
            ShowRoom();

            if (player.CurrentRoom.Guard != null &&
                !player.CurrentRoom.GuardDefeated)
            {
                FightVampire();
            }

            if (!playing)
            {
                break;
            }

            CheckWin();

            if (!playing)
            {
                break;
            }

            Prompt(">");
            string input = Console.ReadLine();

            InterpretCommand(input);

            Console.WriteLine();
        }
    }

    private void ShowRoom()
    {
        Console.WriteLine();
        Box(player.CurrentRoom.Name, ConsoleColor.DarkRed);
        Say(player.CurrentRoom.Description, ConsoleColor.Gray);

        Label("Exits:", string.Join(", ", player.CurrentRoom.Exits.Keys));

        if (player.CurrentRoom.Items.Count > 0)
        {
            List<string> itemNames = new List<string>();

            foreach (Item item in player.CurrentRoom.Items)
            {
                itemNames.Add(item.Name);
            }

            Label("Items:", string.Join(", ", itemNames));
        }

        if (player.CurrentRoom.Npcs.Count > 0)
        {
            List<string> npcNames = new List<string>();

            foreach (NPC npc in player.CurrentRoom.Npcs)
            {
                npcNames.Add(npc.Name);
            }

            Label("People here:", string.Join(", ", npcNames));
        }
    }

    private void FightVampire()
    {
        Vampire vampire = player.CurrentRoom.Guard;

        Console.WriteLine();
        VampireFace();
        Say(vampire.Name + " blocks your way!", ConsoleColor.Red);

        bool passed = false;

        while (!passed)
        {
            Say(vampire.Question, ConsoleColor.Yellow);
            Prompt("Answer:");

            string typed = Console.ReadLine();
            int guess;

            if (!int.TryParse(typed, out guess))
            {
                Say("Type a number.", ConsoleColor.DarkGray);
                continue;
            }

            if (vampire.AskQuestion(guess))
            {
                Say(
                    vampire.Name + ": Correct. You may pass.",
                    ConsoleColor.Green
                );

                player.CurrentRoom.DefeatGuard();
                passed = true;
            }
            else
            {
                Say(
                    vampire.Name + ": Wrong! Try again.",
                    ConsoleColor.Red
                );
            }
        }
    }

    private void CheckWin()
    {
        if (player.CurrentRoom == room7)
        {
            Console.WriteLine();

            if (player.HasItem("Antidote"))
            {
                Say(
                    "You give the antidote to your friend.",
                    ConsoleColor.Gray
                );

                Say(
                    "They recover. You win!",
                    ConsoleColor.Green
                );

                Console.WriteLine();
                Box("YOUR FRIEND IS SAVED!", ConsoleColor.Green);
            }
            else
            {
                Say(
                    "You have nothing to give your friend...",
                    ConsoleColor.Gray
                );

                Say(
                    "Game over.",
                    ConsoleColor.Red
                );

                Console.WriteLine();
                Box("G A M E   O V E R", ConsoleColor.Red);
            }

            playing = false;
        }
    }

    public void InterpretCommand(string input)
    {
        if (input == "inventory")
        {
            player.ShowInventory();
        }
        else if (input == "look")
        {
            ShowRoom();
        }
        else if (input == "help")
        {
            ShowHelp();
        }
        else if (input == "quit")
        {
            Say("The game ends. Goodbye!", ConsoleColor.DarkGray);
            playing = false;
        }
        else if (input == "go north")
        {
            Move("north");
        }
        else if (input == "go south")
        {
            Move("south");
        }
        else if (input == "go east")
        {
            Move("east");
        }
        else if (input == "go west")
        {
            Move("west");
        }
        else if (input == "take")
        {
            TakeItem();
        }
        else if (input == "talk")
        {
            TalkToNpc();
        }
        else
        {
            Say("I don't understand that command.", ConsoleColor.DarkGray);
        }
    }

    private void Move(string direction)
    {
        if (player.CurrentRoom.Exits.ContainsKey(direction))
        {
            Room nextRoom = player.CurrentRoom.Exits[direction];

            player.MoveTo(nextRoom);
        }
        else
        {
            Say("You can't go that way.", ConsoleColor.DarkGray);
        }
    }

    private void TakeItem()
    {
        if (player.CurrentRoom.Items.Count == 0)
        {
            Say("There are no items here.", ConsoleColor.DarkGray);
            return;
        }

        Say("What do you want to take?", ConsoleColor.Gray);

        foreach (Item item in player.CurrentRoom.Items)
        {
            Say("- " + item.Name, ConsoleColor.White);
        }

        Prompt(">");
        string itemName = Console.ReadLine();

        Item foundItem = null;

        foreach (Item item in player.CurrentRoom.Items)
        {
            if (item.Name == itemName)
            {
                foundItem = item;
            }
        }

        if (foundItem != null)
        {
            player.TakeItem(foundItem);

            Say(
                "You take the " + foundItem.Name + ".",
                ConsoleColor.Green
            );
        }
        else
        {
            Say("That item is not here.", ConsoleColor.DarkGray);
        }
    }

    private void TalkToNpc()
    {
        if (player.CurrentRoom.Npcs.Count == 0)
        {
            Say("There is nobody here to talk to.", ConsoleColor.DarkGray);
            return;
        }

        Say("Who do you want to talk to?", ConsoleColor.Gray);

        foreach (NPC npc in player.CurrentRoom.Npcs)
        {
            Say("- " + npc.Name, ConsoleColor.White);
        }

        Prompt(">");
        string name = Console.ReadLine();

        NPC foundNpc = null;

        foreach (NPC npc in player.CurrentRoom.Npcs)
        {
            if (npc.Name == name)
            {
                foundNpc = npc;
            }
        }

        if (foundNpc != null)
        {
            Say(
                foundNpc.Name + ": " + foundNpc.NextLine(),
                ConsoleColor.Cyan
            );
        }
        else
        {
            Say("That person is not here.", ConsoleColor.DarkGray);
        }
    }

    private void ShowHelp()
    {
        Console.WriteLine();
        Say("Available commands:", ConsoleColor.Yellow);
        Say("go north", ConsoleColor.DarkYellow);
        Say("go south", ConsoleColor.DarkYellow);
        Say("go east", ConsoleColor.DarkYellow);
        Say("go west", ConsoleColor.DarkYellow);
        Say("take", ConsoleColor.DarkYellow);
        Say("look", ConsoleColor.DarkYellow);
        Say("inventory", ConsoleColor.DarkYellow);
        Say("talk", ConsoleColor.DarkYellow);
        Say("help", ConsoleColor.DarkYellow);
        Say("quit", ConsoleColor.DarkYellow);
    }
}