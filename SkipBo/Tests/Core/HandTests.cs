using NUnit.Framework;
using SkipBo.App.Core;

namespace SkipBo.Tests.Core
{
    [TestFixture]
    public class GivenAHand
    {
        private Hand _subject;

        [SetUp]
        public void WhenHandIsFull()
        {
            _subject = new Hand();
            _subject.Cards.Add(Card.From(1));
            _subject.Cards.Add(Card.From(2));
            _subject.Cards.Add(Card.From(3));
            _subject.Cards.Add(Card.From(4));
            _subject.Cards.Add(Card.From(5));
        }

        [TestCase(1, true)]
        [TestCase(2, true)]
        [TestCase(3, true)]
        [TestCase(4, true)]
        [TestCase(5, true)]
        [TestCase(6, false)]
        public void ThenItChecksItsHand(int card, bool expected)
        {
            Assert.That(_subject.Has(Card.From(card)), Is.EqualTo(expected));
        }
    }

    [TestFixture]
    public class GivenACorectFormattedString
    {
        private Hand _subject;

        [SetUp]
        public void WhenCreatingAHand()
        {
            _subject = Hand.From("1,2,3,4,5");
        }

        [Test]
        public void ThenItCreatesTheHand()
        {
            Assert.That(_subject.Cards.Count, Is.EqualTo(5));
        }
    }

    [TestFixture]
    public class GivenAIncorectFormattedString
    {
        private Hand _subject;

        [SetUp]
        public void WhenCreatingAHand()
        {
            _subject = Hand.From("1,2,3,4,5,6");
        }

        [Test]
        public void ThenItCreatesTheHandWithATotalOfFiveCards()
        {
            Assert.That(_subject.Cards.Count, Is.EqualTo(5));
        }
    }

    [TestFixture]
    public class GivenACorectShortFormattedString
    {
        private Hand _subject;

        [SetUp]
        public void WhenCreatingAHand()
        {
            _subject = Hand.From("1,2,3");
        }

        [Test]
        public void ThenItCreatesTheHand()
        {
            Assert.That(_subject.Cards.Count, Is.EqualTo(3));
        }
    }

    [TestFixture]
    public class GivenACorectFormattedStringWithASkipBoCard
    {
        private Hand _subject;

        [SetUp]
        public void WhenCreatingAHand()
        {
            _subject = Hand.From("1,2,3,S,5");
        }

        [Test]
        public void ThenItCreatesTheHand()
        {
            Assert.That(_subject.Cards.Count, Is.EqualTo(5));
        }
    }

    [TestFixture]
    public class GivenACorectFormattedStringWithBadValue
    {
        private Hand _subject;

        [SetUp]
        public void WhenCreatingAHand()
        {
            _subject = Hand.From("1,2,3,13");
        }

        [Test]
        public void ThenItCreatesTheHand()
        {
            Assert.That(_subject.Cards.Count, Is.EqualTo(3));
        }
    }

    [TestFixture]
    public class GivenAHandThatHasCards
    {
        private Hand _subject;

        [SetUp]
        public void WhenCreatingAHand()
        {
            _subject = Hand.From("1,2,3");
        }

        [Test]
        public void ThenItCanAddACard()
        {
            Assert.That(_subject.Add(Card.From(2)), Is.EqualTo(true));
        }
    }

    [TestFixture]
    public class GivenAHandThatHasCardsThatWeWantToRemove
    {
        private Hand _subject;

        [SetUp]
        public void WhenCreatingAHand()
        {
            _subject = Hand.From("1,2,3");
        }

        [Test]
        public void ThenItCanAddACard()
        {
            _subject.Remove(Card.From(2));

            Assert.That(_subject.Cards.Count, Is.EqualTo(2));
        }
    }
}
