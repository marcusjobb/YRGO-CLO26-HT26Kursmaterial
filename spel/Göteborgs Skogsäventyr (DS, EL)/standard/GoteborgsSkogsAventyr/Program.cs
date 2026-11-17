

using GoteborgsSkogsAventyr;

class Program
{

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Vad heter du?");
            string namn = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("===== VÄLKOMMEN TILL GÖTEBORGS SKOGSÄVENTYR =====");
            Console.WriteLine();
            Console.WriteLine("Du vaknar upp mitt i Slottskogen utan en aning om hur du hamnade där.");
            Console.WriteLine("Göteborg är inte längre riktigt som du minns det.");
            Console.WriteLine();
            Console.WriteLine("Sälmonster stryker omkring i Slottsskogen, överförfriskade Chalmersstudenter");
            Console.WriteLine("lurar bakom varje hörn och någonstans där ute väntar Marcus med en inlämning...");
            Console.WriteLine();
            Console.WriteLine("Ditt mål är enkelt: överlev så många dagar du kan.");
            Console.WriteLine();
            Console.WriteLine("Kämpa mot monster, vila i Botaniska, samla guld och köp saker på Pressbyrån.");
            Console.WriteLine("Om du blir tillräckligt stark kanske du till och med vågar dig på den");
            Console.WriteLine("ultimata utmaningen – en hel vecka av Valborg.");
            Console.WriteLine();
            Console.WriteLine($"Lycka till {namn}! Du kommer behöva det.");
            Console.WriteLine();


            Spelare spelare = new Spelare(namn, 20, 20, 5, 2, 1, 0, 0, 0);
            shop pressbyrån = new shop();


            while (spelare.Hp > 0)
            {
                if (spelare.DagarÖverlevda == 1)
                {
                    Console.WriteLine();
                    Console.WriteLine("Ny dag! Du har överlevt 1 dag.");
                    Console.WriteLine("Vad vill du göra idag?");
                }
                else if (spelare.DagarÖverlevda > 1)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Ny dag! Du har överlevt {spelare.DagarÖverlevda} dagar.");
                    Console.WriteLine("Vad vill du göra idag?");
                }
                Console.WriteLine("===== DIN STATUS =====");
                Console.WriteLine($"Namn: {spelare.Namn}");
                Console.WriteLine($"HP: {spelare.Hp}/{spelare.MaxHp}");
                Console.WriteLine($"Level: {spelare.Level}");
                Console.WriteLine($"XP: {spelare.Xp}/{spelare.XpTillNästaLevel}");
                Console.WriteLine($"Guld: {spelare.Gold}");
                Console.WriteLine($"Dagar överlevda: {spelare.DagarÖverlevda}");
                Console.WriteLine();

                Console.WriteLine("===== HUVUDMENY =====");
                Console.WriteLine("1. Gå till Azalea och fightas (1 dag)");
                Console.WriteLine("2. Gå till Botaniska trädgården och vila (2 guld, 1 dag)");
                Console.WriteLine("3. Besök Pressbyrån på Linnéplatsen");
                Console.WriteLine("4. En hel vecka av Valborg");
                Console.WriteLine("5. Visa inventory");
                Console.WriteLine();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Random random = new Random();
                        int randomIndex = random.Next(3);

                        Monster monster;

                        switch (randomIndex)
                        {
                            case 0:
                                monster = new Monster.Sealmonster();
                                break;

                            case 1:
                                monster = new Monster.FullChalmersStudent();
                                break;

                            default:
                                monster = new Monster.MarcusMedInlämning();
                                break;
                        }

                        StartCombat(spelare, monster);
                        if (spelare.Hp > 0)
                        {
                            spelare.AddDay();
                        }
                        break;
                    case "2":
                        if (spelare.SpendGold(2))
                        {
                            spelare.Heal();
                            spelare.AddDay();
                        }
                        else
                        {
                            Console.WriteLine("Du behöver 2 guld för att vila i Botaniska trädgården.");
                        }
                        break;

