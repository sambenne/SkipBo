using System.Collections.Generic;
using NUnit.Framework;
using SkipBo.App.AiPlayer.Strategies;
using SkipBo.App.Core;

namespace SkipBo.Tests.SmartAi
{
    [TestFixture]
    public class SimpleSideDeckStrategyTests
    {
        private SideDeckStrategy _subject;
        private Board _board;
        private App.AiPlayer.SmartAi _player;

        [SetUp]
        public void SetUp()
        {
            _player = new App.AiPlayer.SmartAi();
            _board = Board.Fake();
            _board.BoardPiles[0] = new BoardPile { CardPile = new List<Card> { Card.From(1) } };

            _player.SideDeck = new SideDeck();
            _player.SideDeck.SetCards(new List<Card> { Card.From(2), Card.From(12) });
            _player.Hand = Hand.From("3,4,5,6,7");

            _subject = new SideDeckStrategy
            {
                Hand = _player.Hand,
                Discards = new List<List<Card>>(),
                Board = _board,
                Player = _player,
                SideDeck = _player.SideDeck
            };

            _subject.Execute();
        }

        [Test]
        public void Test()
        {
            Assert.That(_subject.Sequence.Plays.Count, Is.EqualTo(1));
        }
    }
}
