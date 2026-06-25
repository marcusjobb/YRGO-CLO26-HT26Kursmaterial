# Mysteries — Programmering och arbetslivet

Anpassade från H.A. Ripleys *Minute Mysteries* (1932, public domain).
Originalet: https://www.gutenberg.org/files/50603/50603-h/50603-h.htm

Varje mystery är en kort berättelse med en logisk omöjlighet inbäddad.
Studerande ska identifiera vad som inte stämmer.

Svaren ligger längst ner — eller i lärarens facit.

---

## 1. Expertens misstag

Inspector Lindberg ringde upp Fordney sent en kväll.

"Vi har fångat någon som skickat ett anonymt felrapport till ett open source-projekt. Personen påstår sig vara en nybörjare som precis börjat lära sig programmera — vill inte avslöja sin identitet för att de är 'för ovana för att bidra ordentligt'. Rapporten är full av stavfel: 'variabol', 'metood', 'looopen'. Men vi vet att det är en erfaren utvecklare som skickat den."

"Hur vet ni det?" frågade Fordney.

"Det är uppenbart," sade Lindberg. "Kan du se varför?"

*Vad avslöjar rapporten?*

> **Svar:** Rapporten innehåller stavfel på enkla ord men stavar "asynkron", "polymorfism" och "NullReferenceException" helt korrekt. En verklig nybörjare hade stavat de enkla orden rätt men missat de svåra fackorden — inte tvärtom.

---

## 2. Offlineutvecklaren

"Jag var helt offline hela eftermiddagen," försäkrade Ahmad. "Routern gick ner klockan 13 och kom inte upp igen förrän vid 18. Jag kodade utan IDE, bara Notepad. Jag har inget ansvar för det här felet som pushades klockan 15."

Fordney tittade på felmeddelandet Ahmad bifogat i sin rapport.

"Ditt alibi håller inte," sade han lugnt.

*Varför inte?*

> **Svar:** Ahmad bifogade en skärmdump av felmeddelandet — inklusive den exakta raden i stack tracen, med filsökvägen formaterad precis som Visual Studio visar den. Notepad visar inte stack traces. Han hade sin IDE öppen.

---

## 3. Den perfekta commit:en

Tre utvecklare satt i mötesrummet. En av dem hade under panik pushat en kritisk buggfix direkt till main mitt i natten — utan review, utan tester.

"Det var en kris," sa Vera. "Jag var stressad och desperat. Jag kokade kaffe klockan 2, satt i halvmörker och bankade in lösningen."

Fordney granskade commit:en i tystnad.

"Stressad och desperat," upprepade han. "Och ändå..."

*Vad avslöjar koden?*

> **Svar:** Commit:en har perfekt indragning, alla variabelnamn är genomtänkta, och koden är kommenterad på tre ställen. En person som kodar i panik vid 2-tiden på natten skriver inte snyggare kod än på dagtid — det här såg ut som en genomarbetad lösning som skrivits lugnt och metodiskt.

---

## 4. Hashen

"Jag knäckte lösenordet på typ tio sekunder," sa Johansson stolt. "Brute force, inga konstigheter. Här är beviset — hash in, lösenord ut."

Fordney tittade på hashen i filen.

"Tio sekunder," sa han. "Det är intressant."

*Varför är det omöjligt?*

> **Svar:** Hashen är bcrypt med cost factor 12. Bcrypt är designat för att vara långsamt — en modern dator testar ungefär 10–20 hashar per sekund. Tio sekunder räcker för att testa ett par hundra lösenord. Att hitta rätt lösenord på tio sekunder med brute force är statistiskt omöjligt om det inte var extremt enkelt — men det var det inte.

---

## 5. Konfigfilen

"Jag har aldrig öppnat den filen i mitt liv," sa Petra bestämt. "Jag visste inte ens att den existerade förrän ni visade mig den nu."

"Och ändå," sa Fordney, "vet du precis vad som är fel med den."

Petra tystnade.

*Vad hade hon sagt?*

