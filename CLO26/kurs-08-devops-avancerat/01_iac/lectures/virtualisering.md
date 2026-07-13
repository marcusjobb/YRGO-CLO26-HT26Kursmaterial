# Virtualisering — fysisk server, virtuell server och Azure VM-storlekar

En fysisk server är som ett flerfamiljshus. Huset finns på riktigt — teglet, röranslutningarna, eltavlan. Ingen kan ta bort det. Men ingen säger att en enda familj måste bo i hela huset. Du kan dela upp det i lägenheter, och varje lägenhet kan ha sin egen dörr, sitt eget postlådenummer och sina egna regler. Det är i grunden vad virtualisering gör med en server.

---

## Del 1 — Inuti en fysisk server

En modern serverprocessor är inte en kärna — den är en stad av kärnor. En vanlig serverchip i dag har 32, 64 eller fler kärnor, och på en datacenternod sitter det ofta två sådana chips bredvid varandra. Det kallas **dual-socket**, och det fördubblar allt: kärnor, minnesbandbredd, beräkningskapacitet.

Minnesarkitekturen har ett eget namn värt att känna till: **NUMA (Non-Uniform Memory Access)**. Det låter krångligt men är egentligen logiskt. Varje CPU-socket har sin egna del av RAM-minnet som den når snabbt. Vill den nå den andra socketens minne tar det lite längre tid — som att hämta kaffe från ett annat rum istället för sitt eget bord. Operativsystem och hypervisorer är medvetna om detta och försöker placera data nära den CPU som ska använda den.

Ovanpå hårdvaran sitter **hypervisorn** — programvaran som gör att en fysisk maskin kan hysa flera virtuella maskiner samtidigt. Den hanterar all delning av resurser. Microsoft Hyper-V, VMware ESXi och KVM är tre vanliga exempel. Azure bygger på en Hyper-V-baserad hypervisor (med Azures egna modifieringar).

Varför delar man upp resurser överhuvudtaget? Enkelt: en enda applikation utnyttjar sällan all kapacitet hela tiden. En webbserver kanske jobbar hårt i fem sekunder och sedan väntar i fem minuter. Om man istället kör tio virtuella maskiner på samma fysisk hårdvara kan de dela kapaciteten och tillsammans nyttja resursen bättre. Det kallas **konsolidering** och är grunden till varför molntjänster kan erbjuda så låga priser.

---

## Del 2 — Inuti en virtuell server

En VM ser ut som en hel dator — men inget av det är fysiskt. Varje del är ett lager av abstraktion ovanpå hårdvaran.

**vCPU (virtuell CPU)** är hypervisorns löfte om processorkraft. En vCPU är inte en fysisk kärna — den är en schemalagd tidskiva. Har du fyra vCPU:er innebär det att operativsystemet i din VM tror att det har fyra kärnor, men hypervisorn bestämmer när varje vCPU faktiskt får köra kod på en riktig kärna. Det är som köerna i kassan: du har din plats i kön, men det är kassörskan (hypervisorn) som bestämmer när det är din tur.

**Virtuellt minne** fungerar på liknande sätt. Hypervisorn reserverar ett minnessegment ur det fysiska RAM:et och presenterar det som om det vore maskinens eget. Moderna hypervisorer kan till och med använda tekniker som *memory ballooning* — att dynamiskt låna tillbaka minne från virtuella maskiner som inte använder det just nu och ge det till en som behöver mer.

**vNIC (virtuellt nätverkskort)** är din VM:s anslutning till omvärlden. Trafik från VM:en flödar genom hypervisorn via en virtuell switch och ut på det fysiska nätverket via serverns riktiga nätverkskort. Det är som att varje lägenhet i huset har sin egen brevinkast, men alla brev faktiskt passerar husets enda brevlåda ute vid gatan.

---

## Del 3 — Azure VM-storlekar och kostnad

Azure erbjuder dussintals VM-serier. Tre är viktigast att känna till när man börjar.

**B-serien (Burstable)** är byggd för arbetsbelastningar som sover mestadels och ibland vaknar upp. En B2s-VM samlar CPU-krediter när den är stilla, och spenderar krediterna när den behöver prestera. Perfekt för testmiljöer, CI-agenter och system som körs schema-baserat. Billigast i klassen.

**D-serien (General Purpose)** är arbetshästen. Balanserat förhållande mellan CPU och minne, jämnt hög prestanda utan surprises. D2s_v5 och D4s_v5 är vanliga startpunkter för produktionsmiljöer, webbservrar och applikationsservrar.

**E-serien (Memory Optimized)** lyfter minnesmängden relativt CPU — för databaser och cachar som håller stora datamängder i minnet.

Kostnadsmodellen är enkel: **du betalar per timme** (eller per sekund, beroende på avtalet). En D2s_v5 kostar ungefär 0,09 USD per timme — stoppar du den betalar du ingenting. Det är hela poängen med molnet: du betalar för det du faktiskt kör, inte för ett rack som står och tickar i ett källarrum.

**Right-sizing** — att välja rätt storlek — handlar om att matcha vCPU och minne mot faktisk last, inte förväntad topplast. Azures monitor visar CPU-utnyttjande över tid. Ligger snittanvändningen på 8 procent är du överdimensionerad och betalar fyra gånger för mycket. Skala ner, mät igen, justera. Det är en kontinuerlig loop — inte ett engångsbeslut.
