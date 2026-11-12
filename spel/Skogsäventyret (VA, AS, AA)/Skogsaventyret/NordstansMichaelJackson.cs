namespace Skogsaventyret
{
    // Tredje monstret.
    public class NordstansMichaelJackson : Monster
    {
        public NordstansMichaelJackson()
        {
            Namn = "Nordstans Michael Jackson";
            Hp = 100;
            Attack = 20;
            Forsvar = 10;
            XpBelöning = 60;
            Beskrivning = "En mystisk figur från Nordstan.";
        }

        // Återställer HP.
        public void Regenerera()
        {
            Hp += 15;
        }

        // Specialattack.
        public void ElAttack(Spelare spelare)
        {
            spelare.TakeDamage(Attack + 10);
        }
    }
}