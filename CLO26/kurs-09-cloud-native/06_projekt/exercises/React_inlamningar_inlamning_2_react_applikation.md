Inlämningsuppgift 2 – React

Täckta kursplansmål:
För godkänt:





Routing & Autentisering - Den studerande påvisar kunskap om hur routing och autentisering
sker mellan användaren och mjukvaran
Hooks - Den studerande följer Reacts komponentmodell Hooks och uppvisar förståelse för i vilka
sammanhang man ska använda sig av den.
React Fundament - Den studerande uppvisar färdighet att tillämpa grunderna i React med
komponenter, klasser, styling och JSX
Single Page Application-utveckling med React (SPA) - Den studerande använder verktyget React
för att bygga en enkel Single Page Application

För väl godkänt:




React Fundament - Den studerande hanterar avancerade koncept inom React såsom React
components vs React containers, Class components vs Functional components, Hooks och
Reacts livscykel.
Single Page Application-utveckling med React (SPA) - Den studerande hanterar relevanta
verktyg och bibliotek och påvisar sin förmåga att tillämpa och dra nytta av dessa genom
inlämningsuppgifter

Inlämningsuppgift 2 – Krav:
För betyget Godkänt:

1. Initiera en ny React applikation med hjälp av NPM & NPX
Det går bra att göra detta i TypeScript, men det är absolut inget krav.
https://reactjs.org/docs/create-a-new-react-app.html

2. React-Router-DOM
Installera det externa paketet React-Router-DOM samt initiera det i ditt projekt. Din applikation
skall ha minst 2 vyer(React komponenter) som man ska kunna navigera sig till. Det skall också
finnas en ’fallback’ vy ifall man navigerar sig till en obefintlig URI så skall man omredigeras till
’fallback’ vyn.
https://reactrouter.com/web/guides/quick-start

3. Inbyggda funktioner I React-Router-DOM
Dina två vyer du har skapat skall kunna navigera sig till varandra. Använd dig av useHistory i
båda komponenterna i valfritt element (<h1>, <span>, <button>, ...) eller link för att skapa denna
funktionalitet.
Navigationen skall se ut som följande:
Vy 1 -> Vy 2
Vy 2 –> Vy 1
https://reactrouter.com/web/api/Hooks
https://reactrouter.com/web/api/Link

4. Skicka med värden i samband med useHistory
När det nu går att navigera sig mellan dessa olika vyer skall du nu också skicka med ett valfritt
värde (Det skulle kunna vara en String, Number, Object, Array...). Du gör detta genom att
antingen lägga till ett andra argument i .push(URI, Value) metoden i useHistory eller genom att
dekonstruera to-attributet i Link. Värdet skall sedan hämtas och visas i den vy man navigerats
till. Du kan nå värdet i den vy du skickar värdena till med hjälp av useLocation ifrån paketet
’React-Router-DOM’
Värdet som skickas med ska synas grafiskt i valfritt element i den vyn man navigerat sig till.
D.V.S att värdet skall synas i valfri tag och inte i exempelvis console.log/localStorage
https://reactrouter.com/web/api/Hooks

5.

Din applikation skall göra minst ett HTTP anrop till ett API som retunerar data ifrån valfritt API
som framträder i ditt GUI på valfritt vis.
Tidigare API’er vi har tittat på som man kan använda sig av:
Pokemon API: https://pokeapi.co /
Starwars API: https://swapi.dev/
Ytterligare exempel på API’er med öppen källkod:
https://mixedanalytics.com/blog/list-actually-free-open-no-auth-needed-apis/

6.

Lokal datalagring i React
Den data som retuneras ifrån API-anropet skall lagras inuti ett useState i valfri komponent.
valfri data ifrån anropet skall synas grafiskt på valfritt vis.
https://reactjs.org/docs/hooks-state.html

7.

Iterationer i JavaScript med .map() funktionen
map är en förekommande funktion som på senare tid har ersatt Array iterationer med
ForEach/While. Använd den data som kommer ifrån ditt API-anrop(alternativt hårdkoda en

Array med valfria värden) för att iterara igenom hela arrayen och visa upp data grafiskt.
https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/map

8. Ladda upp era uppdateringar till ett ’repository’ på Github.
https://docs.github.com/en/github/managing-files-in-a-repository/adding-a-file-to-a-repository

För betyget Väl Godkänt:
1. Anropa en funktion vid första rendering
Din applikation skall använda sig av useEffect för att utföra ett HTTP anrop vid första rendering av

applikationen.
Under tiden detta API anrop sker skall det även framgå en spinner/loadingbar som sedan försvinner
efter att anropet har utförts.
2. Globala värden med useContext hook
Med hjälp av useContext skall din applikation spara den retunerade datan ifrån API-anropet till ett
globalt värde i en komponent som därefter hämtas ifrån en annan komponent som använder sig av det
globala värdet och framträder datan på valfritt vis.
https://reactjs.org/docs/hooks-reference.html#usecontext

3. Ladda upp era uppdateringar till Github.
https://docs.github.com/en/github/managing-files-in-a-repository/adding-a-file-to-a-repository

_____________________________________________________________________________________
Inlämningen sker genom att skicka en zip på ditt project
samt en Github länk till projektet i Google Classroom

Lycka till!
Björn

---
Sådärja. Nu har du koll på det här. Nästa steg — testa själv. Det är då det fastnar.
