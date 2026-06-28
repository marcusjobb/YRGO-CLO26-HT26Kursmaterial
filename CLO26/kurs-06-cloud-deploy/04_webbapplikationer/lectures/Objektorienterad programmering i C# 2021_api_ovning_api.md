---

title: Övning Api
author: Marcus Ackre Medina
type: lecture
topic: api
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/Objektorienterad programmering i C# 2021/API/Övning API.docx"
description: "Klicka eller tryck här för att ange datum."
tags: ["api", "api.docx", "csharp", "git", "oop", "test", "övning"]
week_fit: []
---
Campus Mölndal .Net 21
Marcus Medina
Klicka eller tryck här för att ange datum.

Övning API
Beskrivning:

I den här övningen ska vi skapa ett projekt som kopplar sig till ett API och hämtar data

Kursplanstermer som berörs av uppgiften:
Mål

För godkänt krävs

Kunskap om innehållet i .NET-biblioteket

Den studerande redogör grundligt kring innehållet i .NET-biblioteket.

Kunskaper kring typer, variabler, operationer, uttryck,
villkorssatser och loopar inom programmering.

Den studerande redogör för hur typer, variabler, uttryck,
villkorssatser och loopar används inom programmering.

Kunskap kring namngivning och kodstruktur av klasser,
metoder och variabler i objektorienterade program.

Den studerande redogör för hur namngivning och kodstruktur av
klasser, metoder och variabler i objektorienterade program används.

Utveckla program med en tydligt objektorienterad
struktur.

Den studerande utvecklar program med en tydligt objektorienterad
struktur.

Förstå och använda sig av datastrukturer inom
programmering.

Den studerande använder sig av datastrukturer i sin
mjukvaruutveckling.

Planera, designa och implementera gränssnitt utifrån
användaren.

Den studerande planerar, designar och implementerar gränssnitt
utifrån användaren.

Utveckla felfria fristående program.

Den studerande skapar felfria fristående program.

Termer för övningen:



API = Application programming interfaces. Det är ett port som guidar in oss i en annan
applikation, exempelvis en molndatabas.
Nugets = Samling förkompilerade klasser som läggs till i ditt projekt

Psuedokod:
1.
2.
3.
4.
5.

Skapa nödvändiga nycklar
Skapa strängar för frågor
Installera Nugets
Fråga APIn om filmdata
Visa datan

int page = 0;

Campus Mölndal .Net 21
Marcus Medina

Projektinstruktioner:
Detta projekt går utmärkt att göra tillsammans med andra, antingen i basgruppen eller i par. Välj en
klasskamrat du vill arbeta med och använd LiveShare eller Discordens Screen Share för att
samarbeta.

Kodning:
Vi börjar med att skapa ett c#, .net konsolprojekt.
Skapa nu ett Github repo för ditt projekt (om du inte sparar i ett redan existerande repo)

Filmsökning via OMDB API
Då vi pratar om APIer så lät det som en bra idé att använda sig av OMDB API (Open Movie Database).
OMDB APIn är väldigt enkel och fungerar genom vanliga HTTP anrop. Tyvärr kan man inte söka på
skådespelare än.
http://www.omdbapi.com/?apikey=(API-nyckel)&t=Halloween
http://www.omdbapi.com/?apikey=(API-nyckel)&t=Final+Destination&y=2000
http://www.omdbapi.com/?apikey=(API-nyckel)&t=Silent+Hill&type=movie
http://www.omdbapi.com/?apikey=(API-nyckel)&t=Charmed&=serie&plot=full

Genom enkla parametrar i HTTP strängen kan vi alltså söka på filmer och få tillbaka JSON objekt, som
vi sedan kan omvandla till C# objekt och använda hur vi vill.
Till att börja med behöver vi ett API nyckel, den får vi här: http://www.omdbapi.com/apikey.aspx

När allt är klart får vi ett mail med koden.

int page = 1;

Campus Mölndal .Net 21
Marcus Medina

Klicka på sista länken för att aktivera koden och sen kör vi!
Om vi tittar på webbsidan och klickar på ”Examples” i menyn (eller scrollar ner en massa), så kan vi
testa sökningar. Där vi får faktiskt reda på hur vi ska söka. Bättre upp, vi får även se JSON koden som
genereras.

