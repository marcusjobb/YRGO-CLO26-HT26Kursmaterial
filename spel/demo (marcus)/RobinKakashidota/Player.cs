namespace RobinKakashidota;

class Player
{
    private static readonly Random _rnd = new();

    public string Name { get; }
    public int Level { get; private set; } = 1;
    public int Exp { get; private set; } = 0;
    public int Hp { get; private set; } = 200;
    public int MaxHp => 200;
    public bool IsAlive => Hp > 0;

    public Player(string name) { Name = name; }

    public int Attack() => _rnd.Next(5, 15);
    public void TakeDamage(int damage) => Hp -= damage;

    public void GainExp(int amount)
    {
        Exp += amount;
        while (Exp >= Level * 100)
        {
            Exp -= Level * 100;
            Level++;
            Console.WriteLine($"You leveled up, and are now level {Level}!");
        }
    }

    public void PrintStatus() =>
        Console.WriteLine($"You are level {Level}, and you have {Exp} exp and {Hp} hp");

    public void PrintDetails()
    {
        Console.WriteLine("***********");
        Console.WriteLine($"* Name: {Name}");
        Console.WriteLine($"* Level: {Level}");
        Console.WriteLine($"* Hp: {Hp}/{MaxHp}");
        Console.WriteLine($"* Exp: {Exp}/{Level * 100}");
        Console.WriteLine("***********");
    }
}
