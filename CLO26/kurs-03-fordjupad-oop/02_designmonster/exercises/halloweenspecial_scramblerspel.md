| Scrambler – blandade bokstäver |  |
| --- | --- |


# Beskrivning:

🔴


Vi ska nu skriva ett litet spel som väljer slumpmässigt ett ord och sedan blandar den bokstäverna. Därefter ska man ange vilka positioner som ska byta plats med varandra.

Exempelvis om ordet är bzieom och man vill byta plats på bokstäverna z och b ska man ange 1,2 så att bokstäverna på position 1 och 2 byter plats. Då blir resultatet zbieom. Så fortsätter man tills alla bokstäverna är på plats.

# Kursplanstermer som berörs av uppgiften:

| Mål | Vad du ska lära dig |
| --- | --- |
| Kunskap om innehållet i .NET-biblioteket | Innehållet i .NET-biblioteket. |
| Kunskaper kring typer, variabler, operationer, uttryck, villkorssatser och loopar inom programmering. | Typer, variabler, uttryck, villkorssatser och loopar används inom programmering. |
| Kunskap kring namngivning och kodstruktur av klasser, metoder och variabler i objektorienterade program. | Namngivning och kodstruktur av klasser, metoder och variabler i objektorienterade program används. |


# Termer för övningen:

- D


# Projektinstruktioner:

Spelet är precis som alla andra spel, en while loop som får snurra tills man har rätt. Klassen med spelet är uppdelad flera småmetoder för att lättare förklara koden och för att göra koden snyggare.

# Kodning:

Vi börjar med att skapa ett c#, .net konsolprojekt. Det är OK att göra detta i Windows Forms, tänk bara på att ersätta dina Console anrop med textboxar och labels (eller listor).

Skapa nu ett Github repo för ditt projekt (om du inte sparar i ett redan existerande repo)


## Halloween scrambler

I den här övningen ska vi skriva ett litet spel med Halloweentema. Självklart får du anpassa den hur du vill, exemplet ger dig bara grunden sedan är det fritt för dig att experimentera och ändra. Vill du göra det i Windows forms så måste du ersätta all input och output med Windows Forms objekt. Labels och textboxar främst.
Spelet går ut på att lägga bokstäverna i ordning. Den kommer att visa att ordet med alla bokstäverna i oordning och du ska välja vilka bokstäver som ska byta plats. Genom att ange två positioner.

| Exempel |
| --- |
| +-----+-----+-----+-----+-----+-----+ |  1  |  2  |  3  |  4  |  5  |  6  | +-----+-----+-----+-----+-----+-----+ |     |     |     |     |     |     | |  b  |  z  |  i  |  e  |  o  |  m  | |     |     |     |     |     |     | +-----+-----+-----+-----+-----+-----+ |

Börja med att skapa klassen.

|  |
| --- |
| public class HalloweenScrambler { } |

Vi behöver en lista för att hantera orden vi ska slumpa fram.

| public class HalloweenScrambler {     private readonly List<string> words; } |
| --- |

Vi skapar en statisk privat randomizer för att slippa att nya skapas om vi kör flera rundor av spelet.

| public class HalloweenScrambler {     private static Random Random = new Random(); } |
| --- |

Vi behöver även en variabel för att hantera ordet man ska gissa på.

|  |
| --- |
| public class HalloweenScrambler {     private string currentWord = ""; } |

Vi behöver även en variabel där bokstäverna ska röras om.

|  |
| --- |
| public class HalloweenScrambler {     private string scrambledWord = ""; } |


### Halloween scrambler

| Tips |
| --- |
| // Fyll på med så många ord du vill words = new() {     "spöke", "pumpa", "zombie",     "monster", "alien", "rymdvarelse",     "mummie", "dödskalle", "grav",     "köpcentra", "vampyr", "spindel",     "fladdermus", "godis", "bus eller godis"     }; |

I vår constructor fyller vi på med ord, vi skulle även kunna läsa av en textfil genom att använda System.IO.File.ReadAllLines();


