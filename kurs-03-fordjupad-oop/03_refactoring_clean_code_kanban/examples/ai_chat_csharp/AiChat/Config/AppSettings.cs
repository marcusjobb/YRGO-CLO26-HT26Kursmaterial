namespace AiChat.Config;

/// <summary>
/// Alla inställningar, lästa från miljövariabler. Nyckeln syns aldrig i felmeddelanden.
/// </summary>
/// <param name="ApiKey">API-nyckeln. Krävs.</param>
/// <param name="BaseUrl">Adressen till en OpenAI-kompatibel endpoint.</param>
/// <param name="Model">Modellen som svarar.</param>
/// <param name="CompactModel">Modellen som sammanfattar. Standard: samma som Model.</param>
/// <param name="WorkspacePath">Mappen som verktygen får läsa och skriva i.</param>
/// <param name="DataPath">Mappen där historik.json och kommunikation.json sparas.</param>
/// <param name="UseHistory">Om false skickas bara det senaste meddelandet till modellen.</param>
/// <param name="Window">Hur många senaste turer som skickas med när historik används.</param>
/// <param name="CompactAt">Efter så här många turer komprimeras samtalet automatiskt.</param>
/// <param name="Persona">Vald personlighet, eller null för standardtonen.</param>
public sealed record AppSettings(
    string ApiKey,
    string BaseUrl,
    string Model,
    string CompactModel,
    string WorkspacePath,
    string DataPath,
    bool UseHistory,
    int Window,
    int CompactAt,
    Persona? Persona = null)
{
    /// <summary>
    /// Läser inställningarna. Kastar InvalidOperationException om CHAT_API_KEY saknas.
    /// </summary>
    /// <param name="read">Funktionen som läser en miljövariabel. Tester kan skicka in en egen.</param>
    public static AppSettings Load(Func<string, string?> read)
    {
        var apiKey = read("CHAT_API_KEY")?.Trim();
        if (string.IsNullOrEmpty(apiKey))
        {
            throw new InvalidOperationException("CHAT_API_KEY saknas. Sätt den i din terminal (se guiden).");
        }

        var model = Value(read, "CHAT_MODEL", "gpt-4o-mini");

        return new AppSettings(
            ApiKey: apiKey,
            BaseUrl: Value(read, "CHAT_BASE_URL", "https://api.openai.com/v1"),
            Model: model,
            CompactModel: Value(read, "CHAT_COMPACT_MODEL", model),
            WorkspacePath: Value(read, "CHAT_WORKSPACE", "arbetsyta"),
            DataPath: Value(read, "CHAT_DATA", "data"),
            UseHistory: Value(read, "CHAT_HISTORY", "1") != "0",
            Window: IntValue(read, "CHAT_WINDOW", 20),
            CompactAt: IntValue(read, "CHAT_COMPACT_AT", 50),
            Persona: PersonaValue(read));
    }

    private static string Value(Func<string, string?> read, string name, string fallback)
    {
        var value = read(name)?.Trim();
        return string.IsNullOrEmpty(value) ? fallback : value;
    }

    private static Persona? PersonaValue(Func<string, string?> read)
    {
        var name = read("CHAT_PERSONA")?.Trim();
        return string.IsNullOrEmpty(name) ? null : Personas.Find(name);
    }

    private static int IntValue(Func<string, string?> read, string name, int fallback)
    {
        return int.TryParse(read(name), out var number) && number > 0 ? number : fallback;
    }
}
