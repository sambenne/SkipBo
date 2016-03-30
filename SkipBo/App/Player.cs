using System.Collections.Generic;
using System.Linq;

namespace SkipBo.App
{
    public abstract class Player
    {
        public List<Card> Hand { get; set; } = new List<Card>();
        public List<Card> SideDeck { get; set; } = new List<Card>();
        public List<Discard> Discards { get; set; } = new List<Discard>
        {
            new Discard(),
            new Discard(),
            new Discard(),
            new Discard()
        };
        public readonly Deck Deck = Deck.Instance;
        public readonly Ui Ui = Ui.Instance;

        public int DrawHand()
        {
            if (Hand.Count == 5)
                return 0;

            var totalCardsNeeded = 5 - Hand.Count;
            var newCards = Deck.Cards.GetRange(0, totalCardsNeeded);
            Hand.AddRange(newCards);
            Deck.Cards.RemoveRange(0, totalCardsNeeded);
            return totalCardsNeeded;
        }

        public void FillSideDeck(int totalCards)
        {
            SideDeck = Deck.Cards.Take(totalCards).ToList();
            Deck.Cards.RemoveRange(0, totalCards);
        }

        public bool HasCardInHand(Card card)
        {
            return Hand.Any(x => x.Value == card.Value);
        }

        public bool HasCardInSideDeck(Card card)
        {
            return SideDeck[0].Value == card.Value;
        }

        public int HasCardInDiscards(Card card)
        {
            if (Discards[0].Cards.Count > 0 && Discards[0].Cards.Last().Value == card.Value)
                return 0;
            if (Discards[1].Cards.Count > 0 && Discards[1].Cards.Last().Value == card.Value)
                return 1;
            if (Discards[2].Cards.Count > 0 && Discards[2].Cards.Last().Value == card.Value)
                return 2;
            if (Discards[3].Cards.Count > 0 && Discards[3].Cards.Last().Value == card.Value)
                return 3;

            return -1;
        }

        public bool HasWon()
        {
            return SideDeck.Count == 0;
        }
    }
}