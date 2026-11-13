using System.Text;
using AiChat.Config;
using AiChat.Llm;
using AiChat.Models;
using AiChat.Storage;
using AiChat.Tools;

namespace AiChat.Services;

/// <summary>
/// Chatten: tar emot användarens text, kör verktygsrundor tills modellen svarar med text,
/// och sparar allt i historiken.
/// </summary>
public sealed class ChatService
{
    private const int MaxToolRounds = 4;

    private readonly ILlmClient _llm;
    private readonly HistoryStore _history;
    private readonly ToolRegistry _tools;
    private readonly AppSettings _settings;
    private readonly Compactor _compactor = new();

    /// <summary>
    /// Skapar chatten.
    /// </summary>
    /// <param name="llm">Modellen.</param>
    /// <param name="history">Historiken.</param>
    /// <param name="tools">Verktygen.</param>
    /// <param name="settings">Inställningarna.</param>
    public ChatService(ILlmClient llm, HistoryStore history, ToolRegistry tools, AppSettings settings)
    {
        _llm = llm;
        _history = history;
        _tools = tools;
        _settings = settings;
    }

    /// <summary>
    /// Skickar ett meddelande och returnerar svaret, med tokens och kontextstorlek.
    /// </summary>
    /// <param name="userText">Det användaren skrev.</param>
    /// <param name="onTool">Anropas när ett verktyg körs, så att användaren ser det.</param>
    /// <param name="ct">Avbryt anropet.</param>
    public async Task<ChatResult> SendAsync(string userText, Action<string>? onTool = null, CancellationToken ct = default)
    {
        if (_settings.UseHistory && _history.TurnsSinceCompact() >= _settings.CompactAt)
        {
            await CompactAsync(ct);
        }

        // Allt sparas i historik.json, även när historiken inte skickas med till modellen.
        _history.Append(new HistoryEvent("turn", "user", userText));

        var messages = BuildMessages(userText);
        var contextSize = messages.Count;
        var (text, usage) = await RunToolRoundsAsync(messages, onTool, ct);

        _history.Append(new HistoryEvent("turn", "assistant", text));
        return new ChatResult(text, usage, contextSize);
    }

    /// <summary>
    /// Sammanfattar samtalet på begäran.
    /// </summary>
    /// <param name="ct">Avbryt anropet.</param>
    /// <returns>True om något komprimerades.</returns>
    public Task<bool> CompactAsync(CancellationToken ct = default)
    {
        return _compactor.CompactAsync(_history, _llm, _settings.CompactModel, ct);
    }

    /// <summary>
    /// Rensar historiken för modellen. Loggen i historik.json finns kvar.
    /// </summary>
    public void Reset()
    {
        _history.Append(new HistoryEvent("reset", string.Empty, string.Empty));
    }

    /// <summary>
    /// Beskriver kontexten precis som modellen får den: vilken roll, hur många tecken, och
    /// en uppskattning av tokens (ungefär ett token per fyra tecken).
    /// </summary>
    public string DescribeContext()
    {
        var messages = BuildMessages(string.Empty);
        var sb = new StringBuilder();
        sb.AppendLine($"Kontexten har {messages.Count} meddelanden, ungefär {EstimateTokens(messages)} tokens.");
        sb.AppendLine($"Historiken i historik.json har {_history.Events.Count} händelser totalt.");

        foreach (var message in messages)
        {
            var preview = message.Content.Length > 60 ? message.Content[..60] + "…" : message.Content;
            sb.AppendLine($"  [{message.Role}] {preview.Replace('\n', ' ')}");
        }

        return sb.ToString().TrimEnd();
    }

    /// <summary>
    /// Bygger de meddelanden som skickas. Utan historik skickas bara det nya meddelandet.
    /// </summary>
    private List<ChatMessage> BuildMessages(string userText)
    {
        var messages = new List<ChatMessage>
        {
            new("system", SystemPrompt()),
        };

        if (_settings.UseHistory)
        {
            messages.AddRange(Window(_history.Context()));
        }
        else
        {
            messages.Add(new ChatMessage("user", userText));
        }

        return messages;
    }

    /// <summary>
    /// Systemprompten: vem assistenten är, och vilken personlighet den har.
    /// </summary>
    private string SystemPrompt()
    {
        const string basePrompt = "Du är en hjälpsam assistent. Du kan läsa och skriva filer i arbetsytan med verktygen.";
        return _settings.Persona is null ? basePrompt : $"{_settings.Persona.Instruction}\n\n{basePrompt}";
    }

    /// <summary>
    /// Behåller sammanfattningen och de senaste turerna efter den.
    /// </summary>
    private IEnumerable<ChatMessage> Window(IReadOnlyList<ChatMessage> context)
    {
        var fixedPart = context.Count > 0 && context[0].Role == "system" ? context.Take(1).ToList() : [];
        var rest = context.Skip(fixedPart.Count);
        return fixedPart.Concat(rest.TakeLast(_settings.Window));
    }

    /// <summary>
    /// Kör verktygsrundor tills modellen svarar med text, eller tills vi nått gränsen.
    /// Summerar tokens för alla anrop.
    /// </summary>
    private async Task<(string Text, TokenUsage? Usage)> RunToolRoundsAsync(
        List<ChatMessage> messages,
        Action<string>? onTool,
        CancellationToken ct)
    {
        TokenUsage? total = null;

        for (var round = 0; round < MaxToolRounds; round++)
        {
            var reply = await _llm.CompleteAsync(_settings.Model, messages, _tools.Specs, ct);
            total = Sum(total, reply.Usage);

            if (reply.ToolCalls.Count == 0)
            {
                return (reply.Text, total);
            }

            messages.Add(new ChatMessage("assistant", reply.Text, reply.ToolCalls));
            foreach (var call in reply.ToolCalls)
            {
                onTool?.Invoke($"{call.Name}({call.Arguments})");
                var result = _tools.Execute(call);
                messages.Add(new ChatMessage("tool", result, ToolCallId: call.Id));
            }
        }

        return ("(Avbröt: för många verktygsrundor.)", total);
    }

    private static TokenUsage? Sum(TokenUsage? total, TokenUsage? next)
    {
        if (next is null)
        {
            return total;
        }

        return total is null ? next : total.Add(next);
    }

    private static int EstimateTokens(IEnumerable<ChatMessage> messages)
    {
        return messages.Sum(message => message.Content.Length) / 4;
    }
}
