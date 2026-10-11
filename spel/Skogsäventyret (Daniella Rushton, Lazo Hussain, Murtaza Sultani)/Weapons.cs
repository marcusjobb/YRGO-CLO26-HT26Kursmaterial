public class Weapon
{
    public string WeaponName { get; private set; }
    public int AttackBonus { get; private set; }
    public int Price { get; private set; }

    public Weapon(string weaponName, int attackBonus, int price)
    {
        WeaponName = weaponName;
        AttackBonus = attackBonus;
        Price = price;
    }
}

class Dagger : Weapon
{
    public Dagger() : base("Dagger", 5, 20) // name, attack bonus, price
    {
    }
}

class Sword : Weapon
{
    public Sword() : base("Sword", 10, 40)
    {
    }
}

class Greatsword : Weapon
{
    public Greatsword() : base("Greatsword", 15, 60) 
    {
    }
}