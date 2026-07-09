# 04 Webbapplikationer — Programmeringstermer

## Azure App Service
PaaS-tjänst för att deploya webbapplikationer. Stödjer .NET, Java, Node, Python, PHP.

## App Service Plan
Definierar resurserna (CPU, minne, instanser) för en eller flera App Services.

## Deploy Slot
Stage-slot för App Service. Möjliggör zero-downtime deployment och swap.

## Kudu
Hanteringsverktyg inbyggt i App Service. Ger tillgång till filer, processer och debug-konsol.

## WebJobs
Bakgrundsjobb som körs i en App Service. Kontinuerliga eller schemalagda.

## Always On
Inställning som håller App Service laddad även utan trafik. Krävs för bakgrundsjobb.

## DNS
Domain Name System. Översätter domännamn (mittföretag.se) till IP-adresser.

## Custom Domain
Eget domännamn kopplat till App Service. Kräver DNS-verifiering.

## TLS/SSL
Kryptering av webbtrafik. App Service har stöd för gratis SSL via Let's Encrypt.

## App Configuration
Azure-tjänst för central hantering av konfiguration. Alternativ till appsettings.json.

