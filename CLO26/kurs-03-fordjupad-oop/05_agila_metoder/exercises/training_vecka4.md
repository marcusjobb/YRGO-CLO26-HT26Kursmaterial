# Träningsuppgifter: Fördjupad OOP — Vecka 4

> **Tema:** HELLWEEK — C# hidden gems  
> **Modul:** Inga moduler — discovery-vecka

## Instruktioner
Välj det bästa svaret. Klicka på 'Visa svar' för att se rätt svar och förklaringar.

### Fråga 1

Vad är skillnaden mellan `var` och `dynamic` i C#?

a. De är samma sak<br>b. `var` är statiskt typad vid kompilering, `dynamic` bestämmer typen vid körning<br>c. `dynamic` är statiskt typad, `var` är dynamisk<br>d. Ingen av dem kan ändra typ

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** `var` är statiskt typad vid kompilering, `dynamic` bestämmer typen vid körning

  **Förklaringar:**

  - ❌ **a) Samma sak** - FEL: Helt olika — `var` är bara bekvämlighet för statisk typning
  - ✅ **b) var statisk, dynamic runtime** - **RÄTT**: `var x = "hej"` — kompilatorn ser att x är en string. `dynamic y = "hej"` — typen kollas vid körning. `y.Skrik()` kompilerar (kollas inte förrän runtime) medan `x.Skrik()` ger kompileringsfel
  - ❌ **c) Omvänt** - FEL: Det är tvärtom — var är statisk, dynamic är runtime
  - ❌ **d) Kan inte ändra typ** - FEL: `dynamic` kan byta typ vid körning, `var` kan inte
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 2

Vad gör `?.` (null-condition operator)?

a. Kastar ett undantag om värdet är null<br>b. Anropar metoden bara om värdet INTE är null — annars returneras null<br>c. Sätter värdet till null<br>d. Skapar ett nytt objekt om värdet är null

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Anropar metoden bara om värdet INTE är null — annars returneras null

  **Förklaringar:**

  - ❌ **a) Kastar undantag** - FEL: Det är precis vad `?.` förhindrar — utan den skulle du få NullReferenceException
  - ✅ **b) Anropar bara om inte null** - **RÄTT**: `person?.Address?.City` — om person är null blir resultatet null utan undantag. Om person finns men Address är null blir det också null. Kedjan bryts vid första null
  - ❌ **c) Sätter till null** - FEL: `?.` kollar null, den sätter inte
  - ❌ **d) Skapar nytt objekt** - FEL: Det gör `??=` (null-coalescing assignment)
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 3

Vad gör `??` (null-coalescing operator)?

a. Kastar undantag om värdet är null<br>b. Returnerar värdet om det inte är null, annars ett standardvärde<br>c. Jämför två värden<br>d. Skapar en ny instans

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Returnerar värdet om det inte är null, annars ett standardvärde

  **Förklaringar:**

  - ❌ **a) Kastar undantag** - FEL: `??` är det säkra alternativet — det kastar inget
  - ✅ **b) Värde eller standard** - **RÄTT**: `string name = inmatning ?? "Okänd"` — om inmatning är null används "Okänd". `??` är ett mer koncist sätt än `inmatning != null ? inmatning : "Okänd"`
  - ❌ **c) Jämför** - FEL: Jämförelse är `==`, inte `??`
  - ❌ **d) Skapar instans** - FEL: `??=` skapar och tilldelar om värdet är null. `??` bara returnerar
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 4

Vad är en Tuple?

a. En oföränderlig klass<br>b. Ett sätt att returnera flera värden från en metod utan att skapa en klass<br>c. En typ av loop<br>d. En databas

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Ett sätt att returnera flera värden från en metod utan att skapa en klass

  **Förklaringar:**

  - ❌ **a) Oföränderlig klass** - FEL: En record eller immutable class är oföränderlig. Tuple kan vara det men det är inte definitionen
  - ✅ **b) Flera värden utan klass** - **RÄTT**: `(string Name, int Age) GetPerson() => ("Anna", 28)` och anropa med `var person = GetPerson(); person.Name` + `person.Age`. Inget behov av en separat Person-klass för tillfällig databärning
  - ❌ **c) Loop** - FEL: Tuple är en datastruktur, inte en kontrollstruktur
  - ❌ **d) Databas** - FEL: Ingenting med databaser att göra
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 5

Vad är en `record` i C#?

a. En klass med referenslikhet<br>b. En referenstyp med värde-likhet — två records är lika om deras data är samma, även om det är olika objekt<br>c. En typ av databas<br>d. Ett loggningsverktyg

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En referenstyp med värde-likhet — två records är lika om deras data är samma

  **Förklaringar:**

  - ❌ **a) Referenslikhet** - FEL: Vanliga klasser har referenslikhet (samma objekt). Records har värde-likhet
  - ✅ **b) Värde-likhet** - **RÄTT**: `record Person(string Name, int Age)`. `var a = new Person("Anna", 28); var b = new Person("Anna", 28);` — `a == b` är TRUE. Perfekt för DTOs och immutable data
  - ❌ **c) Databas** - FEL: En C#-datatyp, inte en databas
  - ❌ **d) Loggning** - FEL: Ingenting med loggning att göra
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 6

