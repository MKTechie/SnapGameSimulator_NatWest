namespace SnapGameSimulator.Settings
{
    /// <summary>
    /// User Settings
    /// </summary>
    public interface IUserSettings
    {
        /// <summary>
        /// Gets the user preferred language. 
        /// </summary>
        /// <returns>Returns the preferred user language. default is en-us</returns>
        public string GetUserPreferredLanguage();
    }
}