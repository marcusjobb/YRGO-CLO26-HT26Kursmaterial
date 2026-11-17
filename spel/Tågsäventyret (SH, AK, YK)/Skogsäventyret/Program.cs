namespace Tågäventyret;

public class Program
{
    public static void Main()
    {
        // Gör att spelaren kan starta om spelet efter game over
        while (true)
        {
            Console.Clear();

            Console.WriteLine("================================");
            Console.WriteLine("          TÅGÄVENTYRET");
            Console.WriteLine("================================");
            Console.WriteLine();
            Console.WriteLine("Du vaknar på tåget.");
            Console.WriteLine("Du är säker i den här vagnen. Vad vill du göra?");
            Console.WriteLine();
            Console.WriteLine("1. Starta spel");
            Console.WriteLine("2. Avsluta");
            Console.WriteLine();

            Console.Write("Välj: ");
            string choice = Console.ReadLine() ?? "";

            // Användarens val hanteras med if-satser
            if (choice == "1")
            {
                StartaSpel();
            }
            else if (choice == "2")
            {
                Console.Clear();
                Console.WriteLine("Tack för att du spelade Tågäventyret!");
                break;
            }
            else
            {
                Console.WriteLine("Fel val. Välj 1 eller 2.");
                Console.ReadLine();
            }
        }
    }

    static void StartaSpel()
    {
        Console.Clear();

        // Spelaren får skriva in sitt namn
        Console.Write("Skriv ditt namn: ");
        string namn = Console.ReadLine() ?? "";

        Player player = new Player(namn);
        Battle battle = new Battle();

        // Händelser sparas i en lista
        List<string> händelselogg = new List<string>();

        händelselogg.Add(
            "Du vaknar på tåget. Något väntar i nästa vagn..."
        );

        Random random = new Random();

        // Spelet fortsätter tills spelaren dör
        while (player.Hp > 0)
        {
            // Nya monster skapas varje dag
            List<Zombie> zombies = new List<Zombie>
            {
                new Walker(),
                new Runner(),
                new Mutant()
            };

            Console.Clear();

            Console.WriteLine("================================");
            Console.WriteLine("          TÅGÄVENTYRET");
            Console.WriteLine("================================");
            Console.WriteLine();

            Console.WriteLine($"Dag: {player.Dagar}");
            Console.WriteLine($"Nivå: {player.Level}");
            Console.WriteLine($"XP: {player.Xp}");
            Console.WriteLine($"Attack: {player.Attack}");
            Console.WriteLine($"Försvar: {player.Forsvar}");
            Console.WriteLine($"HP: {player.Hp}/{player.MaxHp}");

            Console.WriteLine();
            Console.WriteLine("Tåget");
            Console.WriteLine();
            Console.WriteLine("Du är säker i den här vagnen. Vad vill du göra?");
            Console.WriteLine();
            Console.WriteLine("1. Gå till nästa vagn (1 dag)");
            Console.WriteLine("2. Vila och hela (1 dag)");
            Console.WriteLine();

            Console.WriteLine("Händelselogg");

            // Visar alla tidigare händelser
            foreach (string händelse in händelselogg)
            {
                Console.WriteLine(händelse);
            }

            Console.WriteLine();
            Console.Write("Välj: ");

            string choice = Console.ReadLine() ?? "";

            // Spelaren går vidare och en dag går
            if (choice == "1")
            {
                player.ÖkaDag();

                // Ett slumpmässigt monster väljs från listan
                Zombie zombie = zombies[random.Next(zombies.Count)];

                händelselogg.Add(
                    $"Dag {player.Dagar}: Du går till nästa vagn."
                );

                händelselogg.Add(
                    $"Du möter {zombie.Name}!"
                );

                // Startar striden
                battle.StartBattle(player, zombie);

                // Sparar resultatet i händelseloggen
                if (zombie.Hp <= 0)
                {
                    händelselogg.Add(
                        $"{zombie.Name} besegrades! +{zombie.XpBeloning} XP."
                    );
                }

                Console.WriteLine();
                Console.WriteLine("Tryck ENTER för att fortsätta.");
                Console.ReadLine();
            }

            // Spelaren kan vila och återställa HP
            else if (choice == "2")
            {
                // Vila är bara möjligt från nivå 3
                if (player.Level >= 3)
                {
                    // Heal återställer HP och ökar dagen med 1
                    player.Heal();

                    händelselogg.Add(
                        $"Dag {player.Dagar}: Du vilade och återställde ditt HP."
                    );

                    Console.WriteLine();
                    Console.WriteLine("Du vilar och återfår ditt HP.");
                    Console.WriteLine($"HP: {player.Hp}/{player.MaxHp}");

                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Du måste nå nivå 3 för att vila.");
                    Console.WriteLine("Du kan inte vila ännu.");

                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Fel val. Välj 1 eller 2.");
                Console.ReadLine();
            }
        }

        // När spelaren dör visas game over
        Console.Clear();

        Console.WriteLine("================================");
        Console.WriteLine("           GAME OVER");
        Console.WriteLine("================================");
        Console.WriteLine();

        Console.WriteLine("Du överlevde inte tågets resa.");
        Console.WriteLine();

        // Poängtavla med spelarens resultat
        Console.WriteLine("Poängtavla");
        Console.WriteLine();
        Console.WriteLine($"Dagar överlevda: {player.Dagar}");
        Console.WriteLine($"Nivå uppnådd: {player.Level}");
        Console.WriteLine($"Total XP: {player.Xp}");

        Console.WriteLine();
        Console.WriteLine("Händelselogg");

        // Visar hela händelseloggen efter game over
        foreach (string händelse in händelselogg)
        {
            Console.WriteLine(händelse);
        }

        Console.WriteLine();
        Console.WriteLine("Tryck ENTER för att gå tillbaka till startsidan.");

        Console.ReadLine();
    }
}
