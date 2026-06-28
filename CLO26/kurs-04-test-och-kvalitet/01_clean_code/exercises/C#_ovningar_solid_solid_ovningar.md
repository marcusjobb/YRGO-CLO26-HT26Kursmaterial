---

title: Solid Övningar
author: Marcus Ackre Medina
type: exercise
topic: clean-code
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/C#/Övningar/SOLID/Solid övningar.docx"
description: "Här ska vi kolla på lite kod med SOLID mönster"
tags: ["clean-code", "csharp", "exercise", "git", "oop", "solid", "test", "övningar.docx"]
week_fit: []
---
Solid övningar
Här ska vi kolla på lite kod med SOLID mönster

Innehåll
(S)RP Single Responsibility principle..............................................................................................................2
FIlhanterare................................................................................................................................................3
NumberHandler.........................................................................................................................................4
(O)pen Closed Principle..................................................................................................................................5
Rektanglar..................................................................................................................................................5
Cirklar.........................................................................................................................................................5
Abstraktion.................................................................................................................................................6
(L)SP The Liskov Substitution Principle..........................................................................................................7
Meow!........................................................................................................................................................7
Hundan också!...........................................................................................................................................7
Arv är inte alltid lätt...................................................................................................................................8
Öppnar upp för arv....................................................................................................................................8
Abstraktion.................................................................................................................................................9
(I)SP The Interface Segregation Principle....................................................................................................10
Attack!......................................................................................................................................................10
Abstraktion (interfaces)...........................................................................................................................10
Attack igen!..............................................................................................................................................11
(D)IP The Dependency Inversion Principle..................................................................................................12
Instansiering av karaktärer......................................................................................................................12
Abstraktion (interface).............................................................................................................................12
TLDR;........................................................................................................................................................14

(S)RP Single Responsibility principle
En klass ska ha en, och bara en, anledning att ändras, vilket innebär att en klass bara ska ha ett jobb.
En klass, eller en metod för den delen ska aldrig utföra något OCH något mer.
Så vi börjar med att skapa en klass.
class Program
{
static void Main(string[] args)
{
// Läs en fil om den finns, annars skapa den
const string filename = "MyText.txt";
var data = "";
if (File.Exists(filename))
{
data = File.ReadAllText(filename);
}
else
{
data = "Hello world " + DateTime.Now.ToString("yyyy-MM-dd mm:ss");
File.WriteAllText(filename, data);
}
// Plocka ut alla tal från texten
var nums = new String(data.Where(Char.IsDigit).ToArray());
// Omvandla det till en Unsigned long för att vara säker att den kan
// hantera enorma tal
ulong.TryParse(nums, out var value);
// Omvandlar num string till en char Array och adderar värderna i den
var sum = nums.Sum(n=>int.Parse(n.ToString()));

}

}

Console.WriteLine(data);
Console.WriteLine(nums);
Console.WriteLine(value);
Console.WriteLine(sum);

Den här klassen kan göra en hel drös med saker, läsa en fil, skapa en fil, plocka ut alla nummer från en
text, summera alla talen, omvandla numren till en long och skriva ut allt. Det är lätt hänt att man gör så,
att man skapar allt i samma klass, eller ännu värre, i samma metod. Den koden är en mardröm att ändra
i eller hitta fel i. Där kommer SOLID in i bilden, genom att dela upp koden i klasser och metoder får vi
hjälp med att splittra upp ansvaret och kan lättare hantera fel om de skulle uppstå.
Hur kan vi snygga upp detta så att det blir mer SOLID aktigt?

FIlhanterare
Om vi börjar med första delen, läser av en fil. Filhantering kan vi lägga i en filhanteringsklass.
public class FileHandler
{
public string Filename { get; set; } = "MyFile.txt";

}
Den ska ha metoder som hanterar att man läser och sparar filer. Vi börjar med med en constructor.
public FileHandler(string filename)
{
Filename = filename;
}

Nästa steg är att skapa en metod för att läsa och skapa filen om det behövs.
public string ReadFile()
{
var data = GetFileData();
if (data == "")
data = CreateFile();
return data;
}

Enkel och trevlig kod, den försöker läsa in filen, mer lättläst än originalet. Självklart måste vi nu skapa
GetFileData() och CreateFile().
GetFileData kommer att läsa in filen (om den finns), annars kommer den att returnera en tom sträng.
private string GetFileData()
{
if (File.Exists(Filename))
{
return File.ReadAllText(Filename);
}
return "";
}

