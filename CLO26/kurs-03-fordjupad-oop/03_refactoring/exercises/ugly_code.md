# "Pelle Fulkod och den studsande bollen" - En refaktoreringsdialog

🔴


## Bakgrunden

Så... jag har något kul att visa dig idag! Träffa **Pelle Fulkod** - han jobbar på samma ställe som oss och är helt övertygad om att han är en "ninja master of C#". Han kom till mig häromdagen och sa: _"Kolla vad jag gjort! Det här ska bli nästa stora spelet!"_

Hmm... vad tror du kommer härnäst i den här berättelsen? Jo, precis - koden är... låt oss bara säga att den _fungerar_. Men någon måste ju kunna underhålla det här grejet också, eller hur?

**Vill du hjälpa mig rädda Pelles projekt?** Vi ska göra det begripligt, modulärt och faktiskt roligt att jobba med. Du kommer att få se alla de där klassiska misstag vi pratat om - och viktigast av allt - lära dig hur man reder ut dem steg för steg.

## Först - låt oss se vad vi har att jobba med

![aaghh](aaghh.png)

Okej, innan vi gör något annat - kör den här koden! Jag vill att du ser att den faktiskt fungerar. Det är viktigt att förstå vad vi börjar med.

> Skapa en vanlig .NET Console-app (.NET 8 funkar fint). Tryck `Q` för att avsluta när du testat lite.

```csharp
using System;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.CursorVisible = false;

        var r = new Random();
        int ww = Console.WindowWidth;
        int wh = Console.WindowHeight;

        int L = 2;
        int T = 1;
        if (ww < 40) ww = 40;
        if (wh < 15) wh = 15;

        for (int i = L; i < ww - 2; i++)
        {
            Console.SetCursorPosition(i, T);
            Console.Write("#");
            Console.SetCursorPosition(i, wh - 2);
            Console.Write("#");
        }
        for (int j = T; j < wh - 1; j++)
        {
            Console.SetCursorPosition(L, j);
            Console.Write("#");
            Console.SetCursorPosition(ww - 3, j);
            Console.Write("#");
        }

        int x = r.Next(L + 2, ww - 5);
        int y = r.Next(T + 2, wh - 5);
        int dx = r.Next(0, 2) == 0 ? -1 : 1;
        int dy = r.Next(0, 2) == 0 ? -1 : 1;

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.SetCursorPosition(4, 0);
        Console.Write("Pelle Fulkods Bouncy-ish Thing 3000  (Q to quit)");
        Console.ResetColor();

        int B = 0;
        int S = 0;
        int z = 0;
        int speed = 25;

        int px = ww / 2 - 4;
        int py = wh - 4;
        int pw = 9;
        Console.ForegroundColor = ConsoleColor.Yellow;
        for (int i = 0; i < pw; i++)
        {
            Console.SetCursorPosition(px + i, py);
            Console.Write("=");
        }
        Console.ResetColor();

        while (true)
        {
            if (Console.KeyAvailable)
            {
                var k = Console.ReadKey(true);
                if (k.Key == ConsoleKey.Q) break;
                if (k.Key == ConsoleKey.LeftArrow) px -= 2;
                if (k.Key == ConsoleKey.RightArrow) px += 2;
                if (k.Key == ConsoleKey.UpArrow) speed = Math.Max(5, speed - 2);
                if (k.Key == ConsoleKey.DownArrow) speed = Math.Min(120, speed + 2);
            }

            if (px < L + 1) px = L + 1;
            if (px + pw > ww - 3) px = ww - 3 - pw;

            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int i = L + 1; i < ww - 3; i++)
            {
                Console.SetCursorPosition(i, py);
                Console.Write(i >= px && i < px + pw ? "=" : " ");
            }
            Console.ResetColor();

            Console.SetCursorPosition(x, y);
            Console.Write(" ");

            x += dx;
            y += dy;

            if (x <= L + 1) { dx = 1; B++; }
            if (x >= ww - 4) { dx = -1; B++; }
            if (y <= T + 1) { dy = 1; B++; }

            if (y >= py - 1)
            {
                if (x >= px && x < px + pw)
                {
                    dy = -1;
                    B++;
                    S += 3;
                    if (r.Next(0, 3) == 0) dx = dx == 0 ? 1 : -dx;
                }
                else
                {
                    dy = -1;
                    S -= 2;
                }
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(x, y);
            Console.Write("O");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(4, wh - 1);
            Console.Write($"Bounces:{B}   Score:{S}   Speed:{speed}ms   Pos:({x},{y})     ");
            Console.ResetColor();

            z++;
            if (z % 37 == 0)
            {
                Console.SetCursorPosition(ww - 25, 0);
                Console.Write("     ");
                Console.SetCursorPosition(ww - 25, 0);
                Console.Write(r.Next(0, 2) == 0 ? "wow!" : "such bounce");
            }

            Thread.Sleep(speed);
        }

        Console.SetCursorPosition(0, wh - 1);
        Console.WriteLine("\nOk byeeee...");
        Console.CursorVisible = true;
    }
}
```

