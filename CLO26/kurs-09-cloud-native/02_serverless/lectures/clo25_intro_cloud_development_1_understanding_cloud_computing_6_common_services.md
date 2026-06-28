---

title: Vanliga molntjänster — vad kan du faktiskt göra?
author: Marcus Ackre Medina
type: lecture
topic: cloud
difficulty: 1
language: swedish
status: adapted
marcus_voice: true
source: "Old_courses/Coud_Development_CLO25/intro-cloud-development/1-understanding-cloud-computing/6-common-services.md"
description: "Molnet är inte bara en server. Det är hundratals tjänster som täcker allt från virtuella maskiner till AI. Här är de viktigaste du borde känna till."
tags: ["cloud", "compute", "database", "faktiskt", "göra?", "molntjänster", "networking", "storage", "vanliga"]
week_fit: []
---
# Vanliga molntjänster — vad kan du faktiskt göra?

## Molnet är inte bara en grej

När folk säger "molnet" låter det som en sak. Det är det inte. Det är hundratals tjänster som täcker allt från virtuella maskiner till AI.

Här är de viktigaste kategorierna — så du vet vad du pratar om nästa gång någon nämner Azure eller AWS.

## Compute — där din kod bor

Det här är motorn. Virtuella maskiner, serverless-funktioner, containrar.

- **Virtuella maskiner** (EC2, Azure VM): full kontroll över OS, applikationer, allt. Som att hyra en dator i molnet.
- **Serverless** (Lambda, Azure Functions): du skriver bara kod. Inga servrar att tänka på. Plattformen kör din kod när den anropas.
- **Containrar** (AKS, EKS): du packar din app i en container och molnet kör den. Skalbar, portabel, effektivt.

## Storage — där din data bor

- **Blob storage**: för bilder, videor, filer. Allt du vill lagra.
- **Diskar**: som hårddiskar till dina virtuella maskiner.
- **File shares**: delade mappar som flera maskiner kan komma åt.

## Databaser — där din strukturerade data bor

- **SQL-databaser** (Azure SQL, RDS): traditionella relationsdatabaser i molnet.
- **NoSQL** (Cosmos DB, DynamoDB): för när du behöver skala brett.
- **Cachning** (Redis Cache): för att göra allt snabbare.

## Nätverk — hur allt pratar med varandra

- **Virtuella nätverk** (VNet, VPC): ditt eget nätverk i molnet.
- **Lastbalanserare**: fördelar trafik mellan servrar så ingen blir överbelastad.
- **CDN**: Content Delivery Network — snabbar upp leverans till användare världen över.
- **DNS**: översätter domännamn till IP-adresser.

## Säkerhet och identifiering

- **Brandväggar**: styr trafik in och ut ur ditt nätverk.
- **Key Vault**: för lösenord, certifikat och anslutningssträngar.
- **Azure AD / Entra ID**: identitetshantering — vem får göra vad.

## Det här är bara början

Varje molnleverantör har typ 200+ tjänster. AI, maskininlärning, IoT, blockkedja, spelutveckling — allt finns som tjänst i molnet.

Du behöver inte kunna allt. Men att veta vad som finns är första steget. Sen kan du Googla resten — precis som alla andra.