CreateFile kommer att skapa en fil med en standardtext, spara det i en fil och returnera innehåller i
texten.
private string CreateFile()
{
var data = "Hello world " + DateTime.Now.ToString("yyyy-MM-dd mm:ss");
File.WriteAllText(Filename, data);
return data;
}

Nu har vi en klass som hanterar läsandet och skapandet av en fil.

NumberHandler
Nästa del är att skapa en klass för nummerhantering
public class NumberHandler
{
}

Denna ska ha metoder för att Plocka ut alla siffror ur en text, omvandla det till ett nummeriskt värde och
att summera alla talen.
Vi börjar enkelt med en property och en constructor
public string Numbers { get; set; }
public NumberHandler(string numbers)
{
Numbers = numbers;
}

Vi behöver nu en metod som plockar ut alla siffror ur en string.
public string ExtractNumbers()
{
return new String(Numbers.Where(Char.IsDigit).ToArray());
}

Sedan ska vi kunna omvandla det till en ulong
public ulong GetNummericValue()
{
ulong.TryParse(ExtractNumbers(), out var num);
return num;
}

Och slutligen behöver vi en metod som summerar alla siffror i strängen
public int SumAllDigits()
{
return ExtractNumbers().Sum(n => int.Parse(n.ToString()));
}

Hur skulle main() se ut nu?
static void Main(string[] args)
{
var fh = new FileHandler("MyText.txt");
var num = new NumberHandler(fh.ReadFile());
Console.WriteLine(num.Numbers);
Console.WriteLine(num.ExtractNumbers());

}

Console.WriteLine(num.GetNummericValue());
Console.WriteLine(num.SumAllDigits());

Nu har vi delat upp ansvaret mellan klasser, och mellan metoder. På så sätt är det mycket lättare att
hantera koden när vi behöver ändra i det.

(O)pen Closed Principle
Principen säger att en modul ska vara öppen för förlängning, men stängd för modifiering. Det betyder att
du bör kunna utöka en modul med nya funktioner inte genom att ändra källkoden utan genom att lägga
till ny kod istället.
Vi kollar på exemplet som fanns med på föreläsningen.

Rektanglar
public class Rectangle
{
public double Width { get; set; }
public double Height { get; set; }
}

Och en klass för att räkna ut information om rektanglarna
public class AreaCalculator
{
public double Area(Rectangle[] shapes)
{
double area = 0;
foreach (var shape in shapes)
{
area += shape.Width * shape.Height;
}
return area;
}
}

Cirklar
Vi lägger nu till Cirklar
public class Circle
{
public double Radius { get; set; }
}

Då får vi ändra i AreCalculator för att kunna ta med cirklar och genast blir koden fulare.
public class AreaCalculator
{
public double Area(object[] shapes)
{
double area = 0;
foreach (var shape in shapes)
{
if (shape is Rectangle)

{
Rectangle rectangle = (Rectangle)shape;
area += rectangle.Width * rectangle.Height;
}
else
{
Circle circle = (Circle)shape;
area += circle.Radius * circle.Radius * Math.PI;
}
}
return area;
}
}

Detta måste lösas på ett snyggare sätt… O:et i SOLID har lösningen

Abstraktion
Vi skapar en söt liten abstract klass
public abstract class Shape
{
public abstract double Area();
}

Och vi anpassar våra objekt till detta
public class Rectangle : Shape
{
public double Width { get; set; }
public double Height { get; set; }
public override double Area()
{
return Width * Height;
}
}

Och
public class Circle : Shape
{
public double Radius { get; set; }
public override double Area()
{
return Radius * Radius * Math.PI;
}
}

Och slutligen ändrar vi i AreaCalculator
public class AreaCalculator
{
public double Area(Shape[] shapes)
{
double area = 0;
foreach (var shape in shapes)
{

area += shape.Area();
}
return area;
}
}

Nu använder vi objekten själva till att hantera beräkningarna, på så sätt kan vi anpassa beräkningarna
när vi behöver dem och vi kan med lätthet lägga till nya objekt som ska räknas på. Värt att observera att
innan vi skapade Shape klassen så fick vi skicka in Object[] för att kunna hantera cirklar och rektanglar.
Det slipper vi nu.
Så SOLID tänkande har gett oss ett enklare sätt att hantera beräkningarna och att ha dem där de hör
hemma, i sin egen klass.
Koden och exemplet är lånat från http://joelabrahamsson.com/a-simple-example-of-the-openclosed-principle/

(L)SP The Liskov Substitution Principle
Principen säger att en instans av en barnklass måste ersätta en instans av föräldraklassen utan att
påverka de resultat vi skulle få från en instans av basklassen själv.

