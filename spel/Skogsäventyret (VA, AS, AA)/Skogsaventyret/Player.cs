using System;

namespace Skogsaventyret
{
    // Klass som beskriver spelaren.
    public class Spelare
    {
        public string Namn { get; private set; }
        public int Hp { get; set; }
        public int MaxHp { get; private set; }
        public int Attack { get; private set; }
        public int Forsvar { get; private set; }
        public int Level { get; private set; }
        public int Xp { get; private set; }
        public int DagarÖverlevda { get; private set; }

        // Skapar en ny spelare.
        public Spelare(string namn)
        {
            Namn = namn;
            MaxHp = 100;
            Hp = MaxHp;
            Attack = 20;
            Forsvar = 5;
            Level = 1;
            Xp = 0;
            DagarÖverlevda = 0;
        }

        // Tar skada och returnerar true om spelaren dör.
        public bool TakeDamage(int skada)
        {
            Hp -= skada;

            if (Hp <= 0)
            {
                Hp = 0;
                return true;
            }

            return false;
        }

        // Återställer HP till max.
        public void Heal()
        {
            Hp = MaxHp;
        }

        // Den gamla metoden behålls.
        public void Hela(int mängd)
        {
            Hp += mängd;

            if (Hp > MaxHp)
            {
                Hp = MaxHp;
            }
        }

        // Ger spelaren XP.
        public void GainXP(int mängd)
        {
            Xp += mängd;

            if (Xp >= Level * 100)
            {
                LevelUp();
            }
        }

        // Den gamla metoden behålls.
        public void FåXp(int xp)
        {
            GainXP(xp);
        }

        // Höjer spelarens level.
        public void LevelUp()
        {
            Level++;
            MaxHp += 20;
            Attack += 5;
            Hp = MaxHp;

            Console.WriteLine(
                $"{Namn} gick upp till level {Level}!"
            );
        }

        // Körs när spelaren överlever en dag.
        public void ÖverlevDag()
        {
            DagarÖverlevda++;

            Console.WriteLine(
                $"{Namn} överlevde dagen."
            );
        }
    }
}