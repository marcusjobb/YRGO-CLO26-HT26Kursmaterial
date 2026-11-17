using AiChat.Config;
using AiChat.Models;
using AiChat.Services;
using AiChat.Storage;
using AiChat.Tools;

namespace AiChat.Tests;

public class PersonaTests
{
    [Fact]
    public void Det_finns_fyra_personligheter()
    {
        Assert.Equal(["Margareta", "Becky", "Bosse", "Glenn"], Personas.All.Select(p => p.Name));
    }

    [Fact]
    public void Namnet_hittas_utan_att_bry_sig_om_stora_bokstaver()
    {
        Assert.Equal("Glenn", Personas.Find("glenn").Name);
    }

    [Fact]
    public void Okand_personlighet_ger_ett_tydligt_fel()
    {
        var error = Assert.Throws<ArgumentException>(() => Personas.Find("Kalle"));
        Assert.Contains("Becky", error.Message);
    }

    [Fact]
    public async Task Vald_personlighet_ligger_forst_i_systemprompten()
    {
        var dir = Directory.CreateTempSubdirectory().FullName;
        var baseSettings = TestSettings.Create(dir);
        var settings = baseSettings with { Persona = Personas.Find("Becky") };
        var llm = new FakeLlm(new LlmReply("Svar", []));
        var chat = new ChatService(llm, new HistoryStore(Path.Combine(dir, "historik.json")),
            new ToolRegistry([new WriteFileTool(new Workspace(settings.WorkspacePath))]), settings);

        await chat.SendAsync("hej");

        var system = llm.Requests[0][0].Content;
        Assert.StartsWith("Du är Becky", system);
    }
}
