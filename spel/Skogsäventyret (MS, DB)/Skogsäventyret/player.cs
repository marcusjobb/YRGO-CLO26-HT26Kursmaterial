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
    public int Poletter { get; private set; } = 2;   // NY – kaffepoletter, du börjar med 2
    public Weapon? AktivtVapen { get; private set; }  // NY – null = inget vapen (bara nävarna)

    // NY – din riktiga attack = grundattack + vapnets bonus
    public int TotalAttack => Attack + (AktivtVapen?.AttackBonus ?? 0);

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
        if (Hp <= 0) {
            return true;
        }
        return false;
    }
    public bool Heal() {

        if (Hp == MaxHp) {
            return false;
        }
        Hp = MaxHp;
        return true;
    }
    
    // NY – köper ett vapen: drar poletter och gör vapnet till ditt aktiva vapen
    public bool KöpVapen(Weapon vapen) {
        if (Poletter < vapen.Pris) {
            return false;
        }
        Poletter -= vapen.Pris;
        AktivtVapen = vapen;
        return true;
    }

    // NY – tar upp ett vapen som ett monster tappat.
    // Byter bara om det nya vapnet är bättre än det du har.
    public bool TaUppVapen(Weapon vapen) {
        if (AktivtVapen != null && AktivtVapen.AttackBonus >= vapen.AttackBonus) {
            return false;
        }
        AktivtVapen = vapen;
        return true;
    }

    // NY – lägger till poletter (när ett monster släpper dem)
    public void GePoletter(int antal) {
        Poletter += antal;
    }

    // NY – använder en polett i kaffemaskinen. Ger false om du inte har någon.
    public bool AnvändPolett() {
        if (Poletter <= 0) {
            return false;
        }
        Poletter--;
        return true;
    }

    // poletter = hur många kaffepoletter monstret släppte (NY)
    public void GainXp(int mängd, int poletter = 0) {
        
        Xp += mängd;
        TotalXp += mängd;
        Poletter += poletter; // NY
        
        Console.Write("Du fick ");
        Console.ForegroundColor = ConsoleColor.Cyan; //ai hjälpte med färg
        Console.Write($"{mängd} XP");
        Console.ResetColor();
        Console.Write("! Du har nu totalt ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write($"{TotalXp} XP");
        Console.ResetColor();
        Console.WriteLine("!");

        // NY – visa kaffepoletterna direkt efter XP:n, i samma stil
        Console.Write("Du fick ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"{poletter} {(poletter == 1 ? "kaffepolett" : "kaffepoletter")}");
        Console.ResetColor();
        Console.Write("! Du har nu totalt ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"{Poletter} {(Poletter == 1 ? "kaffepolett" : "kaffepoletter")}");
        Console.ResetColor();
        Console.WriteLine("!\n");
        
        // NY – "while" i stället för "if", så att en stor XP-bonus (t.ex. från arenan)
        // kan ge flera level ups på en gång
        while (Xp >= Level * 20) {
            int tröskel = Level * 20; //ai hjälp
            Xp -= tröskel; //ai hjälp
            LevelUp();
        }
    }

    public void LevelUp() {
        Level++;
        MaxHp += 10;
        Attack += 2;
        Försvar += 1;
        Hp = MaxHp;

        StridsEffekter.LevelUpFanfar();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Du levlade upp! Grattis du är nu level {Level}!");
        Console.WriteLine($"MaxHp är nu: {MaxHp}");
        Console.WriteLine($"Attack är nu: {Attack} (+{TotalAttack - Attack} från vapen)");
        Console.WriteLine("Du har nu fått tillbaka full hp!");
        Console.ResetColor();
    }
}