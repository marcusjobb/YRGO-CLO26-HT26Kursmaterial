//--------------------------------------- Shop: Skolcafeterian -----------------------------------
// Här köper man vapen för kaffepoletter. Shoppen nås bara från huvudmenyn,
// alltså mellan strider – aldrig mitt i en strid.

class Shop {
    public string Namn { get; private set; }

    // Sortimentet är en List<Weapon>:
    // - ordningen bestämmer vi själva (billigast först) och den visas som en numrerad meny
    // - spelaren väljer med en siffra, som vi gör om till ett index i listan
    private List<Weapon> sortiment;

    public Shop(string namn) {
        Namn = namn;
        sortiment = new List<Weapon> {
            new Blyertspenna(),
            new Laptop(),
            new HdmiKabel(),
        };
    }

    public void Besök(Player spelare) {
        VisaSkylt();
        Console.WriteLine("Cafeteriadamen Gunilla tittar upp från kassan:");
        Console.WriteLine("\"Mazarinerna är slut, men jag har lite... annat. Under disken.\"\n");

        while (true) {
            VisaSortiment(spelare);

            Console.Write("Vad vill du köpa? (0 = gå tillbaka): ");
            string? val = Console.ReadLine();
            Console.WriteLine();

            if (val == "0") {
                Console.WriteLine("Gunilla nickar. \"Kom tillbaka när du har fler poletter.\"\n");
                return;
            }

            // Gör om valet till ett index i listan (1 → 0, 2 → 1 osv.)
            if (!int.TryParse(val, out int nummer) || nummer < 1 || nummer > sortiment.Count) {
                Console.WriteLine("Gunilla höjer ett ögonbryn. \"Det där har vi inte.\"\n");
                continue;
            }

            Weapon valt = sortiment[nummer - 1];

            if (spelare.AktivtVapen != null && spelare.AktivtVapen.Namn == valt.Namn) {
                Console.WriteLine($"Du har redan en {valt.Namn}!\n");
                continue;
            }

            if (spelare.Poletter < valt.Pris) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Du har bara {spelare.Poletter} kaffepoletter. {valt.Namn} kostar {valt.Pris}.\n");
                Console.ResetColor();
                continue;
            }

            spelare.KöpVapen(valt);

            Console.Write("*KA-CHING* Du köper ");
            Console.ForegroundColor = valt.Färg;
            Console.Write(valt.Namn);
            Console.ResetColor();
            Console.WriteLine($"! Din attack är nu {spelare.TotalAttack}. ({spelare.Poletter} kaffepoletter kvar)\n");
        }
    }

    private void VisaSortiment(Player spelare) {
        Console.WriteLine($"Du har {spelare.Poletter} kaffepoletter. Ditt vapen: {spelare.AktivtVapen?.Namn ?? "bara nävarna"}");
        Console.WriteLine("---------------------------------------------------------------");

        for (int i = 0; i < sortiment.Count; i++) {
            Weapon v = sortiment[i];
            bool harRåd = spelare.Poletter >= v.Pris;

            Console.Write($"{i + 1}. ");
            Console.ForegroundColor = v.Färg;
            Console.Write($"{v.Namn,-16}");
            Console.ResetColor();
            Console.Write($" +{v.AttackBonus} attack   ");
            Console.ForegroundColor = harRåd ? ConsoleColor.Yellow : ConsoleColor.DarkGray;
            Console.WriteLine($"{v.Pris,2} poletter");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"   {v.Beskrivning}");
            Console.ResetColor();
        }
        Console.WriteLine("---------------------------------------------------------------");
    }

    private void VisaSkylt() {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine();
        Console.WriteLine("   .--------------------------------------.");
        Console.WriteLine($"   |  ~~  {Namn.ToUpper(),-27}  ~~ |");
        Console.WriteLine("   |   Dagens: Fiskpinnar & vapenhandel   |");
        Console.WriteLine("   '--------------------------------------'");
        Console.ResetColor();
        Console.WriteLine();
    }
}