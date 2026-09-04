# Övning 3 — Hello World

🟢 Grundnivå

---

Datorn vet inte vem du är än.

Det är dags att presentera dig.

---

## Steg 1 — Skapa projektet

Öppna din IDE. Skapa ett nytt **Console App**-projekt och döp det till `HelloWorld`.

```
Fil → Nytt projekt → Console App → .NET 10
```

---

## Steg 2 — Kopiera exempelkoden

Öppna `Program.cs` och ersätt innehållet med koden från `examples/HelloWorld.cs`.

---

## Steg 3 — Presentera dig

Hitta raden:

```csharp
Console.WriteLine("Hello, World!");
```

Byt ut `World` mot ditt namn.

---

## Steg 4 — Kör programmet

Tryck **F5** (Visual Studio) eller kör i terminalen:

```bash
dotnet run
```

Om allt fungerar ska du se något i stil med:

```
=== SYSTEM ONLINE ===
Söker efter användare...

Hello, Anna!

Anslutning upprättad.
```

Grattis — din dator vet nu vem du är.

---

## Steg 5 — Committa och pusha

```bash
git add .
git commit -m "Lägg till HelloWorld — första programmet"
git push
```

Gå till GitHub och verifiera att koden syns där.

---

## Klart!

Du har precis:
- Skapat ett C#-projekt från grunden
- Ändrat och kört din första kod
- Committat och pushat till GitHub

Allt fungerar. Miljön är redo. Nu kör vi på riktigt.
