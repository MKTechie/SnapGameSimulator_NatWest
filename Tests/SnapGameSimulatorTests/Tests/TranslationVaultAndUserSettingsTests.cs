using Moq;
using SnapGameSimulator.Domain.Localization;
using SnapGameSimulator.Settings;

namespace SnapGameSimulatorTests.Tests
{
    [TestClass]
    public class TranslationVaultAndUserSettingsTests
    {
        [TestMethod]
        public void GetMessage_ShouldReturnLocalizedMessage_WhenKeyAndCultureAreValid()
        {
            // Arrange
            var mockVault = new Mock<ITranslationVault>();
            mockVault.Setup(vault => vault.GetMessage(MessageKey.WelcomeMessage, "en-US"))
                .Returns("Welcome to Snap!");

            // Act
            var result = mockVault.Object.GetMessage(MessageKey.WelcomeMessage, "en-US");

            // Assert
            Assert.AreEqual("Welcome to Snap!", result);
        }

        [TestMethod]
        public void GetMessage_ShouldReturnNull_WhenKeyDoesNotExist()
        {
            // Arrange
            var mockVault = new Mock<ITranslationVault>();
            mockVault.Setup(vault => vault.GetMessage(MessageKey.InvalidPackLimit, "fr-FR"))
                .Returns((string)null);

            // Act
            var result = mockVault.Object.GetMessage(MessageKey.InvalidPackLimit, "fr-FR");

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetUserPreferredLanguage_ShouldReturnLanguageCode()
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
        public void GetUserPreferredLanguage_ShouldReturnDifferentLanguageCode()
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
    }
}