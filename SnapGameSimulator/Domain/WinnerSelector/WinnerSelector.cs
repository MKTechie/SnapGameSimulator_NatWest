using SnapGameSimulator.Models;

namespace SnapGameSimulator.Domain.WinnerSelector
{
    /// <summary>
    /// Winner selector implementation for determining the winner of the game.
    /// Note: We can add dependency of translation service here to support multiple languages
    /// </summary>
    public class WinnerSelector : IWinnerSelector
    {
        private const string DrawMessage = "A draw!!!";
        private const string WinnerMessage = "The winner is {0}";

        /// <summary>
        /// Selects the winner based on the player details provided.
        /// </summary>
        /// <param name="playerDetails">Player detail for the game</param>
        /// <returns>Returns the winner name or draw game status</returns>
        public Task<string> SelectWinner(List<PlayerDetail> playerDetails)
        {
            // Find the maximum deck count
            var maxDeckCount = playerDetails.Max(player => player.Cards.Count);

            // Get the players with the maximum deck count
            var playersWithMaxCount = playerDetails
                .Where(player => player.Cards.Count == maxDeckCount)
                .ToList();

            // Output the players with the maximum count
            var result =  playersWithMaxCount.Count > 1
                ? DrawMessage
                : string.Format(WinnerMessage, playersWithMaxCount.First().Name);
            return Task.FromResult(result);
        }
    }
}
