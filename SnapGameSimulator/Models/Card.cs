using SnapGameSimulator.CardType;

namespace SnapGameSimulator.Models;

/// <summary>
/// Card representation with suit and rank.
/// </summary>
/// <param name="Suit"><see cref="Suit"/></param>
/// <param name="Rank"><see cref="Rank"/></param>
public record Card(Suit Suit, Rank Rank)
{
    /// <summary>
    /// Override of ToString method to provide a string representation of the card.
    /// </summary>
    /// <returns>returns the concatenated string of rank of suit.</returns>
    public override string ToString()
    {
        return $"{Rank} of {Suit}";
    }
}