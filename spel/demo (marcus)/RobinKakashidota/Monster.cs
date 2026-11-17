// Originaluppgift: Codic 2021 — "Inlämning 3: The Game"
// Remake: Robin (Kakashidota) versionen
// Känd för: monstret säger *slurp*, arv = VG (ej YH-standard)

namespace RobinKakashidota;

abstract class Monster
{
    public string Name { get; protected set; }

    protected Monster(string name)
    {
        Name = name;
    }

    public abstract int Attack();
    public abstract int Hp { get; protected set; }
    public abstract int GiveExp();

    public void SkrikSlurp() => Console.WriteLine("UUuoooaah *slurp*");
}
