// Lösning: monsterslagsmalet.md
Random slump = new Random();

// Välj monster 1
Console.WriteLine("Välj Monster 1:");
Console.WriteLine("1. Troll    (HP: 20, Attack: 3)");
Console.WriteLine("2. Goblin   (HP: 12, Attack: 5)");
Console.WriteLine("3. Drage    (HP: 30, Attack: 2)");
Console.WriteLine("4. Varg     (HP: 15, Attack: 4)");
Console.Write("Ditt val: ");
int val1 = int.Parse(Console.ReadLine()!);

string monster1Namn = "";
int monster1Hp = 0;
int monster1Attack = 0;

switch (val1)
{
    case 1: monster1Namn = "Troll";  monster1Hp = 20; monster1Attack = 3; break;
    case 2: monster1Namn = "Goblin"; monster1Hp = 12; monster1Attack = 5; break;
    case 3: monster1Namn = "Drage";  monster1Hp = 30; monster1Attack = 2; break;
    default: monster1Namn = "Varg";  monster1Hp = 15; monster1Attack = 4; break;
}

// Välj monster 2
Console.WriteLine("\nVälj Monster 2:");
Console.WriteLine("1. Troll    (HP: 20, Attack: 3)");
Console.WriteLine("2. Goblin   (HP: 12, Attack: 5)");
Console.WriteLine("3. Drage    (HP: 30, Attack: 2)");
Console.WriteLine("4. Varg     (HP: 15, Attack: 4)");
Console.Write("Ditt val: ");
int val2 = int.Parse(Console.ReadLine()!);

string monster2Namn = "";
int monster2Hp = 0;
int monster2Attack = 0;

switch (val2)
{
    case 1: monster2Namn = "Troll";  monster2Hp = 20; monster2Attack = 3; break;
    case 2: monster2Namn = "Goblin"; monster2Hp = 12; monster2Attack = 5; break;
    case 3: monster2Namn = "Drage";  monster2Hp = 30; monster2Attack = 2; break;
    default: monster2Namn = "Varg";  monster2Hp = 15; monster2Attack = 4; break;
}

Console.WriteLine($"\n--- Kampen börjar! {monster1Namn} vs {monster2Namn} ---");

int runda = 1;

while (monster1Hp > 0 && monster2Hp > 0)
{
    Console.WriteLine($"\nRunda {runda}:");

    int slag1 = slump.Next(1, 7);
    int skada1 = slag1 + monster1Attack;
    monster2Hp -= skada1;
    Console.WriteLine($"  {monster1Namn} slår {slag1} + {monster1Attack} = {skada1} i skada → {monster2Namn} har {monster2Hp} HP kvar");

    if (monster2Hp <= 0)
    {
        Console.WriteLine($"  {monster2Namn} besegrades!");
        break;
    }

    int slag2 = slump.Next(1, 7);
    int skada2 = slag2 + monster2Attack;
    monster1Hp -= skada2;
    Console.WriteLine($"  {monster2Namn} slår {slag2} + {monster2Attack} = {skada2} i skada → {monster1Namn} har {monster1Hp} HP kvar");

    runda++;
}

Console.WriteLine();
if (monster1Hp > 0)
{
    Console.WriteLine($"⚔️ {monster1Namn} vinner!");
}
else if (monster2Hp > 0)
{
    Console.WriteLine($"⚔️ {monster2Namn} vinner!");
}
else
{
    Console.WriteLine("Oavgjort — båda föll i samma runda!");
}
