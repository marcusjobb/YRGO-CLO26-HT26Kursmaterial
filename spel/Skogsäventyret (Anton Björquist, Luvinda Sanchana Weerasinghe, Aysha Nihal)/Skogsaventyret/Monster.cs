class Monster
{
    string name;
    int hp;
    int attack;
    int xpGain;

    public Monster(string name, int hp, int attack, int xpGain)
    {
        Name = name;
        Hp = hp;
        Attack = attack;
        XpGain = xpGain;
    }

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

    public int Attack
    {
        get { return attack; }
        private set { attack = value; }
    }

    public int XpGain
    {
        get { return xpGain; }
        private set { xpGain = value; }
    }

    public bool TakeDamage(int Damage)
    {
        Hp -= Damage;

        if (Hp < 0)
            Hp = 0;

        return Hp == 0;
    }
}