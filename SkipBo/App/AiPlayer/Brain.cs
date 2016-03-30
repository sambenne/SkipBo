using System.Collections.Generic;
using System.Linq;

namespace SkipBo.App.AiPlayer
{
    public class Brain
    {
        private readonly List<Card> _board;
        private readonly Card _sideDeck;
        private readonly List<Card> _hand;
        private readonly List<List<Card>> _discards;

        public Brain(List<Card> board, Card sideDeck, List<Card> hand, List<List<Card>> discards)
        {
            _board = board;
            _sideDeck = sideDeck;
            _hand = hand;
            _discards = discards;
        }

        public List<Card> FindSequence()
        {
            var sequence = new List<Card>();
            foreach (var boardCard in _board)
            {
                var currentBoardValue = boardCard.Value;
                
                var nextCard = currentBoardValue + 1;
                var howManyCardsAreRequired = _sideDeck.Value - currentBoardValue;
                if (howManyCardsAreRequired == 0)
                    sequence.Add(_sideDeck);
                else if (howManyCardsAreRequired > 0)
                {
                    var checkedAllCards = false;
                    var totalSkipBoCards = _hand.Count(x => x.Name == Cards.SkipBo);
                    var usedSkipBoCards = 0;
                    while (!checkedAllCards)
                    {
                        var checkSideDeck = CheckSideDeck(nextCard);
                        if (checkSideDeck != null)
                        {
                            sequence.Add(checkSideDeck);
                            nextCard++;
                            continue;
                        }

                        if (_hand.Any(x => x.Value == nextCard))
                        {
                            sequence.Add(_hand.First(x => x.Value == nextCard));
                            nextCard++;
                            continue;
                        }

                        if (_discards.Any(x => x.First().Value == nextCard))
                        {
                            sequence.Add(_discards.First(x => x.First().Value == nextCard).First());
                            nextCard++;
                            continue;
                        }

                        var subSequence = FindCardInDiscards(nextCard);
                        if (subSequence.Count > 0)
                        {
                            sequence.AddRange(subSequence);
                            nextCard++;
                            continue;
                        }

                        if (totalSkipBoCards != usedSkipBoCards)
                        {
                            sequence.Add(Card.From(Cards.SkipBo));
                            nextCard++;
                            usedSkipBoCards++;
                            continue;
                        }

                        checkedAllCards = true;
                    }
                }
            }
            // Side Deck is too low
            return sequence;
        }

        public Card CheckSideDeck(int nextCard)
        {
            return _sideDeck.Value == nextCard ? _sideDeck : null;
        }

        public List<Card> FindCardInDiscards(int nextCard)
        {
            foreach (var cards in _discards.Where(cards => cards.Count >= 2))
            {
                if (_board.Any(x => x.Value == cards[0].Value - 1) && cards[1].Value == nextCard)
                {
                    return new List<Card>
                    {
                        cards[0],
                        cards[1]
                    };
                }
            }

            return new List<Card>();
        }
    }
}
