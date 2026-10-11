namespace Tågäventyret;

public class Player
{
    public string Namn { get; private set; }
    public int Hp { get; private set; }
    public int MaxHp { get; private set; }
    public int Attack { get; private set; }
    public int Forsvar { get; private set; }
    public int Level { get; private set; }
    public int Xp { get; private set; }
    public int Dagar { get; private set; }


    // Konstruktor 
    public Player(string namn)
    {
        // Tilldela startvärden till spelarens private fält
         Namn = namn;
         MaxHp = 60;
         Hp = MaxHp;
         Attack = 6;
         Forsvar = 3;
         Level = 1;
         Xp = 0;
         Dagar = 0;
        
    }
     // Minskar spelarens HP och returnera true om spelaren dör
    public bool TakeDamage(int skada)
    {
        Hp -= skada;

        if (Hp <= 0)
        {
            // om HP är 0 eller mindre så dörr spelaren
            Hp = 0;
            return true;
        }

        return false;
    }

    // Återställer HP till MaxHP (kostar en dag)
    public void Heal()
    {
        Hp = MaxHp;
        Dagar++;
    }
    // Ge spelaren XP 
    // När spelaaren når XP-tröskeln anropas LevelUp()
    public void GainXP(int mängd)
    {
       Xp += mängd;

        if (Xp >= 20)
        {
            LevelUp();
  
        }
    }

    // Höjer spelarens level, maxHP och attack.
    // HP återställs när spelaren levlar upp.
    public void LevelUp()
    {
        Level++;
        MaxHp+= 10;
        Attack += 2;
        Hp = MaxHp;
    }
    // Ökar antalet överlevda dagar med en dag.
    public void ÖkaDag()
    {
        Dagar++;
    }
}


