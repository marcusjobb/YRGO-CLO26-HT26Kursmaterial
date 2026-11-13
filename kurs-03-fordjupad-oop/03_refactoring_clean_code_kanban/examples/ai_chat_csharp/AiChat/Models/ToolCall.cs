namespace AiChat.Models;

/// <summary>
/// Ett verktygsanrop som modellen har bett om.
/// </summary>
/// <param name="Id">Id som svaret i verktygsmeddelandet måste använda.</param>
/// <param name="Name">Verktygets namn, till exempel "read_file".</param>
/// <param name="Arguments">Argumenten som JSON-text.</param>
public sealed record ToolCall(string Id, string Name, string Arguments);
