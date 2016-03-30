using System.Collections.Generic;
using SkipBo.App.Core;

namespace SkipBo.App.AiPlayer.Strategies
{
    public abstract class Strategy
    {
        public Board Board;
        public Card SideDeck;
        public Hand Hand;
        public List<List<Card>> Discards;
        public SmartAi Player;
    }
}
