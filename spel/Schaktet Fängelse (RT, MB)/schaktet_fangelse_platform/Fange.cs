using System;

namespace schaktet_fangelse_platform
{

    public class Fange
    {
        // Properties
        public string Namn { get; private set; }
        public int HP { get; private set; }
        public int MaxHP { get; private set; }
        public int Attack { get; private set; }
        public int Forsvar { get; private set; }
        public int Level { get; private set; }
        public int XP { get; private set; }
        public int Dagar { get; private set; }
        public int Guld { get; private set; }
        public int VapenBonus { get; private set; }
        public int TotalXP { get; private set; }


        private int XPForNastaLevel => 50 * Level;  // Här utförs räkning av XP vid nästa nivå

        // Konstruktorn, hårdkodat eftersom alla spelare börjar med samma förutsättningar, konstruktor tar därför bara emot namn
        public Fange(string namn)
        {
            Namn = namn;
            HP = 100;
            MaxHP = 100;
            Attack = 10;
            Forsvar = 5;
            Level = 1;
            XP = 0;
            Dagar = 0;
            Guld = 0;
            VapenBonus = 0;
        }

        // Metoder

        public void NyDag()
        {
            Dagar++;
            Console.WriteLine($"En dag har gått. Dagar i schaktet: {Dagar}");
        }
        public bool TakeDamage(int skada)
        {
            HP -= skada;

            if (HP < 0)
            {
                HP = 0; // HP kommer aldrig visas som negativt i spelet
            }
            Console.WriteLine($"{Namn} tar {skada}. HP kvar: {HP}/{MaxHP}");
            return HP <= 0;
        }

        public void Heal()
        {
            HP = MaxHP;
            Console.WriteLine($"{Namn} Du har läkt helt");
        }

        public void LaggTillGuld(int mangd)
        {
            Guld += mangd;
            Console.WriteLine($"{Namn} hittar {mangd} guld! Totalt guld: {Guld}");
        }
        public void BetalaGuld(int mangd)
        {
            Guld -= mangd;

        }
        public void BytVapen(int bonus)
        {
            Attack = Attack - VapenBonus + bonus;
            VapenBonus = bonus;
        }

        public void GainXP(int mangd)
        {
            XP += mangd;
            TotalXP += mangd;
            Console.WriteLine($"{Namn} får {mangd} XP. Totalt XP: {TotalXP}");

            // While istället för if, ifall spelaren får så mycket XP att flera levels klaras av samtidigt
            while (XP >= XPForNastaLevel)
            {
                XP -= XPForNastaLevel;
                LevelUp();
            }
        }

        public void LevelUp()
        {
            Level++;
            MaxHP += 20;
            Attack += 3;
            HP = MaxHP;

            Console.WriteLine($"{Namn} har nivå {Level} i schaktet!");
            Console.WriteLine($"Ny MaxHP: {MaxHP}, ny Attack: {Attack}");
        }
    }
}


// Namn, HP, MaxHP, Attack, Försvar, Level, XP, Dagar överlevda - som fält/properties
// TakeDamage (int skada) minskar HP, returnerar en bool true om spelaren dör
// Heal() - Återställer HP till maxHP
// GainXP (int mängd) - lägger till XP, utlöser LevelUP() vid tröskel
// LevelUP() - Höjer level, ökar maxHP och attack, återställer HP
// Konstruktor med privata fält och properties (inte bara publika variabler)

// Värden
//----------
// HP / maxHP = 100
// Attack = 10
// Försvar = 5
// Level = 1
// XP - tröskel för level up = 50 x nuvarande level
// Vid level up: +maxHP = +20
// Vid level up: + attack = 3
// Guld = 0

 
