---

title: INTRODUKTION TILL CONTAINERS & DOCKER
author: Marcus Ackre Medina
type: lecture
topic: docker
difficulty: 2
language: swedish
status: adapted
marcus_voice: true
source: "Old_courses/Coud_Development_CLO25/presentations/mini-lectures/introduction-to-containershtml.md"
description: "Från virtuella maskiner till containrar"
tags: ["azure", "containers", "containershtml", "docker", "dotnet", "linux", "till"]
week_fit: []
---

ACD

# INTRODUKTION TILL CONTAINERS & DOCKER

🟡


Från virtuella maskiner till containrar

Molnapplikationer Fördjupning • 2026

## Varför behövs containers?

Appen fungerar lokalt men inte på VM:en — olika .NET-versioner, saknade beroenden

Manuell serversetup — installera runtime, konfigurera systemd, öppna portar

Skalning kräver att provisionera och konfigurera en helt ny VM

Svårt att garantera att dev, test och produktion är identiska miljöer

## Från fysisk server till container

1

Fysisk server

2

Virtuell maskin

3

Container

Fysisk server: En applikation per server. Dyrt, långsamt att provisionera, slöseri med resurser.

Virtuell maskin: Flera isolerade OS på en maskin. Ni känner igen detta från Azure VMs — men varje VM bär ett helt operativsystem.

Container: Isolerade processer som delar värdkerneln. Megabyte istället för gigabyte, sekunder istället för minuter.

## VM vs Container

VM kör ett helt operativsystem — container delar värdkerneln

VM-image: gigabyte, starttid minuter — container-image: megabyte, starttid sekunder

VM isolerar via hypervisor — container isolerar via Linux-kerneln

## Vad är en container egentligen?

Namespaces — isolation. Containern ser sitt eget filsystem, nätverk och processer

Cgroups — resursbegränsning. CPU och minne allokeras per container

Union filesystem — lager. Images byggs som staplade read-only lager, containern lägger till ett tunt skrivbart lager

## Image vs Container

Image — en oföränderlig, lagrad mall. Tänk: en klass i C#

Container — en körande instans av en image. Tänk: ett objekt

Registry — där images lagras och delas. Docker Hub, Azure Container Registry

## Dockerfile — receptet

En textfil som beskriver hur en image byggs, steg för steg

Tänk: att skriva ner all manuell setup ni gjorde på VM:en — men reproducerbart

Varje instruktion skapar ett lager som cachas — snabba ombyggnader

Multi-stage builds — .NET SDK för att kompilera, bara runtime i slutresultatet

## Varför containers i molnet?

Konsistens — samma image körs identiskt i dev, CI och produktion

Portabilitet — imagen bär allt den behöver, inga manuella installationssteg

Densitet — en VM kan köra många containrar istället för en applikation

Immutability — man patchar inte en körande container, man ersätter den med en ny image

## Vad kommer härnäst?

1

Dockerfile

2

Compose

3

ACR

4

ACA

Dockerfile: Bygga images för er ASP.NET Core-applikation

Docker Compose: Köra appen + MongoDB tillsammans lokalt

Azure Container Registry: Lagra images i molnet

Azure Container Apps: Deploya containrar utan att hantera servrar

?

Frågor

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
