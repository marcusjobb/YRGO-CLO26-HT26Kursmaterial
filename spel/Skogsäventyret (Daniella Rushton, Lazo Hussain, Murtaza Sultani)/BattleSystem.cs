class BattleSystem
{
    public static bool RunBattle(Player player, Monster monster, string playerName)
    {
        bool battleOver = false;

        while (!battleOver)
        {
            Console.WriteLine($"----- {playerName}: {player.Hp} Hp | {monster.MonsterName}: {monster.Hp} Hp -----");

            Console.WriteLine("1) Attack");
            Console.WriteLine("2) Defend");
            Console.WriteLine("3) Run");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                // Player attacks Monster
                int damageToMonster = player.Attack - monster.Defense;
                if (damageToMonster < 1)
                {
                    damageToMonster = 1;
                }
                Console.WriteLine($"{playerName} attacks {monster.MonsterName} dealing {damageToMonster} damage.");
                bool monsterDied = monster.TakeDamage(damageToMonster);


                if (monsterDied)
                {
                    Console.WriteLine($"{playerName} defeated {monster.MonsterName}");
                    player.GainXp(monster.Xp);
                    int goldDropped = monster.GoldDrop();
                    player.GainGold(goldDropped);
                    Console.WriteLine($"{playerName} earned {goldDropped} gold.");
                    return true;
                }


                else
                {
                    bool playerDied = monster.AttackPlayer(player);
                    if (playerDied)
                    {
                        Console.WriteLine($"{playerName} was defeated.");
                        return false;
                    }
                }

            }
            else if (choice == "2")
            {
                // Defend against Monster, take half damage
                int damageTaken = monster.Attack / 2;
                Console.WriteLine($"{playerName} defended themselves and took {damageTaken} damage.");

                bool playerDied = player.TakeDamage(damageTaken); // Player might die when defending
                if (playerDied)
                {
                    Console.WriteLine($"{playerName} was defeated.");
                    return false;
                }

            }
            else if (choice == "3")
            {
                // Run from battle, take random damage
                Random random = new Random();
                int damageTaken = random.Next(1, monster.Attack + 1);

                Console.WriteLine($"{playerName} runs from the battle and take {damageTaken} damage.");

                bool playerDied = player.TakeDamage(damageTaken); // Player might die when running
                if (playerDied)
                {
                    Console.WriteLine($"{playerName} was defeated.");
                }
                return !playerDied;
            }
            else
            {
                Console.WriteLine("Invalid input, please try again");
                continue;
            }
        }
        return true;
    }
}
    