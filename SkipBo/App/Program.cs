using System;
using System.Threading;
using SkipBo.App.AiPlayer;

namespace SkipBo.App
{
    public class Program
    {
        private static bool _isRunning = true;
        private static int _gameLength;
        private static Deck _deck;
        private static Human _player;
        private static Ai _ai;
        private static Board _board;
        private static bool _isPlayersTurn = true;
        private static Ui _ui;

        public static void Main(string[] args)
        {
            Console.Title = "SkipBo";
            Console.CancelKeyPress += Console_CancelKeyPress;
            _deck = Deck.Instance;
            _ui = Ui.Instance;
            _board = Board.Instance;
            _player = new Human();
            _ai = new Ai();
            _ui.Player = _player;
            _ui.Ai = _ai;
            StartUpMessage();
            MainLoop();
        }

        private static void StartUpMessage()
        {
            Console.WriteLine("Welcome to SkipBo\n");
            Console.WriteLine("What game length do you want to play?\n");
            Console.WriteLine("Short: 10 cards\n");
            Console.WriteLine("Medium: 20 cards\n");
            Console.WriteLine("Long: 30 cards\n");
            GetGameLength();
        }

        public static void GetGameLength()
        {
            string input;
            var gotInput = false;
            while (!gotInput && !string.IsNullOrEmpty(input = Console.ReadLine()?.ToLower()))
            {
                if (input.ToLower() == "quit")
                    Environment.Exit(0);

                int length;
                if (int.TryParse(input, out length))
                {
                    if (length != 10 && length != 20 && length != 30)
                        continue;
                    _gameLength = length;
                    gotInput = true;
                }
                else
                {
                    if (input != "short" && input != "medium" && input != "long")
                        continue;
                    if (input == "short")
                        _gameLength = 10;
                    else if (input == "medium")
                        _gameLength = 20;
                    else if (input == "long")
                        _gameLength = 30;
                    gotInput = true;
                }
            }
        }

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs eventArgs)
        {
            _isRunning = false;
            eventArgs.Cancel = true;
        }

        public static void MainLoop()
        {
            string line;

            Console.WriteLine($"Game Length of {_gameLength} starting:");

            _deck.Load();
            _deck.Shuffle();

            _player.FillSideDeck(_gameLength);
            _player.DrawHand();
            _ai.FillSideDeck(_gameLength);
            _ai.DrawHand();

            _ui.CreatePlayScreen();

            while (_isRunning && !string.IsNullOrEmpty(line = Console.ReadLine()))
            {
                HandleInput(line);
            }
        }

        public static void HandleInput(string line)
        {
            try
            {
                var input = ParseInput.For(line);
                switch (input.Action)
                {
                    case "quit":
                        _isRunning = false;
                        Environment.Exit(0);
                        break;
                    case "p":
                    case "play":
                        if (!_isPlayersTurn)
                            break;
                        _ui.Action($"Using card: {input.MainArg}", "Player");
                        if (_player.PlayCard(Card.From(input.MainArg), input.SecondaryArg))
                            _ui.CreatePlayScreen();
                        else
                            _ui.Action("Can't play with that card!", "Player");
                        _isPlayersTurn = true;
                        break;

                    case "d":
                    case "discard":
                        if (!_isPlayersTurn)
                            break;
                        _ui.Action($"Discarding card: {input.MainArg}", "Player");
                        if (_player.DiscardCard(Card.From(input.MainArg), input.SecondaryArg))
                        {
                            _isPlayersTurn = false;
                            AiPlay();
                        }
                        break;
                    case "draw":
                        if (!_isPlayersTurn)
                            break;
                        _player.DrawHand();
                        _ui.CreatePlayScreen();
                        break;
                    default:
                        _ui.CreatePlayScreen();
                        break;
                }
            }
            catch (Exception exception)
            {
                _ui.Error(exception.Message, exception.StackTrace);
            }
        }

        private static void AiPlay()
        {
            if (!_isPlayersTurn)
            {
                var cardsDrawn = _ai.DrawHand();
                if (cardsDrawn > 0)
                    _ui.Action($"drew {cardsDrawn} cards for their hand.", "AI");
                var canPlay = true;
                while (canPlay)
                {
                    canPlay = _ai.PlayCard();
                    if (_player.HasWon())
                    {
                        canPlay = false;
                        _ui.Won("Ai");
                    }
                    else
                    {
                        if (!canPlay)
                            _ai.FindAndDiscardCard();
                    }
                    Thread.Sleep(500);
                }

                _isPlayersTurn = true;
                _player.DrawHand();
                _ui.CreatePlayScreen();
            }
        }
    }
}
