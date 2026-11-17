using System.Text.Json;
using System.Text.Json.Nodes;
using AiChat.Models;

namespace AiChat.Tools;

/// <summary>
/// Samlar alla verktyg, berättar för modellen vilka som finns, och kör det modellen ber om.
/// </summary>
public sealed class ToolRegistry
{
    private readonly Dictionary<string, ITool> _tools;

    /// <summary>
    /// Skapar registret av de verktyg vi vill ge modellen.
    /// </summary>
    /// <param name="tools">Verktygen.</param>
    public ToolRegistry(IEnumerable<ITool> tools)
    {
        _tools = tools.ToDictionary(tool => tool.Name);
    }

    /// <summary>
    /// Verktygens beskrivningar i det format API:t förväntar sig.
    /// </summary>
    public JsonArray Specs => new(_tools.Values.Select(tool => (JsonNode)new JsonObject
    {
        ["type"] = "function",
        ["function"] = new JsonObject
        {
            ["name"] = tool.Name,
            ["description"] = tool.Description,
            ["parameters"] = tool.Parameters.DeepClone(),
        },
    }).ToArray());

    /// <summary>
    /// Kör ett verktygsanrop. Fel blir en text, så att modellen kan se felet och försöka igen.
    /// </summary>
    /// <param name="call">Anropet från modellen.</param>
    public string Execute(ToolCall call)
    {
        if (!_tools.TryGetValue(call.Name, out var tool))
        {
            return $"FEL: okänt verktyg {call.Name}.";
        }

        try
        {
            using var document = JsonDocument.Parse(call.Arguments);
            return tool.Run(document.RootElement);
        }
        catch (Exception ex) when (ex is JsonException or KeyNotFoundException or InvalidOperationException
                                       or ArgumentException or IOException or UnauthorizedAccessException)
        {
            return $"FEL: {ex.Message}";
        }
    }
}
