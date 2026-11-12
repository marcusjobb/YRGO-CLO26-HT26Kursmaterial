using System.Text;

// ---------------------------------------------------------------------------
//  StridsEffekter – roliga visuella effekter för striderna.
//  Allt är "static", så du anropar t.ex. StridsEffekter.Anfall(4);
//  utan att skapa något objekt.
// ---------------------------------------------------------------------------
static class StridsEffekter {

    static readonly Random slump = new Random();

    // Ändra till 0 om du vill stänga av alla animationer (bra när du testar)
    public static int Hastighet = 1;

    // true  = rader skrivs över med animation (vanlig terminal / cmd / Windows Terminal)
    // false = enkla rader utan animation (om konsolen inte klarar det, t.ex. vissa IDE-fönster)
    // Blir false automatiskt om utskriften är omdirigerad. Du kan också sätta den själv.
    public static bool Animera = !Console.IsOutputRedirected;

    static void Vänta(int ms) {
        if (Hastighet > 0) Thread.Sleep(ms * Hastighet);
    }

    // ------------------------------------------------------------------
    //  Körs en gång i början så att tecken som █ ░ ═ visas rätt
    // ------------------------------------------------------------------
    public static void Init() {
        try { Console.OutputEncoding = Encoding.UTF8; }
        catch { /* vissa konsoler tillåter inte detta – då kör vi vidare ändå */ }
    }

    // ------------------------------------------------------------------
    //  Hjälpmetod: skriver text bokstav för bokstav (skrivmaskinseffekt)
    // ------------------------------------------------------------------
    public static void SkrivLångsamt(string text, ConsoleColor färg, int msPerTecken = 25) {
        Console.ForegroundColor = färg;
        foreach (char c in text) {
            Console.Write(c);
            Vänta(msPerTecken);
        }
        Console.ResetColor();
        Console.WriteLine();
    }

    // ------------------------------------------------------------------
    //  1. MÖTE – blinkande varning + ASCII-porträtt av fienden
    // ------------------------------------------------------------------
    public static void Möte(Monster fiende) {
        Console.WriteLine();

        // Blinkande varningsrad (skrivs över sig själv med \r)
        if (Animera) {
            for (int i = 0; i < 6; i++) {
                Console.ForegroundColor = i % 2 == 0 ? ConsoleColor.Red : ConsoleColor.Yellow;
                Console.Write("\r   !!!  ETT OPLANERAT MÖTE BOKAS IN  !!!   ");
                Vänta(150);
            }
        }
        else {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("   !!!  ETT OPLANERAT MÖTE BOKAS IN  !!!   ");
        }
        Console.ResetColor();
        Console.WriteLine("\n");

        // Porträttet ritas upp rad för rad
        Console.ForegroundColor = fiende.Färg;
        foreach (string rad in Porträtt(fiende)) {
            Console.WriteLine("            " + rad);
            Vänta(70);
        }
        Console.ResetColor();
        Console.WriteLine();

        SkrivLångsamt($"   >> {fiende.Namn.ToUpper()} har skickat en kalenderinbjudan: \"Snabb avstämning (2h)\"",
                      ConsoleColor.Gray, 15);
        Console.WriteLine();
    }

    static string[] Porträtt(Monster fiende) {
        return fiende switch {
            Mellanchef => new[] {
                @"  .-------.  ",
                @"  | o   o |  ",
                @"  |   >   |  ",
                @"  |  ===  |  ",
                @"  '---Y---'  ",
                @"     /|\     ",
                @"    / | \    ",
                @"   [SLIPS]   ",
            },
            Flatearther => new[] {
                @"      /\      ",
                @"     /  \     ",
                @"    /FOIL\    ",
                @"   /______\   ",
                @"   ( @  @ )   ",
                @"    \ ~~ /    ",
                @"     '--'     ",
                @"  JORDEN = O  ",
            },
            Karen => new[] {
                @"   .~~~~~~~~~.   ",
                @"  (~~~~~~~~~~~)  ",
                @"  (| >     < |)  ",
                @"   |    ^    |   ",
                @"   |  \___/  |   ",
                @"    \_______/    ",
                @"  'CHEFEN. NU.'  ",
            },
            _ => new[] {
                @"   .----.   ",
                @"   | ?? |   ",
                @"   '----'   ",
            },
        };
    }

