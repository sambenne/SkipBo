using System;
using System.Collections.Generic;
using System.Linq;
using SkipBo.App.AiPlayer;
using SkipBo.App.Core;

namespace SkipBo.App.ConsoleHelper
{
    public class Ui
    {
        private static readonly Lazy<Ui> _instance = new Lazy<Ui>(() => new Ui());
        private readonly Board _board;
        public static Ui Instance => _instance.Value;

        public Human Player { get; set; }
        public Ai Ai { get; set; }

        public List<string> ActionHistory { get; set; } = new List<string>();

        public Ui()
        {
            _board = Board.Instance;
        }

        public bool HasWon()
        {
            if (Player.HasWon())
            {
                Won("Player");
                return true;
            }
            if (Ai.HasWon())
            {
                Won("Ai");
                return true;
            }

            return false;
        }

        public void CreatePlayScreen()
        {
            Console.Clear();
            if (HasWon())
                return;
            BuildAiPlayer(true);

            BuildPlayerDiscards(40, 0, Player.Discards, ConsoleColor.DarkGreen);
            const int left = 0;

            Write(left, 0, "Side Deck:");
            Write(11, 0, $"[{Player.SideDeck.CurrentCard().Name}]", ConsoleColor.DarkGreen);
            Write(16, 0, $"({Player.SideDeck.TotalCards()} left)");
            Write(left, 1, "Hand:");
            Write(6, 1, Player.Hand.Cards.Aggregate("", (current, t) => current + $"[{t.Name}] "), ConsoleColor.DarkGreen);
            Write(left, 4, "Board");
            Console.ForegroundColor = ConsoleColor.Blue;
            Write(left, 5, $"[{string.Join("][", _board.GetTopCards())}]");
            Console.ResetColor();
            Write(left, 9, "--------------------------------------");
            Write(left, 10, "History");
            ActionHistory.Reverse();
            Console.SetCursorPosition(left, 12);
            foreach (var log in ActionHistory)
            {
                if (log.Contains("Error - "))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{log}\n");
                    Console.ResetColor();
                }
                else
                    Console.WriteLine($"{log}\n");
            }
            ActionHistory.Reverse();

            Write(left, 7, " > ");
            Console.SetCursorPosition(3, 7);
            Console.SetWindowPosition(0, 0);
        }

        private void BuildAiPlayer(bool debug)
        {
            const int left = 70;
            Write(left, 0, "AI");
            Write(left, 1, "----------------------");
            Write(left, 2, $"D:{Ai.SideDeck.CurrentCard().Name} ({Ai.SideDeck.TotalCards()} left)");
            if (debug)
            {
                Write(left, 3, Ai.Hand.Cards.Aggregate("Hand: ", (current, t) => current + $"[{t.Name}] "));
                BuildPlayerDiscards(left, 4, Ai.Discards, ConsoleColor.White);
            }
            else
            {
                Write(left, 3, $"Discard: [{string.Join("][", Ai.GetDiscardsAsList())}]");
            }
        }

        public static void Write(int left, int top, string message)
        {
            Write(left, top, message, ConsoleColor.White);
        }

        public static void Write(int left, int top, string message, ConsoleColor colour)
        {
            Console.SetCursorPosition(left, top);
            Console.ForegroundColor = colour;
            Console.Write(message);
            Console.ResetColor();
        }

        private static void BuildPlayerDiscards(int left, int top, Discards discards, ConsoleColor colour)
        {
            Write(left, top, "Discards:");
            left += 10;
            var column = 0;
            foreach (var discardPile in discards.Piles)
            {
                var row = top;
                var leftPosition = left + (column * 5);
                discardPile.Cards.Reverse();
                foreach (var card in discardPile.Cards)
                {
                    if (row == top)
                        Write(leftPosition, row, $"[{card.Name}]", colour);
                    else
                        Write(leftPosition, row, $"[{card.Name}]");
                    row++;
                }
                discardPile.Cards.Reverse();
                column++;
            }
        }

        public void Action(string message, string player)
        {
            ActionHistory.Add($"{player} {message}");
            CreatePlayScreen();
        }

        public void Won(string player)
        {
            if (player == "Ai")
                Console.WriteLine("Sorry you lost!");
            else
                Console.WriteLine("YOU WON!!!!");
        }

        public void Error(string message, string stackTrace)
        {
            ActionHistory.Add($"Error - {message}");
            ActionHistory.Add($"{stackTrace}");
            CreatePlayScreen();
        }

        public static void GetInput(Func<string, bool> action)
        {
            string line;
            var isRunning = true;

            while (isRunning && !string.IsNullOrEmpty(line = Console.ReadLine()))
            {
                if (line == "quit")
                    Environment.Exit(0);

                isRunning = action.Invoke(line);
            }
        }
    }
}
