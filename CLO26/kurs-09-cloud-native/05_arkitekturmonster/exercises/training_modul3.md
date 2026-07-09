# Träningsuppgifter: Cloud Native — Modul 3

> **Modul:** 05 — Arkitekturmönster (CQRS, event sourcing, strangler fig, saga-mönstret)

## Instruktioner
Välj det bästa svaret. Klicka på 'Visa svar' för att se rätt svar och förklaringar.

### Fråga 1

Vad är CQRS (Command Query Responsibility Segregation)?

a. Ett mönster som separerar läsoperationer (queries) från skrivoperationer (commands) — olika modeller och ofta olika databaser<br>b. Att använda samma modell för läs och skriv<br>c. En databas-teknik<br>d. Ett sätt att kompilera kod

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Ett mönster som separerar läsoperationer från skrivoperationer

  **Förklaringar:**

  - ✅ **a) Separata modeller för läs och skriv** - **RÄTT**: Command = "ändra tillstånd" (INSERT, UPDATE, DELETE) — returnerar inget, validerar affärsregler. Query = "hämta data" (SELECT) — returnerar data, ingen sidoeffekt. Vanlig CRUD: samma modell och databas för allt. CQRS: olika databaser (write DB + read DB), read kan vara en denormaliserad vy för snabb hämtning
  - ❌ **b) Samma modell** - FEL: Det är CRUD, inte CQRS
  - ❌ **c) Databas-teknik** - FEL: CQRS är ett arkitekturmönster, inte en specifik teknik
  - ❌ **d) Kompilera** - FEL: Har inget med kompilering att göra
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 2

När är CQRS ett lämpligt val?

a. För en todo-app med 3 användare<br>b. När läs- och skrivmönstren är väldigt olika — t.ex. komplexa writes med validering men enkla reads, eller när du behöver olika databaser för olika prestandabehov<br>c. Alltid, för alla projekt<br>d. Aldrig

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** När läs- och skrivmönster är väldigt olika och kräver olika optimering

  **Förklaringar:**

  - ❌ **a) Todo-app** - FEL: CQRS är överengineering för enkla appar
  - ✅ **b) Olika läs/skriv-mönster** - **RÄTT**: Exempel: e-handel. Write: komplex order-validering, lagerkontroll, prisberäkning. Read: snabb produktlista med få fält. CQRS låter write-databasen vara normaliserad (ACID) och read-databasen denormaliserad (snabb)
  - ❌ **c) Alltid** - FEL: CQRS ökar komplexiteten enormt — använd bara när det ger tydlig nytta
  - ❌ **d) Aldrig** - FEL: CQRS är kraftfullt för rätt scenario
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 3

Vad är event sourcing?

a. Att spara alla händelser som ändrar tillstånd i ett system — istället för nuvarande tillstånd sparar du en sekvens av händelser som LEDDE till nuvarande tillstånd<br>b. Att bara spara det senaste tillståndet<br>c. Att skicka e-posthändelser<br>d. En databas

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Spara alla händelser som ändrar tillstånd — en sekvens av händelser, inte bara nuvarande tillstånd

  **Förklaringar:**

  - ✅ **a) Händelsesekvens** - **RÄTT**: Istället för en rad "Orders" som uppdateras: lagra "OrderPlaced", "OrderPaid", "OrderShipped", "OrderDelivered". Nuvarande tillstånd = spela upp alla events. Fördelar: full audit trail, time travel (se tillstånd vid varje tidpunkt), event-driven arkitektur. Nackdel: komplexare queries, event evolution
  - ❌ **b) Senaste tillståndet** - FEL: Det är traditionell CRUD, motsatsen till event sourcing
  - ❌ **c) E-post** - FEL: Events är domänhändelser, inte kommunikationsmeddelanden
  - ❌ **d) Databas** - FEL: Event sourcing är ett mönster för datalagring, inte en databas i sig
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 4

Vad är fördelen med event sourcing jämfört med traditionell CRUD?

a. Enklare att implementera<br>b. Full revisionshistorik, time travel, och möjlighet att härleda nuvarande tillstånd från händelser — du kan alltid svara på "vad hände egentligen?"<br>c. Snabbare prestanda<br>d. Mindre lagringsutrymme

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Full revisionshistorik, time travel, härleda tillstånd från händelser

  **Förklaringar:**

  - ❌ **a) Enklare** - FEL: Event sourcing är betydligt mer komplext än CRUD
  - ✅ **b) Audit trail + time travel** - **RÄTT**: CRUD: order.total = 500 (vem ändrade? när? varför?). Event sourcing: OrderPlaced (total=400), OrderItemAdded(item, +100). Allt sparas. Du kan rekonstruera tillståndet från dag 1. Perfekt för: finans, logistik, system som kräver revision
  - ❌ **c) Snabbare** - FEL: Event sourcing är ofta långsammare (måste spela upp events)
  - ❌ **d) Mindre lagring** - FEL: Event sourcing kräver MER lagring (alla events sparas)
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 5

