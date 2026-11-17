using System;
using System.Collections.Generic;
using System.Text;

namespace schaktet_fangelse_platform
{
    internal class Hamstraren: Medfange
    {
        public Hamstraren() : base("Hamstraren", hp: 50, attack: 8,forsvar: 4, xpBeloning: 20, guldMin: 10, guldMax: 15)
        {

        }
    }
}
