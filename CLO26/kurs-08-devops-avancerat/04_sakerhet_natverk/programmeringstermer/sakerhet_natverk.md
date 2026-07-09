# 04 Sakerhet Natverk — Programmeringstermer

## Azure Virtual Network
Logiskt isolerat nätverk i Azure. Subnets, routing, firewall, VPN.

## Subnet
Underindelning av ett virtuellt nätverk. Olika subnets för olika tjänster: frontend, backend, databas.

## Network Security Group (NSG)
Brandväggsregler för virtuella nätverk. Allow/Deny på port, källa och destination.

## Application Security Group (ASG)
Gruppera VM efter applikationsroll. Skapa NSG-regler mot ASG istället för IP-adresser.

## Azure Firewall
Hanterad brandväggstjänst i Azure. Central policy, hot intelligence, DNS-säkerhet.

## VPN Gateway
Anslut ditt lokala nätverk till Azure via krypterad VPN-tunnel.

## Private Endpoint
Privat IP-anslutning till Azure-tjänster (Storage, SQL) utan att exponera dem mot internet.

## Load Balancer
Fördela trafik över flera servrar. Azure Load Balancer (L4) och Application Gateway (L7).

## TLS Termination
Avkryptering av HTTPS-trafik vid lastbalanseraren. Mindre belastning på backend-servrar.

## Service Endpoint
Exponera Azure-tjänster enbart till trafik inifrån ditt virtuella nätverk. Alternativ till Private Endpoint.

