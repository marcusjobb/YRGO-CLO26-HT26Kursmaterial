BDD Övningar
Gör uppgifterna gärna i grupp.
Här ska du skapa testscenarios, ingen kod behövs.
Johan och pizzor
Johan håller på att skriva ett program för att hålla kolla på sin ekonomi. Den 26e maj, dagen efter lön
och räkningar har han 6320:- på sitt konto lönekonto, det är fredagskväll och han är väldigt sugen på
en stor kebabpizza. Han har 33:- på kortet och flyttar 100:- från lönekontot till kortet. Han registrerar
sitt kommande inköp i programmet, kebabpizza 105:-, läsk 15:-. 120:- kronor allt som allt.
Den 22a juni, dagen innan lön blir han väldans sugen på pizza igen, men då har lönen inte kommit in
och han har 55:- på lönekontot. Kortkontot har 10:- så han behöver 50:- till minst för att kunna köpa
en pizza. Han knappar in i programmet vad han planerar att göra och programmet svarar då ”Det
finns inte tillräckligt mycket pengar på kontot”.
Beskriv dessa scenarios med Gherkin (valfri språk).
Johan ändrar diet
Efter några månaders pizzaätande inser Johan att ”människan kan inte lever på pizza allena” och
bestämmer sig för att skriva en alldeles egen ekonomi/bantningsprogram. Hans program kommer att
begränsa så att han bara får köpa en pizza i månaden, eller max 500:- i snabbmat. Den 27e augusti
blir han väldans sugen på pizza igen, han har ändå varit duktig denna månad och inte köpt annat än
Churros för 55:- på Matfestivalen (Tyg Malmöfestivalen eller Göteborgs kulturkalas) på stan. Han har
445:- på kortet och knappar in det i sitt program, då han inte köpt så mycket snabbmat får han OK
från programmet.
Beskriv scenariot i Gherkin
Senare i september är Johan på fest hos några kompisar. Hans snabbmatskort har nu 500:- På vägen
till festen handlade han hamburgare på Max för sig själv och en av kompisarna 220:- allt som allt. Han
vaknar dagen efter med en fantastisk skallebånk och en okänd person vid honom i sängen. Trots sin
skallebånk vill han vara en god värd och bestämmer sig för att bjuda på en pizza. Han kollar i
programmet. Pizza för 120 x 2 = 240. Detta innebär att hans kort borde ha 40 kronor kvar på kontot.
Beskriv scenariot i Gherkin

Från Gherkin till kod
Skapa enkla program baserade på beskrivningen och verifiera att det fungerar genom testning.
Nedräknare till julafton
Background: Användaren är en jul-fantast och längtar till jul och vinter
Given: Dagens datum är före julafton samma år
When: Metoden för beräkning körs med dagens datum som inparameter
Then: Metoden ska returnera antalet dagar kvar till jul
Överföring mellan konto
Background: Användaren är sugen på pizza och vill flytta pengar från lönekontot till kortet
Given: Lönekontot innehåller 10532:- och pizza + läsk kostar 120:When: Överföring görs från lönekonto till kortkonto
And: Lönekonto innehåller mer än 120
And: kortkonto innehåller mindre än 120
Then: överför mellanskillnaden från lönekonto till kortkonto
Then: Kortkonto bör ha 120:-
