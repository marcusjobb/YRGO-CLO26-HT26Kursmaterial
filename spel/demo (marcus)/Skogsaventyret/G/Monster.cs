namespace SkogsaventyretG;

abstract class Monster
{
    public string Namn    { get; }
    public int    Hp      { get; set; }
    public int    Attack  { get; }
    public int    Forsvar { get; }
    public int    Xp      { get; }

    protected Monster(string namn, int hp, int attack, int forsvar, int xp)
    {
        Namn    = namn;
        Hp      = hp;
        Attack  = attack;
        Forsvar = forsvar;
        Xp      = xp;
    }
}

class Goblin : Monster { public Goblin() : base("Goblin", 12,  5, 1, 10) {} }
class Troll  : Monster { public Troll()  : base("Troll",  25,  9, 3, 20) {} }
class Drake  : Monster { public Drake()  : base("Drake",  40, 15, 5, 40) {} }

static class MonsterFabrik
{
    private static readonly Func<Monster>[] _pool =
    [
        () => new Goblin(),
        () => new Goblin(),
        () => new Goblin(),
        () => new Troll(),
        () => new Troll(),
        () => new Drake(),
    ];

    public static Monster Slumpa(Random rng) => _pool[rng.Next(_pool.Length)]();
}
