using System;
using System.Collections.Generic;
using System.Text;

namespace GoteborgsSkogsAventyr
{
    public class Greataxe : Weapon
    {
        public Greataxe() : base("Greataxe", 5, 20)
        {

        }
        public override int Använd()
        {
            return AttackBonus * 2;
        }
    }
}
