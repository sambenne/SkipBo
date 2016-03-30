using System.Collections.Generic;
using System.Linq;

namespace SkipBo.App.AiPlayer
{
    public class Ai : Player
    {
        public bool PlayCard()
        {
            var board = Board.Instance;
            if (SideDeck.Count > 0)
            {
                if (board.PlayCard(SideDeck[0]))
                {
                    Ui.Action($"played: {SideDeck[0].Name} from Side Deck.", "AI");
                    SideDeck.RemoveAt(0);
                    return true;
                }
            }

            if (Hand.Count == 0)
            {
                DrawHand();
                Ui.Action("drew 5 cards.", "AI");
            }

            for (var i = 0; i < Hand.Count; i++)
            {
                if (board.PlayCard(Hand[i]))
                {
                    Ui.Action($"played: {Hand[i].Name} from Hand.", "AI");
                    Hand.RemoveAt(i);
                    return true;
                }
            }

            foreach (var discards in Discards.Where(x => x.Cards.Count > 0))
            {
                var topCard = discards.Cards.Last();
                var discardPile = HasCardInDiscards(topCard);
                if (discardPile > -1 && board.PlayCard(topCard))
                {
                    Discards[discardPile].Cards.RemoveAt(Discards[discardPile].Cards.Count - 1);
                    return true;
                }
            }

            return false;
        }

        public void FindAndDiscardCard()
        {
            if(Hand.Count > 1)
                Hand = Hand.OrderBy(x => x.Value).ToList();
            var last = Hand.Last();
            var pileNumber = 0;

            if (HasCardInHand(last))
            {
                pileNumber = pileNumber > 0 && pileNumber < 4 ? pileNumber : 0;

                Discards[pileNumber].Cards.Add(last);
                Ui.Action($"discarded {last.Name} from Hand.", "AI");
                Hand.RemoveAt(Hand.FindIndex(x => x.Value == last.Value));
            }
        }

        public List<string> GetDiscardsAsList()
        {
            return Discards.Select(discard => discard.Cards.Count > 0 ? discard.Cards.Last().Name : string.Empty).ToList();
        }
    }

    public class SmartAi : Player
    {
        public void PlayCard()
        {
            
        }
    }
}
