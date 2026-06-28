---

title: 3. SonarQube och kodtäckning med dotCover
author: Marcus Ackre Medina
type: lecture
topic: testing
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/2025/csharp/4_test/lectures/10_static_code_analysis_and_code_quality_tools/3_exercises.md"
description: "SonarQube och dotCover övningsuppgifter"
tags: ["csharp", "dotcover", "installation", "kodtäckning", "sonarqube", "testing", "visual-studio"]
week_fit: []
---

# 3. SonarQube och kodtäckning med dotCover

🟢


Exercises:

SonarQube och dotCover övningsuppgifter

1. Grundläggande SonarQube-konfiguration

a) Beskrivning: Konfigurera SonarQube för ett nytt .NET-projekt.

b) Instruktioner:

1. Skapa ett nytt projekt i SonarQube.
2. Generera en projekttoken.
3. Installera SonarScanner för .NET globalt.
4. Skriv kommandona för att initiera en SonarQube-analys, bygga projektet och avsluta analysen.

c) Förväntat resultat: En serie kommandon som korrekt initierar, kör och avslutar en SonarQube-analys för ett .NET-projekt.

2. Integrering av dotCover i byggprocessen

a) Beskrivning: Integrera dotCover i ett .NET-projekt för kodtäckningsmätning.

b) Instruktioner:

1. Lägg till nödvändiga NuGet-paket för dotCover i projektfilen.
2. Skriv ett kommando för att köra tester med dotCover och generera en HTML-rapport.
3. Modifiera SonarQube-initieringskommandot för att inkludera dotCover-rapporten.

c) Förväntat resultat: En uppdaterad projektfil och en serie kommandon som kör tester med dotCover och integrerar resultaten i SonarQube-analysen.

3. Implementera kvalitetsportar i SonarQube

a) Beskrivning: Definiera kvalitetsportar i SonarQube för att säkerställa kodkvalitet.

b) Instruktioner:

1. Logga in på SonarQube-servern.
2. Navigera till kvalitetsportsinställningar för ditt projekt.
3. Skapa tre kvalitetsportar:
   - En för kodtäckning (minst 80%)
   - En för duplicerad kod (max 5%)
   - En för att blockera bygget vid upptäckt av kritiska problem
4. Beskriv hur dessa kvalitetsportar påverkar CI/CD-processen.

c) Förväntat resultat: En lista över konfigurerade kvalitetsportar och en kort förklaring av deras påverkan på utvecklingsprocessen.

4. Analys och åtgärd av SonarQube-resultat

a) Beskrivning: Analysera och åtgärda problem rapporterade av SonarQube.

b) Instruktioner:

1. Kör en SonarQube-analys på följande kodexempel:

```csharp
public class UserManager
{
    public void CreateUser(string username, string password)
    {
        if (username == null || password == null)
            throw new Exception("Invalid input");

        string query = "INSERT INTO Users (Username, Password) VALUES ('" + username + "', '" + password + "')";
        // Execute query
    }
}
```

2. Identifiera minst tre problem som SonarQube sannolikt skulle rapportera.
3. Föreslå förbättringar för att åtgärda dessa problem.
4. Skriv en refaktorerad version av koden som adresserar de identifierade problemen.

c) Förväntat resultat: En lista över identifierade problem, förslag på förbättringar och en refaktorerad version av koden som följer bästa praxis.

5. Optimering av SonarQube och dotCover för stora projekt

a) Beskrivning: Föreslå strategier för att optimera användningen av SonarQube och dotCover i ett stort enterprise-projekt.

b) Instruktioner:

1. Beskriv minst tre utmaningar som kan uppstå vid användning av SonarQube och dotCover i stora projekt.
2. För varje utmaning, föreslå en lösning eller strategi för att hantera den.
3. Förklara hur man kan implementera inkrementell analys i SonarQube för att förbättra prestanda.
4. Diskutera hur man kan balansera behovet av omfattande kodanalys med utvecklarproduktivitet.

c) Förväntat resultat: En detaljerad beskrivning av utmaningar, lösningar och strategier för att optimera användningen av SonarQube och dotCover i stora projekt, med fokus på prestanda och utvecklarproduktivitet.

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
