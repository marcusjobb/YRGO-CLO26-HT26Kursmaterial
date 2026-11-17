using AiChat.Llm;
using AiChat.Models;
using AiChat.Storage;

namespace AiChat.Services;

/// <summary>
/// Sammanfattar samtalet, så att historiken blir kort men faktan finns kvar.
/// </summary>
public sealed class Compactor
{
    private const string Prompt =
        "Sammanfatta samtalet nedan till en kort anteckning på samtalets språk, i punktform. " +
        "Behåll fakta, namn, siffror och beslut. Hitta inte på något som inte nämndes.";

    /// <summary>
    /// Sammanfattar det modellen ser just nu, och sparar sammanfattningen i historiken.
    /// </summary>
    /// <param name="history">Historiken.</param>
    /// <param name="llm">Modellen som sammanfattar.</param>
    /// <param name="model">Modellens namn.</param>
    /// <param name="ct">Avbryt anropet.</param>
    /// <returns>True om något sparades, annars false.</returns>
    public async Task<bool> CompactAsync(HistoryStore history, ILlmClient llm, string model, CancellationToken ct = default)
    {
        var context = history.Context();
        if (context.Count < 2)
        {
            return false;
        }

        var transcript = string.Join("\n", context.Select(message => $"{message.Role}: {message.Content}"));
        var messages = new List<ChatMessage>
        {
            new("system", Prompt),
            new("user", transcript),
        };

        var reply = await llm.CompleteAsync(model, messages, tools: null, ct);
        var summary = reply.Text.Trim();
        if (summary.Length == 0)
        {
            return false;
        }

        history.Append(new HistoryEvent("compact", string.Empty, summary));
        return true;
    }
}
