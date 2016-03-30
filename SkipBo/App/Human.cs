using System.Collections.Generic;
using System.Linq;
using SkipBo.App.Core;

namespace SkipBo.App
{
    public class Human : Player
    {
        public List<string> GetDiscardsAsList()
        {
            return Discards.Piles.Select(discard => discard.Cards.Count > 0 ? $"{discard.Cards.Last().Name} ({discard.Cards.Count})" : string.Empty).ToList();
        }

        public bool PlayCard(Card card, string pile)
        {
            int pileCount;
            if (!int.TryParse(pile, out pileCount))
                pileCount = -1;

            var board = Board.Instance;
            var playCard = false;
            if (HasCardInSideDeck(card))
            {
                playCard = pileCount > -1 ? board.PlayCard(card, pileCount) : board.PlayCard(card);
                if (playCard)
                    SideDeck.Remove(0);

                return playCard;
            }

            if (HasCardInHand(card))
            {
                playCard = pileCount > -1 ? board.PlayCard(card, pileCount) : board.PlayCard(card);
                if (playCard)
                    Hand.Remove(card);
            }

            var discardPile = HasCardInDiscards(card);
            if (discardPile > -1)
            {
                playCard = pileCount > -1 ? board.PlayCard(card, pileCount) : board.PlayCard(card);
                if (playCard)
                    Discards.Remove(pileCount);
            }

            return playCard;
        }

        public bool DiscardCard(Card card, string pile)
        {
            if (!Hand.Has(card)) return false;

            if (pile == "")
                Discards.Add(card);
            else
            {
                var pileNumber = pile == "" ? 0 : int.Parse(pile);
                pileNumber--;
                pileNumber = pileNumber > 0 && pileNumber < 4 ? pileNumber : 0;

                Discards.Add(card, pileNumber);
            }
            return Hand.Remove(card);
        }
    }
}