> **Svar:** Tidigare i samtalet hade Petra sagt: "API-nyckeln på rad 47 borde vara omsluten med citattecken, annars parsas den som ett heltal." Hon visste inte bara att filen existerade — hon visste exakt var i filen problemet satt.

---

## 6. Den låsta servern

Produktionsmiljön hade gått ner mitt i natten. Enda sättet att deploya till den servern var via ett SSH-certifikat — och bara tre personer hade certifikaten: Anna, Björn och Carlos.

"Inte jag," sa Anna. "Jag sov."
"Inte jag," sa Björn. "Certifikatet låg på min jobbdator och jag var hemma."
"Inte jag," sa Carlos. "Jag har inte ens tillgång till prod längre — fick certifikatet revokerat för en månad sedan."

Fordney vände sig mot Carlos.

*Varför Carlos?*

> **Svar:** Om Carlos certifikat hade revokerats för en månad sedan — varför nämnde han det? Han hade inte blivit tillfrågad om det. Att frivilligt ta upp att man saknar tillgång, utan att det var relevant i frågan, är en typisk reaktion hos någon som vet att de annars är i riskzonen. Dessutom: ett certifikat kan ha sparats lokalt innan det revokerades.

---

## 7. Tidsstämplarna

"Jag jobbade inte den kvällen," sa Mikael. "Jag var på middag med hela familjen från 18 till 22. Fråga dem. Committen med buggen måste ha kommit in från något annat håll."

Fordney öppnade git-loggen och pekade på skärmen.

"Jo, committen är tidsstämplad 20:34," sa han. "Men titta här."

*Vad ser Fordney?*

> **Svar:** Git loggar både *author date* (kan sättas manuellt) och *committer date* (när git faktiskt körde). Author date är 20:34 — men committer date är 23:11, efter middagen. CI-systemets logg bekräftar att pipelinen triggade 23:12. Någon hade manuellt backsatt author date till 20:34 för att skapa ett falskt alibi.

---

## 8. Ögonvittnet

"Felet stod där tydligt i loggen," sa Susanne. "Jag såg det med egna ögon. Rad efter rad med röda felmeddelanden — hela stacken."

"Och du såg det i produktionsloggen direkt?" frågade Fordney.

"Ja, precis."

"Utan att logga in?"

*Vad är problemet?*

> **Svar:** Produktionsloggen kräver VPN och tvåfaktorsautentisering. Åtkomstloggen visar att Susanne inte loggat in i systemet den kvällen. Hon kan inte ha sett felet "direkt i produktionsloggen". Antingen hittar hon på, eller har hon tillgång via en kanal hon inte borde använda.

---

## 9. Den olästa notisen

"Jag har aldrig läst det mailet," sa Viktor. "Kolla gärna — det är fortfarande oläst i min inkorg. Fetstilt och allt."

"Men," sa Fordney, "du visste ändå att deadline var en fredag, inte en måndag som du påstod."

Viktor öppnade munnen och stängde den igen.

*Hur visste Viktor?*

> **Svar:** Mailet med deadline-informationen kan vara oläst — men deadlinen stod också i ett Slack-meddelande, i en kalenderinbjudan och i projektets README. Det olästa mailet är inte ett alibi för att man inte kände till informationen. Dessutom: att mailet är markerat "oläst" bevisar ingenting — man kan markera om ett mail som oläst.

---

## 10. Automatisk sparning

"Ingen har rört den filen sedan jag sparade den i fredags," sa Lena. "Jag låste filen med en skrivskyddsflagga. Titta på filens senaste ändringsdatum — fredag 16:23."

Fordney nickade. "Och projektet kompilerar fortfarande?"

"Ja, självklart."

"Då ljuger ändringsdatumet," sa han.

*Varför?*

> **Svar:** Koden refererar till ett bibliotek som inte existerade i fredags — det lades till i förra veckans release. Om filen verkligen var orörd sedan fredag kunde kompileringen inte använda det biblioteket. Antingen har filen ändrats trots skrivskyddet, eller så är "ändringsdatumet" fabricerat.

