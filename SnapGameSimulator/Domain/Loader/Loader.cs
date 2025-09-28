using SnapGameSimulator.Domain.Localization;
using SnapGameSimulator.Extension;
using SnapGameSimulator.Models;
using SnapGameSimulator.Settings;

namespace SnapGameSimulator.Domain.Loader
{
    /// <inheritdoc/>
    public class Loader(ITranslationVault translationVault, IUserSettings userSettings) : ILoader
    {
      
        /// <inheritdoc/>
        public ulong GetPackLimit()
        {
            string message = translationVault.GetMessage(MessageKey.EnterPackLimit, userSettings.GetUserPreferredLanguage());
            message.PrintMessage();
            var packsLimitInput = Console.ReadLine();
            if (!ulong.TryParse(packsLimitInput, out var packsLimit) || packsLimit < 1)
            {
                throw new ArgumentException(translationVault.GetMessage(MessageKey.InvalidPackLimit, userSettings.GetUserPreferredLanguage()));
            }
            return packsLimit;
        }

        /// <inheritdoc/>
        public List<PlayerDetail> LoadPlayers(int playersCount)
        {
            List<PlayerDetail> playerDetail = [];
            string player = translationVault.GetMessage(MessageKey.Player, userSettings.GetUserPreferredLanguage());
            for (var i = 0; i < playersCount; i++)
            {
                playerDetail.Add(new PlayerDetail
                {
                    
                    Name = $"{player} {i + 1}",
                    Cards = new Queue<Card>()
                });
            }

            return playerDetail;
        }
    }
}
