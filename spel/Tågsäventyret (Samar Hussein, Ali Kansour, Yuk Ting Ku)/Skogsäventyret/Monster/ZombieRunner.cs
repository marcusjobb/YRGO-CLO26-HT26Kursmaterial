using System;
using System.Collections.Generic;
using System.Text;
// 2. Carriage 2: Medium Zombie - Runner

namespace Tågäventyret
{
    public class Runner : Zombie
    {
        public Runner()
           : base("Mutated Guard", hp: 50, attack: 2, forsvar: 4, xpBeloning: 70)

        {
        }
    }
}
