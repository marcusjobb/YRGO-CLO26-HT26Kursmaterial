using System.Text.Json.Nodes;

namespace AiChat.Llm;

/// <summary>
/// Får se varje JSON-förfrågan och varje JSON-svar, så att de kan sparas för granskning.
/// </summary>
public interface ICommunicationLog
{
    /// <summary>
    /// Skriver en post i loggen.
    /// </summary>
    /// <param name="direction">"request" eller "response".</param>
    /// <param name="body">Hela JSON-kroppen.</param>
    void Record(string direction, JsonNode? body);
}
