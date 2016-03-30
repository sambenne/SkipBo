using System.Collections.Generic;
using System.Linq;

namespace SkipBo.App
{
    public class Human : Player
    {
        public List<string> GetDiscardsAsList()
        {
            return Discards.Select(discard => discard.Cards.Count > 0 ? $"{discard.Cards.Last().Name} ({discard.Cards.Count})" : string.Empty).ToList();
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
                    SideDeck.RemoveAt(0);

                return playCard;
            }

            if (HasCardInHand(card))
            {
                playCard = pileCount > -1 ? board.PlayCard(card, pileCount) : board.PlayCard(card);
                if (playCard)
                    Hand.RemoveAt(Hand.FindIndex(x => x.Value == card.Value));
            }

            var discardPile = HasCardInDiscards(card);
            if (discardPile > -1)
            {
                playCard = pileCount > -1 ? board.PlayCard(card, pileCount) : board.PlayCard(card);
                if (playCard)
                    Discards[discardPile].Cards.RemoveAt(Discards[discardPile].Cards.Count - 1);
            }

            return playCard;
        }

        public bool DiscardCard(Card card, string pile)
        {
            if (HasCardInHand(card))
            {
                if (pile == "")
                {
                    if (Discards[0].Cards.Count == 0)
                        Discards[0].Cards.Add(card);
                    else if (Discards[1].Cards.Count == 0)
                        Discards[1].Cards.Add(card);
                    else if (Discards[2].Cards.Count == 0)
                        Discards[2].Cards.Add(card);
                    else if (Discards[3].Cards.Count == 0)
                        Discards[3].Cards.Add(card);
                    else
                        Discards[0].Cards.Add(card);
                }
                else
                {
                    var pileNumber = pile == "" ? 0 : int.Parse(pile);
                    pileNumber--;
                    pileNumber = pileNumber > 0 && pileNumber < 4 ? pileNumber : 0;

                    Discards[pileNumber].Cards.Add(card);
                }
                Hand.RemoveAt(Hand.FindIndex(x => x.Value == card.Value));
                return true;
            }
            return true;
        }
    }
}