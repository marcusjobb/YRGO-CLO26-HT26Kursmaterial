using AiChat.Models;

namespace AiChat.Storage;

/// <summary>
/// Samtalets historik, sparad i historik.json. Loggen växer bara: en komprimering eller en
/// återställning är också en händelse, och ingen gammal rad tas bort.
/// </summary>
public sealed class HistoryStore
{
    private readonly string _path;
    private readonly List<HistoryEvent> _events;

    /// <summary>
    /// Öppnar historiken och fortsätter från det som redan finns i filen.
    /// </summary>
    /// <param name="path">Filens sökväg, till exempel data/historik.json.</param>
    public HistoryStore(string path)
    {
        _path = path;
        _events = JsonFile.ReadList<HistoryEvent>(path);
    }

    /// <summary>
    /// Alla händelser i ordning, även komprimeringar och återställningar.
    /// </summary>
    public IReadOnlyList<HistoryEvent> Events => _events;

    /// <summary>
    /// Lägger till en händelse och sparar filen direkt.
    /// </summary>
    /// <param name="historyEvent">Händelsen.</param>
    public void Append(HistoryEvent historyEvent)
    {
        _events.Add(historyEvent);
        JsonFile.WriteList(_path, _events);
    }

    /// <summary>
    /// Det modellen ska se, härlett ur händelserna. En återställning nollställer, och en
    /// komprimering ersätter allt före sig med sin sammanfattning.
    /// </summary>
    public IReadOnlyList<ChatMessage> Context()
    {
        var context = new List<ChatMessage>();

        foreach (var historyEvent in _events)
        {
            switch (historyEvent.Type)
            {
                case "turn":
                    context.Add(new ChatMessage(historyEvent.Speaker, historyEvent.Content));
                    break;
                case "compact":
                    context.Clear();
                    context.Add(new ChatMessage("system", "Sammanfattning av tidigare samtal:\n" + historyEvent.Content));
                    break;
                case "reset":
                    context.Clear();
                    break;
            }
        }

        return context;
    }

    /// <summary>
    /// Antal turer sedan senaste komprimering eller återställning.
    /// </summary>
    public int TurnsSinceCompact()
    {
        var count = 0;
        foreach (var historyEvent in _events)
        {
            if (historyEvent.Type == "turn")
            {
                count++;
            }
            else if (historyEvent.Type is "compact" or "reset")
            {
                count = 0;
            }
        }

        return count;
    }
}
