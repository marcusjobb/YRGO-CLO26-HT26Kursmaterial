---
title: Testa Spring Boot REST-API i webbläsaren
author: Marcus Ackre Medina
type: exercise
topic: api
difficulty: 1
language: java
status: adapted
marcus_voice: true
source: "Old_courses/2025/java/SysInt/exercises/springapi/04_browser_instructions.md"
description: "Testa din Spring Boot-applikations REST-endpoints direkt i webbläsaren — addera, subtrahera, multiplicera och dividera via URL:en."
tags: ["java", "spring", "spring-boot", "rest", "api", "webservice"]
week_fit: []
---

# Testa Spring Boot REST-API i webbläsaren

🟢

**Scenario:** Du har byggt ett REST-API med Spring Boot med en kalkylator. Nu ska du testa endpoints — utan Postman, bara webbläsaren.

## Steg för steg

Se till att din Spring Boot-applikation körs (`DemoApplication` eller motsvarande). Öppna webbläsaren och prova URLerna nedan.

Standardport är `8080` → `http://localhost:8080`

### Addition

```bash
http://localhost:8080/add?a=10&b=5
```

Förväntat svar: `Resultat: 15`

### Subtraktion

```bash
http://localhost:8080/subtract?a=10&b=5
```

Förväntat svar: `Resultat: 5`

### Multiplikation

```bash
http://localhost:8080/multiply?a=10&b=5
```

Förväntat svar: `Resultat: 50`

### Division

```bash
http://localhost:8080/divide?a=10&b=5
```

Förväntat svar: `Resultat: 2`

```bash
http://localhost:8080/divide?a=10&b=0
```

Hanterar den division med noll utan att krascha?

---

<details>
<summary>💡 Tips – Felsökning</summary>

- Appen nås inte? Kolla att den startat i IntelliJ (leta efter "Started..." i konsollen)
- Fel svar? Dubbelkolla URL och parametrar — `?a=10&b=5` måste vara exakt rätt
- Server error? Läs loggen i IntelliJ, där står exakt vad som gick fel

</details>

---

<details>
<summary>✅ Exempel-endpoints i Spring Boot</summary>

```java
@RestController
public class CalculatorController {

    @GetMapping("/add")
    public String add(@RequestParam int a, @RequestParam int b) {
        return "Resultat: " + (a + b);
    }

    @GetMapping("/subtract")
    public String subtract(@RequestParam int a, @RequestParam int b) {
        return "Resultat: " + (a - b);
    }

    @GetMapping("/multiply")
    public String multiply(@RequestParam int a, @RequestParam int b) {
        return "Resultat: " + (a * b);
    }

    @GetMapping("/divide")
    public String divide(@RequestParam int a, @RequestParam int b) {
        if (b == 0) return "Fel: Division med noll!";
        return "Resultat: " + (a / b);
    }
}
```

</details>

---

## Reflektion

- Varför används `@RequestParam` här istället för `@PathVariable`?
- Hur skulle du skicka in parametrar via POST istället?
- Vad händer om användaren inte skickar med `a` eller `b`?

---

_Det här är grunden. Öva på den, lek med koden, gör misstag. Det är så du lär dig._
