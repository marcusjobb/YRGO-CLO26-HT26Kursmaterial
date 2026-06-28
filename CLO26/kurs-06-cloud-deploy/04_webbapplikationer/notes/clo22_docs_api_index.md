---

title: API - En Genväg till Webbapplikationskommunikation
author: Marcus Ackre Medina
type: article
topic: api
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/books/CLO22/Docs/api/index.md"
description: "Nu ska vi prata om API, eller Application Programming Interface, som är ett viktigt gränssnitt för att underlätta kommunikationen med webbapplikationer. API:er fungerar som en slags mellanhand mellan "
tags: ["api", "csharp", "genväg", "git", "oop", "till", "webbapplikationskommunikation"]
week_fit: []
---

# API - En Genväg till Webbapplikationskommunikation

🟢


Nu ska vi prata om API, eller Application Programming Interface, som är ett viktigt gränssnitt för att underlätta kommunikationen med webbapplikationer. API:er fungerar som en slags mellanhand mellan två applikationer, vilket gör det möjligt för oss att ställa frågor och få svar från webbapplikationer samt skicka och ta emot data. Genom API:er blir klienten och servern oberoende av varandra och kan enkelt utbyta information.

<details open markdown="block">
  <summary>
    Innehållsförteckning
  </summary>
  {: .text-delta }

1. TOC
{:toc}


## TL;DR

API:er är ett viktigt gränssnitt för att underlätta kommunikationen med webbapplikationer. De gör det möjligt för oss att ställa frågor och få svar från webbapplikationer samt skicka och ta emot data. Genom API:er blir klienten och servern oberoende av varandra och kan enkelt utbyta information.

## Fördjupning

API:er gör det möjligt för oss att använda webbapplikationer utan att behöva interagera med deras grafiska gränssnitt (GUI). Istället kan vi kommunicera med applikationen genom att ställa frågor och begära data via en URL. Svaren från API:et kommer vanligtvis i formatet XML eller JSON, vilket gör det lättare att läsa och tolka informationen.

Genom att använda API:er kan vi skapa mer flexibla och dynamiska applikationer. Vi kan anpassa datautbytet mellan klienten och servern och se till att endast de delar av servern som API:et tillåter blir synliga för klienten. Detta underlättar kommunikationen och ger oss möjlighet att skapa mer effektiva och modulära system.

## Exempel

Här är ett exempel på hur vi kan använda ett API i C# för att hämta data från The Movie Database (TMDb):

```csharp
// För att få en API-nyckel måste du registrera dig på
// https://www.themoviedb.org/documentation/api
string minaDokument = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
string APIKey = File.ReadAllText(Path.Combine(minaDokument, "APiKeys", "themoviedb.APIKey.txt")));

// The Movie Database API, fråga efter filmen "Happy Death Day"
// URL:en
string url = $"https://api.themoviedb.org/3/search/movie?api_key={APIKey}&query=Happy+death+day";

// Skapa en begäran för URL:en.
WebRequest begäran = WebRequest.Create(url);
// Om det krävs av servern, ange autentiseringsuppgifter.
begäran.Credentials = CredentialCache.DefaultCredentials;

// Hämta svaret.
HttpWebResponse svar = (HttpWebResponse)begäran.GetResponse();
// Visa statusen.
Console.WriteLine(svar.StatusDescription);

// Hämta strömmen med innehåll som returneras av servern.
Stream dataStröm = svar.GetResponseStream();
// Öppna strömmen med en StreamReader för enkel åtkomst.
StreamReader läsare = new StreamReader(dataStröm);
// Läs innehållet.
string svarFrånServern = läsare.ReadToEnd();

// Visa innehållet.
Console.WriteLine(svarFrånServern);

// Stäng av strömmarna och svaret.
läsare.Close();
dataStröm.Close();
svar.Close();
```

Koden ovan gör följande:

1. Den hämtar API-nyckeln från en fil där användaren har sparat den.
2. Den skapar en URL för att fråga The Movie Database API efter filmen "Happy Death Day".
3. Den skapar en begäran (WebRequest) för URL:en och anger eventuella autentiseringsuppgifter.
4. Den får svaret från servern som en HTTP-respons (HttpWebResponse) och visar statusbeskrivningen.
5. Den hämtar strömmen (Stream) med innehållet som servern har skickat tillbaka.
6. Den öppnar strömmen med en StreamReader för att enkelt kunna läsa innehållet.
7. Den läser innehållet från strömmen och sparar det i en sträng (string).
8. Den visar innehållet på konsolen.
9. Den stänger av strömmarna och HTTP-responsen för att frigöra resurser.

Jag hoppas att den här förklaringen hjälper till att förstå koden bättre. Om du har några frågor eller funderingar är du välkommen att kontakta mig.

## Referenser

- [The Movie Database API](https://www.themoviedb.org/documentation/api)
- [W3Schools - AJAX Introduction](https://www.w3schools.com/js/js_ajax_http.asp)

Jag hoppas att du har lärt er något nytt om API:er och hur de kan underlätta kommunikationen mellan webbapplikationer. För mer information och djupgående dokumentation rekommenderar jag er att besöka referenserna ovan. Ha en fortsatt bra dag och lycka till med era programmeringsprojekt!

## Obligatorisk Dad-joke

Varför älskar utvecklare att arbeta med APIer?

För att de ger dem en chans att "PUT" sina skämt "GET" ersättningar! 😄

---
Sådärja. Nu har du koll på det här. Nästa steg — testa själv. Det är då det fastnar.
