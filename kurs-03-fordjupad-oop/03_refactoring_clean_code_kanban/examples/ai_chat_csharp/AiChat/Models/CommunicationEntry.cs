using System.Text.Json.Nodes;

namespace AiChat.Models;

/// <summary>
/// En post i kommunikationsloggen: en förfrågan eller ett svar, med hela JSON-kroppen.
/// </summary>
/// <param name="Time">När posten skrevs.</param>
/// <param name="Direction">"request" (till modellen) eller "response" (från modellen).</param>
/// <param name="Body">Hela JSON-kroppen, precis som den skickades eller togs emot.</param>
public sealed record CommunicationEntry(DateTimeOffset Time, string Direction, JsonNode? Body);
