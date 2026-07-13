# Övning — Multi-platform Docker-builds

🔴

HyresBil är ett regionalt biluthyrningsföretag som precis containeriserat sin boknings-API. Teamet ser direkt ett problem: alla utvecklare har MacBooks med M-chip (arm64), men Azure-servern i produktion kör x86_64. Imagen som byggdes på Mac vägrar starta på servern.

Din uppgift är att lösa det — en gång för alla. Imagen ska byggas för både `linux/amd64` och `linux/arm64` och laddas upp till Docker Hub som ett manifest. Oavsett om den körs på en Azure VM, ett Graviton-chip i AWS eller en kollegas M3 Mac — ska det bara fungera.

---

## Vad gäller för den här övningen

- [ ] Som utbildare vill jag att du förstår varför CPU-arkitektur spelar roll för Docker-images
- [ ] Som utbildare vill jag att du kan sätta upp en `buildx`-builder med stöd för flera plattformar
- [ ] Som utbildare vill jag att du kan bygga och pusha en multi-platform image i ett kommando
- [ ] Som utbildare vill jag att du kan verifiera att manifestet innehåller rätt plattformar
- [ ] Som utbildare vill jag att du kan förklara när det här spelar roll i ett riktigt produktionsscenario

---

## Steg 1: Förstå varför det spelar roll

Kör det här på din maskin:

```bash
uname -m
docker info --format '{{.Architecture}}'
```

Du ser antingen `x86_64` (Intel/AMD) eller `arm64` / `aarch64` (Apple Silicon, Raspberry Pi).

En Docker-image innehåller kompilerade binärer för en specifik CPU-arkitektur. En image byggd på arm64 innehåller arm64-binärer — de kan inte köra nativt på en x86_64-server. Docker kan emulera via QEMU, men det är långsamt och ger fel i produktion om det ens fungerar.

Det är exakt det HyresBil-teamet stöter på.

### Förväntad output (exempel på arm64-maskin)

```
arm64
aarch64
```

### Förväntad output (exempel på x86_64-maskin)

```
x86_64
x86_64
```

> **Varför det spelar roll i produktion:** Azure Standard-VMs kör `linux/amd64`. AWS Graviton-instanser kör `linux/arm64` och är upp till 40 % billigare för samma prestanda. Azure har egna ARM-VMs (Ampere Altra). Utan multi-platform support låser du dig till en arkitektur — och det märker du när det är fel.

---

## Steg 2: Sätt upp en buildx-builder

Standardbuildern i Docker stödjer bara din egen plattform. Du behöver en ny builder med QEMU-emulering för cross-compilation.

```bash
docker buildx create --name hyresbil-builder --use
docker buildx inspect --bootstrap
```

Kontrollera att outputen visar stöd för båda plattformarna:

```bash
docker buildx ls
```

### Förväntad output (urval)

```
NAME/NODE              DRIVER/ENDPOINT  STATUS   BUILDKIT  PLATFORMS
hyresbil-builder *     docker-container running  v0.x.x    linux/amd64, linux/arm64, ...
default                docker           running             linux/amd64
```

Stjärnan (`*`) visar att `hyresbil-builder` är aktiv.

<details><summary>Tips — vad flaggorna gör</summary>

- `--name hyresbil-builder` — ett läsbart namn så du hittar buildern sen
- `--use` — sätter den här buildern som aktiv för alla `docker buildx build`-kommandon
- `--bootstrap` startar buildern och laddar ner QEMU-binärer för emulering

Om buildern bara visar din native-plattform: starta om Docker Desktop och kör `--bootstrap` igen.

</details>

---

## Steg 3: Bygg och pusha multi-platform imagen

Du behöver en enkel Dockerfile att bygga från. Skapa en temporär katalog:

```bash
mkdir hyresbil-multiplatform && cd hyresbil-multiplatform
```

Skapa en minimal `Dockerfile`:

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine
WORKDIR /app
RUN echo "HyresBil Boknings-API — plattform: $(uname -m)" > /app/platform.txt
CMD ["sh", "-c", "cat /app/platform.txt && sleep infinity"]
```

Bygg och pusha till Docker Hub i ett enda kommando:

```bash
docker buildx build \
  --platform linux/amd64,linux/arm64 \
  -t DITT_DOCKERHUB_ANVÄNDARNAMN/hyresbil-api:1.0 \
  --push \
  .
