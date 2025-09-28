using SnapGameSimulator.Models;

namespace SnapGameSimulator.Domain.Loader
{
    /// <summary>
    /// Defines methods for loading player data and retrieving configuration limits.
    /// </summary>
    /// <remarks>This interface provides functionality to retrieve the maximum pack limit and load player
    /// details based on a specified count. Implementations of this interface should ensure thread safety if used in
    /// concurrent environments.</remarks>
    public interface ILoader
    {
        /// <summary>
        /// Retrieves the maximum allowable size, in bytes, for a data pack.
        /// </summary>
        /// <returns>The maximum size, in bytes, that a data pack can occupy.</returns>
        public ulong GetPackLimit();

        /// <summary>
        /// Loads a specified number of player details.
        /// </summary>
        /// <param name="playersCount">The number of players to load. Must be a non-negative integer.</param>
        /// <returns>A list of <see cref="PlayerDetail"/> objects representing the loaded players.  The list will contain up to
        /// <paramref name="playersCount"/> players, or fewer if fewer players are available.</returns>
        public List<PlayerDetail> LoadPlayers(int playersCount);
    }
}
