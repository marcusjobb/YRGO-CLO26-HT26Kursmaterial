class Arena
{
    // Run arena, 7 monsters sorted by difficulty. Cannot run
    public static bool RunArena(Player player, string playerName, List<string>defeatedMonsters)
    {
        List<Monster> arenaMonsters = new List<Monster>
        {
            new Goblin(),
            new Goblin(),
            new Orc(),
            new Orc(),
            new Orc(),
            new Ogre(),
            new Ogre()
        };

        int round = 1;
        foreach (Monster monster in arenaMonsters)
        {
            Console.WriteLine($"-------- Arena Round {round}/7 --------");
            Console.WriteLine($"{playerName} faces {monster.MonsterName}");

            bool playerSurvived = BattleSystem.RunBattle(player, monster, playerName);

            if (!playerSurvived)
            {
                Console.WriteLine($"{playerName} was defeated in the Arena");
                return false; // Player lost the arena
            }

            defeatedMonsters.Add(monster.MonsterName);
            round++;

        }
        Console.WriteLine($"{playerName} survived 7 days in the Arena");
        player.GainXp(100); // Player earns 100 XP for surviving 7 days
        return true; // Player won the arena
    }
}