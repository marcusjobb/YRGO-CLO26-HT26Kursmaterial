using System.Text.Json;
using System.Text.Json.Nodes;

namespace AiChat.Tools;

/// <summary>
/// Ett verktyg som modellen kan be att få använda. Varje verktyg gör en sak.
/// </summary>
public interface ITool
{
    /// <summary>Namnet modellen använder, till exempel "read_file".</summary>
    string Name { get; }

    /// <summary>Förklaring som hjälper modellen att välja rätt verktyg.</summary>
    string Description { get; }

    /// <summary>Beskrivningen av argumenten i JSON Schema.</summary>
    JsonObject Parameters { get; }

    /// <summary>
    /// Kör verktyget. Returnerar en text som går tillbaka till modellen.
    /// </summary>
    /// <param name="arguments">Argumenten som modellen skickade.</param>
    string Run(JsonElement arguments);
}
