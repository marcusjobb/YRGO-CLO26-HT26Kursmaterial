# Övning — Lösenordsförsöket

> 🗺️ **Rita ett flödesschema innan du kodar.** Skissa upp programflödet på papper — vilka steg tas? Vilka beslut fattas? Rita klart, lägg ner pennan, öppna sedan VS Code.

🟡

Spelklubben på Campus har ett hemligt forum där nästa LAN-spelkväll planeras. För att komma in måste man skriva rätt lösenord. Men alla har inte rätt — och tre felförsök spärrar kontot. Du bygger inloggningssystemet.

---

## Steg 1: Tre försök att logga in

Rätt lösenord är `kodord123`. Användaren får max 3 försök. Lyckas det inom tre försök: välkomna in. Tar försöken slut: spärra kontot.

Använd en `for`-loop som räknar försöken. Bryt ur loopen med `break` när rätt lösenord skrivits.

### Förväntad output — rätt på andra försöket
```plaintext
Ange lösenord: fel123
Fel lösenord. 2 försök kvar.

Ange lösenord: kodord123
Välkommen in!
```

### Förväntad output — alla försök felaktiga
```plaintext
Ange lösenord: abc
Fel lösenord. 2 försök kvar.

Ange lösenord: 123
Fel lösenord. 1 försök kvar.

Ange lösenord: hejsan
Fel lösenord. 0 försök kvar.
Kontot är spärrat.
```

## Steg 2: Rätt på första försöket

```plaintext
Ange lösenord: kodord123
Välkommen in!
```

Inga "försök kvar"-meddelanden behövs om man lyckas direkt.
