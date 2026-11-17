# Kodgranskning med checklista

En kodgranskning är när någon annan läser din kod innan den används. Det är ett av de bästa sätten att hitta fel, och det är också ett sätt att lära sig av varandra.

Det svåra med granskning är att man lätt missar det man inte tänkte på. Därför använder vi en **checklista**. Den gör granskningen likadan varje gång, och den påminner dig om det som är lätt att glömma.

## Varför checklista?

Utan en lista läser man koden och hittar det man redan tänker på. Med en lista går man igenom samma frågor varje gång, även när man är trött eller när koden verkar bra.

En checklista är inte facit. Den säger vad du ska kolla, inte vad svaret är.

## Två roller

Vid pargranskning har ni två roller:

- **Förare:** kör programmet och skriver in indata.
- **Granskare:** följer checklistan och ställer frågor.

Byt roller efter varje projekt, så att båda får öva på båda.

## Så gör ni i par

1. Kör programmet och testa gränsfallen, till exempel tomma värden och negativa tal.
2. Jämför era checklistor. Vad hittade den ena som den andra missade?
3. Enas om en gemensam lista med fel.
4. Skriv en motivering per fel: vad är felet, och vad blir konsekvensen?

Punkt 4 är viktigast. Ett fel utan konsekvens är bara en åsikt. Om du kan säga vad som händer när felet slår till, har du hittat något som spelar roll.

## Att vara en bra granskare

- Kritisera koden, inte personen. "Den här metoden gör två saker" är bättre än "du har skrivit en dålig metod".
- Ställ frågor i stället för att säga vad som ska göras. "Vad händer om listan är tom?" hjälper mer än "lägg in en kontroll".
- Acceptera att ni inte är överens. Det är meningen med granskningen. Skriv ner det ni är oeniga om.

## Övning

Gå till `exercises/kodgranskning_checklista.md`. Startrepona är `clo26-kodgranskning-biblioteket` och `clo26-kodgranskning-parkering`. Kravspecifikationen och de kända varningarna står i respektive README.
