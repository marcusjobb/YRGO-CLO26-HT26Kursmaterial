# Git återblick

🟢


En liten tillbakablick på föreläsningen och tidigare Github exempel.


# Kommandon

| Kommando | Funktion | Exempel |
| --- | --- | --- |
| Clone | Hämtar en repository från servern | Git clone https://github.com/marcusjobb/NET20D.git |
| Fetch | Hämtar listan med senaste ändringar från servern | Git fetch |
| Pull | Hämtar senaste ändringar i den aktuella branchen | Git pull |
| Pull force | Hämtar senaste ändringar och skriver över allt i den aktuella branchen. | Git pull –force |
| Checkout | Byter till en annan lokal branch | Git checkout MyBranch |
| Checkout -b | Skapar en ny branch | Git checkout -b MyNewBranch |
| Reset | Återställer en branch till originalform | git reset --hard origin/master |
| Clean | Raderar allt som inte commitats | git clean |
| Push | Skickar de commitade ändringarna till server | Git push |
| Stash | Sparar dina ändringar i en "stash" | Git Stash --all |
| Stash Save | Sparar din stash för framtida bruk (commitas inte) | Git stash save "Bra ändring för framtida bruk" |
| Stash List | Visar en lista på sparade stash | Git stash list |
| Stash apply | Lägger tillbaka din stash i din branch | Git stash apply |
| Stash apply id | Lägger tillbaka en specifik stash i din branch | Git stash apply id |
| Stash pop | Lägger tillbaka dina ändringar i din branch | Git stash pop |
| Stash show | Visar ändringarna som är sparade i din stash | Git stash show |
| Stash drop | Raderar en stash eller senaste om inget id anges | Git stash drop |
| Stash clear | Raderar alla stash | Git stash clear |


# Vanligt tillvägagångssätt


## Skapa repo

- Skapa en repo på Github / GitLab / VisualStudio.Com eller i någon annan git server

