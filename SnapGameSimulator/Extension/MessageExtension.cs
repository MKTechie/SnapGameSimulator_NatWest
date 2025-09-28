namespace SnapGameSimulator.Extension
{
    public static class MessageExtensions
    {
        /// <summary>
        /// Prints the specified message to the console.
        /// </summary>
        /// <param name="message">The message to print.</param>
        public static void PrintMessage(this string message)
        {
            Console.WriteLine(message);
        }
    }
}