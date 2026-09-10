# Övning — Gissa talet

> 🗺️ **Rita ett flödesschema innan du kodar.** Skissa upp programflödet på papper — vilka steg tas? Vilka beslut fattas? Rita klart, lägg ner pennan, öppna sedan VS Code.

🟡

På rasten sitter Vera och Kim och spelar ett spel — en tänker på ett tal, den andra gissar. Det är kul men tröttsamt. Du bestämmer dig för att skriva en digital version: datorn tänker på ett tal mellan 1 och 20, du gissar tills du träffar.

---

## Steg 1: Slumpa ett tal och ta emot gissningar

Datorn slumpar ett hemligt tal med `Random`. Använd en `do-while`-loop som fortsätter tills gissningen är rätt. För varje fel gissning: skriv ut om svaret är för högt eller för lågt.

### Förväntad output (exempel — ditt tal varierar)
```plaintext
Gissa ett tal mellan 1 och 20!

Din gissning: 10
För högt!

Din gissning: 5
För lågt!

Din gissning: 7
För lågt!

Din gissning: 9
Rätt! Du klarade det på 4 försök.
```

## Steg 2: Vad händer om du gissar rätt på första försöket?

```plaintext
Gissa ett tal mellan 1 och 20!

Din gissning: 13
Rätt! Du klarade det på 1 försök.
```

Tänk på: "1 försök" — inte "1 försöks".
