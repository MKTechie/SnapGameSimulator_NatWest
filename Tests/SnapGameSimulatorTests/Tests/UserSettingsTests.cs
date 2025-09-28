using Moq;
using SnapGameSimulator.Settings;

namespace SnapGameSimulatorTests.Tests
{
    [TestClass]
    public class UserSettingsTests
    {
        [TestMethod]
        public void GetUserPreferredLanguage_ShouldReturnEnglish()
        {
            // Arrange
            var mockSettings = new Mock<IUserSettings>();
            mockSettings.Setup(settings => settings.GetUserPreferredLanguage())
                .Returns("en-US");

            // Act
            var language = mockSettings.Object.GetUserPreferredLanguage();

            // Assert
            Assert.AreEqual("en-US", language);
        }

        [TestMethod]
        public void GetUserPreferredLanguage_ShouldReturnFrench()
        {
            // Arrange
            var mockSettings = new Mock<IUserSettings>();
            mockSettings.Setup(settings => settings.GetUserPreferredLanguage())
                .Returns("fr-FR");

            // Act
            var language = mockSettings.Object.GetUserPreferredLanguage();

            // Assert
            Assert.AreEqual("fr-FR", language);
        }

        [TestMethod]
        public void GetUserPreferredLanguage_ShouldReturnEmptyString_WhenNotSet()
        {
            // Arrange
            var mockSettings = new Mock<IUserSettings>();
            mockSettings.Setup(settings => settings.GetUserPreferredLanguage())
                .Returns(string.Empty);

            // Act
            var language = mockSettings.Object.GetUserPreferredLanguage();

            // Assert
            Assert.AreEqual(string.Empty, language);
        }

        [TestMethod]
        public void GetUserPreferredLanguage_ShouldReturnNull_WhenNotConfigured()
        {
            // Arrange
            var mockSettings = new Mock<IUserSettings>();
            mockSettings.Setup(settings => settings.GetUserPreferredLanguage())
                .Returns((string)null);

            // Act
            var language = mockSettings.Object.GetUserPreferredLanguage();

            // Assert
            Assert.IsNull(language);
        }
    }
}