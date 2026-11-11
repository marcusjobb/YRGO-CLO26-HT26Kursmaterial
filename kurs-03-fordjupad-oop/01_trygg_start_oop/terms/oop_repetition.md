# 00 Repetition av OOP: termer

## Klass
Ritningen för en sorts sak. `Hero` beskriver vad en hjälte har (`Name`, `Health`) och kan göra (`Greet()`). Av en klass blir det inget synligt förrän vi skapar ett objekt.

## Objekt
Ett färdigbyggt exemplar av en klass, skapat med `new`. Mira och Tobbe är två objekt av klassen `Hero`. Varje objekt har egna värden.

## Fält och property
Det ett objekt håller reda på, till exempel `Name` och `Health`.

## Metod
Något objektet kan göra, till exempel `Greet()`. En metod är en funktion som hör till en klass.

## Konstruktor
Metoden som körs i samma ögonblick som `new` bygger objektet. Den sätter startvärden, så att ingen hjälte föds utan namn.

## `new`
Nyckelordet som bygger ett objekt av en klass.

## Arv
En klass kan ärva fält och metoder från en annan. `Hero : Character` betyder att en `Hero` är en sorts `Character`. Det gemensamma skrivs på ett ställe.

## Basklass och barnklass
Klassen man ärver från är basklassen (`Character`). Klassen som ärver är barnklassen (`Hero`, `Monster`).

## `virtual` och `override`
`virtual` på basklassens metod betyder att barnen får ändra den. `override` i barnklassen betyder att den ändrar den. Samma anrop kan då ge olika svar beroende på objekt.

## Polymorfism
Att behandla olika objekt som samma basklass, och ändå få rätt beteende för varje. En `List<Character>` kan innehålla hjältar och monster, och `Speak()` ger rätt svar för varje.

## `abstract class`
En basklass som aldrig får bli ett objekt. Den kan kräva att barnen skriver vissa metoder (`abstract string Attack()`).

## `interface`
Ett löfte om vad en klass kan göra, utan att säga hur. `IHealer` lovar att det finns en `Heal`-metod. En klass kan ha många interface men bara en basklass.

## `is a` och `can do`
Tumregel: arv och `abstract` är "är en sorts" (Hero är en Character). `interface` är "kan göra" (Wizard kan läka).

## `OfType<T>()`
Plockar ut det som är av en viss typ ur en lista. Fungerar på en klass, en basklass och ett interface. Används för att filtrera typer.
