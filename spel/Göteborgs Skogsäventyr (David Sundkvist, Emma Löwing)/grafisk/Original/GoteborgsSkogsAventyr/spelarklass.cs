
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using GoteborgsSkogsAventyr;

public class Spelare
{
    //Privata fält
    private string _namn;
    private int _hp;
    private int _maxhp;
    private int _attack;
    private int _defense;
    private int _level;
    private int _xp;
    private int _dagarÖverlevda;
    private int _gold;

    //Privata fält för hantering av GainXp
    private int _xpTillNästaLevel;



    //Properties - public get, public set
    // Properties

    
    public string Namn
    {
        get { return _namn; }
    }

    public int Hp
    {
        get { return _hp; }
    }

    public int MaxHp
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

    public int Level
    {
        get { return _level; }
    }

    public int Xp
    {
        get { return _xp; }
    }

    public int DagarÖverlevda
    {
        get { return _dagarÖverlevda; }
    }

    public int XpTillNästaLevel
    {
        get { return _xpTillNästaLevel; }
    }

    public int Gold
    {
        get { return _gold; }
    }
      




    //För att snabbt skapa properties: skriv Prop -> tap; ändra till rätt int/string; lägg till namn (stor bokstav)

    //Konstruktor 
    public Spelare(string namn, int hp, int maxhp, int attack, int defense, int level, int xp, int dagarÖverlevda, int gold)
    {
        _namn = namn;
        _hp = hp;
        _maxhp = maxhp;
        _attack = attack;
        _defense = defense;
        _level = level;
        _xp = xp;
        _dagarÖverlevda = dagarÖverlevda;
        _xpTillNästaLevel = 10;
        _gold = gold;
    }

    private List<items> _inventory = new List<items>();


    //Metoder
    //TakeDamage(int damage)
    //Heal()
    //GainXP(int mägnd)
    //LevelUp()

    public bool TakeDamage(int damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            _hp = 0;
            return true;
        }
        else
        {
            return false;
        }

    }

    public void Heal()
    {
        _hp = _maxhp;
        Console.WriteLine($"Du har vilat i Bontaniska Trädgården. HP = {_hp}");
        //TODO - snygga till och gör tydligare för användaren? 
    }

    public void GainXP(int mängd)
    {
        _xp += mängd;
        if (_xp >= _xpTillNästaLevel)
        {
            LevelUp();
        }

    }

    public void LevelUp()
    {
        _level++;
        _maxhp = _maxhp + 10;
        _attack = _attack + 2;
        _hp = _maxhp;
        _xpTillNästaLevel += _level * 10;
        Console.WriteLine($"New level! Level {_level}");

    }


    public void AddDay()
    {
        _dagarÖverlevda++;
    }

    public void AddGold(int amount)
    {
        _gold += amount;
    }

    public void AddItem(items item)
    {
        _inventory.Add(item);
        _attack += item.AttackBoost;
        _defense += item.DefenseBoost;
        _hp += item.HpBoost;
        _maxhp += item.HpBoost;
    }
    public void ShowInventory()
    {
        Console.WriteLine("===== INVENTORY =====");

        foreach (items item in _inventory)
        {
            Console.WriteLine(item.ItemName);
        }

    }
    

    public bool SpendGold(int amount)
    {
        if (_gold >= amount)
        {
            _gold -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool HasItem(string itemName)
    {
        foreach (items item in _inventory)
        {
            if (item.ItemName == itemName)
            {
                return true;
            }
        }

        return false;
    }
}

