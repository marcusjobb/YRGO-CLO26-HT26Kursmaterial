# Exempel: designmönster i C#

Alla exempel från designmönsterpasset, samlade med en förklaring till varje kodchunk. Projekten ligger i `source/repos`.

| Fil | Projekt | Mönster |
|---|---|---|
| [builder.md](builder.md) | `Designmonster_Builder`, `_Builder_Karaktar`, `_Builder_Titel` | Builder |
| [factory.md](factory.md) | `Designmonster_Factory`, `_Factory_Monster`, `_Factory_Vapen` | Factory |
| [strategy.md](strategy.md) | `Designmonster_Strategy`, `_Strategy_Hero`, `_Strategy_Resa` | Strategy |
| [observer.md](observer.md) | `Designmonster_Observer`, `_Observer_Bank`, `_Observer_Bank_Event`, `Observer_Live` | Observer |
| [uml.md](uml.md) | alla | UML, för skojs skull |

Flödesschemat för vilket mönster som passar finns i [../notes/flodesschema_designmonster.md](../notes/flodesschema_designmonster.md).

---

## Så läser du exemplen

- Varje mönster har ett projekt med **allt i en fil**, och sedan uppdelade versioner med mappar och namespace.
- I varje kodchunk står det **vad som händer** och **stil**. Stilnoterna förklarar varför koden ser ut som den gör, och vilka val som kunde ha gjorts annorlunda.
- Projekten med `_Hero`, `_Resa`, `_Karaktar` och liknande är kopior av delar av det större projektet. De finns för att kunna öva en del i taget.

## Stil som återkommer

- **Namn:** klasser och metoder på engelska, men utskrifter och kommentarer på svenska. Undantaget är Mira, Grölf och liknande namn, som är påhittade karaktärer.
- **Konstruktorer** sätter alla värden, och properties med `{ get; }` ändras inte efteråt.
- **Expression-bodied members** (`=> ...`) för metoder som bara har en rad.
- **Switch-uttryck** (`switch` med `=>`) för val mellan några få alternativ, och `default` med `throw` för okända värden.
- **Interface** med `I` i början, och metoder med `On` i början när de är händelsehanterare.
- **Kommentarer** som säger *varför*, inte bara vad. Till exempel: "Hero ändras inte när ett nytt vapen läggs till."

## Obs

- Projekten är skrivna och granskade, men de är inte körda i den här genomgången. Kör dem i Visual Studio för att se utskrifterna.
- Två ställen är värda att titta extra på: `Observer_Live` skriver ut "Tar ut" även när uttaget misslyckas, och `WeaponFactory` i `Designmonster_Factory_Vapen` använder samma klass för `"sword"` och `"rusty sword"`, men med andra värden. Det är ett exempel på att fabriken kan styra egenskaper, inte bara vilken klass som skapas.
