# 02 Docker — Programmeringstermer

## Container
Lättviktig, fristående körbar enhet som innehåller kod, runtime och verktyg. Delar OS-kärnan.

## Image
Mall för att skapa containrar. Byggs från en Dockerfile. Kan versionshanteras och delas.

## Dockerfile
Textfil med instruktioner för att bygga en Docker-image. Varje rad = ett lager.

## Docker Hub
Standardregistry för Docker-images. Publika och privata repositories.

## Container Registry
Tjänst för att lagra och distribuera container-images. Exempel: Docker Hub, Azure Container Registry.

## Volume
Persistent lagring för containrar. Data i volymer överlever container-omstarter.

## Port Mapping
Mappning av containerport till värddatorns port: `docker run -p 8080:80`.

## Multi-stage Build
Dockerfile med flera FROM-satser. Separerar byggmiljö från runtime för mindre images.

## docker-compose
Verktyg för att definiera och köra flera containrar med en YAML-fil.

## Orchestrator
Verktyg som hanterar containrar i stor skala: start, stoppa, skala, load balancera.

