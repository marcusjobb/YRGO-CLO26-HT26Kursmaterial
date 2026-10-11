# Reflektion

**Namn:**  Max Svensson\
**Kurs:**  Grundläggande OOP i C#, CLO26, Grundläggande objektorienterad\
**Uppgift:**  Skogsäventyret & Skogsäventyret_utan_effekter\
**Datum:**  2026-09-24

---

> Den här reflektionen är obligatorisk och lämnas in individuellt — även om uppgiften gjordes i grupp.  
> Det finns inga rätta eller fel svar. Skriv vad du faktiskt tyckte och upplevde.

---

## Vad var svårast att lösa?

*Var fastnade du? Vad tog längre tid än du trodde — och hur kom du vidare?*

> (program.cs) jag fastnade lite på att skicka över metoder från filerna. det jag menar då är tex att i striden som david skrev så \
> är det bool monsterDog = fiende.TakeDamage(spelare.Attack); att para ihop vad som händer och vem som gör vad. men efter \
> att david skrev anfall så kunde jag göra försvar och spring/fly.
> 
> jag fastnade även på att göra fienderna random. jag löste det genom att fråga ai om vägledning och den gav mig hur jag \
> skrev random och löste resen genom att översätta vad spring/fly faktiskt ska göra.
>
> Annars löste det mest av sig själv genom att köra spelet och se vart saker lade sig och flytta runt lite.\
> kolla buggar om du lyckades ha negativt hp och klura ut att "detta skulle ju hända" och skriva om.
> 
> (player.cs) tyckte det var intressant med xp tröskel vilket jag inte visste hur man gjorde och ai gav mig lite vägledning och \
> och hur jag skulle finskriva koden efter mina tragiska försök. men mycket trial and error, flytta runt och skriva om.

---

## Hur fungerade samarbetet i gruppen?

*Vad fungerade bra? Vad var svårt? Hur delade ni upp arbetet?*  
*(Individuell uppgift? Hoppa över den här.)*

> samarbetet funkade jättebra! vi hade kontuernerlig kontakt via discord och kunde ringa / skärmdela vilket var toppen\
> vi delade upp det lite allt eftersom vad vi kom på. vi skrev hela tiden att jag har pushat nu så dra en pull \
> jag började skapa själva spelare.cs och skapade halva monster.cs då david var hos läkaren. tror jag satt \
> 8 timmar första dagen och bara skrev.
> 
> vi hoppade runt lite och skrev lite här och där och hade bara kontuernerlig kontakt för att inte skapa problem.
> 

---

## Om du fick göra om det — vad hade du gjort annorlunda?

*Tänk på din lösning, din struktur, eller hur ni jobbade. Vad skulle du ändra?*

> Denna gång fick vi ett tomt dokument! det gav lite mer fria ramar att köra på. \
> jag ska vara ärlig och säga att första delen (G-delen) av projektet gick som på räls. jag fattade allt och bockade av \
> rad efter rad på github kriterierna. Sen efter vi hade ett antal commits så började det spåra lite. \
> jag kan inte stå bakom all kod. jag har inte ens rört en rad där alla VG implementationer är skrivna. \
> jag kan inte heller säga att jag exakt vet vad alla effekter gör utom att jag har spelat spelet och sett det visuellt \
> därför skapade jag en mer tydlig och simplare version (jag sa detta till david) där det inte är så mycket kod som jag \
> inte förstår. alla effekter är jätte häftiga och sådär men när jag läser koden kan jag inte förklara varför det står så. \
> arena, shoppen, myntdropp är också en element jag inte har rört utan david körde på. jag kan mer säga att jag står bakom \
> koden i Skogsäventyret_utan_effekter. där jag vet vad koden gör och lite mer tydlig för mig.
