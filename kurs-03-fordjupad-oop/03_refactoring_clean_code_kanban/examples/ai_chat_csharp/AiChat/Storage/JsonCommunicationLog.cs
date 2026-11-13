using System.Text.Json.Nodes;
using AiChat.Llm;
using AiChat.Models;

namespace AiChat.Storage;

/// <summary>
/// Sparar all kommunikation med modellen i en JSON-fil, så att du kan läsa hela samtalet
/// precis som det skickades och togs emot.
/// </summary>
public sealed class JsonCommunicationLog : ICommunicationLog
{
    private readonly string _path;
    private readonly List<CommunicationEntry> _entries;

    /// <summary>
    /// Öppnar loggen och fortsätter från det som redan finns i filen.
    /// </summary>
    /// <param name="path">Filens sökväg, till exempel data/kommunikation.json.</param>
    public JsonCommunicationLog(string path)
    {
        _path = path;
        _entries = JsonFile.ReadList<CommunicationEntry>(path);
    }

    /// <inheritdoc />
    public void Record(string direction, JsonNode? body)
    {
        _entries.Add(new CommunicationEntry(DateTimeOffset.Now, direction, body));
        JsonFile.WriteList(_path, _entries);
    }
}
