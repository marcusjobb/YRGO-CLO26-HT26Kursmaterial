using System.Data.SqlTypes;
//------------------------ Info -----------------------------------

/*
 
 Ai har skapat alla prompter åt oss med temat "office"
 
 */

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

//---------------------------------- Prolog ----------------------------------

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("==========================================================================");
Console.WriteLine("                    * PROLOG: CORPORATE SURVIVAL *                        ");
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
    Console.WriteLine($"Välkommen till kontoret {spelare.Namn}! Din första dag har börjat.");  
    Console.WriteLine("--------------------------------------------------");

//---------------------------------- Kod ----------------------------------

    while (alive) {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n--- DAG {spelare.DagarÖverlevda} ---");
        Console.ResetColor();
        Console.WriteLine($"Status: {spelare.Hp}/{spelare.MaxHp} Energi | Work XP: {spelare.Xp} / {spelare.Tröskel} | Total XP: {spelare.TotalXp}");
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine("Vad vill du göra nu?");
        Console.WriteLine("1. Gå ut i korridoren och ta ett möte (Slåss) (Kostar 1 dag)");
        Console.WriteLine("2. Gå till fikarummet och ta en kaffepaus (Återställ HP) (Kostar 1 dag)");
        Console.WriteLine();
        Console.Write("Ditt val: ");
        
        string val = Console.ReadLine();

        //---------------------------------- Val 1 - Strid ----------------------------------

        if (val == "1") {
            spelare.DagarÖverlevda++;
            
            List<Monster> tillgängligaMonster = new List<Monster>
            {
                new Mellanchef(),
                new Flatearther(),
                new Karen()
            };
            
            int slumpFiende = rnd.Next(tillgängligaMonster.Count);
            Monster fiende = tillgängligaMonster[slumpFiende];
            

            //---------------------------------- Val 1 - Strid ----------------------------------
            Console.WriteLine();
            fiende.SkrivUtFiendeNamn("En "," Dyker upp!"); //metod som anropar monstret kolla monster.cs
            Console.WriteLine();
            
            while (alive) {
                
                Console.WriteLine("Vad vill du göra?");
                Console.WriteLine("1. Låtsas få en akut Windows Update och stirra tomt i väggen (Försvar)");
                Console.WriteLine("2. Kasta kollegan under bussen i teams (Anfall)");
                Console.WriteLine("3. Alt + F4 och skyll på att fiberkabeln grävdes av (Fly) (kostar dig hela arbetsdagen)");
                Console.Write("Ditt val: ");
                string stridsVal = Console.ReadLine();
                Console.WriteLine();
                
                if (stridsVal == "1") {
                    
                    int försvar = fiende.Attack / 2;
                    spelare.TakeDamage(försvar);
                    fiende.SkrivUtFiendeNamn("Du håller upp ditt plastade HR-reglemente som en sköld mot ", $" och tar halverad {försvar} skada!\n");
                    Console.WriteLine($"{spelare.Namn} Energi: {spelare.Hp}/{spelare.MaxHp}");
                }

                else if (stridsVal == "2") {

                    fiende.SkrivUtFiendeNamn("Du gör en aggressiv presentation i PowerPoint med skuggade stapeldiagram mot ", $" och gör {spelare.Attack} skada!");
                    bool monsterDog = fiende.TakeDamage(spelare.Attack);
                    
                    if (monsterDog) {

                        spelare.GainXp(fiende.XpBelöning);
                        break;
                    }
                    
                    fiende.Attackerar(spelare);
                    Console.WriteLine($"{spelare.Namn} Energi: {spelare.Hp}/{spelare.MaxHp}");
                    Console.WriteLine($"{fiende.Namn} Energi: {fiende.Hp}/{fiende.MaxHp}");
                    Console.WriteLine();
                }
                
                else if (stridsVal == "3") {
                    
                    int spring = rnd.Next(1, fiende.Attack + 1);
                    spelare.TakeDamage(spring);
                    
                    Console.ForegroundColor = ConsoleColor.Red;  
                    Console.WriteLine("[RÖT LARM]: Du krossar glasrutan och drar i brandlarmet!");  
                    Console.ResetColor();  
                    Console.WriteLine("Tjuten ekar i korridoren och hela kontoret drabbas av panik.");  
                    fiende.SkrivUtFiendeNamn("I det kaotiska utrymningen blir du trampad på av ", $" och tar {spring} skada!");  
                    Console.WriteLine("Du lyckas smita ut ur byggnaden och flyr tillbaka till tryggheten.\n");  
                    Console.WriteLine($"{spelare.Namn} Energi: {spelare.Hp}/{spelare.MaxHp}");
                }
                
                else {
                    Console.WriteLine("Ogiltigt val, försök igen.");
                    Console.WriteLine();
                }

                if (spelare.Hp <= 0) {
                    VisaGameOver(spelare); //spelar game over credits
                    alive = false;  // du dör
                    break;
                }
                
                if (stridsVal == "3") { // om vi överlever med fly metoden så breakar vi loopen
                    break;              // vi checkar också om vi dör. tidigare commits så hoppade den över hp check.
                }
            }
        }
        
        //---------------------------------- Val 2 - Heal ----------------------------------

        else if (val == "2") {
            GåTillFikarummet(spelare);
        }

        //---------------------------------- Ogiltligt kommando ----------------------------------

        else {
            Console.WriteLine();
            Console.WriteLine("Ogiltigt val, försök igen.");
        }
    }

//---------------------------------- Metoder ----------------------------------
        void GåTillFikarummet(Player spelare) {
            
            Console.WriteLine("\nDu smyger iväg till fikarummet...");
            
            if (spelare.Hp == spelare.MaxHp) {
                Console.WriteLine("Du har redan full energi! Kaffet gör dig bara darrig.\n");
                return;
            }

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