Vad är en extension method?

a. En metod som förlänger en klass utan att du behöver ärva eller ändra den ursprungliga koden<br>b. En metod som är väldigt lång<br>c. En metod som bara fungerar i debug-läge<br>d. En metod som tar bort funktionalitet

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En metod som förlänger en klass utan att du behöver ärva eller ändra den ursprungliga koden

  **Förklaringar:**

  - ✅ **a) Förlänga utan att ändra** - **RÄTT**: `public static bool IsValidEmail(this string str) => str.Contains("@");` Nu har ALLA strings metoden `"test@email.com".IsValidEmail()` — du har lagt till en metod på string utan att ändra String-klassen
  - ❌ **b) Väldigt lång** - FEL: Ingenting med längd att göra
  - ❌ **c) Debug-läge** - FEL: Extension methods fungerar överallt
  - ❌ **d) Ta bort** - FEL: Extension methods lägger till funktionalitet, den tar inte bort
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 7

Vad är pattern matching med `is`?

a. Att jämföra strängar med regex<br>b. Att kolla om ett värde matchar ett mönster — t.ex. `if (obj is string s)` — och samtidigt skapa en typad variabel<br>c. Att skapa designmönster i kod<br>d. Att matcha färger

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Att kolla om ett värde matchar ett mönster — och samtidigt skapa en typad variabel

  **Förklaringar:**

  - ❌ **a) Regex** - FEL: Regex används för textmönster, pattern matching är för C#-typer och värden
  - ✅ **b) Type check + deklarering** - **RÄTT**: `if (obj is string s) { Console.WriteLine(s.Length); }` — kollar om obj är en string OCH deklarerar variabeln s direkt. Modernare än `if (obj is string) { var s = (string)obj; ...}`
  - ❌ **c) Designmönster** - FEL: Pattern matching är en C#-språkfeature, inte ett designmönster
  - ❌ **d) Färger** - FEL: Ingenting med färger att göra
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 8

Vad är en `switch` expression i modern C#?

a. Samma som vanlig switch men med `break`<br>b. Ett kompakt sätt att göra switch-case som returnerar ett värde — utan `case`- och `break`-nyckelord<br>c. En switch som byter databas<br>d. En metod för att byta variabler

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Ett kompakt sätt att göra switch-case som returnerar ett värde

  **Förklaringar:**

  - ❌ **a) Vanlig switch** - FEL: Switch expressions är mer koncisa och returnerar värden
  - ✅ **b) Returnerar värde** - **RÄTT**: `string result = dag switch { 1 => "Måndag", 2 => "Tisdag", _ => "Okänd" };` — pilarna `=>` ersätter `case:` och `_` (discard) ersätter `default:`. Ingen `break` behövs
  - ❌ **c) Byta databas** - FEL: Handlar om C#-syntax, inte databaser
  - ❌ **d) Byta variabler** - FEL: Det är variabeltilldelning, inte switch
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 9

Vad tillåter en indexer i C#?

a. Bara arrayer och listor att använda `[index]`<br>b. Dina egna klasser att användas med hakparentes-syntax — `myClass[5]`<br>c. Indexering av databaser<br>d. Ett index över dina klasser

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Dina egna klasser att användas med hakparentes-syntax

  **Förklaringar:**

  - ❌ **a) Bara arrayer/listor** - FEL: Indexers är för att ge den funktionen till dina EGNA klasser
  - ✅ **b) Egna klasser med [index]** - **RÄTT**: `public string this[int index] { get { return _data[index]; } set { _data[index] = value; } }` — nu kan du göra `minSamling[5]` på din egen klass. Används ofta i samlingsklasser
  - ❌ **c) Indexering av databaser** - FEL: Databasindexering är något helt annat (SQL index)
  - ❌ **d) Ett index av klasser** - FEL: Indexers är en C#-språkfeature, inte en katalog
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 10

Vad gör `async/await` i C#?

a. Gör att metoden körs i en separat tråd<br>b. Låter metoden pausa vid `await` utan att blockera tråden, så tråden kan arbeta med annat medan en asynkron operation pågår<br>c. Gör koden långsammare<br>d. Skapar en ny process

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Låter metoden pausa vid `await` utan att blockera tråden

  **Förklaringar:**

  - ❌ **a) Separat tråd** - FEL: Vanlig missuppfattning! `async/await` handlar inte om trådar — det handlar om att *inte blockera* tråden
  - ✅ **b) Pausa utan att blockera** - **RÄTT**: När du anropar `await db.Heroes.ToListAsync()` — om det tar 2 sekunder så väntar inte tråden passivt. Den går tillbaka till sin pool och kan hantera andra anrop. När databasen svarar, återupptas metoden på en ledig tråd
  - ❌ **c) Långsammare** - FEL: För I/O-operationer (databas, filer, nätverk) är async EFFEKTIVARE för systemets totala genomströmning
  - ❌ **d) Ny process** - FEL: async/await påverkar inte processer, bara den aktuella tråden
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>
