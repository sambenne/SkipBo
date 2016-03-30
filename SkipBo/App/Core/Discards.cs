using System.Collections.Generic;
using System.Linq;

namespace SkipBo.App.Core
{
    public class Discards
    {
        public List<Discard> Piles { get; set; } = new List<Discard> {
            new Discard(),
            new Discard(),
            new Discard(),
            new Discard()
        };

        public int HasCard(Card card)
        {
            if (HasCard(0, card))
                return 0;
            if (HasCard(1, card))
                return 1;
            if (HasCard(2, card))
                return 2;
            if (HasCard(3, card))
                return 3;

            return -1;
        }

        public bool HasCard(int pileNumber, Card card)
        {
            return Piles[pileNumber].Cards.Count > 0 && Piles[pileNumber].Cards.Last().Value == card.Value;
        }

        public void Add(Card card)
        {
            if (Piles[0].Cards.Count == 0)
                Piles[0].Cards.Add(card);
            else if (Piles[1].Cards.Count == 0)
                Piles[1].Cards.Add(card);
            else if (Piles[2].Cards.Count == 0)
                Piles[2].Cards.Add(card);
            else if (Piles[3].Cards.Count == 0)
                Piles[3].Cards.Add(card);
            else
                Piles[0].Cards.Add(card);
        }

        public void Add(Card card, int pileIndex)
        {
            Piles[pileIndex].Cards.Add(card);
        }

        public void Remove(int pileIndex)
        {
            Piles[pileIndex].Cards.RemoveAt(Piles[pileIndex].Cards.Count - 1);
        }
    }
}