    // ------------------------------------------------------------------
    //  2. STATUSPANEL – snygga HP-mätare för både dig och fienden
    // ------------------------------------------------------------------
    public static void Statuspanel(Player spelare, Monster fiende) {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("╔══════════════════════════════════════════════╗");
        Console.ResetColor();
        HpBar(spelare.Namn, spelare.Hp, spelare.MaxHp, ConsoleColor.Cyan);
        HpBar(fiende.Namn, fiende.Hp, fiende.MaxHp, fiende.Färg);
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("╚══════════════════════════════════════════════╝");
        Console.ResetColor();
    }

    public static void HpBar(string namn, int hp, int maxHp, ConsoleColor namnFärg) {
        const int bredd = 20;
        int visadHp = Math.Clamp(hp, 0, maxHp);           // aldrig minus-HP i mätaren
        double andel = maxHp > 0 ? (double)visadHp / maxHp : 0;
        int fyllt = (int)Math.Round(andel * bredd);

        // Grön > 60 %, gul > 30 %, annars röd
        ConsoleColor barFärg = andel > 0.6 ? ConsoleColor.Green
                             : andel > 0.3 ? ConsoleColor.Yellow
                             : ConsoleColor.Red;

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("║ ");
        Console.ForegroundColor = namnFärg;
        Console.Write($"{namn,-10}");
        Console.ResetColor();
        Console.Write(" [");
        Console.ForegroundColor = barFärg;
        Console.Write(new string('█', fyllt));
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write(new string('░', bredd - fyllt));
        Console.ResetColor();
        Console.Write($"] {visadHp,3}/{maxHp,-3}");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("    ║");
        Console.ResetColor();
    }

    // ------------------------------------------------------------------
    //  3. ANFALL – en projektil flyger över skärmen och smäller in
    // ------------------------------------------------------------------
    static readonly string[] SmällOrd = {
        "BAM!", "POW!", "KAPOW!", "REPLY-ALL!", "CC:A VD:N!", "POWERPOINT!", "SYNERGI!", "KPI-SMASH!"
    };

    public static void Anfall(int skada) {
        const int sträcka = 28;
        if (Animera) {
            for (int i = 0; i <= sträcka; i++) {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\r  (ò_ó)o " + new string(' ', i) + "=>" + new string(' ', sträcka - i) + "  ");
                Vänta(12);
            }
        }

        string ord = SmällOrd[slump.Next(SmällOrd.Length)];
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write((Animera ? "\r" : "") + "  (ò_ó)o " + new string('-', sträcka) + "=> *" + ord + "*");
        Console.ResetColor();
        Console.WriteLine();

        SkadeSiffra(skada, ConsoleColor.Yellow, "  ", "");
    }

    // ------------------------------------------------------------------
    //  4. TRÄFF – när fienden slår dig: röd blink + "skakning"
    // ------------------------------------------------------------------

    // Blinkar och skakar en skadesiffra fram och tillbaka på samma rad
    static void SkadeSiffra(int skada, ConsoleColor färg, string före, string efter) {
        int[] skak = { 6, 2, 8, 3, 7, 4, 5 };
        string text = $"{före}-{skada}{efter}";
        if (Animera) {
            foreach (int x in skak) {
                Console.ForegroundColor = x % 2 == 0 ? färg : ConsoleColor.White;
                Console.Write("\r" + new string(' ', 30) + "\r" + new string(' ', x) + text);
                Vänta(45);
            }
            Console.Write("\r" + new string(' ', 30) + "\r");
        }
        Console.ForegroundColor = färg;
        Console.Write(new string(' ', 5) + text);
        Console.ResetColor();
        Console.WriteLine("\n");
    }

