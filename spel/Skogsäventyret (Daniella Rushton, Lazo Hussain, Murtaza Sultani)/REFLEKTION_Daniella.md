# Reflektion

**Namn:**  Daniella
**Kurs:**  Grundläggande OOP i C#
**Uppgift:**  Slutprojekt
**Datum:**  2026-09-28

---

## Vad var svårast att lösa?

* Var fastnade du? Vad tog längre tid än du trodde — och hur kom du vidare?*

>  När jag var klar med min Monster-hierarki så hade jag fortfarnde inte tillgång till Player-klassen, så
   jag fick skapa en egen liten Player-stub som hade de värden jag behövde för att kunna köra programmet
   och se så det inte blir några kompileringsfel. Den var inte lång, det var bara så att programmet hade
   något att basera Player på när monstret attackerar spelaren. Men sedan när jag skulle utveckla programmet och göra en spelloop
   och stridssystem behövde jag betydligt mer värden från Player-klassen som jag ännu inte hade fått tillgång till. Så då frågade
   jag Claude om den kunde göra en färdig Player-stub åt mig så jag kunde kompilera programmet och se till att jag kunde fortsätta
   utveckla spelet. 

   Sedan när det kom till att göra vapenshoppen så fick jag ta över det arbetet senare på kvällen den 4e okt och då ville jag inte 
   sätta massa tid på att planera upp ett litet vapen-system så då frågade jag också Claude om den kunde rita upp ett upplägg, alltså 
   vad som behövdes göras för att få till ett fungerande system så att jag inte skulle behöva arbeta alllt för länge och långt in 
   på natten för att uppfylla kraven. Jag matade också in koden till Claude och bad den felsöka några gånger då jag ibland exempelvis
   fick kompileringsfel som att jag använde fel namn när jag kallade på metoder.

---

## Hur fungerade samarbetet i gruppen?

* Vad fungerade bra? Vad var svårt? Hur delade ni upp arbetet?*  

> Det svåraste har varit kommunikationen. Eftersom vi saknat en gruppmedlem sedan start har det varit svårt att
  fördela delar av projektet jämt mellan oss alla. Men med de medlemmar vi haft, jag och Lazo bestämde vi oss att 
  dela upp klass-hierarkierna mellan oss så vi skulle kunna få en grund att stå på. Tanken var att ge Murtaza i 
  uppgift att skapa spelloopen, men det gick inte då vi inte haft kontakt med honom, så då tog jag över det arbetet.
  Sedan för att satsa på VG-kraven så delade vi upp arbetet med att jag fick skapa arenan och Lazo fick skapa vapensystemet.
  Men vapenshopen blev för utmanande för Lazo att hinna med, så då tog jag över det arbetet också. 

---

## Om du fick göra om det — vad hade du gjort annorlunda?

*Tänk på din lösning, din struktur, eller hur ni jobbade. Vad skulle du ändra?*

> Jag hade nog sett över att vi kom överrens om namn på objekt och klasser osv i förtid så jag inte behövde gissa
  mig till olika namn. Jag hade också önskat att gruppmedlemmar ladda upp sina delar av projektet till 
  GitHub när de var klara ASAP eftersom jag eller de kan behöva använda sig av delar av kod någon annan har. 
  Exempelvis när jag skapade AttackPlayer() så kallade jag på klassen Player som jag egentligen inte hade just 
  då - så jag  behövde lägga lite mer tid på att göra en egen liten kortfattad Player-stub istället för att 
  kunna fokusera på mitt egna arbete.

  Jag hade också sett till att vi kom överrens om vi vill göra VG-krav eller ej då det i detta fall blev rätt rörigt när jag 
  eventuellt fick ta över båda delar av VG-kraven eftersom jag fick börja att göra Lazos del av VG-kraven sent på eftermiddag på 
  inlämningsdagen (söndag).

---

## Vilken datastruktur valde ni för vapensortimentet och varför?

>  Jag valde att använda en List<Weapon> för vapensortimentet i shoppen för att det gjorde det enkelt tt visa sortimentet med 
   numrerade val som speladen kan skriva in för att köpa ett vapen eftersom listan redan har inbyggd indexering. Jag känner att hade 
   jag använt Dictionary<string, Weapon> för att slå upp vapen genom att skriva namn så skulle det vara lite onödigt komplicerat 
   eftersom spelaren hade behövt skriva in exakt namn på vapnet. Dessutom passar det mer med en lista eftersom alla andra val i spelet
   är mellan 1-3, och att ha en random "skriv ditt vapenval" skulle nog se lite konstigt ut i terminalen.

---

## Hur sorterade ni monstren i arenan?

>  Vi valde att sortera monstren i arenan med en List<Monster> för att arenan ska bestå av sju monster i stigande svårighetsgrad. 
   Hade vi valt att använda oss av MonsterSpawner-klassen hade vi inte kunnat garantera att monsterna började med den svagaste och 
   avslutat med den starkaste. Därmed kändes det enklast att bestämma ordningen på monstrena direkt från början, och skulle man sedan
   vilja ändra ordningen eller lägga till/ta bort monster så är det bara att ändra i listan.

---

## Hade ni kunnat lösa arenan utan arv?

> Tekniskt sett ja, men det hade gjort koden mycket mer klunkig och komplicerad. Jag hade behövt skapa flera Monster-objekt med sina
  egna värden istället för att skapa en subklass som ärver från basklassen Monster. Nu kan vi exempelvis skriva new Goblin() 
  istället för att behöva komma ihåg alla givna värden för Goblin-objekt. Det gjorde också så vi kunde använda oss av List<Monster>
  isället för att behöva skapa en lista för varje typ av monster (List<Goblin>, List<Orc> osv), vilket i sin tur gör det enkalare
  att i framtiden lägga till eller ta bort monster vid behov. 