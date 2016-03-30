using System.Collections.Generic;

namespace SkipBo.App.Core
{
    public class SideDeck
    {
        public List<Card> Cards { get; set; } = new List<Card>();

        public void SetCards(List<Card> cards)
        {
            Cards = cards;
        }

        public Card CurrentCard()
        {
            return Cards[0];
        }

        public bool IsCurrentCard(Card card)
        {
            return Cards[0].Value == card.Value;
        }

        public int TotalCards()
        {
            return Cards.Count;
        }

        public bool Remove(int index)
        {
            var totalCards = Cards.Count;
            Cards.RemoveAt(index);
            return Cards.Count < totalCards;
        }
    }
}
