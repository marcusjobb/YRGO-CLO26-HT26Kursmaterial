# Träningsuppgifter: Training: Docker Basics

🔴


## Instruktioner

Välj det bästa svaret för varje fråga. Varje fråga har flera alternativ där ett är korrekt. 
Klicka på 'Visa svar' för att se det rätta svaret och förklaringar för alla alternativ.

### Fråga 1

Vad är Docker?

a. Ett databassystem för att lagra bilder<br>
b. Ett virtual machine system som kräver full OS-installation<br>
c. En containerplattform som paketerar applikationer med sina dependencies<br>
d. Som en IKEA-låda - packar ihop allt du behöver så det fungerar överallt<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En containerplattform som paketerar applikationer med sina dependencies


  **Förklaringar:**

  - ❌ **a) Ett databassystem för att lagra bilder** - FEL: Docker hanterar containers, inte databaser (även om du kan köra databaser i containers)
  - ❌ **b) Ett virtual machine system som kräver full OS-installation** - FEL: Docker använder containers som delar host-OS kernel, inte full VM
  - ✅ **c) En containerplattform som paketerar applikationer med sina dependencies** - **RÄTT**: Docker skapar isolerade containers som innehåller allt en applikation behöver för att köra
  - ❌ **d) Som en IKEA-låda - packar ihop allt du behöver så det fungerar överallt** - FEL: Faktiskt en bra analogi! Docker packar applikationer som fungerar överallt, men det officiella svaret är 'containerplattform'
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 2

Vad är skillnaden mellan en Docker image och en container?

a. Image och container är samma sak<br>
b. Image sparas i databasen, container sparas på disk<br>
c. Image är receptet, container är kakan du bakar<br>
d. Image är en mall/blueprint, container är en körande instans av imagen<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Image är en mall/blueprint, container är en körande instans av imagen


  **Förklaringar:**

  - ❌ **a) Image och container är samma sak** - FEL: De är olika - image är mallen, container är den körande instansen
  - ❌ **b) Image sparas i databasen, container sparas på disk** - FEL: Båda hanteras av Docker, inte av databaser
  - ❌ **c) Image är receptet, container är kakan du bakar** - FEL: Perfekt analogi! Men rätt svar är 'mall/blueprint vs körande instans'
  - ✅ **d) Image är en mall/blueprint, container är en körande instans av imagen** - **RÄTT**: En image är read-only mall, en container är en körande instans med egen state
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 3

Vilket kommando startar en MySQL-container?

a. yolo<br>
b. exec<br>
c. start<br>
d. run<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** run


  **Förklaringar:**

  - ❌ **a) yolo** - FEL: Du tänker på 'docker yolo' - finns tyvärr inte (än)
  - ❌ **b) exec** - FEL: exec kör kommandon INUTI en redan körande container
  - ❌ **c) start** - FEL: start används för att starta en REDAN SKAPAD container, inte för att skapa ny
  - ✅ **d) run** - **RÄTT**: docker run skapar och startar en ny container från en image
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 4

Vad betyder -d flaggan i 'docker run -d'?

a. Debug mode - visar extra information<br>
b. Delete mode - raderar gamla containers<br>
c. Darth Vader mode - containers kör på dark side<br>
d. Detached mode - kör container i bakgrunden<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Detached mode - kör container i bakgrunden


  **Förklaringar:**

  - ❌ **a) Debug mode - visar extra information** - FEL: -d står för detached, inte debug
  - ❌ **b) Delete mode - raderar gamla containers** - FEL: -d är detached mode, för att radera används --rm
  - ❌ **c) Darth Vader mode - containers kör på dark side** - FEL: The Force is strong with this one, men -d står för detached
  - ✅ **d) Detached mode - kör container i bakgrunden** - **RÄTT**: -d (detached) kör containern i bakgrunden så du får tillbaka terminalen
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 5

Vad gör -p flaggan i 'docker run -p 3306:3306'?

a. Port mapping - mappar host-port till container-port<br>
b. Pizza mode - levererar data extra snabbt<br>
c. Password - sätter lösenord för databasen<br>
d. Permissions - sätter access permissions<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Port mapping - mappar host-port till container-port


  **Förklaringar:**

  - ✅ **a) Port mapping - mappar host-port till container-port** - **RÄTT**: -p 3306:3306 mappar host:container så du kan nå MySQL på localhost:3306
  - ❌ **b) Pizza mode - levererar data extra snabbt** - FEL: Önsketänkande! -p står för port, inte pizza
  - ❌ **c) Password - sätter lösenord för databasen** - FEL: -p är port mapping, lösenord sätts med -e MYSQL_ROOT_PASSWORD
  - ❌ **d) Permissions - sätter access permissions** - FEL: -p är port mapping, inte permissions
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 6

Vad gör -e flaggan i 'docker run -e MYSQL_ROOT_PASSWORD=password'?

