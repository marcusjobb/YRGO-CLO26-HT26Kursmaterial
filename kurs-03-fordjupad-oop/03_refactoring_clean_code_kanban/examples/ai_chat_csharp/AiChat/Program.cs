using AiChat.Config;
using AiChat.Llm;
using AiChat.Services;
using AiChat.Storage;
using AiChat.Tools;
using AiChat.Ui;

// Startpunkten: här kopplas alla delar ihop. Resten av programmet vet inget om varandra
// utöver det som skickas in i konstruktorerna.
var settings = AppSettings.Load(Environment.GetEnvironmentVariable);

var workspace = new Workspace(settings.WorkspacePath);
var history = new HistoryStore(Path.Combine(settings.DataPath, "historik.json"));
var communicationLog = new JsonCommunicationLog(Path.Combine(settings.DataPath, "kommunikation.json"));
var llm = new LlmClient(settings.ApiKey, settings.BaseUrl, communicationLog);
var tools = new ToolRegistry([new ReadFileTool(workspace), new WriteFileTool(workspace)]);
var chat = new ChatService(llm, history, tools, settings);

Console.WriteLine($"Modell: {settings.Model} · historik: {(settings.UseHistory ? "på" : "av")} · arbetsyta: {Path.GetFullPath(settings.WorkspacePath)}");

try
{
    await new ChatLoop(chat).RunAsync(Console.In, Console.Out);
}
catch (LlmException ex)
{
    Console.Error.WriteLine($"Fel: {ex.Message}");
    return 1;
}

return 0;
