using System;
using System.Collections.Generic;
using System.Globalization;

namespace schaktet_fangelse_platform
{
    class Program
    {

        static void Main(string[] args)
        {

            // Här skapar vi spelaren
            Console.WriteLine("Välkommen till schaktet!");
            Console.Write("Skriv ditt namn: ");
            string namn = Console.ReadLine();
            Fange fange = new Fange(namn);
            SvartaMarknaden marknad = new SvartaMarknaden();
            


            Random rnd = new Random();

            // Huvudloop som körs till spelaren dör
            while (fange.HP > 0)
            {
                Console.WriteLine("Vad vill du göra idag?");
                Console.WriteLine("1) Vänta på plattformen");
                Console.WriteLine("2) Vila och Läka");
                Console.WriteLine("3) Besök den Svarta Marknaden");
                Console.WriteLine("4) Åk ner till Arenan");
                int val = int.Parse(Console.ReadLine());
                int Level = fange.Level;

                if (val == 1)
                {

                    List<Medfange> monsterTyper = new List<Medfange>
                    {
                        new Utsvulten(),
                        new Hamstraren(),
                        new Bodeln()

                    };

                    Medfange monster = monsterTyper[rnd.Next(0, monsterTyper.Count)];

                    bool striden = true;
                    while (striden == true)
                    {
                        Console.WriteLine($"{monster.Namn} har {monster.HP}HP");
                        Console.WriteLine();
                        Console.WriteLine($"Du har {fange.HP}HP");
                        Console.WriteLine();

                        Console.WriteLine("1) Försvara dig");
                        Console.WriteLine("2) Anfall");
                        Console.WriteLine("3) Spring");
                        int val2 = int.Parse(Console.ReadLine());

                        bool monsterDog = false;
                        bool fangeDog = false;

                        int monsterSkada = monster.Attack - fange.Forsvar;
                        if (monsterSkada < 1)
                        {
                            monsterSkada = 1;
                        }


                        if (val2 == 1) //Försvara
                        {
                            int skada = monsterSkada / 2;
                            if (skada < 1)
                            {
                                skada = 1;
                            }

                            fangeDog = fange.TakeDamage(skada);

                        }
                        else if (val2 == 2) //Anfall
                        {
                            int skada = fange.Attack - monster.Forsvar;
                            if (skada < 1)
                            {
                                skada = 1;
                            }

                            monsterDog = monster.TakeDamage(skada);

                            if (monsterDog == false)
                            {
                                fangeDog = fange.TakeDamage(monsterSkada);
                            }

                        }
                        else if (val2 == 3) // Spring
                        {
                            int skada = rnd.Next(1, monster.Attack + 1);

                            fangeDog = fange.TakeDamage(skada);

                            if (fangeDog == false)
                            {
                                Console.WriteLine("Du flydde!");
                                striden = false;
                            }
                        }
                        if (monsterDog)
                        {

                            Console.WriteLine($"{monster.Namn} blev besegrad. ");

                            fange.GainXP(monster.XPBeloning);

                            int guld = monster.TappaGuld();
                            fange.LaggTillGuld(guld);

                            striden = false;  
                        }
                        if (fangeDog)
                        {
                            Console.WriteLine($"{fange.Namn} dog\nGAME OVER");
                            striden = false;
                        }
                    }


                    if (fange.HP > 0)
                    {
                        fange.NyDag();

                    }
                }

                else if (val == 2)
                {
                    fange.Heal();
                    fange.NyDag();
                }

                else if (val == 3) // Svarta Marknaden
                {
                    bool svartMarknad = true;
                    if (Level >= 0)
                    {
                       
                            Console.WriteLine("Besöker Svarta Marknaden...");
                            Console.WriteLine();
                             while (svartMarknad)
                        {
                            marknad.VisaSortiment();
                            int köp = int.Parse(Console.ReadLine());
                            Metallror metallror = new Metallror();
                            ImproviseradKniv kniv = new ImproviseradKniv();

                            if (köp == 1)
                            {
                                if (fange.VapenBonus == kniv.AttackBonus)
                                {
                                    Console.WriteLine($"Du har redan {kniv.Namn}");
                                }
                                else if (fange.Guld >= kniv.Pris)
                                {
                                    
                                    fange.BetalaGuld(kniv.Pris);
                                    fange.BytVapen(kniv.AttackBonus);
                                    Console.WriteLine($"Du har köpt {kniv.Namn}. Du gör nu {fange.Attack} skada. Guld kvar: {fange.Guld}");
                                    Console.WriteLine();
                                }
                                else if (fange.Guld < kniv.Pris)
                                {
                                    Console.WriteLine($"Du har inte råd med {kniv.Namn} du har {fange.Guld} Guld");
                                }

                            }
                            else if (köp == 2)
                            {
                                if (fange.VapenBonus == metallror.AttackBonus)
                                {
                                    Console.WriteLine($"Du har redan {metallror.Namn}") ;
                                }
                               else if (fange.Guld >= metallror.Pris)
                                {
                                    
                                    fange.BetalaGuld(metallror.Pris);
                                    fange.BytVapen(metallror.AttackBonus);
                                    Console.WriteLine($"Du har köpt {metallror.Namn}. Du gör nu {fange.Attack} skada. Guld kvar: {fange.Guld}");
                                    Console.WriteLine();
                                }
                               
                                else if (fange.Guld < metallror.Pris)
                                {
                                    Console.WriteLine($"Du har inte råd med {metallror.Namn} du har {fange.Guld} Guld");
                                }
                            }
                            else if (köp == 0)
                            {
                                svartMarknad = false;
                                
                            }
                        }

                    }
                    else
                    {
                        Console.WriteLine($"{fange.Namn} måste vara minst level 5 för att åka ner i Arenan");
                    }
                }
                else if (val == 4)
                    if (fange.Level >= 5)
                {
                    Arenan arena = new Arenan();
                    arena.StartaArena(fange, rnd);
                }
                else if (fange.Level < 5)
                    {
                        Console.WriteLine($"Du måste vara level 5 för att åka ner i Arenan. {fange.Namn} är bara level {fange.Level}");
                    }
            }

            Console.WriteLine($"\nSpelet är slut! Du överlevde i {fange.Dagar} dagar och nådde level {fange.Level}. Total XP: {fange.TotalXP}");
        }
    }
}
  