Nu ska vi följa vissa steg
1.
2.
3.
4.
5.

Öppna Visual Studio och skapa ett projekt
Kopiera JSON koden från webbsidan
I ditt projekt, skapa en class (namnet är oviktigt)
I menyn ”Edit”, välj ”Paste Special” och sedan välj ”JSON as classes”
Nu kommer Visual Studio att generera klasserna för dig

int page = 2;

Campus Mölndal .Net 21
Marcus Medina
6. Den första klassen heter Rootobject, döp om den till Movie
7. Längre ner i dokumentet finns även klassen Rating den är bra som den är
8. Flytta ut rating klassen till en egen fil (en klass per fil)
Nu ska vi skapa en metod för att ladda ner JSON objekt
private Movie GetMovie(string url)
{
string json = string.Empty;
using (var wc = new System.Net.WebClient())
{
json = wc.DownloadString(url);
}
return JsonConvert.DeserializeObject<Movie>(json);
}

Vad gör metoden, steg för steg a’la Rubber duck metod.
Först skapar den en tom sträng kallad json. So far so good.
Därefter skapar den en System.Net.WebClient(), och vad är det? Enligt Microsofts hemsida så är det
ett objekt som används för att skicka och ta emot data från C#. Den är dock lite gammal, men vi
kommer att använda det (mest för att slippa Async metoder). WebClient fungerar som en
webbläsare, men i kodform. Den kan ladda ner webbsidor och filer som man sedan kan spara i
hårddisken. I detta fall använder vi DownloadString() metoden för att just ladda ner text.
https://docs.microsoft.com/en-us/dotnet/api/system.net.webclient?view=net-5.0
Vi gör det väldigt enkelt, vi talar om för WebClient att hämta hem en webbsida. Och omvandlar
resultatet till ett JSON objekt.
Det låter enkelt, men det är lite mer komplicerat än så.
För att omvandla JSON kod till en klass behöver vi en JSON parser, den bästa som finns att tillgå (som
även Microsoft rekommenderar) är NewtonSoft. I View menyn i Visual Studio, sök upp “Other
Windows”, och där “Package manage console” för att kunna installera nuget. Den får installeras som
nuget med kommandot
Install-Package Newtonsoft.Json -Version 12.0.3

Därefter kan vi anropa dess funktion för att deserialisera, att omvandla en JSONfil till ett objekt. För
att den ska veta vad den ska göra så får vi instansiera den med typen (som är generisk) med den typ
vi vill ha tillbaka.
JsonConvert.DeserializeObject<Movie>(json);

Vi anropar alltså den statiska klassen JsonConvert och i den hittar vi en statisk generisk metod som vi
använder för att omvandla objektet. (Jag ska förklara Generiska metoder och klasser sen.)
Vi returnerar det vi får från DeserializeObjekt och så är metoden klar.

int page = 3;

Campus Mölndal .Net 21
Marcus Medina
Nu ska vi prova att göra en sökning. För att det ska fungera behöver vi nyckeln du fick i mailet från
OMDB. Jag la min nyckel i en statisk klass kallad ”Settings” med propertyn ”Key”.
public static class Settings
{
public static string Key { get; set; } = "xxxxxxxxx";
}

Nu gör vi en test-sökning efter en film. Koden betyder IMDB-ID = tt1205489.
var url = $"http://www.omdbapi.com/?apikey={Settings.Key}&i=tt1205489";
var movie = GetMovie(url);
Console.WriteLine(movie.Title + "," + movie.Year);
Console.WriteLine(movie.Actors);

Det finns mer information i klassen men just nu nöjer vi oss med att se att den fungerar. Vi skapar nu
en metod för att göra sådana anrop.
public Movie GetByIMDBCode(string id)
{
var url = $"http://www.omdbapi.com/?apikey={Settings.Key}&i={id}";
return GetMovie(url);
}

På OMDBs hemsida kan vi se att det finns fler parametrar vi kan använda

int page = 4;

Campus Mölndal .Net 21
Marcus Medina
Om vi vill söka på titel istället för IMDB kod, så använder vi
var url = $"http://www.omdbapi.com/?apikey={Settings.Key}&t={title}";

