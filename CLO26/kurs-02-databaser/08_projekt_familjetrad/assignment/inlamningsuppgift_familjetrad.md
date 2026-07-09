Din historia är en del av dig. Det som gör dig till den du är. Att forska i dina förfäders historia kan ge dig en del nya synvinklar om dig själv och alla människor en gång vandrat på jorden och alla deras val och handlingar som ledde till att du kom till att existera.

Så för att minnas de som vandrat före dig, och de som förberett din väg… och för att träna C# OOP och SQL så ska vi göra ett enkelt litet program för att hålla koll på ett familjeträd.

Det här låter som en enorm börda att göra, men så är det inte.

Om du gjort övning 9 i "Tisdagsuppgifter" så har du redan gjort halva projektet.

Ditt familjeträdsprojekt ska bestå av följande:

- Databas :

- En databas som du skapar från C#,

- En eller flera tabeller som du skapar från C#

- Program

- C# Objekt som speglar tabellen/tabellerna i din databas

- En CRUD klass om kan hantera dina objekt

- Funktionalitet

- Skapa en person

- Ange personens föräldrar

- Sök i databasen först, annars skapa dem

- Om personen finns redan, visa och föreslå att den ska användas

- Editera person

- Radera person

- Lista alla personer

- Som börjar på en viss bokstav

- Födda ett visst år

- Personer som saknar vissa data exempelvis:

- Födelsedata

- Föräldrar

- Visa mor/far föräldrar till en person

- Visa syskon till en person


# Kodgrund

🔴


Du ska använda dig av kod i stil med koden för Tisdagsuppgift 9.

Skapa en person klass och en CRUD klass.

Exempel på kod (det behöver inte se ut så i din kod)

```

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.


internal class Person
{
public int Id { get; set; }
public string Namn { get; set; }
public string Efternamn { get; set; }
public string Födelsedatum { get; set; }
public string Dödsdatum { get; set; }
public int Mor { get; set; }
public int Far { get; set; }
}
```

```


public class GenealogiCRUD
{
    public string DatabaseName { get; set; } = "Genealogi";
public int MaxRows { get; set; } = 10; // Max rows to return when searching
public string OrderBy { get; set; } = "lastName";
public void Create(Person person) {/* Massor med kod */}
public void Delete(Person person) {/* Massor med kod */}
public bool DoesPersonExist(string name) {/* Massor med kod */}
public bool DoesPersonExist(int Id) {/* Massor med kod */}
public void GetFather(Person person) {/* Massor med kod */}
public void GetMother(Person person) {/* Massor med kod */}
public List<Person> List(string filter = "firstName LIKE @input", string paramValue) {/* Massor med kod */}
public Person Read(string name) {/* Massor med kod */}
public void Update(Person person) {/* Massor med kod */}
}
```


Informationen som ska sparas är följande

- Namn

- Efternamn

- Födelsedatum (år räcker)

- Dödsdatum (år räcker)

- Mor (enbart ID till en annan person)

- Far (enbart ID till en annan person)

- Vill du lägga till fler detaljer är det helt OK.

Du får skriva programmet i Consol eller i Windows Forms, vilket som känns trevligast för dig.

All hantering av personer ska vara med OOP, enbart CRUD klassen ska använda SQL.

Du får gärna lägga till fler metoder i CRUD klassen för att hantera mer specifika sökningar (exempelvis DoesPersonExist mm), det rekommenderas men det är inget krav.

Syskon hittar du genom att söka efter personer med samma Mor och/eller Far.

Familjeträdet behöver inte vara din egen, den kan vara fiktiv eller historisk.


Krav för Godkänt

- Skapa databasen från C# (om de inte finns, eller göm error meddelande om databasen finns)

- Skapa tabell/tabeller från C# (om de inte finns, eller göm error meddelande om tabellen finns)

- När databasen skapas, lägg till några personer som startpunkt för ditt familjeträd

- En klass som hanterar dina personer

- En CRUD klass som hanterar dina objekt

- All hantering av personer ska vara med OOP, enbart CRUD klassen ska använda SQL.

- Ett fungerande program som kan

- Visa namnlistor (exempelvis som börjar på viss bokstav, eller födda visst år)

- Skapa / Ändra / Hitta / Radera personer

- Visa Mor/Far-föräldrar (en generation bakåt bara)

- Visa en lista på barn

- Familjeträdet ska innehålla minst 3 generationer (kusiner är dock inget krav)

- Alla SQL frågor som hanterar inputs från användaren ska använda SQL-parametrar

- En enkel Diagram som visar hur du tänkt dig databasen

Krav för Väl Godkänt

- Att alla krav för Godkänt uppfylls

- Att Födelsestad, Land läggs till personen

- Att Dödsstad, Land läggs till personen

- Lista personer efter Födelsestad

- Väl kommenterad kod

Ninja skillz (dock blir betyget ändå bara VG)

- Lista kusiner

- Att programmet ska kunna visa Mor/Farbröder (en generation bak)

- Att personobjektet returnerar en Personobjekt när man kollar på Mor/Far istället för bara ID
(sök inte upp föräldern förrän man frågar om det för att slippa rundgång)


# Exempel på hur programflödet ska fungera


- Skapa en person "Anakin Skywalker"

- Skapa en person "Padmé Amidala"

- var mor =Hämta("Padmé Amidala")

- var far = Hämta("Anakin Skywalker")

- Lägg till barn

- Skapa person "Luke Skywalker", sätt mor och  far

- Skapa person "Leia Skywalker", sätt mor och  far

- Byt efternamn på "Leia Skywalker" till "Organa"

- Skapa person "Mara Jade"

- var mor =Hämta("Mara Jade")

- var far = Hämta("Luke Skywalker")

- Lägg till barn

- Skapa "Ben Skywalker", sätt mor och far

- Lista personer enligt efternamn
Padmé Amidala
Leia Organa
Anakin Skywalker
Ben Skywalker
Luke Skywalker

- Visa anfäder Ana (Anfader/Anmoder) till "Ben Skywalker"

- Padmé Amidala

- Anakin Skywalker

# Tips!

- Keep it simple!

- Lägg inte till coola funktioner som inte krävs förrän du är klar med VG

- Gör G delen först, sedan VG

- Du har redan grunden för projektet från uppgift 9, uppfinn inte om hjulet

- Använd Git och Comitta ofta

- Var inte rädd för att fråga om hjälp

- Gör din egen kod dock, man lär sig mer på att programmera själv än att använda färdig kod

- Diskutera gärna uppgiften

- Lär av varandra

- Det är helt OK att fråga lärare om mer förklaringar eller kodförslag

- Kommentera din kod