---

## 11. Alibit med versionshantering

"Jag var sjuk hela onsdagen," sa Tobias. "Sängliggande. Det kan mina commits bevisa — ingenting pushades av mig mellan tisdag kväll och torsdag morgon."

Fordney granskade repo:t.

"Du har rätt att du inte pushade något," sa han. "Men du kodade."

*Hur vet Fordney det?*

> **Svar:** Tobias pushade ingenting — men IDE:ns Settings Sync och autosparning hade loggat aktivitet på hans konto onsdagen. Fordney kollade också issue-trackern: ett ärende visade "Tobias läste detta 14:37 onsdag." En sängliggande sjuk person läser inte aktivt i projektets bugg-tracker mitt på eftermiddagen.

---

## 12. Femsekunders-bugg

"Det måste vara ett race condition," sa Erik säkert. "Det händer bara ibland, och jag kan inte reproducera det. Klassisk timing-bugg."

Fordney tittade på felloggarna som Erik skickat.

"Det är inte ett race condition," sa han. "Och du vet om det."

*Varför inte?*

> **Svar:** Race conditions är icke-deterministiska — de uppstår vid oförutsägbara tidpunkter. Men loggen visar att felet uppstår exakt var femte sekund, precis när en bakgrundstask triggar. Det är ett deterministiskt fel med en känd trigger. Erik hade antingen inte undersökt loggarna ordentligt — eller visste exakt var felet låg men ville slippa fixa det.

---

## 13. Pull request-hemligheten

"Jag har inte sett Marcus pull request," sa Diana. "Den kom in under min semester och jag läste inte e-post."

"Ändå hänvisade du till den i code review:n på Frejas PR," noterade Fordney.

"Det måste vara ett sammanträffande — liknande lösning."

Fordney skakade på huvudet.

*Vad avslöjar Diana?*

> **Svar:** Marcus PR använde ett ovanligt variabelnamn — `traversalDepthGuard` — som inte är en standardterm. Dianas kommentar på Frejas PR nämnde specifikt det namnet som "ett bra mönster att följa". Det är inte ett sammanträffande. Diana hade läst PR:en, troligen för att se om den konkurrerade med hennes eget pågående arbete.

---

## 14. Backupen som inte finns

"Jag körde backup klockan 22 precis som rutinen säger," insisterade serveradministratören. "Allting är loggat."

Fordney tittade på backup-loggen.

"Loggen säger att backupen startade," sa han. "Men backupen existerar inte."

"Konstigt. Det måste vara ett diskfel efteråt."

"Nej," sa Fordney. "Det är inte det."

*Vad är förklaringen?*

> **Svar:** Backup-scriptet loggar *start* av jobbet, inte slutförandet. En loggpost med "backup started 22:00" bevisar inte att backupen lyckades. Processen kraschade tyst — eller scriptet hade ändrats så att det loggade utan att faktiskt kopiera något. Loggen är inte ett kvitto på att jobbet slutfördes.

---

## 15. Den stängda IDE:n

"Jag hade stängt ner allt och gått hem," sa Nils. "VS Code, terminalen, allt. Jag såg felet precis innan jag stängde, men det verkade trivialt — ett varningsmeddelande, inget mer."

"Och vad stod det i varningsmeddelandet?" frågade Fordney.

"Att en variabel var odeklarerad på rad 203."

Fordney antecknade.

*Vad är problemet med Nils påstående?*

> **Svar:** Nils sa att felet "verkade trivialt" och att han stängde ner utan att göra något. Sedan mindes han exakt vad meddelandet gällde och var: "odeklarerad variabel, rad 203." En människa som snabbt avfärdar ett varningsmeddelande som trivialt minns inte exakt rad och feltyp timmar senare — om de inte faktiskt läst det noga. Nils hade undersökt felet mer än han medgav.

---

---

## 16. Honeypoten

