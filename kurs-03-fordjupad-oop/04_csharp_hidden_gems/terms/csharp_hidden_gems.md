# Programmeringstermer — C# hidden gems

- **Dynamic (dynamic):** En typ som kontrolleras först när programmet körs, inte när det kompileras.
- **Pattern matching:** Att kontrollera form och värde i samma uttryck, med `is` och `switch`.
- **switch-uttryck:** Ett `switch` som returnerar ett värde, med `=>` för varje fall.
- **Tuple:** En grupp värden utan egen klass, till exempel `(decimal sum, decimal vat)`.
- **Deconstruction:** Att plocka isär en tuple eller ett objekt i separata variabler.
- **Indexer (indexerare):** En `this[...]`-property som gör ett objekt användbart med hakparenteser.
- **Null-conditional (`?.`):** Går vidare bara om objektet inte är `null`.
- **Null-coalescing (`??`):** Ger ett reservvärde om värdet är `null`.
- **Null-coalescing assignment (`??=`):** Tilldelar bara om variabeln är `null`.
- **Nullable (`string?`, `int?`):** Ett `?` efter typen visar att värdet får vara `null`.
- **Extension method:** En statisk metod som ser ut som en metod på en befintlig typ, med `this` i första parametern.
- **sealed:** En klass som ingen får ärva från. `string` är `sealed`, så en extension method är sättet att lägga till en metod på den.
- **Extension member:** Från C# 14: en extension kan också vara en egenskap, genom ett `extension`-block.
- **Record:** En klass för data, med värdelikhet och `with` för kopior med ändrade värden.
- **with-uttryck:** Skapar en kopia av en record med ett eller flera värden ändrade.
- **init (init-only):** En property som bara kan sättas när objektet skapas. Efteråt är den låst. Så fungerar värdena i en record.
- **async och await:** Nyckelord för att vänta på en operation utan att blockera programmet.
- **Task:** Ett löfte om ett resultat som kommer senare. Det som `async`-metoder returnerar.
- **Task.WhenAll:** Väntar på flera `Task` samtidigt. Tre väntetider på en sekund vardera tar en sekund totalt, inte tre.
- **Stopwatch:** Mäter hur lång tid något tar, till exempel för att jämföra `await` en i taget mot `Task.WhenAll`.
- **TimeOnly och DateOnly:** Typer för klockslag och datum utan tidszon eller del av dag.
- **Reflection:** Att ett program undersöker typer, properties och värden medan det körs. Långsammare än vanlig kod, och kompilatorn kontrollerar inte namnen.
- **GetType():** Ger typen (ritningen) för ett objekt, till exempel `ada.GetType().Name` ger `Customer`.
- **PropertyInfo:** Beskriver en property. Du får den från `GetProperties()` och använder `Name`, `GetValue` och `SetValue`.
- **Activator.CreateInstance:** Skapar ett objekt av en typ utan att skriva `new`, till exempel `Activator.CreateInstance(typeof(Customer))`.

<!-- nion-copyright -->
<sub>© 2025–2026 Nion Education. Alla rättigheter förbehållna. Fritt att läsa, men får inte användas av andra skolor eller utbildningsanordnare utan skriftligt tillstånd från Nion Education. Delar bygger på källor som anges i respektive dokument.</sub>
