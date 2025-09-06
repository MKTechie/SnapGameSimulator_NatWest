using SnapGameSimulator.CardType;
using SnapGameSimulator.Domain;
using SnapGameSimulator.Domain.CardPackGenerator;
using SnapGameSimulator.Domain.Loader;
using SnapGameSimulator.Domain.WinnerSelector;
using SnapGameSimulator.Models;

namespace SnapGameSimulator;

public class Program
{
    static void Main(string[] args)
    {
        // will replace with dependnecy injection for the required services.
        ILoader loader = new Loader();
        IWinnerSelector winnerSelector = new WinnerSelector();
        ICardPackGenerator<Card,Suit, Rank> cardPackGenerator = new CardPackGenerator<Card, Suit, Rank> ();
        var game = new GameEngine(loader, winnerSelector, cardPackGenerator, 2);
        _ = game.Start().ConfigureAwait(false);
    }
}



