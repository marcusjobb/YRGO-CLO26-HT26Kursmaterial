# Skapa mappar i din IDE

När du delar upp klasser i egna filer är det bra att organisera dem i mappar.
I exemplet `Djur_arv` skapade vi mappen `MinaDjur` och la `Djur.cs`, `Hund.cs` m.fl. där.

> **OBS:** Mappnamnet matchar ofta namespace-namnet — det är konventionen i C#.

---

## Visual Studio (2022)

1. Högerklicka på **projektet** i Solution Explorer
2. Välj **Add → New Folder**
3. Döp mappen (t.ex. `MinaDjur`)
4. Högerklicka på den nya mappen → **Add → Class...**

> Namespaced skapas automatiskt baserat på mappstrukturen om du använder "Add → Class".

---

## VS Code

1. Klicka på **mappikonen** (New Folder) bredvid projektnamnet i Explorer-panelen
   — eller högerklicka på projektmappen → **New Folder**
2. Döp mappen
3. Högerklicka på den → **New File** → döp den till `Djur.cs` etc.

> I VS Code sätts **inte** namespace automatiskt — du behöver skriva det själv i filen.

---

## Rider (JetBrains)

1. Högerklicka på **projektet** i Solution Explorer
2. Välj **Add → Directory**
3. Döp mappen
4. Högerklicka på den → **Add → C# Class**

> Rider frågar om du vill uppdatera namespace baserat på mappsökvägen — välj **Yes**.

---

## Namespace och mappar hänger ihop

Om din mapp heter `MinaDjur` och projektet heter `Djur_arv` bör namespace vara:

```csharp
namespace Djur_arv.MinaDjur;
```

Filer utanför mappen (t.ex. `Program.cs`) importerar med:

```csharp
using Djur_arv.MinaDjur;
```
