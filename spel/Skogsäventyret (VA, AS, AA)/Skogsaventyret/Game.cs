using System;

namespace Skogsaventyret
{
    // Public class Game som interagerar med resten av projektet.
    public class Game
    {
        // Lista med platser i Göteborg som ska slumpas fram varje gång spelaren är ute på äventyr.
        private static string[] Platser =
        {
            "Du strosar runt i Majorna",
            "Du tar en promenad i Tingstadsvass",
            "Du beundrar landskapet i Partille",
            "Du promenerar på Linnégatan",
            "Du vandrar genom Nordstan",
            "Du promenerar genom Biskopsgården"
        };

        // Slumpgenerator.
        private Random slumpgenerator = new Random();

        // Fältdeklarationer.
        private Spelare spelare = null!;
        private bool spelPågår;
        private int dag;

        // Sätter upp startläget innan spelet börjar.
        public Game()
        {
            dag = 1;
            spelPågår = true;
        }

        // Startar spelet.
        public void Starta()
        {
            Console.WriteLine("Hallå eller! Välkommen till GBG.");
            Console.Write("Vad heter du?: ");

            string namn = Console.ReadLine() ?? "";

            spelare = new Spelare(namn);

            while (spelPågår)
            {
                Promenad();
            }

            Console.WriteLine("\n--- GAME OVER ---");
            Console.WriteLine($"Dagar överlevda: {spelare.DagarÖverlevda}");
            Console.WriteLine($"Level: {spelare.Level}");
            Console.WriteLine($"Total XP: {spelare.Xp}");
        }

        // Kör en dag i spelet.
        private void Promenad()
        {
            Console.WriteLine($"\n--- Dag {dag} ---");
            Console.WriteLine(
                $"{spelare.Namn} | HP: {spelare.Hp}/{spelare.MaxHp} | " +
                $"Level: {spelare.Level} | XP: {spelare.Xp}"
            );

            Console.WriteLine("Vad vill du göra?");
            Console.WriteLine("1) Ta en Öl i Kvillebäcken");
            Console.WriteLine("2) Strosa på stan (äventyra)");

            string val = Console.ReadLine() ?? "";

            switch (val)
            {
                case "1":
                    Vila();
                    break;

                case "2":
                    Äventyra();
                    break;

                default:
                    Console.WriteLine("Välj 1 eller 2.");
                    return;
            }

            // Om spelaren dog under striden är spelet slut.
            if (spelare.Hp <= 0)
            {
                Console.WriteLine(
                    $"{spelare.Namn} klarade inte av trycket... Game over gubben."
                );

                spelPågår = false;
                return;
            }

            // Nästa dag.
            dag++;

            // Spelaren har överlevt dagen.
            spelare.ÖverlevDag();
        }

        // Spelaren vilar och återställer HP till max.
        private void Vila()
        {
            Console.WriteLine(
                $"{spelare.Namn} sover ut hemma i Vassen och känner sig pigg."
            );

            spelare.Heal();
        }

        // Spelaren ger sig ut på äventyr.
        private void Äventyra()
        {
            // Slumpar fram en plats.
            string plats = Platser[slumpgenerator.Next(Platser.Length)];

            Console.WriteLine($"{plats}...");

            // Skapar ett slumpmässigt monster.
            Monster monster = Monsterfabrik.SkapaSlumpatMonster();

            Console.WriteLine(
                $"En {monster.Namn} dyker upp! {monster.Beskrivning}"
            );

            // Startar striden.
            Strid strid = new Strid(spelare, monster);

            bool spelarenVann = strid.Kör();

            // Om spelaren vann får den XP.
            if (spelarenVann)
            {
                Console.WriteLine(
                    $"{spelare.Namn} vann och fick {monster.XpBelöning} XP!"
                );

                spelare.FåXp(monster.XpBelöning);
            }
        }
    }
}