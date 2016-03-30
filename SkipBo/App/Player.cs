using System.Linq;
using SkipBo.App.ConsoleHelper;
using SkipBo.App.Core;

namespace SkipBo.App
{
    public abstract class Player
    {
        public Hand Hand { get; set; } = new Hand();
        public SideDeck SideDeck { get; set; } = new SideDeck();
        public Discards Discards { get; set; } = new Discards();

        public readonly Deck Deck = Deck.Instance;
        public readonly Ui Ui = Ui.Instance;

        public int DrawHand()
        {
            if (Hand.TotalCards() == 5)
                return 0;

            var totalCardsNeeded = 5 - Hand.TotalCards();
            Hand.Add(Deck.Cards.GetRange(0, totalCardsNeeded));
            Deck.Cards.RemoveRange(0, totalCardsNeeded);
            return totalCardsNeeded;
        }

        public void FillSideDeck(int totalCards)
        {
            SideDeck.SetCards(Deck.Cards.Take(totalCards).ToList());
            Deck.Cards.RemoveRange(0, totalCards);
        }

        public bool HasCardInHand(Card card)
        {
            return Hand.Has(card);
        }

        public bool HasCardInSideDeck(Card card)
        {
            return SideDeck.IsCurrentCard(card);
        }

        public int HasCardInDiscards(Card card)
        {
            return Discards.HasCard(card);
        }

        public bool HasWon()
        {
            return SideDeck.TotalCards() == 0;
        }
    }
}