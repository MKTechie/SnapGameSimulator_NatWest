using System.Collections.Generic;
using System.Globalization;

namespace SnapGameSimulator.Domain.Localization
{
    /// <summary>
    /// Provides functionality to generate messages in multiple languages.
    /// </summary>
    public class TranslationVault : ITranslationVault
    {
        private readonly Dictionary<MessageKey, Dictionary<string, string>> _messages;

        public TranslationVault()
        {
            _messages = new Dictionary<MessageKey, Dictionary<string, string>>
            {
                { MessageKey.EnterPackLimit, new Dictionary<string, string>
                    {
                        { "en-US", "Enter the number of packs required in the game" }
                    }
                },
                { MessageKey.InvalidPackLimit, new Dictionary<string, string>
                    {
                        { "en-US", "Invalid number of packs. Please enter a positive integer." }
                    }
                },
                { MessageKey.Player, new Dictionary<string, string>
                    {
                        { "en-US", "Player" }
                    }
                },
                { MessageKey.WelcomeMessage, new Dictionary<string, string>
                    {
                        { "en-US", "Welcome to snap game." }
                    }
                },
                { MessageKey.Loading, new Dictionary<string, string>
                    {
                        { "en-US", "Loading" }
                    }
                },
                { MessageKey.TotalPlayers, new Dictionary<string, string>
                    {
                        { "en-US", "Total players" }
                    }
                },
                { MessageKey.DiscardedMessage, new Dictionary<string, string>
                    {
                        { "en-US", "Discarded cards count" }
                    }
                },
                { MessageKey.Winner, new Dictionary<string, string>
                    {
                        { "en-US", "Winner" }
                    }
                },
                { MessageKey.Result, new Dictionary<string, string>
                    {
                        { "en-US", "Total number of cards won" }
                    }
                },
                { MessageKey.PileWinner, new Dictionary<string, string>
                    {
                        { "en-US", "Snap! Winner of the pile is" }
                    }
                },
                { MessageKey.TotalCards, new Dictionary<string, string>
                    {
                        { "en-US", "Cards count" }
                    }
                },
            };
        }

        /// <summary>
        /// Gets the localized message for the specified key and culture.
        /// </summary>
        /// <param name="messageKey">The key identifying the message.</param>
        /// <param name="culture">The culture code (e.g., "en-US").</param>
        /// <returns>The localized message if found; otherwise, the English message or the key itself.</returns>
        public string GetMessage(MessageKey messageKey, string culture = null)
        {
            culture ??= CultureInfo.CurrentCulture.Name;
            if (_messages.TryGetValue(messageKey, out var translations))
            {
                if (translations.TryGetValue(culture, out var message))
                {
                    return message;
                }
                if (translations.TryGetValue("en-US", out var defaultMessage))
                {
                    return defaultMessage;
                }
            }
            return messageKey.ToString();
        }
    }
}
