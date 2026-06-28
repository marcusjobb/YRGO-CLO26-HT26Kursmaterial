---

title: Benny Cs (net20d) Lista Av Kortkommandon
author: Marcus Ackre Medina
type: exercise
topic: conditions
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/C#/Övningar/Visual Studio Specifikt/Benny Cs (Net20d) lista av kortkommandon.pdf"
description: "Användbara kortkommandon för att göra din programmeringsvardag effektivare och enklare."
tags: ["(net20d)", "benny", "conditions", "csharp", "exercise", "kortkommandon.pdf", "lista", "oop", "test"]
week_fit: []
---
Användbara kortkommandon
Visual Studio 2019
Användbara kortkommandon för att göra din programmeringsvardag effektivare och enklare.
Skapat av Benny Christensen, published according to GPL 3.0. E-post: benny@potatismoose.com

Innehållsförteckning
Söka......................................................................................................................................................2
Vanlig sökfunktion...........................................................................................................................2
Sök deluxe........................................................................................................................................2
Refaktorering........................................................................................................................................3
Refaktoreringsmeny.........................................................................................................................3
Kommentarer/Organisering/Indentering..............................................................................................4
Kommentera kod..............................................................................................................................4
Avkommentera kod..........................................................................................................................4
Bokmärka kodrader..........................................................................................................................4
Bokmärka kodrader..........................................................................................................................4
Indentera kod...................................................................................................................................5
Indentera kod deluxe........................................................................................................................5
Programming lifehacks.........................................................................................................................6
Döpa om...........................................................................................................................................6
Duplicera kodrad..............................................................................................................................6
Kopiera kodrad................................................................................................................................6
Flytta kodrad....................................................................................................................................7
Multi-Line typing.............................................................................................................................7
Fullskärmskodning...........................................................................................................................7
Minimera ett block med kod............................................................................................................7
Tabba genom flikar..........................................................................................................................8
Stänga tabbar....................................................................................................................................8
Omsluta kodblock (med class, loop, if m.m)...................................................................................8
Vad tar metoden för inparametrar?..................................................................................................8
Alternativa metoder.........................................................................................................................8
Hoppa till deklaration (klass eller metod t.ex).................................................................................9
Debugging..........................................................................................................................................10
Kör programmet utan debug..........................................................................................................10
Kör programmet med debug..........................................................................................................10
Skapa stoppunkt för debug............................................................................................................10
Step into.........................................................................................................................................10
Step over........................................................................................................................................11
Användbara snippets..........................................................................................................................12
Console.WriteLine();.....................................................................................................................12
Do While loop................................................................................................................................12
While loop......................................................................................................................................12
For loop..........................................................................................................................................12
Try-Catch block.............................................................................................................................13
Property..........................................................................................................................................13
Konstruktor....................................................................................................................................13

Söka

Vanlig sökfunktion
Kortkommando

Förklaring

CTRL+F

Sök i dokumentet.

Sök deluxe
Kortkommando

Förklaring

CTRL+ ,

Söka efter en Flik, en Class eller en Metod i ett
projekt. Kanske inget man har nytta av vid mindre
projekt, men vid större kan detta vara en
tidsbesparande funktion som är bra att känna till.

Refaktorering

Refaktoreringsmeny
Kortkommando

Förklaring

CTRL+.

Ställ dig på en rad eller markera ett kodstycke
du vill refaktorera och tryck CTRL+. för att få
upp menyn med alternativ på refaktorering.

Kommentarer/Organisering/Indentering

Kommentera kod
Kortkommando

Förklaring

CTRL+K+C

Markera ett helt stycke med kod och tryck detta
kortkommando för att kommentera bort de
markerade raderna.

Avkommentera kod
Kortkommando

Förklaring

CTRL+K+U

Markera ett stycke kod som blivit kommenterat
och tryck kortkommandot för att avkommentera
dessa så koden blir körbar.

Bokmärka kodrader
Kortkommando

Förklaring

CTRL+K+K)

Placera markören på den raden du vill
bokmärka för att komma tillbaka till vid ett
senare tillfälle. Tryck kortkommandot för att
bokmärka raden. Kortkommandot igen för att ta
bort bokmärket när du står på raden.
Bokmärken är ett jättebra verktyg för dig som
har många idéer om förändringar på din kod,
men inte vill tappa fokus på det du håller på
med just nu.

