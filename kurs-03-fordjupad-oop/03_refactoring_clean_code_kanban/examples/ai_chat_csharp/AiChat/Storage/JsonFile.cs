using System.Text.Encodings.Web;
using System.Text.Json;

namespace AiChat.Storage;

/// <summary>
/// Läser och skriver listor som JSON-filer, läsbara för människor.
/// </summary>
public static class JsonFile
{
    /// <summary>
    /// Inställningar för läsbar JSON: indrag, svenska tecken och camelCase-namn.
    /// </summary>
    public static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
    };

    /// <summary>
    /// Läser en lista. Om filen inte finns returneras en tom lista.
    /// </summary>
    /// <typeparam name="T">Typen av poster.</typeparam>
    /// <param name="path">Filens sökväg.</param>
    public static List<T> ReadList<T>(string path)
    {
        if (!File.Exists(path))
        {
            return [];
        }

        return JsonSerializer.Deserialize<List<T>>(File.ReadAllText(path), Options) ?? [];
    }

    /// <summary>
    /// Skriver hela listan. Skrivningen sker via en temporär fil, så att en halvskriven fil aldrig uppstår.
    /// </summary>
    /// <typeparam name="T">Typen av poster.</typeparam>
    /// <param name="path">Filens sökväg.</param>
    /// <param name="items">Posterna som ska sparas.</param>
    public static void WriteList<T>(string path, IEnumerable<T> items)
    {
        var fullPath = Path.GetFullPath(path);
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);

        var temp = fullPath + ".tmp";
        File.WriteAllText(temp, JsonSerializer.Serialize(items, Options));
        File.Move(temp, fullPath, overwrite: true);
    }
}
