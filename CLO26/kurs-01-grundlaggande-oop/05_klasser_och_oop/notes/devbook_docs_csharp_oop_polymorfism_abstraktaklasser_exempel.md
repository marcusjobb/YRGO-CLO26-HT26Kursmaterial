---

title: Exempel
author: Marcus Ackre Medina
type: article
topic: oop
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/books/devbook/Docs/csharp/oop/polymorfism/abstraktaklasser/exempel.md"
description: "I den här artikeln ska vi titta på hur man skapar en abstrakt klass med abstrakta och virtuella metoder i C#."
tags: ["csharp", "exempel", "git", "oop"]
week_fit: []
---

# Exempel

🟢


I den här artikeln ska vi titta på hur man skapar en abstrakt klass med abstrakta och virtuella metoder i C#.

<details open markdown="block">
  <summary>
    Innehållsförteckning
  </summary>
  {: .text-delta }

1. TOC
{:toc}

</details>

## Beskrivning

En abstrakt klass kan innehålla vanliga metoder, abstrakta och virtuella metoder. Vi kommer att använda den här mallen för att skapa en abstrakt klass som hanterar webbskrapning för olika sidor. Vi använder även följande NuGet-paket:

- SixLabors.ImageSharp för att hantera grafik eftersom .NET-biblioteket inte är kompatibelt med Mac.
- HtmlAgilityPack för att hantera HTML-dokument.

## Fördelar

När du använder abstrakta klasser kan du dra nytta av följande fördelar:

1. **Återanvändbarhet:** Abstrakta klasser kan fungera som grund för andra klasser och möjliggöra återanvändning av kod.
2. **Moduläritet:** Genom att använda abstrakta metoder kan du separera implementationen av en metod från själva klassen, vilket leder till en modulär design.
3. **Flexibilitet:** Abstrakta klasser kan användas som bas för olika implementationer och ge flexibilitet i utvecklingen.

## Begränsningar

Det finns några begränsningar att vara medveten om när du använder abstrakta klasser:

1. **Enkel arv:** En klass kan bara ärva från en enda abstrakt klass, vilket kan begränsa möjligheterna till hierarkiska strukturer.
2. **Ingen direkt instansiering:** Eftersom en abstrakt klass inte kan instansieras direkt måste den ärvas och implementeras i en konkret klass för att användas.

## Användningsområden

Abstrakta klasser är användbara i olika scenarier, inklusive:

1. **Framtida utbyggbarhet:** Genom att definiera abstrakta metoder kan du planera för framtida utökning och implementation av specifika beteenden.
2. **Grundläggande mallar:** Abstrakta klasser kan fungera som grund för att skapa mallar eller ramverk med fördefinierad funktionalitet.
3. **Plugin-arkitektur:** Genom att använda abstrakta klasser kan du skapa en flexibel arkitektur för att lägga till och hantera plugins i din applikation.

## Kodexempel

Här är en implementation av en abstrakt klass för webbskrapning i C#:

