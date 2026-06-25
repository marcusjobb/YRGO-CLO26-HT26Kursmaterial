# Whiteboard-skiss — BankAccount

*Det här ritar du på tavlan INNAN du öppnar VS Code.*
*Låt dem sitta med pennor och kopiera — det tar 3 minuter och de minns det.*

---

## UML-klassdiagram

```
┌─────────────────────────────┐
│         BankAccount         │
├─────────────────────────────┤
│ - owner : string            │
│ - balance : decimal         │
│ - accountNumber : string    │
├─────────────────────────────┤
│ + Owner : string (get)      │
│ + Balance : decimal (get)   │
├─────────────────────────────┤
│ + BankAccount(owner,        │
│     accountNumber,          │
│     startBalance)           │
│ + Deposit(amount)           │
│ + Withdraw(amount) : bool   │
│ + PrintInfo()               │
└─────────────────────────────┘
```

**Förklara medan du ritar — inte efter:**

- Rutan = klassen. Ritningen. Inte pengarna — bankkontot.
- Tre sektioner: namn / fält / metoder
- `-` = private. `+` = public.
- Ingen av dem har skrivit en rad kod ännu — och de förstår redan vad klassen gör.

---

## Det du säger högt

> "Den här rutan är inte ett bankkonto. Det är *beskrivningen* av hur ett bankkonto ser ut.
> När vi skriver `new BankAccount(...)` i koden — *då* skapar vi ett faktiskt konto.
> Klassen är formen. Objektet är kakan."

Pausa. Låt det landa.

> "Och ser ni minustecknen? Det betyder att ingen annan kod kan röra `balance` direkt.
> Den enda som får ändra balance är klassen själv — via `Deposit` och `Withdraw`.
> Det är inkapsling. Det är därför vi gör det."

---

## Fråga att ställa INNAN du kodar

> "Om `balance` vore publik — vad skulle kunna gå fel?"

Vänta på svar. Någon säger "man kan sätta den till vad som helst".  
Exakt. Skriv det på tavlan: `konto.balance = 1000000;`  
Låt det sjunka in. Sedan: öppna VS Code.

---

## Koppling till Spellistan (presenteras tisdag)

De kommer att rita ett liknande diagram för `MusicArtist` på tisdag —
innan de börjar koda sin inlämning. Påminn dem om det.
Rita inte åt dem. Låt dem rita själva.
