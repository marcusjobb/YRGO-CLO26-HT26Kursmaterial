namespace AiChat.Config;

/// <summary>
/// En personlighet som assistenten kan ha. Den styr tonen, inte fakta.
/// </summary>
/// <param name="Name">Namnet man väljer med CHAT_PERSONA, till exempel "Becky".</param>
/// <param name="Description">En rad om vem personen är, för att visa i listan.</param>
/// <param name="Instruction">Texten som läggs först i systemprompten.</param>
public sealed record Persona(string Name, string Description, string Instruction);