Vill vi söka på titel och år lägger vi till
var url = $"http://www.omdbapi.com/?apikey={Settings.Key}&t={title}&y={year}";

Vill vi söka på serier (movie, serie, episode) till exempel och år lägger vi till
var url = $"http://www.omdbapi.com/?apikey={Settings.Key}&t={title}&type=movie";

Om vi vill ha en lång beskrivning av handlingen använder vi
var url = $"http://www.omdbapi.com/?apikey={Settings.Key}&t={title}&={year}&plot=full";

Nu skapar vi en metod för att hämta en specifik titel
public Movie GetByTitle(string title, string year="", bool longPlot=false)
{
var url = $"http://www.omdbapi.com/?apikey={Settings.Key}&t={title}&y={year}";
if (longPlot) url += "&plot=long";
return GetMovie(url);
}

Men nu vill vi kunna söka. Hur gör vi det?
Nu är det så att svaren man får vid en sökning skiljer sig från svaren när man hämtar en specifik film,
så vi får skapa en ny uppsättning klasser. Vi börjar med att öppna webbläsaren och skriva
http://www.omdbapi.com/?apikey={DinEgenNyckel}&s=Spider
(glöm inte att använda din nyckel)
Vi får då fram en JSON sträng som vi kopierar.

Klistra in den koden i en classfil (Edit/Paste special/Paste JSON as Classes)
Återigen får du en klass som heter RootObject, döp om den till SearchResult. Vi kan inte döpa den till
Search då det finns en egenskap som heter så.

Nu kan vi göra en ny metod som vi har till sökningar
int page = 5;

Campus Mölndal .Net 21
Marcus Medina
private SearchResult SearchMovie(string url)
{
string json = string.Empty;
using (var wc = new System.Net.WebClient())
{
json = wc.DownloadString(url);
}
var obj = JsonConvert.DeserializeObject<SearchResult>(json);
return obj;
}

Denna metod fungerar precis som metoden för att hämta en specifik film, enda skillnaden är att den
omvandlar JSON koden till ett SearchResult objekt.
Nu testar vi att söka från main
var url = $" http://www.omdbapi.com/?apikey={Settings.Key}&s={title}";
var result = SearchMovie(url);
Console.WriteLine(result.totalResults+" movies found");
foreach (var item in result.Search)
{
Console.WriteLine(" "+item.Title + " " + item.Year);
}

Vi får en lista med matchande filmer

På sidan kan vi se att vi kan använda parameterar som

&type=movie / series / episode,
&y=year
Vi kan alltså ha dessa parametrar i våra sökningar, så nu skapar vi en metod
public SearchResult Search(string title, string year = "", string type = "", string page = "1")
{
var url = $"http://www.omdbapi.com/?apikey={Settings.Key}&s={title}&y={year}&type={type}&page={page}";
return SearchMovie(url);
}

Page parametern är inte angiven men den behövs, det är nämligen så att om sökningen returnerar
mer än 10 filmer så kommer vi bara att få 10 filmer i listan. Detta för att inte behöva skicka hur
många filmer som helst i en lista.
int page = 6;

Campus Mölndal .Net 21
Marcus Medina
Så vi måste kompensera för detta om vi vill ha en lista på alla filmerna.
Ett sätt är att loopa sökningen och anropa den så många gånger vi behöver. Detta kan vi göra på två
sätt. Antingen att vi loopar tills vi får null som svar, eller att vi kollar hur många filmer som kommer
med i sökningen och loopa antal sidor/antal filmer.
Det låter rörigare än vad det är. Vi kör en Do While();
int movies = 0;
int page = 0; // Börja med sida noll
do
{
page++; // Öka sidnummer
var search = omdb.Search("Catwoman", "", "", page.ToString()); // Sök på Catwoman
movies = int.Parse(search.totalResults); // Kolla hur många träffar som finns
PrintMovies(search); // Skriv ut resultatet
}
while (movies > page * 10); // Så länge sidoantal*10 är mindre än antal träffar, fortsätt

