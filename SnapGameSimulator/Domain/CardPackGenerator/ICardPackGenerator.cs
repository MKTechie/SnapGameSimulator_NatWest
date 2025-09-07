using System.ComponentModel.DataAnnotations;

namespace SnapGameSimulator.Domain.CardPackGenerator
{
    public interface ICardPackGenerator<TObject, TEnumSuit, TEnumRank>
        where TObject : class
        where TEnumSuit : struct, Enum
        where TEnumRank : struct, Enum
    {
        /// <summary>
        /// Generates and returns a shuffled list of card objects based on the specified number of packs.
        /// </summary>
        /// <param name="numberOfPacks">Number of packs of cards</param>
        /// <param name="cardType">Type of cards</param>
        /// <returns>Returns the shuffled cards</returns>
        /// <exception cref="ArgumentException">Throws an exception</exception>
        List<TObject> GetCardsPack([Range(1, int.MaxValue, ErrorMessage = "")] int numberOfPacks, Func<TEnumSuit, TEnumRank, TObject> cardType);
    }
}