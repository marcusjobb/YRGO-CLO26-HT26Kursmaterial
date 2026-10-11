using System.Data.SqlTypes;

//--------------------- Initiering av programtillstånd ----------------------
Player spelare = new Player(
    "",
    30,
    30,
    4,
    1,
    1,
    0,
    1
    );

bool alive = true;
Random rnd = new Random();
StridsEffekter.Init(); // NY – gör att specialtecken (█ ░ ═) visas rätt
Shop cafeterian = new Shop("Cafeterian"); // NY – shoppen

//---------------------------------- Prolog ----------------------------------

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("==========================================================================");
Console.WriteLine("                    * PROLOG: CORPORATE SURVIVAL *                        "); // ai skapad
Console.WriteLine("==========================================================================");
Console.ForegroundColor = ConsoleColor.Gray;

Console.WriteLine(@"Måndag morgon. Klockan är 07:58. 

Du står framför skjutdörrarna till 'Synergy Corp HQ'. I handen har du 
ett plastigt passerkort där det står 'Unknown name? - Intern'. Lite udda
men vi kommer dit. Luften luktar billigt automatkaffe och bränd mikro-lunch.

Ditt uppdrag är enkelt: Överlev din första vecka.

Men korridorerna kryllar av faror. En svag doft av parfym och mikromanagement 
skvallrar om att mellancheferna är på krigsstigen. Flatearthers från 
IT-supporten sprider vilda konspirationer vid vattenkylaren. Och någonstans 
i de övre hörnrummen ruvar den ultimata slutbossen: Karen.

Förlorar du all din energi (HP) tvingas du skriva på ett tillsvidarekontrakt 
och bli en av dem.

Tryck på [ENTER] för att stämpla in och starta första arbetsdagen...");

Console.ReadLine();
Console.Clear();

Console.Write("Skriv in ditt namn för att trycka upp namnskylten: ");
spelare.Namn = Console.ReadLine();

//---------------------------------- Intro ----------------------------------

    Console.WriteLine("\n[BEEP] *Klockar in passerkortet*");
    Console.WriteLine($"Välkommen till kontoret {spelare.Namn}! Dag 1 har börjat.");  
    Console.WriteLine("--------------------------------------------------");

//---------------------------------- Kod Meny ----------------------------------

    while (alive) {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n--- DAG {spelare.DagarÖverlevda} ---");
        Console.ResetColor();
        Console.WriteLine($"Status: {spelare.Hp}/{spelare.MaxHp} Energi | Work XP: {spelare.Xp} | Total XP: {spelare.TotalXp} | Kaffepoletter: {spelare.Poletter}");
        Console.WriteLine($"Vapen: {spelare.AktivtVapen?.Namn ?? "bara nävarna"} | Attack: {spelare.TotalAttack}"); // NY
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine("Vad vill du göra nu?");
        Console.WriteLine("1. Gå ut i korridoren och ta ett möte (Slåss) (Kostar 1 dag)");
        Console.WriteLine("2. Gå till fikarummet och ta en kaffepaus (Återställ HP) (Kostar 1 dag + 1 kaffepolett)");
        Console.WriteLine("3. Gå till Cafeterian och köp vapen (Kostar ingen dag)"); // NY
        Console.WriteLine("4. Gå in i ARENAN – kvartalsmötet (7 dagar, kan inte avbrytas)"); // NY
        Console.WriteLine();
        Console.Write("Ditt val: ");

        string? val = Console.ReadLine();

        //---------------------------------- Val 1 - Genererar motståndare ----------------------------------

        if (val == "1") {

            int slumpFiende = rnd.Next(1, 101); //ai sa 1,101 var mer precist en 0,100
            Monster fiende;
            spelare.DagarÖverlevda++;

            switch (slumpFiende) {
                case <= 50: // 0 - 50 att Mellanchef dyker upp
                    fiende = new Mellanchef();
                    break;
                case <= 95: // 51 - 95 att Flatearther dyker upp
                    fiende = new Flatearther();
                    break;
                default: // 96 - 100 att Karen dyker upp
                    fiende = new Karen();
                    break;
            }

            Strid(fiende, kanFly: true); // NY – själva striden ligger nu i en egen metod
        }

        //---------------------------------- Val 2 - Heal ----------------------------------

        else if (val == "2") {
            GåTillFikarummet(spelare);
        }

        //---------------------------------- Val 3 - Shop (NY) ----------------------------------

        else if (val == "3") {
            cafeterian.Besök(spelare); // shoppen går bara att nå härifrån, alltså mellan strider
        }

        //---------------------------------- Val 4 - Arenan (NY) ----------------------------------

        else if (val == "4") {
            Arenan();
        }

        //---------------------------------- Ogiltligt kommando ----------------------------------

        else {
            Console.WriteLine();
            Console.WriteLine("Ogiltigt val, försök igen.");
        }
    }

//---------------------------------- Metoder ----------------------------------

        // NY – striden är flyttad hit så att både vanliga möten och arenan kan använda den.
        // Returnerar "vann", "flydde" eller "dog".
        string Strid(Monster fiende, bool kanFly) {

            StridsEffekter.Möte(fiende);


            while (true) {

                StridsEffekter.Statuspanel(spelare, fiende);
                Console.WriteLine("Vad vill du göra?");
                Console.WriteLine("1. Försvara dig");
                Console.WriteLine("2. Anfall tillbaka");
                if (kanFly) Console.WriteLine("3. Fly");
                Console.Write("Ditt val: ");
                string? stridsVal = Console.ReadLine();
                Console.WriteLine();

                if (stridsVal == "1") {

                    
                    int försvar = fiende.Attack / 2;
                    if (rnd.Next(1, 101) <= 10) { försvar *= 2; StridsEffekter.Kritisk($"{fiende.Namn} critar dig trots ditt försvar!", ConsoleColor.Red); } // fienden kan crita även när du försvarar dig
                    spelare.TakeDamage(försvar);
                    Console.WriteLine($"Du håller upp ditt plastade HR-reglemente som en sköld! Du tar halverad {försvar} skada!\n");

                }

                else if (stridsVal == "2") {

                    bool spelareCrit = rnd.Next(1, 101) <= 10;                               // 10 % chans att crita
                    int skada = spelareCrit ? spelare.TotalAttack * 2 : spelare.TotalAttack; // NY – TotalAttack = attack + vapnets bonus

                    if (spelareCrit) StridsEffekter.Kritisk($"Du critar {fiende.Namn}! Dubbel skada: {skada}!", ConsoleColor.Green);
                    StridsEffekter.Anfall(skada);

                    // NY – vapnet bestämmer hur anfallet beskrivs
                    if (spelare.AktivtVapen != null) {
                        fiende.SkrivUtFiendeNamn($"Du {spelare.AktivtVapen.Anfallsverb} ", $" med din {spelare.AktivtVapen.Namn} och gör {skada} skada!");
                    }
                    else {
                        fiende.SkrivUtFiendeNamn("Du gör en aggressiv presentation i PowerPoint med skuggade stapeldiagram mot ", $" och gör {skada} skada!");
                    }

                    bool monsterDog = fiende.TakeDamage(skada);


                    if (monsterDog) {

                        StridsEffekter.Seger(fiende);
                        int poletter = fiende.SläppPoletter();          // monstret tappar kaffepoletter
                        StridsEffekter.PolettDrop(fiende, poletter);    // snurrande mynt
                        spelare.GainXp(fiende.XpBelöning, poletter);    // visar XP och sedan poletter

                        // NY – chans att monstret tappar sitt vapen
                        Weapon? drop = fiende.SläppVapen();
                        if (drop != null) {
                            StridsEffekter.VapenDrop(fiende, drop);
                            if (spelare.TaUppVapen(drop)) {
                                Console.WriteLine($"Du tar upp {drop.Namn}! Din attack är nu {spelare.TotalAttack}.\n");
                            }
                            else {
                                Console.WriteLine($"Du låter {drop.Namn} ligga kvar – din {spelare.AktivtVapen?.Namn} är minst lika bra.\n");
                            }
                        }

                        return "vann";
                    }

                    fiende.Attackerar(spelare);

                }

                else if (stridsVal == "3" && kanFly) {

                    int spring = rnd.Next(1, fiende.Attack);
                    spelare.TakeDamage(spring); // flykten kostar energi
                    StridsEffekter.Flykt();
                    Console.ForegroundColor = ConsoleColor.Red;  
                    Console.WriteLine("[RÖT LARM]: Du krossar glasrutan och drar i brandlarmet!");  
                    Console.ResetColor();  
                    Console.WriteLine("Tjuten ekar i korridoren och hela kontoret drabbas av panik.");  
                    fiende.SkrivUtFiendeNamn("I det kaotiska utrymningen blir du trampad på av ", $" och tar {spring} skada!\n");  
                    Console.WriteLine("Du lyckas smita ut ur byggnaden och flyr tillbaka till tryggheten.");  
                    if (spelare.Hp > 0) return "flydde";
                }

                else {
                    Console.WriteLine(kanFly ? "Ogiltigt val, försök igen." : "Ogiltigt val. I arenan finns ingen flyktväg!");
                    Console.WriteLine();
                }

                if (spelare.Hp <= 0) {
                    VisaGameOver(spelare); //spelar game over credits
                    alive = false;
                    return "dog";
                }
            }
        }

        // NY – Arenan: 7 dagar, 7 monster från svagast till starkast, går inte att avbryta
        void Arenan() {
            StridsEffekter.ArenaIntro();
            Console.WriteLine("Kvartalsmötet pågår i 7 dagar. Du kan inte fly, och du kan inte gå till fikarummet.");
            Console.WriteLine("Dör du här är det GAME OVER direkt. Överlever du väntar ära och en stor XP-bonus.");
            Console.Write("Är du säker på att du vill gå in? (j/n): ");
            string? svar = Console.ReadLine();
            Console.WriteLine();

            if (svar?.ToLower() != "j") {
                Console.WriteLine("Du backar försiktigt ut ur konferensrummet. Ingen såg dig. Tror du.\n");
                return;
            }

            // Skapa 7 motståndare: 6 slumpade + Karen som slutboss
            List<Monster> motståndare = new List<Monster>();
            for (int i = 0; i < 6; i++) {
                if (rnd.Next(2) == 0) motståndare.Add(new Mellanchef());
                else motståndare.Add(new Flatearther());
            }
            motståndare.Add(new Karen());

            // Sortera från svagast till starkast
            motståndare = motståndare.OrderBy(m => m.Styrka).ToList();

            Console.WriteLine("Dagordning för kvartalsmötet:");
            for (int i = 0; i < motståndare.Count; i++) {
                motståndare[i].SkrivUtFiendeNamn($"  Dag {i + 1}: ", "");
            }
            Console.WriteLine();

            for (int i = 0; i < motståndare.Count; i++) {
                spelare.DagarÖverlevda++;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n=========== ARENAN – DAG {i + 1} AV {motståndare.Count} ===========");
                Console.ResetColor();

                string resultat = Strid(motståndare[i], kanFly: false);

                if (resultat == "dog") {
                    return; // game over har redan visats i Strid()
                }
            }

            // Överlevde alla 7!
            StridsEffekter.ArenaSeger();
            Console.WriteLine("Hela kontoret står upp och applåderar. Till och med Karen nickar motvilligt.\n");
            spelare.GainXp(150, 10); // stor XP-bonus + 10 kaffepoletter
        }

        void GåTillFikarummet(Player spelare) {
            Console.WriteLine("\nDu smyger iväg till fikarummet...");

            // Full energi? Då behövs inget kaffe (och ingen polett används)
            if (spelare.Hp == spelare.MaxHp) {
                Console.WriteLine("Du har redan full energi! Kaffet gör dig bara darrig.\n");
                return;
            }

            // NY – kaffemaskinen kräver en polett
            if (!spelare.AnvändPolett()) {  
                StridsEffekter.KaffemaskinTom();  
                Console.ForegroundColor = ConsoleColor.Red;  
                Console.WriteLine("Displayen blinkar rött: [INSERT TOKEN]. Du har inga kaffepoletter kvar! Gå på möten (slåss) för att få fler.");  
                Console.WriteLine("Du tvingas dricka ljummet kranvatten ur en pappersmugg... Ingen energi återställs.\n");  
                Console.ResetColor();  
                return;  
            }  

            Console.ForegroundColor = ConsoleColor.Yellow;  
            Console.WriteLine($"*KLINK* Poletten trillar ner i maskinen. (Poletter kvar: {spelare.Poletter})");  
            Console.ResetColor();  
            Console.WriteLine("Maskinen brummar högljutt, hostar till och pressar ut en tjock, mörkrostat automat-espresso.");  
            Console.WriteLine("Du trycker i dig en torr, dammig mazarin från städerskans hemliga förråd.");

            spelare.Heal();
            Console.ForegroundColor = ConsoleColor.Green;  
            Console.WriteLine($"\n[KAFFECHOCK!] Nikotin, socker och koffein rusar genom blodet. HP återställt till max ({spelare.Hp}/{spelare.MaxHp})!");  
            Console.ResetColor();  
            Console.WriteLine("Du torkar mazarinsmulorna från tangentbordet och är redo för nästa möte.");  
  
            spelare.DagarÖverlevda++;  
        }

//---------------------------------- End credits ----------------------------------

void VisaGameOver(Player spelare) { 
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("==========================================================================");
    Console.WriteLine("                           * GAME OVER *                                  ");
    Console.WriteLine("==========================================================================");
    Console.ResetColor();
    Console.WriteLine($@"
Du faller ihop på det heltäckande mattgolvet. Energidrycken spills ut över 
tangentbordet och ögonlocken känns tunga som bly. 

Du orkade inte hela vägen. Corporate-maskineriet bröt ner dig...

Din sista tanke innan allt blir svart:
'Jag skulle bara ha tagit den där vabb-dagen...'

--------------------------------------------------------------------------
SLUTRESULTAT:
- Dagar överlevda: {spelare.DagarÖverlevda}
- Slutgiltig Level: {spelare.Level}
- Total Work XP: {spelare.TotalXp}
--------------------------------------------------------------------------
"); 
    Console.ForegroundColor = ConsoleColor.DarkYellow; 
    Console.WriteLine("PROMOTION NOTICE:");
    Console.WriteLine("You have been promoted to middle management.");
    Console.WriteLine("Du har nu ett eget glasskåp, men din själ är förlorad för alltid."); 
    Console.ResetColor();
}