using System.Text.Json;
using System.Text.Json.Nodes;

namespace AiChat.Tools;

/// <summary>
/// Skriver en textfil i arbetsytan. Skriver över om filen redan finns.
/// </summary>
public sealed class WriteFileTool(Workspace workspace) : ITool
{
    /// <inheritdoc />
    public string Name => "write_file";

    /// <inheritdoc />
    public string Description => "Skriver text till en fil i arbetsytan. Skriver över om filen finns.";

    /// <inheritdoc />
    public JsonObject Parameters => new()
    {
        ["type"] = "object",
        ["properties"] = new JsonObject
        {
            ["path"] = new JsonObject { ["type"] = "string", ["description"] = "Filens sökväg i arbetsytan" },
            ["content"] = new JsonObject { ["type"] = "string", ["description"] = "Texten som ska skrivas" },
        },
        ["required"] = new JsonArray("path", "content"),
    };

    /// <inheritdoc />
    public string Run(JsonElement arguments)
    {
        var path = arguments.GetProperty("path").GetString() ?? string.Empty;
        var content = arguments.GetProperty("content").GetString() ?? string.Empty;
        var full = workspace.Resolve(path);

        Directory.CreateDirectory(Path.GetDirectoryName(full)!);
        File.WriteAllText(full, content);
        return $"Sparade {path} ({content.Length} tecken).";
    }
}
