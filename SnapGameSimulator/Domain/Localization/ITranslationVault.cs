namespace SnapGameSimulator.Domain.Localization
{
    /// <summary>
    /// Provides functionality to retrieve localized messages based on a message key and optional culture.
    /// </summary>
    /// <remarks>This interface is designed to support localization by allowing the retrieval of messages in
    /// different languages or cultures. If no culture is specified, the default culture is used.</remarks>
    public interface ITranslationVault
    {
        /// <summary>
        /// Retrieves a localized message corresponding to the specified key.
        /// </summary>
        /// <param name="messageKey">The key identifying the message to retrieve. Cannot be null or empty.</param>
        /// <param name="culture">An optional culture identifier (e.g., "en-US") to retrieve the message for a specific culture.  If null or
        /// not provided, the default culture is used.</param>
        /// <returns>The localized message corresponding to the specified key and culture.  Returns null if the message key does
        /// not exist.</returns>
        string GetMessage(MessageKey messageKey, string culture);
    }
}