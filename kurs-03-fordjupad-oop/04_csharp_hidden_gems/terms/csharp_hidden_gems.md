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
- **Extension method:** En statisk metod som ser ut som en metod på en befintlig typ, med `this` i första parametern.
- **Extension member:** Från C# 14: en extension kan också vara en egenskap, genom ett `extension`-block.
- **Record:** En klass för data, med värdelikhet och `with` för kopior med ändrade värden.
- **with-uttryck:** Skapar en kopia av en record med ett eller flera värden ändrade.
- **async och await:** Nyckelord för att vänta på en operation utan att blockera programmet.
- **Task:** Ett löfte om ett resultat som kommer senare. Det som `async`-metoder returnerar.
- **TimeOnly och DateOnly:** Typer för klockslag och datum utan tidszon eller del av dag.
