using System;
using System.Collections.Generic;
using System.Text;

namespace schaktet_fangelse_platform
{
    internal class Vakthunden : Medfange
    {
        public Vakthunden() : base("Vakthunden", hp: 45, attack: 17, forsvar: 3, xpBeloning: 40, guldMin: 10, guldMax: 20)
        {

        }
    }
}
