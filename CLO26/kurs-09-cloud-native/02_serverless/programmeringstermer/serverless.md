# 02 Serverless — Programmeringstermer

## Serverless
Exekveringsmodell där molnleverantören hanterar servrar. Du betalar per exekvering, inte per server.

## Azure Functions
Serverless compute i Azure. Kör kod som svar på händelser: HTTP, timer, kömeddelanden.

## Function App
Container för en eller flera Azure Functions. Delar konfiguration och resurser.

## Trigger
Händelse som startar en Function. HTTP, Timer, Queue, Blob, Event Grid.

## Binding
Deklarativ koppling mellan en Function och en datakälla. Input (läs) och Output (skriv).

## Durable Functions
Orkestreringsramverk för långvariga serverless-arbetsflöden. Stateful och checkpoint-aware.

## Cold Start
Fördröjning när en serverless-funktion körs första gången efter inaktivitet. Kan vara 0.5-5 sekunder.

## Consumption Plan
Serverless-värdplan. Betala per exekvering + GB-sekund. Automatisk skalning från 0.

## Premium Plan
Värdplan med pre-warmed instances. Inga cold starts. Dedikerad CPU.

## Event Grid
Azure-händelserouter. Publicera/prenumerera på händelser mellan Azure-tjänster.

