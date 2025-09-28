using SnapGameSimulator.Extension;
using SnapGameSimulator.Models;

namespace SnapGameSimulator.Helper
{
    public static class GameEngineHelper
    {
        public static void ShowPlayersCount(string message, int playersCount)
        {
           $"{message} {playersCount}".PrintMessage();
        }

        public static void DisplayPlayerResults(string message, List<PlayerDetail> playerDetails)
        {
            foreach (var player in playerDetails)
            {
                $"{player.Name} - {message} {player.Cards.Count}".PrintMessage();
            }
        }

        public static void DisplayDiscardedPile(string discardedMessage, int pileCount)
        {
            if (pileCount > 0)
            {
                $"{discardedMessage}{pileCount}".PrintMessage();
            }
        }
    }
}
