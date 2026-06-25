using System;

namespace IlumCrystalCave
{
    class Program
    {
        static void Main(string[] args)
        {
            // Anakins namn som ung padawan
            // Vi lagrar namnet för att använda genom hela programmet
            // Detta följer principen att definiera viktiga data en gång
            string padawanName = "Anakin Skywalker";

            Console.WriteLine("🌌 Välkommen till kristallgrottan på Ilum, " + padawanName + "!");
            Console.WriteLine("⚡ Tid att välja din första ljussabelkristall...");
            Console.WriteLine();

            // Den första kristallens färg
            // Varje kristall har sin unika färg som avspeglar användarens anslutning till Force
            // Lagra kristallens primära färg som text
            string crystalColor = "Blå";

            // Kristallens grundstyrka
            // Force-kristaller har olika naturlig styrka från 1-100
            // Denna kraft förstärks av Jedins förmåga att kanalisera Force
            int basePower = 75;

            Console.WriteLine("💎 Du har funnit en " + crystalColor.ToLower() + " kristall!");
            Console.WriteLine("⚡ Kristallens grundstyrka: " + basePower + " Force-enheter");
            Console.WriteLine();

            // Anakins Force-potential som ung padawan
            // Hans midichlorian-antal ger honom exceptionell Force-känslighet
            // Detta värde multipliceras med kristallens grundstyrka
            double anakinForcePotential = 2.3;

            // Beräkna den totala ljussabelstyrkan
            // När kristall kombineras med Jedins Force-potential skapas verklig makt
            // Denna beräkning avgör ljussabelns slutliga kraft
            double totalSaberPower = basePower * anakinForcePotential;

            Console.WriteLine("🌟 " + padawanName + "s Force-potential: x" + anakinForcePotential);
            Console.WriteLine("⚔️  Total ljussabelstyrka: " + totalSaberPower + " enheter!");
            Console.WriteLine();

            // Kristallens speciella egenskaper
            // Olika kristaller ger olika förmågor och känslor
            // Denna information hjälper Jedi att förstå sin kristalls natur
            string specialAbility = "Förhöjd koncentration";

            // Kristallens sällsynthetsvärde (1-10)
            // Sällsynta kristaller är kraftfullare men svårare att behärska
            // Detta påverkar ljussabelns stabilitet och effektivitet
            int rarity = 7;

            Console.WriteLine("✨ Speciell förmåga: " + specialAbility);
            Console.WriteLine("💍 Sällsynthetsnivå: " + rarity + "/10");
            Console.WriteLine();

            // Beräkna kristallens totala värde
            // Kombinerar styrka och sällsynthet för att ge ett helhetsvärde
            // Detta hjälper Jedi att förstå kristallens fulla potential
            double crystalValue = totalSaberPower * rarity;

            Console.WriteLine("💰 Kristallens totala värde: " + crystalValue + " galaktiska krediter");
            Console.WriteLine();
            Console.WriteLine("🎯 " + padawanName + ", kristallen har valt dig!");
            Console.WriteLine("⚡ Din " + crystalColor.ToLower() + " ljussabel kommer att lysa starkt i galaxens mörka tider.");
            Console.WriteLine("🌟 Må Force vara med dig, unge Skywalker!");
        }
    }
}