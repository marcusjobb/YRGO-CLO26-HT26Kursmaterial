# Övning — Årstiden

> 🗺️ **Rita ett flödesschema innan du kodar.** Skissa upp programflödet på papper — vilka steg tas? Vilka beslut fattas? Rita klart, lägg ner pennan, öppna sedan VS Code.

🟢

En väderapp på telefonen visar bara månadstal — 1 till 12. Din kollega vill ha en snabb funktion som slår upp årstid baserat på månaden, för att visa rätt bakgrundsbild i appen.

---

## Steg 1: Slå upp årstiden

Läs in ett månadstal (1–12). Använd `switch` med grupperade `case`-grenar för att skriva ut rätt årstid. Kom ihåg `default` för ogiltiga värden.

Gruppering:
- December, januari, februari → Vinter
- Mars, april, maj → Vår
- Juni, juli, augusti → Sommar
- September, oktober, november → Höst

### Förväntad output
```plaintext
Ange månadstal (1-12): 7
Juli tillhör: Sommar

Ange månadstal (1-12): 11
November tillhör: Höst

Ange månadstal (1-12): 99
Okänd månad.
```

Obs: Du behöver inte skriva ut månadsnamnet — det räcker med siffran och årstiden.
