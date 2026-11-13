namespace AiChat.Llm;

/// <summary>
/// Fel från API:t, eller ett svar som inte gick att tolka. Meddelandet visas för användaren.
/// </summary>
public sealed class LlmException(string message) : Exception(message);
