partial class Player {
    public string Namn{ get; set; }
    public int Hp{ get; private set; }
    public int MaxHp{ get; private set; }
    public int Attack{ get; private set; }
    public int Försvar{ get; private set; }
    public int Level{ get; private set; }
    public int Xp{ get; private set; }
    public int TotalXp { get; private set; }
    public int DagarÖverlevda{ get; set; }
    public int Tröskel => Level * 20;
    
    public Player(string name, int hp, int maxHp, int attack, int försvar, int level, int xp, int dagarÖverlevda) {
        Namn = name;
        Hp = hp;
        MaxHp = maxHp;
        Attack = attack;
        Försvar = försvar;
        Level = level;
        Xp = xp;
        DagarÖverlevda = dagarÖverlevda;
    }

    public bool TakeDamage(int skada) {
        Hp -= skada;
        if (Hp < 0) {
            Hp = 0; //ai tyckte detta var en bättre lösning.
        }
        return Hp <= 0;
    }
    public bool Heal() {

        if (Hp == MaxHp) {
            return false;
        }
        Hp = MaxHp;
        return true;
    }


    public void GainXp(int mängd) {
        
        Xp += mängd;
        TotalXp += mängd;
        
        Console.Write("Du fick ");
        Console.ForegroundColor = ConsoleColor.Cyan; //ai hjälpte med färg
        Console.Write($"{mängd} XP");
        Console.ResetColor();
        Console.Write("! Du har nu totalt ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write($"{Xp} XP");
        Console.ResetColor();
        Console.WriteLine("!");
        
        if (Xp >= Tröskel) {
            Xp -= Tröskel;
            LevelUp();
        }
    }

    public void LevelUp() {
        Level++;
        MaxHp += 10;
        Attack += 2;
        Försvar += 1;
        Hp = MaxHp;
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Du levlade upp! Grattis du är nu level {Level}!");
        Console.WriteLine($"MaxHp är nu: {MaxHp}");
        Console.WriteLine($"Attack är nu: {Attack}");
        Console.WriteLine("Du har nu fått tillbaka fullt hp!");
        Console.ResetColor();
    }
}