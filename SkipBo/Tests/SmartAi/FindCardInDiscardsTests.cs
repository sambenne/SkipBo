using System.Collections.Generic;
using NUnit.Framework;
using SkipBo.App;
using SkipBo.App.AiPlayer;

namespace SkipBo.Tests.SmartAi
{
    [TestFixture]
    public class FindCardInDiscardsTests
    {
        private Brain _subject;

        [SetUp]
        public void SetUp()
        {
            var board = new List<Card>
            {
                Card.From(1),
                Card.From(5)
            };
            var discards = new List<List<Card>>
            {
                new List<Card>
                {
                    Card.From(6),
                    Card.From(2)
                }
            };
            _subject = new Brain(board, Card.From(3), new List<Card>(), discards);
        }

        [Test]
        public void ThenItReturnsTheCorrectSequence()
        {
            var result = _subject.FindCardInDiscards(2);
            var expected = new List<Card>
            {
                Card.From(6),
                Card.From(2)
            };
            CollectionAssert.AreEqual(expected, result);
        }
    }
}
