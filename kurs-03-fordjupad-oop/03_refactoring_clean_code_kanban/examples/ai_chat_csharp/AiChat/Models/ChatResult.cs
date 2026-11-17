namespace AiChat.Models;

/// <summary>
/// Svaret på ett meddelande, med det som behövs för att förklara vad som hände.
/// </summary>
/// <param name="Text">Modellens svar.</param>
/// <param name="Usage">Tokens för alla anrop i svaret, om API:t skickade dem.</param>
/// <param name="ContextMessages">Hur många meddelanden som skickades med i det första anropet.</param>
public sealed record ChatResult(string Text, TokenUsage? Usage, int ContextMessages);
