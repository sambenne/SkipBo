using System.Collections.Generic;
using NUnit.Framework;
using SkipBo.App.AiPlayer.Strategies;
using SkipBo.App.Core;

namespace SkipBo.Tests.SmartAi
{
    [TestFixture]
    public class SideDeckStrategyTests
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
            _board.BoardPiles[1] = new BoardPile { CardPile = new List<Card> { Card.From(5) } };

            _player.SideDeck = new SideDeck();
            _player.SideDeck.SetCards(new List<Card> { Card.From(4), Card.From(9) });
            _player.Hand = Hand.From("2,3,4,5,6");

            _subject = new SideDeckStrategy
            {
                Hand = Hand.From("2,3,4,5,6"),
                Discards = new List<List<Card>>(),
                Board = _board,
                Player = new App.AiPlayer.SmartAi()
            };

            _subject.Execute();
        }

        [Test]
        public void Test()
        {
            Assert.That(_board.BoardPiles[0].GetCurrentValue(), Is.EqualTo(6));
        }
    }
}
