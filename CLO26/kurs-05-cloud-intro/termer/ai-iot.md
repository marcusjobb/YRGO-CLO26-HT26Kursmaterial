# AI, ML och IoT — Programmeringstermer

Azure erbjuder färdiga tjänster för att bygga intelligenta och anslutna lösningar — utan att börja från scratch. Den här filen täcker AI-tjänster, maskininlärning och IoT.

---

## AI-tjänster

## Azure AI Services · Azure AI-tjänster

Färdiga AI-funktioner som exponeras som API:er. Du anropar en endpoint och får intelligens tillbaka — utan att behöva träna en enda modell.

Täcker vanliga AI-scenarier:
- **Språk** — textanalys, översättning, sentimentanalys
- **Tal** — tal-till-text, text-till-tal
- **Vision** — bildanalys, ansiktsigenkänning, OCR
- **Dokumentbearbetning** — extrahera strukturerad data ur ostrukturerade dokument

Tänk på det som ett AI-bibliotek du kan importera via HTTP. Ingen datavetare behövs.

## Azure OpenAI Service

Microsofts version av OpenAI:s modeller (GPT, DALL·E, Whisper) med inbyggda säkerhets- och styrningskontroller. Passar generativa AI-scenarier: chattbottar, textgenerering, kodhjälp.

Skillnaden mot vanlig Azure AI-tjänster: OpenAI Service genererar nytt innehåll. AI-tjänster analyserar befintligt.

## Agentic AI pattern · Agentiskt AI-mönster

Ett arkitekturmönster där du kombinerar en AI-modell med instruktioner, kontext och verktyg — och låter den lösa flerstegsmål på egen hand.

Exempel: "Ta emot ett kundärende, klassificera det, hämta orderhistorik, och formulera ett svar" — allt utan mänsklig styrning per steg.

Byggs i Azure med Azure AI-tjänster + Azure OpenAI + din egen programlogik. Det är ett mönster, inte en specifik tjänst.

## Azure Machine Learning · Azure Maskininlärning

Plattform för att bygga, träna, utvärdera och hantera egna maskininlärningsmodeller från grunden. Inte färdiga API:er — utan en hel pipeline för datavetare och ML-ingenjörer.

Välj Azure Machine Learning när du behöver:
- Träna en modell på dina egna data
- Experimentera med olika algoritmer
- Versionshantera modeller och datamängder
- Driftsätta en custom-modell som en endpoint

**Skillnaden:** Azure AI-tjänster = köp en färdig modell. Azure ML = bygg och träna din egen.

---

## IoT och Edge

## IoT · Internet of Things (sakernas internet)

Nätverket av fysiska enheter — sensorer, maskiner, apparater — som skickar och tar emot data via internet. En temperaturgivare i ett kylrum, en frakt-tracker på ett paket, en tillverkningsrobot på golvet.

## Telemetry · Telemetri

Data som enheter skickar kontinuerligt — mätningar, sensoravläsningar, statusinformation. Temperaturen i kylrummet var 3°C kl 14:32. Det är telemetri.

Rådata. Måste processas och analyseras innan det blir information.

## Azure IoT Hub

Microsofts meddelandebrygga mellan molnet och IoT-enheter. Hanterar säker, dubbelriktad kommunikation: enheter skickar telemetri upp till molnet, molnet skickar kommandon ner till enheter.

Tänk: en router för miljarder enheter.

## Azure IoT Central

En färdigpaketerad SaaS-plattform för att bygga IoT-lösningar utan att hantera IoT Hub direkt. Drag-and-drop-koppling av enheter, dashboards, larmsättning — utan att skriva infrastrukturkod.

Enklare att komma igång. Mindre flexibel än IoT Hub + egna lösningar.

| | IoT Hub | IoT Central |
|---|---|---|
| Kontroll | Full | Begränsad |
| Komplexitet | Hög | Låg |
| Passar | Skalbar, anpassad lösning | Snabb prototyp, standardscenario |

## Azure IoT Edge

Utökar molnets intelligens till gränsenheten — kör bearbetning och logik *på* enheten istället för att skicka allt till molnet.

Varför? Latens. En fabriksrobot som ska reagera på 10ms kan inte vänta på ett tur-och-retur-anrop till ett datacenter i Sverige. Den måste fatta beslut lokalt.

IoT Edge kör containeriserade workloads på enheten, med synkronisering mot molnet när uppkoppling finns.

## Edge computing · Kantberäkning

Att flytta beräkningskraft nära källan — enheten eller sensorn — istället för att allt processas centralt i ett datacenter. Minskar latens, minskar bandbreddsbehovet, ökar robusthet vid dålig uppkoppling.

**Moln:** beräkningar sker i ett datacenter långt borta.
**Edge:** beräkningar sker så nära enheten som möjligt.

---

## Flödet i praktiken

```
Enhet (sensor)
  → skickar telemetri via IoT Hub
    → molnanalys + AI-modell bearbetar
      → insikt och modelluppdatering
        → IoT Edge-körtid tillämpar logiken nära enheten
```

IoT Central förenklar hela flödet för standardscenarier. IoT Hub + IoT Edge + Azure ML ger full kontroll för avancerade lösningar.
