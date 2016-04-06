using System.Collections.Generic;
using SkipBo.App.Core;

namespace SkipBo.App.AiPlayer.Strategies
{
    public abstract class Strategy
    {
        public Board Board;
        public SideDeck SideDeck;
        public Hand Hand;
        public List<List<Card>> Discards;
        public SmartAi Player;
        public Sequence Sequence = new Sequence();
    }

    public class Sequence
    {
        public List<Play> Plays { get; set; } = new List<Play>();
        public void Add(Play play) => Plays.Add(play);
    }

    public class Play
    {
        public Card Card { get; set; }
        public BoardLocation BoardLocation { get; set; }

        public Play(Card card, BoardLocation boardLocation)
        {
            Card = card;
            BoardLocation = boardLocation;
        }
    }

    public enum BoardLocation
    {
        SideDeck = 0,
        Hand = 1,
        Discard1 = 2,
        Discard2 = 3,
        Discard3 = 4,
        Discard4 = 5
    }
}
