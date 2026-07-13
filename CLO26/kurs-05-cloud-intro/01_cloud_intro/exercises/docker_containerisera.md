# Övning — Containerisera ett .NET API

🟡

---

## Vad du ska göra

Du ska containerisera ett enkelt .NET 10 Minimal API som returnerar hårdkodad väderdata. Övningen är uppdelad i tre tydliga steg:

1. Skapa appen och verifiera att den fungerar lokalt
2. Bygg en single-stage Docker-image — se vad som händer med storleken
3. Bygg om med multi-stage — förstå varför det är rätt väg i produktion

Du kommer inte integrera ett riktigt väder-API. Fokus ligger på Docker, inte datakällor.

---

## Förutsättningar

- Docker Desktop installerat och igång (`docker ps` ger svar utan fel)
- .NET 10 SDK installerat (`dotnet --version` visar `10.x.x`)
- Terminal öppen i en lämplig arbetsmapp

---

## Steg 1 — Skapa appen

Skapa projektet med flaggan `--no-openapi` — du behöver inte Swagger för den här övningen.

```
dotnet new webapi --no-openapi -n VaderApi
cd VaderApi
```

Ersätt hela innehållet i `Program.cs` med följande:

> `VaderApi/Program.cs`

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/vader", () =>
{
    // Hårdkodad väderdata — ingen extern API-integration
    return Results.Ok(new
    {
        stad        = "Göteborg",
        temperatur  = 18,
        enhet       = "°C",
        beskrivning = "Molnigt med solglimtar",
        vind        = "Sydvästlig, 5 m/s"
    });
});

app.Run();
```

Starta appen och kontrollera att endpointen svarar:

```
dotnet run
```

Öppna en ny terminal och testa:

```
curl http://localhost:5000/vader
```

### Förväntad output

```json
{
  "stad": "Göteborg",
  "temperatur": 18,
  "enhet": "°C",
  "beskrivning": "Molnigt med solglimtar",
  "vind": "Sydvästlig, 5 m/s"
}
```

Stoppa appen med `Ctrl+C` när du bekräftat att det fungerar.

> **Kontrollpunkt:** `/vader` returnerar JSON och inga fel i terminalen.

---

## Steg 2 — Single-stage Dockerfile (problemet)

Skapa filen `Dockerfile` direkt i `VaderApi/`-mappen:

> `VaderApi/Dockerfile`

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:10.0
WORKDIR /app

COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

WORKDIR /app/publish
EXPOSE 8080
ENTRYPOINT ["dotnet", "VaderApi.dll"]
```

Bygg imagen och döp den till `vader-api:single`:

```
docker build -t vader-api:single .
```

Kontrollera storleken:

```
docker images vader-api
```

Du ser något i stil med:

```
REPOSITORY   TAG      SIZE
vader-api    single   ~900MB
```

**Varför är det ett problem?**

SDK-imagen innehåller kompilatorn, NuGet-verktyg, och allt annat du behöver för att *bygga* .NET-kod. Men när appen väl är byggd behöver du inget av det för att *köra* den. Du har alltså packad 700+ MB verktyg som aldrig används i produktion — i varje deployment, varje pull till molnet, varje container som startar.

Större image = långsammare deploy, högre kostnad, större attackyta.

> **Kontrollpunkt:** Imagen byggs utan fel. Storleken är runt 900 MB.

---

## Steg 3 — Multi-stage Dockerfile (lösningen)

Ersätt hela innehållet i `Dockerfile` med:

> `VaderApi/Dockerfile`

```dockerfile
# --- Steg 1: Bygg appen med SDK-imagen ---
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Restore-first: kopiera .csproj separat och kör restore innan källkoden
COPY VaderApi.csproj .
RUN dotnet restore

# Nu kopieras resten av källkoden och publish körs
COPY . .
RUN dotnet publish -c Release -o /app/publish

# --- Steg 2: Kör appen med den minimala runtime-imagen ---
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "VaderApi.dll"]
```

Bygg den nya imagen:

```
docker build -t vader-api:multi .
```

Jämför storlekarna:

```
docker images vader-api
```

```
REPOSITORY   TAG      SIZE
vader-api    multi    ~220MB
vader-api    single   ~900MB
```

**Vad händer här?**