Bokmärka kodrader
Kortkommando

Förklaring

CTRL+K+W

Titta på dina bokmärken, ta bort och döp om
dessa.

Indentera kod
Kortkommando

Förklaring

TAB (Indentera höger)
För att indentera kod som blivit felaktigt
SHIFT+TAB (Indentera vänster) indenterad så kan man markera koden och
sedan trycka TAB för att flytta koden ett tabsteg
till höger. Vill du istället flytta den till vänster så
kombinera SHIFT+TAB

Indentera kod deluxe
Kortkommando

Förklaring

CTRL+K+D

För ett av de smidigaste sätten att indentera
kod på så kan man använda sig av detta
kortkommando så indenteras all kod korrekt i
hela dokumentet.
Eller för riktigt smidig indentering som
automatiskt sker vid sparande av dokument, se
följande länk:
Productivity power tools
Jag rekommenderar också CodeMaid som
extension för att hjälpa till med indentering,
städa upp, och hitta refaktoreringsmöjligheter i
din kod. Klicka på menyn Extensions och sedan
manage extensions i Visual Studio, sök sedan
reda på och installera CodeMaid. Du kan även
gå via länken här ovan för att installera det.

Programming lifehacks

Döpa om
Kortkommando

Förklaring

CTRL+R+R

Döper om ett element på alla ställen i
dokumentet. Har du en t.ex. en variabel som
heter tal1 och du vill döpa om denna till tal5 så
markerar du alltså namnet på variabeln, trycker
kortkommandot och sedan skriver i det nya
namnet på variabeln i dokumentet. Alla
metoder/variabler/övriga klassmedlemmar i ditt
projekt med samma namn kommer då att
ändras. Du kan även välja att ändra referenser
till denna variabel i kommentarer genom att
välja ett av flera tillval när du gör ändringen.
Fungerar på allt du har namngett.

Duplicera kodrad
Kortkommando

Förklaring

CTRL+E+V

Ställ markören vart som helst på kodraden och
tryck kortkommandot för att duplicera kodraden
och placera den under originalraden. Denna
dupliceras EN gång och kan inte klistras in igen
utan att köra hela kortkommandot igen.

Kopiera kodrad
Kortkommando

Förklaring

CTRL+C+V

Ställ markören vart som helst på kodraden och
tryck CTRL+C+V för att kopiera och klistra in en
kopia av kodraden. Koden kan sedan klistras in
fler gånger på andra ställen med CTRL+V

Flytta kodrad
Kortkommando

Förklaring

ALT+Pil upp eller pil ner

Markera en kodrad och tryck kortkommandot
för att flytta kodraden uppåt eller nedåt i det
sekventiella flödet (koden).

Multi-Line typing
Kortkommando

Förklaring

CTRL+ALT+Vänsterklick

Markera ett ställe där du vill skriva något, tryck
kortkommandot och vänsterklicka på ett annat
ställe där du också vill skriva exakt samma text.
På så sätt får du markören på fler ställen
samtidigt och kan skriva samma kod på flera
ställen samtidigt.

Fullskärmskodning
Kortkommando

Förklaring

SHIFT+ALT+ENTER

Ett tips: Om ni vill arbeta i fullskärm med er
kod, tryck SHIFT+ALT+ENTER.
För att återgå till normalläget, tryck samma
kortkommando igen. På det sättet får ni
ingenting på skärmen som distraherar er.

Minimera ett block med kod
Kortkommando

Förklaring

CTRL+M+M

Med detta kortkommando kan du collapsa
(minimera) ett kodblock så den underliggande
koden inte syns. Ställ dig på kodraden där
blocket börjar, tryck kortkommandot. Tryck
samma kortkommando igen för att expandera
kodblocket.

Tabba genom flikar
Kortkommando

Förklaring

CTRL+TAB
CTRL+SHIFT+TAB

Dessa kortkommandon fungerar för att tabba
framåt eller bakåt i tabbarna med projekten i
Visual Studio. Tips: Fungerar även i Google
Chrome.

Stänga tabbar
Kortkommando

