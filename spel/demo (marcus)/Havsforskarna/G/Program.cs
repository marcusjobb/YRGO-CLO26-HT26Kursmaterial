using Havsforskarna_G;

const int  STORLEK = 10;
var        rng     = new Random();

// -------- hjälpfunktioner --------

char[,] NyttHav() => new char[STORLEK, STORLEK];

void PlaceraFormation(Formation f, char[,] hav)
{
    bool horisontell;
    int  r, k;
    do
    {
        horisontell = rng.Next(2) == 0;
        r = horisontell ? rng.Next(STORLEK)              : rng.Next(STORLEK - f.Storlek + 1);
        k = horisontell ? rng.Next(STORLEK - f.Storlek + 1) : rng.Next(STORLEK);

        f.Pos.Clear();
        for (int i = 0; i < f.Storlek; i++)
        {
            int pr = horisontell ? r     : r + i;
            int pk = horisontell ? k + i : k;
            f.Pos.Add((pr, pk));
        }
    } while (f.Pos.Any(p => hav[p.r, p.k] != '\0'));

    foreach (var p in f.Pos)
        hav[p.r, p.k] = 'F';
}

void VisaHav(char[,] dolt, char[,] synlig, string rubrik)
{
    Console.WriteLine($"\n  {rubrik}");
    Console.Write("    ");
    for (int k = 0; k < STORLEK; k++) Console.Write($" {(char)('A' + k)}");
    Console.WriteLine();
    for (int r = 0; r < STORLEK; r++)
    {
        Console.Write($" {r,2} |");
        for (int k = 0; k < STORLEK; k++)
        {
            char c = synlig[r, k];
            Console.Write(c switch
            {
                'X' => " X",
                'O' => " ·",
                _   => " ~"
            });
        }
        Console.WriteLine();
    }
}

bool Träff(List<Formation> formationer, int r, int k, char[,] synlig)
{
    foreach (var f in formationer)
    {
        if (f.RegistreraTräff((r, k)))
        {
            synlig[r, k] = 'X';
            if (f.ÄrKartlagd())
                Console.WriteLine($"  ✅ {f.Namn} är kartlagd!");
            return true;
        }
    }
    synlig[r, k] = 'O';
    return false;
}

// -------- spelbräde --------

var spelareHav      = NyttHav();
var datornHav       = NyttHav();
var spelareSynlig   = NyttHav();
var datornSynlig    = NyttHav();    // spelarens vy av datorns hav

var spelarFormationer = new List<Formation>
{
    new("Blåval",           5),
    new("Hammarhajen",      4),
    new("Delfinflock",      3),
    new("Bläckfisk",        2),
};

var datornFormationer = new List<Formation>
{
    new("Blåval",           5),
    new("Hammarhajen",      4),
    new("Delfinflock",      3),
    new("Bläckfisk",        2),
};

foreach (var f in spelarFormationer) PlaceraFormation(f, spelareHav);
foreach (var f in datornFormationer) PlaceraFormation(f, datornHav);

int omgång = 1;

while (true)
{
    Console.Clear();
    Console.WriteLine($"=== HAVSFORSKARNA [G] — omgång {omgång} ===");
    VisaHav(datornHav,   datornSynlig,   "Ditt mål (oceanens okända)");
    VisaHav(spelareHav,  spelareSynlig,  "Din flotta");
    Console.WriteLine();

    // ---- spelararens tur ----
    (int r, int k) spalareMål;
    while (true)
    {
        Console.Write("Kartlägg ruta (t.ex. B3): ");
        var inmatning = Console.ReadLine()?.Trim().ToUpper() ?? "";
        if (inmatning.Length >= 2
            && inmatning[0] >= 'A' && inmatning[0] < 'A' + STORLEK
            && int.TryParse(inmatning[1..], out int rad) && rad >= 0 && rad < STORLEK
            && datornSynlig[rad, inmatning[0] - 'A'] == '\0')
        {
            spalareMål = (rad, inmatning[0] - 'A');
            break;
        }
        Console.WriteLine("  Ogiltig ruta — försök igen.");
    }

    bool sträff = Träff(datornFormationer, spalareMål.r, spalareMål.k, datornSynlig);
    Console.WriteLine(sträff ? "  🎯 TRÄFF!" : "  💦 Miss.");

    if (datornFormationer.All(f => f.ÄrKartlagd()))
    {
        Console.Clear();
        VisaHav(datornHav, datornSynlig, "Ditt mål");
        Console.WriteLine($"\n🏆 DU VANN på {omgång} omgångar! Alla formationer kartlagda.");
        break;
    }

    // ---- datorns tur ----
    int dr, dk;
    do { dr = rng.Next(STORLEK); dk = rng.Next(STORLEK); }
    while (spelareSynlig[dr, dk] != '\0');

    bool dträff = Träff(spelarFormationer, dr, dk, spelareSynlig);
    Console.WriteLine($"\n  Datorns skott: {(char)('A' + dk)}{dr} — {(dträff ? "🎯 TRÄFF på din flotta!" : "💦 Miss.")}");

    if (spelarFormationer.All(f => f.ÄrKartlagd()))
    {
        Console.Clear();
        VisaHav(spelareHav, spelareSynlig, "Din flotta");
        Console.WriteLine($"\n💀 GAME OVER — datorns sonar kartlade hela din flotta.");
        break;
    }

    omgång++;
    Console.Write("\nTryck Enter för nästa omgång...");
    Console.ReadLine();
}
