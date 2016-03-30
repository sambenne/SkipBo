using System;
using System.Threading;
using SkipBo.App.ConsoleHelper;
using SkipBo.App.Core;

namespace SkipBo.App
{
    public class Program
    {
        private static Game _game;
        private static Ui _ui;

        public static void Main(string[] args)
        {
            Console.Title = "SkipBo";
            Console.CancelKeyPress += Console_CancelKeyPress;
            _game = new Game(Difficulty.Easy);
            _ui = Ui.Instance;
            _ui.Player = _game.Player;
            _ui.Ai = _game.Ai;
            _game.SetUp();
            StartUpMessage();
            MainLoop();
        }

        private static void StartUpMessage()
        {
            Ui.Write(0, 0, "Welcome to SkipBo!");
            Ui.Write(0, 2, "What game length do you want to play?");
            Ui.Write(5, 3, "Short: 10 cards");
            Ui.Write(5, 4, "Medium: 20 cards");
            Ui.Write(5, 5, "Long: 30 cards");
            Ui.Write(0, 7, " > ");
            Console.SetCursorPosition(3, 7);
            _game.GetGameLength();
        }

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs args)
        {
            Console.Clear();
            args.Cancel = true;
        }

        public static void MainLoop()
        {
            _ui.CreatePlayScreen();

            Ui.GetInput(line =>
            {
                var isRunning = HandleInput(line);
                _ui.CreatePlayScreen();
                return isRunning;
            });
        }

        public static bool HandleInput(string line)
        {
            try
            {
                var input = ParseInput.For(line);
                switch (input.Action)
                {
                    case "p":
                    case "play":
                        _ui.Action($"Using card: {input.MainArg}", "Player");
                        if (!_game.Player.PlayCard(Card.From(input.MainArg), input.SecondaryArg))
                            _ui.Action("Can't play with that card!", "Player");
                        break;
                    case "d":
                    case "discard":
                        _ui.Action($"Discarding card: {input.MainArg}", "Player");
                        if (_game.Player.DiscardCard(Card.From(input.MainArg), input.SecondaryArg))
                            AiPlay();
                        else
                            _ui.Action("Can't discard that card.", "Player");
                        break;
                    case "draw":
                        _game.Player.DrawHand();
                        break;
                }
            }
            catch (Exception exception)
            {
                _ui.Error(exception.Message, exception.StackTrace);
            }

            return true;
        }

        private static void AiPlay()
        {
            var cardsDrawn = _game.Ai.DrawHand();
            if (cardsDrawn > 0)
                _ui.Action($"drew {cardsDrawn} cards for their hand.", "AI");
            var canPlay = true;
            while (canPlay)
            {
                canPlay = _game.Ai.PlayCard();
                if (_game.Player.HasWon())
                {
                    canPlay = false;
                    _ui.Won("Ai");
                }
                else if (!canPlay)
                {
                    _game.Ai.FindAndDiscardCard();
                }
                Thread.Sleep(500);
            }

            _game.Player.DrawHand();
            _ui.CreatePlayScreen();
        }
    }
}
