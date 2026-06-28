---

title: Lite Information Om Typer
author: Marcus Ackre Medina
type: lecture
topic: syntax
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/Objektorienterad programmering i C# 2021/Variabler/Lite information om typer.docx"
description: "Bool kallas även System.Boolean och är inget nummer."
tags: ["csharp", "information", "lite", "syntax", "typer.docx"]
week_fit: []
---
Bool
Bool kallas även System.Boolean och är inget nummer.
Boolean är True or false typ, den kan vara antingen sant eller falskt. Dessa värden kan omvandlas till
siffror men då det skiljer på olika system så kan det bli lite rörigt. Ibland är True = 0, False = -1 och
Ibland är True = 1, False = 0. Så det rekommenderas att du kontrollerar först med det system du
arbetar mot, för att vara säker på hur de vill omvandla det booleanska värdet till decimaltal.
Bool är bra att ha när man ska utvärdera om värden matchar ett visst villkor. Faktum är att när du kör
en If-sats så blir resultatet en bool

Byte
Byte även kallad System.Byte, det är en Unsigned 8-bit integer, som har värden mellan 0 och 255.En
byte använder mann är man inte vill använda värden större än 255. Inte ofta man använder det, men
det kan behövas ibland. Just en sådan typ orsakade den omtalade buggen "Nuclear Ghandi" i
Civilization. När Ghandis agressivitet nådde -1 blev den automatiskt 255. ’

Char
Char kallas även System.Char är 16 bitar stor och handskas med värden mellan U+0000 to U+FFFF.
Char är lite speciella då de kan behandlas både som symboler och siffror. Tips! En char array fungerar
fint med Console.WriteLine.

Decimal
Decimal kallas även System.Decimal och är 16 bytes stor. Decimal kan handskas med väldigt stora tal
och med hög precision av decimaler. Lite långsammare än Double. Decimal använder man mest när
man arbetar med värden som kräver precision, matematiska beräkningar för forskning, pengar, GPS
signaler och annat.

Double
Double kallas även System.Double, är 8 bytes stor och kan handskas med stora tal dock inte med
samma precision som Decimal. Lite snabbare än Decimal, långsammare än Float. Double används
också för pengar och matematiska beräkningar som kräver precision, den är inte lika djupgående i
decimalerna som Decimal är men den är snabbare och får oftast fram bra resultat utan alltför många
decimaler.

Float
Float kallas även System.Float, är 4 bytes stor och har inte lika mycket precision som Double, dock är
den snabbare än Double. Float används när man behöver decimaler men föredrar att inte lägga ner
tid på alldeles för exakta beräkningar. Den är snabb och säker.

Int
Int kallas även System.Int32 Signed, är en 32-bit integer, som kan handskas med tal mellan
-2,147,483,648 och 2,147,483,647. Den kan dock inte handskas med decimaltal, vilket gör den väldigt
snabb. Heltalens konung, det mest använda nummer-typen skulle jag tro.

Long
Long kallas även System.Int64 och är en Signed 64-bit integer som kan handskas med tal mellan
-9,223,372,036,854,775,808 och 9,223,372,036,854,775,807. På grund av sin storlek är den lite
långsammare än int. Behöver man tal upp till 9.223372036854799561e18. så är den här typen bra att
använda.

String
String även System.String och är en klass (även om den används som en typ) för hantering och lagring
av text, den kan lagra ungefär 2GB tecken i minnet. String är en utökning av Char och fungerar som
en collection eller en array av chars, men tack o lov har den många specialmetoder som gör vi slipper
hantera Char arrayer.
