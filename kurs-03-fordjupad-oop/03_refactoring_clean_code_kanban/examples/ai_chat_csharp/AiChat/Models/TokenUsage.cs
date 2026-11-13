namespace AiChat.Models;

/// <summary>
/// Hur många tokens ett anrop kostade. En token är ungefär en fjärdedel av ett engelskt ord,
/// och det är tokens du betalar för, inte tecken.
/// </summary>
/// <param name="PromptTokens">Tokens som skickades in (hela kontexten).</param>
/// <param name="CompletionTokens">Tokens som modellen skrev ut (svaret).</param>
public sealed record TokenUsage(int PromptTokens, int CompletionTokens)
{
    /// <summary>
    /// Lägger ihop två anrop, till exempel när ett svar kräver flera verktygsrundor.
    /// </summary>
    public TokenUsage Add(TokenUsage other) =>
        new(PromptTokens + other.PromptTokens, CompletionTokens + other.CompletionTokens);
}
