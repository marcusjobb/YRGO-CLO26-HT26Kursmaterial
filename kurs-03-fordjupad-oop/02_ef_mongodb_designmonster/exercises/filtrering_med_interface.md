| Mer om interfaces |
| --- |


# Beskrivning:

🟡


I den här övningen ska vi skapa ett projekt med interfaces för att hitta gemensamma nämnare mellan klasserna.

# Kursplanstermer som berörs av uppgiften:

| Mål | Vad du ska lära dig |
| --- | --- |
| Kunskap om innehållet i .NET-biblioteket | Innehållet i .NET-biblioteket. |
| Kunskaper kring typer, variabler, operationer, uttryck, villkorssatser och loopar inom programmering. | Typer, variabler, uttryck, villkorssatser och loopar används inom programmering. |
| Kunskap kring namngivning och kodstruktur av klasser, metoder och variabler i objektorienterade program. | Namngivning och kodstruktur av klasser, metoder och variabler i objektorienterade program används. |
| Utveckla program med en tydligt objektorienterad struktur. | Utveckla program med en tydligt objektorienterad struktur. |
| Förstå och använda sig av datastrukturer inom programmering. | Att använda datastrukturer i sin mjukvaruutveckling. |


# Termer för övningen:

- Interface – Mall på vad en klass ska innehålla.

- Instansiering genom interface – Instansiering av ett objekt som direkt Castas till att bli av typen som interfacet antyder. All data i objektet finns kvar, men bara de gemensamma delarna med interfacet är synliga.


# Pseudokod:

- Skapa en klass med personer

- Skapa en klass med bankkonton

- Skriv ut namnet på personen och kontot med hjälp av två metoder (en för var typ)

- Skapa ett interface för egenskapen Namn

- "Tagga" dina klasser med interfacet, för att visa att de implementerar interfacet

- Hämna namn från klassen genom en metod som tar emot objekt som impementerar interfacet.


# Projektinstruktioner:

Detta projekt går utmärkt att göra tillsammans med andra, antingen i basgruppen eller i par. Välj en klasskamrat du vill arbeta med och använd LiveShare eller Discordens Screen Share för att samarbeta.

# Kodning:

Vi börjar med att skapa ett c#, .net konsolprojekt.

Skapa nu ett Github repo för ditt projekt (om du inte sparar i ett redan existerande repo)

Vi börjar med att skapa två classer

| C# |
| --- |
| internal class Person {     public string Name { get; set; }     public int Age { get; set; } }  internal class BankAccount {     public string Name { get; set; }     public string AccountNumber { get; set; } } |


Vi vill nu kunna söka i alla bankkonton och alla personer som matchar ett specifikt namn, alltså gör vi en metod som söker efter namn på personer och en metod som söker på kontonamn.

| C# |
| --- |
| private static void FindStuff(string name, List<Person> lst) {     foreach (var item in lst)         if (item.Name == name) Console.WriteLine($"Person: {item.Name} {item.Age}"); }  private static void FindStuff(string name, List<BankAccount> lst) {     foreach (var item in lst)         if (item.Name == name) Console.WriteLine($"Account: {item.Name} {item.AccountNumber}"); } |


Det fungerar men det innebär en del upprepad kod och som bra programmerare tycker man inte om upprepad kod.


Vi skapar nu ett interface, vars enda egenskap är att hålla koll på namn

| C# |
| --- |
| internal interface INameable {     public string Name { get; set; } } |


Och så anpassar vi klasserna. Vi behöver inte göra mycket mer än att lägga till interfacet, för att båda redan uppfyller kravet som den ställer. Båda har Name property.


| C# |
| --- |
| internal class Person : INameable {     public string Name { get; set; }     public int Age { get; set; } }  internal class BankAccount : INameable {     public string Name { get; set; }     public string AccountNumber { get; set; } } |


Nu kan vi kalla på namn-metoden utan att egentligen tänka på om det är ett bankkonto eller en person.


Här kommer första fina delen av Interfaces in. Man kan använda det för att gruppera klasser.

Så vi skapar ett interface för att kunna gruppera informationen.


| C# |
| --- |
| static void Main(string[] args) {     Console.WriteLine("Hello World!");      var person = new Person { Name = "Barnabas Collins", Age = 47 };     var account = new BankAccount { Name = "Savings account" };     GetName(person);     GetName(account);  }  private static void GetName(INameable named) {     Console.WriteLine(named.Name); } |


Testa att köra programmet och passa på att pusha ditt projekt till Git!

När du är klar med projektet, pusha allting till Github!

# Sammanfattning:

## Vad har vi lärt oss av detta exempel?

- Klasser som är av helt olika typer kan samlas genom en gemensam faktor som anges i ett interface.

- Metoder kan ta emot inparameter med Interface typer och använda objekten enligt interfacets regler för vad som ska finnas därinne.


## Vad kan göras bättre?

- Du skulle kunna ha ett interface som hanterar Namn, ålder, typ mm i en lista samla Husdjur, Pokemons, SCP objekt.

- På så sätt kan du anropa metoder som gör specifika saker med specifika delar av din klass, men ändå skydda informationen som inte har med metoden att göra.

---
Sådärja. Nu har du koll på det här. Nästa steg — testa själv. Det är då det fastnar.
