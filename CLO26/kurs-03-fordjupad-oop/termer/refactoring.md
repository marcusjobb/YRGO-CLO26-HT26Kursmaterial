# 03 Refactoring — Programmeringstermer

## Refactoring
Att förbättra befintlig kodstruktur utan att ändra dess externa beteende. Gör koden lättare att förstå och underhålla.

## Code Smell
Ytlig indikation på djupare problem i koden. Exempel: lång metod, stor klass, duplicerad kod.

## Extract Method
Refactoring-teknik där en kodsekvens flyttas till en egen metod med ett beskrivande namn.

## Rename Variable
Enkel men kraftfull refactoring som förbättrar läsbarheten genom att ge variabler meningsfulla namn.

## Replace Magic Number with Constant
Ersätter hårdkodade siffror med namngivna konstanter för bättre läsbarhet.

## Extract Class
När en klass gör för mycket — flytta relaterade fält och metoder till en ny klass.

## Inline Method
Ersätter en metodkropp direkt på anropsstället när metoden är trivial och bara används på ett ställe.

## Move Method
Flytta en metod till en klass där den hör hemma (där datan finns).

## Replace Conditional with Polymorphism
Ersätt switch/if-else med arv och polymorfism.

## Red-Green-Refactor
TDD-cykeln: skriva ett misslyckande test (rött) → få det att fungera (grönt) → förbättra koden (refaktorera).

