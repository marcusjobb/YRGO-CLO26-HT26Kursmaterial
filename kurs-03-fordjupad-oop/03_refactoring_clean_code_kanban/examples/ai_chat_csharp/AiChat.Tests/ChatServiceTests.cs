using AiChat.Models;
using AiChat.Services;
using AiChat.Storage;
using AiChat.Tools;
using System.Text.Json;

namespace AiChat.Tests;

public class ChatServiceTests
{
    private static (ChatService Chat, HistoryStore History) Build(FakeLlm llm, string dir, bool useHistory = true)
    {
        var settings = TestSettings.Create(dir, useHistory);
        var history = new HistoryStore(Path.Combine(dir, "historik.json"));
        var tools = new ToolRegistry([new WriteFileTool(new Workspace(settings.WorkspacePath))]);
        return (new ChatService(llm, history, tools, settings), history);
    }

    [Fact]
    public async Task Utan_historik_skickas_bara_systemmeddelandet_och_det_nya()
    {
        var dir = Directory.CreateTempSubdirectory().FullName;
        var llm = new FakeLlm(new LlmReply("Hej!", []));
        var (chat, _) = Build(llm, dir, useHistory: false);

        await chat.SendAsync("Jag heter Anna");
        await chat.SendAsync("Vad heter jag?");

        var secondRequest = llm.Requests[1];
        Assert.Equal(2, secondRequest.Count);
        Assert.Equal("Vad heter jag?", secondRequest[1].Content);
    }

    [Fact]
    public async Task Med_historik_ser_modellen_tidigare_turer()
    {
        var dir = Directory.CreateTempSubdirectory().FullName;
        var llm = new FakeLlm(new LlmReply("Hej Anna!", []), new LlmReply("Du heter Anna.", []));
        var (chat, _) = Build(llm, dir, useHistory: true);

        await chat.SendAsync("Jag heter Anna");
        await chat.SendAsync("Vad heter jag?");

        var secondRequest = llm.Requests[1];
        Assert.Contains(secondRequest, m => m.Role == "assistant" && m.Content == "Hej Anna!");
        Assert.Contains(secondRequest, m => m.Content == "Jag heter Anna");
    }

    [Fact]
    public async Task Verktygsanrop_kors_och_svaret_kommer_tillbaka_till_modellen()
    {
        var dir = Directory.CreateTempSubdirectory().FullName;
        var args = JsonSerializer.Serialize(new { path = "anteckning.txt", content = "hej" });
        var llm = new FakeLlm(
            new LlmReply("", [new ToolCall("c1", "write_file", args)]),
            new LlmReply("Jag sparade filen.", []));
        var (chat, _) = Build(llm, dir);

        var result = await chat.SendAsync("skriv hej i anteckning.txt");

        Assert.Equal("Jag sparade filen.", result.Text);
        Assert.Equal("hej", File.ReadAllText(Path.Combine(dir, "arbetsyta", "anteckning.txt")));
        var secondRequest = llm.Requests[1];
        Assert.Contains(secondRequest, m => m.Role == "tool" && m.ToolCallId == "c1");
    }

    [Fact]
    public async Task Tokens_summeras_over_alla_anrop_i_svaret()
    {
        var dir = Directory.CreateTempSubdirectory().FullName;
        var args = JsonSerializer.Serialize(new { path = "a.txt", content = "x" });
        var llm = new FakeLlm(
            new LlmReply("", [new ToolCall("c1", "write_file", args)], new TokenUsage(100, 10)),
            new LlmReply("Klart.", [], new TokenUsage(150, 5)));
        var (chat, _) = Build(llm, dir);

        var result = await chat.SendAsync("gör det");

        Assert.Equal(new TokenUsage(250, 15), result.Usage);
    }

    [Fact]
    public async Task Varje_meddelande_loggas_i_historiken_aven_utan_historik()
    {
        var dir = Directory.CreateTempSubdirectory().FullName;
        var llm = new FakeLlm(new LlmReply("Svar", []));
        var (chat, history) = Build(llm, dir, useHistory: false);

        await chat.SendAsync("fråga");

        Assert.Equal(["user", "assistant"], history.Events.Select(e => e.Speaker));
    }
}
