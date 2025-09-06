using SnapGameSimulator.Models;

namespace SnapGameSimulator.Domain.WinnerSelector
{
    public interface IWinnerSelector
    {
        public Task<string> SelectWinner(List<PlayerDetail> playerDetails);
    }
}
