---

title: Scp Objekt (solid Övning)
author: Marcus Ackre Medina
type: exercise
topic: clean-code
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/C#/Övningar/SOLID/SCP Objekt (SOLID övning).docx"
description: "“Alla avvikande föremål, enheter och fenomen som kräver särskilda förvaringsprocedurer tilldelas en"
tags: ["(solid", "clean-code", "csharp", "exercise", "objekt", "oop", "scp", "test", "övning).docx"]
week_fit: []
---
SCP Objekt
“Alla avvikande föremål, enheter och fenomen som kräver särskilda förvaringsprocedurer tilldelas en
objektklass. En objektklass är en del av standard SCP-mallen och fungerar som en grov indikator för hur
svårt ett objekt ska innehålla. I universum är objektklasser avsedda att identifiera inneslutningsbehov,
forskningsprioritet, budgetering och andra överväganden. En SCPs objektklass bestäms av ett antal
faktorer, men de viktigaste faktorerna är svårigheten och syftet med inneslutningen.” – SCP Foundation,
http://scp-wiki.wikidot.com/
Vi ska nu använda SOLID för att kolla på specifika SCP objekt.

Interfaces.......................................................................................................................................................2
Modell............................................................................................................................................................2
XML................................................................................................................................................................3
JSON...............................................................................................................................................................4
Webbscraping!...............................................................................................................................................5
Hanterare för klasserna.................................................................................................................................6
Testkörning....................................................................................................................................................7
Sammanfattning.............................................................................................................................................8

Item #: Page-001.

Vi börjar med att skapa några Interfaces som vi kommer att använda.
Skapa först en mapp i ditt projekt, kallad ”Interfaces”

Interfaces
Skapa sedan följande interfaces:
public interface ISCPObject
{
string Url { get; } // { return "http://scp-wiki.wikidot.com/scp-" + ItemNr; }
string ItemNr { get; set; }
string Class { get; set; }
string Description { get; set; }
}
public interface IFilehandler
{
void Save(string filename, ISCPObject Data);
ISCPObject Load(string filename);
}
public interface IFetcher
{
ISCPObject ReadUrl(string url);
}

Modell
Dessa borde räcka för vad vi behöver.
Vi skapar först en mapp för modeller, och därinne skapar vi SCPObject klassen som ärver från vårt
interface.
public class SCPObject : ISCPObject
{
public string Url { get { { return "http://scp-wiki.wikidot.com/scp-" + ItemNr; } } }
public string ItemNr { get; set; }
public string Class { get; set; }
public string Description { get; set; }
}

Klassen kommer att ta emot ItemNr, Class och Description. Url fylls på automatiskt i propertyn.

Item #: Page-002.

XML
Nästa del är filhanterare, att läsa och spara filer.
En större utmaning är hur man läser och sparar filer i XML. Detta finns inbyggt i .Net men används sällan.
Nu ska vi använda det!
public class FileHandlerXML : IFilehandler
{
public ISCPObject Load(string filename)
{
if (File.Exists(filename + ".xml"))
{
XmlSerializer serializer = new XmlSerializer(typeof(SCPObject));
using TextReader reader = new StreamReader(filename+".xml");
return (ISCPObject)serializer.Deserialize(reader);
}
return null;
}
public void Save(string filename, SCPObject Data)
{
var s = new XmlSerializer(typeof(SCPObject));
using (var streamWriter = new StreamWriter(filename + ".xml"))
s.Serialize(streamWriter, Data);
}
}

XMLSerialiser objektet omvandlar dina klasser till XML kod. Den är lite gammalmodig och använder sig
av Text och Stream readers och writers. Men det fungerar!
Du kan läsa mer om detta på





https://docs.microsoft.com/en-us/dotnet/api/system.xml.serialization.xmlserializer?view=net-5.0
https://docs.microsoft.com/en-us/dotnet/api/system.io.streamreader?view=net-5.0
https://docs.microsoft.com/en-us/dotnet/api/system.io.textreader?view=net-5.0
https://docs.microsoft.com/en-us/dotnet/api/system.io.streamwriter?view=net-5.0

