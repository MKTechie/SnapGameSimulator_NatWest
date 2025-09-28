namespace SnapGameSimulator.CardType;

/// <summary>
/// Represents the rank of a playing card in a standard deck.
/// </summary>
/// <remarks>The <see cref="Rank"/> enumeration defines the ranks of cards in ascending order,  starting from <see
/// cref="Two"/> (2) and ending with <see cref="Ace"/>.  These values can be used to compare card ranks or to represent
/// cards in card games.</remarks>
public enum Rank
{
    Two = 2,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
    Ace
}