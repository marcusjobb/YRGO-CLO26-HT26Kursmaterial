
public class Monster
{
    //Privata fält
    private string _name;
    private int _hp;
    private int _maxhp;
    private int _attack;
    private int _defense;
    private int _xpdropp;

    //Properties - public get, public set
    public string Name
    {
        get { return _name; }
    }

    public int HP
    {
        get { return _hp; }
    }
    public int MaxHP
    {
        get { return _maxhp; }
    }
    public int Attack
    {
        get { return _attack; }
    }

    public int Defense
    {
        get { return _defense; }
    }

    public int XPReward
    {
        get { return _xpdropp; }
    }

    public Monster(string name, int hp, int maxhp, int attack, int defense, int xpdropp)
    {
        _name = name;
        _hp = hp;
        _maxhp = maxhp;
        _attack = attack;
        _defense = defense;
        _xpdropp = xpdropp;
    }


    public bool TakeDamage(int damage)
    {
        _hp -= damage;

        if (_hp < 0)
        {
            _hp = 0;
        }

        return _hp == 0;
    }



    //För att snabbt skapa properties: skriv Prop -> tap; ändra till rätt int/string; lägg till namn (stor bokstav)
    public virtual void AttackPlayer(Spelare spelare)

    {
        int damage = _attack - spelare.Defense;
        if (damage < 1)
        {
            damage = 1;
        }
        spelare.TakeDamage(damage);

        Console.WriteLine($"{_name} attackerar och gör {damage}");
    }
    public int DropGold()
    {
        Random random = new Random();
        int gold = random.Next(5, 16);

        return gold;
    }

    public class Sealmonster : Monster
    {
        public Sealmonster() : base("Sealmonster", 10, 10, 2, 2, 10)
        {

        }
        public override void AttackPlayer(Spelare spelare)
        {
            int damage = Attack - spelare.Defense;

            if (damage < 1)
            {
                damage = 1;
            }

            spelare.TakeDamage(damage);
            Console.WriteLine($"sealmonster biter dig och gör {damage} i damage!");
        }
        // polymorfism så att list<mosnter> som emma gjorde kan inneholla olika monster värden med egena attackplayer metoder
        // override borde göra så man kan göra olika saker i attackplayer metoden beroende på vilket monster det är.
    }

    public class FullChalmersStudent : Monster
    {
        public FullChalmersStudent() : base("Full Chalmers student", 18, 18, 12, 4, 15)
        {

        }
        public override void AttackPlayer(Spelare spelare)
        {
         
            int damage = Attack - spelare.Defense;

            if (damage < 1)
            {
                damage = 1;
            }

            spelare.TakeDamage(damage);
            Console.WriteLine($"full chalmers student slår dig i ansiktet och det gör {damage} i damage!");
        }
    }

    public class MarcusMedInlämning : Monster
    {
        public MarcusMedInlämning() : base("Marcus med inlämning", 30, 30, 15, 6, 25)
        {

        }
        public override void AttackPlayer(Spelare spelare)
        {
            if (spelare.HasItem("Katt") && spelare.HasItem("Taylor Swift Skiva"))
            {
                Console.WriteLine("Marcus blir förvirrad av katten och Taylor Swift-skivan och glömmer att attackera!");
                return;
            }

            int damage = Attack - spelare.Defense;

            if (damage < 1)
            {
                damage = 1;
            }
            spelare.TakeDamage(damage);
            Console.WriteLine($"Marcus med inlämning ger dig en läxa och det gör {damage} i confusiondamage!");
        }



    }
}
      