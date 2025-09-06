using System.Collections;

namespace SnapGameSimulator.Models
{
    public record PlayerDetail
    {
        public required string Name { get; init; }

        public required Queue<Card> Cards { get; init; }
    }
}
