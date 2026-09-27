# UML-klassdiagram — BankAccount

Det här är klassen `BankAccount` ritad i UML-format.  
Du ser samma information som i koden — men utan kod.

```
┌─────────────────────────────┐
│         BankAccount         │
├─────────────────────────────┤
│ - ägare : string            │
│ - saldo : double            │
│ - ärAktivt : bool           │
├─────────────────────────────┤
│ + BankAccount(ägare,        │
│     startSaldo)             │
│ + SättIn(belopp)            │
│ + TaUt(belopp) : bool       │
│ + Presentera()              │
└─────────────────────────────┘
```

| Tecken | Betyder |
|--------|---------|
| `-`    | private — bara klassen kan nå det |
| `+`    | public — synligt utifrån |

UML är ett gemensamt språk för att rita klasser utan att skriva kod.  
Det används för att planera och kommunicera design med andra.
