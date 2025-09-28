using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SnapGameSimulator.CardType;
using SnapGameSimulator.Domain;
using SnapGameSimulator.Domain.CardPackGenerator;
using SnapGameSimulator.Domain.Loader;
using SnapGameSimulator.Domain.Localization;
using SnapGameSimulator.Domain.WinnerSelector;
using SnapGameSimulator.Models;
using SnapGameSimulator.Settings;

namespace SnapGameSimulator;

/// <summary>
/// The entry point of the application.
/// </summary>
/// <remarks>This method initializes the dependency injection container, configures services, and starts the game
/// engine. The application expects an optional command-line argument specifying the number of players. If no argument
/// is provided, the default player count is set to 2. Any unhandled exceptions during execution are logged.</remarks>
public class Program
{
    /// <summary>
    /// The entry point of the application. Configures services, initializes dependencies, and starts the game engine.
    /// </summary>
    /// <remarks>This method sets up the dependency injection container, retrieves required services, and
    /// initializes the game engine. It handles unhandled exceptions by logging them.</remarks>
    /// <param name="args">An array of command-line arguments. The first argument, if provided, specifies the number of players. Defaults
    /// to 2 players if not specified or invalid.</param>
    /// <returns></returns>
    public static async Task Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        using var serviceProvider = serviceCollection.BuildServiceProvider();

        try
        {
            var loader = serviceProvider.GetRequiredService<ILoader>();
            var winnerSelector = serviceProvider.GetRequiredService<IWinnerSelector>();
            var cardPackGenerator = serviceProvider.GetRequiredService<ICardPackGenerator<Card, Suit, Rank>>();
            var translationVault = serviceProvider.GetRequiredService<ITranslationVault>();
            var userSettings = serviceProvider.GetRequiredService<IUserSettings>();

            int playerCount = args.Length > 0 && int.TryParse(args[0], out var count) ? count : 2;

            var game = new GameEngine(loader, winnerSelector, cardPackGenerator, translationVault, userSettings, playerCount);

            await game.Start();
        }
        catch (Exception)
        {
            Console.WriteLine("Something went wrong!!!");
        }
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ILoader, Loader>();
        services.AddSingleton<IWinnerSelector, WinnerSelector>();
        services.AddSingleton<IUserSettings, UserSettings>();
        services.AddSingleton<ITranslationVault, TranslationVault>();
        services.AddSingleton<ICardPackGenerator<Card, Suit, Rank>, CardPackGenerator<Card, Suit, Rank>>();
    }
}