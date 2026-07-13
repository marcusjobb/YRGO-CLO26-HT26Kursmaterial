# Introduktion till containers

## Filen som slutade fungera

Du har byggt en app. Den fungerar på din maskin. Du pushar koden, din kollega klonar repot — och ingenting fungerar. Fel .NET-version. Saknade beroenden. En miljövariabel som du aldrig tänkte dokumentera. Klassikern.

Det här är inte ett konstigt kantfall. Det är det normala tillståndet för mjukvaruutveckling — fram till att containers löste det.

## Vad är egentligen en container?

En container är en isolerad process på din dator. Den ser sitt eget filsystem, sitt eget nätverk, sina egna processer — men delar faktiskt operativsystemets kärna (kernel) med allt annat som körs på maskinen.

Det skiljer sig fundamentalt från en virtuell maskin. En VM är som att ha en dator inuti datorn — ett eget operativsystem, ett eget minne, en egen processor som hypervisorn delar ut. Det tar minuter att starta, kostar gigabyte på disk, och kräver att du underhåller ett helt OS.

En container är inte en mini-dator. Den är mer som ett rum i ett hus: egna väggar, eget lås, men du delar fortfarande värmesystem och elinstallation med resten av byggnaden. Det gör den extremt lätt — megabyte istället för gigabyte, sekunder istället för minuter.

Tekniken under huven heter Linux namespaces och cgroups. Namespaces ger containern sin isolation — den ser inte vad som finns utanför sina egna väggar. Cgroups begränsar hur mycket CPU och minne containern får använda. Du behöver inte memorera dessa begrepp, men det är bra att veta att containers inte är magi — de är Linux-funktioner som Docker paketerade på ett sätt som folk faktiskt ville använda.

## Image och container — klass och objekt

Innan du kan köra en container behöver du en image. Tänk på det precis som relationen mellan en klass och ett objekt i C#.

En image är ritningen. Den är oföränderlig, komprimerad, och innehåller allt som applikationen behöver: rätt .NET-version, beroenden, konfigurationsfiler, och din kod. Du skapar den en gång, och sedan kan du köra den exakt likadant varhelst den hamnar — din laptop, din kollegas laptop, en Azure-tjänst i Irland.

En container är instansen. Du startar en image och får en levande, körande process. Du kan köra tio containers från samma image samtidigt, precis som du kan skapa tio objekt från samma klass. Stänger du ner en container är den borta — men imagen finns kvar.

Images lagras i ett register. Docker Hub är det vanligaste, ungefär som npm eller NuGet fast för containers. Azure Container Registry är det du kommer använda i kursen — ett privat register kopplat till ditt Azure-konto.

## Varför Docker vann

Containers som koncept är äldre än Docker. Linux har haft liknande mekanismer sedan 2008. Men det var Docker 2013 som gjorde det tillgängligt för vanliga utvecklare.

Docker löste tre saker på en gång. Det gav en enkel syntax för att beskriva hur en image byggs (Dockerfile), ett standardiserat format för att paketera och distribuera images, och ett CLI som faktiskt gick att använda utan att vara Linux-expert. Plötsligt kunde en .NET-utvecklare på Windows bygga en container, pusha den till ett register, och köra den på en Linux-server i molnet — utan att förstå varje lager under huven.

Det finns alternativ i dag — Podman, containerd, och andra — men Docker satte standarden och terminologin som hela branschen använder.

## Containers och microservices

Du kommer höra dessa ord tillsammans ofta, och det är inte av en slump. Microservices är ett sätt att strukturera en applikation som många små, fristående tjänster — en tjänst för autentisering, en för betalningar, en för notifikationer — istället för en stor monolit.

Containers är inte ett krav för microservices, men de passar varandra extremt bra. Varje tjänst kan paketeras som sin egen image med sina egna beroenden, utan att behöva kompromissa med resten. Skalning blir enkelt: behöver betalingstjänsten hantera mer trafik kör du bara fler containers av den imagen, utan att röra något annat.

Det är också precis den här kombinationen som gjort Kubernetes så central i modern molnarkitektur — men det är nästa kapitel.
