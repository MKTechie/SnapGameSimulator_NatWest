using SnapGameSimulator.CardType;
using SnapGameSimulator.Domain.CardPackGenerator;
using SnapGameSimulator.Domain.Loader;
using SnapGameSimulator.Domain.Localization;
using SnapGameSimulator.Domain.WinnerSelector;
using SnapGameSimulator.Extension;
using SnapGameSimulator.Helper;
using SnapGameSimulator.Models;
using SnapGameSimulator.Settings;

namespace SnapGameSimulator.Domain;

/// <summary>
/// Game engine for simulating the Snap card game.
/// </summary>
public sealed class GameEngine
{
    private readonly IWinnerSelector _winnerSelector;
    private readonly ICardPackGenerator<Card, Suit, Rank> _cardPackGenerator;
    private readonly ITranslationVault _translationVault;
    private List<PlayerDetail> _playerDetail;
    private ulong _packLimit;
    private readonly ILoader _loader;
    private readonly int _playersCount;
    private readonly IUserSettings _userSettings;
    private const int doublePack = 2;
    private const int ThriplePacks = 3;

    /// <summary>
    /// Initializes a new instance of the GameEngine class.
    /// </summary>
    public GameEngine(ILoader loader, IWinnerSelector winnerSelector, ICardPackGenerator<Card, Suit, Rank> cardPackGenerator, ITranslationVault translationVault, IUserSettings userSettings, int playersCount)
    {
        _playerDetail = [];
        _winnerSelector = winnerSelector;
        _cardPackGenerator = cardPackGenerator;
        _loader = loader;
        _translationVault = translationVault;
        _userSettings = userSettings;
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
        result.PrintMessage();
    }

    private void HandleMultiplePacks(Stack<Card> pile)
    {
        if (_packLimit % doublePack == 0)
        {
            ProcessCards(_packLimit, doublePack, doublePack, pile);
        }
        else
        {
            var exhaustLimit = _packLimit % ThriplePacks;
            ProcessCards(exhaustLimit, exhaustLimit, doublePack, pile);
            // Logic for handling odd number of packs greater than 3
            var remainingPacks = _packLimit - exhaustLimit;
            if (remainingPacks % ThriplePacks == 0)
            {
                ProcessCards(remainingPacks, exhaustLimit, ThriplePacks, pile);
            }
        }
    }

    private void ProcessCards(ulong packLimit, ulong exhaustLimit, int numberOfPacks, Stack<Card> pile)
    {
        while (exhaustLimit <= packLimit)
        {
            var cards = _cardPackGenerator.GetCardsPack(numberOfPacks, (suit, rank) => new Card(suit, rank));
            CardsMovement(pile, cards);
            exhaustLimit += (ulong)numberOfPacks;
            Console.WriteLine($"Dealing cards from {numberOfPacks} pack(s)... {exhaustLimit} exhaust limit");
        }
    }

    private void LoadGameSettings()
    {
        var message = _translationVault.GetMessage(MessageKey.WelcomeMessage, _userSettings.GetUserPreferredLanguage());
        message.PrintMessage();
        _packLimit = _loader.GetPackLimit();
        var totalPlayers = _translationVault.GetMessage(MessageKey.TotalPlayers, _userSettings.GetUserPreferredLanguage());
        GameEngineHelper.ShowPlayersCount(totalPlayers, _playersCount);
        _playerDetail = _loader.LoadPlayers(_playersCount);
    }

    private void DisplayResults(Stack<Card> pile)
    {
        var discarPile = _translationVault.GetMessage(MessageKey.DiscardedMessage, _userSettings.GetUserPreferredLanguage());
        GameEngineHelper.DisplayDiscardedPile(discarPile, pile.Count);

        var displayResult = _translationVault.GetMessage(MessageKey.Result, _userSettings.GetUserPreferredLanguage());
        GameEngineHelper.DisplayPlayerResults(displayResult, _playerDetail);
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
                Thread.Sleep(900);
                playerBucket.Add(playerDetail.Name);
                var currentCard = cards[i];
                Console.WriteLine($"Card {cards[i]} on the pile...");
                pile.Push(cards[i]);
                if (MatchingCards(lastCard, currentCard))
                {
                    ProcessPile(pile, playerBucket);
                    Thread.Sleep(900);
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
        var winner = PileWinnerHelper.Selection(playerBucket);
        var snapMessage = _translationVault.GetMessage(MessageKey.PileWinner, _userSettings.GetUserPreferredLanguage());
        $"{snapMessage} {winner}".PrintMessage();
        var player = _playerDetail.First(p => p.Name == winner);
        CollectPile(player.Cards, pile);
        var totalCards = _translationVault.GetMessage(MessageKey.TotalCards, _userSettings.GetUserPreferredLanguage());
        $"{winner} - {totalCards} {player.Cards.Count}".PrintMessage();
    }

    private static bool MatchingCards(Card? lastCard, Card currentCard)
    {
        return lastCard != null && (currentCard.Rank == lastCard.Rank || currentCard.Suit == lastCard.Suit);
    }

    private static void CollectPile(Queue<Card> playerDeck, Stack<Card> pile)
    {
        while (pile.Count > 0)
        {
            playerDeck.Enqueue(pile.Pop());
        }
    }
   
}