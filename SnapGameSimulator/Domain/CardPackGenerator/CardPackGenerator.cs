using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;

namespace SnapGameSimulator.Domain.CardPackGenerator
{
    /// <inheritdoc/>
    public class CardPackGenerator<TObject, TEnumSuit, TEnumRank> : ICardPackGenerator<TObject, TEnumSuit, TEnumRank>
         where TObject : class
         where TEnumSuit : struct, Enum
         where TEnumRank : struct, Enum
    {
        private const int MinimumPacks = 1;
        private const int MaximumPacks = int.MaxValue;

        /// <inheritdoc/>
        public List<TObject> GetCardsPack([Range(MinimumPacks, MaximumPacks, ErrorMessage = "")] int numberOfPacks, Func<TEnumSuit, TEnumRank, TObject> cardType)
        {
            if (numberOfPacks < MinimumPacks)
                throw new ArgumentException($"Number of packs must be at least {MinimumPacks}.", nameof(numberOfPacks));

            var allCards = new ConcurrentBag<TObject>();

            Parallel.For(0, numberOfPacks, _ =>
            {
                var generatedCards = GenerateCards(cardType);
                foreach (var card in generatedCards)
                {
                    allCards.Add(card);
                }
            });

            var allCardsList = allCards.ToList();
            Shuffle(allCardsList);
            return allCardsList;
        }

        private static void Shuffle(List<TObject> packsCards)
        {
            var random = Random.Shared; // Use Random.Shared for better performance and thread safety
            for (var i = packsCards.Count - 1; i > 0; i--)
            {
                var j = random.Next(0, i + 1);
                (packsCards[i], packsCards[j]) = (packsCards[j], packsCards[i]);
            }
        }

        private static List<TObject> GenerateCards(Func<TEnumSuit, TEnumRank, TObject> cardPackType)
        {
            return (from suit in Enum.GetValues<TEnumSuit>()
                    from rank in Enum.GetValues<TEnumRank>()
                    select cardPackType(suit, rank)).ToList();
        }
    }
}
