# Göteborgs Skogsäventyr – grafiskt gränssnitt

Dubbelklicka på **Starta.bat** efter att du har packat upp hela zip-filen. Datorn behöver **.NET 10 SDK**. Fullständiga instruktioner finns i **STARTA-HAR.txt**.

## Originalkod och grafik

Original innehåller det ursprungliga projektet, dess källkod och rapporter. Alla originalfiler är oförändrade och har jämförts med den inskickade zip-filen.

Grafik är ett separat projekt. Det kompilerar originalets C#-filer direkt, väljer GraphicHost som startpunkt och anropar originalets Main. Console.SetIn och Console.SetOut kopplar konsolen till webbgränssnittet. Knappar skickar samma menyval som konsolen frågar efter. Skada, slump, XP, guld, föremålsbonusar, vila, död och Valborg hanteras av originalet. Ingen spellogik är kopierad till JavaScript.

Webbsidan läser originalets konsoltext för att visa menyer, status, stridsfigurer och ägda föremål. Om originalets menytexter ändras behöver presentationsdelen anpassas. Originalets text finns också i Äventyrsdagboken.

## Gränssnitt

Den bifogade bilden används som bakgrund för hela spelet. Text och menyer ligger på ljusa halvgenomskinliga paneler. Spelaren och alla tre monster har egna SVG-figurer. Ägda föremål visas på spelaren. Azalea väljs i menyn och visar en gånganimation före mötet. Animationen ändrar inga dagar eller regler. Vid avklarad Valborg visas konfetti och ett tydligt meddelande om originalets belöning på 50 XP.

Starta om spelet längst ner avslutar den pågående körningen vid konsolinmatningen och anropar originalets Main igen. Detta hanteras i den separata grafikservern, utan ändringar i originalkoden. Layouten anpassas för mindre skärmar och respekterar inställningen för minskad rörelse.

## Verifiering

Grafikprojektet kompilerar med .NET 10. Originalkoden ger befintliga kompilatorvarningar. Originalfilerna har jämförts med SHA-256 utan avvikelser. Menyer, butik, strid, flykt och omstart har kontrollerats. Layout, figurer, föremål och Valborgsmeddelande har granskats i webbläsaren.

Starta.bat öppnar nu automatiskt en ny spelserver på en egen ledig lokal port, först när servern är redo. Äldre spelservrar och flikar återanvänds inte. Varje start börjar vid namnfrågan.
