// KaraktärsSkapare.cs — switch-satsen och scope
//
// Livekod som visar:
//   • switch med case, break och default
//   • varför variabler måste deklareras UTANFÖR switch för att leva vidare
//   • Environment.Exit() för att tvångsstänga programmet

Console.WriteLine("+----------------------------+");
Console.WriteLine("|    Hello, World of magic!  |");
Console.WriteLine("+----------------------------+");
Console.WriteLine();
Console.WriteLine("Välj din karaktärsras:");
Console.WriteLine("1 - Krigare");
Console.WriteLine("2 - Trollkarl");
Console.WriteLine("3 - Lömsk");       // Typ Han Solo? Scoundrel?
Console.WriteLine("4 - Dvärg");
Console.WriteLine("5 - Hobbit");
Console.WriteLine("0 - Avsluta");

int raceChoice = int.Parse(Console.ReadLine()); // farligt men coolt just nu

// ─────────────────────────────────────────────────────────────
// Scope-regeln:
//   Allt som deklareras INNE I en scope (innanför { }) lever
//   bara så länge som den scopen är aktiv.
//   Scopen dör vid stängande måsvinge } — och variabeln med den.
//
// Därför deklarerar vi race, strength m.fl. UTANFÖR switch.
// Annars vore de osynliga efter att switch är klar.
// ─────────────────────────────────────────────────────────────
string race = "";
int strength = 0;
int intelligence = 0;
int agility = 0;

switch (raceChoice)
{
    case 1:
        race = "Krigare";
        strength = 9;
        intelligence = 3;
        agility = 5;
        break; // ← obligatoriskt — utan break faller koden vidare till nästa case
    case 2:
        race = "Trollkarl";
        strength = 2;
        intelligence = 10;
        agility = 4;
        break;
    case 3:
        race = "Lömsk";
        strength = 5;
        intelligence = 6;
        agility = 9;
        break;
    case 4:
        race = "Dvärg";
        strength = 8;
        intelligence = 5;
        agility = 3;
        break;
    case 5:
        race = "Hobbit";
        strength = 3;
        intelligence = 7;
        agility = 8;
        break;
    case 6: // Human — inte i menyn, men handlar om att visa att case-nummer inte behöver följa ordning
        race = "Human";
        strength = 4;
        intelligence = 3; // ← Politiker
        agility = 9;      // ← Idrottsnörd
        break;
    default:
        // default körs om inget case matchade — alltid sist, alltid en
        Console.WriteLine("Bye Bye!");
        Environment.Exit(0); // ← tvingar programmet att stänga direkt, oavsett vad som händer
        break;
} // ← scopen för switch stängs här

// race, strength, intelligence, agility lever fortfarande — de skapades utanför switch
Console.WriteLine();
Console.WriteLine("Du valde:");
Console.WriteLine($"Rasen       : {race}");
Console.WriteLine($"Styrka      : {strength}");
Console.WriteLine($"Intelligens : {intelligence}");
Console.WriteLine($"Smidighet   : {agility}");
