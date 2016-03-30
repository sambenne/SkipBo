using System.Collections.Generic;
using System.Linq;

namespace SkipBo.App.Core
{
    public class BoardPile
    {
        public List<Card> CardPile { get; set; } = new List<Card>();

        public bool IsFull()
        {
            return CardPile.Count == 12;
        }

        public bool CanAddCard(Card card)
        {
            if (card.Name == Cards.SkipBo)
            {
                CardPile.Add(card);
                return true;
            }

            if (CardPile.Count == 0)
            {
                if (card.Value != 1) return false;

                CardPile.Add(card);
                return true;
            }

            var lastCard = CardPile.Last();
            if (lastCard == null) return false;

            if (!lastCard.IsSkipBo() && !card.IsSkipBo() && (lastCard.Value + 1 == card.Value))
            {
                CardPile.Add(card);
                return true;
            }

            if (lastCard.IsSkipBo() && card.Value - 1 == GetCurrentValue())
            {
                CardPile.Add(card);
                return true;
            }

            return false;
        }

        public int GetCurrentValue()
        {
            return CardPile.Count;
        }

        public string GetLastCard()
        {
            if (CardPile.Count == 0)
                return string.Empty;

            if (CardPile.Last().IsSkipBo())
                return $"{Cards.SkipBo}={GetCurrentValue()}";

            return GetCurrentValue().ToString();
        }
    }
}