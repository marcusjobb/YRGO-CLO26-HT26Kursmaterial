using System;
using System.Collections.Generic;
using System.Text;

namespace GoteborgsSkogsAventyr
{
    public class Longsword : Weapon
    {
        public Longsword() : base("Longsword", 10, 30)
        {
        }
        public override int Använd()
        {
            return AttackBonus + 2;
        }
    }
}