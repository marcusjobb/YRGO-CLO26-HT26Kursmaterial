# Entity Framework Övning

🔴


Vi ska ta en titt på hjältar och deras styrkor, svagheter och annat skoj vi kan komma på


Övning 1 - Struktur

Vi börjar lätt med att skapa strukturen

- Skapa mappen "Database"

- Skapa mappen "Controllers"

- Installera Entity Framework nugets

- install-package Microsoft.EntityFrameworkCore.SqlServer

- install-package Microsoft.EntityFrameworkCore.Tools


# Övning 2 – Skapa modeller

I mappen modeller, skapa följande klass

```

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.


public class Hero
{
[Key]
public int ID { get; set; }
public string RealName { get; set; }
public string Alias { get; set; }
public List<string> Powers { get; set; }
}
```


# Övning 3 – Skapa databasklassen

I mappen Database skapar du en klass kallad HeroContext (valfri namn faktiskt). Det är alltid bra att döpa klassen till någonting+Context så man vet att det är en databasklass. Databasen kan heta något helt annat dock.

Din klass ska ärva från DbContext och skapa en DBSet med Hero klassen du skapade innan.

```


public class HeroContext : DbContext
{
private const string DatabaseName = "Heroes";
public DbSet<Hero> Heroes { get; set; }
```

```


protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
optionsBuilder.UseSqlServer(
          $@"Server = .\SQLEXPRESS;Database={DatabaseName};trusted_connection=true");
}
}
```


# Övning 4 – Skapa databasen och fixa buggarna

Nu ska vi köra Add-Migration


Vi får följande felmeddelande: The property 'Hero.Powers' could not be mapped because it is of type 'List<string>', which is not a supported primitive type or a valid entity type. Either explicitly map this property, or ignore it using the '[NotMapped]' attribute or by using 'EntityTypeBuilder.Ignore' in 'OnModelCreating'.

Vad betyder detta?

Att även om Entity Framework är smart och hjälper oss med våra objekt så kan den inte skapa en string array i vår tabell. Just nu ser vår klass ut såhär:

```


public class Hero
{
[Key]
public int ID { get; set; }
public string RealName { get; set; }
public string Alias { get; set; }
public List<string> Powers { get; set; }
}
```

Och den fungerar alltså inte.

För att lösa problemet skapar vi nu en ny klass

```


public class Power
{
[Key]
public int ID { get; set; }
public string Description { get; set; }
}
```

Vilket i och för sig är logiskt, vi vill spara en array av strängar – för det behövs en tabell med strängar, vilket vi inte angett. Alltså skapar vi en ny modell och ändrar vår Hero klass till att använda den istället. Vad som nu kommer att ske är att alla Powers vi ger vår hjälte kommer att sparas i tabellen Power, en styrka i taget.

```


public class Hero
{
[Key]
public int ID { get; set; }
public string RealName { get; set; }
public string Alias { get; set; }
public List<Power> Powers { get; set; }
}
```


Kör  Add-Migration igen och nu kommer det att fungera!


Nu testar vi vår databas från Main()

Men först måste vi köra Update-Database


La du märke till att vi inte skapade en DBset för Powers, men vår Update-Database gick igenom ändå.

Vi dubbelcheckar i SSMS, Yup där finns tabellen.

Men vi kommer att behöva den så vi får skapa en DBSet (i databasklassen) för Powers ändå.

```
public DbSet<Power> Powers { get; set; }
```

Add Migration och sedan Update-Database

Nu kommer Power tabellen att ha bytt namn till Powers.

Vi tar en titt på tabellen i databasen och ser att den skapat en Foreign till till Heroes på Powers.


När vi nu ger en hjältes superkrafter kommer dessa att kopplas automatiskt via databasen.


# Uppgift 4 – Skapa en hjälte


Vi provar med Clark Kent * Spoiler alert för alla som inte kan Clark Kents hemliga identitet *

```


static void Main()
{
using (var db = new HeroContext())
{
db.Heroes.Add(new Models.Hero
{
RealName = "Clark Kent",
Alias = "Superman",
Powers = new List<Power>
{
new Power { Description = "Flying" },
new Power { Description = "X-ray vision" },
}
});
db.SaveChanges();
}
}
```


Kör programmet och kontrollera med SSMS

I Heroes tabellen hittar vi nu


Och i Powers tabellen hittar vi nu


# Uppgift 5 – Alla mot alla

