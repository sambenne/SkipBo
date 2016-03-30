using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using SkipBo.App.AiPlayer;

namespace SkipBo.App
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
            BuildAiPlayer(false);

            BuildPlayerDiscards(40);
            const int left = 0;

            Write(left, 0, "Side Deck:");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Write(11, 0, $"[{Player.SideDeck[0].Name}]");
            Console.ResetColor();
            Write(16, 0, $"({Player.SideDeck.Count} left)");
            Write(left, 1, "Hand:");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Write(6, 1, Player.Hand.Aggregate("", (current, t) => current + $"[{t.Name}] "));
            Console.ResetColor();
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
            Write(left, 2, $"D:{Ai.SideDeck[0].Name} ({Ai.SideDeck.Count} left)");
            if (debug)
            {
                Write(left, 3, Ai.Hand.Aggregate("Hand: ", (current, t) => current + $"[{t.Name}] "));
            }
            Write(left, debug ? 4 : 3, $"Discard: [{string.Join("][", Ai.GetDiscardsAsList())}]");
        }

        private static void Write(int left, int top, string message)
        {
            Console.SetCursorPosition(left, top);
            Console.Write(message);
        }

        private void BuildPlayerDiscards(int left)
        {
            Write(left, 0, "Discards:");
            left += 10;
            var column = 0;
            foreach (var discardPile in Player.Discards)
            {
                var row = 0;
                var leftPosition = left + (column * 5);
                discardPile.Cards.Reverse();
                foreach (var card in discardPile.Cards)
                {
                    if (row == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Write(leftPosition, row, $"[{card.Name}]");
                        Console.ResetColor();
                    }
                    else
                    {
                        Write(leftPosition, row, $"[{card.Name}]");
                    }
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
    }
}
