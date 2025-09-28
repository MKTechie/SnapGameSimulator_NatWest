namespace SnapGameSimulator.Helper
{
    /// <summary>
    /// Helps to select the pile winner
    /// </summary>
    public static class PileWinnerHelper
    {
        /// <summary>
        /// Randomly selects a winner from the provided player bucket.
        /// </summary>
        /// <param name="playerBucket">List of player names eligible to win the pile.</param>
        /// <returns>The name of the selected winner.</returns>
        public static string Selection(List<string> playerBucket)
        {
            if (playerBucket == null || playerBucket.Count == 0)
                throw new ArgumentException("playerBucket must not be null or empty.", nameof(playerBucket));

            var random = new Random();
            return playerBucket[random.Next(0, playerBucket.Count)];
        }
    }
}
