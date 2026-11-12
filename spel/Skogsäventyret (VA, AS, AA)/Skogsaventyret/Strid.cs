using System;

namespace Skogsaventyret
{
    // Klass som hanterar striden mellan spelaren och monstret.
    public class Strid
    {
        private Spelare spelare;
        private Monster monster;

        private Random slumpgenerator = new Random();

        public Strid(Spelare spelare, Monster monster)
        {
            this.spelare = spelare;
            this.monster = monster;
        }

        // Kör själva striden.
        public bool Kör()
        {
            while (spelare.Hp > 0 && monster.Hp > 0)
            {
                Console.WriteLine("\n--- STRID ---");

                Console.WriteLine(
                    $"{spelare.Namn}: {spelare.Hp}/{spelare.MaxHp} HP"
                );

                Console.WriteLine(
                    $"{monster.Namn}: {monster.Hp} HP"
                );

                Console.WriteLine("\nVad vill du göra?");
                Console.WriteLine("1) Försvara");
                Console.WriteLine("2) Anfall");
                Console.WriteLine("3) Spring");

                string val = Console.ReadLine() ?? "";

                switch (val)
                {
                    case "1":
                        Försvara();
                        break;

                    case "2":
                        Anfall();
                        break;

                    case "3":
                        Spring();
                        return false;

                    default:
                        Console.WriteLine("Välj 1, 2 eller 3.");
                        continue;
                }
            }

            return spelare.Hp > 0;
        }

        // Försvara: skadan halveras.
        private void Försvara()
        {
            int skada = monster.Attack / 2;

            spelare.TakeDamage(skada);

            Console.WriteLine(
                $"{spelare.Namn} försvarar sig och tar {skada} skada!"
            );

            VisaHp();
        }

        // Anfall: spelarens attack minus monstrets försvar.
        private void Anfall()
        {
            int skada = spelare.Attack - monster.Forsvar;

            if (skada < 1)
            {
                skada = 1;
            }

            monster.TaSkada(skada);

            Console.WriteLine(
                $"{spelare.Namn} anfaller och gör {skada} skada!"
            );

            // Monstret attackerar tillbaka om det lever.
            if (monster.ÄrVidLiv)
            {
                spelare.TakeDamage(monster.Attack);

                Console.WriteLine(
                    $"{monster.Namn} attackerar tillbaka och gör " +
                    $"{monster.Attack} skada!"
                );
            }

            VisaHp();
        }

        // Spring: spelaren tar slumpmässig skada.
        private void Spring()
        {
            int skada = slumpgenerator.Next(
                1,
                monster.Attack + 1
            );

            spelare.TakeDamage(skada);

            Console.WriteLine(
                $"{spelare.Namn} springer iväg men tar {skada} skada!"
            );

            VisaHp();
        }

        // Visar HP.
        private void VisaHp()
        {
            Console.WriteLine(
                $"HP kvar: {spelare.Namn}: {spelare.Hp} | " +
                $"{monster.Namn}: {monster.Hp}"
            );
        }
    }
}