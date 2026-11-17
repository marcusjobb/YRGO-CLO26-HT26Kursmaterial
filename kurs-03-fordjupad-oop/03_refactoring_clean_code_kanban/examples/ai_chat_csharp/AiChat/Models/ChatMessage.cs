namespace AiChat.Models;

/// <summary>
/// Ett meddelande som skickas till modellen.
/// Roll är "system", "user", "assistant" eller "tool".
/// </summary>
/// <param name="Role">Vem som säger det.</param>
/// <param name="Content">Texten i meddelandet.</param>
/// <param name="ToolCalls">Verktygsanrop som assistenten vill göra, om några.</param>
/// <param name="ToolCallId">Vilket verktygsanrop ett "tool"-meddelande svarar på.</param>
public sealed record ChatMessage(
    string Role,
    string Content,
    IReadOnlyList<ToolCall>? ToolCalls = null,
    string? ToolCallId = null);
