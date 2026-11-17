using System.Text.Json.Nodes;
using AiChat.Models;

namespace AiChat.Llm;

/// <summary>
/// Det som chatten behöver från en språkmodell. Gör det enkelt att byta ut mot en låtsasmodell i tester.
/// </summary>
public interface ILlmClient
{
    /// <summary>
    /// Skickar meddelanden och får tillbaka ett svar.
    /// </summary>
    /// <param name="model">Modellens namn.</param>
    /// <param name="messages">Meddelandena i ordning.</param>
    /// <param name="tools">Verktygens beskrivningar, eller null när inga verktyg ska användas.</param>
    /// <param name="ct">Avbryt anropet.</param>
    Task<LlmReply> CompleteAsync(
        string model,
        IReadOnlyList<ChatMessage> messages,
        JsonArray? tools,
        CancellationToken ct = default);
}
