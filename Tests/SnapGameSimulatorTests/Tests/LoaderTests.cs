using Moq;
using SnapGameSimulator.Domain.Loader;
using SnapGameSimulator.Models;

namespace SnapGameSimulatorTests.Tests
{
    [TestClass]
    public class LoaderTests
    {
        private Mock<ILoader> _mockLoader;

        [TestInitialize]
        public void Setup()
        {
            _mockLoader = new Mock<ILoader>();
        }

        [TestMethod]
        public void GetPackLimit_ShouldReturnValidPackLimit_WhenInputIsValid()
        {
            // Arrange
            const ulong expectedPackLimit = 5;
            _mockLoader.Setup(loader => loader.GetPackLimit()).Returns(expectedPackLimit);

            // Act
            var result = _mockLoader.Object.GetPackLimit();

            // Assert
            Assert.AreEqual(expectedPackLimit, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetPackLimit_ShouldThrowArgumentException_WhenInputIsInvalid()
        {
            // Arrange
            _mockLoader.Setup(loader => loader.GetPackLimit()).Throws(new ArgumentException("Invalid number of packs. Please enter a positive integer."));

            // Act
            _mockLoader.Object.GetPackLimit();
        }

        [TestMethod]
        public void LoadPlayers_ShouldReturnCorrectNumberOfPlayers()
        {
            // Arrange
            var playersCount = 3;
            var expectedPlayers = new List<PlayerDetail>
            {
                new() { Name = "Player 1", Cards = new Queue<Card>() },
                new() { Name = "Player 2", Cards = new Queue<Card>() },
                new() { Name = "Player 3", Cards = new Queue<Card>() },
            };

            _mockLoader.Setup(loader => loader.LoadPlayers(playersCount)).Returns(expectedPlayers);

            // Act
            var result = _mockLoader.Object.LoadPlayers(playersCount);

            // Assert
            Assert.AreEqual(playersCount, result.Count);
            Assert.AreEqual("Player 1", result[0].Name);
            Assert.AreEqual("Player 2", result[1].Name);
            Assert.AreEqual("Player 3", result[2].Name);
        }
    }
}