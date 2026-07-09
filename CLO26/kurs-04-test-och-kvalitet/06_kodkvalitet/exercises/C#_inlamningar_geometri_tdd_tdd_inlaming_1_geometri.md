TDD Inläming 1
För att riktigt komma in i TDD tänkandet ska vi göra en återblick i högstadiets och gymnasiets geometri.
Uppgiften är alltså att skapa en enkel Facadeklass som ska kunna räkna ut arean och omkretsen.






Cirklar
Trianglar
o Rättvinkliga
o Likbenta
o Liksidiga
kvadrater
rektanglar

Var och en av dessa typer ska vara en klass för sig. Självklart ska denna testas för att bekräfta att
beräkningarna stämmer. Vill du lägga till fler typer, så får du gärna göra det.
För tips, kolla in






https://www.matteboken.se/lektioner/skolar-8/geometri-och-enheter
https://www.mathsisfun.com/geometry/index.html
https://www.mathsisfun.com/geometry/perimeter.html
https://www.mathsisfun.com/geometry/area.html
http://www.learnify.se/learnifyer/ObjectResources/3618a9cf-a67e-4386-847e-a8ba47bd0bdb/6.html

Man ska även kunna summera flera objekts omkrets. Genom att skicka in en array, exempelvis
var geoThings = new GeometricThing[]
{
new Triangle(height:10, tbase:10),
new Square(width:10,height:10),
new Circle(radius:5)
};
var geoCal = new GeometricCalculator();
Console.WriteLine(geoCal.GetPerimeter(geoThings));

Där GeometricThing är en abstrakt klass eller interface som alla de geometriska typerna ärver eller
implementerar, och GeometricCalculator är din klass som ska göra beräkningarna.
Klassen GeometricCalculator ska ha följande metoder
 GetArea(GeometricThing thing) – Räknar ut arean
 GetPerimeter(GeometricThing thing) – räknar ut omkretsen av en figur
 GetPerimeter(GeometricThing[] thing) – Räknar ut sammanlagd omkrets av flera figurer
Alla värden och beräkningar ska ske med float (inte lika exakt som double, men snabbare), och
metoderna ska returnera float. Alla beräkningar kommer att vara i metriska enheter.
Det behövs inget användargränssnitt då all verifiering sker i unit-testerna.

För godkänt krävs







En fungerande klass som ska beräkna area och omkrets (även summan av flera objekts omkrets)
o Cirklar
o Rektanglar
o Kvadrater
o Trianglar (liksidiga)
Det räcker med att klasserna tar emot hela mått (ex bas och höjd), ekvationer för att räkna ut
saknade värden uppskattas men är inte ett krav.
Minst ett test på var och en av de publika metoderna i klassen
Alla tester ska fungera
XML kommenterad kod av de publika metoderna och objekten

För VG krävs





Alla G punkter ska vara godkända
Minst två fungerande test på var och en av de publika metoderna i klassen
o Positiv = Tar emot en eller flera värden och räknar ut det rätt
o Negativ = Tar emot felaktiga värden, exempelvis null och hanterar det utan krasch
Alla publika metoder i klasserna ska testas
Dokumentation av testerna som gjorts

Disclaimer
Om geometri inte är din grej, ta gärna hjälp av dina kurskamrater för att lösa beräkningarna. Det är helt
OK, men se bara till att all kod som skrivs är din egen.
Tänk på att problemlösningen inte är det viktiga i denna inlämning, det är testandet som är viktigt.
