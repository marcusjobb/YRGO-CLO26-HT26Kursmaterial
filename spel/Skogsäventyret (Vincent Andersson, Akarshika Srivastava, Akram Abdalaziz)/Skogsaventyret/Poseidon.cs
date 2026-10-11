namespace Skogsaventyret
{
    // Andra monstret.
    public class Poseidon : Monster
    {
        public Poseidon()
        {
            Namn = "Poseidon";
            Hp = 70;
            Attack = 15;
            Forsvar = 8;
            XpBelöning = 40;
            Beskrivning = "Poseidon från Avenyn.";
        }

        // Poseidon återställer HP.
        public void Regenerera()
        {
            Hp += 10;
        }
    }
}