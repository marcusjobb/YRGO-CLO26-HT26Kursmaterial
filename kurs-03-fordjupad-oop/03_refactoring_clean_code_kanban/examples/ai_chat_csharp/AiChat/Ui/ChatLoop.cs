using AiChat.Models;
using AiChat.Services;

namespace AiChat.Ui;

/// <summary>
/// Konsolloopen: läser det användaren skriver och visar svaren, tokens och kontext.
/// </summary>
public sealed class ChatLoop(ChatService chat)
{
    /// <summary>
    /// Kör loopen tills användaren skriver exit eller input tar slut.
    /// </summary>
    /// <param name="input">Varifrån texten kommer. Normalt Console.In.</param>
    /// <param name="output">Vart svaren skrivs. Normalt Console.Out.</param>
    /// <param name="ct">Avbryt loopen.</param>
    public async Task RunAsync(TextReader input, TextWriter output, CancellationToken ct = default)
    {
        output.WriteLine("Skriv något. Kommandon: /kontext visar vad modellen ser, /compact sammanfattar, /reset rensar, exit avslutar.");

        while (true)
        {
            output.Write("\nDu: ");
            var line = input.ReadLine()?.Trim();

            if (line is null or "exit" or "avsluta")
            {
                break;
            }

            if (line.Length == 0)
            {
                continue;
            }

            if (line == "/kontext")
            {
                output.WriteLine(chat.DescribeContext());
                continue;
            }

            if (line == "/compact")
            {
                var done = await chat.CompactAsync(ct);
                output.WriteLine(done ? "(samtalet komprimerades)" : "(inget att komprimera)");
                continue;
            }

            if (line == "/reset")
            {
                chat.Reset();
                output.WriteLine("(historiken rensad)");
                continue;
            }

            var result = await chat.SendAsync(line, status => output.WriteLine($"  [verktyg] {status}"), ct);
            output.WriteLine($"AI: {result.Text}");
            output.WriteLine($"  ({Describe(result)})");
        }
    }

    private static string Describe(ChatResult result)
    {
        var context = $"kontext: {result.ContextMessages} meddelanden";
        return result.Usage is null
            ? context
            : $"tokens in: {result.Usage.PromptTokens}, ut: {result.Usage.CompletionTokens} · {context}";
    }
}
