using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;
using AiChat.Models;

namespace AiChat.Llm;

/// <summary>
/// Pratar med en OpenAI-kompatibel endpoint över HTTP. Skriver både förfrågan och svar till loggen.
/// </summary>
public sealed class LlmClient : ILlmClient
{
    private readonly HttpClient _http;
    private readonly string _apiKey;
    private readonly string _baseUrl;
    private readonly ICommunicationLog _log;

    /// <summary>
    /// Skapar klienten.
    /// </summary>
    /// <param name="apiKey">API-nyckeln. Skickas bara i HTTP-huvudet.</param>
    /// <param name="baseUrl">Till exempel https://api.openai.com/v1.</param>
    /// <param name="log">Loggen som får varje förfrågan och svar.</param>
    /// <param name="http">Valfri HttpClient. Tester kan skicka in en egen.</param>
    public LlmClient(string apiKey, string baseUrl, ICommunicationLog log, HttpClient? http = null)
    {
        _apiKey = apiKey;
        _baseUrl = baseUrl;
        _log = log;
        _http = http ?? new HttpClient { Timeout = TimeSpan.FromSeconds(120) };
    }

    /// <inheritdoc />
    public async Task<LlmReply> CompleteAsync(
        string model,
        IReadOnlyList<ChatMessage> messages,
        JsonArray? tools,
        CancellationToken ct = default)
    {
        var body = new JsonObject
        {
            ["model"] = model,
            ["messages"] = ToJsonMessages(messages),
        };
        if (tools is { Count: > 0 })
        {
            body["tools"] = JsonNode.Parse(tools.ToJsonString());
        }

        _log.Record("request", body);

        using var request = new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl.TrimEnd('/')}/chat/completions")
        {
            Content = new StringContent(body.ToJsonString(), Encoding.UTF8, "application/json"),
        };
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

        using var response = await _http.SendAsync(request, ct);
        var raw = await response.Content.ReadAsStringAsync(ct);
        var parsed = JsonNode.Parse(raw);
        _log.Record("response", parsed ?? JsonValue.Create(raw));

        if (!response.IsSuccessStatusCode)
        {
            var reason = parsed?["error"]?["message"]?.GetValue<string>() ?? "okänt fel";
            throw new LlmException($"API:t svarade {(int)response.StatusCode}: {reason}");
        }

        return ParseReply(parsed);
    }

    /// <summary>
    /// Gör om våra meddelanden till det format som API:t förväntar sig.
    /// </summary>
    private static JsonArray ToJsonMessages(IEnumerable<ChatMessage> messages)
    {
        var array = new JsonArray();
        foreach (var message in messages)
        {
            var item = new JsonObject { ["role"] = message.Role };

            if (message.Role == "tool")
            {
                item["tool_call_id"] = message.ToolCallId;
                item["content"] = message.Content;
            }
            else if (message.ToolCalls is { Count: > 0 } calls)
            {
                item["content"] = message.Content.Length == 0 ? null : JsonValue.Create(message.Content);
                item["tool_calls"] = new JsonArray(calls.Select(call => (JsonNode)new JsonObject
                {
                    ["id"] = call.Id,
                    ["type"] = "function",
                    ["function"] = new JsonObject { ["name"] = call.Name, ["arguments"] = call.Arguments },
                }).ToArray());
            }
            else
            {
                item["content"] = message.Content;
            }

            array.Add(item);
        }

        return array;
    }

    /// <summary>
    /// Plockar ut texten och verktygsanropen ur API:ts svar.
    /// </summary>
    private static LlmReply ParseReply(JsonNode? parsed)
    {
        var message = parsed?["choices"]?[0]?["message"]
            ?? throw new LlmException("Oväntat svar: saknar 'choices'.");

        var text = message["content"]?.GetValue<string>() ?? string.Empty;
        var calls = new List<ToolCall>();

        if (message["tool_calls"] is JsonArray toolCalls)
        {
            foreach (var call in toolCalls)
            {
                calls.Add(new ToolCall(
                    Id: call!["id"]!.GetValue<string>(),
                    Name: call["function"]!["name"]!.GetValue<string>(),
                    Arguments: call["function"]!["arguments"]!.GetValue<string>()));
            }
        }

        var usage = ParseUsage(parsed);
        return new LlmReply(text.Trim(), calls, usage);
    }

    /// <summary>
    /// Läser hur många tokens som användes, om API:t skickar med "usage".
    /// </summary>
    private static TokenUsage? ParseUsage(JsonNode? parsed)
    {
        var usage = parsed?["usage"];
        if (usage is null)
        {
            return null;
        }

        return new TokenUsage(
            PromptTokens: usage["prompt_tokens"]?.GetValue<int>() ?? 0,
            CompletionTokens: usage["completion_tokens"]?.GetValue<int>() ?? 0);
    }
}