```csharp
public abstract class WebScraper
{
    // Adressen till sidan som ska webscrapas
    public string Url { get; private set; } = "";

    // Titeln på sidan
    public string Title { get; private set; } = "";

    // Sidans egna beskrivning
    public string Description { get; private set; } = "";

    // Meta keywords från sidan
    public string Tags { get; private set; } = "";

    // HTML-dokumentet i råform
    public HtmlDocument? HtmlDocument { get; private set;

 } = null;

    // HTML-dokumentet i en sträng
    public string Html { get => HtmlDocument?.DocumentNode?.OuterHtml ?? ""; }

    // All text utan HTML-taggar
    public string Text { get => HtmlDocument?.DocumentNode?.InnerText ?? ""; }

    // Öppna en webbsida
    public virtual string GetHtmlDocument(string url)
    {
        Url = url;
        var web = new HtmlWeb();
        HtmlDocument = GetUrlAsync().Result;
        Title = HtmlDocument.DocumentNode.SelectSingleNode("//title").InnerText;
        Description = HtmlDocument?.DocumentNode?.SelectSingleNode("//meta[@name='description']")?.Attributes["content"]?.Value ?? "";
        Tags = HtmlDocument?.DocumentNode?.SelectSingleNode("//meta[@name='keywords']")?.Attributes["content"]?.Value ?? "";
        return Html;
    }

    private async Task<HtmlDocument> GetUrlAsync()
    {
        var myClient = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true });
        var response = await myClient.GetAsync(Url, HttpCompletionOption.ResponseContentRead);
        response.EnsureSuccessStatusCode();
        var streamResponse = await response.Content.ReadAsStreamAsync();
        var doc = new HtmlDocument();
        doc.Load(streamResponse);
        return doc;
    }

    // Hämtar innehållet i en specifik div
    public virtual string GetDivById(string id)
    {
        var div = HtmlDocument?.DocumentNode.SelectSingleNode($"//div[@id='{id}']");
        return div?.OuterHtml ?? "";
    }

    // Hämtar innehållet i alla div med en specifik klass
    public virtual List<string> GetDivByClass(string id)
    {
        var divs = HtmlDocument?.DocumentNode.SelectNodes($"//div[@class='{id}']");
        return divs?.Select(x => x.OuterHtml).ToList() ?? new List<string>();
    }

    // Hämtar innehållet i ett element med en specifik klass
    public virtual List<string> GetElementByClass(string element, string className)
    {
        var divs = HtmlDocument?.DocumentNode.SelectNodes($"//{element}[@class='{className}'");
        return divs?.Select(x => x.OuterHtml).ToList() ?? new List<string>();
    }

    // Hämtar url till alla bilder IMG SRC-länkar
    public virtual List<string> GetImages()
    {
        var imageUrls = new List<string>();
        if (HtmlDocument != null)
        {
            var images = HtmlDocument.DocumentNode.SelectNodes("//img");
            if (images != null)
            {
                foreach (var image in images)
                {
                    var imageUrl = image?.Attributes["src"]?.Value;
                    if (imageUrl != null)
                    {
                        if (imageUrl.EndsWith(".jpg") || imageUrl.EndsWith(".png"))
                        {
                            if (!imageUrl.StartsWith("http:") && !imageUrl.StartsWith("https:"))
                                imageUrl = Url.TrimEnd('/') + imageUrl;
                            imageUrls.Add(imageUrl);
                        }
                    }
                }
            }
        }
        return imageUrls;
    }

    // Laddar ner bild från en URL: http://pictures.net/mypic.jpg returnerar en Image-objekt
    public virtual Image GetImage(string imageUrl)
    {
        var web = new HtmlWeb();
        var client = new HttpClient();
        var imageBytes = client.GetByteArrayAsync(imageUrl).Result;
        var imageStream = new MemoryStream(imageBytes);
        return Image.Load(imageStream);
    }

    // Sparar en bildobjekt
    public virtual void SaveImage(string filename, Image image)
    {
        image.Save(filename);
    }


 // Scrapear en specifik sida
    public abstract void Scrape(string url);
}
```

Nu ska vi göra en implementation av denna klass för att hämta innehållet från en specifik sida.

```csharp
public class GetKittens : WebScraper
{
    public string DownloadKitten()
    {
        Scrape("https://www.pinterest.com/katesaidy/cute-kitten-pics/");
        var imgFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        var filename = Path.Combine(imgFolder, "Daily kitten.jpg");
        if (File.Exists(filename))
            return filename;
        else
            return "";
    }

    public override List<string> GetImages()
    {
        var imageUrls = new List<string>();

        // IMG SRC fungerade inte på Pinterest så vi söker med regex istället
        // För att göra det mer genomskinnligt så överridar vi bara GetImages
        if (HtmlDocument != null)
        {
            var regex = new Regex(@"(['""])([^'""]+\.(jpg|png|bmp|gif))\1");
            var match = regex.Match(Html);
            while (match.Success)
            {
                Console.WriteLine(match.Groups[2].Value);
                imageUrls.Add(match.Groups[2].Value);
                match = match.NextMatch();
            }
        }
        return imageUrls;
    }

    // Här är scrape-metoden som vi måste implementera
    public override void Scrape(string url)
    {
        Console.WriteLine("Downloading page");
        GetHtmlDocument(url);
        Console.WriteLine("Getting image list");
        var images = GetImages();
        Console.WriteLine("Removing crap images");
        images = images.Where(pic => !pic.Contains("/images/user/")).ToList();
        if (images.Count == 0) return;

        // Hämtar en slumpmässig bild från listan
        var random = new Random();
        Console.WriteLine("Selecting random image");
        var randomImage = images[random.Next(0, images.Count)];
        Console.WriteLine("Downloading image");
        Console.WriteLine(randomImage);
        var image = GetImage(randomImage);
        Console.WriteLine("Saving image");
        var imgFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        var filename = Path.Combine(imgFolder, "Daily kitten.jpg");
        SaveImage(filename, image);
    }
}
```

Jag hoppas att du gillar exemplet och att det är till hjälp för dig. Kattbilder är alltid en hit! Ha det roligt med webbscraping och experimentera med olika sidor att hämta innehåll från. Lycka till! 😺

## Termer

Här är en lista över några termer som används i koden:

