using System.Text.Json;
using System.Text.Json.Nodes;

namespace AiChat.Tools;

/// <summary>
/// Läser en textfil i arbetsytan.
/// </summary>
public sealed class ReadFileTool(Workspace workspace) : ITool
{
    private const int MaxChars = 20_000;

    /// <inheritdoc />
    public string Name => "read_file";

    /// <inheritdoc />
    public string Description => "Läser en textfil i arbetsytan och returnerar innehållet.";

    /// <inheritdoc />
    public JsonObject Parameters => new()
    {
        ["type"] = "object",
        ["properties"] = new JsonObject
        {
            ["path"] = new JsonObject { ["type"] = "string", ["description"] = "Filens sökväg i arbetsytan" },
        },
        ["required"] = new JsonArray("path"),
    };

    /// <inheritdoc />
    public string Run(JsonElement arguments)
    {
        var path = arguments.GetProperty("path").GetString() ?? string.Empty;
        var full = workspace.Resolve(path);

        if (!File.Exists(full))
        {
            return "FEL: filen finns inte.";
        }

        var text = File.ReadAllText(full);
        return text.Length <= MaxChars ? text : text[..MaxChars] + "\n(avkortad)";
    }
}
