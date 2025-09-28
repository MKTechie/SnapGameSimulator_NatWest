using SnapGameSimulator.Models;

namespace SnapGameSimulator.Domain.WinnerSelector
{
    /// <summary>
    /// Defines a contract for selecting a winner from a list of player details.
    /// </summary>
    /// <remarks>Implementations of this interface should provide the logic to determine a winner based on the
    /// provided player details. The selection criteria may vary depending on the specific implementation.</remarks>
    public interface IWinnerSelector
    {
        /// <summary>
        /// Selects a winner from the provided list of player details.
        /// </summary>
        /// <param name="playerDetails">A list of <see cref="PlayerDetail"/> objects representing the players eligible for selection. The list must
        /// not be null or empty.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The task result contains the name of
        /// the selected winner as a string.</returns>
        public Task<string> SelectWinner(List<PlayerDetail> playerDetails);
    }
}