Även om det är en smart lösning så är det inte det mest rekommenderade, de har begränsat antalet
sökningar till 1000 om dagen och antalet svar man får från sökningen för att man inte ska belasta
servern. Då det är en gratis tjänst ska vi vara snälla. Vi kan fråga användare om denne vill söka mer,
och i så fall fråga servern igen.
För att kunna skriva ut det gör vi ett par enkla metoder
private static void PrintMovies(SearchResult result)
{
Console.WriteLine($"{result.totalResults} movies found");
foreach (var item in result.Search)
{
Console.WriteLine($" {item.Title} {item.Year}");
}
}
private static void PrintMovie(OMDBHelper.Movie movie)
{
Console.WriteLine($"{movie.Title},{movie.Year}");
Console.WriteLine(movie.Actors);
}

Så nu har vi egentligen allt vi behöver för att söka filmer via OMDB
Så vi slår ihop allting
1. Skapa en klass kallad OMDB
2. I den stoppar du in metoderna
a. GetMovie() – för att hämta JSON filen från nätet och omvandla till film-objekt
b. SearchMovie() – för att hämta JSON filen från nätet och omvandla till sök-objekt
c. GetMovieByIMDB() för att hämta film med IMDB Id
d. GetByTitle() för att hämta första bästa som matchar titeln
e. Search() för att kunna söka
int page = 7;

Campus Mölndal .Net 21
Marcus Medina
Nu kan du söka på filmer, få information som Titel, skådespelare, rating, handling, länk till filmaffish,
längd på filmen, antal säsonger om det är en serie, författare och typ (film, serie, avsnitt) och en
massa annat.
Nu är projektet klar och fullt körbar, men för att vara snälla ska vi ändra metoderna lite…
För att vara snälla mot servern ska vi skapa två metoder till i klassen
private string ReadCache(string file)
{
var filename = $"{file}.cache.json";
if (File.Exists(filename))
{
return File.ReadAllText(filename);
}
return "";
}
private void WriteCache(string file, string json)
{
var filename = $"{file}.cache.json";
File.WriteAllText(filename, json);
}

Dessa metoder ska hjälpa dig att spara och läsa dina sökningar på hårddisken.
Nu får vi ändra våra metoder för att hämta information.
private Movie GetMovie(string url, string id)
{
string json = ReadCache(id);
if (json == "")
{
using (var wc = new System.Net.WebClient())
{
json = wc.DownloadString(url);
WriteCache(id, json);
}
}
return JsonConvert.DeserializeObject<Movie>(json);
}

int page = 8;

Campus Mölndal .Net 21
Marcus Medina
Och
private SearchResult SearchMovie(string url, string id)
{
string json = ReadCache(id);
if (json == "")
{
using (var wc = new System.Net.WebClient())
{
json = wc.DownloadString(url);
WriteCache(id, json);
}
}
return JsonConvert.DeserializeObject<SearchResult>(json);
}

Så att du kan skicka in en sträng som ska användas som ID för din cache.
Nu kan du anpassa metoderna som hämtar information till att skicka in sina inparametrar som ”id”
för cache namnet. Exempelvis:
public SearchResult Search(string title, string year = "", string type = "", string page = "1")
{
var url = $"http://www.omdbapi.com/?apikey={Settings.Key}&s={title}&y={year}&type={type}&page={page}";
return SearchMovie(url, title + year + type + page);
}

Tänk på att anpassa dina andra metoder på samma sätt.
Sådär nu borde du ha en riktigt cool class som kan hämta hem information om filmer till dig.

int page = 9;

Campus Mölndal .Net 21
Marcus Medina
Så nu kan du göra ett program som kopplar sig till en databas och sparar dina sökningar, och i din
databas kan du själv lägga in information om vad du tyckte om filmen. (Will Ferrell varning i mitt fall)
Eller exempelvis att den ska varna om det är en skådespelare du inte gillar i huvudrollen, eller om
regissören är kass.
Då klassen inte har några input eller output funktioner kan du använda den till Windows Forms,
ASP.net eller consolen.

Testa att köra programmet och passa på att pusha ditt projekt till Git!
När du är klar med projektet, pusha allting till Github!

int page = 10;

Campus Mölndal .Net 21
Marcus Medina

Sammanfattning:
Vad har vi lärt oss av detta exempel?
1. Hur man kan hämta information från nätet
2. Hur lätt det är att kommunicera med APIs från C#

Vad kan göras bättre?
1. Skapa ett Winforms projekt som visar information om filmer du söker på

int page = 11;
