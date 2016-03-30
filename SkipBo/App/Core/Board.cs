using System;
using System.Collections.Generic;

namespace SkipBo.App.Core
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

        public static Board Fake()
        {
            return new Board();
        }
    }
}