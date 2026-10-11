abstract partial class Monster { //ai sa till mig att göra klassen abstract så jag inte "råkar" skapa monster utan behöver använda subklasserna.
    public string Namn { get; private set; }
    public int Hp { get; private set; }
    public int MaxHp { get; private set; }
    public int Attack { get; private set; }
    public int XpBelöning { get; private set; }
    public ConsoleColor Färg { get; set; } //ai genererat för att göra det möjligt för färg byten
    public int PolettMin { get; protected set; } = 0;   // NY – minsta antal kaffepoletter monstret släpper
    public int PolettMax { get; protected set; } = 1;   // NY – högsta antal kaffepoletter monstret släpper

    // NY – chans i procent att monstret tappar sitt vapen när det dör
    public int VapenDropChans { get; protected set; } = 0;

    // NY – vilket vapen monstret har. Varje subklass bestämmer själv (override).
    protected virtual Weapon? SkapaVapen() => null;

    // NY – slumpar om monstret tappar sitt vapen. Ger null om inget droppas.
    public Weapon? SläppVapen() {
        Weapon? vapen = SkapaVapen();
        if (vapen == null) return null;
        if (Random.Shared.Next(1, 101) <= VapenDropChans) return vapen;
        return null;
    }

    // NY – används för att sortera arenans monster från svagast till starkast
    public int Styrka => MaxHp + Attack * 3;

    public Monster(string namn, int hp, int maxHp, int attack, int xpBelöning) {
        Namn = namn;
        Hp = hp;
        MaxHp = maxHp;
        Attack = attack;
        XpBelöning = xpBelöning;
    }

    public bool TakeDamage(int skada) {
        Hp -= skada;
        if (Hp <= 0) {
            Console.WriteLine("\nDu har dödat monstret!");
            return true;
        }
        return false;
    }

    static readonly Random slump = new Random();

    // NY – slumpar hur många kaffepoletter monstret tappar när det besegras
    public int SläppPoletter() {
        return slump.Next(PolettMin, PolettMax + 1);
    }

    public int SenasteSkada { get; private set; }   // så Program.cs vet hur hårt det blev

    public virtual bool Attackerar(Player spelare) {
        int skada = Attack;

        if (slump.Next(1, 101) <= 10) {             // 10 % chans att crita
            skada *= 2;
            StridsEffekter.Kritisk($"{Namn} critar dig! Dubbel skada: {skada}!", ConsoleColor.Red);
        }

        SenasteSkada = skada;
        SkrivUtFiendeNamn("", $" attackerar och gör {skada} skada!");
        Console.WriteLine();
        return spelare.TakeDamage(skada);
    }
    
    public void SkrivUtFiendeNamn( string textFöre = "", string textEfter = "") {
        Console.Write(textFöre);
        Console.ForegroundColor = Färg;
        Console.Write(Namn);
        Console.ResetColor();
        Console.WriteLine(textEfter);
    }
}

//--------------------------------------- Sub Monster -----------------------------------

class Karen : Monster // Final boss
{
    public Karen() : base("Karen", 45,45, 8, 50) {
        
        Färg = ConsoleColor.DarkYellow;
        PolettMin = 6;   // NY – Karen släpper 6–10 poletter
        PolettMax = 10;
        VapenDropChans = 50;   // NY – 50 % chans att tappa HDMI-kabeln
    }

    public override bool Attackerar(Player spelare)
    {
        Console.WriteLine("Karen klagar över något världsligt igen..."); //random genererade prompter?
        return base.Attackerar(spelare);
    }

    protected override Weapon? SkapaVapen() => new HdmiKabel(); // NY
}

class Mellanchef : Monster // peon noob
{
    public Mellanchef() : base("Robert", 12,12, 2, 5) {
        
        Färg = ConsoleColor.Green;
        PolettMin = 1;   // NY – Robert släpper 1–3 poletter
        PolettMax = 3;
        VapenDropChans = 5;    // NY – 5 % chans att tappa blyertspennan
    }

    public override bool Attackerar(Player spelare)
    {
        Console.WriteLine("Jag bestämmer jag är faktiskt chef!");   //random genererade prompter?
        return base.Attackerar(spelare);
    }

    protected override Weapon? SkapaVapen() => new Blyertspenna(); // NY
}

class Flatearther : Monster // foliehatts kungen!
{
    public Flatearther() : base("Foliehatt", 16,16, 4, 12) {
        
        Färg = ConsoleColor.DarkBlue;
        PolettMin = 2;   // NY – Foliehatt släpper 2–5 poletter
        PolettMax = 5;
        VapenDropChans = 15;   // NY – 15 % chans att tappa laptopen
    }

    public override bool Attackerar(Player spelare)
    {
        Console.WriteLine("NASA ljuger!");  //random genererade prompter?
        return base.Attackerar(spelare);
    }

    protected override Weapon? SkapaVapen() => new Laptop(); // NY
}