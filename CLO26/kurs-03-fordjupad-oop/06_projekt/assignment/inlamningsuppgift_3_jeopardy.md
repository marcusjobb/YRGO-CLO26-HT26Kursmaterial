Täckta kursplansmål:

- Kunna planera, designa och implementera gränssnitt utifrån användaren.

- Kunna utveckla felfria fristående program.

- Självständigt kunna dokumentera sitt arbete.

VG mål:

- Den studerande utvecklar med säkerhet program med en tydlig objektorienterad struktur.

- Den studerande skapar med säkerhet felfria fristående program.


Projektuppgift       2-3 personer per grupp

Deadline:    23 Oktober

Mål:

Skapa ett eget Jeopardy program med hjälp av en .tsv fil (en slags text fil) fylld med Jeopardy frågor. https://github.com/jwolle1/jeopardy_clue_dataset en repo där man kan hämta filerna.

Programmet har en visuell del (Jeopardy i konsollen, att man kan köra flera rundor och samla poäng, osv.) och en del som läser igenom .tsv filen och plockar ut slumpade frågor efter önskad kategori.

Dela gärna upp arbetet så att en person ansvarar för den visuella biten (Spelrundor, UI) och att en person jobbar med att läsa av/samla in data. Sen får ni gärna hjälpa varandra när ni fastnar.

Ett bra ställe att börja är att skriva en klass var. T.ex. en 'JeopardyGame' klass som sköter spelet och när den behöver få tag på frågor att visa, kallar på en 'JeopardyQuestions' klass som sköter att ladda in och sortera/slumpa bland frågor efter behov.

OBS! Använd en VS Solution per grupp och en gemensam git repo uppe på GitHub som ni pushar till och jobbar på gemensamt!

Minimikrav:

- Använd Git och GitHub för att verisionshantera VS projektet. (Det förväntas att alla i gruppen bidragit jämt och regelbundet i commit historiken)

- Programmet ska gå att köras utan några större fel.

- Använd OOP för att strukturera koden med rimliga namn på saker.

- Koden ska vara läslig och det ska finnas kommentarer där det behövs.

VG krav:

- Koden du själv skrivit har omstrukturerats där det varit lämpligt. (Upprepad kod bryts ut i metoder, gemensamma beteenden mellan klasser löses genom arv, etc.)

- Där nödvändigt så används try/catch för att hantera gränsfall
