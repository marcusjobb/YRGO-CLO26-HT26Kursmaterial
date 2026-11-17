namespace AiChat.Tools;

/// <summary>
/// Arbetsytan: den enda mappen verktygen får läsa och skriva i. Allt som försöker ta sig
/// utanför (till exempel med "..") stoppas här.
/// </summary>
public sealed class Workspace
{
    private readonly string _root;

    /// <summary>
    /// Skapar arbetsytan om den inte finns.
    /// </summary>
    /// <param name="root">Mappen, relativt den nuvarande mappen eller absolut.</param>
    public Workspace(string root)
    {
        _root = Path.GetFullPath(root);
        Directory.CreateDirectory(_root);
    }

    /// <summary>
    /// Gör en relativ sökväg till en absolut sökväg inom arbetsytan.
    /// </summary>
    /// <param name="relativePath">Sökvägen som modellen angav.</param>
    /// <exception cref="ArgumentException">Om sökvägen är tom eller pekar utanför arbetsytan.</exception>
    public string Resolve(string relativePath)
    {
        if (string.IsNullOrWhiteSpace(relativePath))
        {
            throw new ArgumentException("Sökvägen är tom.");
        }

        var full = Path.GetFullPath(Path.Combine(_root, relativePath));
        var inside = full.StartsWith(_root + Path.DirectorySeparatorChar, StringComparison.Ordinal);
        if (!inside)
        {
            throw new ArgumentException("Sökvägen ligger utanför arbetsytan.");
        }

        return full;
    }
}
