---

title: API
author: Marcus Ackre Medina
type: article
topic: api
difficulty: 1
language: java
status: adapted
marcus_voice: true
source: "Old_courses/books/devbook/Docs/java/api/index.md"
description: "Nu ska vi prata om API, eller Application Programming Interface, som är ett viktigt gränssnitt för att underlätta kommunikationen med webbapplikationer. API:er fungerar som en slags mellanhand mellan "
tags: ["api", "design-patterns", "git", "java", "oop"]
week_fit: []
---

# API

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

Här är ett exempel på hur vi kan använda ett API i Java för att hämta data från The Movie Database (TMDb):

```java
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;

public class TmdbApiExample {
public static void main(String[] args) {
try {
// API-nyckel från The Movie Database (TMDb)
String apiKey = "DIN_API_NYCKEL_HÄR";

// URL för att fråga efter filmen "Happy Death Day"
String url = "https://api.themoviedb.org/3/search/movie?api_key=" + apiKey + "&query=Happy+death+day";

// Skapa en URL-objekt från strängen
URL apiURL = new URL(url);

// Öppna en anslutning till URL:en
HttpURLConnection connection = (HttpURLConnection) apiURL.openConnection();

// Ange att vi vill göra en GET-begäran
connection.setRequestMethod("GET");

// Hämta svarskod från anslutningen
int responseCode = connection.getResponseCode();
System.out.println("Svarskod: " + responseCode);

// Läs svaret från anslutningen
BufferedReader reader = new BufferedReader(new InputStreamReader(connection.getInputStream()));
String inputLine;
StringBuilder response = new StringBuilder();
while ((inputLine = reader.readLine()) != null) {
response.append(inputLine);
}
reader.close();

// Visa svaret
System.out.println(response.toString());

// Stäng anslutningen
connection.disconnect();
} catch (IOException e) {
e.printStackTrace();
}
}
}
```

Koden ovan gör följande:

1. Den skapar en URL för att fråga The Movie Database API efter filmen "Happy Death Day" med hjälp av en API-nyckel.
2. Den skapar en HTTP-anslutning (HttpURLConnection) till URL:en och specifierar att vi vill göra en GET-begäran.
3. Den hämtar svarskoden från anslutningen och visar den.
4. Den läser svaret från anslutningen och sparar det i en sträng.
5. Den visar svaret på konsolen.
6. Den stänger anslutningen.

Jag hoppas att den här förklaringen hjälper till att förstå koden bättre. Om du har några frågor eller funderingar är du välkommen att kontakta mig.

## Referenser

- [The Movie Database API](https://www.themoviedb.org/documentation/api)
- [Oracle - HttpURLConnection](https://docs.oracle.com/en/java/javase/11/docs/api/java.net/java/net/HttpURLConnection.html)

Jag hoppas att du har lärt er något nytt om API:er och hur de kan underlätta kommunikationen mellan webbapplikationer. För mer information och djupgående dokumentation rekommenderar jag er att besöka referenserna ovan. Ha en fortsatt bra dag och lycka till med era programmeringsprojekt!

## Obligatorisk Dad-joke

Varför älskar utvecklare att arbeta med APIer?

För att de ger dem en chans att "PUT" sina skämt "GET" ersättningar! 😄

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