Dockerfilen har nu två `FROM`-instruktioner — det är multi-stage. Den första fasen (`build`) använder SDK-imagen för att kompilera appen. Den andra fasen (`runtime`) startar om från en minimal ASP.NET Core runtime-image och kopierar bara de kompilerade filerna med `COPY --from=build`. Hela SDK-fasen — kompilatorn, NuGet-cachen, källkoden — kastas bort. Den hamnar aldrig i den slutliga imagen.

**Restore-first-mönstret förklarar:**

Docker cachar varje lager. Så fort ett lager ändras måste Docker bygga om det och allt efter det — inklusive `dotnet restore`, vilket kan ta en minut eller mer.

Ordningen i Dockerfilen är därför viktig:

```
COPY VaderApi.csproj .   ← ändras sällan (bara när du lägger till paket)
RUN dotnet restore        ← cachas så länge .csproj inte ändras
COPY . .                  ← ändras ofta (varje gång du redigerar kod)
RUN dotnet publish        ← måste köras om när källkod ändras
```

Om du hade skrivit `COPY . .` direkt och sedan `dotnet restore`, hade en ändring i en enda rad i `Program.cs` tvingat Docker att ladda ner alla NuGet-paket på nytt från scratch. Med restore-first cachas paket-steget oberoende av källkodsändringar. I praktiken går ett ombygge från minuter till sekunder.

**Testa cachen:**

Ändra ett ord i `Program.cs` — t.ex. byt `"Molnigt med solglimtar"` till `"Soligt"` — och bygg om:

```
docker build -t vader-api:multi .
```

Du ska se `CACHED` på `dotnet restore`-steget. Bara `COPY . .` och `dotnet publish` körs om.

> **Kontrollpunkt:** Multi-stage imagen är runt 220 MB. Cachen fungerar — restore-steget visar `CACHED` vid ombygge.

---

## Steg 4 — Kör lokalt

Starta en container från multi-stage-imagen:

```
docker run --name vader-local -d -p 5000:8080 vader-api:multi
```

Testa endpointen:

```
curl http://localhost:5000/vader
```

### Förväntad output

```json
{
  "stad": "Göteborg",
  "temperatur": 18,
  "enhet": "°C",
  "beskrivning": "Molnigt med solglimtar",
  "vind": "Sydvästlig, 5 m/s"
}
```

Appen ska svara identiskt mot hur den svarade lokalt med `dotnet run`.

Visa container-loggar om något inte fungerar:

```
docker logs vader-local
```

Städa upp när du är klar:

```
docker rm -f vader-local
```

> **Kontrollpunkt:** Containern svarar på `/vader`, loggar inga fel, och stoppen funkar utan kvarstående containers.

---

## Utmaning 🔴 — Pusha till registry

Välj ett av alternativen nedan. Båda fungerar, men Azure Container Registry passar bättre om du redan har ett Azure-konto från kursen.

### Alternativ A: Docker Hub

```
docker login
docker tag vader-api:multi dittanvändarnamn/vader-api:v1
docker push dittanvändarnamn/vader-api:v1
```

Verifiera att imagen syns på `hub.docker.com` under ditt konto.

### Alternativ B: Azure Container Registry

Skapa ett registry (byt ut `rg-clodemo` och `acrclo26demo` mot egna namn):

```
az group create --name rg-clodemo --location swedencentral
az acr create --resource-group rg-clodemo --name acrclo26demo --sku Basic
az acr login --name acrclo26demo
```

Tagga och pusha:

```
docker tag vader-api:multi acrclo26demo.azurecr.io/vader-api:v1
docker push acrclo26demo.azurecr.io/vader-api:v1
```

Verifiera att imagen finns i registryt:

```
az acr repository list --name acrclo26demo --output table
```

> **Kontrollpunkt:** Du kan lista imagen i registryt från kommandoraden.

---

## Kontrollpunkter

Gå igenom listan innan du markerar övningen som klar:

- [ ] `/vader` returnerar korrekt JSON när appen körs lokalt med `dotnet run`
- [ ] Single-stage imagen byggs och är runt 900 MB
- [ ] Multi-stage imagen byggs och är runt 220 MB
- [ ] Du kan förklara varför storleksskillnaden uppstår
- [ ] Containern från multi-stage-imagen svarar identiskt mot lokal körning
- [ ] Ett ombygge efter källkodsändring visar `CACHED` på restore-steget
- [ ] Du kan förklara restore-first-mönstret med egna ord
- [ ] (🔴) Imagen är pushad till Docker Hub eller Azure Container Registry och syns i registryt

---

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.
