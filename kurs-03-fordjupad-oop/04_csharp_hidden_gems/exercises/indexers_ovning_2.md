# En spellista

**Frivillig.** Gör den om du vill träna mer. Den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgift

Skapa `class Playlist` med en lista av låtnamn. Lägg till en indexer så att `playlist[0]` ger första låten, och en `Count`-property. Skriv ut alla låtar med en loop över indexern.

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
class Playlist
{
    private readonly List<string> songs = new();

    public void Add(string song) => songs.Add(song);
    public int Count => songs.Count;
    public string this[int index] => songs[index];
}

var playlist = new Playlist();
playlist.Add("Yesterday");
playlist.Add("Creep");
for (int i = 0; i < playlist.Count; i++)
    Console.WriteLine(playlist[i]);
```

</details>
