using SnapGameSimulator.Models;

namespace SnapGameSimulator.Domain.Loader
{
    public interface ILoader
    {
        public ulong GetPackLimit();

        public List<PlayerDetail> LoadPlayers(int playersCount);
    }
}
