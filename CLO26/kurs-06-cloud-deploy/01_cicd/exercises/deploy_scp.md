# Övning — Deploya HyresBil till Azure VM med SCP

🟡

---

## Bakgrunden

Du jobbar som backend-utvecklare på HyresBil AB. Det är en liten startup och de har inte satt upp några automatiska pipelines än — än så länge deployas allt manuellt. Din uppgift är att ta er Minimal API, bygga den för produktion, kopiera upp den till er Azure VM och starta den.

Det är precis så det såg ut på de flesta arbetsplatser för 5–10 år sedan. Och ibland gör det det fortfarande.

---

## Vad gäller för den här övningen

- Som utbildare vill jag att du kan bygga en .NET-app för produktion med `dotnet publish`
- Som utbildare vill jag att du kan kopiera filer till en VM med SCP
- Som utbildare vill jag att du förstår vad som saknas för att det här ska fungera i ett riktigt produktionssystem

Dessa punkter är vad vi tittar på — inte om outputen ser exakt likadan ut.

---

## Förutsättningar

Du behöver ha:

- En Azure VM med Ubuntu från tidigare övning (med SSH-åtkomst)
- .NET 10 SDK installerat lokalt
- .NET 10 runtime installerat på VM:en (`aspnetcore-runtime-10.0`)

---

## Steg 1 — Skapa HyresBil-appen

Skapa ett nytt Minimal API-projekt lokalt:

```bash
dotnet new webapi -n HyresBil --no-openapi
cd HyresBil
```

Öppna `Program.cs` och ersätt innehållet med:

```csharp
// HyresBil AB — Minimal API
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var bilar = new[]
{
    new { Id = 1, Modell = "Volvo XC60", Registreringsnummer = "ABC123", Ledig = true },
    new { Id = 2, Modell = "Toyota Corolla", Registreringsnummer = "DEF456", Ledig = false },
    new { Id = 3, Modell = "Tesla Model 3", Registreringsnummer = "GHI789", Ledig = true },
};

app.MapGet("/bilar", () => bilar);

app.Run();
```

Testa att det funkar lokalt:

```bash
dotnet run
```

Öppna en annan terminal och kör:

```bash
curl http://localhost:5000/bilar
```

Du ska se JSON med tre bilar. Bra — nu är appen redo att deployas.

---

## Steg 2 — Bygg för produktion

Stäng av den körande appen (`Ctrl+C`) och kör:

```bash
dotnet publish -c Release -o ./publish
```

Flaggan `-c Release` aktiverar kompilatoroptimeringar och tar bort debug-symboler — rätt inställning för produktion.

Titta på vad som skapades:

```bash
ls ./publish/
```

Du ska se `HyresBil.dll`, `HyresBil.runtimeconfig.json` och ett gäng andra filer. Det är allt som behövs för att köra appen på servern.

---

## Steg 3 — Kopiera till VM med SCP

Nu kopierar du publish-mappen till din VM. Byt ut `<DIN_VM_IP>` mot din faktiska IP:

```bash
scp -r ./publish azureuser@<DIN_VM_IP>:~/hyresbil
```

Vad händer här?

- `scp` — Secure Copy Protocol, krypterad filöverföring via SSH
- `-r` — rekursiv, kopierar hela mappen
- `./publish` — källmappen lokalt
- `azureuser@<DIN_VM_IP>:~/hyresbil` — målet på VM:en (hemkatalogen, undermapp `hyresbil`)

Du ser en progress-rad per fil när de kopieras. Om SSH-anslutningen fungerar ska det gå snabbt — alla filer tillsammans brukar vara under 20 MB.

---

## Steg 4 — SSH in och starta appen

Logga in på VM:en:

```bash
ssh azureuser@<DIN_VM_IP>
```

Kontrollera att filerna kom fram:

```bash
ls ~/hyresbil/
```

Starta appen:

```bash
dotnet ~/hyresbil/HyresBil.dll
```

Du ska se något i stil med:

```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
```

Observera att appen lyssnar på `localhost:5000` — det betyder att den bara är nåbar inifrån VM:en. Det är bra nog för den här övningen.

---

## Steg 5 — Testa med curl

Öppna ett **nytt terminalfönster** (lämna det gamla med appen igång). SSH:a in på VM:en igen:

```bash
ssh azureuser@<DIN_VM_IP>
```

Testa endpointen:

```bash
curl http://localhost:5000/bilar
```

### Förväntad output

```json
[{"id":1,"modell":"Volvo XC60","registreringsnummer":"ABC123","ledig":true},{"id":2,"modell":"Toyota Corolla","registreringsnummer":"DEF456","ledig":false},{"id":3,"modell":"Tesla Model 3","registreringsnummer":"GHI789","ledig":true}]
```

Ser du JSON:en? Bra. Appen körs på din Azure VM.

Gå tillbaka till det första terminalfönstret och stäng appen med `Ctrl+C`.

---

## Steg 6 — Vad saknas för produktion?

Du har nu deployat en .NET-app manuellt. Det funkar — men det är inte production-ready. Förstå varför:

**Problem 1: Ingen process manager**

Du startade appen med `dotnet HyresBil.dll` direkt i terminalen. När du stänger SSH-sessionen dör appen. Om appen kraschar startar den inte om. Det finns inget som vaktar den.

I produktion används en process manager — till exempel `systemd` på Linux. Systemd startar appen automatiskt när VM:en bootar, och startar om den om den kraschar.

**Problem 2: Inget auto-start vid omstart**

Starta om VM:en. Appen är borta. Du måste SSH:a in och starta den manuellt igen. Det är inte acceptabelt om systemet ska vara tillgängligt dygnet runt.

**Problem 3: Manuell process**

Varje gång du gör en förändring i koden måste du:
1. Bygga lokalt
2. SCP:a upp filerna
3. SSH:a in och starta om appen

Det är tidskrävande och felkänsligt. I ett riktigt team löser man det med en CI/CD-pipeline (GitHub Actions, Azure DevOps) som gör allt automatiskt vid varje push till main.

**Det här är ett bra tillfälle att se helheten:** SCP är ett bra verktyg för att lära sig hur deployment fungerar på grundnivå. Det är en del av grunden. Men nästa steg är att förstå varför proffsen inte gör det manuellt.

---

## Tips

> Filen heter `HyresBil.dll` — inte `HyresBil.exe`. På Linux körs .NET-appar med `dotnet` + `.dll`, inte som fristående binärer (om du inte explicit publicerar self-contained).

> Om `scp` klagar på SSH-nyckeln — kontrollera att din nyckel på VM:en finns i `~/.ssh/authorized_keys` och att SSH är tillåtet i Azure NSG på port 22.

> Om `curl http://localhost:5000/bilar` ger "Connection refused" — appen startade inte korrekt. Kolla felmeddelandet i terminalen där `dotnet` körs.

> ⏱ **15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.

---

*Facit finns hos läraren.*
