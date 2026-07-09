# 03 Skalning Och Lastbalansering — Programmeringstermer

## Horisontell Skalning
Lägg till fler instanser av samma tjänst. Vanligt i molnet. Kräver stateless design.

## Vertikal Skalning
Öka resurserna (CPU, minne) på en befintlig server. Begränsad av maxstorlek.

## Load Balancer
Fördelar inkommande trafik över flera servrar. Azure Load Balancer (L4) och Application Gateway (L7).

## Health Probe
Kontroll som avgör om en instans är frisk och kan ta emot trafik. HTTP-endpoint eller TCP-port.

## Session Persistence (Sticky Sessions)
Säkerställ att en användare alltid skickas till samma server. Krävs för stateful appar.

## Auto Scaling
Automatisk justering av resurser baserat på fördefinierade regler. CPU > 70% → lägg till instans.

## Scale Set
Azure VM Scale Sets. Grupp av identiska VM som kan skalas automatiskt.

## Vertical Pod Autoscaler
Justera CPU/minne för Kubernetes-poddar automatiskt baserat på användning.

## Rate Limiting
Begränsa antalet request per tidsenhet. Skyddar backend mot överbelastning.

## CDN
Content Delivery Network. Cacha statiskt innehåll (bilder, JS, CSS) på servers globalt.

