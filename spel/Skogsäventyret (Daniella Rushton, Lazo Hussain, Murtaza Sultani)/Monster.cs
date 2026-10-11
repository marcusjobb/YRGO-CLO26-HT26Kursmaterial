
class Monster
{
    public string MonsterName { get; private set; }
    public int Hp { get; private set; }
    public int Attack { get; private set; }
    public int Xp { get; private set; }
    public int Defense { get; private set; }

    public Monster(string monsterName, int monsterHp, int monsterAttack, int monsterXp, int monsterDefense)
    {
        MonsterName = monsterName;
        Hp = monsterHp; 
        Attack = monsterAttack;
        Xp = monsterXp;
        Defense = monsterDefense;
    }

    public bool TakeDamage(int damage)
    {
        Hp -= damage;
        if (Hp < 0)
        {
            Hp = 0; // negative health is 0 health
        }
        return Hp == 0;
    }

    public bool AttackPlayer(Player player)
    {
        Console.WriteLine($"{MonsterName} attacks {player.Name} for {Attack} damage!");
        return player.TakeDamage(Attack);
    }

    public int GoldDrop()
    {
        Random random = new Random();
        return random.Next(5, 16); // Monster drops random gold amount between 5 and 15
    }
}

// types of monsters. arv
class Goblin : Monster
{
    public Goblin() : base("Goblin", 10, 5, 10, 1) //name, hp, atk, xp, def
    {

    }

}

class Orc : Monster
{
    public Orc() : base("Orc", 20, 10, 30, 5)
    {

    }
}

class Ogre : Monster
{
    public Ogre() : base("Ogre", 40, 30, 45, 10)
    {

    }
}