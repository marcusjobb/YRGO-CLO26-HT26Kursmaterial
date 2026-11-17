
Console.WriteLine("What's your name?");
string playerName = Console.ReadLine();
Player player = new Player(playerName, 30, 20, 15);

int day = 1;
bool playing = true;

List<string> defeatedMonsters = new List<string>(); // List that stores defeated monsters

while (playing)
{
    Console.WriteLine($"---------------Day {day}---------------");
    Console.WriteLine("----- What do you want to do? -----");
    Console.WriteLine("1) Go into the forest");
    Console.WriteLine("2) Rest");
    Console.WriteLine("3) Go into the arena");
    Console.WriteLine("4) Visit the weapons shop");


    string playingChoice = Console.ReadLine();

    if (playingChoice == "1") // Go into forest --> battle
    {
        MonsterSpawner spawner = new MonsterSpawner();
        Monster monster = spawner.SpawnRandomMonster();
        Console.WriteLine($"{playerName} encounters one {monster.MonsterName}");

        // Run battle - true if player won or ran, false if player died
        bool playerSurvived = BattleSystem.RunBattle(player, monster, playerName);

        if (playerSurvived)
        {
            defeatedMonsters.Add(monster.MonsterName); //Adds defeated monster to list
        }

        if (player.Hp <= 0) // When player dies, shows stats
        {
            Console.WriteLine("-------- Game Over --------");
            Console.WriteLine($"Days survived: {day}");
            Console.WriteLine($"Level reached: {player.Level}");
            Console.WriteLine($"Total XP:      {player.Xp}");
            Console.WriteLine();
            Console.WriteLine("----- Defeated Monsters -----");
            foreach (string defeatedMonster in defeatedMonsters) // Shows defeated monsters
            {
                Console.WriteLine(defeatedMonster);
            }
            playing = false;
        }
        else
        {
            day++; // Player survived, one day elapsed
        }


    }
    else if (playingChoice == "2") // Rest
    {
        Console.WriteLine($"{playerName} rests and restores Hp");
        player.Heal();
        day++;
    }

    else if (playingChoice == "3") //Arena  
    {
        if (day < 7) // Player must be level 7 to enter arena
        {
            Console.WriteLine("You must survive 7 days to enter the arena");
        }
        else
        {
            Console.WriteLine($"{playerName} walks into the arena");
            bool survivedArena = Arena.RunArena(player, playerName, defeatedMonsters);

            if (player.Hp <= 0) // When player dies, shows stats
            {
                Console.WriteLine("-------- Game Over --------");
                Console.WriteLine($"Days survived: {day}");
                Console.WriteLine($"Level reached: {player.Level}");
                Console.WriteLine($"Total XP:      {player.Xp}");
                Console.WriteLine();
                Console.WriteLine("----- Defeated Monsters -----");
                foreach (string defeatedMonster in defeatedMonsters) // Shows defeated monsters
                {
                    Console.WriteLine(defeatedMonster);
                }
                playing = false;
            }
            else
            {
                day += 7; // Arena takes 7 days to complete
            }
        }
    }
    else if (playingChoice == "4") // Weapons shop
    {
        
        Shop shop = new Shop();
        shop.ShowShop(player);
    }
}