Vad är strangler fig-mönstret (Strangler Fig Pattern)?

a. Att gradvis ersätta en gammal applikation med en ny genom att skapa nya komponenter bredvid — en efter en, tills den gamla appen kan stängas av<br>b. Att ta bort en applikation helt och ersätta med en ny på en gång<br>c. Att migrera all data på en gång<br>d. Att byta namn på applikationen

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Att gradvis ersätta en gammal app med en ny — komponent för komponent

  **Förklaringar:**

  - ✅ **a) Gradvis ersättning** - **RÄTT**: Inspirerad av fikusstranglers växtsätt. Du bygger nya delarna bredvid den gamla monoliten. En routing-mekanism (API Gateway) skickar trafik till de nya eller gamla komponenterna. När alla delar är ersatta tar du bort den gamla appen. Minimal risk — du kan alltid gå tillbaka
  - ❌ **b) Allt på en gång** - FEL: Det är "big bang rewrite" — hög risk, ofta misslyckad
  - ❌ **c) All data samtidigt** - FEL: Strangler migrerar GRADVIS, både kod och data
  - ❌ **d) Byta namn** - FEL: 😄 Namnbyten löser inga arkitekturproblem
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 6

Vad är saga-mönstret (Saga Pattern)?

a. En lång historia<br>b. Ett mönster för att hantera distribuerade transaktioner över flera microservices — varje steg har ett kompenserande steg om något går fel<br>c. En databas-transaktion<br>d. Ett sätt att logga händelser

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Ett mönster för distribuerade transaktioner med kompenserande steg

  **Förklaringar:**

  - ❌ **a) Lång historia** - FEL: 😄 Teknisk term, inte bokstavlig saga
  - ✅ **b) Distribuerad transaktion** - **RÄTT**: En order går över 3 tjänster: 1) Reservation Service (reservera lager) → 2) Payment Service (dra kort) → 3) Shipping Service (skapa frakt). Om betalning misslyckas: skicka kompenserande event "ReleaseReservation". ACID-transaktioner fungerar inte över tjänster. Choreography (events) vs Orchestration (central coordinator)
  - ❌ **c) DB-transaktion** - FEL: Saga är för distribuerade transaktioner över tjänster, inte en lokal DB-transaktion
  - ❌ **d) Logga** - FEL: Sagor hanterar konsistens, inte loggning
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 7

Vad är skillnaden mellan choreography och orchestration i saga-mönstret?

a. Samma sak<br>b. Choreography = varje tjänst reagerar på events och publicerar egna (decentraliserat). Orchestration = en central coordinator (orchestrator) säger åt varje tjänst vad den ska göra<br>c. Orchestration är decentraliserat<br>d. Choreography använder en central coordinator

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Choreography = decentraliserat (events). Orchestration = central coordinator

  **Förklaringar:**

  - ❌ **a) Samma** - FEL: Två olika implementationer
  - ✅ **b) Events vs coordinator** - **RÄTT**: Choreography: OrderService publicerar "OrderCreated" → PaymentService reagerar, publicerar "PaymentCompleted" → ShippingService reagerar. Ingen central kontroll. Orchestration: OrderSagaOrchestrator säger: "PaymentService: dra kort" → väntar på svar → "ShippingService: skapa frakt". Mer kontroll men tightare koppling
  - ❌ **c) Orchestration decentraliserat** - FEL: Orchestration är centraliserat per definition
  - ❌ **d) Choreography central coordinator** - FEL: Choreography är decentraliserat
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 8

Vad är ett kompenserande transaktionssteg i en saga?

a. Ett steg som gör en backup av databasen<br>b. Ett steg som ÅNGRA effekten av ett tidigare steg — t.ex. "ReleaseReservation" ångrar "ReserveInventory" om betalningen misslyckas<br>c. Ett steg som loggar vad som hänt<br>d. Ett extra steg för säkerhet

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Ett steg som ångra effekten av ett tidigare steg — rollback på tjänstnivå

  **Förklaringar:**

  - ❌ **a) Backup** - FEL: Kompenserande steg är logiska ångringar, inte databassäkerhetskopior
  - ✅ **b) Ångra tidigare steg** - **RÄTT**: Du kan inte bara "rollback" över flera tjänster. Istället: varje steg har ett motsatt steg. "ReserveInventory" → kompensation: "ReleaseInventory" (lägg tillbaka lagret). "ChargeCard" → kompensation: "RefundCard" (återbetala). Detta ger "eventuell konsistens" (eventual consistency)
  - ❌ **c) Logga** - FEL: Loggning är för observability, inte för att hantera transaktioner
  - ❌ **d) Extra säkerhet** - FEL: Kompensation är för att hantera FEL, inte för att öka säkerhet
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>
