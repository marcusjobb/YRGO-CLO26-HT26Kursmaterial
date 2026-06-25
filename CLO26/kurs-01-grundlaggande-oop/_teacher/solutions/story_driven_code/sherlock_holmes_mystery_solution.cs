using System;

namespace HolmesMystery
{
    class Program
    {
        static void Main(string[] args)
        {
            // Mästardetektivens namn (text)
            // Vi lagrar namn för konsekvent rapportering genom hela undersökningen
            // Detta följer DRY-principen och gör koden maintainerbar
            string detectiveName = "Sherlock Holmes";

            // Totalt antal ledtrådar att undersöka
            // Loop boundaries: vi definierar hur många iterationer vi behöver
            // I verkliga system: antal poster i databas, filer att processa, API calls att göra
            int totalClues = 7;

            Console.WriteLine("🔍 " + detectiveName + " påbörjar sin undersökning...");
            Console.WriteLine("📋 Antal ledtrådar att granska: " + totalClues);
            Console.WriteLine("════════════════════════════════════════");
            Console.WriteLine();

            // Räknare för viktiga bevis
            // Accumulator pattern: vi samlar data genom iterationerna
            // Detta är fundamental för statistik, summering, filtrering
            int importantEvidence = 0;

            // Misstänksamhetspoäng (ackumuleras för varje ledtråd)
            // Running totals: vanligt mönster för scorekeeping, budget tracking, metrics
            int suspicionScore = 0;

            // HOLMES' SYSTEMATISKA GRANSKNING
            // For-loop: repetitiv process för att undersöka varje ledtråd
            // Loop structure: initialization; condition; increment
            for (int clueNumber = 1; clueNumber <= totalClues; clueNumber++)
            {
                Console.WriteLine("🔎 Undersöker ledtråd #" + clueNumber + ":");

                // Varje ledtråd har olika värden beroende på dess nummer
                // Conditional logic within loops: olika behandling baserat på iteration
                // Business rules: ofta varierar processing baserat på input data

                if (clueNumber == 1)
                {
                    Console.WriteLine("   📜 Hotelsebrev med blodfläck - Viktigt!");
                    importantEvidence++;
                    suspicionScore += 15;
                }
                else if (clueNumber == 2)
                {
                    Console.WriteLine("   🚬 Cigarettfimpar - 3 olika märken");
                    suspicionScore += 8;
                }
                else if (clueNumber == 3)
                {
                    Console.WriteLine("   👞 Muddy fotspår storlek 42 - Viktigt!");
                    importantEvidence++;
                    suspicionScore += 12;
                }
                else if (clueNumber == 4)
                {
                    Console.WriteLine("   🕰️  Trasig fickur stannad på 23:47");
                    suspicionScore += 10;
                }
                else if (clueNumber == 5)
                {
                    Console.WriteLine("   💍 Glitterspår från smyckesskrin - Viktigt!");
                    importantEvidence++;
                    suspicionScore += 20;
                }
                else if (clueNumber == 6)
                {
                    Console.WriteLine("   🗝️  Främmande nyckel som inte passar något lås");
                    suspicionScore += 5;
                }
                else if (clueNumber == 7)
                {
                    Console.WriteLine("   📰 Tidningsurklipp om liknande brott - Viktigt!");
                    importantEvidence++;
                    suspicionScore += 18;
                }

                // Visa löpande status för varje ledtråd
                // Progress reporting: håll användaren informerad om framsteg
                Console.WriteLine("   📊 Misstanksamhet så här långt: " + suspicionScore + " poäng");
                Console.WriteLine("   🎯 Viktiga bevis hittade: " + importantEvidence + " st");
                Console.WriteLine();
            }

            // HOLMES' ANALYS OCH SLUTSATS
            Console.WriteLine("════════════════════════════════════════");
            Console.WriteLine("🧠 " + detectiveName + "s analys:");
            Console.WriteLine("📋 Totalt antal ledtrådar granskade: " + totalClues);
            Console.WriteLine("🎯 Viktiga bevis funna: " + importantEvidence + " av " + totalClues);
            Console.WriteLine("📊 Total misstanksamhetspoäng: " + suspicionScore);

            // Beräkna procentandel viktiga bevis
            // Percentage calculations: vanligt för rapporter och analytics
            // Type casting för precision i divisionsberäkningar
            double evidencePercentage = ((double)importantEvidence / totalClues) * 100;
            Console.WriteLine("📈 Andel viktiga bevis: " + evidencePercentage + "%");

            // Holmes' slutsats baserat på ackumulerad data
            // Decision logic: använd samlad data för slutsatser
            if (suspicionScore >= 70 && importantEvidence >= 3)
            {
                Console.WriteLine();
                Console.WriteLine("🎩 " + detectiveName + "s slutsats:");
                Console.WriteLine("✅ Fallet LÖST! Bevisen pekar tydligt på en inombrottstjuv");
                Console.WriteLine("🔒 Rekommendation: Kontakta Scotland Yard omedelbart");
            }
            else if (suspicionScore >= 40)
            {
                Console.WriteLine();
                Console.WriteLine("🎩 " + detectiveName + "s slutsats:");
                Console.WriteLine("🤔 Mer undersökning krävs - ledtrådarna är tvetydiga");
                Console.WriteLine("📝 Rekommendation: Samla ytterligare bevis");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("🎩 " + detectiveName + "s slutsats:");
                Console.WriteLine("❌ Otillräckliga bevis för att lösa fallet");
                Console.WriteLine("🔍 Rekommendation: Ny brottsplatsundersökning behövs");
            }

            Console.WriteLine();
            Console.WriteLine("🎭 \"Elementary, my dear Watson!\" - " + detectiveName);
        }
    }
}
