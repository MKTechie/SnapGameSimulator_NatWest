using Moq;
using SnapGameSimulator.CardType;
using SnapGameSimulator.Domain;
using SnapGameSimulator.Domain.CardPackGenerator;
using SnapGameSimulator.Domain.Loader;
using SnapGameSimulator.Domain.Localization;
using SnapGameSimulator.Domain.WinnerSelector;
using SnapGameSimulator.Models;
using SnapGameSimulator.Settings;

namespace SnapGameSimulatorTests.Tests
{
    [TestClass]
    public class GameEngineTests
    {
        private Mock<ILoader> _mockLoader;
        private Mock<IWinnerSelector> _mockWinnerSelector;
        private Mock<ICardPackGenerator<Card, Suit, Rank>> _mockCardPackGenerator;
        private Mock<ITranslationVault> _mockTranslationVault;
        private Mock<IUserSettings> _mockUserSettings;
        private GameEngine _gameEngine;

        [TestInitialize]
        public void Setup()
        {
            _mockLoader = new Mock<ILoader>();
            _mockWinnerSelector = new Mock<IWinnerSelector>();
            _mockCardPackGenerator = new Mock<ICardPackGenerator<Card, Suit, Rank>>();
            _mockTranslationVault = new Mock<ITranslationVault>();
            _mockUserSettings = new Mock<IUserSettings>();

            // Initialize GameEngine with mocked dependencies
            _gameEngine = new GameEngine(
                _mockLoader.Object,
                _mockWinnerSelector.Object,
                _mockCardPackGenerator.Object,
                _mockTranslationVault.Object,
                _mockUserSettings.Object,
                2
            );
        }

        [TestMethod]
        public async Task Start_ShouldLoadPlayersAndPlayGame()
        {
            // Arrange
            var mockPlayers = new List<PlayerDetail>
            {
                new() { Name = "Player 1", Cards = new Queue<Card>() },
                new() { Name = "Player 2", Cards = new Queue<Card>() }
            };

            _mockLoader.Setup(loader => loader.GetPackLimit()).Returns(3);
            _mockLoader.Setup(loader => loader.LoadPlayers(2)).Returns(mockPlayers);
            _mockWinnerSelector.Setup(selector => selector.SelectWinner(It.IsAny<List<PlayerDetail>>()))
                .ReturnsAsync("The winner is Player 1");

            var mockCards = new List<Card>
            {
                new(Suit.Hearts, Rank.Ace),
                new(Suit.Clubs, Rank.Two),
                new(Suit.Spades, Rank.Three)
            };

            _mockCardPackGenerator
                .Setup(generator => generator.GetCardsPack(It.IsAny<int>(), It.IsAny<Func<Suit, Rank, Card>>()))
                .Returns(mockCards);

            // Act
            await _gameEngine.Start();

            // Assert
            _mockLoader.Verify(loader => loader.GetPackLimit(), Times.Once);
            _mockLoader.Verify(loader => loader.LoadPlayers(2), Times.Once);
            _mockWinnerSelector.Verify(selector => selector.SelectWinner(It.IsAny<List<PlayerDetail>>()), Times.Once);
            _mockCardPackGenerator.Verify(generator => generator.GetCardsPack(It.IsAny<int>(), It.IsAny<Func<Suit, Rank, Card>>()), Times.AtLeastOnce);
        }

        [TestMethod]
        public async Task Start_ShouldHandleMultiplePacksCorrectly()
        {
            // Arrange
            var mockPlayers = new List<PlayerDetail>
            {
                new() { Name = "Player 1", Cards = new Queue<Card>() },
                new() { Name = "Player 2", Cards = new Queue<Card>() }
            };

            _mockLoader.Setup(loader => loader.GetPackLimit()).Returns(5);
            _mockLoader.Setup(loader => loader.LoadPlayers(2)).Returns(mockPlayers);
            _mockWinnerSelector.Setup(selector => selector.SelectWinner(It.IsAny<List<PlayerDetail>>()))
                .ReturnsAsync("The winner is Player 2");

            var mockCards = new List<Card>
            {
                new(Suit.Hearts, Rank.Ace),
                new(Suit.Clubs, Rank.Two),
                new(Suit.Spades, Rank.Three)
            };

            _mockCardPackGenerator
                .Setup(generator => generator.GetCardsPack(It.IsAny<int>(), It.IsAny<Func<Suit, Rank, Card>>()))
                .Returns(mockCards);

            // Act
            await _gameEngine.Start();

            // Assert
            _mockLoader.Verify(loader => loader.GetPackLimit(), Times.Once);
            _mockLoader.Verify(loader => loader.LoadPlayers(2), Times.Once);
            _mockCardPackGenerator.Verify(generator => generator.GetCardsPack(It.IsAny<int>(), It.IsAny<Func<Suit, Rank, Card>>()), Times.AtLeastOnce);
        }

        [TestMethod]
        public async Task Start_ShouldDisplayResultsCorrectly()
        {
            // Arrange
            var mockPlayers = new List<PlayerDetail>
            {
                new() { Name = "Player 1", Cards = new Queue<Card>() },
                new() { Name = "Player 2", Cards = new Queue<Card>() }
            };

            _mockLoader.Setup(loader => loader.GetPackLimit()).Returns(3);
            _mockLoader.Setup(loader => loader.LoadPlayers(2)).Returns(mockPlayers);
            _mockWinnerSelector.Setup(selector => selector.SelectWinner(It.IsAny<List<PlayerDetail>>()))
                .ReturnsAsync("The winner is Player 1");

            var mockCards = new List<Card>
            {
                new(Suit.Hearts, Rank.Ace),
                new(Suit.Clubs, Rank.Two),
                new(Suit.Spades, Rank.Three)
            };

            _mockCardPackGenerator
                .Setup(generator => generator.GetCardsPack(It.IsAny<int>(), It.IsAny<Func<Suit, Rank, Card>>()))
                .Returns(mockCards);

            // Act
            await _gameEngine.Start();

            // Assert
            _mockLoader.Verify(loader => loader.GetPackLimit(), Times.Once);
            _mockLoader.Verify(loader => loader.LoadPlayers(2), Times.Once);
            _mockWinnerSelector.Verify(selector => selector.SelectWinner(It.IsAny<List<PlayerDetail>>()), Times.Once);
        }
    }
}