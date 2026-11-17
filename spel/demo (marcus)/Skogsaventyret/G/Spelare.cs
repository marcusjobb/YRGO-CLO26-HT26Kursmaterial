namespace SkogsaventyretG;

class Spelare
{
    public int Hp          { get; set; } = 30;
    public int MaxHp       { get; set; } = 30;
    public int Attack      { get; set; } = 8;
    public int Forsvar     { get; set; } = 3;
    public int Level       { get; set; } = 1;
    public int Xp          { get; set; } = 0;
    public int XpTillNästa { get; set; } = 20;
    public int Dagar       { get; set; } = 0;

    public void LevelUp()
    {
        Level++;
        MaxHp       += 10;
        Attack      += 2;
        XpTillNästa *= 2;
        Hp           = MaxHp;
        Console.WriteLine($"⬆️  Level up! Du är nu level {Level}!");
    }
}