Förklaring

CTRL+F4

För att stänga tabbarna i Visual studio.

Omsluta kodblock (med class, loop, if m.m)
Kortkommando

Förklaring

CTRL+K+S

Markera ett block med kod som du vill omsluta
med exempelvis en loop eller if-sats

Vad tar metoden för inparametrar?
Kortkommando

Förklaring

SHIFT+CTRL+SPACE
Ställ dig i metodhuvudet och tryck
kortkommandot för att få upp en ruta med vad
metoden tar för inparametrar.

Alternativa metoder
Kortkommando

Förklaring

CTRL+J

Om du klickar på en metods namn när du kallar
på den och trycker "ctrl + j" så får du upp en
lista på andra metoder du kan byta mot.

Hoppa till deklaration (klass eller metod t.ex)
Kortkommando

Förklaring

F12

Enormt användbart kortkommando. Markera ett
metodnamn där du kallar på en metod, eller
motsvarande för en klass. Tryck F12 så
kommer du direkt till den klassen eller
metodens deklaration. Testa på WriteLine så får
du se vad som händer. Då kommer du till
metoden WriteLine och ser koden bakom
kommandot Console.WriteLine()

Debugging

Kör programmet utan debug
Kortkommando

Förklaring

CTRL+F5

Kör programmet UTAN debugging

Kör programmet med debug
Kortkommando

Förklaring

F5

Kör programmet med debug

Skapa stoppunkt för debug
Kortkommando

Förklaring

F9

Skapar en brytpunkt där programmet ska
stanna vid debugging. Markera raden där du vill
att koden ska stanna och tryck F9. På det sättet
kan du enkelt inspektera värden i variabler och
hur koden exekveras vid ett visst tillfälle. F9
igen för att ta bort brytpunkten. Kommer stanna
här nästa gång du kör programmet med F5

Step into
Kortkommando

Förklaring

F11

Step into. Om du satt en stopppunkt (se ovan)
och koden stannat vid denna punkt, så kan du
välja att gå IN i t.ex. en metod eller loop och se
varje steg och varv som körs. Detta gör du med
F11 när du står på raden med metodanropet.

Step over
Kortkommando

Förklaring

F10

Step over. Med F10 körs metoden eller loopen,
men du går aldrig in i koden och ser vad som
händer för varje varv eller steg som utförs, utan
debuggern hoppar till nästa sekventiella steg.

Användbara snippets
Snippets är till för att göra din programmeringsvardag enklare. Skriv keywordet under
rubriken i vänsterspalten följt av två TAB tryckningar för att skapa kod på ett smidigt sätt.
Testa gärna om du kan hitta fler snippets som du tycker är användbara under tools/code
snippets manager. Välj C# i dropdownlistan.

Console.WriteLine();
Keyword

Förklaring

cw+TAB+TAB

Skapar en Console.WriteLine();

Do While loop
Keyword

Förklaring

do+TAB+TAB

Skapar en Do While loop

While loop
Keyword

Förklaring

while+TAB+TAB

Skapar en While loop

For loop
Keyword

Förklaring

for+TAB+TAB

Skapar en For loop

Try-Catch block
Keyword

Förklaring

try+TAB+TAB

Skapar ett Try Catch block

Property
Keyword

Förklaring

Prop+TAB+TAB
propfull+TAB+TAB

Skapar automatiskt en property där du direkt
kan
skriva in datatypen och sedan tabba till namnet
och ange det. Du behöver inte skriva
måsvingarna eller get; set;

Konstruktor
Keyword

Förklaring

ctor

Ctor, som är förkortningen för constructor, är ett
extremt smart sätt att skapa en konstruktor på.
Du slipper skriva namnet på konstruktorn
manuellt och minskar risken för att det blir fel.
Skrivs in och skapas direkt under klassen.

Jag skulle rekommendera att ni tittar på Tim Coreys video om hur man skapar egna snippets i
C#. Mycket smidigt om man har en återkommande kodrad/stycke som man skriver ofta och vill
ha ett kortkommando för detta.
Videon hittar du på youtube

Skapat av Benny Christensen, Published according to GPL 3.0.
E-post: benny@potatismoose.com
LinkedIn: LinkedIn profile
