using AiChat.Models;
using AiChat.Storage;

namespace AiChat.Tests;

public class HistoryStoreTests
{
    [Fact]
    public void Context_innehaller_turerna_i_ordning()
    {
        var dir = Directory.CreateTempSubdirectory().FullName;
        var store = new HistoryStore(Path.Combine(dir, "historik.json"));

        store.Append(new HistoryEvent("turn", "user", "hej"));
        store.Append(new HistoryEvent("turn", "assistant", "hej du"));

        var context = store.Context();
        Assert.Equal(["user", "assistant"], context.Select(m => m.Role));
        Assert.Equal("hej du", context[1].Content);
    }

    [Fact]
    public void Reset_nollstaller_kontexten_men_raderar_inget_i_loggen()
    {
        var dir = Directory.CreateTempSubdirectory().FullName;
        var store = new HistoryStore(Path.Combine(dir, "historik.json"));
        store.Append(new HistoryEvent("turn", "user", "hej"));
        store.Append(new HistoryEvent("reset", string.Empty, string.Empty));

        Assert.Empty(store.Context());
        Assert.Equal(2, store.Events.Count);
    }

    [Fact]
    public void Compact_ersatter_allt_fore_med_sammanfattningen()
    {
        var dir = Directory.CreateTempSubdirectory().FullName;
        var store = new HistoryStore(Path.Combine(dir, "historik.json"));
        store.Append(new HistoryEvent("turn", "user", "gammalt"));
        store.Append(new HistoryEvent("compact", string.Empty, "- kort"));
        store.Append(new HistoryEvent("turn", "user", "nytt"));

        var context = store.Context();
        Assert.Equal(2, context.Count);
        Assert.Equal("system", context[0].Role);
        Assert.Contains("- kort", context[0].Content);
        Assert.Equal("nytt", context[1].Content);
    }

    [Fact]
    public void TurnsSinceCompact_raknar_bara_efter_senaste_komprimering()
    {
        var dir = Directory.CreateTempSubdirectory().FullName;
        var store = new HistoryStore(Path.Combine(dir, "historik.json"));
        store.Append(new HistoryEvent("turn", "user", "a"));
        store.Append(new HistoryEvent("turn", "assistant", "b"));
        store.Append(new HistoryEvent("compact", string.Empty, "- kort"));
        store.Append(new HistoryEvent("turn", "user", "c"));

        Assert.Equal(1, store.TurnsSinceCompact());
    }

    [Fact]
    public void Historiken_ligger_kvar_i_json_filen()
    {
        var dir = Directory.CreateTempSubdirectory().FullName;
        var path = Path.Combine(dir, "historik.json");
        new HistoryStore(path).Append(new HistoryEvent("turn", "user", "hej på svenska: åäö"));

        var reloaded = new HistoryStore(path);
        Assert.Single(reloaded.Events);
        Assert.Equal("hej på svenska: åäö", reloaded.Events[0].Content);
        Assert.Contains("åäö", File.ReadAllText(path));
    }
}
