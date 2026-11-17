// Robin (Kakashidota) versionen remake
// Originaluppgift: Codic 2021 — "Inlämning 3: The Game"
// Krav som faktiskt användes: Player, Monster (abstrakt), SpecificMonster (arv = G)
// VG = Shop + guld + amuletter (strength/toughness)
// Notera: kravlistan stämde inte med YH-standard — det är hela vitsen med denna demo.

using RobinKakashidota;

var rnd = new Random();

Console.WriteLine("***********************");
Console.WriteLine("* Welcome to The Game! *");
Console.WriteLine("***********************");
Console.Write("Enter your name: ");
string name = Console.ReadLine() ?? "Unknown";

var player = new Player(name);

while (player.IsAlive && player.Level < 10)
{
    Console.WriteLine();
    Console.WriteLine("1. Go adventuring");
    Console.WriteLine("2. Show details about your character");
    Console.WriteLine("3. Exit game");
    Console.Write("> ");
    string choice = Console.ReadLine() ?? "";

    if (choice == "1")
    {
        // 10% chans att inget händer
        if (rnd.Next(10) == 0)
        {
            Console.WriteLine("You see nothing but swaying grass all around you...");
            Console.WriteLine("[Press enter to continue]");
            Console.ReadLine();
            continue;
        }

        var monster = new GiantWurm();
        Console.WriteLine($"Uh oh! A wild {monster.Name} appeared!");

        while (player.IsAlive && monster.Hp > 0)
        {
            int playerDmg = player.Attack();
            monster.TakeDamage(playerDmg);
            Console.WriteLine($"You hit the monster, dealing {playerDmg} damage");

            if (monster.Hp <= 0)
            {
                int exp = monster.GiveExp();
                Console.WriteLine($"You killed the monster, gaining {exp} experience!");
                player.GainExp(exp);
                player.PrintStatus();
                break;
            }

            monster.SkrikSlurp();
            int monsterDmg = monster.Attack();
            player.TakeDamage(monsterDmg);
            Console.WriteLine($"The monster hit you, dealing {monsterDmg} damage");
            Console.WriteLine($"{player.Name}: {player.Hp} hp");
            Console.WriteLine($"{monster.Name}: {monster.Hp} hp");

            if (!player.IsAlive)
            {
                Console.WriteLine($"You were killed by the monster :(");
                break;
            }

            Console.WriteLine("[Press enter to continue]");
            Console.ReadLine();
        }
    }
    else if (choice == "2")
    {
        player.PrintDetails();
    }
    else if (choice == "3")
    {
        break;
    }
}

Console.WriteLine();
if (player.Level >= 10)
    Console.WriteLine("Congratulations! You won The Game!");
else if (!player.IsAlive)
    Console.WriteLine("GAME OVER");