Så... kör det och berätta för mig: vad händer? Kan du gissa vad olika delar gör? Jag lovar att vi ska reda ut det här monstret tillsammans!

## Steg 1: "Vad i hela världen betyder alla dessa variabler?"

Okej, nu när du sett vad koden gör... titta på variablerna igen. Vad tror du `ww` och `wh` betyder? Och `L` och `T`?

Jag menar, `x` och `y` kan man ju gissa sig till, men `dx` och `dy`? Och vad med `B`, `S` och `z`? Det här påminner mig om när jag började programmera och trodde att korta variabelnamn gjorde mig till en bättre programmerare. Spoiler alert: det gjorde det inte!

**Vad säger du om att vi börjar här?** Låt oss döpa om alla dessa kryptiska namn till något som faktiskt berättar vad de gör.

<details>
<summary>💡 Tips om refaktor-verktyg (klicka här)</summary>

Har du provat **Rename Symbol**? Det är magiskt! I VS Code trycker du `F2`, i Visual Studio är det `Ctrl+R,R`, och i Rider `Shift+F6`. Då kan du byta namn på en variabel överallt på en gång.

Gör det i små steg: döp om `ww` → `windowWidth`, kör programmet, kolla att det fortfarande funkar. Sedan nästa variabel. Så blir du aldrig osäker på vad som gick fel.

</details>

<details>
<summary>🧭 Mina förslag (men kör på dina egna om du vill)</summary>

Vad skulle du säga om:

- `ww/wh` → `windowWidth/windowHeight`
- `L/T` → `leftMargin/topMargin` (eller `borderLeft/borderTop`?)
- `x,y` → `ballX/ballY`
- `dx,dy` → `velocityX/velocityY` (eller `deltaX/deltaY`?)
- `px,py,pw` → `paddleX/paddleY/paddleWidth`
- `B` → `bounceCount`
- `S` → `score`
- `z` → `tick` (eller `frameCount`?)

Vad tycker du? Passar de eller har du bättre idéer?

</details>

## Steg 2: "Och alla dessa magiska nummer då?"

Hmm, har du märkt alla dessa konstiga tal som dyker upp överallt? `25`, `120`, `5`, `40`, `15`, `9`...

Om jag kommer tillbaka till den här koden om sex månader - tror du jag kommer komma ihåg varför hastigheten är just 25 millisekunder? Eller varför paddeln är 9 tecken bred? Jag gissar nej!

**Vad säger du om att vi samlar alla dessa tal och ger dem vettiga namn?** Typ `SLEEP_SPEED_MS`, `MIN_WINDOW_WIDTH`, `PADDLE_WIDTH` och så vidare?

<details>
<summary>💡 Var brukar jag sätta konstanter?</summary>

Jag brukar antingen:

1. Lägga dem längst upp i filen: `const int MIN_WINDOW_WIDTH = 40;`
2. Eller skapa en liten klass: `static class GameSettings { public const int MinWindowWidth = 40; }`

Vilken variant känns bäst för dig? Varför?

</details>

<details>
<summary>🔍 Vilka tal borde bli konstanter?</summary>

Titta efter tal som:

- Definierar gränser (minsta fönsterstorlek, marginaler)
- Är hastigheter eller timing (25 ms, max/min speed)
- Är storlekar (paddel, boll)
- Är poäng eller spelregler (+3, -2 poäng)
- Är tecken ('O', '=', '#')

Kan du hitta fler än de jag nämnde?

</details>

## Steg 3: "Den här koden upprepar sig ju hela tiden!"

Oj, har du sett hur ramarna ritas? Först en loop för horisontella linjer, sedan en för vertikala. Och paddeln! Den ritas på ett sätt först, sedan på ett annat sätt i game loopen.

