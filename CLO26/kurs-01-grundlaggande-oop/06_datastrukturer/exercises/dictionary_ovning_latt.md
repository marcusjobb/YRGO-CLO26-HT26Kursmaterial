| Dictionary<Lätt,Övning> | 16 september 2021 |
| --- | --- |


Beskrivning:

I den här övningen ska vi skapa grunden för ditt CV

Kursplanstermer som berörs av uppgiften:

| Mål | För godkänt krävs |
| --- | --- |
| Kunskaper kring typer, variabler, operationer, uttryck, villkorssatser och loopar inom programmering. | Den studerande redogör för hur typer, variabler, uttryck, villkorssatser och loopar används inom programmering. |
| Kunskap kring namngivning och kodstruktur av klasser, metoder och variabler i objektorienterade program. | Den studerande redogör för hur namngivning och kodstruktur av klasser, metoder och variabler i objektorienterade program används. |
| Förstå och använda sig av datastrukturer inom programmering. | Den studerande använder sig av datastrukturer i sin mjukvaruutveckling. |


Termer för övningen:

- Dictionary, en lista med värden som index istället för siffror

- SortedDictionary, en dictionary som sorterar nyklarna


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

Fyll nu dina arbetslivserfarenheter, exempelvis

```


work.Add("2020-11", "Lärare, Codic Education, C#");
work.Add("2020-02", "Senior System Developer, Flutter, ASP.net, C#");
work.Add("2019-09", "Supporttekniker, Sykes AB, Samsung");
work.Add("2018-04", "Consultant");
work.Add("2017-12", "Lärare, Radix AB, Samhällskunskap för SFI studenter");
work.Add("2016-08", "Handledare, Beda Hallbergs gymnasium");
work.Add("2015-10", "Mentor för student på YH");
work.Add("2015-02", "Consultant, Webbmaster, fotograf och videoredigerare");
work.Add("2014-11", "Lärare, Nodebite AB, IT Högskolan");
work.Add("2013-06", "System developer, Ventac Partners, PHP");
work.Add("2012-02", "Mentor för student på Malmö Högskola");
```

Fyll sedan i dina utbildningar, inklusive den du går nu och Udemykurser om du vill

```


education.Add("2008", "C#, Yrkeshögskolan");
education.Add("1998", "C++, Valdemarsro");
education.Add("1995", "Marketing & Management, Institute of European Studies");
```

Och nu kan du slutligen skriva ut det.

```


Console.WriteLine("Min CV");
foreach (var employment in work)
{
Console.WriteLine(employment.Key + " - " + employment.Value);
}
Console.WriteLine("Utbildningar");
foreach (var student in education)
{
Console.WriteLine(student.Key + " - " + student.Value);
}
```


Testa att köra programmet och passa på att comitta ditt projekt till Git! När du är klar med projektet, pusha allting till Github!

Sammanfattning:

Vad har vi lärt oss av detta exempel?

- Hur vi kan använda Dictionaries för samla ihop vår CV

- Har du beskrivningarna som ren text är det enklare att klistra in dem i jobbsökarprofiler i framtiden.


Vad kan göras bättre?

- Listan kan sparas i ett textdokument

- Eller i ett HTML dokument i <article> sektioner.

- Eller skapa ett Word dokument som du fyller med informationen från C# (jodå man kan göra sånt)

---
Det här är grunden. Öva på den, lek med koden, gör misstag. Det är så du lär dig.