| Constructor |
| --- |
| public class HalloweenScrambler {     public HalloweenScrambler()     {         // Skriv din kod här     } } |


### Swap metod

Vi gör en swap metod som byter platsen för bokstäverna, den arbetar mot scrambledWord variabeln.

| Swap |
| --- |
| public class HalloweenScrambler {     private void Swap(int num1, int num2)     {         var letters = scrambledWord.ToCharArray();         // Gör en swap funktion här som byter plats på         // två positioner enligt num1, och num 2 i arrayen         scrambledWord = new string (letters);     } } |


### Scramble word

Nu gör vi en metod som loopar igenom alla bokstäver i scrambledWord variabeln och slumpar deras positioner.

| ScrambleWord |
| --- |
| public class HalloweenScrambler {     private void ScrambleWord()     {         // Skriv din kod här och använd Swap metoden     } } |


### Centre

För att få snygga celler med bokstäverna så ska metoden se till att texten som skickas in till metoden blir minst 5 tecken lång och att det läggs till mellanslag på båda sidorna om texten.

| Centre |
| --- |
| internal static class Program {     static void Main()     {         var test=new  HalloweenScrambler();         var demo = HalloweenScrambler();         Console.WriteLine("|" + demo.Centre("a") + "|");     } }  public class HalloweenScrambler {     private string Centre(string v)     {         // Skriv din kod här     } } |


| Förväntad output: |  a  | |
| --- |


### Show scrambled word

| Tips |
| --- |
| // Exempel: // +-----+-----+-----+-----+-----+-----+ // |  1  |  2  |  3  |  4  |  5  |  6  | // +-----+-----+-----+-----+-----+-----+ // |     |     |     |     |     |     | // |  x  |  x  |  x  |  x  |  x  |  x  | // |     |     |     |     |     |     | // +-----+-----+-----+-----+-----+-----+ |

I den hör metoden ska du komma på ett snyggt sätt att visa upp bokstäverna och positionerna på. Tänk på att visa positionerna från 1 och upp, även om arrayen börjar på 0. Du får korrigera för det sedan vi input, krångligt, men det är mycket trevligare för spelaren att börja från 1 och upp än från 0.

Din version behöver inte se ut såhär, detta är ett exempel bara.

| ShowScrambledWord |
| --- |
| public class HalloweenScrambler {     private void ShowScrambledWord()     {         // Skriv din kod här     } } |


### Select word

I den här metoden ska du välja ett slumpvald ord från listan och spara ordet i currentWord variablen, och i scrambledWord variabeln.

| SelectWord |
| --- |
| public class HalloweenScrambler {     private void SelectWord()     {         // Skriv din kod här     } } |

### Is ok

Här ska du göra en metod som kollar att värdet som angetts är större än 0 och mindre än längden på det valda ordet (scrambledWord).

| IsOK |
| --- |
| public class HalloweenScrambler {     private bool IsOK(int num)     {         // Skriv din kod här     } } |


### Show input

| Tips |
| --- |
| // I mitt exempel matar man in kommaseparerade värden exempelvis 4,2. // det är ett enkelt trick, man frågar om ett kommaseparerat värde och // splittar sedan strängen. Därefter omvandlar man båda talen till // var sitt nummeriska värde. Det ser klart coolare ut än vad det är :) var input = Console.ReadLine(); var split = input.Split(','); _ = int.TryParse(split[0], out num1); _ = int.TryParse(split[1], out num2); |


Den här metoden ska fråga om vilka bostäver man vill byta plats på, använd dig av Swap och isOK metoderna vi skapade innan.

| ShowInput |
| --- |
| public class HalloweenScrambler {     private void ShowInput()     {         // Skriv din kod här     } } |


| Förväntad output:  +-----+-----+-----+-----+-----+-----+-----+-----+-----+ |  1  |  2  |  3  |  4  |  5  |  6  |  7  |  8  |  9  | +-----+-----+-----+-----+-----+-----+-----+-----+-----+ |     |     |     |     |     |     |     |     |     | |  ö  |  s  |  l  |  l  |  d  |  k  |  a  |  e  |  d  | |     |     |     |     |     |     |     |     |     | +-----+-----+-----+-----+-----+-----+-----+-----+-----+  Ange bokstäverna som ska byta plats, exempel 4,2 0,19 Ange bokstäverna som ska byta plats, exempel 4,2 0,99 Ange bokstäverna som ska byta plats, exempel 4,2 1,10 Ange bokstäverna som ska byta plats, exempel 4,2 3,7 |
| --- |


### Run

Nu när vi har alla metoder igång ska vi skapa en metod som kör spelet. Först ska den kalla på metoden som väljer ett slumpvalt ord och metoden som slumpar runt bokstäverna. Därefter ska du göra en While loop som kör så länge som scrambledWord != currentWord. Loopen ska visa bokstäverna i oordning och uppdatera bokstäverna för varje runda som man gissat.

| Run |
| --- |
| internal static class Program {     static void Main()     {         var test=new  HalloweenScrambler();         var game = new HalloweenScrambler();         game.Run();     } }  public class HalloweenScrambler {     internal void Run()     {         // Skriv din kod här     } } |


| Förväntad output: +-----+-----+-----+-----+-----+ |  1  |  2  |  3  |  4  |  5  | +-----+-----+-----+-----+-----+ |  p  |  k  |  s  |  e  |  ö  | +-----+-----+-----+-----+-----+  Ange bokstäverna som ska byta plats, exempel 4,2 3,1 +-----+-----+-----+-----+-----+ |  1  |  2  |  3  |  4  |  5  | +-----+-----+-----+-----+-----+ |  s  |  k  |  p  |  e  |  ö  | +-----+-----+-----+-----+-----+  Ange bokstäverna som ska byta plats, exempel 4,2 3,2 +-----+-----+-----+-----+-----+ |  1  |  2  |  3  |  4  |  5  | +-----+-----+-----+-----+-----+ |  s  |  p  |  k  |  e  |  ö  | +-----+-----+-----+-----+-----+  Ange bokstäverna som ska byta plats, exempel 4,2 5,3 +-----+-----+-----+-----+-----+ |  1  |  2  |  3  |  4  |  5  | +-----+-----+-----+-----+-----+ |  s  |  p  |  ö  |  e  |  k  | +-----+-----+-----+-----+-----+  Ange bokstäverna som ska byta plats, exempel 4,2 4,5 Du klarade det på 4 försök Ordet var spöke |
| --- |


# Facit: Halloween scrambler

Vi behöver en lista för att hantera orden vi ska slumpa fram.

| private readonly List<string> words; private static Random Random = new Random(); private string currentWord = ""; private string scrambledWord = ""; |
| --- |


### Halloween scrambler

I vår constructor fyller vi på med ord, vi skulle även kunna läsa av en textfil genom att använda System.IO.File.ReadAllLines(); men det får vi ta en annan gång.

| Constructor |
| --- |
| public HalloweenScrambler() {     words = new()     {         "spöke", "pumpa","zombie", "monster", "alien", "rymdvarelse",         "mummie", "dödskalle", "grav", "köpcentra", "vampyr",         "spindel", "fladdermus", "godis", "bus eller godis"     }; } |


### Swap

Vi gör en swap metod som byter platsen för bokstäverna, den arbetar mot scrambledWord variabeln.

| Swap |
| --- |
| private void Swap(int num1, int num2) {     var letters = scrambledWord.ToCharArray();     var tmp = letters[num1];     letters[num1] = letters[num2];     letters[num2] = tmp;     scrambledWord = new string(letters); } |


### Scramble word

Nu gör vi en metod som loopar igenom alla bokstäver i scrambledWord variabeln och slumpar deras positioner.

| ScrambleWord |
| --- |
| private void ScrambleWord() {     for (int scramble = 0; scramble < currentWord.Length; scramble++)     {         var pos = Random.Next(scrambledWord.Length);         Swap(scramble, pos);     } } |


### Centre

För att få snygga celler med bokstäverna så ska metoden se till att texten som skickas in till metoden blir minst 5 tecken lång och att det läggs till mellanslag på båda sidorna om texten.

| Centre |
| --- |
| private string Centre(string v) {     int cellWidth = 5;     while (v.Length < cellWidth)     {         v = " " + v + " ";     }     return v; } |


### Show scrambled word

I den hör metoden ska du komma på ett snyggt sätt att visa upp bokstäverna och positionerna på. Tänk på att visa positionerna från 1 och upp, även om arrayen börjar på 0. Du får korrigera för det sedan vi input, krångligt, men det är mycket trevligare för spelaren att börja från 1 och upp än från 0.

| ShowScrambledWord |
| --- |
| private void ShowScrambledWord() {     var top = "|";     var middle = "+";     var bottom = "|";     var separator = "|";     for (var i = 0; i < scrambledWord.Length; i++)     {         top += Centre((i + 1).ToString()) + "|";         middle += "-----+";         separator += "     |";         bottom += Centre(scrambledWord[i].ToString()) + "|";     }     Console.WriteLine(middle);     Console.WriteLine(top);     Console.WriteLine(middle);     Console.WriteLine(separator);     Console.WriteLine(bottom);     Console.WriteLine(separator);     Console.WriteLine(middle); } |


### Select word

I den här metoden ska du välja ett slumpvald ord från listan och spara ordet i currentWord variablen, och i scrambledWord variabeln.

| SelectWord |
| --- |
| private void SelectWord() {     currentWord = words[Random.Next(words.Count)];     scrambledWord = currentWord; } |


### Is ok

Här ska du göra en metod som kollar att värdet som angetts är större än 0 och mindre än längden på det valda ordet (scrambledWord).

| IsOK |
| --- |
| private bool IsOK(int num) {     return (num >= 1) && (num <= scrambledWord.Length); } |


### Show input

Den här metoden ska fråga om vilka bostäver man vill byta plats på, använd dig av Swap och isOK metoderna vi skapade innan.

| ShowInput |
| --- |
| private void ShowInput() {     var num1 = 0;     var num2 = 0;     bool check = false;     do     {         Console.WriteLine("Ange bokstäverna som ska byta plats, exempel 4,2");         var input = Console.ReadLine();         var split = input.Split(',');         _ = int.TryParse(split[0], out num1);         _ = int.TryParse(split[1], out num2);         check = IsOK(num1) && IsOK(num2);         } while (!check);         num1--;         num2--;         Swap(num1, num2);     } } |


### Run

Nu när vi har alla metoder igång ska vi skapa en metod som kör spelet. Först ska den kalla på metoden som väljer ett slumpvalt ord och metoden som slumpar runt bokstäverna. Därefter ska du göra en While loop som kör så länge som scrambledWord != currentWord. Loopen ska visa bokstäverna i oordning och uppdatera bokstäverna för varje runda som man gissat.

| Run |
| --- |
| internal void Run() {     SelectWord();     ScrambleWord();     int counter = 0;     while (scrambledWord != currentWord)     {         Console.Clear();         ShowScrambledWord();         Console.WriteLine();         counter++;         ShowInput();     }     Console.Clear();     Console.WriteLine("Du klarade det på " + counter + " försök");     Console.WriteLine("Ordet var " + scrambledWord); } |

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
