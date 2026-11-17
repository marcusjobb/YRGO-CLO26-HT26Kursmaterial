using System;
using System.Collections.Generic;
using System.Text;

namespace schaktet_fangelse_platform
{
    internal class SvartaMarknaden
    {
        private List<Redskap> sortiment;

        public SvartaMarknaden()
        {
            sortiment = new List<Redskap>
            {
                new ImproviseradKniv(),
                new Metallror()
            };
        }

        public void VisaSortiment()
        {
            Console.WriteLine("\n---Svarta Marknaden ---");
            for (int i = 0; i < sortiment.Count; i++)
            {
                Redskap r = sortiment[i];
                Console.WriteLine($"{i + 1}) {r.Namn} - Attackbonus: +{r.AttackBonus}, Pris: {r.Pris} guld");
            }
            Console.WriteLine("0) Lämna marknaden");

        }        

    }
}
