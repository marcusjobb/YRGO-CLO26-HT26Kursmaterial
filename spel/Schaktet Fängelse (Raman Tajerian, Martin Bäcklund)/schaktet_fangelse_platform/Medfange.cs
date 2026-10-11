
using System;
using System.Collections.Generic;
using System.Text;

namespace schaktet_fangelse_platform
{
    //Abstract för att den endast ska kunna skapa konkreta typer; utsvulten, hamstraren och bödeln.
    internal abstract class Medfange
    {
        //Public get, private set - utomstående ska kunna läsa värdena, men bara klassen ska kunna ändra dem.
        public string Namn { get; private set; }
        public int HP { get; private set; }
        public int Attack { get; private set; }
        public int Forsvar { get; private set; }
        public int XPBeloning { get; private set; }
        public int GuldMin { get; private set; } //Intervall för guld drop inte ett fast belopp
        public int GuldMax { get; private set; }
        
      


        //Protected konstruktor för att endast subklasserna ska kunna anropa konstruktorn
        protected Medfange(string namn, int hp, int attack,int forsvar, int xpBeloning, int guldMin, int guldMax)
        {
            Namn = namn;
            HP = hp;
            Attack = attack;
            Forsvar = forsvar;
            XPBeloning = xpBeloning;
            GuldMin = guldMin;
            GuldMax = guldMax;

        }
        public bool TakeDamage(int skada)
        {
            HP -= skada;
            if (HP < 0)
            {
                HP = 0; // Monster HP blir aldrig negativt
            }

            return HP <= 0;
        }
        public void Anfall(Fange fange)
        {
            fange.TakeDamage(this.Attack);
        }

        private static Random slump = new Random();
        public int TappaGuld()
        {
            return slump.Next(GuldMin, GuldMax + 1); //En range mellan GuldMin och GuldMax, + 1 -> för att man ska kunna få GuldMax värdet också
        }

    }
}
