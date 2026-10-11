using SkogsaventyretVG;

var rng   = new Random();
var s     = new Spelare();
var logg  = new List<string> { "Du vaknar i byn. Skogen väntar..." };
var fas   = "byn";
var arenaRunda = 0;
Monster? monster = null;

Vapen[] vapenSortiment = [new Spjut(), new Svärd(), new Stridsyxa()];

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
    Console.WriteLine("=== SKOGSÄVENTYRET [VG] ===");
    Console.Write($"Dag {s.Dagar}  |  Level {s.Level}  |  XP {s.Xp}/{s.XpTillNästa}  |  Guld {s.Guld}g");
    if (s.Vapen != null) Console.Write($"  |  Vapen: {s.Vapen.Namn}");
    Console.WriteLine();
    Console.WriteLine($"HP {s.Hp}/{s.MaxHp}  |  Attack {s.Attack}  |  Försvar {s.Forsvar}");
    Console.WriteLine();
}

void HanteraStrid(bool ärArena)
{
    if (monster == null) return;

    Console.WriteLine($"⚔️  {(ärArena ? $"ARENAN — Runda {arenaRunda}/7: " : "STRID: ")}{monster.Namn} (HP: {monster.Hp})");
    Console.WriteLine("1. Anfall");
    Console.WriteLine("2. Försvara dig");
    if (!ärArena) Console.WriteLine("3. Spring");
    Console.Write("\nVal: ");
    var val = Console.ReadLine()?.Trim();

    if (val == "1")
    {
        int skada   = Math.Max(1, s.Attack - monster.Forsvar);
        monster.Hp -= skada;
        Logg($"Du anfaller {monster.Namn} för {skada} skada. (Monster HP: {monster.Hp})");

        if (monster.Hp <= 0)
        {
            int guld = rng.Next(5, 16);
            s.Xp   += monster.Xp;
            s.Guld += guld;
            Logg($"🗡️  {monster.Namn} besegrades! +{monster.Xp} XP, +{guld} guld");
            if (s.Xp >= s.XpTillNästa) s.LevelUp();

            if (ärArena)
            {
                arenaRunda++;
                if (arenaRunda > 7)
                {
                    s.Xp += 100;
                    Logg("🏆 Du klarade arenan! +100 bonus-XP!");
                    fas = "byn"; monster = null;
                }
                else
                {
                    monster = MonsterFabrik.ArenaMonster(arenaRunda);
                    Logg($"⚔️  Runda {arenaRunda}/7: {monster.Namn} (HP: {monster.Hp})");
                }
            }
            else { fas = "byn"; monster = null; }
        }
        else
        {
            int mskada = Math.Max(1, monster.Attack - s.Forsvar);
            s.Hp      -= mskada;
            Logg($"{monster.Namn} svarar med {mskada} skada. (Ditt HP: {s.Hp})");
            if (s.Hp <= 0) fas = "game_over";
        }
    }
    else if (val == "2")
    {
        int mskada = (int)Math.Ceiling(monster.Attack / 2.0);
        s.Hp      -= mskada;
        Logg($"Du försvarar dig — {monster.Namn} gör {mskada} skada. (Ditt HP: {s.Hp})");
        if (s.Hp <= 0) fas = "game_over";
    }
    else if (val == "3" && !ärArena)
    {
        int mskada = rng.Next(1, monster.Attack + 1);
        s.Hp      -= mskada;
        Logg($"Du flyr men tar {mskada} skada. (Ditt HP: {s.Hp})");
        if (s.Hp <= 0) fas = "game_over";
        else { fas = "byn"; monster = null; }
    }
    else if (ärArena)
    {
        Logg("Du kan inte fly från arenan!");
    }
}

while (true)
{
    Console.Clear();
    VisaStats();

    if (fas == "game_over")
    {
        Console.WriteLine("💀 GAME OVER");
        Console.WriteLine($"Du föll i strid efter {s.Dagar} dagar. Level {s.Level}, {s.Xp} XP, {s.Guld} guld.");
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
        Console.WriteLine("3. Vapenshopen");
        if (s.Level >= 3) Console.WriteLine("4. Arenan (7 dagar)");
        else              Console.WriteLine("   (Arenan kräver level 3)");
        Console.Write("\nVal: ");
        var val = Console.ReadLine()?.Trim();

        if (val == "1")
        {
            s.Dagar++;
            monster = MonsterFabrik.Slumpa(rng);
            fas     = "strid";
            Logg($"Dag {s.Dagar}: Du möter en {monster.Namn}! (HP: {monster.Hp})");
        }
        else if (val == "2")
        {
            s.Dagar++;
            s.Hp = s.MaxHp;
            Logg($"Dag {s.Dagar}: Du vilar och återhämtar dig fullt.");
        }
        else if (val == "3")
        {
            fas = "shop";
        }
        else if (val == "4" && s.Level >= 3)
        {
            s.Dagar  += 7;
            arenaRunda = 1;
            monster    = MonsterFabrik.ArenaMonster(1);
            fas        = "arena";
            Logg($"⚔️  Du kliver in i arenan! Runda 1/7: {monster.Namn}");
        }
        else if (val == "4")
        {
            Logg("Du behöver minst level 3 för att delta i arenan.");
        }
    }
    else if (fas == "shop")
    {
        Console.WriteLine($"🛒 VAPENSHOPEN — Du har {s.Guld} guld.");
        Console.WriteLine("0. Tillbaka");
        for (int i = 0; i < vapenSortiment.Length; i++)
        {
            var v  = vapenSortiment[i];
            var råd = s.Guld >= v.Pris ? "" : " (ej råd)";
            Console.WriteLine($"{i + 1}. {v.Namn} (+{v.AttackBonus} attack) — {v.Pris}g{råd}");
        }
        Console.Write("\nVal: ");
        var val = Console.ReadLine()?.Trim();

        if (val == "0") { fas = "byn"; }
        else if (int.TryParse(val, out int idx) && idx >= 1 && idx <= vapenSortiment.Length)
        {
            var valt = vapenSortiment[idx - 1];
            if (s.Guld >= valt.Pris)
            {
                int gammaltBonus = s.Vapen?.AttackBonus ?? 0;
                s.Guld   -= valt.Pris;
                s.Attack  = s.Attack - gammaltBonus + valt.AttackBonus;
                s.Vapen   = valt;
                fas       = "byn";
                Logg($"Du köper {valt.Namn} (+{valt.AttackBonus} attack).");
            }
            else Logg("Inte tillräckligt med guld.");
        }
    }
    else if (fas == "strid")  HanteraStrid(ärArena: false);
    else if (fas == "arena")  HanteraStrid(ärArena: true);

    VisaLogg();
}
