namespace Tågäventyret;

public class Zombie
{
    // Modernare stil
    // get och private set för Namn, Hp, Attack,XpBeloning
    public string Name { get; private set; }
    public int Hp { get; private set; }
    public int Attack { get; private set; }
    public int Forsvar { get; private set; }
    public int XpBeloning { get; private set; }
    

    // Konatruktor

    public Zombie (string name, int hp, int attack, int forsvar, int xpBeloning)
    {
        Name = name;
        Hp = hp;
        Attack = attack;
        Forsvar = forsvar;
        XpBeloning = xpBeloning;
       
    }

    // Minskar Zombiens HP och returnerar true om Zombien dör.
    public bool TakeDamage(int skada)
    {
        Hp -= skada;

        // HP ska aldrig kunna vara mindre än 0.
        if (Hp <= 0)
        {
            Hp = 0;
            return true;
        }

        return false;
    }
    public void AnpassaTillLevel(int level)
    {
        if (level >= 4)
        {
            Hp += 50;
            Attack += 7;
        }
    }


    // Zombie attackerar spelaren.
    public void AttackPlayer(Player player)
    {
        player.TakeDamage(Attack);
    }
}