```

Byt ut `DITT_DOCKERHUB_ANVÄNDARNAMN` mot ditt faktiska Docker Hub-konto. Logga in om du inte redan gjort det:

```bash
docker login
```

Bygget tar längre tid än vanligt — den icke-native plattformen emuleras via QEMU.

### Förväntad output (slutet av bygget)

```
=> pushing manifest for docker.io/DITT_DOCKERHUB_ANVÄNDARNAMN/hyresbil-api:1.0
=> pushing manifest done
```

> **15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.

<details><summary>Tips — varför --push är obligatoriskt</summary>

Multi-platform images existerar inte lokalt. De lagras som ett **manifest list** i registret — ett index som pekar på plattformsspecifika images. Du kan inte köra `docker images` och se dem lokalt. `--push` är inte valfritt, det är hur det fungerar.

Glömmer du `--push` får du felmeddelandet: `docker exporter does not currently support exporting manifest lists`.

</details>

---

## Steg 4: Verifiera manifestet

Kontrollera att Docker Hub verkligen har båda plattformarna:

```bash
docker manifest inspect DITT_DOCKERHUB_ANVÄNDARNAMN/hyresbil-api:1.0
```

Eller med buildx-verktyget (mer läsbar output):

```bash
docker buildx imagetools inspect DITT_DOCKERHUB_ANVÄNDARNAMN/hyresbil-api:1.0
```

### Förväntad output (urval)

```json
{
  "schemaVersion": 2,
  "mediaType": "application/vnd.docker.distribution.manifest.list.v2+json",
  "manifests": [
    {
      "platform": {
        "architecture": "amd64",
        "os": "linux"
      }
    },
    {
      "platform": {
        "architecture": "arm64",
        "os": "linux"
      }
    }
  ]
}
```

Ser du båda `amd64` och `arm64` i manifestet? Då är HyresBil-imagen klar för produktion.

Verifiera också direkt i Docker Hubs webbgränssnitt: `https://hub.docker.com/r/DITT_DOCKERHUB_ANVÄNDARNAMN/hyresbil-api/tags` — taggen ska visa flera OS/Architecture-varianter.

---

## Steg 5: Testa att rätt plattform väljs automatiskt

Kör imagen utan att ange plattform — Docker väljer rätt variant automatiskt:

```bash
docker run --rm DITT_DOCKERHUB_ANVÄNDARNAMN/hyresbil-api:1.0
```

Tvinga sedan fram den andra plattformen:

```bash
docker run --rm --platform linux/amd64 \
  DITT_DOCKERHUB_ANVÄNDARNAMN/hyresbil-api:1.0
```

### Förväntad output (på arm64-maskin, utan --platform)

```
HyresBil Boknings-API — plattform: aarch64
```

### Förväntad output (med --platform linux/amd64)

```
HyresBil Boknings-API — plattform: x86_64
```

Det visar att samma image-tag levererar rätt binärer för rätt CPU. Precis det HyresBil-teamet behövde.

---

## Städa upp

```bash
docker buildx rm hyresbil-builder
docker rm -f $(docker ps -aq) 2>/dev/null
```

---

## Utmaning

Fundera på det här och skriv ner svaret (en kort mening räcker):

**AWS Graviton3-instanser kostar 20 % mindre än motsvarande x86-instanser och kör linux/arm64. HyresBil överväger att migrera sin produktionsmiljö. Vad är det enda de behöver säkerställa i sin Docker-pipeline för att det ska fungera utan ändringar i koden?**

<details><summary>Svar</summary>

De behöver säkerställa att alla images i pipeline byggs och pushas som multi-platform med stöd för `linux/arm64` — antingen via `docker buildx build --platform` lokalt, eller via `docker/build-push-action` i GitHub Actions. Koden ändras inte. Byggsystemet ändras.

</details>

---

*Facit finns hos läraren.*
