using SkogsaventyretG;

var rng     = new Random();
var s       = new Spelare();
var logg    = new List<string> { "Du vaknar i byn. Skogen väntar..." };
var fas     = "byn";
Monster? monster = null;

void Logg(string msg)
{
    logg.Insert(0, msg);
    if (logg.Count > 8) logg.RemoveAt(logg.Count - 1);
}

void VisaLogg()
{
    Console.WriteLine("\n--- Händelselogg ---");
    foreach (var rad in logg)
        Console.WriteLine($"  {rad}");
}

void VisaStats()
{
    Console.WriteLine("=== SKOGSÄVENTYRET [G] ===");
    Console.WriteLine($"Dag {s.Dagar}  |  Level {s.Level}  |  XP {s.Xp}/{s.XpTillNästa}");
    Console.WriteLine($"HP {s.Hp}/{s.MaxHp}  |  Attack {s.Attack}  |  Försvar {s.Forsvar}");
    Console.WriteLine();
}

while (true)
{
    Console.Clear();
    VisaStats();

    if (fas == "game_over")
    {
        Console.WriteLine("💀 GAME OVER");
        Console.WriteLine($"Du föll i strid efter {s.Dagar} dagar. Level {s.Level}, {s.Xp} XP.");
        VisaLogg();
        Console.WriteLine("\nTryck Enter för att avsluta.");
        Console.ReadLine();
        break;
    }

    if (fas == "byn")
    {
        Console.WriteLine("🏠 BYN — Du är i säkerhet.");
        Console.WriteLine("1. Ut i skogen (1 dag)");
        Console.WriteLine("2. Vila och hela (1 dag)");
        Console.Write("\nVal: ");
        var val = Console.ReadLine()?.Trim();
        s.Dagar++;

        if (val == "1")
        {
            monster = MonsterFabrik.Slumpa(rng);
            fas     = "strid";
            Logg($"Dag {s.Dagar}: Du möter en {monster.Namn}! (HP: {monster.Hp})");
        }
        else
        {
            s.Hp = s.MaxHp;
            Logg($"Dag {s.Dagar}: Du vilar och återhämtar dig fullt.");
        }
    }
    else if (fas == "strid" && monster != null)
    {
        Console.WriteLine($"⚔️  STRID: {monster.Namn} (HP: {monster.Hp})");
        Console.WriteLine("1. Anfall");
        Console.WriteLine("2. Försvara dig");
        Console.WriteLine("3. Spring");
        Console.Write("\nVal: ");
        var val = Console.ReadLine()?.Trim();

        if (val == "1")
        {
            int skada   = Math.Max(1, s.Attack - monster.Forsvar);
            monster.Hp -= skada;
            Logg($"Du anfaller {monster.Namn} för {skada} skada. (Monster HP: {monster.Hp})");

            if (monster.Hp <= 0)
            {
                s.Xp += monster.Xp;
                Logg($"🗡️  {monster.Namn} besegrades! +{monster.Xp} XP (totalt: {s.Xp})");
                if (s.Xp >= s.XpTillNästa) s.LevelUp();
                fas     = "byn";
                monster = null;
            }
            else
            {
                int mskada = Math.Max(1, monster.Attack - s.Forsvar);
                s.Hp      -= mskada;
                Logg($"{monster.Namn} anfaller dig för {mskada} skada. (Ditt HP: {s.Hp})");
                if (s.Hp <= 0) fas = "game_over";
            }
        }
        else if (val == "2")
        {
            int mskada = (int)Math.Ceiling(monster.Attack / 2.0);
            s.Hp      -= mskada;
            Logg($"Du försvarar dig — {monster.Namn} gör bara {mskada} skada. (Ditt HP: {s.Hp})");
            if (s.Hp <= 0) fas = "game_over";
        }
        else
        {
            int mskada = rng.Next(1, monster.Attack + 1);
            s.Hp      -= mskada;
            Logg($"Du springer tillbaka till byn men tar {mskada} skada. (Ditt HP: {s.Hp})");
            if (s.Hp <= 0) fas = "game_over";
            else { fas = "byn"; monster = null; }
        }
    }

    VisaLogg();
}
