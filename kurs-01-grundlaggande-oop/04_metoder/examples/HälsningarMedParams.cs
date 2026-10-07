// Vad gör det här programmet:
//   Demonstrerar DRY-principen med metoder, och visar hur `params`
//   låter en metod ta emot valfritt antal argument.
//
// Koncept som visas:
//   - void-metod med parameter
//   - DRY (Don't Repeat Yourself)
//   - params-nyckelordet
//   - foreach-loop

// --- Utan metoder ser det ut så här ---
// Console.WriteLine("Hej, Marcus!");
// Console.WriteLine("--------------------------------");
// Console.WriteLine("Hej, Kalle!");
// Console.WriteLine("--------------------------------");
// Console.WriteLine("Hej, Pelle!");
// Console.WriteLine("--------------------------------");
// Tre upprepningar — och vi ändrar ändå på flera ställen om något ska justeras.

// --- Med metoder och params ---
PrintGreetings("Johan", "David", "Pet", "Marcus", "Kalle", "Pelle");


// Tar emot ett enda namn och skriver ut en hälsning.
static void PrintGreeting(string name)
{
    Console.WriteLine($"Hej, {name}!");
    Console.WriteLine("--------------------------------");
}

// params string[] names — tar emot valfritt antal strängar som ett array.
// Du kan anropa den med 1, 3, 10 namn — utan att ändra metoden.
//
// Hur params fungerar:
//   PrintGreetings("A", "B", "C")   → args = ["A", "B", "C"]
//   PrintGreetings("Anna")           → args = ["Anna"]
//   string[] lista = { "X", "Y" };
//   PrintGreetings(lista)            → också okej, array funkar direkt
//
// params-parametern måste alltid vara sist i parameterlistan.
static void PrintGreetings(params string[] names)
{
    foreach (string name in names)
        PrintGreeting(name);
}