Det här är som att skriva samma mening två gånger i en uppsats. Det funkar, men... varför inte bara säga det en gång och säga det bra?

**Skulle vi kunna göra små metoder som gör varje sak en gång och gör det bra?** Typ `DrawBorder()`, `DrawPaddle()`, `UpdateHud()`?

<details>
<summary>🛠️ Använd IDE:ns Extract Method-funktion!</summary>

Här får du använda ett av de bästa verktygen vi har:

**Visual Studio**: Markera koden → högerklick → "Quick Actions" → "Extract Method" (eller `Ctrl+R, M`)
**VS Code**: Markera koden → `Ctrl+Shift+P` → "Extract to method" (kräver C# extension)
**Rider**: Markera koden → `Ctrl+Alt+M` eller högerklick → "Refactor" → "Extract Method"

⚠️ **Varning**: IDE:n kommer skapa massor av `ref`-parametrar! Det är helt normalt - den vet inte vilka variabler som "hör ihop". Vi kommer fixa det snyggt i nästa steg med hjälpklasser.

Så oroa dig inte om du får metoder som ser ut så här:

```csharp
static void DrawBorder(ref int windowWidth, ref int windowHeight, ref int leftMargin, ref int topMargin)
```

Det fixar vi snart! 😊

</details>

<details>
<summary>💡 Vilka små metoder skulle hjälpa?</summary>

Jag tänker:

- `DrawBorder(int left, int top, int right, int bottom)` - ritar hela ramen på en gång
- `DrawPaddle(int x, int y, int width, char symbol)` - ritar paddeln
- `ClearBall(int x, int y)` och `DrawBall(int x, int y)` - hanterar bollen
- `UpdateHud(int bounces, int score, int speed, int ballX, int ballY)` - all info längst ner

Vad tycker du? Vilken skulle hjälpa mest?

</details>

## Steg 4: "Den där main-metoden är ju en hel roman!"

Wow, titta på Main-metoden... den är typ 80 rader! Tänk om du skulle förklara för en kompis vad spelet gör - skulle du verkligen börja med "först sätter vi cursor visible till false, sedan skapar vi en random..."?

Nej väl? Du skulle väl säga: "Spelet hanterar input, flyttar bollen, kollar kollisioner och ritar allt".

**Vad säger du om att vi bryter ut stora bitar till egna metoder?** Så att Main berättar _vad_ som händer, inte _hur_ varje liten detalj fungerar?

<details>
<summary>💡 Vilka stora bitar ser du?</summary>

Jag ser ungefär:

- **Input-hantering**: piltangenter och hastighetsändringar
- **Bollens fysik**: flytta bollen och väggkollisioner
- **Paddel-kollision**: den stora if-satsen med träff/miss
- **Rendering**: allt som ritar på skärmen

Skulle vi kunna göra metoder som `HandleInput()`, `MoveBall()`, `CheckPaddleHit()` och `Render()`?

</details>

<details>
<summary>🔧 Exempel på metodsignaturer</summary>

Vad säger du om något sånt här:

```csharp
static void HandleInput(ref int paddleX, ref int gameSpeed)
static void MoveBall(ref int ballX, ref int ballY, ref int velocityX, ref int velocityY)
static bool CheckWallBounce(ref int ballX, ref int ballY, ref int velocityX, ref int velocityY, ref int bounceCount)
static bool CheckPaddleHit(int ballX, int ballY, int paddleX, int paddleWidth, ref int velocityY, ref int score)
static void RenderEverything(...)
```

Fast du vet vad? De här blir lite jobbiga med alla `ref`-parametrar. Kanske det finns ett bättre sätt...

</details>

## Steg 4.5: "Märkte du alla dessa knepiga boolean-grejer?"

Vänta lite innan vi går vidare... har du märkt några rader som ser riktigt förvirrande ut? Titta på den här till exempel:

```csharp
int dx = r.Next(0, 2) == 0 ? -1 : 1;
```

Wow, vad händer där? Jag måste ju läsa det tre gånger för att förstå att det bara betyder "välj slumpmässigt mellan -1 och 1".

Och vad sägs om den här skönheten:

```csharp
Console.Write(i >= px && i < px + pw ? "=" : " ");
```

**Skulle vi kunna göra såna här rader lättare att förstå?** Även om du förstår dem nu - kommer du förstå dem om tre månader?

<details>
<summary>💡 Gör boolean-logik tydligare</summary>

Istället för kryptisk ternary operator:

```csharp
// Före: Vad betyder det här?
int dx = r.Next(0, 2) == 0 ? -1 : 1;

// Efter: Ah, nu förstår jag!
bool movingLeft = r.Next(0, 2) == 0;
int dx = movingLeft ? -1 : 1;

// Eller ännu tydligare:
bool movingLeft = r.NextDouble() < 0.5; // 50% chans
int velocityX = movingLeft ? -1 : 1;
```

Och för paddel-ritningen:

```csharp
// Före: Måste tänka för att förstå
Console.Write(i >= px && i < px + pw ? "=" : " ");

// Efter: Säger vad den gör
bool shouldDrawPaddle = (i >= paddleX && i < paddleX + paddleWidth);
Console.Write(shouldDrawPaddle ? "=" : " ");
```

Se skillnaden? Din hjärna behöver inte "kompilera" logiken varje gång!

</details>

## Steg 5: "Alla dessa variabler flyter omkring fritt..."

Okej, nu måste vi prata om något som kanske känns abstrakt men som är superviktigt: **var alla variabler lever**.

Titta på Main-metoden - `ballX`, `paddleY`, `score`, `windowWidth`... allt finns i samma "rum". Det är som att ha alla dina kläder, kökssaker, verktyg och böcker i en enda stor låda. Tekniskt sett _funkar_ det, men...

**Vad händer när du vill lägga till en till boll? Eller en fiende? Eller bara förstå vilka variabler som hör ihop?**

Tänk om vi kunde gruppera saker som hör ihop, som `Ball`, `Paddle`, `GameState`?

<details>
<summary>🧠 Varför är "allt-i-Main" problematiskt?</summary>

När alla variabler är "globala" (dvs. tillgängliga överallt):

1. **Du kan ändra vad som helst från vart som helst** - även av misstag
2. **Det är svårt att hålla reda på vad som påverkar vad**
3. **Lägga till ny funktionalitet** blir en mardröm
4. **Testa enskilda delar** blir nästan omöjligt

Exempel: Om paddel-logiken är utspridd över hela Main, hur ska du testa att paddeln stannar vid kanten utan att också testa bollens rörelse, poängsystemet och allt annat?

</details>

## Steg 6: "Tänk om vi hade riktiga objekt att jobba med?"

Nu när vi pratat om problemet... vad sägs om lösningen? Istället för `ballX`, `ballY`, `velocityX`, `velocityY` - vad sägs om bara `Ball`?

**Det behöver inte vara komplicerat!** Bara enkla klasser som håller saker som hör ihop.

<details>
<summary>💡 Enkla hjälpklasser</summary>

Vad sägs om något sånt här:

```csharp
class Ball
{
    public int X { get; set; }
    public int Y { get; set; }
    public int VelocityX { get; set; }
    public int VelocityY { get; set; }
    public char Symbol { get; set; } = 'O';

    public void Move()
    {
        X += VelocityX;
        Y += VelocityY;
    }

    public void BounceHorizontal() => VelocityX = -VelocityX;
    public void BounceVertical() => VelocityY = -VelocityY;
}

class Paddle
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public char Symbol { get; set; } = '=';

    // Hjälpmetod för att kolla om paddeln träffas
    public bool IsHitBy(int ballX, int ballY)
    {
        return ballY >= Y && ballX >= X && ballX < X + Width;
    }
}

class GameBounds
{
    public int Left { get; set; }
    public int Top { get; set; }
    public int Right { get; set; }
    public int Bottom { get; set; }

    // Tydliga metoder istället för förvirrande matematik överallt
    public bool IsBallHittingLeftWall(int ballX) => ballX <= Left;
    public bool IsBallHittingRightWall(int ballX) => ballX >= Right;
    public bool IsBallHittingTopWall(int ballY) => ballY <= Top;
}

class GameStats
{
    public int Score { get; set; }
    public int Bounces { get; set; }
    public int SpeedMs { get; set; } = 25;

    public void AddScore(int points) => Score += points;
    public void RegisterBounce() => Bounces++;
}
```

Ser det vettigt ut? **Märk hur mycket tydligare** `ball.IsHitBy(paddleX, paddleY)` är än `ballY >= paddleY && ballX >= paddleX && ballX < paddleX + paddleWidth`!

Och istället för att komma ihåg vad `B++` betyder, kan vi skriva `gameStats.RegisterBounce()`. Koden berättar vad den gör!

</details>

<details>
<summary>🔍 Varför properties istället för bara public fields?</summary>

Du märkte säkert att alla våra klasser använder `{ get; set; }` istället för bara `public int X;`.

**Varför då?** Några viktiga anledningar:

1. **Vi kan lägga till logik senare** utan att ändra kod som använder klassen:

```csharp
public int X
{
    get => _x;
    set => _x = Math.Max(0, value); // Kan aldrig bli negativ!
}
```

2. **Det ser professionellt ut** - andra C#-programmerare förväntar sig properties

3. **Konsistens** - .NET-biblioteken använder properties överallt

4. **Framtidssäkert** - om du senare behöver validering, logging, etc.

**Bonus:** Properties kostar ingenting i prestanda - kompilatorn gör dem lika snabba som vanliga fields!

</details>

## Steg 7: "Vad med all den här mystiska matematiken?"

Har du märkt alla dessa konstiga beräkningar som dyker upp?

- `windowWidth - 3`
- `windowHeight - 2`
- `frameCount % 37 == 0` (varför just 37??)
- `paddleX + paddleWidth > windowWidth - 3`

**Vad betyder de egentligen?** Och viktigare - kommer du komma ihåg det om en månad?

<details>
<summary>🧮 Gör matematiken begriplig</summary>

Istället för mystiska beräkningar:

```csharp
// Före: Vad betyder det här?
if (paddleX + paddleWidth > windowWidth - 3)
    paddleX = windowWidth - 3 - paddleWidth;

// Efter: Ah, det håller paddeln inom spelområdet!
int rightEdge = windowWidth - borderWidth;
if (paddleX + paddleWidth > rightEdge)
{
    paddleX = rightEdge - paddleWidth;
}

// Eller ännu bättre med hjälp av vår GameBounds:
if (gameBounds.IsPaddleOutsideRight(paddleX, paddleWidth))
{
    paddleX = gameBounds.ClampPaddleRight(paddleWidth);
}
```

Och den mystiska `frameCount % 37`:

```csharp
// Före: Varför 37? Vad händer?
if (frameCount % 37 == 0) { /* visa random meddelande */ }

// Efter: Ah, ett random meddelande ibland!
const int RANDOM_MESSAGE_FREQUENCY = 37; // Ungefär en gång per sekund på normal hastighet
if (frameCount % RANDOM_MESSAGE_FREQUENCY == 0)
{
    ShowRandomEncouragementMessage();
}
```

Se hur mycket lättare det är att förstå OCH ändra!

</details>

## Steg 8: "Input-hantering är utspritt överallt!"

Titta på hur tangentbordet hanteras... det är blandat med all annan logik! Input-läsning, paddel-flytt, hastighetsändring - allt i samma while-loop.

**Vad händer om vi vill lägga till fler kontroller? Eller ändra vilka tangenter som gör vad?**

<details>
<summary>⌨️ Samla input-logik</summary>

Vad sägs om en egen Input-klass?

```csharp
class InputHandler
{
    public bool ShouldQuit { get; private set; }
    public bool MovePaddleLeft { get; private set; }
    public bool MovePaddleRight { get; private set; }
    public bool SpeedUp { get; private set; }
    public bool SlowDown { get; private set; }

    public void UpdateInput()
    {
        // Rensa föregående frame
        MovePaddleLeft = MovePaddleRight = SpeedUp = SlowDown = false;

        while (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.Q: ShouldQuit = true; break;
                case ConsoleKey.LeftArrow: MovePaddleLeft = true; break;
                case ConsoleKey.RightArrow: MovePaddleRight = true; break;
                case ConsoleKey.UpArrow: SpeedUp = true; break;
                case ConsoleKey.DownArrow: SlowDown = true; break;
            }
        }
    }
}
```

Nu kan game loopen bara säga:

```csharp
input.UpdateInput();
if (input.ShouldQuit) break;
if (input.MovePaddleLeft) paddle.MoveLeft();
if (input.MovePaddleRight) paddle.MoveRight();
```

Mycket tydligare, eller hur? Och lätt att lägga till WASD-kontroller senare!

</details>

## Steg 9: "Kan vi göra Main till en riktig huvudkaraktär?"

Nu när vi har städat upp det mesta... titta på Main igen. Känns det som den berättar en tydlig historia?

Jag skulle vilja att den säger ungefär: "Initiera spelet. Kör game loop. Stäng av snyggt." Och sen är det andra metoder som sköter detaljerna.

**Vad sägs om vi gör Main riktigt kort och tydlig?**

<details>
<summary>💡 En huvudkaraktär som berättar historien</summary>

Kanske något sånt här:

```csharp
static void Main()
{
    var game = new Game();
    game.Initialize();
    game.Run();
    game.Shutdown();
}
```

Eller till och med:

```csharp
static void Main()
{
    new Game().Run();
}
```

Och sen har Game-klassen en Run-metod som innehåller game loopen. Vad tycker du?

</details>

## Bonus-tankar för nyfikna: "Vad skulle hända om...?"

Nu när vi har gjort alla stora refaktoreringssteg - här är några saker du kanske märkt men som vi inte hunnit prata om:

<details>
<summary>🤔 Vad händer om konsolfönstret är för litet?</summary>

Koden försöker hantera det med:

```csharp
if (windowWidth < 40) windowWidth = 40;
if (windowHeight < 15) windowHeight = 15;
```

Men vad händer egentligen om konsolen bara är 20x10? Koden _säger_ att den ska vara 40x15, men konsolen kanske inte kan bli så stor!

**När du blir bekvämare med programmering** kommer du lära dig om `try-catch` för att hantera sådana problem elegantare. Men just nu räcker det att du _märker_ problemet och tänker "hmm, det där kunde gå fel".

</details>

<details>
<summary>🎮 Varför känns kontrollen lite seg?</summary>

Märkte du att du måste hålla in piltangenterna? Det beror på hur `Console.KeyAvailable` funkar - den läser bara tangenter som _tryckts ned_ sen sist.

För ett riktigt spel skulle man vilja ha "smooth movement" där paddeln rör sig så länge du håller in tangenten. Det kräver lite mer avancerad input-hantering, men bra att du märker skillnaden!

</details>

<details>
<summary>🔄 Varför ibland "hackig" animation?</summary>

`Thread.Sleep(gameSpeed)` pausar _hela programmet_. Om datorn är upptagen med annat kan pausen bli längre än planerat.

Professionella spel använder "delta time" - de mäter hur lång tid som faktiskt gått och anpassar rörelsen därefter. Men för vårt lilla spel funkar `Thread.Sleep` helt okej!

</details>

## Bonus-utmaning: "Gör det till ett riktigt spel!"

Om du har tid och lust kvar... vad sägs om att göra det här till något man faktiskt vill spela?

Några enkla idéer:

- **Game Over**: 3 missade bollar = slut
- **Svårare över tid**: bollen blir snabbare eller paddeln smalare
- **High Score**: spara bästa resultatet till en fil

Men börja smått! Ett steg i taget, precis som vi gjort med refaktoreringen.

## Är vi klara? Låt oss kolla!

- [ ] **Variabelnamn som säger sanningen** - inga mysterier kvar?
- [ ] **Konstanter istället för magiska tal** - kan man förstå varför varje siffra är som den är?
- [ ] **Ingen upprepad kod** - säger vi samma sak flera gånger någonstans?
- [ ] **Små, fokuserade metoder** - gör varje metod en tydlig sak?
- [ ] **Enkla hjälpklasser** - Ball, Paddle, osv. istället för lösa variabler?
- [ ] **Properties med { get; set; }** - vänjer oss vid professionella mönster?
- [ ] **Minimal Main** - berättar den historien på hög nivå?

## Vad har vi lärt oss?

Innan vi avslutar - jag är nyfiken på vad du tänker:

1. **Vilka tre namnbyten** märkte du störst skillnad av? Varför just de?

2. **Vilken metod** gav dig känslan av "ah, nu ser jag vad som händer!"?

3. Om du skulle bygga det här spelet **från scratch** idag - vad skulle du göra annorlunda från början?

4. **Vad skulle nästa steg** vara för att göra det här till ett riktigt kul spel?

Så där! Pelles kaotiska kod är nu något vi faktiskt kan jobba med. Bra jobbat!

Nästa gång Pelle kommer med ett "coolt projekt" så vet vi precis vilka frågor vi ska ställa: _"Vad gör den här variabeln? Varför är det här talet 37? Kan du förklara vad den här metoden gör på en mening?"_

Det är skillnaden mellan kod som _fungerar_ och kod som _funkar att jobba med_. 🎯

---
Det här är grunden. Öva på den, lek med koden, gör misstag. Det är så du lär dig.
