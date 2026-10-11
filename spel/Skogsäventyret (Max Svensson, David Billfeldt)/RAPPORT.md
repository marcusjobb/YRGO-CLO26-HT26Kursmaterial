# Rapport

**Kurs:** Grundläggande OOP i C#  \
**Uppgift:**  Skogsäventyret\
**Grupp:**  team Lando Calrisian\
**Datum:**  24-9-2026\
**GitHub:** https://github.com/Maxswe99/Skogsaventyret

---

## Gruppmedlemmar

| Namn | Lämnade in |
|------|-----------|
| [Max] | Zip + RAPPORT.md + REFLEKTION.md |
| [David] | REFLEKTION.md |

---

## G — Hur vi löste uppgiften

> Förklara kortfattat hur du löste varje G-krav. En till tre meningar per punkt räcker.  
> Skriv med egna ord — kopiera inte uppgiftsbeskrivningen.

### Klasserna
Player håller spelarens HP, attack, XP, level, kaffepoletter och aktiva vapen. Monster är en abstrakt basklass med subklasserna Mellanchef (Robert), Flatearther (Foliehatt) och Karen. Weapon är en abstrakt basklass med subklasserna Blyertspenna, Laptop och HdmiKabel, och Shop är cafeterian där man köper vapen.
>

### Metoderna
Klasserna har egna metoder för sitt beteende, till exempel TakeDamage, Heal, GainXp, LevelUp och KöpVapen i Player och Attackerar och SläppVapen i Monster. Subklasserna overridar Attackerar för att få egna repliker och SkapaVapen för att bestämma vilket vapen de tappar.
>

### Main()
Program.cs skapar spelaren och kör en loop med en meny: slåss, fikarummet, cafeterian eller arenan. Själva striden ligger i en egen metod, Strid(), så att både vanliga möten och arenan kan använda den.
>

### Git
Committat så ofta vi kommer ihåg på github
>

### Kodkvalitet
Vi använder private set så att andra klasser inte kan ändra värden direkt, och abstrakta basklasser så att man inte råkar skapa ett "tomt" monster eller vapen. Koden är uppdelad i en fil per klass, och kommentarerna visar var vi har fått inspiration av AI.
>

---

## VG — Motivering

> Fyll i det här avsnittet om du siktar på VG. Lämna tomt = G-bedömning.

### Vad vi lade till
Kritiska träffar (10 % chans att göra dubbel skada) för både spelaren och monstren, kaffepoletter som valuta och vapen som monster kan tappa med olika sannolikhet. Vi lade också till arenan: sju monster sorteras från svagast till starkast med OrderBy, och klarar man alla får man en bonus på 150 XP. Till sist lade vi till visuella effekter för striderna i StridsEffekter, som HP-mätare och animationer.
>

### Varför vi löste det såhär
Sortimentet i shoppen är en List<Weapon> eftersom ordningen spelar roll (billigast först) och spelaren väljer med en siffra som direkt blir ett index. En Dictionary hade passat bättre om man sökte upp vapen efter namn, men det gör man aldrig i spelet. Vi flyttade striden till en egen metod för att slippa ha samma kod två gånger, en för vanliga strider och en för arenan.
>

---

## Git-logg

Klistra in utskriften från `git log --oneline` här:

