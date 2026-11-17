# Labb 6 — Dependency Injection 🔴

## Vad du ska bygga

I labb 5 laddade vi husdjuren för hand i `Program.cs`, och handlers skapades med `new`. Nu flyttar vi logiken till två *services*, `HeroService` och `PetService`, och låter en DI-container skapa allt åt oss. Det är samma mekanism som ASP.NET Core använder.

DI i en konsolapp känns överdrivet. Det är det också, om man bara ser appens storlek. Vi gör det här i en konsolapp för att isolera begreppet. I en riktig webbapplikation är det containern som sätter ihop allt, och du skriver samma registreringsrader.

*Förutsättning: du har `IDbHandler<T>`, `Hero`, `Pet` och de generiska handlers från labb 5. Du skapar `HeroService` och `PetService` i den här labben.*

## Steg

1. Lägg till NuGet-paketet för DI-containern:
   ```
   dotnet add package Microsoft.Extensions.DependencyInjection
   ```

2. Skapa `HeroService`. Den tar emot *båda* handlers i konstruktorn och har metoden `GetWithPets`, som gör den laddning du skrev för hand i labb 5:
   ```csharp
   public class HeroService
   {
       private readonly IDbHandler<Hero> _heroDb;
       private readonly IDbHandler<Pet> _petDb;

       public HeroService(IDbHandler<Hero> heroDb, IDbHandler<Pet> petDb)
       {
           _heroDb = heroDb;
           _petDb = petDb;
       }

       public void Add(Hero hero) => _heroDb.Insert(hero);
       public Hero? Get(string name) => _heroDb.Find(name);
       public List<Hero> Search(string token) => _heroDb.Search(token);
       // GetWithPets(string name): hämta hero, ladda husdjuren, returnera. Du skriver den.
   }
   ```

3. Skapa `PetService` på samma sätt med `IDbHandler<Pet>` (`Add`, `Get`, `Search`).

4. I `Program.cs`: bygg upp en service collection och registrera beroenden:
   ```csharp
   var services = new ServiceCollection();

   services.AddSingleton<IDbHandler<Hero>>(new SQLiteHandler<Hero>(connectionString));
   services.AddSingleton<IDbHandler<Pet>>(new SQLiteHandler<Pet>(connectionString));

   services.AddTransient<HeroService>();
   services.AddTransient<PetService>();

   var serviceProvider = services.BuildServiceProvider();
   ```

5. Skapa tabellerna med handlers som du hämtar ur containern (`serviceProvider.GetRequiredService<IDbHandler<Hero>>()`). Hämta sedan services och använd dem:
   ```csharp
   var heroService = serviceProvider.GetRequiredService<HeroService>();
   var petService = serviceProvider.GetRequiredService<PetService>();
   ```

6. Byt databas: ändra `SQLiteHandler<Hero>` till `TiDBHandler<Hero>` (och samma för `Pet`) i registreringen. Resten av koden förblir orörd. Det är poängen.

## Förväntad output

*(Samma data som labb 5, men hämtad via services)*

```
=== Databas skapad ===
=== Tabeller skapade ===

=== Heroes ===
Bella inlagd (id: 1)
Carlos inlagd (id: 2)

=== Pets ===
Mjao inlagd (id: 1, heroId: 1)
Woff inlagd (id: 2, heroId: 1)
Sture inlagd (id: 3, heroId: 2)

=== HeroService.GetWithPets: Bella ===
Hero { Id = 1, Name = Bella, Age = 28, City = Malmö }
  Pet: Mjao (Katt)
  Pet: Woff (Hund)

=== HeroService.Search: ar ===
Hero { Id = 2, Name = Carlos, Age = 34, City = Göteborg }

=== PetService.Search: o ===
Mjao, Katt (heroId: 1)
Woff, Hund (heroId: 1)

=== PetService.Get: Saknas ===
Hittade inget husdjur med det namnet.
```

## Kriterier

**G**
- `HeroService` och `PetService` tar emot sina interface i konstruktorn
- `GetWithPets` laddar husdjuren via `IDbHandler<Pet>`
- DI-containern registrerar och skapar services
- Inga `new SQLiteHandler`, `new HeroService` eller `new PetService` utanför registreringen

**VG**
- Du kan förklara skillnaden mellan `AddSingleton`, `AddScoped` och `AddTransient`
- Du kan förklara vad som händer om du byter `SQLiteHandler` mot `TiDBHandler` i registreringen
- Du kan förklara varför `HeroService` inte vet, och inte ska veta, vilken databas som används
- Du kan förklara varför `HeroService` behöver *två* handlers, och vad det säger om serviceklassens ansvar
- Du kan sätta ett namn på mönstret: *"Detta kallas Dependency Injection. Så här fungerar ASP.NET Core."*
