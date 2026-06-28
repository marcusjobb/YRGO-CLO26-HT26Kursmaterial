---

title: Molnbaserad E-commerce Integration - Del 1
author: Marcus Ackre Medina
type: example
topic: cloud
difficulty: 1
language: python
status: adapted
marcus_voice: true
source: "Old_courses/2025/java/8_cloud_integration/assignments/part1.md"
description: "Skapa en molnbaserad integrationslösning bestående av **tre separata tjänster** som kommunicerar via både synkron och asynkron kommunikation. Systemet ska vara deployat på Microsoft Azure med automati"
tags: ["cloud", "e-commerce", "git", "integration", "molnbaserad", "part1", "ssh", "visual-studio"]
week_fit: []
---

# Molnbaserad E-commerce Integration - Del 1

🟢


## Projektöversikt

Skapa en molnbaserad integrationslösning bestående av **tre separata tjänster** som kommunicerar via både synkron och asynkron kommunikation. Systemet ska vara deployat på Microsoft Azure med automatiserad CI/CD och omfattande monitoring.

## Systemets tre delar

### Del 1: User Service

**Ansvar och funktionalitet:**

- Användarregistrering och autentisering (JWT-tokens)
- Användarprofilhantering och personliga inställningar
- Lösenordshantering och säkerhetsvalidering
- Användarhistorik och aktivitetsloggning
- REST API för alla användaroperationer

**Teknisk implementation:**

- Spring Boot applikation med Spring Security
- JWT-baserad autentisering för API-säkerhet
- Azure SQL Database för användardata
- Password hashing och säkerhetsvalidering
- Utförlig logging för alla användaraktiviteter

### Bonusuppgift

- Skapa en React frontend som kan prata med User Service (Kika på tidigare bonus lektioner om frontend).
- Helt fritt val av teknik och design.
- Ni får lov att Vibe-koda en frontend med ChatGPT, Clade mm.
- Det underlättar om ni har Swagger UI för att testa API:erna och där ni kan skicka med den informationen till AI.

Lars kommer lägga in bonuslektioner på fredsförmiddagarna med frontend, React och AI-kodning.

## Uppgift

- Skapa ett fristående Spring Boot projekt som innehåller User Service i ett eget GitHub-repo.
- Demo av kod och funktionalitet måndag 1/9 kl 9:00.
- Använd allt ni lärt er under utbildningen, SonarCube, loggning, envirnment variables, Unit-tester mm etc.

## Gruppindelning

### Grupp 1

- Alexander
- Erik
- Kalid
- Ludvig
- Robin

### Grupp 2

- Ali
- Angelica
- David
- Ismete
- Johan
