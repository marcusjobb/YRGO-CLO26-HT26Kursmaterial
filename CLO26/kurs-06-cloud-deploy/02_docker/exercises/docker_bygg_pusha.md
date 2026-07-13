# Övning — Bygg och pusha HyresBil

🟡

Du har en .NET-app som rullar lokalt. Nu ska du packa in den i en Docker-image, köra den i en container och sen pusha den till Docker Hub. Det är precis det här flödet som används i verkliga projekt — build lokalt, pusha till registry, dra ner och kör var som helst.

Appen heter **HyresBil** och är ett Minimal API i .NET 10.

---

## Det här gäller för övningen

- [ ] Du kan skriva en multi-stage Dockerfile för en .NET-app
- [ ] Du förstår skillnaden mellan `sdk`-imagen och `runtime`-imagen — och varför det spelar roll
- [ ] Du kan bygga, tagga och pusha en image till Docker Hub
- [ ] Du förstår varför vi taggar med både `:v1` och `:latest`

---

## Steg 1 — Skapa ett nytt Minimal API-projekt

Öppna terminalen och kör:

```bash
dotnet new webapi -minimal -n HyresBil
cd HyresBil
```

Öppna `Program.cs` och ersätt innehållet med det här:

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Returnerar en lista med tillgängliga bilar
app.MapGet("/bilar", () => new[]
{
    new { Id = 1, Modell = "Volvo V60", Tillgänglig = true },
    new { Id = 2, Modell = "Tesla Model 3", Tillgänglig = false },
    new { Id = 3, Modell = "Toyota Yaris", Tillgänglig = true }
});

app.MapGet("/", () => "HyresBil API körs!");

app.Run();
```

Verifiera att det fungerar lokalt:

```bash
dotnet run
```

Öppna `http://localhost:5000/bilar` i webbläsaren och kolla att du ser bilarna.

---

## Steg 2 — Skriv Dockerfilen

Skapa en fil som heter `Dockerfile` (exakt så, stor D, ingen filändelse) i projektmappen.

```dockerfile
# === STEG 1: Bygg appen ===
# sdk-imagen innehåller kompilatorn — vi behöver den för att bygga
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /app

# Kopiera projektfilen och hämta beroenden
COPY *.csproj ./
RUN dotnet restore

# Kopiera resten av koden och publicera
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# === STEG 2: Kör appen ===
# runtime-imagen är mycket mindre — den kan bara köra, inte bygga
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime

WORKDIR /app

# Kopiera det publicerade resultatet från build-steget
COPY --from=build /app/publish .

# Talar om att appen lyssnar på port 8080 (EXPOSE är informativt, inte ett löfte)
EXPOSE 8080

# Startkommando
ENTRYPOINT ["dotnet", "HyresBil.dll"]
```

**Varför två steg?** Build-imagen med SDK är runt 900 MB. Runtime-imagen är runt 220 MB. Utan multi-stage hade du pushat en 900 MB image till registryt varje gång. Med multi-stage tar du med dig bara det som behövs för att köra. Det är vad som faktiskt går ut — den lilla runtime-imagen.

---

## Steg 3 — Bygg imagen lokalt

Ersätt `dittnamn` med ditt Docker Hub-användarnamn.

```bash
docker build -t dittnamn/hyresbil:v1 .
```

Titta på output — du ser båda stegen köras. Build-steget kompilerar, runtime-steget tar emot det publicerade resultatet.

Kontrollera att imagen finns:

```bash
docker images | grep hyresbil
```

---

## Steg 4 — Kör och verifiera lokalt

```bash
docker run -d -p 8080:8080 --name hyresbil-test dittnamn/hyresbil:v1
```

Öppna `http://localhost:8080/bilar` — du ska se bilarna, precis som förut. Fast nu körs de inuti en container.

Kika på loggarna om något är fel:

```bash
docker logs hyresbil-test
```

Stoppa och ta bort containern när du är klar:

```bash
docker rm -f hyresbil-test
```

---

## Steg 5 — Logga in och tagga

Du behöver ett konto på [hub.docker.com](https://hub.docker.com) om du inte redan har det.

Logga in i terminalen:

```bash
docker login
```

Nu ska vi tagga med `:latest` också. `:v1` berättar exakt vilken version det är. `:latest` är genvägen som Docker använder som standard när ingen tagg anges. Båda ska peka på samma image:

```bash
docker tag dittnamn/hyresbil:v1 dittnamn/hyresbil:latest
```

Kontrollera att du har båda taggarna:

```bash
docker images | grep hyresbil
```

Du ser två rader — men de pekar på samma image-ID. Det kostar ingenting extra att ha båda.

---

## Steg 6 — Pusha till Docker Hub

```bash
docker push dittnamn/hyresbil:v1
docker push dittnamn/hyresbil:latest
```

Du ser layer-by-layer upload. Layers som redan finns på Hub laddas inte upp igen — Docker är smart på det sättet.

Gå till `https://hub.docker.com/r/dittnamn/hyresbil` och verifiera att båda taggarna syns.

---

## Steg 7 — Bevisa att det fungerar från Hub

Det här är det riktiga testet. Ta bort den lokala imagen och dra ner den från Docker Hub:

```bash
# Ta bort lokala images
docker rmi dittnamn/hyresbil:v1
docker rmi dittnamn/hyresbil:latest
```

Verifiera att de är borta:

```bash
docker images | grep hyresbil
```

Nu — dra ner och kör direkt från Docker Hub:

```bash
docker run -d -p 8080:8080 --name hyresbil-hub dittnamn/hyresbil:latest
```

Docker hittar inte imagen lokalt. Den hämtar den automatiskt från Hub. Öppna `http://localhost:8080/bilar` igen.

Fungerar det? Då är du klar. Koda vilt!

Städa upp:

```bash
docker rm -f hyresbil-hub
```

---

## Taggarna — varför det spelar roll

`:v1` är som ett commit-hash. Oföränderligt. Om du pushar en bugg i `:v2` kan du alltid rulla tillbaka till `:v1` — du vet exakt vad du får.

`:latest` är en pekare. Den berättar ingenting om version. I produktion ska du **aldrig** deploya med `:latest` — du vet inte vad du faktiskt drar ner. I dev och lokala tester är den bekväm, men det är hela grejen med den.

**Tumregel:** Tagga alltid med version. Uppdatera `:latest` som bonus. Deployer i produktion ska alltid ange explicit tagg.

---

## Tips

> Fick du `no such file or directory` i build-steget? Kontrollera att `Dockerfile` ligger i projektrotens mapp, inte i en undermapp.

> Svarar appen med 502 eller inget alls? Kolla att .NET-appen lyssnar på port 8080 i containern. Lägg till `ASPNETCORE_URLS=http://+:8080` som miljövariabel om det krånglar: `docker run -e ASPNETCORE_URLS=http://+:8080 ...`

> ⏱️ **15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.

---

*Facit finns hos läraren.*
