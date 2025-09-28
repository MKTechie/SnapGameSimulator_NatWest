namespace SnapGameSimulator.Settings
{
    /// <inheritdoc/>
    public sealed class UserSettings : IUserSettings
    {
        public string Language { get; init; } = "en-US";

        /// <inheritdoc/>
        public string GetUserPreferredLanguage()
        {
            return Language;
        }
    }
}