En man hittades medvetslös utanför serverrummet klockan 02 på natten. Hans hand var skadad och ett USB-minne låg bredvid honom. När säkerhetschefen anlände fördes mannen direkt till polisen.

*Varför arresterades han omedelbart?*

> **Svar:** Mannen var en angripare som försökt köra ett exploit via USB-minnet. Systemet var ett honeypot — det såg ut som ett vanligt nätverk men varje anslutning triggade ett motangrepp som kraschade angriparens egna verktyg och stack en process som hängde sig hårt i OS:et. USB-minnet innehöll hans egna verktyg, hans fingeravtryck satt på det, och loggarna visade exakt vad han försökt göra. Han hade lämnat beviset för sin egen attack på plats.

---

## 17. Loggfilen

En server hade kraschad och ingen förstod varför. På skärmen spelades en loggfil upp automatiskt — rad för rad scrollade den igenom systemhändelserna från kvällen innan, avslutades med ett enda felmeddelande och stannade.

"Den måste ha startat automatiskt vid uppstart," sa driftchefen. "Inget konstigt."

Fordney tittade på skärmen en stund.

"Loggfilen startade inte automatiskt," sa han.

*Varför inte?*

> **Svar:** Loggfilen *spelades upp* — det vill säga någon hade öppnat den och scrollat igenom den. En loggfil startar inte och scrollar sig själv vid en systemrestart. Någon hade suttit vid datorn efter kraschen, läst igenom loggen, och glömt att stänga fönstret. Den personen visste redan vad som hade hänt — och hade inte berättat det.

---

## 18. On-call-drömmen

Företaget hade en kritisk incident mitt i natten. Systemet gick ner klockan 03:47. On-call-utvecklaren — vars jobb var att övervaka larmsystemet — hörde inte av sig förrän klockan 06:15.

"Jag fick ingen avisering," sa han. "Inget SMS, inget mail, ingenting."

Loggarna bekräftade: aviseringen skickades klockan 03:47.

Chefen tackade honom för att han berättat om felet — och sparkade honom sedan.

*Varför?*

> **Svar:** On-call-vakten ska vara vaken och *aktivt övervaka* systemet under sin jour. Att inte reagera på en avisering i drygt två timmar — oavsett om det var tekniskt fel eller att han sov — betyder att han inte skötte sitt jobb. Om aviseringen faktiskt inte kom fram hade hans ansvar varit att ha en backup-rutin. Anledningen till att han sparkades: han var on-call, systemet brann ner, och han dök upp två timmar senare. Det spelar ingen roll varför.

---

## 19. Mörkret vid skärmen

"Jag var inte inloggad," sa Sara. "Hela nätverket låg nere hos mig. Inga lampor, inget wifi, ingenting. Jag satt i totalt mörker och kunde inte se ett dugg."

"Och ändå," sa Fordney, "såg du att det var ett syntaxfel på rad 112."

Sara hejdade sig.

*Hur såg hon det?*

> **Svar:** Om hela nätverket låg nere och det var totalt mörker — hur lyste skärmen? En skärm som är på ger ifrån sig ljus. Sara satt inte i totalt mörker. Skärmen var på, IDE:n var öppen, och hon hade kunnat se koden — och felet. Hennes påstående om att hon "inte kunde se ett dugg" var antingen en överdrift eller en lögn.

---

---

## 20. Katastrofen som gladde henne

Teamets staging-server kraschade. Databasen wipad. CI-pipelinen körde fel script och raderade tre veckors byggartefakter. Infrastrukturkostnaden stack iväg med 800 %.

Men Linnea, som ansvarade för övningen, kunde knappt dölja sitt leende.

*Varför var hon lycklig?*

> **Svar:** Det var ett planerat disaster recovery-test. Allt som gick fel var tänkt att gå fel — det var hela poängen. Att servern kraschade, databasen wipad och larmen gick var *bevis på att övervakningssystemet fungerade*. Linnea hade byggt det. Allt gick precis som det skulle.

---

## 21. De som rusade

