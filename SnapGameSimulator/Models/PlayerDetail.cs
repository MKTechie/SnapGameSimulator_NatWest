namespace SnapGameSimulator.Models
{
    /// <summary>
    /// Player detail including name and their cards.
    /// </summary>
    public record PlayerDetail
    {
        /// <summary>
        /// Gets the name associated with the object. This property is required and must be set during initialization.
        /// </summary>
        public required string Name { get; init; }

        /// <summary>
        /// Gets the queue of cards to be processed or accessed.
        /// </summary>
        public required Queue<Card> Cards { get; init; }
    }
}
