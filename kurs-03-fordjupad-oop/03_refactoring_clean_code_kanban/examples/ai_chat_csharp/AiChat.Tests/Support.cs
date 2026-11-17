using System.Text.Json.Nodes;
using AiChat.Config;
using AiChat.Llm;
using AiChat.Models;

namespace AiChat.Tests;

/// <summary>Låtsasmodell som svarar enligt ett manus och sparar alla förfrågningar.</summary>
public sealed class FakeLlm(params LlmReply[] script) : ILlmClient
{
    private readonly Queue<LlmReply> _script = new(script);

    /// <summary>Alla meddelandelistor som skickats, i ordning.</summary>
    public List<IReadOnlyList<ChatMessage>> Requests { get; } = [];

    /// <inheritdoc />
    public Task<LlmReply> CompleteAsync(string model, IReadOnlyList<ChatMessage> messages, JsonArray? tools, CancellationToken ct = default)
    {
        Requests.Add(messages.ToList());
        var reply = _script.Count > 0 ? _script.Dequeue() : new LlmReply("klart", []);
        return Task.FromResult(reply);
    }
}

/// <summary>Loggen som inte sparar något, för tester.</summary>
public sealed class MemoryLog : AiChat.Llm.ICommunicationLog
{
    /// <inheritdoc />
    public void Record(string direction, JsonNode? body)
    {
    }
}

/// <summary>Skapar inställningar för tester.</summary>
public static class TestSettings
{
    /// <summary>Inställningar som pekar på en temporär mapp.</summary>
    public static AppSettings Create(string folder, bool useHistory = true, int window = 20, int compactAt = 50) =>
        new(ApiKey: "test", BaseUrl: "http://localhost", Model: "m", CompactModel: "m",
            WorkspacePath: Path.Combine(folder, "arbetsyta"), DataPath: folder,
            UseHistory: useHistory, Window: window, CompactAt: compactAt,
            Persona: null);
}
