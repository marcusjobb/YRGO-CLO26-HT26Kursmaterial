namespace AiChat.Models;

/// <summary>
/// Svaret från modellen: text, verktygsanrop eller båda, och hur många tokens det kostade.
/// </summary>
/// <param name="Text">Texten i svaret. Kan vara tom när modellen bara anropar verktyg.</param>
/// <param name="ToolCalls">Verktygsanrop som modellen vill göra. Tom lista betyder att svaret är klart.</param>
/// <param name="Usage">Tokens för anropet, om API:t skickade dem.</param>
public sealed record LlmReply(string Text, IReadOnlyList<ToolCall> ToolCalls, TokenUsage? Usage = null);
