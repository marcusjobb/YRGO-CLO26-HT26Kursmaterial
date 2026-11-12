using System;
using System.Collections.Generic;
using System.Text;

namespace schaktet_fangelse_platform
{
    internal class Arenan
    {
        private List<Medfange> motståndare;
        private int SegerBonus = 100;

        public Arenan()
        {
            motståndare = new List<Medfange>
            {
                new Hamstraren(),
                new Bodeln(),
                new Kannibalen(),
                new Samurajen(),
                new Utsvulten(),
                new Vakthunden(),
                new Slutboss()
            };
            motståndare.Sort((a, b) => a.Attack.CompareTo(b.Attack));

        }
        public bool StartaArena(Fange fange, Random rnd)
        {
            Console.WriteLine("Du vet inte vad du gett dig in på...");
            Console.WriteLine();

            for (int i = 0; i < motståndare.Count; i++)
            {
                Medfange monster = motståndare[i];
                Console.WriteLine($"Dag {i + 1} av {motståndare.Count}. Motståndare: {monster.Namn}");
                Console.WriteLine();

                bool striden = true;
                while (striden)
                {
                    Console.WriteLine($"{monster.Namn} har {monster.HP}HP, Du har {fange.HP}");
                    Console.WriteLine();
                    Console.WriteLine("Vad vill du göra?");
                    Console.WriteLine("1) Försvara");
                    Console.WriteLine("2) Anfall");
                    int val = int.Parse(Console.ReadLine());

                    int monsterSkada = monster.Attack - fange.Forsvar;
                    if (monsterSkada < 1) monsterSkada = 1;
                    bool monsterDog = false;
                    bool fangeDog = false;

                    if (val == 1)
                    {

                        int skada = monsterSkada / 2;
                        if (skada < 1)
                        {
                            skada = 1;
                        }
                        fangeDog =fange.TakeDamage(skada);

                        if (monster.HP == 0)
                        {

                        }
                    }
                    else if (val == 2)
                    {
                        int skada = fange.Attack - monster.Forsvar;
                        if (skada < 1)
                        {
                            skada = 1;
                        }
                       monsterDog = monster.TakeDamage(skada);
                        if (!monsterDog)
                        {
                            fangeDog = fange.TakeDamage(monsterSkada);
                        }
                    }
                    if (fangeDog)
                    { return false; }
                    if (monsterDog)
                        {
                        Console.WriteLine($"{monster.Namn} blev besegrad");
                        fange.GainXP(monster.XPBeloning);
                        fange.LaggTillGuld(monster.TappaGuld());
                        striden = false;
                    }
                    
                }
                fange.NyDag();
            } 
            Console.WriteLine("Du vann! Du besegrade samtliga motståndare!");
            fange.GainXP(SegerBonus);
            return true;
        } 
    }
}
