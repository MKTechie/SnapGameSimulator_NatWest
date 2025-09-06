using SnapGameSimulator.CardType;
using SnapGameSimulator.Domain.WinnerSelector;
using SnapGameSimulator.Models;

namespace SnapGameSimulatorTests.Tests
{
    [TestClass]
    public class WinnerSelectorTests
    {
        private WinnerSelector _winnerSelector;

        [TestInitialize]
        public void Setup()
        {
            _winnerSelector = new WinnerSelector();
        }

        [TestMethod]
        public async Task SelectWinner_ShouldReturnWinner_WhenSinglePlayerHasMaxCards()
        {
            // Arrange
            var playerDetails = new List<PlayerDetail>
            {
                new()
                {
                    Name = "Player1",
                    Cards = new Queue<Card>(new[]
                    {
                        new Card(Suit.Hearts, Rank.Ace),
                        new Card(Suit.Diamonds, Rank.Two)
                    })
                },
                new()
                {
                    Name = "Player2",
                    Cards = new Queue<Card>(new[]
                    {
                        new Card(Suit.Clubs, Rank.Three),
                        new Card(Suit.Spades, Rank.Four)
                    })
                },
                new()
                {
                    Name = "Player3",
                    Cards = new Queue<Card>(new[]
                    {
                        new Card(Suit.Spades, Rank.King),
                        new Card(Suit.Diamonds, Rank.Queen),
                        new Card(Suit.Hearts, Rank.Ten)
                    })
                }
            };

            // Act
            var result = await _winnerSelector.SelectWinner(playerDetails);

            // Assert
            Assert.AreEqual("The winner is Player3", result);
        }

        [TestMethod]
        public async Task SelectWinner_ShouldReturnDraw_WhenMultiplePlayersHaveMaxCards()
        {
            // Arrange
            var playerDetails = new List<PlayerDetail>
            {
                new()
                {
                    Name = "Player1",
                    Cards = new Queue<Card>(new[]
                    {
                        new Card(Suit.Hearts, Rank.Ace),
                        new Card(Suit.Diamonds, Rank.Two)
                    })
                },
                new()
                {
                    Name = "Player2",
                    Cards = new Queue<Card>(new[]
                    {
                        new Card(Suit.Clubs, Rank.Three),
                        new Card(Suit.Spades, Rank.Four),
                        new Card(Suit.Hearts, Rank.Five)
                    })
                },
                new PlayerDetail
                {
                    Name = "Player3",
                    Cards = new Queue<Card>(new[]
                    {
                        new Card(Suit.Diamonds, Rank.Queen),
                        new Card(Suit.Hearts, Rank.Jack),
                        new Card(Suit.Spades, Rank.Nine)
                    })
                }
            };

            // Act
            var result = await _winnerSelector.SelectWinner(playerDetails);

            // Assert
            Assert.AreEqual("A draw!!!", result);
        }
    }
}