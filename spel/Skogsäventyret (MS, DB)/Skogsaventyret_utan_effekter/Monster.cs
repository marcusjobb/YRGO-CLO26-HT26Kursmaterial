abstract partial class Monster { //ai sa till mig att göra klassen abstract så jag inte "råkar" skapa monster utan behöver använda subklasserna.
    public string Namn { get; private set; }
    public int Hp { get; private set; }
    public int MaxHp { get; private set; }
    public int Attack { get; private set; }
    public int XpBelöning { get; private set; }
    public ConsoleColor Färg { get; set; } //ai genererat för att göra det möjligt för färg byten

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
            Console.WriteLine("\nDu har dödat monstret!\n");
            return true;
        }
        return false;
    }

    public virtual bool Attackerar(Player spelare) {
        SkrivUtFiendeNamn("", $" attackerar och gör {Attack} skada!");
        Console.WriteLine();
        return spelare.TakeDamage(Attack);
        
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
    }

    public override bool Attackerar(Player spelare)
    {
        SkrivUtFiendeNamn("", " Karen klagar över något världsligt igen... ");
        return base.Attackerar(spelare);
    }
}

class Mellanchef : Monster // peon noob
{
    public Mellanchef() : base("Robert", 12,12, 2, 5) {
        
        Färg = ConsoleColor.Green;
    }

    public override bool Attackerar(Player spelare)
    {
        SkrivUtFiendeNamn("", " säger Jag bestämmer jag är faktiskt chef! ");
        return base.Attackerar(spelare);
    }
}

class Flatearther : Monster // foliehatts kungen!
{
    public Flatearther() : base("Foliehatt", 16,16, 4, 12) {
        
        Färg = ConsoleColor.DarkBlue;
    }

    public override bool Attackerar(Player spelare)
    {
        SkrivUtFiendeNamn("", " säger att NASA ljuger! ");
        return base.Attackerar(spelare);
    }
}