I övrigt så är det så att Serialisering betyder ” Serialisering är en process inom datavetenskapen som innebär att en
datastruktur eller ett objekttillstånd sparas till ett format (till exempel en datafil) som kan lagras i, eller överföras
till, ett datorminne eller en annan datamiljö.” (https://sv.wikipedia.org/wiki/Serialisering)
Enklare förklarat, man omvandlar klasser till datafiler, oftast i textform – i detta fall gjorde vi det med XML.

Item #: Page-003.

JSON
Men XML är lite klurig att editera, så vi gör om detta, fast med JSON.
För detta behöver vi lägga till följande Nuget : Install-Package Newtonsoft.Json
Sen är det bara att koda på

public class FileHandlerJSON : IFilehandler
{
public ISCPObject Load(string filename)
{
if (File.Exists(filename + ".json"))
{
var jsonString = File.ReadAllText(filename+".json");
return JsonSerializer.Deserialize<SCPObject>(jsonString);
}
return null;
}
public void Save(string filename, SCPObject Data)
{
var options = new JsonSerializerOptions { WriteIndented = true };
var jsonString = JsonSerializer.Serialize(Data, options);
File.WriteAllText(filename+".json", jsonString);
}
}

File.ReadAllText() läser in en hel textfil och returnerar det till en sträng.
JSONSerializer är lite enklare att hantera än XML serialiseraren och den funkar alldeles utmärk för det mesta.

Du kan läsa mer om Newtonsoft JSON här
 https://www.newtonsoft.com/json/help/html/Introduction.htm
 https://www.newtonsoft.com/json/help/html/SerializeObject.htm
 https://www.newtonsoft.com/json/help/html/DeserializeObject.htm

Item #: Page-004.

Webbscraping!
Nu räcker det med alternativ för att läsa och spara data. Nu kör vi lite webscraping istället.
För detta behöver vi följande nuget: Install-Package HtmlAgilityPack
public class FetcherSCP : IFetcher
{
public ISCPObject Fetch(string IdNr)
{
return ReadUrl($"http://scp-wiki.wikidot.com/scp-{IdNr.Trim()}");
}
public ISCPObject ReadUrl(string url)
{
HtmlWeb web = new HtmlWeb();
var htmlDoc = web.Load(url);
var node = htmlDoc.DocumentNode.SelectSingleNode("//div[@id='main-content']");
var nodes = htmlDoc.DocumentNode.SelectNodes("//p");
var title = htmlDoc.DocumentNode.SelectSingleNode("//title");

}

}

var cls = nodes.FirstOrDefault(n => n.InnerText.
Contains("Object Class:"))?.InnerText.Replace("Object Class: ", "").Trim();
var obj = new SCPObject();
obj.ItemNr = title.InnerText.
Replace(" - SCP Foundation", "").Replace("SCP-", "").Trim();
obj.Class = cls;
var text = HttpUtility.HtmlDecode(node.InnerText.Trim());
var pos = text.IndexOf('«');
text = text.Substring(0, pos);
obj.Description = text;
return obj;

Den här klassen ser komplicerad ut. Tack vare HTMLAgilityPack slipper vi en hel tråkigheter när det
gäller att ladda ner källkoden till webbsidor.
Först skapar vi ett HtmlWeb-objekt som vi använder för att ladda ner en webbsida.
Därefter läser vi av ”main-content” div från sidand HTML-kod.
I nästa rad samlar vi ihop alla <P> taggar och dess innehåll.
//p betyder <P> för HTMLAgilityPack
[@id=’myDiv’] söker upp en specifik attribut i själva HTML Taggen
Sedan letar vi upp första bästa som innehåller ”Object Class” och tar bort lite text så att vi får fram bara
vilken klassificering som SCPobjektet har. Vi sparar numret i vårt objekt.

Item #: Page-005.

Av titeln på sidan kan vi få fram numret på SCPObjektet. Sedan använder vi .Nets egen HTML
symbolöversättare för att rensa bort specialtecken som HTML använder. Slutligen tar vi all resterande
text i HTML koden fram till « symbolen och sparar det som beskrivning av vårt SCPObjekt.

Du kan läsa mer här



https://html-agility-pack.net/
https://docs.microsoft.com/en-us/dotnet/api/system.web.httputility.htmldecode?view=net-5.0

Hanterare för klasserna
Sista delen av vårt solida projekt är att skapa en klass som hanterar alltihopa
public class SCPHandler
{
IFilehandler FileHandler;
IFetcher Fetcher;
public SCPHandler(IFilehandler fileHandler, IFetcher fetcher)
{
FileHandler = fileHandler;
Fetcher = fetcher;
}
public ISCPObject GetSCPObject(int IdNr)
{
return GetSCPObject(IdNr.ToString());
}
public ISCPObject GetSCPObject(string IdNr)
{
var scp = FileHandler.Load(IdNr) ?? Fetcher.Fetch(IdNr);
if (scp != null) FileHandler.Save(IdNr, scp);
return scp;
}
}

En väldigt enkel klass. Den tar emot två parametrar i Constructorn, en Filehandler och en Fetcher, som
den sparar i sina privata variabler.
Metoden GetSCPObject() använder sig av instansieringen av FileHandler för att försöka läsa in den som
fil, finns inte filen så kommer den att försöka ladda ner objektet från webbsidan.
Det finns två sätt att ange vilket objekt som ska hämtas, den första är genom att skicka in en sträng och
det andra genom att skicka in en int. Bara för att det är roligt.

Item #: Page-006.

Vad betyder de två frågetecknen då? Egentligen betyder den raden samma sak som detta:
if (FileHandler.Load(IdNr) != null)
{
scp = FileHandler.Load(IdNr);
}
else
{
scp = Fetcher.Fetch(IdNr);
}

Och även samma sak som detta
scp = FileHandler.Load(IdNr) != null ? FileHandler.Load(IdNr) : Fetcher.Fetch(IdNr);

men snyggast är nog ändå
scp = FileHandler.Load(IdNr) ?? Fetcher.Fetch(IdNr);

Testkörning
Nu ska vi testa vår klass.
private static void Main()
{
var handler = new SCPHandler(new FileHandlerJSON(), new FetcherSCP());
var scp = handler.GetSCPObject(999);
Console.WriteLine(scp.ItemNr);
Console.WriteLine(scp.Class);
Console.WriteLine(scp.Url);
}

Vi skickar in parametrar för JSON hantering och den (för tillfället) enda webbhämtaren vi har. Sedan
frågar vi om SCPObjekt 999. Slutligen skriver vi ut informationen vi fått (inte beskrivningen dock för den
är alldeles för lång)
Vill du hellre använda XML är det bara att ändra i vilken klass du skickar med i Constructorn.

Item #: Page-007.

Sammanfattning
Vi har skapat klasser för




XML sparare och läsare
JSON sparare och läsare
Webbscraping från http://scp-wiki.wikidot.com

Klasserna har ingen specifik felhantering (för att det tar plats att skriva det och jag vill hålla dokumentet
enkelt).
Vi använder SOLID för att lätt kunna ersätta funktionalitet och lätt kunna bygga på med fler moduler. Ifall
vi vill ha en bättre webbscraper eller vill hämta objekt från en annan sida, det är bara att bygga nya
klasser.
Vi instansierar klasserna i main, vilket gör att ingen av klasserna är beroende av andra. Förutom Load
funktionen som förståeligt nog inte fungerar mot interfaces och måste hårdkodas till SCPObject.
Även om du inte använder detta projekt så kan det hjälpa dig att fungera som grund för andra liknande
projekt.
Lycka till!
/Marcus

Item #: Page-008.
