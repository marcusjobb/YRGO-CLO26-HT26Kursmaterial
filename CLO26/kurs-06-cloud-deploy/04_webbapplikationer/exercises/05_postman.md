---

title: Testa Spring Boot Kalkylator med Postman
author: Marcus Ackre Medina
type: exercise
topic: api
difficulty: 1
language: python
status: adapted
marcus_voice: true
source: "Old_courses/2025/java/SysInt/exercises/springapi/05_postman.md"
description: "För att testa din Spring Boot-applikations RESTful-tjänster med Postman, följ dessa steg för varje matematisk operation."
tags: ["api", "boot", "exercise", "installation", "kalkylator", "postman", "spring", "testa", "visual-studio"]
week_fit: []
---

# Testa Spring Boot Kalkylator med Postman

🟢


För att testa din Spring Boot-applikations RESTful-tjänster med Postman, följ dessa steg för varje matematisk operation.
Vi kommer att göra anrop för addition, subtraktion, multiplikation, division, och ett specialfall för division med noll.

Ladda ner Postman [här](https://www.postman.com/downloads/).

## Förberedelser

Innan du börjar, se till att din Spring Boot-applikation är igång och lyssnar på port 8080. Du bör ha Postman
installerat och öppnat på din dator.

## Gör Anrop

### Addition

1. **Välj metod**: GET
2. **Ange URL**: `http://localhost:8080/add?a=10&b=5`
3. **Skicka anropet** och observera svaret. Du bör se "Resultat: 15".

### Subtraktion

1. **Välj metod**: GET
2. **Ange URL**: `http://localhost:8080/subtract?a=10&b=5`
3. **Skicka anropet** och observera svaret. Du bör se "Resultat: 5".

### Multiplikation

1. **Välj metod**: GET
2. **Ange URL**: `http://localhost:8080/multiply?a=10&b=5`
3. **Skicka anropet** och observera svaret. Du bör se "Resultat: 50".

### Division

1. **Välj metod**: GET
2. **Ange URL**: `http://localhost:8080/divide?a=10&b=5`
3. **Skicka anropet** och observera svaret. Du bör se "Resultat: 2".

### Division med Noll

1. **Välj metod**: GET
2. **Ange URL**: `http://localhost:8080/divide?a=10&b=0`
3. **Skicka anropet**. Eftersom division med noll inte är tillåtet, bör du se ett lämpligt felmeddelande, exempelvis "
   Fel: Division med noll är inte tillåtet."

## Tips för Felsökning i Postman

- **Om du inte får något svar**: Kontrollera att din applikation körs korrekt och att URL:en är korrekt formaterad.

- **Felmeddelanden**: Läs igenom svaret noggrant. Felmeddelanden kan ge viktig information om vad som gick fel.

- **Loggar**: Se över loggarna i din Spring Boot-applikation om du stöter på oväntade problem eller serverfel.

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