Fem utvecklare jobbade mot samma deadline på en fredag eftermiddag. Fyra av dem rusade att pusha sina commits i sista minuten. Alla fyra introducerade merge-konflikter eller buggar som bröt bygget.

Den femte satt still och pushade ingenting. Hennes kod var felfri.

*Hur kom det sig?*

> **Svar:** Hon hade pushat sina ändringar redan på torsdagen. De andra fyra hade arbetat mot en gammal version av branchen — varje gång de rusade att pusha utan att pullas senaste main introducerade de konflikter. Hon behövde inte göra något för att vinna. Hon hade redan gjort rätt.

---

## 22. Mentorn

Under en incident response för ett allvarligt produktionshaveri träffade juniorn Tova en seniorkonsult hon aldrig sett förut. Han löste problemet på fyrtio minuter. Hon frågade aldrig om hans namn eller kontaktuppgifter.

En vecka senare introducerade Tova en kritisk bugg i betalningssystemet — medvetet.

*Varför?*

> **Svar:** Hon hoppades att samma konsult skulle kallas in igen. Incident response på betalningssystemet var hans specialitet — det var därför han hade dykt upp förra gången. Tova ville träffa honom igen, och ett nytt haveri i samma system var det säkraste sättet att säkerställa det.

---

## 23. Det 53:e testet

I ett klassrum: en dator, ett whiteboard med 53 enhetstester listade, tre utvecklare. En av dem blev avskedad på fläcken.

*Vad avslöjade det 53:e testet?*

> **Svar:** En normal testsvit för modulen hade 52 tester. Det 53:e testet existerade inte i repot — det var gömt, kört lokalt, och designat för att alltid returnera `true` oavsett input. Utvecklaren hade manipulerat sin kodtäckning för att nå 100 % utan att faktiskt testa sin kod. Det 53:e testet var bluffen.

---

## 24. Förlorade utan att committa

I en intern code review-tävling förlorade en av utvecklarna utan att ha skrivit en enda rad kod.

*Hur?*

> **Svar:** Han öppnade en pull request men commitade aldrig någonting till den. GitHub auto-stänger inaktiva PRs efter 60 dagar. Hans PR stängdes automatiskt — han hade förlorat tävlingen utan att ha deltagit alls. (Baserad på Fischer vs Spassky, VM 1972 — Fischer forfeitade match 2 utan att spela ett enda drag.)

---

## 25. Utan adress

Säkerhetsteamet ringde vakthavande utvecklaren. "Vi har hittat en kritisk sårbarhet. Gå in och kontrollera betalningstjänsten omedelbart."

Utvecklaren lade på och loggade in direkt på rätt microservice — utan att fråga vilken av de tolv betalningstjänsterna det gällde.

Säkerhetsteamet flaggade honom som misstänkt källa till intrånget.

*Varför?*

> **Svar:** Säkerhetsteamet hade med avsikt inte sagt vilken av de tolv betalningstjänsterna som var drabbad. En oskyldig utvecklare hade frågat — eller gissat fel. Den som gick direkt till rätt tjänst utan att tveka visste redan vad som hade hänt. Och den enda som visste det var den som hade introducerat sårbarheten.

---

---

## 26. För stor för pipelinen

En CI-pipeline vägrade köra ett bygge. Docker-imagen var 1 MB för stor för registrets gräns. Utvecklaren hade ingen tid att refaktorera koden — men hittade ändå en lösning på under en minut.

*Vad gjorde hon?*

> **Svar:** Hon bytte bas-image. Alpine Linux-varianten av samma runtime väger 5–8 MB istället för 200+ MB. Precis som lastbilschauffören som töjde på däcken — inte ändrade lasten — justerade hon behållaren, inte innehållet.

---

## 27. 1988 commits

Varför är 1988 commits värda mer än 1983 commits?

*Tänk efter innan du svarar.*

> **Svar:** Av samma anledning som 20 kronor är mer än 15 kronor. 1988 och 1983 är antalet commits — inte det år de skapades.

---

## 28. Tack för code reviewen

