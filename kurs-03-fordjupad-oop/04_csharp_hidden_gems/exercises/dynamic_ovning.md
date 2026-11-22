# Dynamic: frivillig övning

**Frivillig.** Gör den om du vill träna mer. Den är inte obligatorisk, och den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgifter

1. Skriv ett program med `dynamic x = 5;`. Byt till en sträng och skriv ut längden.
2. Lägg till `x.Foo();` och kör. Vad händer? Varför kompilerar det?
3. Byt `dynamic` till `var` och kör igen. Vad ändras i felmeddelandet?

<details markdown="1">
<summary>Lösningsförslag</summary>

Svar: Steg 2 kompilerar eftersom kompilatorn inte kontrollerar `dynamic`. Det kraschar i körningen med `RuntimeBinderException`. Med `var` får du ett kompileringsfel direkt.

</details>
