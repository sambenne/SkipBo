using System.Collections.Generic;
using NUnit.Framework;
using SkipBo.App.AiPlayer;
using SkipBo.App.Core;

namespace SkipBo.Tests.SmartAi
{
    [TestFixture]
    public class FindSequenceTests
    {
        public Card SideDeck;
        public List<Card> Hand = new List<Card>();
        public List<List<Card>> Discards = new List<List<Card>>();
        public List<Card> Board = new List<Card>();
        private Brain _subject;

        [SetUp]
        public void SetUp()
        {
            SideDeck = Card.From(3);

            Hand.Add(Card.From(Cards.SkipBo));
            Hand.Add(Card.From(4));
            Hand.Add(Card.From(5));
            Hand.Add(Card.From(6));
            Hand.Add(Card.From(11));

            Board.Add(Card.From(1));

            _subject = new Brain(Board, SideDeck, Hand, Discards);
        }

        [Test]
        public void FindSequence()
        {
            var result = _subject.FindSequence();

            var expected = new List<Card>
            {
                Card.From(Cards.SkipBo),
                Card.From(3),
                Card.From(4),
                Card.From(5),
                Card.From(6)
            };

            Assert.That(result.Count, Is.EqualTo(5));
            CollectionAssert.AreEqual(expected, result);
        }
    }

    [TestFixture]
    public class FindSequenceTests2
    {
        public Card SideDeck;
        public List<Card> Hand = new List<Card>();
        public List<List<Card>> Discards = new List<List<Card>>();
        public List<Card> Board = new List<Card>();
        private Brain _subject;

        [SetUp]
        public void SetUp()
        {
            SideDeck = Card.From(3);

            Hand.Add(Card.From(Cards.SkipBo));
            Hand.Add(Card.From(4));
            Hand.Add(Card.From(9));
            Hand.Add(Card.From(6));
            Hand.Add(Card.From(11));

            Board.Add(Card.From(1));
            Board.Add(Card.From(7));

            Discards.Add(new List<Card> {Card.From(12) });
            Discards.Add(new List<Card> {Card.From(11) });
            Discards.Add(new List<Card> { Card.From(8), Card.From(5)});

            _subject = new Brain(Board, SideDeck, Hand, Discards);
        }

        [Test]
        public void FindSequence()
        {
            var result = _subject.FindSequence();

            var expected = new List<Card>
            {
                Card.From(Cards.SkipBo),
                Card.From(3),
                Card.From(4),
                Card.From(5),
                Card.From(6)
            };

            Assert.That(result.Count, Is.EqualTo(5));
            CollectionAssert.AreEqual(expected, result);
        }
    }
}
