using System;
using System.Collections.Generic;
using SkipBo.Tests.SmartAi;

namespace SkipBo.App
{
    public class Card
    {
        public string Name { get; set; }
        public int Value { get; set; }

        private static readonly List<string> AcceptedCards = new List<string> {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "S" }; 
        public static Card From(string input)
        {
            input = input.ToUpper();
            if (AcceptedCards.Contains(input))
            {
                int parsed;
                if (!int.TryParse(input, out parsed))
                    parsed = -1;

                return new Card { Name = input, Value = parsed };
            }

            throw new Exception($"Unable to created card from {input}");
        }

        public static Card From(int input)
        {
            return From(input.ToString());
        }

        public bool IsSkipBo()
        {
            return Name == Cards.SkipBo;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Card;
            return Value == other?.Value;
        }
    }

    public struct Cards
    {
        public const string SkipBo = "S";
    }
}