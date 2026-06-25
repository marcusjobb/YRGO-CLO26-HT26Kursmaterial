using System;

namespace SpaceExploration
{
    class Program
    {
        static void Main(string[] args)
        {
            // Rymdforskarens namn (text)
            // Vi lagrar namn i en variabel så vi kan återanvända det genom programmet
            // Detta följer DRY-principen: Don't Repeat Yourself - om vi behöver ändra namnet senare, gör vi det på ett ställe
            string explorerName = "Commander Zara Nova";

            // En Lumens energinivå (heltal)  
            // Vi behöver en "konstant" som representerar baslinjen för jämförelser
            // I riktiga program kommer sådana värden ofta från konfigurationsfiler eller databaser
            int lumenEnergy = 25;

            // Zaras energinivå (tre gånger så hög som en Lumen)
            // Istället för att hardkoda 75, beräknar vi från lumenEnergy
            // Detta gör koden flexibel - om lumenEnergy ändras, uppdateras zaraEnergy automatiskt
            int zaraEnergy = lumenEnergy * 3;

            Console.WriteLine("Upptäcktslogg startar. Jag är " + explorerName + ".");
            Console.WriteLine(explorerName + " har " + zaraEnergy + " energienheter tillgängliga!");

            // Antal Lumens som upptäckts
            // Vi behöver hålla reda på tillståndet (state) genom programmet
            // Denna variabel kommer att uppdateras senare - det kallas för mutable state
            int discoveredLumens = 12;

            Console.WriteLine("Sensorer visar " + discoveredLumens + " Lumens i närområdet");

            // Kommunikationsförsök - energikostnad per försök  
            // Vi använder double för precision - energi kan vara 3.5, inte bara heltal
            // Detta representerar "cost per unit" - ett vanligt mönster i programmering
            double commCostPerAttempt = 3.5;

            // Antal kommunikationsförsök
            // Att separera "antal" från "kostnad per" gör koden modulär och testbar
            // Nu kan vi enkelt ändra antalet försök utan att påverka kostnaden per försök
            int commAttempts = 4;

            // Total energikostnad för kommunikation
            // Computed values: vi beräknar från andra variabler istället för att gissa
            // Detta eliminerar mänskliga räknefel och gör koden självdokumenterande
            double totalCommCost = commCostPerAttempt * commAttempts;

            Console.WriteLine("Kommunikationsförsök påbörjas...");
            Console.WriteLine("Energikostnad för " + commAttempts + " försök: " + totalCommCost + " enheter");

            // Uppdatera Zaras energinivå efter kommunikationsförsöken
            // Resource management: vi spårar konsumtion av begränsade resurser
            // I verkliga system: batteriladdning, minnesutrymme, nätverksbandbredd, API-calls, etc.
            double zaraRemainingEnergy = zaraEnergy - totalCommCost;

            Console.WriteLine(explorerName + " har " + zaraRemainingEnergy + " energienheter kvar");

            // Antal vänliga Lumens som svarade
            // Business logic: detta representerar resultatet av en operation
            // I riktiga appar: antal framgångsrika transaktioner, lyckade login-försök, godkända ansökningar
            int friendlyLumens = 7;

            Console.WriteLine("Genombrott! " + friendlyLumens + " Lumens svarade vänligt!");
            Console.WriteLine(explorerName + " har etablerat första kontakten med en främmande civilisation!");

            // Andra dagens aktiviteter
            double scanCost = 2.5;
            int numberOfScans = 6;
            double dailyScanCost = scanCost * numberOfScans;
            zaraRemainingEnergy = zaraRemainingEnergy - dailyScanCost;

            Console.WriteLine("Dag 2: " + numberOfScans + " energiskanningar genomförda");
            Console.WriteLine("Energikostnad: " + dailyScanCost + " enheter");
            Console.WriteLine(explorerName + " har " + zaraRemainingEnergy + " energienheter kvar");

            // Tredje dagen - provtagning
            int sampleCost = 12;
            zaraRemainingEnergy -= sampleCost; // Kort version av samma sak

            Console.WriteLine("Dag 3: Provtagning av Lumen-habitat genomförd");
            Console.WriteLine("Energikostnad: " + sampleCost + " enheter");
            Console.WriteLine(explorerName + " har " + zaraRemainingEnergy + " energienheter kvar");

            // Fjärde dagen - ytterligare kommunikation
            totalCommCost += commCostPerAttempt * 2; // Två extra försök
            zaraRemainingEnergy -= commCostPerAttempt * 2;

            Console.WriteLine("Dag 4: Fördjupad kommunikation med Lumens");
            Console.WriteLine("Total kommunikationskostnad: " + totalCommCost + " enheter");
            Console.WriteLine(explorerName + " har " + zaraRemainingEnergy + " energienheter kvar");

            // Femte dagen - uppdragssumma
            discoveredLumens += friendlyLumens - 2; // Vissa Lumens var blyga och gömde sig
            
            Console.WriteLine("Uppdrag slutfört!");
            Console.WriteLine("Totalt antal Lumens dokumenterade: " + discoveredLumens);
            Console.WriteLine(explorerName + " återvänder till rymdskeppet med historiska upptäckter!");
        }
    }
}