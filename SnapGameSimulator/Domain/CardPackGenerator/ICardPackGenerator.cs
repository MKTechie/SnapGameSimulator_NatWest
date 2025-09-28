using System.ComponentModel.DataAnnotations;

namespace SnapGameSimulator.Domain.CardPackGenerator
{
    /// <summary>
    /// Defines a contract for generating shuffled card packs using specified card types, suits, and ranks.
    /// </summary>
    /// <remarks>This interface provides a method to generate a collection of card objects, allowing
    /// customization of the card type and the number of packs to include. The generated cards are shuffled to ensure
    /// randomness.</remarks>
    /// <typeparam name="TObject">The type representing a card object.</typeparam>
    /// <typeparam name="TEnumSuit">The enumeration type representing card suits.</typeparam>
    /// <typeparam name="TEnumRank">The enumeration type representing card ranks.</typeparam>
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