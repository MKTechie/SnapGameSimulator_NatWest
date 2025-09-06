using SnapGameSimulator.CardType;
using SnapGameSimulator.Domain.CardPackGenerator;
using SnapGameSimulator.Domain.Loader;
using SnapGameSimulator.Domain.WinnerSelector;
using SnapGameSimulator.Models;

namespace SnapGameSimulator.Domain;

/// <summary>
/// Game engine for simulating the Snap card game.
/// </summary>
public sealed class GameEngine
{
    private readonly IWinnerSelector _winnerSelector;
    private readonly ICardPackGenerator<Card, Suit, Rank> _cardPackGenerator;
    private List<PlayerDetail> _playerDetail;
    private long _packLimit;
    private readonly ILoader _loader;
    private readonly int _playersCount;
    /// <summary>
    /// Initializes a new instance of the GameEngine class.
    /// </summary>
    public GameEngine(ILoader loader, IWinnerSelector winnerSelector, ICardPackGenerator<Card, Suit, Rank> cardPackGenerator,  int playersCount)
    {
         _playerDetail = [];
         _winnerSelector = winnerSelector;
         _cardPackGenerator = cardPackGenerator;
         _loader = loader;
         _playersCount = playersCount;
    }

    /// <summary>
    /// Starts the game.
    /// </summary>
    /// <returns></returns>
    public async Task Start()
    {
        LoadGameSettings();
        await PlayAsync().ConfigureAwait(false);
    }

    private async Task PlayAsync()
    {
       
        var pile = new Stack<Card>();
        if (_packLimit <= 3)
        {
            ProcessCards(_packLimit, _packLimit, (int)_packLimit, pile);
        }
        else
        {
            HandleMultiplePacks(pile);
        }

        DisplayResults(pile);
        var result = await _winnerSelector.SelectWinner(_playerDetail).ConfigureAwait(false);
        Console.WriteLine(result);
    }

    private void HandleMultiplePacks(Stack<Card> pile)
    {
        if (_packLimit % 2 == 0)
        {
            long exhaustLimit = 2;
            ProcessCards(_packLimit, exhaustLimit, 2, pile);
        }
        else
        {
            var exhaustLimit = _packLimit % 3;
            ProcessCards(exhaustLimit, exhaustLimit, 2, pile);
            // Logic for handling odd number of packs greater than 3
            var remainingPacks = _packLimit - exhaustLimit;
            if (remainingPacks % 3 == 0)
            {
                ProcessCards(remainingPacks, exhaustLimit, 3, pile);
            }
        }
    }

    private void ProcessCards(long packLimit, long exhaustLimit, int numberOfPacks, Stack<Card> pile)
    {
        while (exhaustLimit <= packLimit)
        {
            var cards = _cardPackGenerator.GetCardsPack(numberOfPacks, (suit, rank) => new Card(suit, rank));
            CardsMovement(pile, cards);
            exhaustLimit += numberOfPacks;
            Console.WriteLine($"Dealing cards from {numberOfPacks} pack(s)... {exhaustLimit} exhaust limit");
        }
    }

    private void LoadGameSettings()
    {
        Console.Write("Welcome to snap game.Loading...");
        _packLimit = _loader.GetPackLimit();
        Console.WriteLine($"Number of players {_playersCount}");
        _playerDetail = _loader.LoadPlayers(_playersCount);
    }

    private void DisplayResults(Stack<Card> pile)
    {
        if (pile.Count > 0)
        {
            Console.WriteLine($"Discarded cards count {pile.Count}");
        }

        foreach (var player in _playerDetail)
        {
            Console.WriteLine($"Total number of cards {player.Name} has won {player.Cards.Count} ");
        }
    }

    private void CardsMovement(Stack<Card> pile, List<Card> cards)
    {
        Card? lastCard = null;
        var playerBucket = new List<string>();

        for (var i = 0; i < cards.Count;)
        {
            foreach (var playerDetail in _playerDetail.Where(_ => i < cards.Count))
            {
                Console.WriteLine($"{playerDetail.Name} Turn.Count: {playerDetail.Cards.Count} cards");
                playerBucket.Add(playerDetail.Name);
                var currentCard = cards[i];
                Console.WriteLine($"Card {cards[i]} on the pile...");
                pile.Push(cards[i]);
                if (MatchingCards(lastCard, currentCard))
                {
                    ProcessPile(pile, playerBucket);
                    i++;
                    lastCard = null;
                    continue;
                }

                lastCard = cards[i];
                i++;
            }

            playerBucket.Clear();
        }
    }

    public void ProcessPile(Stack<Card> pile, List<string> playerBucket)
    {
        var winner = PileWinner(playerBucket);
        Console.WriteLine($"Snap! {winner} wins the pile!");
        var player = _playerDetail.First(p => p.Name == winner);
        CollectPile(player.Cards, pile);
        Console.WriteLine($"{winner} now has {player.Cards.Count} cards.");
    }

    private static bool MatchingCards(Card? lastCard, Card currentCard)
    {
        return lastCard != null && (currentCard.Rank == lastCard.Rank || currentCard.Suit == lastCard.Suit);
    }

    private static string PileWinner(List<string> playerBucket)
    {
        // Randomly select a player from the playerBucket
        var random = new Random();
        return playerBucket[random.Next(playerBucket.Count)];
    }
    
    private static void CollectPile(Queue<Card> playerDeck, Stack<Card> pile)
    {
        while (pile.Count > 0)
        {
            playerDeck.Enqueue(pile.Pop());
        }
    }
}