                        break;
                    case "3":
                        pressbyrån.ShopMenu(spelare);
                        break;
                    case "4":
                        if (spelare.Level < 5)
                        {
                            Console.WriteLine("Du måste vara minst level 5 för att delta i Valborgsarenan.");
                            break;
                        }

                        Console.WriteLine();
                        Console.WriteLine("===== VARNING =====");
                        Console.WriteLine("Valborgsarenan pågår i 7 dagar.");
                        Console.WriteLine("När du väl har gått in kan du inte lämna arenan.");
                        Console.WriteLine("Vill du verkligen fortsätta? (ja/nej)");

                        bool validAnswer = false;

                        while (!validAnswer)
                        {
                            string arenaChoice = Console.ReadLine();

                            if (arenaChoice.ToLower() == "ja")
                            {
                                validAnswer = true;
                                StartArena(spelare);
                            }
                            else if (arenaChoice.ToLower() == "nej")
                            {
                                validAnswer = true;
                                Console.WriteLine("Du bestämmer dig för att inte gå in i arenan.");
                            }
                            else
                            {
                                Console.WriteLine("Ogiltigt svar. Svara ja eller nej.");
                            }
                        }

                        break;

                        break;
                    case "5":
                        spelare.ShowInventory();
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val. Välj 1, 2, 3, 4 eller 5.");
                        break;
                }


            }
            Console.WriteLine();
            Console.WriteLine("För att starta om spelet, tryck 1.");

            bool validRestart = false;

            while (!validRestart)
            {
                string restartChoice = Console.ReadLine();

                switch (restartChoice)
                {
                    case "1":
                        validRestart = true;
                        break;

                    default:
                        Console.WriteLine("Ogiltigt val. Tryck 1 för att starta om spelet.");
                        break;
                }
            }
        }

        }

    //Metod StartCombat
    static void StartCombat(Spelare spelare, Monster monster)
    {
        while (spelare.Hp > 0 && monster.HP > 0)
        {
            //Visa statistik
            Console.WriteLine($"Du möter {monster.Name}!");
            Console.WriteLine();
            Console.WriteLine($"===== STRID =====");
            Console.WriteLine(spelare.Namn);
            Console.WriteLine($"HP: {spelare.Hp}/{spelare.MaxHp}");
            Console.WriteLine($"Attack: {spelare.Attack}");
            Console.WriteLine($"Defense: {spelare.Defense}");
            Console.WriteLine();
            Console.WriteLine(monster.Name);
            Console.WriteLine($"HP: {monster.HP}/{monster.MaxHP}");
            Console.WriteLine($"Attack: {monster.Attack}");
            Console.WriteLine($"Defense: {monster.Defense}");
            Console.WriteLine();

            //Meny
         
            Console.WriteLine("1. Försvara");
            Console.WriteLine("2. Attackera");
            Console.WriteLine("3. Spring");
            Console.WriteLine();
            string choice = Console.ReadLine();

            //Vad som händer när du valt i meny
            switch (choice)
            {
                case "1":
                    int defenseDamage = monster.Attack - spelare.Defense;
                    defenseDamage = defenseDamage / 2;
                    if (defenseDamage < 1)
                    {
                        defenseDamage = 1;
                    }
                    spelare.TakeDamage(defenseDamage);
                    Console.WriteLine($"Du försvarar dig och tar {defenseDamage} i skada!");
                    break;
                case "2":

                    if (monster is Monster.FullChalmersStudent && spelare.HasItem("snus"))
                    {
                        Console.WriteLine("Du använder ditt snus mot Full Chalmers Student!");
                        Console.WriteLine("Chalmersstudenten ser ditt snus, rullar runt på backen och besegras!");

                        monster.TakeDamage(monster.HP);
                        spelare.GainXP(monster.XPReward);
                        spelare.AddGold(monster.DropGold());

                        break;
                    }

                    if (monster is Monster.MarcusMedInlämning)
                    {
                        if (!spelare.HasItem("Katt") || !spelare.HasItem("Taylor Swift Skiva"))
                        {
                            Console.WriteLine("Du kan inte besegra Marcus utan både Katt och Taylor Swift-skiva!");
                            monster.AttackPlayer(spelare);
                            break;
                        }

                        Console.WriteLine("Du använder Katt och Taylor Swift-skiva mot Marcus!");
                    }

                    int weaponBonus = 0;

                    if (spelare.EquippedWeapon != null)
                    {
                        weaponBonus = spelare.EquippedWeapon.Använd();
                    }

                    int damage = spelare.Attack + weaponBonus - monster.Defense;

                    if (damage < 1)
                    {
                        damage = 1;
                    }

                    bool monsterDied = monster.TakeDamage(damage);
                    Console.WriteLine($"Du attackerar {monster.Name} och gör {damage} skada!");

                    if (monsterDied)
                    {
                        spelare.GainXP(monster.XPReward);
                        spelare.AddGold(monster.DropGold());
                        Console.WriteLine($"Du besegrade {monster.Name}!");
                    }
                    else
                    {
                        monster.AttackPlayer(spelare);
                    }

                    break;
                case "3":
                    Random random = new Random();

                    int damageWhenRunning = random.Next(1, monster.Attack + 1);

                    // Fick hjälp av AI med syntax för att generera ett random tal.
                    spelare.TakeDamage(damageWhenRunning);
                    Console.WriteLine($"Du springer från {monster.Name} och tar {damageWhenRunning} i skada!");
                    // return används istället för break eftersom spelaren ska lämna hela striden, inte bara switchen.
                    return;
                default:
                    Console.WriteLine("Ogiltigt val. Välj 1, 2 eller 3.");
                    break;
            }
        }
        if (spelare.Hp <= 0)
        {
            Console.WriteLine($"Du blev dödad av {monster.Name} :(");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("===== GAME OVER =====");
            Console.WriteLine($"Namn: {spelare.Namn}");
            Console.WriteLine($"Dagar överlevda: {spelare.DagarÖverlevda}");
            Console.WriteLine($"Level: {spelare.Level}");
            Console.WriteLine($"XP: {spelare.Xp} / {spelare.XpTillNästaLevel}");
            Console.WriteLine($"Max HP: {spelare.MaxHp}");
            Console.WriteLine($"Attack: {spelare.Attack}");
            Console.WriteLine($"Defense: {spelare.Defense}");
            Console.WriteLine("=====================");
        }
    }
    //Metod StartArena
    static void StartArena(Spelare spelare)
    {
        List<Monster> arenaMonsters = new List<Monster>()
    {
        new Monster.Sealmonster(),
        new Monster.Sealmonster(),
        new Monster.Sealmonster(),
        new Monster.FullChalmersStudent(),
        new Monster.FullChalmersStudent(),
        new Monster.FullChalmersStudent(),
        new Monster.MarcusMedInlämning()
    };

        for (int i = 0; i < 7; i++)
        {
            Console.WriteLine();
            Console.WriteLine($"===== ARENADAG {i + 1} AV 7 =====");

            Monster monster = arenaMonsters[i];

            while (spelare.Hp > 0 && monster.HP > 0)
            {
                // Visa statistik
                Console.WriteLine($"Du möter {monster.Name}!");
                Console.WriteLine();
                Console.WriteLine("===== STRID =====");
                Console.WriteLine(spelare.Namn);
                Console.WriteLine($"HP: {spelare.Hp}/{spelare.MaxHp}");
                Console.WriteLine($"Attack: {spelare.Attack}");
                Console.WriteLine($"Defense: {spelare.Defense}");
                Console.WriteLine();
                Console.WriteLine(monster.Name);
                Console.WriteLine($"HP: {monster.HP}/{monster.MaxHP}");
                Console.WriteLine($"Attack: {monster.Attack}");
                Console.WriteLine($"Defense: {monster.Defense}");
                Console.WriteLine();

                // Meny
                Console.WriteLine("1. Försvara");
                Console.WriteLine("2. Attackera");
                Console.WriteLine();

                string choice = Console.ReadLine();

                // Vad som händer när du valt i menyn
                switch (choice)
                {
                    case "1":
                        int defenseDamage = monster.Attack - spelare.Defense;
                        defenseDamage = defenseDamage / 2;

                        if (defenseDamage < 1)
                        {
                            defenseDamage = 1;
                        }

                        spelare.TakeDamage(defenseDamage);
                        Console.WriteLine($"Du försvarar dig och tar {defenseDamage} i skada!");
                        break;

                    case "2":

                        if (monster is Monster.FullChalmersStudent && spelare.HasItem("snus"))
                        {
                            Console.WriteLine("Du använder ditt snus mot Full Chalmers Student!");
                            Console.WriteLine("Chalmersstudenten ser ditt snus, rullar runt på backen och besegras!");

                            monster.TakeDamage(monster.HP);
                            spelare.GainXP(monster.XPReward);
                            spelare.AddGold(monster.DropGold());

                            break;
                        }

                        if (monster is Monster.MarcusMedInlämning)
                        {
                            if (!spelare.HasItem("Katt") || !spelare.HasItem("Taylor Swift Skiva"))
                            {
                                Console.WriteLine("Du kan inte besegra Marcus utan både Katt och Taylor Swift-skiva!");
                                monster.AttackPlayer(spelare);
                                break;
                            }

                            Console.WriteLine("Du använder Katt och Taylor Swift-skiva mot Marcus!");
                        }

                        int weaponBonus = 0;

                        if (spelare.EquippedWeapon != null)
                        {
                            weaponBonus = spelare.EquippedWeapon.Använd();
                        }

                        int damage = spelare.Attack + weaponBonus - monster.Defense;

                        if (damage < 1)
                        {
                            damage = 1;
                        }

                        bool monsterDied = monster.TakeDamage(damage);
                        Console.WriteLine($"Du attackerar {monster.Name} och gör {damage} skada!");

                        if (monsterDied)
                        {
                            spelare.GainXP(monster.XPReward);
                            Console.WriteLine($"Du besegrade {monster.Name}!");
                            spelare.AddGold(monster.DropGold());
                        }
                        else
                        {
                            monster.AttackPlayer(spelare);
                        }

                        break;
                    default:
                        Console.WriteLine("Ogiltigt val. Välj 1 eller 2.");
                        break;
                }
            }

            // Om spelaren dog avslutas hela Arenan
            if (spelare.Hp <= 0)
            {
                Console.WriteLine($"Du blev dödad av {monster.Name} :(");
                Console.WriteLine();
                Console.WriteLine("===== GAME OVER =====");
                Console.WriteLine($"Namn: {spelare.Namn}");
                Console.WriteLine($"Dagar överlevda: {spelare.DagarÖverlevda}");
                Console.WriteLine($"Level: {spelare.Level}");
                Console.WriteLine($"XP: {spelare.Xp} / {spelare.XpTillNästaLevel}");
                Console.WriteLine($"Max HP: {spelare.MaxHp}");
                Console.WriteLine($"Attack: {spelare.Attack}");
                Console.WriteLine($"Defense: {spelare.Defense}");
                Console.WriteLine("=====================");

                return;
            }

            // Spelaren överlevde arenadagen
            spelare.AddDay();
            Console.WriteLine($"Du överlevde arenadag {i + 1} av 7!");
            Console.WriteLine();
        }
        Console.WriteLine("===== DU ÖVERLEVDE VALBORG =====");
        Console.WriteLine("Efter sju långa dagar står du fortfarande upp.");
        Console.WriteLine("Chalmersstudenterna darrar. Göteborg jublar.");
        spelare.GainXP(50);

        Console.WriteLine("🏆 Valborgsbelöning 🏆");
        Console.WriteLine("Du får 50 XP i bonus!");
        Console.WriteLine($"Du har nu {spelare.Xp} XP.");
    }
}