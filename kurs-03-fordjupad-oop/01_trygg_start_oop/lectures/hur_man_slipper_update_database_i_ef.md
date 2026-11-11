# Hur man slipper Update-Database

🟡



Update-Database är något man ofta glömmer när man arbetar med Entity Framework. Så hur kan man göra för att databasen ska uppdatera sig själv vid programstart (om det finns nya migrations).

Vi börjar med att skapa en databasklass precis som vanligt

```


public class MyDatabase : DbContext
{
public DbSet<Name> Names { get; set; }
public MyDatabase(DbContextOptions<MyDatabase> options) : base(options) { }
public MyDatabase() : base(new ContextFactory().CreateOptions()) { }
}
```


De två sista raderna är kryptiska, innan har vi använt oss av en annan metod

```


protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
optionsBuilder.UseSqlServer(
$@"Server = .\SQLEXPRESS;Database={DatabaseName};trusted_connection=true");
}
```

Men den skippar vi den här gången, vi bygger upp vår konfiguration till databasen på i en annan klass.

Men innan vi går vidare ska vi ta en titt på vad som händer här.

Vad gör den här raden?

```
public MyDatabase(DbContextOptions<MyDatabase> options) : base(options) { }
```


Det är en constructor till min databasklass, och den tar emot en parameter med databasinställningar i stil med det vi gör i OnConfiguring metoden, i ärlighetens namn, det som skapas i OnConfiguring metoden är precis det vi skickar in till Constructorn där, den skickar informationen vidare till huvudklassen.

Nästa rad är nästan likadan, det är en constructor som inte tar emot parametrar.

```
public MyDatabase() : base(new ContextFactory().CreateOptions()) { }
```


När den kallats kommer den att anropa en metod i klassen ContextFactory för att få databasinställningar och skickar det sedan vidare till huvudklassen.

Detta behövs egentligen inte, men jag la till det för att slippa krånglig kod.


Nu tar vi en titt på den mystiska klassen som anropas av constructorn.

```


class ContextFactory : IDesignTimeDbContextFactory<MyDatabase>
{
private const string DatabaseName = "NameList";
public MyDatabase CreateDbContext(params string[] args)
{
return new MyDatabase(CreateOptions());
}
```

```


public DbContextOptions<MyDatabase> CreateOptions()
{
var optionsBuilder = new DbContextOptionsBuilder<MyDatabase>();
optionsBuilder.UseSqlServer($@"Server = .\SQLEXPRESS;Database={DatabaseName};trusted_connection=true");
return optionsBuilder.Options;
}
}
```


Den här klassen är mer mystisk, den baserar sig på interfacet IDesignTimeDbContextFactory, man använder detta interface just för att arbeta med DBContext kodmässigt. Den blir "typad" till vår databasklass. Detta för att den generiska delen av interfacet, då anpassar sig interfacet till vår databas-klass.

I klassen skapar vi en variabel för att hålla koll på databasnamnet. Inget märkvärdigt där, det är precis som det vi gjort i databasklassen innan.

Metoden CreateDBContext skapar en instans av databasen och returnerar det. Innan den gör det kommer den dock att skapa en instans av Databasinställningarna. Den här metoden behövs egentligen inte och koden kan flyttas in i CreateDbContext() men då kan vi inte anropa den från Constructorn i vår databasklass.

CreateOptions() är en enkel metod som bara skapar databasinställningar för vår databas. Har vi fler konfigurationsinställningar så kan vi lägga dem i den här lilla metoden med. Det enda metoden gör att att skapa inställningarna och returnera dem.

Det är allt vad vår klass för att skapa DatabasContext ska göra.

Vi behöver en klass till för att det ska fungera, och det är en klass som talar om för vårt program att uppdatera databasen och köra alla migrationer. Det är en superenkel.

```


public static class DatabaseCreator
{
public static void Create()
{
var contextFactory = new ContextFactory();
using (var dbContext = contextFactory.CreateDbContext())
{
dbContext.Database.Migrate();
}
}
}
```


Vad den gör är att den skapar en instans av ContextFactory klassen och anropar metoden som skapar en Context av den. När den fått en kontext kommer den att anropa den inbyggda metoden i databasen för att "Update-Database" från koden.

I main kan vi nu köra DatabaseCreator.Create(); Vid programstart så skapas databasen.

Nu kan vi alltså skapa våra klasser, köra "Add-migration" och sedan bara starta programmet.

Mer information om "Factory" pattern finns här:

https://www.c-sharpcorner.com/article/factory-method-design-pattern-in-c-sharp/

och

https://refactoring.guru/design-patterns/factory-method


Lite mer läsning om detta

https://docs.microsoft.com/en-us/ef/ef6/modeling/code-first/migrations/

---
Och kom ihåg: allt vi gått igenom här är grunden. Resten bygger på det. Så var inte rädd att experimentera.
