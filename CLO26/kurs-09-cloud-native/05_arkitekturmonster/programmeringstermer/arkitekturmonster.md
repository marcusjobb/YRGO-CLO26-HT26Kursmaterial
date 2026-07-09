# 05 Arkitekturmonster — Programmeringstermer

## Arkitekturmönster
Återanvändbar arkitekturlösning för vanliga designproblem: microservices, event-driven, CQRS.

## CQRS
Command Query Responsibility Segregation. Separera läs- och skrivoperationer i olika modeller.

## Event Sourcing
Lagringsstrategi där alla tillståndsändringar sparas som en sekvens av händelser.

## Strangler Fig Pattern
Graduellt migrera en monolit till microservices genom att gradvis ersätta delar.

## Circuit Breaker
Skyddsmekanism som stoppar anrop till en trasig tjänst. Låter den återhämta sig.

## Event-Driven Architecture
Tjänster kommunicerar via händelser (meddelandekö). Lös koppling, bra skalbarhet.

## Saga Pattern
Distribuerad transaktion över flera microservices. Kompenserande åtgärder vid fel.

## API Gateway
Entrépunkt för alla klienter. Hanterar autentisering, rate limiting, routing, caching.

## Backend for Frontend (BFF)
Separat API-backend per klient (web, mobile, desktop). Anpassad för varje gränssnitt.

## Sidecar Pattern
Extra container som körs bredvid huvudcontainern. Delad livscykel, samma pod.

