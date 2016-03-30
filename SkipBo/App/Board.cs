using System;
using System.Collections.Generic;
using System.Linq;

namespace SkipBo.App
{
    public class Board
    {
        private static readonly Lazy<Board> InstanceOfBoard = new Lazy<Board>(() => new Board());
        public static Board Instance => InstanceOfBoard.Value;

        public List<BoardPile> BoardPiles { get; set; } = new List<BoardPile>();

        public Board()
        {
            BoardPiles.Add(new BoardPile());
            BoardPiles.Add(new BoardPile());
            BoardPiles.Add(new BoardPile());
            BoardPiles.Add(new BoardPile());
        }

        public bool PlayCard(Card card, int pile)
        {
            pile--;
            return AddCardToPile(pile, card);
        }

        public bool PlayCard(Card card)
        {
            if (AddCardToPile(0, card))
                return true;

            if (AddCardToPile(1, card))
                return true;

            if (AddCardToPile(2, card))
                return true;

            if (AddCardToPile(3, card))
                return true;

            return false;
        }

        public bool AddCardToPile(int pile, Card card)
        {
            var deck = Deck.Instance;
            var addedCard = BoardPiles[pile].CanAddCard(card);

            if (addedCard && BoardPiles[pile].IsFull())
            {
                deck.AddSet(BoardPiles[pile].CardPile);
                BoardPiles[pile].CardPile = new List<Card>();
                return true;
            }

            return addedCard;
        }

        public List<string> GetTopCards()
        {
            return new List<string>
            {
                BoardPiles[0].GetLastCard(),
                BoardPiles[1].GetLastCard(),
                BoardPiles[2].GetLastCard(),
                BoardPiles[3].GetLastCard()
            };
        }
    }

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