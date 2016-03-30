using System;
using SkipBo.App.AiPlayer;

namespace SkipBo.App.Core
{
    public class Game
    {
        public int Length = 10;
        public Difficulty Difficulty;
        public Deck Deck = Deck.Instance;
        public Human Player = new Human();
        public Ai Ai = new Ai();

        public Game(Difficulty difficulty)
        {
            Difficulty = difficulty;
        }

        public void SetUp()
        {
            Deck.Load();
            Deck.Shuffle();

            Player.FillSideDeck(Length);
            Player.DrawHand();
            Ai.FillSideDeck(Length);
            Ai.DrawHand();

        }

        public void GetGameLength()
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
                    Length = length;
                    gotInput = true;
                }
                else
                {
                    if (input != "short" && input != "medium" && input != "long")
                        continue;
                    if (input == "short")
                        Length = 10;
                    else if (input == "medium")
                        Length = 20;
                    else if (input == "long")
                        Length = 30;
                    gotInput = true;
                }
            }
        }
    }

    public enum Difficulty
    {
        Easy = 1,
        Hard = 2
    }
}
