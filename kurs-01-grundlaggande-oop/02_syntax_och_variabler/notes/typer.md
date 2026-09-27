# Typer

🟢


En typ är en klassificering av data som används för att bestämma vilken typ av värde en variabel kan lagra. I C# finns det två typer av typer: värdestyper och referenstyper.

## Värdestyper

Här är en lista över några vanliga värdetyper i C# tillsammans med deras beskrivning, minsta värde, högsta värde, datalagring och storlek i minnet:

| Kategori    | Typ     | Beskrivning                            | Minsta                         | Högsta                        | Lagrar         | Storlek                                                |
| ----------- | ------- | -------------------------------------- | ------------------------------ | ----------------------------- | -------------- | ------------------------------------------------------ | -------- |
| Primitiv    | bool    | Booleskt värde                         | False                          | True                          | Booleskt värde | 1 byte                                                 |
| Primitiv    | char    | 16-bitars Unicode-tecken               | '\0'                           | '\uFFFF'                      | Unicode-tecken | 2 byte                                                 |
| Primitiv    | byte    | 8-bitars heltal                        | 0                              | 255                           | Heltal         | 1 byte                                                 |
| Primitiv    | sbyte   | 8-bitars heltal (signerat)             | -128                           | 127                           | Heltal         | 1 byte                                                 |
| Primitiv    | short   | 16-bitars heltal                       | -32768                         | 32767                         | Heltal         | 2 byte                                                 |
| Primitiv    | ushort  | 16-bitars heltal (osignerat)           | 0                              | 65535                         | Heltal         | 2 byte                                                 |
| Primitiv    | int     | 32-bitars heltal                       | -2147483648                    | 2147483647                    | Heltal         | 4 byte                                                 |
| Primitiv    | uint    | 32-bitars heltal (osignerat)           | 0                              | 4294967295                    | Heltal         | 4 byte                                                 |
| Primitiv    | long    | 64-bitars heltal                       | -9223372036854775808           | 9223372036854775807           | Heltal         | 8 byte                                                 |
| Primitiv    | ulong   | 64-bitars heltal (osignerat)           | 0                              | 18446744073709551615          | Heltal         | 8 byte                                                 |
| Primitiv    | float   | 32-bitars flyttal med enkels precision | -3.4028235E+38                 | 3.4028235E+38                 | Flyttal        | 4 byte                                                 |
| Primitiv    | double  | 64-bitars flyttal med dubbel precision | -1.79769313486232E+308         | 1.79769313486232E+308         | Flyttal        | 8 byte                                                 |
| Primitiv    | decimal | 128-bitars decimaltal                  | -79228162514264337593543950335 | 79228162514264337593543950335 | Decimaltal     | 16 byte                                                |
| Abstrakt    | enum    | Uppräkningsvärde                       | -                              | -                             | Heltal         | Varierar                                               |
| Referenstyp | string  | Textsträng                             | -                              | -                             | Textsträng     | Varierar                                               |
| Referenstyp | object  | Basobjekt för                          | Alla andra objekt              | -                             | -              | Alla objekt                                            | Varierar |
| Pekare      | -       | Pekare till minnesadresser             | -                              | -                             | Minnesadresser | 4 byte på 32-bitars system, 8 byte på 64-bitars system |

Observera att...

- Storleken för referenstyper och objekt varierar beroende på implementationen och plattformen (32-bitars eller 64-bitars).
- Storleken som anges i tabellen är bara en generell indikation.
- Storleken i minnet kan variera beroende på den faktiska implementationen och plattformen.
- Värdestyper lagras i stacken och referenstyper lagras i heapen.

### Stack och heap

Tänk dig att du har en leksakslåda där du kan placera olika saker. Lådan har två delar:
en övre del som kallas "stacken" och en nedre del som kallas "heapen".

**Stacken** är som en hög med pappersark. Du kan bara lägga till eller ta bort saker längst upp på högen. När du lägger till något, placerar du det överst på högen och när du tar bort något tar du det översta pappersarket. Det är snabbt och enkelt att lägga till eller ta bort saker från stacken eftersom du alltid arbetar med det översta pappersarket. I programmering används stacken för att hantera små bitar av information som behövs direkt och temporärt. Till exempel kan variabler och funktioner placeras på stacken. När en funktion anropas lägger vi till information om den funktionen längst upp på stacken. När funktionen är klar tas den översta informationen bort och vi fortsätter där vi slutade.

**Heapen** är som en stor lekyta där du kan placera större saker. Du kan lägga till och ta bort saker var som helst på lekytan. När du placerar något i heapen behöver du inte oroa dig för att det ska vara längst upp eller längst ned. Du kan placera det var som helst där det finns plats. Men när du vill ta bort något från heapen måste du veta exakt var det är och ta bort det därifrån. I programmering används heapen för att hantera större bitar av information och data som kan vara mer permanenta. Till exempel kan objekt och datastrukturer placeras i heapen. De kan vara större och behöva mer plats, och vi kan komma åt dem och använda dem från olika delar av programmet.

Så sammanfattningsvis, stacken är som en hög med pappersark där vi snabbt kan lägga till och ta bort saker från toppen. Heapen är som en stor lekyta där vi kan placera större saker och ta bort dem när vi vet exakt var de är.

## Termer

| Term             | Förklaring                                                                                        |
| ---------------- | ------------------------------------------------------------------------------------------------- |
| Abstrakt         | En typ som inte kan instansieras.                                                                 |
| Datatyp          | En klassificering av data som används för att bestämma vilken typ av värde en variabel kan lagra. |
| Enum             | En typ som används för att skapa uppräkningsvärden.                                               |
| Heap             | En del av minnet som används för att lagra referenstyper.                                         |
| Instans          | En specifik förekomst av en klass.                                                                |
| Klass            | En mall för att skapa objekt.                                                                     |
| Minne            | En plats där data lagras.                                                                         |
| Minnesadress     | En unik identifierare för en plats i minnet.                                                      |
| Objekt           | En instans av en klass.                                                                           |
| Pekare           | En variabel som lagrar en minnesadress.                                                           |
| Primitiv         | En typ som är inbyggd i språket.                                                                  |
| Referens         | En pekare till en plats i minnet där ett objekt finns.                                            |
| Referenstyp      | En typ som lagrar en referens till ett objekt.                                                    |
| Stack            | En del av minnet som används för att lagra värdestyper.                                           |
| Uppräkningsvärde | En typ som används för att skapa uppräkningsvärden.                                               |
| Värde            | En bit av information som lagras i en variabel.                                                   |
| Värdestyp        | En typ som lagrar ett värde.                                                                      |
| Variabel         | En behållare som används för att lagra data.                                                      |

---
Sådärja. Nu har du koll på det här. Nästa steg — testa själv. Det är då det fastnar.
