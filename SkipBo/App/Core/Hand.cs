using System;
using System.Collections.Generic;
using System.Linq;

namespace SkipBo.App.Core
{
    public class Hand
    {
        public List<Card> Cards { get; set; } = new List<Card>();

        public bool Has(Card card)
        {
            return Cards.Contains(card);
        }

        public bool Remove(Card card)
        {
            var totalCards = Cards.Count;
            if (!Has(card)) return false;

            Cards.RemoveAt(Cards.FindIndex(x => x.Value == card.Value));
            return Cards.Count < totalCards;
        }

        public bool Add(Card card)
        {
            if (Cards.Count == 5) return false;

            Cards.Add(card);
            return true;
        }

        public void Add(List<Card> newCards)
        {
            foreach (var newCard in newCards)
                Add(newCard);
        }

        public int TotalCards()
        {
            return Cards.Count;
        }

        public void OrderCards()
        {
            Cards = Cards.OrderBy(x => x.Value).ToList();
        }

        public Card Last()
        {
            return Cards.Last();
        }

        public static Hand From(string cards)
        {
            var hand = new Hand();
            var separateCards = cards.Split(',');
            for (var i = 0; i < 5; i++)
            {
                if (separateCards.Length <= i) continue;
                try
                {
                    var card = Card.From(separateCards[i]);
                    hand.Add(card);
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            return hand;
        }
    }
}