    // ------------------------------------------------------------------
    //  6. FLYKT – du springer iväg och lämnar ett dammoln efter dig
    // ------------------------------------------------------------------
    public static void Flykt() {
        const int sträcka = 30;
        if (!Animera) {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(new string(' ', sträcka - 8) + new string('~', 8));
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" ε=ε=┌( >_<)┘");
        }
        else for (int i = 0; i <= sträcka; i++) {
            string damm = new string('~', Math.Min(i, 8));
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("\r" + new string(' ', Math.Max(0, i - 8)) + damm);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(i % 2 == 0 ? " ε=ε=┌( >_<)┘" : " ε=ε=└( >_<)┐");
            Console.Write("   ");
            Vänta(30);
        }
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine();
    }

    // ------------------------------------------------------------------
    //  7. SEGER – konfetti + blinkande banner
    // ------------------------------------------------------------------
    public static void Seger(Monster fiende) {
        ConsoleColor[] färger = {
            ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Green,
            ConsoleColor.Cyan, ConsoleColor.Magenta, ConsoleColor.Blue
        };
        char[] konfetti = { '*', '+', '.', 'o', '°', '♦' };

        Console.WriteLine();
        for (int rad = 0; rad < 3; rad++) {
            Console.Write("  ");
            for (int i = 0; i < 44; i++) {
                Console.ForegroundColor = färger[slump.Next(färger.Length)];
                Console.Write(slump.Next(3) == 0 ? konfetti[slump.Next(konfetti.Length)] : ' ');
            }
            Console.WriteLine();
            Vänta(80);
        }

        int blinkar = Animera ? 6 : 1;
        for (int i = 0; i < blinkar; i++) {
            Console.ForegroundColor = färger[i % färger.Length];
            Console.Write((Animera ? "\r" : "") + "        * * *   MÖTET ÄR AVSLUTAT!   * * *   ");
            Vänta(130);
        }
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"        {fiende.Namn} loggar ut från Teams för idag.\n");
        Console.ResetColor();
    }

    // ------------------------------------------------------------------
    //  8. LEVEL UP – liten fanfar (valfri, används i player.cs)
    // ------------------------------------------------------------------
    public static void LevelUpFanfar() {
        string[] ram = {
            "   ╔════════════════════════════╗   ",
            "   ║   ▲  LÖNEFÖRHÖJNING!  ▲    ║   ",
            "   ╚════════════════════════════╝   ",
        };
        int antalBlink = Animera ? 3 : 1;
        for (int blink = 0; blink < antalBlink; blink++) {
            Console.ForegroundColor = blink % 2 == 0 ? ConsoleColor.Yellow : ConsoleColor.Green;
            foreach (string rad in ram) Console.WriteLine(rad);
            Vänta(150);
            if (blink < antalBlink - 1) {
                try { Console.SetCursorPosition(0, Console.CursorTop - ram.Length); }
                catch { break; }  // om terminalen inte tillåter det hoppar vi bara över blinket
            }
        }
        Console.ResetColor();
    }

    // ------------------------------------------------------------------
    //  9. KRITISK TRÄFF – blinkande "CRIT!" före skadan
    // ------------------------------------------------------------------
    //  Skicka med ett meddelande, t.ex. "Du critar Robert!"
    public static void Kritisk(string meddelande = "", ConsoleColor färg = ConsoleColor.Yellow) {
        ConsoleColor[] färger = { ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.White, ConsoleColor.Magenta };
        int blinkar = Animera ? 8 : 1;
        for (int i = 0; i < blinkar; i++) {
            Console.ForegroundColor = färger[i % färger.Length];
            Console.Write((Animera ? "\r" : "") + new string(' ', i % 2 == 0 ? 4 : 6) + ">>> KRITISK TRÄFF! x2 <<<   ");
            Vänta(70);
        }
        Console.ResetColor();
        Console.WriteLine();

        if (meddelande != "") {
            SkrivLångsamt("    " + meddelande, färg, 20);
        }
    }

    // ------------------------------------------------------------------
    //  10. POLETT-DROP – snurrande mynt när ett monster tappar poletter
    // ------------------------------------------------------------------
    public static void PolettDrop(Monster fiende, int antal) {
        if (antal <= 0) {
            SkrivLångsamt($"  {fiende.Namn} vänder ut och in på fickorna... inga poletter idag.", ConsoleColor.DarkGray, 15);
            Console.WriteLine();
            return;
        }

        string[] snurr = { "(o)", "(0)", "(|)", "(0)" };   // myntet "snurrar"
        if (Animera) {
            for (int i = 0; i < 12; i++) {
                Console.ForegroundColor = i % 2 == 0 ? ConsoleColor.Yellow : ConsoleColor.DarkYellow;
                string mynt = "";
                for (int m = 0; m < antal; m++) mynt += snurr[(i + m) % snurr.Length] + " ";
                Console.Write("\r    " + mynt + "  ");
                Vänta(70);
            }
            Console.Write("\r");
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        string klirr = "";
        for (int m = 0; m < antal; m++) klirr += "(o) ";
        Console.Write("    " + klirr + " *KLING*");
        Console.ResetColor();
        Console.WriteLine("\n");
    }

    // ------------------------------------------------------------------
    //  11. KAFFEMASKINEN – när du inte har några poletter
    // ------------------------------------------------------------------
    public static void KaffemaskinTom() {
        string[] maskin = {
            "     .---------------.",
            "     |  KAFFE-O-MAT  |",
            "     |  [ SÄTT IN ]  |",
            "     |  [ POLETT! ]  |",
            "     |    _____      |",
            "     |   |     |     |",
            "     '---'-----'-----'",
        };
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        foreach (string rad in maskin) {
            Console.WriteLine(rad);
            Vänta(50);
        }
        Console.ResetColor();
    }

    // ------------------------------------------------------------------
    //  12. ARENAN – intro-banner
    // ------------------------------------------------------------------
    public static void ArenaIntro() {
        string[] banner = {
            "  ╔══════════════════════════════════════════════╗",
            "  ║        *  KVARTALSMÖTET – ARENAN  *          ║",
            "  ║   7 dagar. 7 möten. Ingen väg ut.            ║",
            "  ╚══════════════════════════════════════════════╝",
        };
        Console.WriteLine();
        for (int blink = 0; blink < (Animera ? 3 : 1); blink++) {
            Console.ForegroundColor = blink % 2 == 0 ? ConsoleColor.Red : ConsoleColor.DarkRed;
            foreach (string rad in banner) Console.WriteLine(rad);
            Vänta(200);
            if (Animera && blink < 2) {
                try { Console.SetCursorPosition(0, Console.CursorTop - banner.Length); }
                catch { break; }
            }
        }
        Console.ResetColor();
        Console.WriteLine();
    }

    // ------------------------------------------------------------------
    //  13. ARENAN – ärorik seger med pokal
    // ------------------------------------------------------------------
    public static void ArenaSeger() {
        ConsoleColor[] färger = {
            ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Green,
            ConsoleColor.Cyan, ConsoleColor.Magenta, ConsoleColor.Blue
        };
        string[] pokal = {
            @"          ___________          ",
            @"         '._==_==_=_.'         ",
            @"         .-\:      /-.         ",
            @"        | (|:.     |) |        ",
            @"         '-|:.     |-'         ",
            @"           \::.    /           ",
            @"            '::. .'            ",
            @"              ) (              ",
            @"            _.' '._            ",
            @"           '-------'           ",
        };

        Console.WriteLine();
        for (int rad = 0; rad < 4; rad++) {
            Console.Write("  ");
            for (int i = 0; i < 44; i++) {
                Console.ForegroundColor = färger[slump.Next(färger.Length)];
                Console.Write(slump.Next(3) == 0 ? "*+.o"[slump.Next(4)] : ' ');
            }
            Console.WriteLine();
            Vänta(80);
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        foreach (string rad in pokal) {
            Console.WriteLine("      " + rad);
            Vänta(60);
        }
        Console.ResetColor();
        Console.WriteLine();

        int blinkar = Animera ? 8 : 1;
        for (int i = 0; i < blinkar; i++) {
            Console.ForegroundColor = färger[i % färger.Length];
            Console.Write((Animera ? "\r" : "") + "     *** ÄRORIK SEGER! DU ÖVERLEVDE KVARTALSMÖTET! ***   ");
            Vänta(150);
        }
        Console.ResetColor();
        Console.WriteLine("\n");
    }

    // ------------------------------------------------------------------
    //  14. VAPENDROP – ett vapen faller till golvet
    // ------------------------------------------------------------------
    public static void VapenDrop(Monster fiende, Weapon vapen) {
        string[] fall = { "      |", "      v", "   ~~*~~" };
        Console.ForegroundColor = vapen.Färg;
        foreach (string rad in fall) {
            Console.WriteLine(rad);
            Vänta(90);
        }
        Console.ResetColor();

        Console.Write($"  *KLONK* {fiende.Namn} tappade ");
        Console.ForegroundColor = vapen.Färg;
        Console.Write($"{vapen.Namn} (+{vapen.AttackBonus} attack)");
        Console.ResetColor();
        Console.WriteLine("!");
    }
}