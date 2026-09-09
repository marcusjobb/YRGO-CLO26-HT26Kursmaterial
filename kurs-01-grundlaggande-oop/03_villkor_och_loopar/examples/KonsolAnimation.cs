// KonsolAnimation.cs — ett studsande emoji i konsolfönstret
//
// Visar hur man styr konsolens markör för att skapa enkel animation.
// Kräver ett riktigt konsolfönster — se README längst ner i filen.
//
// Koncept som används:
//   • variabler och datatyper (int)
//   • while-loop (oändlig)
//   • if-satser (vändning vid kanten)
//   • Console-klassen (CursorTop, CursorLeft, OutputEncoding)
//   • Thread.Sleep (paus i millisekunder)

using System.Text;

// Startposition
int x = 10;
int y = 10;

// Rörelseriktning: 1 = framåt, -1 = bakåt
int dx = 1;
int dy = 1;

// Krävs för att emoji ska visas korrekt i terminalen
Console.OutputEncoding = Encoding.UTF8;

// Döljer den blinkande markören (snyggare animation)
Console.CursorVisible = false;

while (true)
{
    // Radera föregående position genom att skriva ett mellanslag dit
    Console.CursorTop = y;
    Console.CursorLeft = x;
    Console.Write(" ");

    // Flytta positionen ett steg i rörelseriktningen
    x += dx;
    y += dy;

    // Vänd riktning om vi når kanten
    if (x > 75) dx = -1;
    if (x < 1)  dx = 1;
    if (y > 20) dy = -1;
    if (y < 1)  dy = 1;

    // Rita emoji på den nya positionen
    Console.CursorTop = y;
    Console.CursorLeft = x;
    Console.Write("😊");

    // Vänta 20 ms innan nästa bildruta
    Thread.Sleep(20);
}

// ─────────────────────────────────────────────────────────────
// Så här öppnar du ett riktigt konsolfönster i din IDE
// ─────────────────────────────────────────────────────────────
//
// RIDER
//   Run > Edit Configurations > din konfiguration
//   Kryssa i "Use external console"
//   Kör sedan som vanligt — ett separat fönster öppnas.
//
// VS CODE
//   Öppna (eller skapa) .vscode/launch.json
//   Hitta raden "console" och ändra värdet:
//
//     "console": "externalTerminal"
//
//   Standardvärdet "internalConsole" kör i VS Codes inbyggda panel
//   och stöder inte markörpositionering — animationen syns inte där.
// ─────────────────────────────────────────────────────────────