Meow!
Vi skapar en katt-klass
public class Cat
{
private string Sound = "Meeeoooowwwww";
public string Name { get; set; }
public string MakeSound() => $"{Name} is a nice {GetType().Name} who says {Sound}";
}

Och en klass för att leka med djuret
public class PlayWithPets
{
public void Play()
{
Cat c = new Cat() { Name = "Misse" };
Console.WriteLine(c.MakeSound());
}
}

Hundan också!
Nu ska vi leka med en hund också. Hunden får ärva från katten.
public class Dog : Cat
{
private string Sound = "Wooff";
}

Vi anpassar vår kod nu, då Hund ärver från katt kan vi spara båda i en lista med katt definition. Perfekt
ju!
public void Play()
{
var pets = new List<Cat>()
{
new Cat { Name = "Misse" },
new Dog{Name="Voffsing"},
};
foreach (var pet in pets)
{
Console.WriteLine(pet.MakeSound());
}
}

Arv är inte alltid lätt...
Vi testar programmet….
Det blev inte så bra

Öppnar upp för arv
Vi får ändra våra djur, så att man kan ärva dem
public class Cat
{
private string Sound = "Meeeoooowwwww";
public virtual string Name { get; set; }
public virtual string MakeSound() => $"{Name} is a nice {GetType().Name} who says {Sound}";
}

Om vi testar nu får vi följande resultat

Nästan rätt. Problemet som är kvar är att vi har en privat variabel och vovven som ärver får inte tillgång
till det. Vi gör om det till protected istället.
Vi anpassar hunden till detta
public class Dog : Cat
{
public Dog()
{
Sound = "Wooff";
}
}

Och katten till detta
public class Cat
{
protected string Sound = "Meeeoooowwwww";
public virtual string Name { get; set; }
public virtual string MakeSound() => $"{Name} is a nice {GetType().Name} who says {Sound}";
}p

Abstraktion
Sista steget nu är att göra en abstract klass som vi kallar Pets för att slippa ha en massa katter som inte
är katter.
public abstract class Animal
{
protected string Sound = "grrrr";
public virtual string Name { get; set; }
public virtual string MakeSound() => $"{Name} is a nice {GetType().Name} who says {Sound}";
}

Och ändrar katten till
public class Cat : Animal
{
public Cat() => Sound = "Meeeooww";
}

Och hunden till
public class Dog : Animal
{
public Dog() => Sound = "Wooff";
}

Och vi får följande resultat

Nu kan vi fortsätta att skapa djur, och alla kommer att fungera lika fint.
public class PlayWithPets
{
public void Play()
{
var pets = new List<Animal>()
{
new Cat { Name = "Misse" },
new Dog { Name = "Voffsing"},
};
foreach (var pet in pets)
{
Console.WriteLine(pet.MakeSound());
}
}
}

Som du märker här så delar vi upp koden i många klasser och metoder, det gör att det blir fler filer men
koden blir lättare att arbeta med, då vi vet att varje metod gör bara ett jobb och varje klass gör bara ett
jobb.
Genom att använda virtual (får overridas) och protected (private för alla utom de som ärver) kan vi göra
vår klass mer öppen för att andra ska kunna anpassa den utan att ändra i den.

(I)SP The Interface Segregation Principle
Vi har skapat ett spel och vi använder oss av olika Interfaces för att hantera hur våra karaktärer slåss.
Vi har Warrios som använder rå styrka, Archers som skjuter pilar men i nödfall kan slåss med svärd,
Mage som använder spells men slåss även med sin stav, och slutligen Elf som slåss använder Spells, kan
skjuta pilar och slåss med svärd.

Attack!
Vi skapar ett interface för att låta krigarna slåss
public interface IAttackAble
{
int BluntAttack();
int ShootArrow();
int CastSpell();
}

Det blir grymt, om alla klasserna implementerar det här interfacet kommer vi att kunna ha alla i en lista
och bara göra attacker genom en for loop.
public class Warrior : IAttackAble
{
public int BluntAttack() => /* Massor med kod */;
public int CastSpell() => throw new NotImplementedException();
public int ShootArrow() => throw new NotImplementedException();
}

Nu har vi problemet att vår Warrior har en massa onödiga attackformer, den ende som kommer att
fungera bra med detta interface är en Elf… Gör om gör rätt.

Abstraktion (interfaces)
Vi skapar tre interfaces
public interface IBluntable
{
int BluntAttack();
}
public interface IArcheryable
{
int ShootArrow();
}
public interface IMagicable
{
int CastSpell();
}

