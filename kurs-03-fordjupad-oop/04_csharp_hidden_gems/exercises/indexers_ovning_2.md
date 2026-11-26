# En spellista

**Frivillig.** Gör den om du vill träna mer. Den ingår inte i bedömningen.

**Nivå:** 🟡

**Tid:** ca 20 minuter.

## Uppgift

Skapa `class Playlist` med en lista av låtnamn. Lägg till en indexer så att `playlist[0]` ger första låten, och en `Count`-property. Skriv ut alla låtar med en loop över indexern.

Fyll i det som står `/* din kod */`.

```csharp
var playlist = new Playlist();
playlist.Add("Yesterday");
playlist.Add("Creep");
/* din kod:
   loopa från 0 upp till antalet låtar
   skriv ut låten på varje plats */

class Playlist
{
    private readonly List<string> songs = new();

    public void Add(string song) => songs.Add(song);

    /* din kod:
       en Count-property som ger antalet låtar i listan
       en indexer som tar ett index och ger låten på den platsen */
}
```

Exempeloutput:

```text
Yesterday
Creep
```

<details markdown="1">
<summary>Tips 1: Count</summary>

Listan vet redan hur många låtar den har. Din `Count` kan bara lämna vidare `songs.Count`. En property med `=>` räcker.

</details>

<details markdown="1">
<summary>Tips 2: hur ser en indexer ut?</summary>

En indexer heter `this` och har parametern inom hakparenteser: `public string this[int index]`. Den behöver bara `get` här, så du kan skriva den med `=>`.

</details>

<details markdown="1">
<summary>Tips 3: loopen</summary>

Använd en vanlig `for`-loop med `i < playlist.Count`. Inuti skriver du ut `playlist[i]`. Då anropas din indexer en gång per låt.

</details>

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
var playlist = new Playlist();
playlist.Add("Yesterday");
playlist.Add("Creep");
for (int i = 0; i < playlist.Count; i++)
    Console.WriteLine(playlist[i]);

class Playlist
{
    private readonly List<string> songs = new();

    public void Add(string song) => songs.Add(song);
    public int Count => songs.Count;
    public string this[int index] => songs[index];
}
```

</details>

<!-- nion-copyright -->
<sub>© 2025–2026 Nion Education. Alla rättigheter förbehållna. Fritt att läsa, men får inte användas av andra skolor eller utbildningsanordnare utan skriftligt tillstånd från Nion Education. Delar bygger på källor som anges i respektive dokument.</sub>
