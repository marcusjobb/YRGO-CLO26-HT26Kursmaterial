using System;
using System.Collections.Generic;
using System.Text;

namespace schaktet_fangelse_platform
{
    internal abstract class Redskap
    {
        public string Namn { get; private set; }
        public int AttackBonus { get; private set; }
        public int Pris { get; private set; }

        protected Redskap(string namn, int attackBonus, int pris)
        {
            Namn = namn;
            AttackBonus = attackBonus;
            Pris = pris;
        }
    }


}
