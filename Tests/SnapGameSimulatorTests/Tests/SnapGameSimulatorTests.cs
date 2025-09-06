using Moq;
using SnapGameSimulator.CardType;
using SnapGameSimulator.Domain.CardPackGenerator;
using System.Collections.Concurrent;
using SnapGameSimulator.Models;

namespace SnapGameSimulatorTests.Tests
{
    [TestClass]
    public class CardPackGeneratorTests
    {
        private CardPackGenerator<Card, Suit, Rank> _cardPackGenerator;

        [TestInitialize]
        public void Setup()
        {
            _cardPackGenerator = new CardPackGenerator<Card, Suit, Rank>();
        }

        [TestMethod]
        public void GetCardsPack_ShouldReturnCorrectNumberOfCards_WhenSinglePackIsGenerated()
        {
            // Arrange
            var mockCardFactory = new Mock<Func<Suit, Rank, Card>>();
            mockCardFactory.Setup(factory => factory(It.IsAny<Suit>(), It.IsAny<Rank>()))
                .Returns((Suit suit, Rank rank) => new Card(suit, rank));

            // Act
            var result = _cardPackGenerator.GetCardsPack(1, mockCardFactory.Object);

            // Assert
            var expectedCardCount = Enum.GetValues<Suit>().Length * Enum.GetValues<Rank>().Length;
            Assert.AreEqual(expectedCardCount, result.Count);
        }

        [TestMethod]
        public void GetCardsPack_ShouldReturnCorrectNumberOfCards_WhenMultiplePacksAreGenerated()
        {
            // Arrange
            var mockCardFactory = new Mock<Func<Suit, Rank, Card>>();
            mockCardFactory.Setup(factory => factory(It.IsAny<Suit>(), It.IsAny<Rank>()))
                .Returns((Suit suit, Rank rank) => new Card(suit, rank));

            var numberOfPacks = 3;

            // Act
            var result = _cardPackGenerator.GetCardsPack(numberOfPacks, mockCardFactory.Object);

            // Assert
            var expectedCardCount = numberOfPacks * Enum.GetValues<Suit>().Length * Enum.GetValues<Rank>().Length;
            Assert.AreEqual(expectedCardCount, result.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetCardsPack_ShouldThrowArgumentException_WhenNumberOfPacksIsLessThanMinimum()
        {
            // Arrange
            var mockCardFactory = new Mock<Func<Suit, Rank, Card>>();

            // Act
            _cardPackGenerator.GetCardsPack(0, mockCardFactory.Object);
        }

        [TestMethod]
        public void GetCardsPack_ShouldShuffleCards()
        {
            // Arrange
            var mockCardFactory = new Mock<Func<Suit, Rank, Card>>();
            mockCardFactory.Setup(factory => factory(It.IsAny<Suit>(), It.IsAny<Rank>()))
                .Returns((Suit suit, Rank rank) => new Card(suit, rank));

            var numberOfPacks = 1;

            // Act
            var result = _cardPackGenerator.GetCardsPack(numberOfPacks, mockCardFactory.Object);

            // Assert
            var expectedCardCount = Enum.GetValues<Suit>().Length * Enum.GetValues<Rank>().Length;
            Assert.AreEqual(expectedCardCount, result.Count);

            // Verify that the cards are shuffled (not in the original order)
            var originalOrder = new ConcurrentBag<Card>();
            foreach (var suit in Enum.GetValues<Suit>())
            {
                foreach (var rank in Enum.GetValues<Rank>())
                {
                    originalOrder.Add(new Card(suit, rank));
                }
            }

            CollectionAssert.AreNotEqual(originalOrder.ToList(), result);
        }
    }
}