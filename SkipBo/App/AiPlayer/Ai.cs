using System.Collections.Generic;
using System.Linq;
using SkipBo.App.Core;

namespace SkipBo.App.AiPlayer
{
    public class Ai : Player
    {
        public bool PlayCard()
        {
            var board = Board.Instance;
            if (SideDeck.TotalCards() > 0 && board.PlayCard(SideDeck.CurrentCard()))
            {
                Ui.Action($"played: {SideDeck.CurrentCard().Name} from Side Deck.", "AI");
                return SideDeck.Remove(0);
            }

            if (Hand.TotalCards() == 0)
            {
                DrawHand();
                Ui.Action("drew 5 cards.", "AI");
            }

            foreach (var card in Hand.Cards.Where(card => board.PlayCard(card)))
            {
                Ui.Action($"played: {card.Name} from Hand.", "AI");
                Hand.Remove(card);
                return true;
            }

            foreach (var discards in Discards.Piles.Where(x => x.Cards.Count > 0))
            {
                var topCard = discards.Cards.Last();
                var discardPile = HasCardInDiscards(topCard);
                if (discardPile > -1 && board.PlayCard(topCard))
                {
                    Discards.Piles[discardPile].Cards.RemoveAt(Discards.Piles[discardPile].Cards.Count - 1);
                    return true;
                }
            }

            return false;
        }

        public void FindAndDiscardCard()
        {
            if(Hand.TotalCards() > 1)
                Hand.OrderCards();
            var card = Hand.Last();

            if (!Hand.Has(card)) return;

            Discards.Add(card);
            Ui.Action($"discarded {card.Name} from Hand.", "AI");
            Hand.Remove(card);
        }

        public List<string> GetDiscardsAsList()
        {
            return Discards.Piles.Select(discard => discard.Cards.Count > 0 ? discard.Cards.Last().Name : string.Empty).ToList();
        }
    }
}
