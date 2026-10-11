using Havsforskarna_VG;

const int STORLEK = 10;
var       rng     = new Random();

// -------- hjälpfunktioner --------

char[,] NyttHav() => new char[STORLEK, STORLEK];

void PlaceraFormation(Formation f, char[,] hav)
{
    bool horisontell;
    int  r, k;
    do
    {
        horisontell = rng.Next(2) == 0;
        r = horisontell ? rng.Next(STORLEK)                : rng.Next(STORLEK - f.Storlek + 1);
        k = horisontell ? rng.Next(STORLEK - f.Storlek + 1) : rng.Next(STORLEK);

        f.Pos.Clear();
        for (int i = 0; i < f.Storlek; i++)
        {
            int pr = horisontell ? r     : r + i;
            int pk = horisontell ? k + i : k;
            f.Pos.Add((pr, pk));
        }
    } while (f.Pos.Any(p => hav[p.r, p.k] != '\0'));

    foreach (var p in f.Pos) hav[p.r, p.k] = 'F';
}

void VisaHav(char[,] synlig, string rubrik)
{
    Console.WriteLine($"\n  {rubrik}");
    Console.Write("    ");
    for (int k = 0; k < STORLEK; k++) Console.Write($" {(char)('A' + k)}");
    Console.WriteLine();
    for (int r = 0; r < STORLEK; r++)
    {
        Console.Write($" {r,2} |");
        for (int k = 0; k < STORLEK; k++)
            Console.Write(synlig[r, k] switch { 'X' => " X", 'O' => " ·", _ => " ~" });
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
                Console.WriteLine($"  ✅ {f.Namn} är kartlagd (sjunken)!");
            return true;
        }
    }
    synlig[r, k] = 'O';
    return false;
}

// -------- smart AI-logik --------

// Datorns AI: jagar runtom en träff tills formationen är kartlagd, sedan reset
var aiKö        = new Queue<(int r, int k)>();
var aiProbade   = new HashSet<(int, int)>();
bool aiJägar    = false;

(int r, int k) AiVälj(char[,] spelareSynlig)
{
    // Ta nästa ur kön om vi jägar
    while (aiKö.Count > 0)
    {
        var kandidat = aiKö.Dequeue();
        if (spelareSynlig[kandidat.r, kandidat.k] == '\0' && aiProbade.Add(kandidat))
            return kandidat;
    }

    // Annars slumpa ett otarget ruta
    int r, k;
    do { r = rng.Next(STORLEK); k = rng.Next(STORLEK); }
    while (spelareSynlig[r, k] != '\0' || !aiProbade.Add((r, k)));
    return (r, k);
}

void AiRegistreraResultat(int r, int k, bool träff, char[,] spelareSynlig)
{
    if (!träff) return;
    // Lägg omgivande rutor i kön
    (int dr, int dk)[] grannar = [(−1, 0), (1, 0), (0, −1), (0, 1)];
    foreach (var (dr, dk) in grannar)
    {
        int nr = r + dr, nk = k + dk;
        if (nr >= 0 && nr < STORLEK && nk >= 0 && nk < STORLEK
            && spelareSynlig[nr, nk] == '\0')
            aiKö.Enqueue((nr, nk));
    }
}

void AiResetOmKartlagd(List<Formation> formationer)
{
    // Om en formation precis sjönk: töm kön och börja om
    if (formationer.Any(f => f.ÄrKartlagd()))
        aiKö.Clear();
}

// -------- spelbräde --------

var spelareHav    = NyttHav();
var datornHav     = NyttHav();
var spelareSynlig = NyttHav();
var datornSynlig  = NyttHav();

var spelarFormationer = new List<Formation>
{
    new("Blåval",      5),
    new("Hammarhajen", 4),
    new("Delfinflock", 3),
    new("Bläckfisk",   2),
};
var datornFormationer = new List<Formation>
{
    new("Blåval",      5),
    new("Hammarhajen", 4),
    new("Delfinflock", 3),
    new("Bläckfisk",   2),
};

foreach (var f in spelarFormationer) PlaceraFormation(f, spelareHav);
foreach (var f in datornFormationer) PlaceraFormation(f, datornHav);

int omgång = 1;

while (true)
{
    Console.Clear();
    Console.WriteLine($"=== HAVSFORSKARNA [VG] — omgång {omgång} ===");
    VisaHav(datornSynlig,  "Ditt mål (oceanens okända)");
    VisaHav(spelareSynlig, "Din flotta");
    Console.WriteLine();

    // ---- spelarens tur ----
    (int r, int k) mål;
    while (true)
    {
        Console.Write("Kartlägg ruta (t.ex. B3): ");
        var inn = Console.ReadLine()?.Trim().ToUpper() ?? "";
        if (inn.Length >= 2
            && inn[0] >= 'A' && inn[0] < 'A' + STORLEK
            && int.TryParse(inn[1..], out int rad) && rad >= 0 && rad < STORLEK
            && datornSynlig[rad, inn[0] - 'A'] == '\0')
        {
            mål = (rad, inn[0] - 'A');
            break;
        }
        Console.WriteLine("  Ogiltig ruta — försök igen.");
    }

    bool sTräff = Träff(datornFormationer, mål.r, mål.k, datornSynlig);
    Console.WriteLine(sTräff ? "  🎯 TRÄFF!" : "  💦 Miss.");

    if (datornFormationer.All(f => f.ÄrKartlagd()))
    {
        Console.Clear();
        VisaHav(datornSynlig, "Ditt mål");
        Console.WriteLine($"\n🏆 DU VANN på {omgång} omgångar! Alla formationer kartlagda.");
        break;
    }

    // ---- datorns tur (smart AI) ----
    var (dr, dk) = AiVälj(spelareSynlig);
    bool dTräff  = Träff(spelarFormationer, dr, dk, spelareSynlig);
    Console.WriteLine($"\n  Datorns sonar: {(char)('A' + dk)}{dr} — {(dTräff ? "🎯 TRÄFF!" : "💦 Miss.")}");

    AiRegistreraResultat(dr, dk, dTräff, spelareSynlig);
    AiResetOmKartlagd(spelarFormationer);

    if (spelarFormationer.All(f => f.ÄrKartlagd()))
    {
        Console.Clear();
        VisaHav(spelareSynlig, "Din flotta");
        Console.WriteLine($"\n💀 GAME OVER — datorns smarta sonar kartlade hela din flotta.");
        break;
    }

    omgång++;
    Console.Write("\nTryck Enter för nästa omgång...");
    Console.ReadLine();
}