Det här är grymt coolt om vi bara vill koppla en viss egenskap till en viss hjälte (en till många relation), men nu är det så att det är mer än en hjälte som kan flyga eller har röntgensyn. Att koppla en egenskap enbart till en hjälte kommer att innebära att vi får skapa många likadana poster, och det vill vi inte. För databaser ska inte innehålla upprepad information.

Hur fixar vi detta?

I vår Power klass skapar vi en egenskap till, en lista med Heroes, detta innebär att vi kan leta efter en viss kraft och få reda på alla hjältar som har den kraften. Coolt va!

```


public class Power
{
[Key]
public int ID { get; set; }
public string Description { get; set; }
public List<Hero> Heroes { get; set; }
}
```

Kör Add-Migration och Update-Database


Observera varningen… vi kollar på den senare.

Nu ser våra tabeller ut såhär

Heroes


Powers


Nu finns ingen koppling mellan dem längre, men om vi tittar lite noggrannare ser vi att Entity Framework har skapat en tabell till

HeroesPower


Den här tabellen är tom, så Stålis har blivit av med sina gåvor  då databasens struktur ändrades drastiskt.

Tack vare kopplingstabellen har vi nu en många till många relation.


# Uppgift 6 – Fixa stålis krafter

Vi måste korrigera detta, det är lättare sagt än gjort! Vi får göra en hel del arbete för att det ska fungera, vi ändrar lite i vår using i main()

```

using (var db = new HeroContext())
{
```

}

Först ska vi leta reda på Clark

```

var clark = db.Heroes.Include("Powers").FirstOrDefault(s => s.RealName == "Clark Kent");
if (clark.Powers == null) clark.Powers = new List<Power>();
```


Då vår tabell (Heroes) nu innehåller en lista med objekt från en annan tabell ("Powers") så måste vi tala om för Entity Framework att ta med alla objekt som är kopplade i listan, den gör inte detta som standard för att inte flytta omkring med alldeles för mycket data. Genom kommandot Include("Powers") talar vi om för vår sökning att vi vill att all information som finns kopplat via Powers egenskapen ska tas med i vår sökning. Annars kommer Powers egenskapen att bli NULL.

Därefter söker vi som vanligt.

Vi kollar också att ifall Clarks förmågor är NULL så ska de bli en lista.

Nu söker vi upp första förmågan, detta söker vi i Powers tabellen efter "Flying", om vi får något svar hamnar det i power variabeln, annars blir variabeln NULL. Därför måste vi kolla så att den inte är NULL innan vi lägger in den i Clarks lista på förmågor.

```

var power = db.Powers.FirstOrDefault(p => p.Description == "Flying");
if (power != null) clark.Powers.Add(power);
```


Grymt! Nu söker vi efter nästa förmåga på samma sätt.

```

power = db.Powers.FirstOrDefault(p => p.Description == "X-ray vision");
if (power != null) clark.Powers.Add(power);
```


När vi är Klara uppdaterar vi Clark och sparar allt till databasen.

```

db.Update(clark);
db.SaveChanges();
```


# Uppgift 7 – Objektifiering av hjältarna

Vi skapar en hjälpklass till våra hjältar.

```

public static class HeroHelper
{
```

}

I den skapar vi metoden

```


public static Hero FindOrCreateHero(string name, string alias = "")
{
using (var db = new HeroContext())
{
var hero = db.Heroes.
Include("Powers").
FirstOrDefault(
h => h.RealName == name || // Sök på namnet eller
(alias != "" && h.Alias == alias) // Sök på alias om  ej ""
);
if (hero == null) // om hjälten inte finns, skapa den
{
hero = new Hero { RealName = name, Alias = alias };
db.Heroes.Add(hero);
db.SaveChanges(); // objektet uppdateras med ID efter save
}
return hero;
}
}
```

Metoden kommer att söka efter hjältens namn eller alias om alias är angivet, hjälten skapas om den inte finns.

```


public static Hero AddPower(Hero hero, string superPower)
{
using (var db = new HeroContext())
{
var power = db.Powers.FirstOrDefault(p => p.Description == superPower);
if (power == null)
{
power = new Power { Description = superPower };
db.Powers.Add(power);
}
if (hero.Powers == null) hero.Powers = new List<Power>();
/*
Rensa listan med powers, det tar inte bort dem från databasen
men det hindrar att EF försöker spara ALLA powers igen
*/
hero.Powers.Clear();
hero.Powers.Add(power);
db.Heroes.Update(hero);
db.SaveChanges();
}
return hero;
}
```

Denna metod tar emot en Hero och söker upp vald förmåga, om den inte finns så skapas den innan den läggs till vår hjälte. Vi provar den.

