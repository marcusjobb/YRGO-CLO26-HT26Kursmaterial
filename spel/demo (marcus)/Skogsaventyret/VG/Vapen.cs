namespace SkogsaventyretVG;

abstract class Vapen
{
    public string Namn        { get; }
    public int    AttackBonus { get; }
    public int    Pris        { get; }

    protected Vapen(string namn, int attackBonus, int pris)
    {
        Namn        = namn;
        AttackBonus = attackBonus;
        Pris        = pris;
    }
}

class Spjut     : Vapen { public Spjut()     : base("Spjut",     3,  15) {} }
class Svärd     : Vapen { public Svärd()     : base("Svärd",     6,  30) {} }
class Stridsyxa : Vapen { public Stridsyxa() : base("Stridsyxa", 10, 60) {} }