```
e6b1ce8 (HEAD -> main, origin/main, origin/HEAD) ändrade lite prompter
0c9873e fixade lite prompts + lade till att det faktiskt kostar en dag att gå i strid. lade även till att den checkar ditt up om du flyr och tar tillräckligt med hp för att dö
04046ea fixade en bugg som tillät spelaren att fortsätta om du hade 0 hp efter du flydde
9af944a ändrade en prompt
3896bc9 ändrade en prompt
827b745 lade till rapport och reflektion
321d714 tog bort preset name
0962a11 tog bort private och lade till user får välja spelar namn
6218cc5 ändrade så spelare.Namn inte är privat då user väljer användarnamn.
bc9797c visade fel total xp efter dödad fiende
8fadf56 Flyttade ut mappen ur dubbelmappen och städade strukturen
66ba532 Rensade den sista idea-mappen
f1e532e Rensar bort alla .idea-mappar
c01a95e Rensar alla .idea-mappar och uppdaterar gitignore
e12943b Tar bort .idea och lägger till mappen i gitignore
bba6404 Gör om Skogsaventyret_utan_effekter till en vanlig mapp
dc28210 Merge branch 'main' of https://github.com/Maxswe99/Skogsaventyret
ba75870 Lägger till båda projekten: Skogsäventyret och Skogsäventyret utan effekter
dccdefd Tog bort rapport och feflektion då de ska skicas in separat.
4e2f331 Lade till reflektion för David.B
466556f Lade till Rapport för David.B
d1764c6 Merge remote-tracking branch 'origin/main'
0d78cbf Lade till mall för rapport och reflektion
c17345d anropade level upp effekter och flyttade kod
081b3d1 tog bort överflödig kod + effekter
01529fb ändrade lite prompter
0ae8be6 Fixade att vapen blev dyrae och mer rare som monsterdrop.
8b4faeb Lade till en cafeteria där man kan köpa vapen.
3ac6677 Fixade med Weapons.cs/DB
90c24c0 Lade till Weapon.cs med 3 vapen ocg drop chans
379b771 Fixade hur att den displayar hur många Kaffe Poletter du får efter kill.
f89d896 Fixade att fienderna droppa kaffepolätter när de dör.
9c69ea8 fixade meddelanden när man critar så man tydligare ser.
192251f fixade så man kan göra crit dmg 10% chans.
5415dda Lite ändringar i Program.cs för att förska fixa visaulals.
093e1d3 Färdig med StridsEffekter.cs med lite ai hjälp för visuals.
b714d46 Forsätter bygga på StridsEffekter.cs med lite visuals för skojs skull. med lite ai hjälp eftersom det ändå inte ingår i betyget hur det ser ut
b67e7bf Börjar bygga StridsEffekter.cs
cb2bed2 flytta  runt kod för att det skulle "make sense"
a8fe967 tog bort överflödiga kommentarer.
beba1cc Lade till att om du dör när du flyr att det går till game over.
719b4f7 kommenterade bort rad som skapade bugg som skippade dubbelräkning av dagar ibland.
4ecbb47 Ändrade namnet på alternativ tre till fly istället för spring.
541887b Lade till alternativ 3 att fly.
3e3c2ed lade till en else sats under strid för att inte utforska buggar under redovisningen haha
e990840 lade till game over utanför stiden så jag även checkas om jag dör när jag försvarar mig. rensade bool kod som inte behövdes.
970ab0e flyttade spelare.DagarÖverlevda++; till metoden fikarummet istället. dubblering av if sats
a545334 flyttade metoden fiendeskrivutnamn till monster för att den inte gick att anropa till monster.cs så la den där istället och andropade den till program.cs genom att lägga till fiende.fiendeskrivutnamn
4862004 skapade en metod som ersätter DRY (SkrivUtFiendeNamn)
eaec7de lade till färg på fienderna baserat på rarity "darkyellow är legendary monster" "dark blue är rare monster" "green är common"
1965d87 lade till kommentarer +´försök till ett försvar i inre loopen strid
0a60bfd lade val 2 else utanför inre if satsen. rad 117.
105b523 uppdaterade så loopen inte bara returnar utan faktiskt att alive = false;
883464c ändrade kommentarer på switch sattsen
c376d71 lade till totalxp
30e87e3 Ändrade oddsen på att Karen spawnar
ade2ed3 Merge remote-tracking branch 'origin/main'
b3a46e9 Ändrade HP på monsterna
f986eff glömde en måsvinge
39e30aa vi pushade typ samtidigt så detta är bara uppdatering från vad jag gjorde. lade till att det kostar 1 dag att använda heal (elexir) lade till kommentarer
35affaa Merge branch 'main' of https://github.com/Maxswe99/Skogsaventyret
b100999 Ändrade från HP till Energi
44d7aa8 Merge branch 'main' of https://github.com/Maxswe99/Skogsaventyret
140b9b2 små ändringar plus att jag la till att det kostar OM du blev healad annars inte
0be6e12 Tog bort order energi innan {spelare.Hp} HP
9ba49d9 ändrade så chansen var mindre att möta karen det första som hände då du fick game over direkt.
aa5ba96 nu lekte jag bara med lite färg i prompterna
3586716 tog bort console.writeline i metoden heal. lade även till lite "new lines (\n)". lade även till prolog och ändrade lite prompter i alla .cs filer.
67434c9 lade till att det är random vilket monster som dyker upp. vi märkte att om endast flat earther dyker upp så kunde du spamma 1 i konsolen och levla och bli som one punch man
e6b1ce8 (HEAD -> main, origin/main, origin/HEAD) ändrade lite prompter
0c9873e fixade lite prompts + lade till att det faktiskt kostar en dag att gå i strid. lade även till att den checkar ditt up om du flyr och tar tillräckligt med hp för att dö
04046ea fixade en bugg som tillät spelaren att fortsätta om du hade 0 hp efter du flydde
9af944a ändrade en prompt
3896bc9 ändrade en prompt
827b745 lade till rapport och reflektion
321d714 tog bort preset name
0962a11 tog bort private och lade till user får välja spelar namn
6218cc5 ändrade så spelare.Namn inte är privat då user väljer användarnamn.
bc9797c visade fel total xp efter dödad fiende
8fadf56 Flyttade ut mappen ur dubbelmappen och städade strukturen
66ba532 Rensade den sista idea-mappen
f1e532e Rensar bort alla .idea-mappar
c01a95e Rensar alla .idea-mappar och uppdaterar gitignore
e12943b Tar bort .idea och lägger till mappen i gitignore
bba6404 Gör om Skogsaventyret_utan_effekter till en vanlig mapp
dc28210 Merge branch 'main' of https://github.com/Maxswe99/Skogsaventyret
ba75870 Lägger till båda projekten: Skogsäventyret och Skogsäventyret utan effekter
dccdefd Tog bort rapport och feflektion då de ska skicas in separat.
4e2f331 Lade till reflektion för David.B
466556f Lade till Rapport för David.B
466556f Lade till Rapport för David.B
d1764c6 Merge remote-tracking branch 'origin/main'
0d78cbf Lade till mall för rapport och reflektion
c17345d anropade level upp effekter och flyttade kod
081b3d1 tog bort överflödig kod + effekter
01529fb ändrade lite prompter
0ae8be6 Fixade att vapen blev dyrae och mer rare som monsterdrop.
8b4faeb Lade till en cafeteria där man kan köpa vapen.
3ac6677 Fixade med Weapons.cs/DB
90c24c0 Lade till Weapon.cs med 3 vapen ocg drop chans
379b771 Fixade hur att den displayar hur många Kaffe Poletter du får efter kill.
f89d896 Fixade att fienderna droppa kaffepolätter när de dör.
9c69ea8 fixade meddelanden när man critar så man tydligare ser.
192251f fixade så man kan göra crit dmg 10% chans.
5415dda Lite ändringar i Program.cs för att förska fixa visaulals.
093e1d3 Färdig med StridsEffekter.cs med lite ai hjälp för visuals.
b714d46 Forsätter bygga på StridsEffekter.cs med lite visuals för skojs skull. med lite ai hjälp eftersom det ändå inte ingår i betyget hur det ser ut
b67e7bf Börjar bygga StridsEffekter.cs
cb2bed2 flytta  runt kod för att det skulle "make sense"
a8fe967 tog bort överflödiga kommentarer.
beba1cc Lade till att om du dör när du flyr att det går till game over.
719b4f7 kommenterade bort rad som skapade bugg som skippade dubbelräkning av dagar ibland.
4ecbb47 Ändrade namnet på alternativ tre till fly istället för spring.
541887b Lade till alternativ 3 att fly.
3e3c2ed lade till en else sats under strid för att inte utforska buggar under redovisningen haha
e990840 lade till game over utanför stiden så jag även checkas om jag dör när jag försvarar mig. rensade bool kod som inte behövdes.
970ab0e flyttade spelare.DagarÖverlevda++; till metoden fikarummet istället. dubblering av if sats
a545334 flyttade metoden fiendeskrivutnamn till monster för att den inte gick att anropa till monster.cs så la den där istället och andropade den till program.cs genom att lägga till fiende.fiendeskrivutnamn
4862004 skapade en metod som ersätter DRY (SkrivUtFiendeNamn)
eaec7de lade till färg på fienderna baserat på rarity "darkyellow är legendary monster" "dark blue är rare monster" "green är common"
1965d87 lade till kommentarer +´försök till ett försvar i inre loopen strid
0a60bfd lade val 2 else utanför inre if satsen. rad 117.
105b523 uppdaterade så loopen inte bara returnar utan faktiskt att alive = false;
883464c ändrade kommentarer på switch sattsen
c376d71 lade till totalxp
30e87e3 Ändrade oddsen på att Karen spawnar
ade2ed3 Merge remote-tracking branch 'origin/main'
b3a46e9 Ändrade HP på monsterna
f986eff glömde en måsvinge
39e30aa vi pushade typ samtidigt så detta är bara uppdatering från vad jag gjorde. lade till att det kostar 1 dag att använda heal (elexir) lade till kommentarer
35affaa Merge branch 'main' of https://github.com/Maxswe99/Skogsaventyret
b100999 Ändrade från HP till Energi
44d7aa8 Merge branch 'main' of https://github.com/Maxswe99/Skogsaventyret
140b9b2 små ändringar plus att jag la till att det kostar OM du blev healad annars inte
0be6e12 Tog bort order energi innan {spelare.Hp} HP
9ba49d9 ändrade så chansen var mindre att möta karen det första som hände då du fick game over direkt.
aa5ba96 nu lekte jag bara med lite färg i prompterna
3586716 tog bort console.writeline i metoden heal. lade även till lite "new lines (\n)". lade även till prolog och ändrade lite prompter i alla .cs filer.
67434c9 lade till att det är random vilket monster som dyker upp. vi märkte att om endast flat earther dyker upp så kunde du spamma 1 i konsolen och levla och bli som one punch man
717f778 ändrade outputen på hur det såg ut när du levlade stod endast grattis du är nu 2 hahaha
8c59f06 Merge branch 'main' of https://github.com/Maxswe99/Skogsaventyret
e5e2fdd Fixade val mellan att slåss eller att gå till byn med grundläggande combat
961ea51 Merge remote-tracking branch 'origin/main'
0345162 La till möjlighet att vila i staden och få tillbaka HP över natten, samt lade till möjlighet att fightas i skogen.
274e6f7 lade till abstract på bas klassen monster och tog bort själva monstret warwick då han inte ska finnas
3824c1b skapade nya monster i program.cs och lade sub klasserna utanför monster klassen
a243cb9 tog bort överflödiga kommenterarer
3e22863 Ändrade i vad en monstersubklass säger
2b1f474 Lade till 2 till subklasse mer slumpade stats
89dcd87 Lade till subklassen karen som monster med slumpade stats för tilläfllet,
531ddcd La till en attack metod för spelaren.
6c9013a vet inte exakt vad jag ändrade men sista push innan letkionen tog slut
2f2119d uppladdning av monster.cs och lade till TakeDamage metmod
ac336e0 lade till lite statistik när du levlar som konsolen skriver ut
fcd9b95 fixade gainxp och levelup med lite andra små saker. kallar även på metoderna
46906d1 lade till take heal (nästan färdig)
3042569 fixade takedamage. nu drar den av skada från hp och rör inte maxhp!
ba5ff39 flyttade spelare classen till egen fil och lade till en äventyrare (new object)
6c5f5b7 lade till classen player med konstruktor
e54d619 Initial commit
```