using SnapGameSimulator.CardType;

namespace SnapGameSimulator.Models;

public record Card(Suit Suit, Rank Rank)
{
    public override string ToString()
    {
        return $"{Rank} of {Suit}";
    }
}