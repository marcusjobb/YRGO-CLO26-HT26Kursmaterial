using System;
using System.Collections.Generic;

namespace Skogsaventyret
{
    public static class Monsterfabrik
    {
        private static Random slumpgenerator = new Random();

        // Lista med alla typer av monster.
        private static List<Monster> monsterLista = new List<Monster>
        {
            new Fyllegubbe(),
            new Poseidon(),
            new NordstansMichaelJackson()
        };

        // Skapar ett slumpmässigt monster.
        public static Monster SkapaSlumpatMonster()
        {
            int index = slumpgenerator.Next(monsterLista.Count);

            Monster monster = monsterLista[index];

            if (monster is Fyllegubbe)
            {
                return new Fyllegubbe();
            }

            if (monster is Poseidon)
            {
                return new Poseidon();
            }

            return new NordstansMichaelJackson();
        }
    }
}