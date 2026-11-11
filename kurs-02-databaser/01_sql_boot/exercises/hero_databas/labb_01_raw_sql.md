# Labb 1 — Raw SQL 🟢

## Vad du ska bygga

Du ska skapa en SQLite-databas direkt i `Program.cs` — utan klasser, utan handlers. Allt SQL skrivs rakt in i koden. Det funkar. Det är också rörigt. Det är poängen.

---

## Varför appsettings.json?

### Konfiguration hör inte hemma i koden

Konfiguration — connectionstrings, sökvägar, inställningar — och kod är två olika saker och ska bo på två olika ställen. Connectionstring hårdkodad i koden:

```csharp
var connectionString = "Data Source=heroes.db";
```

...gör att du måste ändra och kompilera om koden varje gång miljön förändras: ny server, ny sökväg, ny databas. Det är ett designfel. Kod och konfiguration blandas ihop.

Med `appsettings.json` och `IConfiguration` gäller detta från dag ett:

```csharp
var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
var connectionString = config.GetConnectionString("Default");
```

`Program.cs` vet inte längre var databasen finns — och ska inte veta det. Du kan byta miljö (utveckling, test, produktion) utan att röra ett enda tecken i koden.

### Varför committar vi inte appsettings.json?

Git-historiken är permanent. Om du pushar ett lösenord eller en API-nyckel till ett publikt repo — även om du tar bort filen i nästa commit — är nyckeln läckt. Botar skannar GitHub efter hemligheter inom minuter.

Lösningen: lägg `appsettings.json` i `.gitignore` och checka istället in en `appsettings.example.json` med platshållare:

```json
{
  "ConnectionStrings": {
    "Default": "Data Source=DIN_DATABASFIL.db"
  }
}
```

Den berättar för andra vilka nycklar som behövs, utan att avslöja några riktiga värden. I README skriver du: *"Kopiera `appsettings.example.json` till `appsettings.json` och fyll i dina värden."*

### Nästa steg (utanför den här kursen)

`appsettings.json` är inte rätt plats för riktiga hemligheter ens på din egna maskin. Nästa steg — som du inte behöver implementera nu — är:
- **Utveckling:** `dotnet user-secrets` — lagrar utanför projektet, hamnar aldrig i git
- **Produktion:** miljövariabler eller Azure Key Vault

I labb 4 introduceras TiDB-lösenord — det är ett exempel på när det blir säkerhetskritiskt.

---

## Steg

1. Skapa ett nytt Console-projekt:
   ```
   dotnet new console -n HeroDb
   cd HeroDb
   ```

2. Lägg till NuGet-paket:
   ```
   dotnet add package Microsoft.Data.Sqlite
   dotnet add package Microsoft.Extensions.Configuration.Json
   ```

3. Skapa `appsettings.json` i projektroten:
   ```json
   {
     "ConnectionStrings": {
       "Default": "Data Source=heroes.db"
     }
   }
   ```

4. Lägg till `Copy to output directory` i `.csproj` — annars hittar inte programmet filen när det körs:
   ```xml
   <ItemGroup>
     <None Update="appsettings.json">
       <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
     </None>
   </ItemGroup>
   ```

5. Skapa `appsettings.example.json` med platshållare — den ska checkas in i git:
   ```json
   {
     "ConnectionStrings": {
       "Default": "Data Source=DIN_DATABASFIL.db"
     }
   }
   ```

6. Skapa `.gitignore`:
   ```
   bin/
   obj/
   *.db
   appsettings.json
   ```

7. Läs connectionstring i `Program.cs`:
   ```csharp
   var config = new ConfigurationBuilder()
       .AddJsonFile("appsettings.json")
       .Build();
   var connectionString = config.GetConnectionString("Default");
   ```

8. Börja med `DROP TABLE IF EXISTS heroes` så att programmet kan köras om med samma utskrift. Skapa sedan tabellen `heroes` med kolumnerna:
   - `id` — INTEGER PRIMARY KEY AUTOINCREMENT
   - `name` — TEXT
   - `age` — INTEGER
   - `city` — TEXT

9. Infoga tre heroes med `INSERT INTO`-satser direkt i koden. Använd parametrar (`@name`), inte strängkonkatenering

10. Implementera Find: hämta en hero med ett givet namn och skriv ut resultatet

11. Implementera Search: sök efter heroes vars namn innehåller ett givet sökord (`LIKE`) och skriv ut alla träffar

## Förväntad output

```
=== Databas skapad ===
Tabell skapad.

=== Infoga heroes ===
Bella inlagd.
Carlos inlagd.
Zara inlagd.

=== Find: Bella ===
Hittade: Bella, 28 år, Malmö

=== Search: ar ===
Carlos, 34 år, Göteborg
Zara, 22 år, Stockholm
```

## Kriterier

**G**
- Databasfilen `heroes.db` skapas och programmet kan köras flera gånger i rad med samma utskrift
- Tre heroes infogas utan fel
- Find returnerar rätt hero
- Search returnerar alla matchande heroes
- `appsettings.json` finns i `.gitignore`
- `appsettings.example.json` är incheckad i git

**VG**
- SQL-parametrar används (ingen string-konkatenering i WHERE-satser)
- Koden körs utan varningar
- Du kan förklara varför connectionstring inte ska ligga i koden, även för en lokal SQLite-fil
- Du kan svara på: *"Vad gör du om du råkat pusha ett lösenord till GitHub?"*  
  *(Svar: rotera/byt lösenordet direkt. Att ta bort filen i nästa commit räcker inte.)*
