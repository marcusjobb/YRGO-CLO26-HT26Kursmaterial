# AI, ML och IoT — Programmeringstermer

Azure är inte bara servrar och lagring. Det finns en hel kategori av tjänster för att bygga intelligenta och anslutna lösningar — utan att börja från noll.

---

## Azure AI Services

Färdiga AI-funktioner som API:er. Du anropar en endpoint, Azure gör det smarta.

Täcker: textanalys, talöversättning, bildanalys, dokumentbearbetning. Ingen tränad modell behövs — du köper in intelligensen färdigpaketerad.

## Azure OpenAI Service

Microsofts version av OpenAI (GPT, DALL·E) med säkerhets- och styrningskontroller inbyggda. Används för chattbottar, textgenerering och liknande generativa scenarier.

Skillnad mot Azure AI Services: AI-tjänster *analyserar* befintligt innehåll. OpenAI Service *genererar* nytt.

## Maskininlärning (Machine Learning)

Att träna ett program på data istället för att programmera regler. Modellen hittar mönstren själv.

## Azure Machine Learning

Plattform för att bygga, träna och hantera egna maskininlärningsmodeller. Välj detta när du har egna data och behöver en skräddarsydd modell — inte en färdig tjänst.

## Agentiskt AI-mönster (Agentic AI)

Ett mönster där AI-modellen kombineras med instruktioner, kontext och verktyg för att klara flerstegsmål utan mänsklig styrning vid varje steg. Byggs med Azure AI-tjänster och programlogik — är inte en färdig tjänst.

---

## IoT och Edge

## IoT · Internet of Things (sakernas internet)

Fysiska enheter — sensorer, maskiner, apparater — kopplade till internet. En temperaturgivare i ett kylrum, en GPS-tracker på ett paket.

## Telemetri

Data som enheter skickar kontinuerligt — mätningar, sensoravläsningar, status. Rådata som måste processas innan det blir användbar information.

## Azure IoT Hub

Meddelandebryggan mellan molnet och IoT-enheter. Hanterar säker, dubbelriktad kommunikation: enheter skickar data upp, molnet skickar kommandon ner.

## Azure IoT Central

En färdigpaketerad SaaS-plattform för IoT-lösningar — enklare att komma igång med än IoT Hub direkt. Passar prototyper och standardscenarier.

## Azure IoT Edge

Kör molnlogik *på* enheten, nära där datan skapas. Minskar latens och minskar beroendet av konstant internetuppkoppling.

## Edge computing · Kantberäkning

Att flytta beräkningskraft nära källan — enheten eller sensorn — istället för att allt skickas till ett datacenter. Lägre latens, robustare vid dålig uppkoppling.

---

## Välj rätt tjänst

| Scenario | Välj |
|----------|------|
| Lägga till AI-funktioner via API | Azure AI Services |
| Generera text eller föra dialog | Azure OpenAI Service |
| Träna egen modell på egna data | Azure Machine Learning |
| Koppla sensorer och enheter till molnet | Azure IoT Hub / IoT Central |
| Kör logik lokalt på enheten | Azure IoT Edge |