a. Environment variable - sätter miljövariabler i containern<br>
b. Execute - kör kommandon i containern<br>
c. Encryption - krypterar databasen<br>
d. E.T. mode - phone home till Docker Hub<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Environment variable - sätter miljövariabler i containern


  **Förklaringar:**

  - ✅ **a) Environment variable - sätter miljövariabler i containern** - **RÄTT**: -e sätter environment variables som containern kan läsa
  - ❌ **b) Execute - kör kommandon i containern** - FEL: -e är environment, för att köra kommandon används 'docker exec'
  - ❌ **c) Encryption - krypterar databasen** - FEL: -e sätter environment variables, inte encryption
  - ❌ **d) E.T. mode - phone home till Docker Hub** - FEL: E.T. would approve, men -e står för environment
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 7

Vilket kommando visar alla körande containers?

a. docker list<br>
b. docker where-is-everyone<br>
c. docker ls<br>
d. docker ps<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** docker ps


  **Förklaringar:**

  - ❌ **a) docker list** - FEL: docker list finns inte, använd 'docker ps'
  - ❌ **b) docker where-is-everyone** - FEL: Bra fråga! Men kommandot är 'docker ps'
  - ❌ **c) docker ls** - FEL: docker ls finns inte, rätt kommando är 'docker ps'
  - ✅ **d) docker ps** - **RÄTT**: docker ps (process status) listar alla körande containers
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 8

Hur visar du ALLA containers (även stoppade)?

a. --even-the-dead-ones<br>
b. --all<br>
c. -s<br>
d. -a<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** -a


  **Förklaringar:**

  - ❌ **a) --even-the-dead-ones** - FEL: Dramatiskt! Men flaggan är -a (all)
  - ❌ **b) --all** - FEL: Fungerar faktiskt, men kortformen är -a
  - ❌ **c) -s** - FEL: -s visar storlek, inte alla containers
  - ✅ **d) -a** - **RÄTT**: -a (all) visar både körande och stoppade containers
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 9

Vilket kommando stoppar en körande container?

a. pause<br>
b. kill<br>
c. chill<br>
d. stop<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** stop


  **Förklaringar:**

  - ❌ **a) pause** - FEL: pause fryser containern temporärt, stop stänger ner den helt
  - ❌ **b) kill** - FEL: kill fungerar men är mer brutal (SIGKILL), stop är graceful (SIGTERM)
  - ❌ **c) chill** - FEL: docker chill vore nice, men kommandot är 'stop'
  - ✅ **d) stop** - **RÄTT**: docker stop skickar SIGTERM och stoppar containern gracefully
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 10

Vad är Docker volumes?

a. Persistent storage som överlever när container tas bort<br>
b. En typ av nätverk mellan containers<br>
c. Ljudnivån på container-loggar<br>
d. Temporär storage som försvinner när container stoppas<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Persistent storage som överlever när container tas bort


  **Förklaringar:**

  - ✅ **a) Persistent storage som överlever när container tas bort** - **RÄTT**: Volumes lagrar data utanför container lifecycle så data inte försvinner
  - ❌ **b) En typ av nätverk mellan containers** - FEL: Nätverk är separata från volumes, volumes är storage
  - ❌ **c) Ljudnivån på container-loggar** - FEL: Turn it up to 11! Men volumes handlar om persistent storage
  - ❌ **d) Temporär storage som försvinner när container stoppas** - FEL: Det är default container storage, volumes är persistent
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 11

Hur mountar du en volume för MySQL data?

a. /secret/batcave<br>
b. /data/mysql<br>
c. /var/lib/mysql<br>
d. /mysql<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** /var/lib/mysql


  **Förklaringar:**

  - ❌ **a) /secret/batcave** - FEL: Batman would approve, men MySQL använder /var/lib/mysql
  - ❌ **b) /data/mysql** - FEL: MySQL's default data directory är /var/lib/mysql
  - ✅ **c) /var/lib/mysql** - **RÄTT**: MySQL lagrar data i /var/lib/mysql inne i containern
  - ❌ **d) /mysql** - FEL: Data ligger i /var/lib/mysql, inte /mysql
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 12

Vilket kommando visar loggar från en container?

a. diary<br>
b. tail<br>
c. log<br>
d. logs<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** logs


  **Förklaringar:**

  - ❌ **a) diary** - FEL: Dear diary... men kommandot är 'docker logs'
  - ❌ **b) tail** - FEL: tail är ett Unix-kommando, inte docker-kommando (använd 'docker logs')
  - ❌ **c) log** - FEL: Kommandot är logs (plural), inte log
  - ✅ **d) logs** - **RÄTT**: docker logs visar stdout/stderr från containern
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 13

Hur följer du loggar i realtid (live tail)?

a. --live<br>
b. --stalker-mode<br>
c. -r<br>
d. -f<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** -f


  **Förklaringar:**

  - ❌ **a) --live** - FEL: Flaggan är -f (follow), inte --live
  - ❌ **b) --stalker-mode** - FEL: Lite creepy! Flaggan är -f (follow)
  - ❌ **c) -r** - FEL: -r finns inte, använd -f för realtid
  - ✅ **d) -f** - **RÄTT**: -f (follow) streamar loggar i realtid, som 'tail -f'
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 14

