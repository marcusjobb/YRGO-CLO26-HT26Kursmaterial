class Player
{
    // fields
    string name;
    int hp;
    int maxHp;
    int attack;
    int defence;
    int level;
    int xp;
    int daysAlive;

    public Player(string Name)
    {
        Name = name;
        MaxHp = 100;
        Hp = MaxHp;
        Attack = 5;
        Defence = 1;
        Level = 1;
        Xp = 0;
        DaysAlive = 0;
    }

    // properties
    public string Name
    {
        get { return name; }
        private set { name = value; }
    }

    public int Hp
    {
        get { return hp; }
        private set { hp = value; }
    }

    public int MaxHp
    {
        get { return maxHp; }
        private set { maxHp = value; }
    }

    public int Attack
    {
        get { return attack; }
        private set { attack = value; }
    }

    public int Defence
    {
        get { return defence; }
        private set { defence = value; }
    }

    public int Level
    {
        get { return level; }
        private set { level = value; }
    }

    public int Xp
    {
        get { return xp; }
        private set { xp = value; }
    }

    public int DaysAlive
    {
        get { return daysAlive; }
        private set { daysAlive = value; }
    }

    // reduce HP
    public bool TakeDamage(int damage)
    {
        int realDamage = damage - Defence;

        if (realDamage < 1)
        {
            realDamage = 1;
        }

        Hp -= realDamage;

        if (Hp < 0)
        {
            Hp = 0;
        }

        return Hp == 0;
    }

    // gain HP
    public void Heal()
    {
        Hp = MaxHp;
        Console.WriteLine($"{Name} har nu {Hp}/{MaxHp} HP.");
    }

    // gain experience
    public void GainXp(int amount)
    {
        Xp += amount;
        Console.WriteLine($"Du fick {amount} XP!");

        if (Xp >= Level * 20)
        {
            LevelUp();
        }
    }

    // level up
    void LevelUp()
    {
        Xp -= Level * 20;
        Level++;
        Attack += 1;
        MaxHp += 10;
        Hp = MaxHp;

        Console.WriteLine();
        Console.WriteLine("==========================================");
        Console.WriteLine($"LEVEL UP! Du är nu level {Level}!");
        Console.WriteLine($"Attack: {Attack}");
        Console.WriteLine($"Max HP: {MaxHp}");
        Console.WriteLine("==========================================");
    }

    public void PassDay()
    {
        DaysAlive++;
    }


    public override string ToString()
    {
        return $"Namn: {Name}, HP: {Hp}, {MaxHp}, level: {Level}, XP: {Xp}, dagar: {DaysAlive}";
    }
}