En junior bad sin tech lead om hjälp med en bugg hon fastnat på hela dagen. Tech lead:en tittade inte på buggen — hon vände sig om och sa istället: "Visa mig hela din klassdesign."

Juniorn suckade, visade designen, fick den sönderplockad i fem minuter, och gick tillbaka till skrivbordet.

En stund senare kom hon tillbaka och tackade tech lead:en varmt.

*Varför?*

> **Svar:** Hon hade fastnat i ett mentalt spår. Att behöva förklara och försvara sin design ur ett helt annat perspektiv bröt loopen — och när hon gick tillbaka till koden såg hon buggen direkt. Tech lead:en hade inte löst buggen. Hon hade löst blockeringen.

---

## 29. Vem sitter fast?

En junior och en senior satt i ett videomöte för att gå igenom den juniors kod. När mötet var slut var det seniorn som fick sitta kvar och jobba ikapp.

*Hur?*

> **Svar:** Det var seniorn som hade begärt mötet — för att förstå en del av systemet som juniorn hade byggt och som seniorn inte förstod. Juniorn hade inte blivit granskad. Det var seniorn som hade fått en genomgång. Juniorn gick hem i tid.

---

## 30. Kill, Hang, Deploy

En ops-tekniker stoppade en process, packade in den i en container, och satte den i produktion. Processen körde felfritt resten av kvällen.

*Vad hände egentligen?*

> **Svar:** Hon "sköt" processen (kill), "hängde" den (la den i en Dockerfile och byggde imagen), och "middagen" var produktionsdriftsättningen. Ingen dog. Det var ett vanligt deploys-flöde.

---

## 31. Tre feltyper

Du är en utvecklare som debuggar ett system med tre kända problem:
- Bakom dörr 1: en kritisk minnesläcka som sakta dödar servern.
- Bakom dörr 2: ett race condition som kraschar systemet slumpmässigt.
- Bakom dörr 3: en bugg som inte har triggat på tre år.

Vilken dörr öppnar du sist?

> **Svar:** Dörr 3. En bugg som inte triggat på tre år är förmodligen i kod som inte längre körs — dead code. Den är ingen omedelbar fara. Minnesläckan och race condition:et är aktiva och pågående problem. Öppna dem först.

---

## 32. Sex processer

Sex processer startades i ett system på en måndag. Alpha, Beta och Gamma läste in en konfigurationsfil. Deploy anslöt och de körde tillsammans. Process crashed och rapporterade felet. Bara en process avslutades utan fel.

*Vilken — och varför?*

> **Svar:** De sex processerna heter Alpha, Beta, Gamma, Deploy, Crashed och Monday. Alpha, Beta, Gamma och Deploy läste in konfigurationsfilen och körde — och kraschade. Crashed rapporterade om felet. Monday — processen döpt till Monday — hade aldrig startats som en del av flödet, och avslutades utan fel.

---

## 33. Välj rätt bibliotek

Du ska välja ett open source-bibliotek för ett nytt projekt. Du hittar två alternativ som gör exakt samma sak:

**Bibliotek A:** Perfekt dokumentation, snygg README, konsekvent API, noll öppna issues på GitHub.

**Bibliotek B:** Ojämn dokumentation, 200 öppna issues, ett aktivt och ibland stökigt diskussionsforum.

Vilket väljer du för produktionskod?

> **Svar:** Antagligen Bibliotek B. Noll öppna issues kan betyda att ingen använder det och att inga buggar har hittats än. 200 öppna issues och ett aktivt forum tyder på en stor användarbas, engagerade underhållare och att problemen faktiskt rapporteras — inte att de döljs. Det stökiga är ett tecken på liv.

---

*Fler mysteries läggs till löpande.*
*Källor:*
- *H.A. Ripley, Minute Mysteries (1932) — public domain — https://www.gutenberg.org/files/50603/50603-h/50603-h.htm*
- *Lateral thinking puzzles — https://www.puzzleprime.com/category/brain-teasers/lateral/*