- Clona din repo antingen via Visual Studio eller consolen (git clone https://.....)

- Lägg till Readme.md

- Lägg till .gitignore (för visual studio eller för vad du utvecklar i)

- Lägg till Licens (viktigt så att ingen tar din kod och påstår att de själva gjort det)

- Välj en passande licens här: https://choosealicense.com/ 
jag föreslår någon av dessa

- MIT – använd koden hur du vill men ta med originalkoden och copyright texten

- GNU V3 – Lite mer strikt

- I din readme skriv att all koden är copyrightad till dig och får bara användas enligt den licens som du valt.

## Vanliga branches

Main/Master branchen finns alltid men skapa följande brancher till att börja med

- test (release, inlämning eller vad du vill kalla den, baserad på main)

- develop (baserad på test)

Därefter skapar du en ny branch som baserar sig develop.

## Tilldela rättigheter på GitHub till andra att arbeta med ditt repo

Klicka på Settings menyn och sedan på sidomenyn "Manage Access"

Klicka sedan på knappen  och där kan du mata in emailadressen eller github alias till den du vill lägga till.


När personen tryckt på länken i emailet så kommer denne att ha tillgång till din repo.

# Skrivskydda din main

I sidomenyn finns alternativet Branches, använd den för att skydda upp din main.


# Testning på Github

När vi arbetar i projekt är det bra att kunna samla koden på en plats. Github är snäll på så sätt, vi samlar koden och den håller historiken för vår kod. Hur lyxigt som helst. Inne på GitHub skapar vi en workflow. Har vi laddat upp ett C# core projekt så kommer den att föreslå


Klicka på den så kommer den att visa källkoden för scriptet


Du kan radera koden med gott samvete och ersätta den enligt nedan och spara.

| Kod: |  |
| --- | --- |
| name: .NET Core Build with Tests  on:   push:     branches: [ main ]   pull_request:     branches: [ main ]  jobs:   tests:     name: Unit Testing     runs-on: windows-latest     steps:       - uses: actions/checkout@v2.1.0       - run: dotnet test |  |

När du du kör en pull request eller push till main kommer du att se följande status.


Även om det är en fantastisk grej så använd det sparsamt, Github bjuder på 2000 minuter i kompileringstid per månad, en enkel körning tar ungefär 1-3 minuter, beroende på storlek på projektet och antal tester.

Vad betyder koden ?

| Kod | Förklaring |
| --- | --- |
| name: .NET Core Build with Tests | Namnet eller etiketten som visas när den kör |
| on:   push:     branches: [ main ]   pull_request:     branches: [ main ] | Var och när regeln ska gälla I detta fall vid alla Push eller Pull requests på main branchen  Samma regler kan implementeras på develop branchen exempelvis |
| jobs: | Efter den kommer olika uppgifter att definieras |
| tests: | Namnet på uppgiften |
| name: Unit Testing | Etikett för uppgiften |
| runs-on: windows-latest | Server den ska köras på |
| steps: | Steg som ska utföras i uppgiften |
| - uses: actions/checkout@v2.1.0 | Talar om vilket script som ska köras, vi kan alltså skapa egna scripts och köra dem vid detta steg |
| - run: dotnet test | Kör kommandot dotnet test, som kompilerar och kör test funktionerna i projektet |


# Morgonritual

Detta gäller om man är flera som arbetar mot samma repo

- Git refresh

- Git checkout develop

- Git pull

- Git checkout {branchen du arbetar på}

- Git merge develop

- Git add .

- Git commit -m "Morgon merge med develop"

- Git push

# Eftermiddagsritual / Push ritual

Detta gäller om man är flera som arbetar mot samma repo

- Git refresh

- Git pull

- Git add .

- Git commit -m "{Beskriv vad du gjort}"

- Git push


# Kodningsritual

Detta gäller oavsett om det är flera på repot. Gör detta vare sig du ska skapa en ny feature eller ändra i koden. Det räcker med att du kör steg 1-3 en gång om dan såvida inte du vet att andra har pushat mot develop.

- Git refresh

- Git checkout develop

- Git pull

- Git checkout -b {ny branch med namn matchande vad du ska göra för feature}

- Koda vilt

- Git add .

- Git commit "{beskriv dina coola ändringar}"

- Git push

När du är klar och allt fungerar som det ska… fortsätt från punkt 9.

- Om allt fungerar – skapa en pull request mot develop

- Git refresh

- Git checkout develop

- Git pull

- Git checkout {branchen du arbetar på}

- Git merge develop

- Git add .

- Git commit -m "Merge med develop"

- Git push

- Skapa pull request


# Visual Studio 2019

Visual studio gör att vi slipper skriva alla de git kommandon om och om igen. Längst ner på skärmen kan vi se följande.


- Det första talar om vilken server och repo vi arbetar mot

- Klickar vi på den kan vi se listan på pull requests och där kan vi skapa pull requests

- Pilen visar att vi har commitat men inte pushat, den visar hur många commits vi har.

- Klickar vi på den kan vi se repots historik och möjlighet att pusha våra commits

- Använd helst push and sync för att pusha och fetcha samtidigt

- Pennan visar hur många ändringar vi gjort sedan senaste commit

- Klickar vi på den får vi en lista på ändringar och en textruta där vi kan committa

- Det är lätt att fylla i den lite snabbt och committa. Gör det ofta!

- De ändrade filerna visar där och vi kan lätt se om vi ändrat något av misstag och ångrar det. Vi kan även ta tillbaka filer vi raderat sen senaste comitten.

- Versionshanteringsinkonen visar vilken repo vi är på

- Den visar samma sak som push knappen

- Pilarna visar vilken branch vi är på.

- Här kan vi välja loka branch att arbeta på och om det inte finns lokalt kan vi hämta branchen från servern.

- Här kan vi också skapa egna brancher som sedan kan skickas upp och pull requestas.


# Att tänka på

- Pusha vid dagens slut om du inte orkar pusha efter commit.

- Skapa nya feature branches för varje gång du ska ändra i koden.

- Pusha innan du skapar en ny branch.

- Commita efter varje större ändring, exempelvis om du skapat en ny metod, ändrat drastiskt i en metod eller lagt till nya saker.

- Undvik att göra ändringar direkt i din develop branch, ändra i din feature branch och merga med develop senare.

- Alla nya ideér som du vill testa i din kod, gör det i egna feature brancher.

- Skapa en ny branch innan du går bersärkagång på din kod med refactoring och annat uppsnyggande.

- Lika bra att vänja sig vid det nu under utbildningen då det kommer att bli så du arbetar sen på LIAn eller ditt framtida jobb.

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