Lite då och då blir Stålmannen av med sina krafter, så vi behöver en metod för att ta bort krafterna med

```


public static Hero DeletePower(Hero hero, string superPower)
{
using (var db = new HeroContext())
{
var power = db.Powers.Include("Heroes").FirstOrDefault(p => p.Description == superPower);
if (power == null) return hero;
if (hero.Powers == null) return hero;
```

```


if (hero.Powers.Find(p => p.ID == power.ID) != null)
{
/*
Ta bort hjälten från powers listan
Av någon mystisk orsak bråkar den om man tar bort förmågan från
hjälten, men inte om man tar bort hjälten från förmågan :-/
```

```


För att försäkra oss om att hjälten är synkad med databasen
vi får alltså söka upp den igen, annars kan vi inte ta bort den.
*/
hero = db.Heroes.FirstOrDefault(h => h.ID == hero.ID);
power.Heroes.Remove(hero);
db.Update(power);
db.SaveChanges();
}
}
return hero;
}
```


# Uppgift 8 – Skapa Batman

```


static void Main()
{
var batman = HeroHelper.FindOrCreateHero("Bruce Wayne", "Batman");
HeroHelper.AddPower(batman,"Dark scary voice");
}
```


Addpower skulle kunna ta emot en array med förmågor och loopa igenom dem… men det kan du fixa


# Uppgift 9 – Lista av hjältar

Vi ändrar om main igen

```


static void Main()
{
using (var db = new HeroContext())
{
foreach (var hero in db.Heroes.Include("Powers"))
{
Console.WriteLine($"{hero.Alias}");
foreach (var power in hero.Powers)
{
Console.Write($"{power.Description}, ");
}
Console.WriteLine();
}
}
}
```


Vi kan med helt vanliga foreach loopa igenom våra hjältar och deras förmågor. Vi kan även lista alla hjältar med en specifik förmåga

```


foreach (var power in db.Powers.Include("Heroes"))
{
Console.WriteLine($"{power.Description}");
foreach (var hero in power.Heroes)
{
Console.Write($"{hero.Alias}, ");
}
Console.WriteLine();
}
```


# Uppgift 10 – Nu är det din tur

Nu ska du trixa till hjältedatabasen.

Glöm inte Add-Migration och Update-Database för varje ny ändring i modellerna eller

## Standarduttryck

Hjältarna brukar ha något speciellt uttryck, exempelvis Stålmannen "Up, Up and away"

Skapa en property i din hjältetabell där du kan spara hjältarnas standarduttryck.

## AddPowers()

Ifall du inte gjort det än, gör en metod som tar emot en hjälte och en string array med förmågor och kopplar alla dessa förmågor till hjälten (skapar dem om de om de inte finns).

## Dubbelchecka

Nu kollar vi om en förmåga finns, men vad händer om hjälten redan har förmågan, exempelvis stålmannen. Vad händer om vi lägger till att han ska kunna flyga, 2 gånger?

## Svagheter

Skapa en ny modell där man kan beskriva hjältarnas svagheter, exempelvis 
Namn: Grön kryptonit
Verkan: Dödar personer från planeten Krypton

Och koppla modellen till din hjältelista, så att olika hjältar kan ha olika eller samma svagheter (många till många)

# Lärdomar

Vad har vi lärt oss den här gången?

- Entity Framework tycker inte om stränglistor

- Vi kan använda modeller som typer i våra modeller

- En Modell som har en lista av andra modeller, får sitt ID som FK i den andra modellen

- Två modeller som hänvisar till varandra med en List<> eller Array får en kopplingstabell automagiskt

- När en ny post skapas i tabellen kommer Entity Framework att fylla på ID nummer i vår instans.

- Include(egenskapsnamn) krävs för att Entity Framework ska hämta värdena från kopplingstabellen

- FirstOrDefault returnerar NULL om den inte hittar något

- Att alltid söka i tabellen innan du skapar något, för att vara säker på att du inte skapar dubbletter

- När vi ska ta bort objekt från en Lista, dubbelkolla med databasen att objektet är synkat, annars kommer den inte att ta bort den – då den jämför exakt med modellen.

- När vi lägger till objekt i en lista kan den få för sig att uppdatera ALLA objekt i listan, vi kan slippa det problemet genom att rensa listan först.

- Om två modeller har varandra i List<> och du vill ta bort ett värde, ta bort det från den ena listan, funkar inte det ta bort den från den andra.

---
Sådärja. Nu har du koll på det här. Nästa steg — testa själv. Det är då det fastnar.
