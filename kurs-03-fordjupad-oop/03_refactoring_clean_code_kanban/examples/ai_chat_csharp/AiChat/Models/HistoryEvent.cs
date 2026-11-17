namespace AiChat.Models;

/// <summary>
/// En händelse i historiklogen. Loggen växer bara, och ingen rad skrivs över.
/// </summary>
/// <param name="Type">"turn" (ett meddelande), "compact" (en sammanfattning) eller "reset".</param>
/// <param name="Speaker">"user" eller "assistant" för turer. Tom för övriga.</param>
/// <param name="Content">Meddelandets text, eller sammanfattningen.</param>
public sealed record HistoryEvent(string Type, string Speaker, string Content);
