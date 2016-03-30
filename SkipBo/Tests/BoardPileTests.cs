using NUnit.Framework;
using SkipBo.App;

namespace SkipBo.Tests
{
    [TestFixture]
    public class GivenAnEmptyCardPile
    {
        private BoardPile _subject;

        [SetUp]
        public void WhenAddingACard()
        {
            _subject = new BoardPile();
        }

        [TestCase("1")]
        [TestCase("S")]
        public void ThenItWillAddTheCard(string card)
        {
            Assert.True(_subject.CanAddCard(Card.From(card)));
            Assert.That(_subject.GetCurrentValue(), Is.EqualTo(1));
        }

        [TestCase("2")]
        public void ThenItWillNotAddTheCard(string card)
        {
            Assert.False(_subject.CanAddCard(Card.From(card)));
            Assert.That(_subject.GetCurrentValue(), Is.EqualTo(0));
        }
    }

    [TestFixture]
    public class GivenACardPileWithOneCards
    {
        private BoardPile _subject;

        [SetUp]
        public void WhenAddingACard()
        {
            _subject = new BoardPile();
            _subject.CardPile.Add(Card.From("1"));
        }

        [TestCase("2")]
        [TestCase("S")]
        public void ThenItWillAddTheCard(string card)
        {
            Assert.True(_subject.CanAddCard(Card.From(card)));
            Assert.That(_subject.GetCurrentValue(), Is.EqualTo(2));
        }

        [TestCase("1")]
        [TestCase("3")]
        [TestCase("12")]
        public void ThenItWillNotAddTheCard(string card)
        {
            Assert.False(_subject.CanAddCard(Card.From(card)));

            Assert.That(_subject.GetCurrentValue(), Is.EqualTo(1));
        }
    }

    [TestFixture]
    public class GivenACardPileWithSkipBoCards
    {
        private BoardPile _subject;

        [SetUp]
        public void WhenAddingACard()
        {
            _subject = new BoardPile();
            _subject.CardPile.Add(Card.From("1"));
            _subject.CardPile.Add(Card.From("S"));
            _subject.CardPile.Add(Card.From("S"));
        }

        [TestCase("4")]
        [TestCase("S")]
        public void ThenItWillAddTheCard(string card)
        {
            Assert.True(_subject.CanAddCard(Card.From(card)));
            Assert.That(_subject.GetCurrentValue(), Is.EqualTo(4));
        }

        [TestCase("1")]
        [TestCase("3")]
        [TestCase("12")]
        public void ThenItWillNotAddTheCard(string card)
        {
            Assert.False(_subject.CanAddCard(Card.From(card)));

            Assert.That(_subject.GetCurrentValue(), Is.EqualTo(3));
        }
    }
}
