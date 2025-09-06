using SnapGameSimulator.Models;

namespace SnapGameSimulator.Domain.Loader
{
    public interface ILoader
    {
        public long GetPackLimit();

        public List<PlayerDetail> LoadPlayers(int playersCount);
    }
}
