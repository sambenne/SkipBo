using System.Linq;

namespace SkipBo.App.AiPlayer.Strategies
{
    public class SideDeckStrategy : Strategy, IStrategy
    {
        public void Execute()
        {
            // Can I play my side deck card
                // Yes - play it and check the next card
                // No - Which pile has the shortest amount of cards to get to it.
                    // Check if card is higher than any on board
                    // If it is higher get the shortest journey
                        // With shortest journey work out what cards I have and need
                        // Easiest - Do I have cards in my hand and top level of discards - play them
                        // Harder - Do I have cards under top layer of the discards
                            // Can I use the cards on top
                                // Yes - Play them then play the cards needed for the side deck
                                // No - Don't use pile as discard, Move on to next strategy

            if (CanPlaySideDeckCard())
            {
                // Play the side deck card
                Player.RemoveSideDeckCard();
            }
            else
            {
                // Do we need cards to play
                var sideDeckPlay = SideDeckCardNeedsMoreCards();
                if (sideDeckPlay.CanPlay)
                {
                    var cardsToPlay = sideDeckPlay.CardsNeeded;
                    // Look at hand for cards
                }
            }
        }

        public bool CanPlaySideDeckCard()
        {
            return Board.BoardPiles.Any(pile => pile.GetCurrentValue() + 1 == SideDeck.Value);
        }

        public SideDeckInformation SideDeckCardNeedsMoreCards()
        {
            var response = new SideDeckInformation();

            foreach (var boardPile in Board.BoardPiles.Where(x => x.GetCurrentValue() + 1 < SideDeck.Value))
            {
                var totalCardsNeeded = SideDeck.Value - boardPile.GetCurrentValue();
                if (response.CardsNeeded == 0 || totalCardsNeeded < response.CardsNeeded)
                {
                    response.CardsNeeded = totalCardsNeeded - 1;
                    response.CanPlay = true;
                }
            }

            return response;
        }
    }

    public class SideDeckInformation
    {
        public bool CanPlay { get; set; } = false;
        public int CardsNeeded { get; set; }
    }
}
