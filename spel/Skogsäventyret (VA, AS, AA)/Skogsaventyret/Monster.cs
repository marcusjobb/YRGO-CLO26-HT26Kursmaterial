namespace Skogsaventyret
{
    // Basklass för alla monster.
    public class Monster
    {
        public string Namn { get; protected set; } = "";
        public int Hp { get; protected set; }
        public int Attack { get; protected set; }
        public int Forsvar { get; protected set; }
        public int XpBelöning { get; protected set; }
        public string Beskrivning { get; protected set; } = "";

        // Kollar om monstret lever.
        public bool ÄrVidLiv
        {
            get { return Hp > 0; }
        }

        // Tar skada och returnerar true om monstret dör.
        public virtual bool TaSkada(int skada)
        {
            Hp -= skada;

            if (Hp < 0)
            {
                Hp = 0;
            }

            return Hp <= 0;
        }
    }
}