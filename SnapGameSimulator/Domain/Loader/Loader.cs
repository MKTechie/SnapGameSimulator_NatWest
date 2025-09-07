using SnapGameSimulator.Models;

namespace SnapGameSimulator.Domain.Loader
{
    public class Loader : ILoader
    {
        public ulong GetPackLimit()
        {
            Console.WriteLine("Enter the number of packs required in the game");
            var packsLimitInput = Console.ReadLine();
            if (!ulong.TryParse(packsLimitInput, out var packsLimit) || packsLimit < 1)
            {
                throw new ArgumentException("Invalid number of packs. Please enter a positive integer.");
            }
            return packsLimit;
        }

        public List<PlayerDetail> LoadPlayers(int playersCount)
        {
            List<PlayerDetail> playerDetail = [];
            for (var i = 0; i < playersCount; i++)
            {
                playerDetail.Add(new PlayerDetail
                {
                    Name = $"Player {i + 1}",
                    Cards = new Queue<Card>()
                });
            }

            return playerDetail;
        }
    }
}
