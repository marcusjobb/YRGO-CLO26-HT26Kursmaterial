# Hellweek: C# hidden gems

Den här veckan är inte tentastoff. Det är funktioner som dyker upp överallt i riktig kod, och i kod som AI skriver. Nästa gång du ser dem vet du vad de gör.

## Så här är veckan upplagd

- **Varje funktion har ett kort bildspel**, med ett eller två kodexempel och ett wow-moment.
- **Varje funktion har tre frivilliga övningar**, som du kan göra i den ordning du vill.
- **Ingen bedömning.** Det här är för att du ska känna igen dem, inte för att kunna dem utantill.

## Funktionerna i korthet

| Funktion | Vad den gör | Varför den är bra att känna till |
|---|---|---|
| `dynamic` | Typen avgörs först när koden körs | Du ser den i äldre kod och i JSON-arbete |
| Pattern matching | Kontrollerar form och värde i ett steg | Kortare och tydligare `if`-kedjor |
| Tuples | Två eller flera värden utan en ny klass | Snabbt när värdena inte behöver egna regler |
| Indexers | Ett objekt används med `[ ]` | Gör en klass lik en lista eller en ordbok |
| Null-operatorer | `?.`, `??` och `??=` | Färre `if (x != null)` |
| Extension methods | Metoder på typer du inte äger | Hjälpmetoder som ser ut som vanliga metoder |
| Records | Data med värdelikhet och `with` | Bra för data som inte ska ändras |
| async och await | Vänta utan att frysa programmet | Nätverksanrop och filer |

## Ett råd

När du ser en funktion du inte känner igen, fråga dig: vilket problem löser det här? Om du inte kan svara, är det troligen ett tecken på att koden kan bli enklare.
