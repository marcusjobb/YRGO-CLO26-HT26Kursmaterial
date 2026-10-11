namespace SkogsaventyretVG;

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

class Goblin   : Monster { public Goblin()   : base("Goblin",    12,  5, 1,  10) {} }
class Troll    : Monster { public Troll()    : base("Troll",     25,  9, 3,  20) {} }
class Drake    : Monster { public Drake()    : base("Drake",     40, 15, 5,  40) {} }
class Ärkdrake : Monster { public Ärkdrake() : base("Ärkdraken", 80, 20, 8, 100) {} }

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

    public static Monster ArenaMonster(int runda) => runda switch
    {
        1 or 2 => new Goblin(),
        3 or 4 => new Troll(),
        5 or 6 => new Drake(),
        7      => new Ärkdrake(),
        _      => new Goblin(),
    };
}