Vilket kommando kör ett kommando INUTI en körande container?

a. run<br>
b. hack<br>
c. attach<br>
d. exec<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** exec


  **Förklaringar:**

  - ❌ **a) run** - FEL: run skapar NY container, exec kör i BEFINTLIG container
  - ❌ **b) hack** - FEL: Mr. Robot style! Men kommandot är 'exec'
  - ❌ **c) attach** - FEL: attach kopplar till main process, exec startar ny process
  - ✅ **d) exec** - **RÄTT**: docker exec kör kommandon i en redan körande container
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 15

Hur öppnar du ett interaktivt bash-shell i en container?

a. -d<br>
b. -a<br>
c. --matrix-mode<br>
d. -it<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** -it


  **Förklaringar:**

  - ❌ **a) -d** - FEL: -d är detached (bakgrund), inte interaktiv terminal
  - ❌ **b) -a** - FEL: -a är attach, men för interaktiv shell behövs -it
  - ❌ **c) --matrix-mode** - FEL: Neo would use -it (interactive + tty)
  - ✅ **d) -it** - **RÄTT**: -i (interactive) + -t (tty) ger interaktiv terminal
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 16

Vad är docker-compose?

a. Ett verktyg för att skriva Dockerfile<br>
b. Mozart för containers<br>
c. Ett verktyg för att definiera och köra multi-container applikationer<br>
d. Ett verktyg för att komponera musik i Docker<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Ett verktyg för att definiera och köra multi-container applikationer


  **Förklaringar:**

  - ❌ **a) Ett verktyg för att skriva Dockerfile** - FEL: docker-compose är för multi-container apps, inte för att skriva Dockerfile
  - ❌ **b) Mozart för containers** - FEL: Container Symphony! Men docker-compose är för multi-container orchestration
  - ✅ **c) Ett verktyg för att definiera och köra multi-container applikationer** - **RÄTT**: docker-compose använder YAML-fil för att konfigurera flera containers samtidigt
  - ❌ **d) Ett verktyg för att komponera musik i Docker** - FEL: Inte musik, utan multi-container orchestration
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 17

Vilken fil använder docker-compose för konfiguration?

a. docker-compose.yml<br>
b. docker-orchestra.yml<br>
c. compose.json<br>
d. Dockerfile<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** docker-compose.yml


  **Förklaringar:**

  - ✅ **a) docker-compose.yml** - **RÄTT**: docker-compose.yml (eller .yaml) definierar services, networks, volumes
  - ❌ **b) docker-orchestra.yml** - FEL: Musikaliskt! Men filen heter docker-compose.yml
  - ❌ **c) compose.json** - FEL: Docker Compose använder YAML-format, inte JSON
  - ❌ **d) Dockerfile** - FEL: Dockerfile bygger images, docker-compose.yml konfigurerar multi-container setup
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 18

Vilket kommando startar alla services i docker-compose.yml?

a. gogoGO<br>
b. run<br>
c. up<br>
d. start<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** up


  **Förklaringar:**

  - ❌ **a) gogoGO** - FEL: Enthusiastiskt! Men kommandot är 'up'
  - ❌ **b) run** - FEL: run kör EN specific service, up startar ALLA
  - ✅ **c) up** - **RÄTT**: docker-compose up startar alla definierade services
  - ❌ **d) start** - FEL: start kör BEFINTLIGA containers, up skapar OCH startar
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 19

Hur startar du docker-compose i bakgrunden?

a. --background<br>
b. -b<br>
c. -d<br>
d. --ninja-mode<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** -d


  **Förklaringar:**

  - ❌ **a) --background** - FEL: Flaggan är -d, inte --background
  - ❌ **b) -b** - FEL: Flaggan är -d (detached), inte -b
  - ✅ **c) -d** - **RÄTT**: -d (detached) kör services i bakgrunden
  - ❌ **d) --ninja-mode** - FEL: Stealthy! Men flaggan är -d (detached)
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 20

Vilket kommando stoppar och tar bort alla docker-compose containers?

a. down<br>
b. stop<br>
c. rm<br>
d. bye-felicia<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** down


  **Förklaringar:**

  - ✅ **a) down** - **RÄTT**: docker-compose down stoppar och tar bort containers, networks (men inte volumes)
  - ❌ **b) stop** - FEL: stop stannar bara containers, down stannar OCH tar bort
  - ❌ **c) rm** - FEL: rm tar bort stoppade containers, down stoppar OCH tar bort
  - ❌ **d) bye-felicia** - FEL: Classic! Men kommandot är 'down'
</details>


## Sammanfattning

Du har nu genomgått 20 träningsfrågor om training: docker basics. 
Dessa frågor täcker viktiga koncept som du behöver känna till för att lyckas i kursen.


**Tips för fortsatt lärande:**

- Gå igenom frågorna igen om du hade svårt med några

- Testa att skriva egen kod för att förstärka koncepten

- Diskutera svåra frågor med klasskamrater eller lärare
