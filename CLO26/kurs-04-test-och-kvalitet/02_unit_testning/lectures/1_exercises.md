# 1. Introduktion till statisk kodanalys

🟢


Exercises:

Statisk kodanalys i C# - övningsuppgifter

1. Identifiera potentiella problem

Beskrivning: Granska följande C#-kod och identifiera minst tre potentiella problem som en statisk kodanalys skulle kunna upptäcka.

Instruktioner:
a) Läs igenom koden noggrant.
b) Skriv ner minst tre specifika problem du identifierar.
c) För varje problem, förklara kort varför det är problematiskt och hur det kan åtgärdas.

Förväntat resultat: En lista med minst tre identifierade problem, inklusive förklaringar och förslag på åtgärder.

```csharp
public class User
{
    public string username;
    public int Age;

    public string GetInfo()
    {
        return username + " is " + Age + " years old";
    }

    public void UpdateAge(int newAge)
    {
        if (newAge > 0)
            Age = newAge;
    }
}
```

2. Konfigurera ReSharper

Beskrivning: Konfigurera ReSharper för att analysera ett enkelt C#-projekt.

Instruktioner:
a) Skapa ett nytt C#-konsolprojekt i Visual Studio.
b) Installera ReSharper om det inte redan är installerat.
c) Öppna ReSharper-inställningarna och aktivera följande regler:

- Namngivningskonventioner för variabler
- Onödig kodanvändning
- Potentiella nullreferensexceptions
d) Skriv en enkel klass med några metoder och variabler.
e) Kör ReSharper-analysen och notera resultaten.

Förväntat resultat: En lista över varningar och förslag från ReSharper baserat på de aktiverade reglerna.

3. Integrera SonarQube

Beskrivning: Sätt upp SonarQube för analys av ett C#-projekt och tolka resultaten.

Instruktioner:
a) Installera SonarQube lokalt eller använd SonarCloud.
b) Konfigurera ett C#-projekt för analys med SonarQube.
c) Kör en analys av projektet.
d) Granska resultaten och identifiera:

- Kodens övergripande kvalitetsbetyg
- Antalet "code smells"
- Eventuella säkerhetsproblem
- Duplicerad kod
e) Välj tre problem som identifierats av SonarQube och föreslå hur de kan åtgärdas.

Förväntat resultat: En sammanfattning av SonarQube-analysens resultat och förslag på åtgärder för tre specifika problem.

4. Skapa en anpassad analysregel

Beskrivning: Utveckla en enkel anpassad regel för ReSharper som identifierar metoder med för många parametrar.

Instruktioner:
a) Skapa en ny klass som ärver från ElementProblemAnalyzer<IMethodDeclaration>.
b) Implementera logik som flaggar metoder med mer än 5 parametrar.
c) Testa regeln på ett exempelprojekt med olika metoder.
d) Dokumentera hur regeln fungerar och motivera varför den är användbar.

Förväntat resultat: Källkod för den anpassade regeln, exempel på hur den fungerar i praktiken, och en kort dokumentation.

5. Analysera och förbättra kodtäckning

Beskrivning: Använd dotCover för att mäta och förbättra kodtäckningen i ett C#-projekt.

Instruktioner:
a) Skapa ett enkelt C#-biblioteksprojekt med minst tre klasser och tillhörande enhetstester.
b) Kör dotCover för att mäta den initiala kodtäckningen.
c) Identifiera områden med låg täckning.
d) Skriv ytterligare enhetstester för att öka täckningen.
e) Kör dotCover igen och jämför resultaten.
f) Reflektera över processen och vilka lärdomar du kan dra om effektiv testning.

Förväntat resultat: En rapport som visar den initiala och förbättrade kodtäckningen, samt en reflektion över processen och lärdomar.

---
Det här är grunden. Öva på den, lek med koden, gör misstag. Det är så du lär dig.
