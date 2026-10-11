using System.Collections.Concurrent;
using System.Reflection;
using System.Text;

public class GraphicHost
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Logging.ClearProviders();
        builder.WebHost.UseUrls(builder.Configuration["GameUrl"] ?? "http://127.0.0.1:0");
        var app = builder.Build();
        var bridge = new ConsoleBridge();
        Console.SetOut(bridge);
        Console.SetIn(bridge.Input);
        app.UseDefaultFiles();
        app.UseStaticFiles();
        app.MapGet("/api/state", () => Results.Json(bridge.State()));
        app.MapPost("/api/input", async (HttpRequest request) =>
        {
            var input = await request.ReadFromJsonAsync<Choice>();
            if (input?.Value == null || input.Value.Length > 120) return Results.BadRequest();
            return bridge.Submit(input.Version, input.Value) ? Results.Ok() : Results.Conflict();
        });
        app.MapPost("/api/restart", () => bridge.Restart() ? Results.Ok() : Results.Conflict());
        new Thread(() =>
        {
            while (true)
            {
                try { typeof(Program).GetMethod("Main", BindingFlags.Static | BindingFlags.NonPublic)!.Invoke(null, new object[] { Array.Empty<string>() }); break; }
                catch (Exception ex) when (ex.GetBaseException() is RestartGameException) { bridge.ResetPresentation(); }
                catch (Exception ex) { bridge.WriteLine("Spelet avslutades: " + ex.GetBaseException().Message); bridge.Publish(false); break; }
            }
        }) { IsBackground = true }.Start();
        app.Lifetime.ApplicationStarted.Register(() =>
        {
            var address = app.Urls.Single();
            Console.Error.WriteLine("Speladress: " + address);
            if (string.Equals(builder.Configuration["OpenBrowser"], "true", StringComparison.OrdinalIgnoreCase))
            {
                try { System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(address) { UseShellExecute = true }); }
                catch { Console.Error.WriteLine("Oppna speladressen ovan i din webblasare."); }
            }
        });
        app.Run();
    }
    public record Choice(string Value, int Version);
}

// Only transports Console output and input. All game decisions run in the original Program.
public sealed class ConsoleBridge : TextWriter
{
    private readonly object gate = new();
    private readonly StringBuilder buffer = new();
    private readonly BlockingCollection<string> answers = new();
    private string screen = "";
    private int version;
    private bool waiting;
    private const string RestartSignal = "\0restart-game";
    private readonly HashSet<string> ownedItems = new();
    public override Encoding Encoding => Encoding.UTF8;
    public TextReader Input { get; }
    public ConsoleBridge() { Input = new BridgeReader(this); }
    public override void Write(char value) { lock (gate) buffer.Append(value); }
    public override void Write(string? value) { lock (gate) buffer.Append(value); }
    public override void WriteLine(string? value) { lock (gate) buffer.AppendLine(value); }
    public void Publish(bool ready = true) { lock (gate) { screen = buffer.ToString(); buffer.Clear();
            if (screen.Trim() == "Vad heter du?") ownedItems.Clear();
            foreach (System.Text.RegularExpressions.Match match in System.Text.RegularExpressions.Regex.Matches(screen, @"du har köpt (.+?) för \d+ guld!", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                ownedItems.Add(match.Groups[1].Value); version++; waiting = ready; } }
    public object State() { lock (gate) return new { text = screen, version, waiting, ownedItems = ownedItems.ToArray() }; }
    public bool Submit(int expected, string value) { lock (gate) { if (!waiting || expected != version) return false; waiting = false; answers.Add(value); return true; } }
    public bool Restart() { lock (gate) { if (!waiting) return false; waiting = false; answers.Add(RestartSignal); return true; } }
    public void ResetPresentation() { lock (gate) { buffer.Clear(); screen = ""; ownedItems.Clear(); waiting = false; } }
    private sealed class BridgeReader(ConsoleBridge owner) : TextReader
    {
        public override string ReadLine() { owner.Publish(); var answer = owner.answers.Take(); if (answer == RestartSignal) throw new RestartGameException(); return answer; }
    }
}

public sealed class RestartGameException : Exception { }