- `WebScraper`: En abstrakt klass som hanterar webbscraping och definierar olika metoder för att hämta och bearbeta webbsidor.
- `Url`: En egenskap som representerar adressen till den webbsida som ska webscrapas.
- `Title`: En egenskap som representerar titeln på webbsidan.
- `Description`: En egenskap som representerar sidans egna beskrivning.
- `Tags`: En egenskap som representerar meta-nyckelord från webbsidan.
- `HtmlDocument`: En egenskap som representerar det HTML-dokument som hämtas från webbsidan.
- `Html`: En egenskap som representerar HTML-dokumentet som en sträng.
- `Text`: En egenskap som representerar all text på webbsidan utan HTML-taggar.
- `GetHtmlDocument`: En virtuell metod som öppnar en webbsida och hämtar dess HTML-dokument.
- `GetDivById`: En virtuell metod som hämtar innehållet i en div med en specifik id från webbsidan.
- `GetDivByClass`: En virtuell metod som hämtar innehållet i alla divar med en specifik klass från webbsidan.
- `GetElementByClass`: En virtuell metod som hämtar innehållet i alla element med en specifik klass från webbsidan.
- `GetImages`: En virtuell metod som hämtar URL:er till alla bilder (IMG SRC-länkar) på webbsidan.
- `GetImage`: En virtuell metod som laddar ner en bild från en URL och returnerar en Image-objekt.
- `SaveImage`: En virtuell metod som sparar en bild till en specifik fil.
- `Scrape`: En abstrakt metod som definierar hur webbsidan ska webscrapas.
- `GetKittens`: En klass som ärver från `WebScraper` och implementerar `Scrape`-metoden för att hämta kattbilder från en specifik sida.
- `Abstrakt klass`: En klass som inte kan instansieras direkt utan måste ärvas och implementeras i en konkret klass.
- `Virtuell metod`: En metod som kan överskridas i en subklass.
- `Nuget-paket`: Ett paket som kan installeras i Visual Studio för att lägga till funktionalitet i ett projekt.
- `Grafik`: Bilder, ikoner och andra visuella element som används på en webbsida.
- `HTML-dokument`: En textfil som innehåller HTML-kod för att skapa en webbsida.
- `Överskrida`: Att skriva om en metod i en subklass.
- `Utöka`: Att lägga till funktionalitet i en subklass.
- `Klassimplementation`: En klass som ärver från en abstrakt klass och implementerar dess abstrakta metoder.
- `Återanvändbar kod`: Kod som kan återanvändas i olika delar av ett projekt.
- `Underhållbar kod`: Kod som är lätt att förstå och underhålla.
- `Abstrakt metod`: En metod som inte har någon implementation och måste implementeras i en subklass.
- `Regex`: Ett uttryck för att matcha mönster i textsträngar.
- `Regex.Match`: En metod som matchar ett uttryck mot en textsträng.
- `Regex.NextMatch`: En metod som matchar nästa uttryck mot en textsträng.
- `Regex.Success`: En egenskap som representerar om ett uttryck matchar en textsträng.
- `Regex.Groups`: En egenskap som representerar grupper av matchade uttryck.
- `Regex.Value`: En egenskap som representerar värdet av ett matchat uttryck.

## Slutsats

I den här artikeln har vi utforskat användningen av abstrakta klasser och metoder i C#. Vi har sett hur man kan skapa en abstrakt klass med abstrakta och virtuella metoder för att hantera webbscraping av olika sidor. Vi har också diskuterat användningen av olika nuget-paket, såsom SixLabors.ImageSharp och HtmlAgilityPack, för att underlätta hanteringen av grafik och HTML-dokument.

En abstrakt klass ger oss en bra grundstruktur för att implementera specifik funktionalitet och samtidigt möjliggöra flexibilitet genom att tillåta subklasser att överskrida och utöka funktionaliteten genom att implementera abstrakta metoder. Detta hjälper oss att skapa återanvändbar och underhållbar kod.

Vi har också sett en konkret implementation av den abstrakta klassen för att hämta kattbilder från en specifik sida. Genom att använda olika metoder som `GetImages` och `GetImage`, kan vi hämta och bearbeta bilder från webbsidan.

Det är viktigt att nämna att det finns många andra sätt att använda abstrakta klasser och metoder i C#. Det är en kraftfull teknik som kan tillämpas i olika scenarier och projekt.

Jag hoppas att denna artikel har varit till hjälp för att förstå abstrakta klasser och metoder samt deras användning i C#. Fortsätt utforska och experimentera med kod, och ha kul med programmering!

## That's all folks!

Jag hoppas att du har haft nöje av att läsa artikeln och att den har varit till hjälp för dig.

Om du har några frågor är du välkommen att ställa dem!

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
