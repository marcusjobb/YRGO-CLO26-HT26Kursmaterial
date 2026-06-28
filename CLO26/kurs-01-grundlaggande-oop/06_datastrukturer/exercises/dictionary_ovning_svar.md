---

title: Dictionary övning svår
author: Marcus Ackre Medina
type: exercise
topic: datastrukturer
difficulty: 2
language: csharp
status: adapted
marcus_voice: true
source: "reference/exercises_to_spread_out/Dictionary övning svår.md"
description: "| Dictionary<Svår,Övning> | 16 september 2021 |"
tags: ["csharp", "cv", "datastrukturer", "dictionary", "exercise", "filhantering", "konsol", "sorteddictionary", "stringbuilder", "system-io"]
week_fit: []
---

| Dictionary<Svår,Övning> | 16 september 2021 |
| --- | --- |


Beskrivning:

I den här övningen ska vi skapa grunden för ditt CV och sparar det i en textfil

Kursplanstermer som berörs av uppgiften:

| Mål | För godkänt krävs |
| --- | --- |
| Kunskaper kring typer, variabler, operationer, uttryck, villkorssatser och loopar inom programmering. | Den studerande redogör för hur typer, variabler, uttryck, villkorssatser och loopar används inom programmering. |
| Kunskap kring namngivning och kodstruktur av klasser, metoder och variabler i objektorienterade program. | Den studerande redogör för hur namngivning och kodstruktur av klasser, metoder och variabler i objektorienterade program används. |
| Förstå och använda sig av datastrukturer inom programmering. | Den studerande använder sig av datastrukturer i sin mjukvaruutveckling. |


Termer för övningen:

- Dictionary, en lista med värden som index istället för siffror

- SortedDictionary, en dictionary som sorterar nyklarna

- System.IO – namespace med funktioner för läsande och skrivande av data till disk

- ReadAllText(filnamn) – Läser in en textfil till en sträng

- ReadAllLines(filnamn) – Läser in en string array

- WriteAllText(filnamn, string) – skriver en string till en fil

- WriteAllLines(filnamn, string[]) – skriver en array till en fil

- System.Text – namespace med funktioner för text hanterdande

- StringBuilder – en snabb stränghanterare som har mer funktionalitet än string

- GetFolderPath – en metod för att hämta sökvägar i användarens profil

- SpecialFolders – enum som används för att välja sökväg från GetFolderPath


Psuedokod:

- Skapa två Dictionaries utanför din main metod men inne i klassen, en för arbeten och en för utbildningar.

- Mata in dina tidigare jobb

- Mata in dina tidigare utbildningar

- Skriv ut listorna


Projektinstruktioner:

Detta projekt går utmärkt att göra tillsammans med andra, antingen i basgruppen eller i par. Välj en klasskamrat du vill arbeta med och använd LiveShare eller Discordens Screen Share för att samarbeta.

Kodning:

Vi börjar med att skapa ett c#, .net konsolprojekt.

Skapa nu ett Github repo för ditt projekt (om du inte sparar i ett redan existerande repo)

Vi börjar med att skapa två dictionaries

```


class Program
{
static Dictionary<string, string> work = new();
static Dictionary<string, string> education = new();
static void Main()
{
```

}
}

Fyll nu dina arbetslivserfarenheter, här är några exempel från mitt CV

```


work.Add("2020-11", "Utbildare, Codic Education, C#");
work.Add("2020-02", "Senior System Developer, Flutter, ASP.net, C#");
work.Add("2019-09", "Supporttekniker, Sykes AB, Samsung");
work.Add("2018-04", "Consultant Penta Parts, Hedin Bil");
work.Add("2017-12", "Utbildare, Radix AB, Samhällskunskap för SFI studenter");
work.Add("2016-08", "Handledare, Beda Hallbergs gymnasium");
work.Add("2015-10", "Mentor för student på YH");
work.Add("2015-02", "Consultant, Webbmaster, fotograf och videoredigerare");
work.Add("2014-11", "Lärare, Nodebite AB, IT Högskolan");
work.Add("2013-06", "System developer, Ventac Partners, PHP");
work.Add("2012-02", "Mentor för student på Malmö Högskola");
work.Add("2010-10", "Systemutvecklare, Scan Coin, C++ ");
work.Add("2009-10", "Handledare för C# studenter");
```

Fyll sedan i dina utbildningar, inklusive den du går nu och Udemykurser om du vill

```


education.Add("2008", "C#, Yrkeshögskolan");
education.Add("1998", "C++, Valdemarsro");
education.Add("1995", "Marketing & Management, Institute of European Studies");
```

Och nu ska vi generera ett dokument. För att göra detta skapar vi nu en instans av StringBuilder som ska göra det enklare för oss att skapa en sträng. Den är lite snabbare än string också så den rekommenderas att användas i loopar.

```
StringBuilder sb = new StringBuilder();
```


Det är allt som behövs. Om den blir rödmarkerad är det för att du saknar using System.Text;

Nu kan vi skriva till en sträng istället för consolen.


```
StringBuilder sb = new StringBuilder();
```


AppendLine() är strängens version av Console.WriteLine, den skriver in raden och lägger till radslut. Självklart finns metoden Append() som är som Console.Write() och den skriver in raden men fortsätter utan radslut.

```
sb.AppendLine("Min CV");
```

```


foreach (var employment in work)
{
sb.AppendLine(employment.Key + " - " + employment.Value);
}
sb.AppendLine();
sb.AppendLine("Utbildningar");
foreach (var student in education)
{
sb.AppendLine(student.Key + " - " + student.Value);
}
```


Nu ska vi spara filen som ett textdokument också. För att göra det snyggt behöver vi sökvägen till användarens dokumentmapp. Den får vi genom environment klassen som ger oss sökvägen till specialmapparna. I GetFolderPath kan du hitta sökvägen till Skrivbordet, Dokument, Musik, Bilder och annat som tillhör den inloggade användaren. GetFolderPath använder sig av SpecialFolders som är en enum, för att välja vilken mapp som ska returneras.

```
string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
```


Sen använder vi oss av Path klassen som hjälper oss att slå ihop adressen till "Mina dokument" med filnamnet "MinCV.txt". Att vi använder den metoden är för att vara säkra på att sökvägen blir rätt skriven.

```
string filename = Path.Combine(documents, "MinCV.txt");
```


Nu kan vi spara den med File klassen. Den kan spara arrays av text (WriteAllLines) eller hela strängen på en gång. Självklart kan vi också läsa in en hel text med antingen ReadAllLines och då få en array med en rad text i varje array rad, eller så tar vi ReadAllText och får allt i en string. Nu ska vi bara skriva hela texten på en gång.

```
File.WriteAllText(filename, sb.ToString());
```


Vi skriver ut allt på skärmen också


```
Console.WriteLine(sb.ToString());
```


Testa att köra programmet och kolla sen din "Mina dokuments mapp" passa på att comitta ditt projekt till Git! När du är klar med projektet, pusha allting till Github!

Sammanfattning:

Vad har vi lärt oss av detta exempel?

- Hur vi kan använda Dictionaries för samla ihop vår CV

- Har du beskrivningarna som ren text är det enklare att klistra in dem i jobbsökarprofiler i framtiden.


Vad kan göras bättre?

- Sortera listan med SortedDictionary.

- Eller i ett HTML dokument i <article> sektioner.

- Dela upp koden i mindre metoder

---
Och kom ihåg: allt vi gått igenom här är grunden. Resten bygger på det. Så var inte rädd att experimentera.
