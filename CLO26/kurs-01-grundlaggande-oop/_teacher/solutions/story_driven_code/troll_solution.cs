using System;

namespace TrollensStyrka
{
    class Program
    {
        static void Main(string[] args)
        {
            // Trollets namn (text)
            // Vi lagrar namn i en variabel så vi kan återanvända det genom programmet
            // Detta följer DRY-principen: Don't Repeat Yourself - om vi behöver ändra namnet senare, gör vi det på ett ställe
            string trollName = "Grlub";

            // Människans styrka (heltal)
            // Vi behöver en "konstant" som representerar baslinjen för jämförelser
            // I riktiga program kommer sådana värden ofta från konfigurationsfiler eller databaser
            int humanStrength = 15;

            // Grlubs styrka (dubbelt så stark som fem människor)
            // Istället för att hardkoda 150, beräknar vi från humanStrength
            // Detta gör koden flexibel - om humanStrength ändras, uppdateras grlubStrength automatiskt
            int grlubStrength = humanStrength * 5 * 2;

            Console.WriteLine("Hej! Jag heter " + trollName + ".");
            Console.WriteLine(trollName + " är " + grlubStrength + " stark!");

            // Antal människor
            // Vi behöver hålla reda på tillståndet (state) genom programmet
            // Denna variabel kommer att uppdateras senare - det kallas för mutable state
            int humanCount = 17;

            Console.WriteLine("Människorna anfaller, de är " + humanCount + " stycken");

            // Nu räknar vi slagen
            // 3 människor mot kroppen, 1 mot huvudet
            // Vi skapar två int variabler: "hitsToBody" (3*3) och "hitsToHead" (1*1)
            int hitsToBody = 3 * 3; // 3 människor, 3 slag var
            int hitsToHead = 1 * 1; // 1 människa, 1 slag var

            // Hur många slag totalt per runda?
            // Computed values: vi beräknar från andra variabler istället för att gissa
            // Detta eliminerar mänskliga räknefel och gör koden självdokumenterande
            int totalHits = hitsToBody + hitsToHead;

            // Räkna ut antal besegrade människor
            // Business logic: detta representerar resultatet av en operation
            // I riktiga appar: antal framgångsrika transaktioner, lyckade login-försök, godkända ansökningar
            int defeatedHumans = 4;

            // Minska antal människor med antal besegrade
            // Resource management: vi spårar konsumtion av begränsade resurser
            // I verkliga system: batteriladdning, minnesutrymme, nätverksbandbredd, API-calls, etc.
            humanCount = humanCount - defeatedHumans;

            Console.WriteLine(humanCount + " människor kvar.");
            Console.WriteLine(trollName + " kämpar vidare...");

            // Skriv ut antal slag som använts
            Console.WriteLine(trollName + " har använt " + totalHits + " slag.");

            // Andra runda
            totalHits += hitsToBody + hitsToHead;
            humanCount = humanCount - defeatedHumans;

            Console.WriteLine(humanCount + " människor kvar.");
            Console.WriteLine(trollName + " har använt " + totalHits + " slag.");
            Console.WriteLine(trollName + " kämpar vidare...");

            // Tredje runda
            totalHits += hitsToBody + hitsToHead;
            humanCount -= defeatedHumans; // Kort version av samma sak

            Console.WriteLine(humanCount + " människor kvar.");
            Console.WriteLine(trollName + " har använt " + totalHits + " slag.");
            Console.WriteLine(trollName + " kämpar vidare...");

            // Fjärde runda
            totalHits += hitsToBody + hitsToHead;
            humanCount -= defeatedHumans;

            Console.WriteLine(humanCount + " människor kvar.");
            Console.WriteLine(trollName + " har använt " + totalHits + " slag.");
            Console.WriteLine(trollName + " har besegrat alla människor!");

            // Femte runda
            totalHits += hitsToBody + hitsToHead;
            humanCount -= defeatedHumans;

            Console.WriteLine(humanCount + " människor kvar.");
            Console.WriteLine(trollName + " har använt " + totalHits + " slag.");
            Console.WriteLine(trollName + " har besegrat alla människor!");
        }
    }
}