Och kan nu implementera det i våra karaktärer
public class Warrior : IBluntable

{
public int BluntAttack() {/* Massor med kod */ }
}

public class Archer : IArcheryable
{
public int ShootArrow() {/* Massor med kod */ }
}
public class Mage : IMagicable
{
public int CastSpell() {/* Massor med kod */ }
}
public class Elf : IBluntable, IArcheryable,IMagicable
{
public int BluntAttack() {/* Massor med kod */ }
public int ShootArrow() {/* Massor med kod */ }
public int CastSpell() {/* Massor med kod */ }
}

Attack igen!
Och vi kan förbereda för attackerna i spelet på detta sätt
public class Game
{
public void BluntAttack(List<IBluntable> warriors)
{
foreach (var warrior in warriors)
{
warrior.BluntAttack();
}
}
public void ArrowAttack(List<IArcheryable> archers)
{
foreach (var archer in archers)
{
archer.ShootArrow();
}
}
public void MagicAttack(List<IMagicable> mages)
{
foreach (var mage in mages)
{
mage.CastSpell();
}
}
}

Genom att splittra upp de gemensamma egenskaperna i klasser, kan vi skapa metoder som är specifika
för vissa typer av klasser, och använda sedan dessa metoder som inparametrar eller i listor, trots att
klasserna i sig kanske inte har något gemensamt alls – förutom en specifik funktion.

(D)IP The Dependency Inversion Principle
I exemplet med spelet så har vi en sak till vi borde hantera. Och det är skapandet av kataktärer.

Instansiering av karaktärer
När vi startar spelet kan vi göra såhär
public class Game
{
private Warrior conan = new Warrior();
private Archer legolas = new Archer();
private Mage merlin = new Mage();
private Elf willFerrell= new Elf();

}
Då har vi skapat standardklasserna som ska ingå i spelet, men det är lite klumpigt då vi faktiskt gjort
koden väldigt dynamiskt. Vad som är värre är att vi skapar karaktärerna direkt och det gör att ändringar i
spelet kan bli komplicerade… gör om gör rätt…

Abstraktion (interface)
Vi skapar ett interface som definierar hur våra spelare ska vara i grunden
public interface IPlayableCharacterable
{
public string Name { get; set; }
public int XP { get; set; }
public int HP { get; set; }
public int XPNeeded { get; set; }
public int Level { get; set; }
}

Och vi anpassar våra karaktärer igen
public class Warrior : IBluntable, IPlayableCharacterable
public class Archer : IArcheryable, IPlayableCharacterable
public class Mage : IMagicable, IPlayableCharacterable
public class Elf : IBluntable, IArcheryable, IMagicable, IPlayableCharacterable
public class Game
{
List<IPlayableCharacterable> Players = new List<IPlayableCharacterable>();
public void StartGame()
{
Players = new List<IPlayableCharacterable>
{
new Warrior(),
new Archer(),
new Mage(),
new Elf(),
};

}

}

Nu är vår spel-klass inte längre beroende av vilken sorts karaktär vi skapar, vi kan lägga till fler och vi kan
ta bort några. Det påverkar inte huvudprogrammet i sig. Detta gör att vårt spel blir skyddad från
framtida buggar som kan uppstå när man börjar uppgradera lite överallt. Nu kan man uppgradera en
karaktär i taget utan att sabba de andra, eller spelet i sig.
Solid är bra!

TLDR;
Det är inte alltid lätt att förstå varför eller hur man ska dela upp klasser och metoder, så här kommer
några punkter som kan hjälpa dig på vägen















Varje klass har bara ett jobb
o Ett område den arbetar med
o Det gör att vi har bara en orsak att rota i den
Varje metod har bara ett jobb
o En uppgift
o Anropar andra metoder om det andra jobb behövs göra
Viktiga klasser ska vara öppna för arv, så att man inte lockas att ändra i huvudklassen
Använd arv för att ändra i funktionalitet, då förblir programmet stabilt
Små interfaces är bra
o Orsakar inte meningslösa eller tomma metoder
o Hjälper till med filtrering av objekt
Huvudklassen ska inte instansiera klasser enligt sort, den ska intansiera via Interfaces
o Lättare att lägga till och ta bort funktioner och klasser utan att huvudprogrammet
ändras
SOLID gör ditt program stabilare (fel är lättare att hitta)
SOLID gör ditt program lättare att uppgradera
SOLID gör det lättare att lägga till / ta bort moduler
SOLID är bra!
