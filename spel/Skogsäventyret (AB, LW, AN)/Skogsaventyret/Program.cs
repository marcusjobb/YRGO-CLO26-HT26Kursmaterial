class Program
{
    static void Main()
    {

        // Stores all available monster types in one list
        List<Monster> monsters = new List<Monster>
        {
            new Teddybear(), // creates a Teddybear
            new GrumpyTeddybear(),  // creates a GrumpyTeddybear
            new GiantTeddybear()   // creates a GiantTeddybear
        };
        Random random = new Random();

        Console.Clear();
        Console.WriteLine("==========================================");
        Console.WriteLine("                KRAMSKOGEN                ");
        Console.WriteLine("==========================================");
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("   En gammal skylt står vid skogskanten:  ");
        Console.WriteLine();
        Console.WriteLine(" +---------------------------------------+");
        Console.WriteLine(" |                VARNING                |");
        Console.WriteLine(" |          KRAMA INTE TEDDYBEARS!       |");
        Console.WriteLine(" +---------------------------------------+");


        Console.WriteLine();
        Console.WriteLine("Du är en modig hjälte.");
        Console.WriteLine("Tre mystiska teddybjörnar bor inne i skogen.");
        Console.WriteLine("Ingen vet riktigt vad som händer där inne...");
        Console.WriteLine();
        Console.WriteLine("Ingen vet vad som väntar där inne...");
        Console.WriteLine();

        // Examplecode for taking a input from user
        //feel free to change, might need to place some code into other classes and refactor code


        string inputName;

        do
        {
            Console.Write("Vad heter du, hjälte? > ");
            inputName = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(inputName) ||
                !inputName.All(c => char.IsLetter(c) || c == ' '))
            {
                Console.WriteLine();
                Console.WriteLine("Ogiltigt namn. Använd bara bokstäver.");
                Console.WriteLine();
            }

        } while (string.IsNullOrWhiteSpace(inputName) ||
                 !inputName.All(c => char.IsLetter(c) || c == ' '));

        Player createdPlayer = new Player(inputName);

        //as long as user doesnt put in an correct name they has to re-entrered
        //takes a input from user "name" and saves it into "name" in constrcuctor in class Player.cs

        Console.WriteLine();
        Console.WriteLine("==========================================");
        Console.WriteLine($"Hjälten {inputName} är redo!");
        Console.WriteLine("==========================================");
        Console.WriteLine();

        Console.WriteLine("Tryck ENTER för att börja äventyret...");
        Console.ReadLine();

        bool gameRunning = true;

        while (gameRunning && createdPlayer.Hp > 0)
        {
            Console.Clear();

            //If name has a blank space or numbers the users has to chose another name

            Console.WriteLine("==========================================");
            Console.WriteLine("              VAD VILL DU GÖRA?           ");
            Console.WriteLine("==========================================");
            Console.WriteLine();

            Console.WriteLine($"Hjälte: {createdPlayer.Name}");
            Console.WriteLine($"HP: {createdPlayer.Hp}/{createdPlayer.MaxHp}");
            Console.WriteLine($"Level: {createdPlayer.Level}   XP: {createdPlayer.Xp}");
            Console.WriteLine();

            Console.WriteLine("1. Utforska kramskogen");
            Console.WriteLine("2. Vila och hela dig");
            Console.WriteLine("3. Arenan");
            Console.WriteLine("4. Visa hjältestatus");
            Console.WriteLine("5. Avsluta");

            Console.WriteLine();

            int choice = ReadChoice(1, 5);

            switch (choice)
            {
                case 1:

                    Console.Clear();

                    // A random monster is selected when the player enters the forest
                    Monster currentMonster = monsters[random.Next(monsters.Count)];

                    Console.WriteLine("=====================================================");
                    Console.WriteLine("DU GÅR IN I KRAMSKOGEN....Du hör ett konstigt ljud...");
                    Console.WriteLine("=====================================================");
                    Console.WriteLine();
                    Console.WriteLine("Plötsligt dyker en teddybjörn upp!");
                    Console.WriteLine();

                    Console.WriteLine($"Det är en {currentMonster.Name}!");
                    Console.WriteLine($"Björnens HP: {currentMonster.Hp}");
                    Console.WriteLine();

                    Fight(createdPlayer, currentMonster);

                    break;

                case 2:

                    Console.Clear();

                    Console.WriteLine("==========================================");
                    Console.WriteLine("                  VILA                    ");
                    Console.WriteLine("==========================================");
                    Console.WriteLine();
                    Console.WriteLine("Du hittar en mysig plats i skogen.");
                    Console.WriteLine("Du vilar en stund och återhämtar dig.");
                    Console.WriteLine();

                    createdPlayer.Heal();
                    createdPlayer.PassDay();

                    break;

                case 3:

                    Console.Clear();

                    Console.WriteLine("==========================================");
                    Console.WriteLine("                 ARENAN                   ");
                    Console.WriteLine("==========================================");
                    Console.WriteLine();
                    Console.WriteLine("Alla tre teddybjörnar väntar på dig!");
                    Console.WriteLine("Du måste besegra dem en efter en.");
                    Console.WriteLine();

                    for (int i = 0; i < monsters.Count; i++)
                    {
                        Console.WriteLine(
                            $"{i + 1}. {monsters[i].Name} - {monsters[i].Hp} HP"
                        );
                    }

                    Console.WriteLine();
                    Console.WriteLine("Tryck ENTER för att börja arenan...");
                    Console.ReadLine();

                    for (int i = 0; i < monsters.Count; i++)
                    {
                        if (createdPlayer.Hp <= 0)
                        {
                            break;
                        }

                        Monster arenaMonster;

                        if (i == 0)
                        {
                            arenaMonster = new Teddybear();
                        }
                        else if (i == 1)
                        {
                            arenaMonster = new GrumpyTeddybear();
                        }
                        else
                        {
                            arenaMonster = new GiantTeddybear();
                        }

                        Console.Clear();

                        Console.WriteLine("==========================================");
                        Console.WriteLine($"                 ROND {i + 1}");
                        Console.WriteLine("==========================================");
                        Console.WriteLine();
                        Console.WriteLine($"En {arenaMonster.Name} står framför dig!");
                        Console.WriteLine($"Björnens HP: {arenaMonster.Hp}");
                        Console.WriteLine();

                        Fight(createdPlayer, arenaMonster);
                    }

                    if (createdPlayer.Hp > 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("==========================================");
                        Console.WriteLine("          DU VANN ARENAN!");
                        Console.WriteLine("==========================================");
                        Console.WriteLine("Alla tre teddybjörnar är besegrade!");

                        createdPlayer.GainXp(20);
                    }

                    break;

                case 4:

                    Console.Clear();

                    Console.WriteLine("==========================================");
                    Console.WriteLine("             HJÄLTESTATUS");
                    Console.WriteLine("==========================================");
                    Console.WriteLine();

                    Console.WriteLine(createdPlayer);
                    Console.WriteLine($"Attack: {createdPlayer.Attack}");
                    Console.WriteLine($"Försvar: {createdPlayer.Defence}");

                    break;

                case 5:

                    gameRunning = false;

                    Console.WriteLine();
                    Console.WriteLine("Du lämnar Kramskogen. Tack för äventyret!");

                    break;
            }

            if (gameRunning && createdPlayer.Hp > 0)
            {
                Console.WriteLine();
                Console.WriteLine("Tryck ENTER för att fortsätta...");
                Console.ReadLine();
            }
        }

        if (createdPlayer.Hp <= 0)
        {
            Console.Clear();

            Console.WriteLine("==========================================");
            Console.WriteLine("                 GAME OVER");
            Console.WriteLine("==========================================");
            Console.WriteLine();

            Console.WriteLine($"{createdPlayer.Name} orkade inte mer.");
            Console.WriteLine($"Du överlevde {createdPlayer.DaysAlive} dagar.");
        }

        Console.WriteLine();
        Console.WriteLine("Tryck ENTER för att avsluta...");
        Console.ReadLine();
    }


    static void Fight(Player player, Monster monster)
    {
        while (player.Hp > 0 && monster.Hp > 0)
        {
            Console.WriteLine();
            Console.WriteLine($"Din HP: {player.Hp}/{player.MaxHp}");
            Console.WriteLine($"{monster.Name}s HP: {monster.Hp}");
            Console.WriteLine();

            Console.WriteLine("1. Säg något elakt");
            Console.WriteLine("2. Försvara dig");
            Console.WriteLine("3. Spring");

            Console.WriteLine();

            int fightChoice = ReadChoice(1, 3);

            switch (fightChoice)
            {
                case 1:

                    Console.WriteLine();
                    Console.WriteLine(
                        $"Du säger något riktigt elakt till {monster.Name}!"
                    );

                    Console.WriteLine($"Du gör {player.Attack} skada.");

                    bool monsterDefeated = monster.TakeDamage(player.Attack);

                    if (monsterDefeated)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"{monster.Name} blev besegrad!");

                        player.GainXp(monster.XpGain);

                        break;
                    }

                    Console.WriteLine();
                    Console.WriteLine($"{monster.Name} attackerar!");

                    bool playerDefeated =
                        player.TakeDamage(monster.Attack);

                    Console.WriteLine(
                        $"Du har {player.Hp}/{player.MaxHp} HP kvar."
                    );

                    if (playerDefeated)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Du blev besegrad!");
                    }

                    break;


                case 2:

                    Console.WriteLine();
                    Console.WriteLine("Du försvarar dig!");
                    Console.WriteLine(
                        "Du håller upp garden och tar mindre skada."
                    );

                    player.TakeDamage(monster.Attack / 2);

                    Console.WriteLine(
                        $"{monster.Name} attackerar försiktigt!"
                    );

                    Console.WriteLine(
                        $"Du har {player.Hp}/{player.MaxHp} HP kvar."
                    );

                    break;


                case 3:

                    Console.WriteLine();
                    Console.WriteLine("Du springer iväg!");
                    Console.WriteLine(
                        "Ibland är det smartaste valet att fly."
                    );

                    break;
            }

            if (fightChoice == 3)
            {
                break;
            }
        }

    }


    static int ReadChoice(int min, int max)
    {
        int choice;

        while (!int.TryParse(Console.ReadLine(), out choice) || choice < min || choice > max)

        {
            Console.WriteLine($"Skriv ett tal mellan {min} och {max}.");
        }
        return choice;
    }
}